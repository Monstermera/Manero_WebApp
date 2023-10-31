using Manero_WebApp.Models.Schemas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        return new ProductModel
        {
            ArticleNumber = entity.ArticleNumber,
            Name = entity.Name,
            Price = entity.Price,
            Description = entity.Description,
            AmountInStock = entity.AmountInStock
        };

    }
    #endregion

}



