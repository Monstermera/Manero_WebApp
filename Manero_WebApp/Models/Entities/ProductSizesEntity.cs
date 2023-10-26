using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models.Entities;

public class ProductSizesEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;
    public int SizeId { get; set; }
    public SizesEntity ProductSize { get; set; } = null!;
}
