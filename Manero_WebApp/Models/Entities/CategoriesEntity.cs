using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models.Entities;

public class CategoriesEntity
{
    [Key]
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public List<ProductEntity> Products { get; set; } = new();
}
