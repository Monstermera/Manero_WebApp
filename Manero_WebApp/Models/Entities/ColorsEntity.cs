using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models.Entities;

public class ColorsEntity
{
    [Key]
    public int Id { get; set; }
    public string ColorName { get; set; } = null!;
    public List<ProductEntity> Products { get; set; } = new();
}
