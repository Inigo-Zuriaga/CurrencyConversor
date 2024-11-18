namespace WebConversor.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _apiKey2;


    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiKey = Environment.GetEnvironmentVariable("API_KEY");
        _apiKey2 = Environment.GetEnvironmentVariable("API_KEY2");
    }

    // Método para obtener datos actuales de conversión de moneda
    public async Task<Dictionary<string, object>> GetDataFromApiAsync(string fromCurrency, string toCurrency,
        int amount)
    {
        // Construye la URL con los parámetros necesarios
        var response =
            await _httpClient.GetAsync(
                $"https://v6.exchangerate-api.com/v6/{_apiKey}/pair/{fromCurrency}/{toCurrency}/{amount}");

        response.EnsureSuccessStatusCode(); // Lanza una excepción si la solicitud falla

        var content = await response.Content.ReadAsStringAsync(); // Obtiene el contenido de la respuesta
        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(content); // Deserializa JSON a un objeto genérico

        return data; // Devuelve los datos deserializados
    }

    // Nuevo método para obtener datos históricos para el gráfico de las monedas
    public async Task<Dictionary<string, object>> GetDataFromApiAsync(string fromCurrency, string toCurrency)
    {
        // Define el rango de fechas para los datos históricos.
        string startDate = ("1999-01-01");
        string endDate = ("2024-11-01");

        // Construye la URL con los parámetros necesarios
        var response = await _httpClient.GetAsync(
            $"https://www.alphavantage.co/query?function=FX_DAILY&from_symbol={fromCurrency}&to_symbol={toCurrency}&apikey={_apiKey2}&start_date={startDate}&end_date={endDate}");

        response.EnsureSuccessStatusCode();  // Lanza una excepción si la solicitud falla

        var content = await response.Content.ReadAsStringAsync(); // Obtiene el contenido de la respuesta
        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(content);  // Deserializa JSON a un objeto genérico

        return data; // Devuelve los datos deserializados
    }
}