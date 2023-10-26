using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models.Entities;

public class TagsEntity
{
    [Key]
    public int Id { get; set; }
    public string TagName { get; set; } = null!;
    public ICollection<ProductTagsEntity> Tags { get; set; } = new List<ProductTagsEntity>();
}
