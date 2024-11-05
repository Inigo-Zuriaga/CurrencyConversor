namespace WebConversor.Services.Interfaces
{
    public interface IApiService
    {

        // Task<Dictionary<string, object>>GetDataFromApiAsync();
        Task<Dictionary<string, object>> GetDataFromApiAsync(string FromCurrency, string ToCurrency);

    }
}
