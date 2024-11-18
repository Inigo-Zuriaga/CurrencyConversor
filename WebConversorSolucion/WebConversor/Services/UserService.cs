namespace WebConversor.Services;

public class UserService
{
    private readonly IConfiguration _configuration;

    private readonly DbContexto _context;

    public UserService(DbContexto context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;

    }

    // public async Task<IActionResult> SignInUser(string name,string lastname,string email,string password)
    public async Task<string> RegisterUser(User user)
    {

        var userExist = _context.Users.FirstOrDefault(x => x.Email == user.Email);

        if (userExist != null)
        {
            return "El usuario ya existe";
        }

        var newUser = new User
        {
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            FechaNacimiento = user.FechaNacimiento,
            Img = user.Img
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return "Usuario registrado con exito"; ;
    }
    //login
    public async Task<string> LoginUser(LoginRequest request)
    {
 
        var userExist = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (userExist == null)
        {
            return "EL correo o contraseña son incorrectos";
        }

        return "Usuario registrado con exito";

        // Genera el token JWT si el usuario existe y las credenciales son correctas
        //return GenerateJwtToken(userExist.Email, userExist.Password);
    }



    //Configurar segun los datos que queramos pasar
    public string GenerateJwtToken(string email)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            //Indicamos los datos que queremos pasar con el token
            new Claim("email", email)
            //new Claim(JwtRegisteredClaimNames.Exp, expirationTime.ToString()) // Tiempo de expiraci�n
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            //issuer: _configuration["Jwt:Issuer"],
            //audience: _configuration["Jwt:Audience"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}