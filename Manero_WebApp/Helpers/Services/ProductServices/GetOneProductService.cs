using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Models.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Manero_WebApp.Helpers.Services.ProductServices;

public class GetOneProductService
{
    #region constructors & private fields

    private readonly DataContext _context;

    public GetOneProductService(DataContext context)
    {
        _context = context;
    }

    #endregion

    public async Task<ProductModel> GetOneProductAsync(Guid articleNumber)
    {
		var productEntity = await _context.Products
			.Include(x => x.ImageUrl)
			.Include(x => x.Reviews).ThenInclude(x => x.User)
			.Include(x => x.Categories)
			.Include(x => x.Tags)
			.Include(x => x.Sizes)
			.Include(x => x.Colors)
			.FirstOrDefaultAsync(x => x.ArticleNumber == articleNumber);

		if (productEntity != null)
        {
            var productModel = new ProductModel
            {
                ArticleNumber = productEntity.ArticleNumber,
                Name = productEntity.Name,
                Price = productEntity.Price,
                Description = productEntity.Description,
                AmountInStock = productEntity.AmountInStock,
                Categories = productEntity.Categories.Select(c => c.CategoryName).ToList(),
                Sizes = productEntity.Sizes.Select(s => s.SizeName).ToList(),
                Colors = productEntity.Colors.Select(c => c.ColorName).ToList(),
                Tags = productEntity.Tags.Select(t => t.TagName).ToList(),
                ImageUrl = productEntity.ImageUrl.Select(i => i.ImageUrl).ToList(),
                Reviews = productEntity.Reviews.Select(r => new ReviewModel
                {
                    Id = r.Id,
                    User = r.User,
                    ProductId = r.ProductId,
                    DateCreated = r.DateCreated,
                    Rating = r.Rating,
                    ReviewDescription = r.Review
                }).ToList()
            };

            return productModel;
        }

        return null; 
    }

}
