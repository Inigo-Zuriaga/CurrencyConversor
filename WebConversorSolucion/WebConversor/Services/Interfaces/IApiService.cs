namespace WebConversor.Services.Interfaces;

public interface IApiService
{
    Task<Dictionary<string, object>> GetExchangeRateAsync(string fromCurrency, string toCurrency, decimal amount);

    Task<Dictionary<string, object>> GetHistoricalDataAsync(string fromCurrency, string toCurrency);

}
