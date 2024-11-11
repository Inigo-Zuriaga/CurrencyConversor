using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebConversor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private object _validUsers;

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


//         CREAR UN DICCIONARIO Q PILLE 2 STRINGS

        [HttpPost("Registro")]
        public IActionResult Login([FromBody] User request)
        {
            // Validar el correo y la contraseña
            if (_validUsers.ContainsKey(request.Email) && _validUsers[Request.Email] == request.Password)
            {
                // Generar un token o simplemente devolver un mensaje de éxito
                return Ok(new { Message = "Login exitoso", Token = "abc123" });
            }

            else
            {
                return Unauthorized(new { Message = "Credenciales inválidas" });
            }
        }

        // llevar el usuario a la base de datos una vez se ha validado

        public class UsuarioController : Controller
        {
            private readonly DbContexto _context;

            public UsuarioController(DbContexto context)
            {
                _context = context;
            }

            [HttpPost]
            public async Task<IActionResult> Registrar(User user)
            {
                if (ModelState.IsValid)
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(user);
            }

            private IActionResult View(User viewName)
            {
                throw new NotImplementedException();
            }
        }

    }
    
}

