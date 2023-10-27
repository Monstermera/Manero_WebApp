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

    public async Task<ProductModel> GetAsync(Guid articleNumber)
    {
        var productEntity = await _context.Products
            .Include(p => p.Categories)
            .Include(p => p.Sizes)
            .Include(p => p.Colors)
            .Include(p => p.Tags)
            .Include(p => p.ImageUrl)
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.ArticleNumber == articleNumber);

        if (productEntity == null)
        {
            return null!;
        }

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

}
