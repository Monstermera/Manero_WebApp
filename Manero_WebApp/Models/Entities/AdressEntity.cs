using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models.Entities;

public class AdressEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string StreetName { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "char(6)")]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string City { get; set; } = string.Empty;

    public List<UserEntity> Users { get; set; } = new();
}
