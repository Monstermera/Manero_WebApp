using Manero_WebApp.Models.Schemas;

namespace Manero_WebApp.Helpers.Services.ProductServices
{
    public interface IGetAllProductsService
    {
        Task<List<ProductModel>> GetAllAsync();
    }
}