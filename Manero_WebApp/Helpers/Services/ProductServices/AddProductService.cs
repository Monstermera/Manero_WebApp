using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.Models.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Manero_WebApp.Helpers.Services.ProductServices;

public class AddProductService
{
    #region constructors & private fields

    private readonly DataContext _context;
    private readonly ProductDbRepo _productDbRepo;

    public AddProductService(DataContext context, ProductDbRepo productDbRepo)
    {
        _context = context;
        _productDbRepo = productDbRepo;
    }

    #endregion

	public async Task<ProductModel> AddAsync(ProductModel model)
	{
        ProductEntity entity = new()
        {
            Name = model.Name,
            Price = model.Price,
            Description = model.Description,
        };

        var _entity = await _productDbRepo.GetAsync(x => x.ArticleNumber == model.ArticleNumber);
        if (_entity == null)
        {
            if (model.ImageUrl != null)
            {
                foreach (var imageUrl in model.ImageUrl)
                {
                    var result = await _context.ProductImages.FirstOrDefaultAsync(x => x.ImageUrl == imageUrl);
                    if (result != null)
                    {
                        entity.ImageUrl.Add(result);
                    }
                }
            }

            foreach (var category in model.Categories)
            {
                var result = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == category);
                if (result != null)
                {
                    entity.Categories.Add(result);
                }
            }

            foreach (var tag in model.Tags)
            {
                var result = await _context.Tags.FirstOrDefaultAsync(x => x.TagName == tag);
                if (result != null)
                {
                    entity.Tags.Add(result);
                }
            }

            foreach (var size in model.Sizes)
            {
                var result = await _context.Sizes.FirstOrDefaultAsync(x => x.SizeName == size);
                if (result != null)
                {
                    entity.Sizes.Add(result);
                }
            }

            foreach (var color in model.Colors)
            {
                var result = await _context.Colors.FirstOrDefaultAsync(x => x.ColorName == color);
                if (result != null)
                {
                    entity.Colors.Add(result);
                }
            }

            _entity = await _productDbRepo.AddAsync(entity);
            if (_entity != null)
                return _entity;
        }
        return null!;
    }
}
