namespace WebConversor.Services;

public class HistoryService
{
    
    private readonly IConfiguration _configuration;

    private readonly DbContexto _context;
    
    public HistoryService(DbContexto context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;

    }
    
    // public async Task<string> CreateHistory(History history)
    public async Task<string> CreateHistory(HistoryRequest history)
    {
        
        var userExist = _context.Users.FirstOrDefault(x => x.Email == history.Email);

        if(userExist == null)
        {
            return "No se puede generar el historial, el usuario no existe";
        }
        
        var newHistory = new History
        {
            User = userExist,
            Date = history.Date,
            FromCoin = history.FromCoin,
            ToCoin = history.ToCoin,
            ToAmount = history.ToAmount,
            FromAmount = history.FromAmount,
        };
        
        _context.ExchangeHistory.Add(newHistory);
        await _context.SaveChangesAsync();
        
        return "Historial creado con exito";
    }
    
}