namespace WebConversor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService; // Servicio que gestiona la lógica relacionada con usuarios
    private readonly DbContexto _context; // Contexto para interactuar con la base de datos
    private readonly ILogger<UserController> _logger;
    
    // Constructor del controlador, inyecta el contexto y el servicio de usuario
    public UserController(DbContexto context, UserService userService,ILogger<UserController> logger)
    {
        _context = context; // Asigna el contexto a una propiedad privada
        _userService = userService; // Asigna el servicio de usuario a una propiedad privada
        _logger = logger;
    }

    // Endpoint para obtener todos los usuarios registrados
    [HttpGet("mostrarUsuarios")] // Este endpoint responde a solicitudes GET en "api/User/mostrarUsuarios"
    public async Task<IActionResult> Get()
    {
        var users = await _context.Users.ToListAsync(); // Obtiene todos los usuarios de la base de datos
        return Ok(users); // Devuelve la lista de usuarios con un código HTTP 200 (OK)
    }
    
    [HttpPost("ChangeProfile")] // Este endpoint responde a solicitudes POST en "api/User/ChangeProfile"
    public async Task<IActionResult> ChangeProfile([FromBody] UserRequest request)
    {
        if (request == null)
        {
            return BadRequest(new { error = "Datos de perfil inválidos." });
        }

        var result = await _userService.ChangeProfile(request);
        
        if (result == false)
        {
            return BadRequest(new { error = "No se ha podido actualizar el perfil" });
        }
        return Ok(new { message = "Datos modificados correctamente" });
    }
    
        
    // [HttpGet("GetUserData")] // This endpoint responds to GET requests at "api/User/GetUserData"
    // [Authorize]
    // public async Task<IActionResult> GetUserData()
    // {
    //     //Comprobamos si los datos de la autorizacion son correctos
    //     if (User == null || !User.Identity.IsAuthenticated)
    //     {
    //         return Unauthorized("The user is not authenticated.");
    //     }
    //
    //     //Verificamos si el usuario tiene un claim de email en el Token
    //     var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
    //     
    //     try
    //     {
    //         var userData = await _context.Users
    //             .Where(x => x.Email == emailClaim)
    //             .FirstOrDefaultAsync();
    //
    //         return Ok(userData);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(new { error = "No se han podido enviar los datos del usuario" });
    //     }
    //    
    // }
    [HttpGet("GetUserData")] // This endpoint responds to GET requests at "api/User/GetUserData"
    [Authorize]
    public async Task<IActionResult> GetUserData()
    {
        //Comprobamos si los datos de la autorizacion son correctos
        if (User == null || !User.Identity.IsAuthenticated)
        {
            return Unauthorized("The user is not authenticated.");
        }
    
        //Verificamos si el usuario tiene un claim de email en el Token
        var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
        
        try
        {
            var userData = await _context.Users
                .Where(x => x.Email == emailClaim)
                .FirstOrDefaultAsync();

            return Ok(userData);
        }
        catch (Exception e)
        {
            return BadRequest(new { error = "No se han podido enviar los datos del usuario" });
        }
    }
    
    // Endpoint para registrar un nuevo usuario
    [HttpPost("SignIn")] // Este endpoint responde a solicitudes POST en "api/User/SignIn"
    public async Task<IActionResult> Register([FromBody] User request)
    {
        if (request == null)
        {
            return BadRequest("Datos de registro inválidos.");
        }

        // Llama al servicio para registrar un nuevo usuario
        var result = await _userService.RegisterUser(request);

        if (result != "Usuario registrado con exito")
        {
            return BadRequest(new { error = "No se ha podido registrar el usuario" });
        }

        return Ok(new { message = "Usuario registrado correctamente" });
    }

    // Endpoint para iniciar sesión
    [HttpPost("Login")] // Este endpoint responde a solicitudes POST en "api/User/Login"
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (request == null)
        {
            return BadRequest(new { error = "Por favor, complete todos los campos." });
        }

        // Llama al servicio para validar las credenciales del usuario
        var result = await _userService.LoginUser(request);

        if (result == false)
        {
            return Unauthorized(new { error = "El correo o la contraseña son incorrectos" });
        }

        // var userData = await _context.Users
        //     .Where(x => x.Email == request.Email)
        //     .Select(x => new {x.Img }) // Obtén solo los campos necesarios
        //     .FirstOrDefaultAsync();
        var imagen  = "userData.Img";
        // Console.WriteLine("Datos del usuario logueado:");
        // Console.WriteLine(userData.Img);
        // Genera un token JWT para el usuario autenticado
        // var token = _userService.GenerateJwtToken(request.Email,userData.Img);
        var token = _userService.GenerateJwtToken(request.Email);

        return Ok(new { Token = token });
    }
}
