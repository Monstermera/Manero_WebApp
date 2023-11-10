namespace Manero_WebApp.Helpers.Services.ProductServices
{
    public interface IDeleteOneProductService
    {
        Task<bool> DeleteAsync(Guid articleNumber);
    }
}