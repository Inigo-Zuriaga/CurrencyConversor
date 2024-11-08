using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebConversor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbContexto _context;
        
        public UserController(DbContexto context)
        {
            _context = context;
        }
        
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
        
        //CREAR UN DICCIONARIO Q PILLE 2 STRINGS
        [HttpPost("SignIn")]

        public async Task<IActionResult> SignIn([FromBody] User request)
        {
            // request.Email; 
            var user = await _context.Users
                .Where(m => m.Email == request.Email)
                .ToListAsync();
            
            if (user == null)
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                return Ok();
                // return NotFound();
            }else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El usuario ya existe");
            }

            
            return NotFound();
        }
        // public IActionResult Login([FromBody] User request)
        // {
        //     // Validar el correo y la contraseña
        //     if (_context.ContainsKey(request.Email) && _validUsers[request.Email] == request.Password)
        //     {
        //         // Generar un token o simplemente devolver un mensaje de éxito
        //         return Ok(new { Message = "Login exitoso", Token = "abc123" });
        //     }
        //     else
        //     {
        //         return Unauthorized(new { Message = "Credenciales inválidas" });
        //     }
        
        
    }
}
