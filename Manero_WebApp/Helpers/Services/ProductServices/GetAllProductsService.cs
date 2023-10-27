using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Models.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Manero_WebApp.Helpers.Services.ProductServices
{
	public class GetAllProductsService
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
                .Include(p => p.Reviews)
                .ToListAsync();

            var productModel = productEntity.Select(entity => new ProductModel
            {
                ArticleNumber = entity.ArticleNumber,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                AmountInStock = entity.AmountInStock,
                Categories = entity.Categories.Select(c => c.CategoryName).ToList(),
                Sizes = entity.Sizes.Select(s => s.SizeName).ToList(),
                Colors = entity.Colors.Select(c => c.ColorName).ToList(),
                Tags = entity.Tags.Select(t => t.TagName).ToList(),
                ImageUrl = entity.ImageUrl.Select(i => i.ImageUrl).ToList(),
                Reviews = entity.Reviews.Select(r => new ReviewModel
                {
                    Id = r.Id,
                    User = r.User,
                    ProductId = r.ProductId,
                    DateCreated = r.DateCreated,
                    Rating = r.Rating,
                    ReviewDescription = r.Review
                }).ToList()
            }).ToList();

            return productModel;
        }
    }
}
