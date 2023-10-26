using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models.Entities;

public class ProductCategoryEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;
    public int CategoryId { get; set; }
    public CategoriesEntity Categories { get; set; } = null!;
}
