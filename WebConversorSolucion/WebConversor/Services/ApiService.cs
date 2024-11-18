using System.Text.Json;
using WebConversor.Services.Interfaces;
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
        // Nuevo método para obtener datos históricos para el gráfico de las monedas
        public async Task<Dictionary<string, object>> GetDataFromApiAsync(string fromCurrency, string toCurrency)
        {

            string startDate = ("1999-01-01");
            string endDate = ("2024-11-01");

            var response = await _httpClient.GetAsync(
                $"https://www.alphavantage.co/query?function=FX_DAILY&from_symbol={fromCurrency}&to_symbol={toCurrency}&apikey={_apiKey2}&start_date={startDate}&end_date={endDate}");

            response.EnsureSuccessStatusCode();  // Lanza una excepción si la solicitud falla

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(content);  // Deserializa JSON a un objeto genérico

            return data;
        }
    }
}