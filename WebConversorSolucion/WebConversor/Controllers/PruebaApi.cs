using Microsoft.AspNetCore.Mvc;

namespace WebConversor.Controllers;

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
}