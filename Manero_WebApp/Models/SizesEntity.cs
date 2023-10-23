using Manero_WebApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models;

public class SizesEntity
{
    [Key]
    public int Id { get; set; }
    public string SizeName { get; set; } = null!;
    public ICollection<ProductSizesEntity> ProductSizes { get; set; } = new List<ProductSizesEntity>();
}
