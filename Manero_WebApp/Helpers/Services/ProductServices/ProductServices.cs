using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories.MainRepo;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.Models.Schemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Manero_WebApp.Services
{
    public class ProductServices
    {
        private readonly MainDbRepo<ProductEntity> _productRepo;


        public ProductServices(DataContext context)
        {
            _productRepo = new MainDbRepo<ProductEntity>(context);
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
        {
            return await _productRepo.GetAllAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetProductsAsync(Expression<Func<ProductEntity, bool>> predicate)
        {
            return await _productRepo.GetAllAsync(predicate);
        }

        //public async Task<ProductEntity> GetProductWithReviewsAsync(Guid articleNumber)
        //{
        //    return await _productRepo.GetAsync(
        //        p => p.ArticleNumber == articleNumber,
        //        pr => pr.Reviews,
        //        pr => pr.ImageUrl,
        //        pr => pr.Categories,
        //        pr => pr.Tags,
        //        pr => pr.Sizes,
        //        pr => pr.Colors
        //    );
        //}




        public async Task<ProductEntity> AddProductAsync(ProductEntity product)
        {
            return await _productRepo.AddAsync(product);
        }

        public async Task<ProductEntity> UpdateProductAsync(ProductEntity product)
        {
            return await _productRepo.UpdateAsync(product);
        }

        public async Task<bool> RemoveProductAsync(ProductEntity product)
        {
            return await _productRepo.RemoveAsync(product);
        }
    }
}
