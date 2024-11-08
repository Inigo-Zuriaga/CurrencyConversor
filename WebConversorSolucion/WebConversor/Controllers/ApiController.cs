using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebConversor.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ApiController : ControllerBase
{
    private readonly IApiService _apiService;

    public ApiController(IApiService apiService)
    {
        _apiService = apiService;
    }
    
    [HttpPost("exchange-rate")]
    public async Task<IActionResult> GetExchangeRate([FromBody] ExchangeRequest request)
    {
        try
        {
            var data = await _apiService.GetDataFromApiAsync(request.FromCurrency, request.ToCurrency, request.Amount);
            return Ok(data);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
}

