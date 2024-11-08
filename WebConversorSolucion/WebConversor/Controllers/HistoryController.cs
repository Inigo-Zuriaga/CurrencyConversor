using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebConversor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        
        private readonly DbContexto _context;
        
        public HistoryController(DbContexto context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<History>>> Get()
        {
            // var email = User.Identity?.Name;
            //Correo Hardcodeado para pruebas
            var email = "ggrg2@gmail.com";
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("El usuario no está autenticado.");
            }

            List<History> exchangeList =await _context.ExchangeHistory.Include(x =>x.User)
                .Where(x => x.User.Email == email)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
            
            return Ok(exchangeList);
        }
        
        
    }
}
