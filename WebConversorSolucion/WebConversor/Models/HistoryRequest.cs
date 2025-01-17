namespace WebConversor.Models;

public class HistoryRequest
{
     public string FromCoin { get; set; }
        
     public decimal FromAmount { get; set; }

     public string ToCoin { get; set; }
        
     public decimal ToAmount { get; set; }
     
     public DateTime Date { get; set; } = DateTime.Now;
     
     public string Email { get; set; }
     
     public string  Name { get; set; }
     
     public string LastName { get; set; }
}