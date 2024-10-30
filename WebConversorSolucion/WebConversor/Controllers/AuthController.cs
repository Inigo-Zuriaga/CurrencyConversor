namespace WebConversor.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Aquí validas las credenciales (esto es solo un ejemplo)
        // if (request.username == "usuario" && request.password == "contraseña")
        // {
        //     // Generar un token JWT o similar (esto es solo un ejemplo)
        //     return Ok(new { token = "tu_token_aqui" });
        // }
        
        if (request.username !="" && request.password != "")
        {
            // Generar un token JWT o similar (esto es solo un ejemplo)
            return Ok(new { token = "tu_token_aqui" });
        }

        return Unauthorized();
    }
}
public class LoginRequest
{
    public string username { get; set; }
    public string password { get; set; }
}