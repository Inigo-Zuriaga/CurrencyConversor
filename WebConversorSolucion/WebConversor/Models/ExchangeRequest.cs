namespace WebConversor.Models;

public class ExchangeRequest
{
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }
    public int Amount { get; set; }
}