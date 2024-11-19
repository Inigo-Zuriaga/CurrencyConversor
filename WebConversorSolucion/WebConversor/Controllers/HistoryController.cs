using Microsoft.AspNetCore.Authorization;

namespace WebConversor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {

        private readonly DbContexto _context; // Contexto de la bbdd
        private readonly HistoryService _historyService; // Servicio para manejar la lógica de historial

        public HistoryController(DbContexto context, HistoryService historyService)
        {
            _context = context;
            _historyService = historyService;
        }

        // Endpoint para obtener el historial de intercambios de un usuario autenticado
        [HttpGet]
        public async Task<ActionResult<List<History>>> Get()
        {
            // var email = User.Identity?.Name;

            //Correo Hardcodeado para pruebas
            var email = "ggrg2@gmail.com";

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("El usuario no está autenticado."); // Retorna 401 Unauthorized si no hay correo
            }

            // Obtiene el historial de intercambios del usuario ordenado por fecha descendente
            List<History> exchangeList = await _context.ExchangeHistory
                .Include(x => x.User)              // Incluye los datos del usuario asociado
                .Where(x => x.User.Email == email) // Filtra por correo electrónico
                .OrderByDescending(x => x.Date)    // Ordena por fecha descendente
                .ToListAsync();

            return Ok(exchangeList);
        }

        // Endpoint para obtener el historial de intercambios basándose en un correo electrónico proporcionado
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

            // Obtiene el historial de intercambios para el correo proporcionado
            List<History> exchangeList = await _context.ExchangeHistory
                .Include(x => x.User)
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

            var createdHistory = await _historyService.CreateHistory(history);

            // if (string.IsNullOrEmpty(email))
            // {
            //     return Unauthorized("El usuario no está autenticado.");
            // }

            return Ok(createdHistory);
        }

    }
}
