using System.Text.Json;
namespace WebConversor.Services
{
    


    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;


        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<Dictionary<string, object>> GetDataFromApiAsync()
        {
            var response = await _httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/ditto");
            response.EnsureSuccessStatusCode(); // Lanza una excepción si la solicitud falla

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(content); // Deserializa JSON a un objeto genérico

            return data;
        }
    }
}
