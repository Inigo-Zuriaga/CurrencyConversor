namespace WebConversor.Models;

public class HistoryRequest
{
     public string FromCoin { get; set; }
        
     public double FromAmount { get; set; }

     public string ToCoin { get; set; }
        
     public double ToAmount { get; set; }
     
     public DateTime Date { get; set; } = DateTime.Now;
     
     public string Email { get; set; }
}