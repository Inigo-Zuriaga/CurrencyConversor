namespace WebConversor.Controllers;

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
        
        // Endpoint para obtener el historial de intercambios basándose en un correo electrónico proporcionado
        [HttpPost("History")]
        public async Task<ActionResult<List<History>>> GetHistory([FromBody] string email)
        {

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
    public async Task<ActionResult> CreateHistory([FromBody] HistoryRequest history)
    {
        var createdHistory = await _historyService.CreateHistory(history);
        
        return Ok(createdHistory);
    }
        
    // [Authorize]
    [HttpPost("DeleteHistory")]
    public async Task<ActionResult> DeleteHistory([FromBody] int id)
    {
        var deletedHistory = await _historyService.DeleteHistory(id);

        if (!deletedHistory)
        {
            // return Unauthorized("El usuario no está autenticado.");
            return BadRequest(new { error = "No se ha podido borrar la conversion" });
        }

        return Ok(new { message = "Conversion eliminada correctamente" });
        }
        
    }
    

