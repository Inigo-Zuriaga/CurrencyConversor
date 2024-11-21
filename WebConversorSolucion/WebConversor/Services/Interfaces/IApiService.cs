namespace WebConversor.Services.Interfaces;

public interface IApiService
{
    Task<Dictionary<string, object>> GetDataFromApiAsync(string fromCurrency, string toCurrency, decimal amount);

}
