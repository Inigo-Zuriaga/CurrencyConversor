namespace WebConversor.Controllers;

[Route("api/[controller]")] // Define la ruta base para este controlador (e.g., api/User)
[ApiController] // Indica que este controlador manejará solicitudes HTTP y valida automáticamente los modelos
public class UserController : ControllerBase
{
    private readonly UserService _userService; // Servicio que gestiona la lógica relacionada con usuarios
    private readonly DbContexto _context; // Contexto para interactuar con la base de datos

    // Constructor del controlador, inyecta el contexto y el servicio de usuario
    public UserController(DbContexto context, UserService userService)
    {
        _context = context; // Asigna el contexto a una propiedad privada
        _userService = userService; // Asigna el servicio de usuario a una propiedad privada
    }

    // Endpoint para obtener todos los usuarios registrados
    [HttpGet("mostrarUsuarios")] // Este endpoint responde a solicitudes GET en "api/User/mostrarUsuarios"
    public async Task<IActionResult> Get()
    {
        var users = await _context.Users.ToListAsync(); // Obtiene todos los usuarios de la base de datos
        return Ok(users); // Devuelve la lista de usuarios con un código HTTP 200 (OK)
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
           // return BadRequest(new { error = result }); // Enviar el error al frontend

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

            //return BadRequest("Datos de inicio de sesión inválidos.");
        }

        // Llama al servicio para validar las credenciales del usuario
        var result = await _userService.LoginUser(request);

        if (result == "El correo o la contraseña son incorrectos")
        {
            //return Unauthorized(new { error = result }); // Mensaje de error para el frontend

            return Unauthorized(new { error = "El correo o la contraseña son incorrectos" });
        }

        // Genera un token JWT para el usuario autenticado
        var token = _userService.GenerateJwtToken(request.Email);

        return Ok(new { Token = token });
    }
}
