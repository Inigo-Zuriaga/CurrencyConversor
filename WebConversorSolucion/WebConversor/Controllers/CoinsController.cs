using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebConversor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoinsController : ControllerBase
{

    private readonly ILogger<CoinsController> _logger;
    private readonly DbContexto _dbContexto;

    public CoinsController(ILogger<CoinsController> logger, DbContexto contexto)
    {
        _logger = logger;
        _dbContexto = contexto;
    }


    [HttpGet]
    public IActionResult Get()
    {
        //Crear una lista de monedas
        var monedas = new List<Coin>
        {
            new Coin { Id = 1, Name = "Dolar" },
            new Coin { Id = 2, Name = "Euro" },
            new Coin { Id = 3, Name = "Peso" }
        };
        return Ok(monedas);
    }

}
