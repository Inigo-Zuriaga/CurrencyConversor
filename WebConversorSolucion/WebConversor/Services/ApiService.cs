using System.Text.Json;
namespace WebConversor.Services
{
    


    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("API_KEY");
        }
        
        public async Task<Dictionary<string, object>> GetDataFromApiAsync(string fromCurrency, string toCurrency)
        {
            // var response = await _httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/ditto");
            var response = await _httpClient.GetAsync($"https://v6.exchangerate-api.com/v6/{_apiKey}/pair/{fromCurrency}/{toCurrency}");

            response.EnsureSuccessStatusCode(); // Lanza una excepción si la solicitud falla

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(content); // Deserializa JSON a un objeto genérico

            return data;
        }
    }
}
