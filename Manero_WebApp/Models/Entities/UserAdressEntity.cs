using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models.Entities;

public class UserAdressEntity
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public int AddressId { get; set; }
    public AdressEntity Address { get; set; } = null!;
}
