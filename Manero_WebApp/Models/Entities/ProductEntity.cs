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
    public ICollection<ProductImageUrlEntity> ImageUrl { get; set; } = new List<ProductImageUrlEntity>();

    public ICollection<ReviewsEntity> Reviews { get; set; } = new List<ReviewsEntity>();

    public ICollection<ProductCategoryEntity> Categories { get; set; } = new List<ProductCategoryEntity>();
    public ICollection<ProductTagsEntity> Tags { get; set; } = new List<ProductTagsEntity>();
    public ICollection<ProductSizesEntity> Sizes { get; set; } = new List<ProductSizesEntity>();
    public ICollection<ProductColorsEntity> Colours { get; set; } = new List<ProductColorsEntity>();
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



