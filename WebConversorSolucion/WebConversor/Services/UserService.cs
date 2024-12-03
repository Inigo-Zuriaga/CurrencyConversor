using Microsoft.AspNetCore.Identity;

namespace WebConversor.Services;

public class UserService
{
    private readonly IConfiguration _configuration; // Configuración para obtener claves y valores
    private readonly DbContexto _context; // Contexto de la bbdd

    private readonly IPasswordHasher<User>
        _passwordHasher; // Instancia de IPasswordHasher para gestionar el hashing de contraseñas

    public UserService(DbContexto context, IConfiguration configuration, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _configuration = configuration;
        _passwordHasher = passwordHasher; // Inicializa la instancia de IPasswordHasher
    }

    // Método para registrar un nuevo usuario
    // public async Task<IActionResult> SignInUser(string name,string lastname,string email,string password)
    public async Task<string> RegisterUser(User user)
    {
        if (user.Password.Length < 6)
        {
            return "La contraseña debe tener al menos 6 caracteres.";
        }

        // Verifica si el usuario ya existe basado en el mail
        var userExist = _context.Users.FirstOrDefault(x => x.Email == user.Email);

        if (userExist != null)
        {
            return "El usuario ya existe"; // Retorna mensaje si el usuario ya está registrado
        }

        // Hashea la contraseña antes de almacenarla
        user.Password = _passwordHasher.HashPassword(user, user.Password);

        // Crea un nuevo usuario con los datos proporcionados
        var newUser = new User
        {
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password, //ya hasheada encriptada
            FechaNacimiento = user.FechaNacimiento,
            Img = user.Img
        };

        _context.Users.Add(newUser); // Agrega el nuevo usuario al contexto
        await _context.SaveChangesAsync(); // Guarda los cambios en la bbdd

        return "Usuario registrado con exito";
    }

    public async Task<bool> ChangeProfile(UserRequest userRequest)
    {
        var userExist = _context.Users.FirstOrDefault(x => x.Email == userRequest.Email);

        if (userExist == null)
        {
            // return "No se puede generar el historial, el usuario no existe";
            return false;
        }

        userExist.Img = userRequest.Img;

        try
        {
            _context.Users.Update(userExist);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

// Método para iniciar sesión con las credenciales del usuario
    public async Task<string> LoginUser(LoginRequest request)
    {
        try
        {

            //Borrar los console
            Console.WriteLine("Datos recibidos en el endpoint Login:");
            Console.WriteLine($"Email: {request.Email}");
            Console.WriteLine($"Password: {request.Password}");

            var userExist = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (userExist == null)
            {
                return "El correo o la contraseña son incorrectos";
            }

            var passwordVerificationResult =
                _passwordHasher.VerifyHashedPassword(userExist, userExist.Password, request.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return "El correo o la contraseña son incorrectos";
            }

            return GenerateJwtToken(userExist.Email);
        }
        catch (Exception ex)
        {
            // Loguear el error o enviarlo al cliente
            return $"Error al procesar la solicitud: {ex.Message}";
        }
    }

    // public async Task<bool> GetUserData(string email)
    // {
    //     var userExist = _context.Users.FirstOrDefault(x => x.Email == email);
    //
    //     if (userExist == null)
    //     {
    //         // return "No se puede generar el historial, el usuario no existe";
    //         return false;
    //     }
    //
    //     try
    //     {
    //         var userData = await _context.ExchangeHistory
    //             .Include(x => x.User)
    //             .Where(x => x.User.Email == email)
    //             .OrderByDescending(x => x.Date)
    //             .ToListAsync();
    //         
    //         
    //     }catch(Exception ex)
    //     {
    //         return false;
    //     }
    //     
    //     
    // }

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