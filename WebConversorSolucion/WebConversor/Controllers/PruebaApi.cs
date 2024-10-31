using Microsoft.AspNetCore.Mvc;

namespace WebConversor.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PruebaApi : Controller
{
    private readonly IApiService _apiService;
    
    public PruebaApi(IApiService apiService)
    {
        _apiService = apiService;
    }
    
    // GET
    // public IActionResult Index()
    // {
    //     var mensaje = _apiService.GetDataFromApiAsync();
    //     ViewData["Mensaje"] = mensaje;
    //     return View();
    // }
    
    public async Task<IActionResult> Index()
    {
        var pokemonData = await _apiService.GetDataFromApiAsync();

        if (pokemonData == null)
        {
            return View("Error");
        }

        return View(pokemonData);
    }
    [HttpGet("exchange-data")]
    public async Task<IActionResult> GetExchangeData()
    {
        using (var httpClient = new HttpClient())
        {
            // Agrega tu API key o autenticación aquí
            // httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer TU_API_KEY");

            var response = await httpClient.GetAsync("https://v6.exchangerate-api.com/v6/e8232f3beca6ca12993140cd/latest/EUR");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return Ok(data);
            }
            return StatusCode((int)response.StatusCode, "Error al obtener datos");
        }
    }
    
    
}