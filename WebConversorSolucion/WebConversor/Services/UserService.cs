using Microsoft.AspNetCore.Identity;
using WebConversor.Models;

namespace WebConversor.Services;

public class UserService
{
    private readonly IConfiguration _configuration; // Configuración para obtener claves y valores
    private readonly DbContexto _context; // Contexto de la bbdd

    public UserService(DbContexto context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;

    }

    // Método para registrar un nuevo usuario
    // public async Task<IActionResult> SignInUser(string name,string lastname,string email,string password)
    public async Task<string> RegisterUser(User user)
    {
        // Verifica si el usuario ya existe basado en el mail
        var userExist = _context.Users.FirstOrDefault(x => x.Email == user.Email);

        if (userExist != null)
        {
            return "El usuario ya existe"; // Retorna mensaje si el usuario ya está registrado
        }

        // Crea un nuevo usuario con los datos proporcionados
        var newUser = new User
        {
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            FechaNacimiento = user.FechaNacimiento,
            Img = user.Img
        };
        
        _context.Users.Add(newUser); // Agrega el nuevo usuario al contexto
        await _context.SaveChangesAsync(); // Guarda los cambios en la bbdd

        return "Usuario registrado con exito";
    }

    // Método para iniciar sesión con las credenciales del usuario
    public async Task<string> LoginUser(LoginRequest request)
    {
        // Verifica si el usuario existe basado en su mail
        var userExist = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (userExist == null)
        {
            return "EL correo o contraseña son incorrectos"; // Devuelve mensaje de error si no existe
        }

        // Aquí deberías comparar la contraseña (esto se puede hacer más adelante con hashing/encriptación)
        if (userExist.Password == request.Password) // Compara las contraseñas (por ahora sin encriptación)
        {
            return userExist.Email; // Las credenciales son correctas
        }
        return "Usuario registrado con exito";

        // Genera el token JWT si el usuario existe y las credenciales son correctas
        //return GenerateJwtToken(userExist.Email, userExist.Password);
    }



    //Configurar segun los datos que queramos pasar, genera un token JWT
    public string GenerateJwtToken(string email)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings"); // Obtiene configuración JWT
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])); // Llave secreta
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256); // Credenciales de firma

        // Reclamaciones del token (datos que se incluirán)
        var claims = new[]
        {
            //Indicamos los datos que queremos pasar con el token
            new Claim("email", email)
            //new Claim(JwtRegisteredClaimNames.Exp, expirationTime.ToString()) // Tiempo de expiraci�n
        };

        // Genera el token JWT
        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"], // Emisor del token
            audience: jwtSettings["Audience"], // Audiencia del token
            claims: claims, // Reclamaciones del token
            expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])), // Tiempo de expiración
            signingCredentials: credentials // Credenciales de firma
        );

        return new JwtSecurityTokenHandler().WriteToken(token); // Retorna el token como una cadena
    }

}