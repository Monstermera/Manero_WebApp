using Manero_WebApp.Models.Schemas;

namespace Manero_WebApp.Helpers.Services.ProductServices
{
    public interface IGetOneProductService
    {
        Task<ProductModel> GetOneProductAsync(int articleNumber);
    }
}