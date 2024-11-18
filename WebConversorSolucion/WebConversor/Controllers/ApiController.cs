namespace WebConversor.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ApiController : ControllerBase
{
    private readonly IApiService _apiService; // Servicio para obtener los datos de la API

    public ApiController(IApiService apiService)
    {
        _apiService = apiService;
    }

    // Endpoint para obtener la tasa de cambio entre dos monedas
    [HttpPost("exchange-rate")] // Indica que este método responde a solicitudes POST en "api/Api/exchange-rate"
    public async Task<IActionResult> GetExchangeRate([FromBody] ExchangeRequest request)
    {
        // Llama al servicio para obtener los datos de la tasa de cambio
        try
        {
            var data = await _apiService.GetDataFromApiAsync(request.FromCurrency, request.ToCurrency, request.Amount);
            return Ok(data);
        }
        catch (Exception e)
        {
            // Si ocurre un error, devuelve un código 500 (Internal Server Error) con el mensaje de excepción
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    // Endpoint para obtener los datos históricos para el gráfico de tasas de cambio entre dos monedas
    [HttpPost("historical-data")]
    public async Task<IActionResult> ChartDataRequest([FromBody] ExchangeRequest request)
    {
        try
        {
            // Llama al servicio para obtener los datos históricos
            var data = await _apiService.GetDataFromApiAsync(request.FromCurrency, request.ToCurrency, request.Amount);
            return Ok(data);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }


}

