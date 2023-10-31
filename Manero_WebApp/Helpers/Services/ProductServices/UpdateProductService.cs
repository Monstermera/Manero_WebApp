using Manero_WebApp.Contexts;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.Models.Schemas;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Manero_WebApp.Helpers.Services.ProductServices;

public class UpdateProductService
{

    private readonly DataContext _context;

    public UpdateProductService(DataContext context)
    {
        _context = context;
    }

    public async Task<ProductEntity> UpdateAsync(ProductModel updatedProduct)
    {
        var existingProduct = await _context.Products
            .Include(x => x.ImageUrl)
            .Include(x => x.Reviews).ThenInclude(x => x.User)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .Include(x => x.Sizes)
            .Include(x => x.Colors)
            .FirstOrDefaultAsync(p => p.ArticleNumber == updatedProduct.ArticleNumber);

        if (existingProduct != null)
        {
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.AmountInStock = updatedProduct.AmountInStock;

            //Updates Categories
            var categories = await _context.Categories.ToListAsync();
            foreach (var category in categories)
            {
                if (updatedProduct.Categories.Contains(category.CategoryName))
                {
                    if (!existingProduct.Categories.Contains(category))
                    {
                        existingProduct.Categories.Add(category);
                    }
                }
                else
                {
                    if (existingProduct.Categories.Contains(category))
                    {
                        existingProduct.Categories.Remove(category);
                    }
                }
            }

            //Updates Tags
            var tags = await _context.Tags.ToListAsync();
            foreach (var tag in tags)
            {
                if(updatedProduct.Tags.Contains(tag.TagName))
                {
                    if (!existingProduct.Tags.Contains(tag))
                    {
                        existingProduct.Tags.Add(tag);
                    }                    
                }
                else
                {
                    if(existingProduct.Tags.Contains(tag))
                    {
                        existingProduct.Tags.Remove(tag);
                    }
                }
            }

            //Updates Sizes
            var sizes = await _context.Sizes.ToListAsync();
            foreach (var size in sizes)
            {
                if (updatedProduct.Sizes.Contains(size.SizeName))
                {
                    if (!existingProduct.Sizes.Contains(size))
                    {
                        existingProduct.Sizes.Add(size);
                    }
                }
                else
                {
                    if (existingProduct.Sizes.Contains(size))
                    {
                        existingProduct.Sizes.Remove(size);
                    }
                }
            }

            //Updates Colors
            var colors = await _context.Colors.ToListAsync();
            foreach (var color in colors)
            {
                if (updatedProduct.Colors.Contains(color.ColorName))
                {
                    if (!existingProduct.Colors.Contains(color))
                    {
                        existingProduct.Colors.Add(color);
                    }
                }
                else
                {
                    if (existingProduct.Colors.Contains(color))
                    {
                        existingProduct.Colors.Remove(color);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return existingProduct;

        }

        return null!;
    }

}
