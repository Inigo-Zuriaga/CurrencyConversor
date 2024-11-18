namespace WebConversor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{ 
    private readonly UserService _userService; // Servicio para la lógica de usuarios
    private readonly DbContexto _context; // Contexto de la base de datos
    public UserController(DbContexto context,UserService userService)
    {
        _context = context;
        _userService = userService;
    }

    // Endpoint para obtener todos los usuarios registrados
    [HttpGet("mostrarUsuarios")] // Indica que este método responde a solicitudes GET en la ruta "api/User/mostrarUsuarios"
    public async Task<IActionResult> Get()
    {
        var users = await _context.Users.ToListAsync();// Obtiene la lista de usuarios
        return Ok(users); // Devuelve la lista en la respuesta con código 200 OK
    }

    // Endpoint para registrar un nuevo usuario
    [HttpPost("SignIn")]
    public async Task<IActionResult> Register([FromBody] User request)
    {

        if (request == null)
        {
            return BadRequest("Datos de registro inválidos."); // Devuelve código 400 BadRequest si los datos son nulos
        }

        // Llama al servicio para registrar al usuario
        // var usuario=await _userService.RegisterUser(request.Name,request.LastName,request.Email,request.Password);
        var result = await _userService.RegisterUser(request);

        if (result != "Usuario registrado con exito")
        {
            // return StatusCode(StatusCodes.Status500InternalServerError, result);
            // return BadRequest(result);
            return BadRequest(new { error = "No se ha podido registrar el usuario" });

        }
        // return Ok("Usuario registrado con exito");
        return Ok(new { message = "Usuario registrado correctamente" });
    }

    // Endpoint para iniciar sesión
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if(request == null)
        {
            return BadRequest("Datos de registro inválidos."); // Devuelve si los datos son nulos
        }

        // Llama al servicio para validar las credenciales
        // var usuario=await _userService.RegisterUser(request.Name,request.LastName,request.Email,request.Password);
        var result = await _userService.LoginUser(request);

        if (result != "Usuario registrado con exito")
        {
            // return StatusCode(StatusCodes.Status500InternalServerError, result);
            // return BadRequest(result);
            return BadRequest("El correo o la contraseña son incorrectos");
        }

        // Genera un token JWT para el usuario autenticado
        var token = _userService.GenerateJwtToken(request.Email);
        return Ok(new { Token = token }); // Devuelve el token
    }
}