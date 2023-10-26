using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories.MainRepo;
using Manero_WebApp.Models.Schemas;
using System.Linq.Expressions;

namespace Manero_WebApp.Services
{
    public class ProductServices
    {
        private readonly MainDbRepo<ProductModel> _productRepo;

        public ProductServices(DataContext context)
        {
            _productRepo = new MainDbRepo<ProductModel>(context);
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            return await _productRepo.GetAllAsync();
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync(Expression<Func<ProductModel, bool>> predicate)
        {
            return await _productRepo.GetAllAsync(predicate);
        }

        public async Task<ProductModel> GetProductAsync(Guid articleNumber)
        {
            return await _productRepo.GetAsync(p => p.ArticleNumber == articleNumber);
        }

        public async Task<ProductModel> AddProductAsync(ProductModel product)
        {
            return await _productRepo.AddAsync(product);
        }

        public async Task<ProductModel> UpdateProductAsync(ProductModel product)
        {
            return await _productRepo.UpdateAsync(product);
        }

        public async Task<bool> RemoveProductAsync(ProductModel product)
        {
            return await _productRepo.RemoveAsync(product);
        }

        public async Task<IEnumerable<ProductModel>> GetProductsByTagsAsync(List<string> tags)
        {
            return await _productRepo.GetAllAsync(p => p.Tags.Any(tag => tags.Contains(tag)));
        }
    }
}
