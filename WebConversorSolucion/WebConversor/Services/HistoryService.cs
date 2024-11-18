namespace WebConversor.Services;

public class HistoryService
{
    private readonly IConfiguration _configuration; // Permite acceder a las configuraciones
    private readonly DbContexto _context; // Contexto de la bbdd
    
    public HistoryService(DbContexto context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;

    }

    // Método para crear un historial de intercambio basado en una solicitud de historial
    public async Task<string> CreateHistory(HistoryRequest history)
    {
        // Verifica si el usuario existe en la base de datos
        var userExist = _context.Users.FirstOrDefault(x => x.Email == history.Email);

        if(userExist == null)
        {
            return "No se puede generar el historial, el usuario no existe";
        }

        // Crea un nuevo objeto de historial con los datos proporcionados
        var newHistory = new History
        {
            User = userExist,
            Date = history.Date,
            FromCoin = history.FromCoin,
            ToCoin = history.ToCoin,
            ToAmount = history.ToAmount,
            FromAmount = history.FromAmount,
        };
        
        _context.ExchangeHistory.Add(newHistory); // Agrega el nuevo historial al contexto
        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

        return "Historial creado con exito"; // Devuelve un mensaje de éxito
    }
    
}