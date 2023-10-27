using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories.MainRepo;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.Models.Schemas;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<ProductModel>> GetProductsByTagNameAsync(string tagName)
        {
            var products = await _productRepo.GetProductsByTagNameAsync(tagName);

            // Convert products from ProductEntity to ProductModel and return
            return products.Select(p => new ProductModel
            {
                ArticleNumber = p.ArticleNumber,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl.Select(img => img.ImageUrl).ToList(),
                Reviews = p.Reviews.Select(r => new ReviewModel
                {
                    Id = r.Id,
                    User = new UserModel
                    {
                        Id = r.User.Id,
                        FullName = r.User.FullName,
                        Email = r.User.Email,
                        PhoneNumber = r.User.PhoneNumber,
                        Role = "", // Not provided in UserEntity. Need to handle roles separately if required.
                        ProfileImgUrl = r.User.ImageUrl
                    },
                    ProductId = r.ProductId,
                    DateCreated = DateTime.Now, // Assuming the DateCreated is set at the time of review creation
                    Rating = r.Rating,
                    ReviewDescription = r.Review
                }).ToList(),
                Category = p.Categories.Select(c => c.Categories.CategoryName).ToList(),
                Tags = p.Tags.Select(t => t.Tag.TagName).ToList(),
                Sizes = p.Sizes.Select(s => s.ProductSize.SizeName).ToList(),
                Colours = p.Colours.Select(c => c.Color.ColorName).ToList(),
                InStock = p.AmountInStock
            }).ToList();
        }
    }
}
