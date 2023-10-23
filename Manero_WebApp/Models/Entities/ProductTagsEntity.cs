using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models.Entities;

public class ProductTagsEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;
    public int TagId { get; set; }
    public TagsEntity Tag { get; set; } = null!;
}
