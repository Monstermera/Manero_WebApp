using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models.Entities;

public class SizesEntity
{
    [Key]
    public int Id { get; set; }
    public string SizeName { get; set; } = null!;
    public List<ProductEntity> ProductSizes { get; set; } = new();
}
