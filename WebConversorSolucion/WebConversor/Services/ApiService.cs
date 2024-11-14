using System.Text.Json;
namespace WebConversor.Services
{

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

        public async Task<Dictionary<string, object>> GetDataFromApiAsync(string fromCurrency, string toCurrency,
            int amount)
        {

            var response =
                await _httpClient.GetAsync(
                    $"https://v6.exchangerate-api.com/v6/{_apiKey}/pair/{fromCurrency}/{toCurrency}/{amount}");

            response.EnsureSuccessStatusCode(); // Lanza una excepción si la solicitud falla

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer
                .Deserialize<Dictionary<string, object>>(content); // Deserializa JSON a un objeto genérico

            return data;
        }
        // Nuevo método para obtener datos históricos de la moneda
        public async Task<Dictionary<string, object>> GetDataFromApiAsync(string fromCurrency, string toCurrency)
        {

            // Convierte las fechas a cadenas en el formato esperado por la API
            string startDateStr = ("1999-01-01");
            string endDateStr = ("2024-11-1");

            var response = await _httpClient.GetAsync(
                $"https://www.alphavantage.co/query?function=FX_DAILY&from_symbol={fromCurrency}&to_symbol={toCurrency}&apikey={_apiKey2}&start_date={startDateStr}&end_date={endDateStr}");

            response.EnsureSuccessStatusCode();  // Lanza una excepción si la solicitud falla

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(content);  // Deserializa JSON a un objeto genérico

            return data;
        }
    }
}