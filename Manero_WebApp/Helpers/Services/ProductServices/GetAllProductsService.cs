using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Models.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Manero_WebApp.Helpers.Services.ProductServices
{
    public class GetAllProductsService : IGetAllProductsService
    {
        #region constructors & private fields

        private readonly DataContext _context;

        public GetAllProductsService(DataContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<List<ProductModel>> GetAllAsync()
        {
            var productEntity = await _context.Products
                .Include(p => p.Categories)
                .Include(p => p.Sizes)
                .Include(p => p.Colors)
                .Include(p => p.Tags)
                .Include(p => p.ImageUrl)
                .Include(p => p.Reviews).ThenInclude(x => x.User)
                .ToListAsync();

            if (productEntity != null)
            {
                List<ProductModel> products = new();

                foreach (var product in productEntity)
                {
                    ProductModel productModel1 = product;
                    products.Add(productModel1);
                }
                return products;
            };
            return null!;
        }
    }
}
