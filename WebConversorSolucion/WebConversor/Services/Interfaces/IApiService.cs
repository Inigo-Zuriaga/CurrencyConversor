public interface IApiService
{
    Task<Dictionary<string, object>> GetDataFromApiAsync(string fromCurrency, string toCurrency, int amount);

}
