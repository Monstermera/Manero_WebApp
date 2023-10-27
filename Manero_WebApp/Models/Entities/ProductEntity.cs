using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Manero_WebApp.Models.Schemas;

namespace Manero_WebApp.Models.Entities;

public class ProductEntity
{
    [Key]
    public Guid ArticleNumber { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;

    [Required]
    [Column(TypeName = "money")]
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public List<ProductImageUrlEntity> ImageUrl { get; set; } = new();
    public List<ReviewsEntity> Reviews { get; set; } = new();
    public List<CategoriesEntity> Categories { get; set; } = new();
    public List<TagsEntity> Tags { get; set; } = new();
    public List<SizesEntity> Sizes { get; set; } = new();
    public List<ColorsEntity> Colors { get; set; } = new();
    [Required]
    public int AmountInStock { get; set; }

    #region implicit operators 
    public static implicit operator ProductModel(ProductEntity entity)
    {
        ProductModel model = new()
        {
            ArticleNumber = entity.ArticleNumber,
            Name = entity.Name,
            Price = entity.Price,
            Description = entity.Description,
            AmountInStock = entity.AmountInStock
        };


        // Categories
        if (entity.Categories.Count > 0)
        {
            foreach (var categories in entity.Categories)
            {
                model.Category.Add(categories.CategoryName);
            }
        }


        // Sizes
        if (entity.Sizes.Count > 0)
        {
            foreach (var size in entity.Sizes)
            {
                model.Sizes.Add(size.SizeName);
            }
        }

        // Colors
        if (entity.Colors.Count > 0)
        {
            foreach (var color in entity.Colors)
            {
                model.Colors.Add(color.ColorName);
            }
        }

        // Tags
        if (entity.Tags.Count > 0)
        {
            foreach (var tag in entity.Tags)
            {
                model.Tags.Add(tag.TagName);
            }
        }

        // ImageUrl
        if (entity.ImageUrl != null)
        {
            foreach (var imageUrl in entity.ImageUrl)
            {
                model.ImageUrl!.Add(imageUrl.ImageUrl);
            }
        }


        // Reviews
        if (entity.Reviews.Count > 0)
        {
            foreach (var reviews in entity.Reviews)
            {
                ReviewModel reviewModel = new()
                {
                    Id = reviews.Id,
                    User = reviews.User,
                    ProductId = reviews.ProductId,
                    DateCreated = reviews.DateCreated,
                    Rating = reviews.Rating,
                    ReviewDescription = reviews.Review
                };
                model.Reviews.Add(reviewModel);
            }
        }


        return model;
    }

    #endregion
}
