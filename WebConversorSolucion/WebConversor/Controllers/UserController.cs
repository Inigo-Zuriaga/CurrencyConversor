namespace WebConversor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    // private readonly UserManager<IdentityUser> _userManager;
    // private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserService _userService;
    private readonly DbContexto _context;
    public UserController(
        // UserManager<IdentityUser> userManager,
        // SignInManager<IdentityUser> signInManager,
        DbContexto context,
        UserService userService)
    {
        // _userManager = userManager;
        // _signInManager = signInManager;
        _context = context;
        _userService = userService;
    }

    // Acción POST para manejar el login de usuarios
    // [HttpPost("login")]
    // public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest("Datos de login inválidos.");
    //     }
    //
    //     var user = await _userManager.FindByEmailAsync(model.Email);
    //     if (user != null)
    //     {
    //         var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
    //         if (result.Succeeded)
    //         {
    //             // Generar un JWT token aquí si es necesario
    //             return Ok(new { Message = "Login exitoso", Token = "BearerTokenAquí" });
    //         }
    //     }
    //
    //     return Unauthorized(new { Message = "Correo o contraseña incorrectos." });
    // }

    // Acción POST para manejar el registro de usuarios
    // [HttpPost("register")]
    // public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest("Datos de registro inválidos.");
    //     }
    //
    //     var user = new IdentityUser { UserName = model.Email, Email = model.Email };
    //     var result = await _userManager.CreateAsync(user, model.Password);
    //
    //     if (result.Succeeded)
    //     {
    //         await _signInManager.SignInAsync(user, isPersistent: false);
    //         return Ok(new { Message = "Registro exitoso" });
    //     }
    //
    //     return BadRequest(result.Errors);
    // }

    // Acción para cerrar sesión
    // [HttpPost("logout")]
    // public async Task<IActionResult> Logout()
    // {
    //     await _signInManager.SignOutAsync();
    //     return Ok(new { Message = "Cierre de sesión exitoso." });
    //     // public IActionResult Login([FromBody] User request)
    //     // {
    //     //     // Validar el correo y la contraseña
    //     //     if (_context.ContainsKey(request.Email) && _validUsers[request.Email] == request.Password)
    //     //     {
    //     //         // Generar un token o simplemente devolver un mensaje de éxito
    //     //         return Ok(new { Message = "Login exitoso", Token = "abc123" });
    //     //     }
    //     //     else
    //     //     {
    //     //         return Unauthorized(new { Message = "Credenciales inválidas" });
    //     //     }
    // }

    //[Authorize]
    [HttpGet("mostrarUsuarios")]
    public async Task<IActionResult> Get()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> Register([FromBody] User request)
    {

        if (request == null)
        {
            return BadRequest("Datos de registro inválidos.");
        }


        // var usuario=await _userService.RegisterUser(request.Name,request.LastName,request.Email,request.Password);
        var result = await _userService.RegisterUser(request);

        if (result != "Usuario registrado con exito")
        {
            // return StatusCode(StatusCodes.Status500InternalServerError, result);
            return BadRequest(result);
        }
        return Ok("Usuario registrado con exito");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {

        /*if(request == null)
        {
            return BadRequest("Datos de registro inválidos.");
        }*/


        // var usuario=await _userService.RegisterUser(request.Name,request.LastName,request.Email,request.Password);
        var result = await _userService.LoginUser(request);

        if (result != "Usuario registrado con exito")
        {
            // return StatusCode(StatusCodes.Status500InternalServerError, result);
            return BadRequest(result);
        }
        return Ok("Usuario registrado con exito");
    }


}