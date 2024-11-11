using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace WebConversor.Services
{

    public class UserService 
    {
        private readonly DbContexto _context;
        
        public UserService(DbContexto context)
        {
            _context = context;
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
    }
}