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

    // M�todo para crear un historial de intercambio basado en una solicitud de historial
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

        return "Historial creado con exito"; // Devuelve un mensaje de �xito
    }
    
    public async Task<bool> DeleteHistory(int id)
    {
        // Busca el historial por su id
        var history = _context.ExchangeHistory.FirstOrDefault(x => x.Id == id);

        if(history == null)
        {
            return false;
        }

        _context.ExchangeHistory.Remove(history); // Elimina el historial del contexto
        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

        // return "Historial eliminado con exito"; // Devuelve un mensaje de �xito
        return true;
    }

    public static string ToHtmlFile(List<HistoryRequest> data)
    {
        string templatePath=Path.Combine(Directory.GetCurrentDirectory(), "HtmlTemplates", "historyPdf.html");
        string tempHtml=File.ReadAllText(templatePath);
        StringBuilder stringData=new StringBuilder(String.Empty);
        for (int i = 0; i < data.Count; i++)
        {
        //     stringData.Append($"<tr><td>{data[i].Id}</td><td>{data[i].FromCoin}</td><td>{data[i].FromAmount}</td><td>{data[i].ToCoin}</td><td>{data[i].ToAmount}</td><td>{data[i].Date}</td></tr>");
        //<td class="py-2 px-4 border-b">{{ item.fromAmount }} {{ item.fromCoin }}</td>
        // <td class="py-2 px-4 border-b">{{ item.toAmount }} {{ item.toCoin }}</td>
        //     <td class="py-2 px-4 border-b">{{ item.date | date:'yyyy-MM-dd HH:mm' }}</td>
        
       stringData.Append($"<tr><td>{data[i].FromAmount} {data[i].FromCoin}</td><td>{data[i].ToAmount} {data[i].ToCoin}</td><td>{data[i].Date}</td></tr>");
        };
        return tempHtml.Replace("{data}", stringData.ToString());
        // return stringData.ToString();
    }
    
}