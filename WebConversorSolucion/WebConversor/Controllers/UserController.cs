using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebConversor.ViewModels;

namespace WebConversor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Acción POST para manejar el login de usuarios
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Datos de login inválidos.");
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                // Generar un JWT token aquí si es necesario
                return Ok(new { Message = "Login exitoso", Token = "BearerTokenAquí" });
            }
        }

        return Unauthorized(new { Message = "Correo o contraseña incorrectos." });
    }

    // Acción POST para manejar el registro de usuarios
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Datos de registro inválidos.");
        }

        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok(new { Message = "Registro exitoso" });
        }

        return BadRequest(result.Errors);
    }

    // Acción para cerrar sesión
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Message = "Cierre de sesión exitoso." });
    }
}