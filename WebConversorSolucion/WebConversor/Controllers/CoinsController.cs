namespace WebConversor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoinsController : ControllerBase
{

    private readonly ILogger<CoinsController> _logger; // Logger para registrar información y errores
    private readonly DbContexto _dbContexto; // Contexto de base de datos (aunque aquí no se utiliza)

    public CoinsController(ILogger<CoinsController> logger, DbContexto contexto)
    {
        _logger = logger;
        _dbContexto = contexto;
    }

    // Endpoint para obtener una lista de monedas
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
