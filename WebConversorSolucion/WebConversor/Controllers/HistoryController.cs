using Microsoft.AspNetCore.Authorization;

namespace WebConversor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {

        private readonly DbContexto _context;
        private readonly HistoryService _historyService;
        public HistoryController(DbContexto context, HistoryService historyService)
        {
            _context = context;
            _historyService = historyService;
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

            List<History> exchangeList = await _context.ExchangeHistory
                .Include(x => x.User)
                .Where(x => x.User.Email == email)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return Ok(exchangeList);
        }
        [HttpPost("History")]
        public async Task<ActionResult<List<History>>> GetHistory([FromBody] string email)
        {
            // var email = User.Identity?.Name;
            //Correo Hardcodeado para pruebas
            // var email = "ggrg2@gmail.com";
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("El usuario no está autenticado.");
            }

            List<History> exchangeList =await _context.ExchangeHistory
                .Include(x =>x.User)
                .Where(x => x.User.Email == email)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
          
            return Ok(exchangeList);
        }

        [Authorize]
        [HttpPost("CreateHistory")]
        // public async Task<ActionResult> CreateHistory([FromBody] History history)
        public async Task<ActionResult> CreateHistory([FromBody] HistoryRequest history)
        {
            // var email = User.Identity?.Name;
            //Correo Hardcodeado para pruebas
            
            var createdHistory=await _historyService.CreateHistory(history);

            // if (string.IsNullOrEmpty(email))
            // {
            //     return Unauthorized("El usuario no está autenticado.");
            // }

            return Ok(createdHistory);
        }

    }
}
