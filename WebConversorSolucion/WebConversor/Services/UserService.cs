using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Configuration;
namespace WebConversor.Services
{

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
        
            if(userExist != null)
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



        //Configurar segun los datos que queramos pasar
        private string GenerateJwtToken(string username, string password)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                //Indicamos los datos que queremos pasar con el token
                new Claim("username", username),
                new Claim("password", password)
                //new Claim(JwtRegisteredClaimNames.Exp, expirationTime.ToString()) // Tiempo de expiración
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}