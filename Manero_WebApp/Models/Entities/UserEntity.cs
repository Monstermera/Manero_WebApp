using Manero_WebApp.Models.Schemas;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models.Entities;

public class UserEntity : IdentityUser
{
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public ICollection<UserAdressEntity> Address { get; set; } = new List<UserAdressEntity>();

    public static implicit operator UserModel(UserEntity model)
    {
        return new UserModel
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email!,
            PhoneNumber = model.PhoneNumber!,
            ProfileImgUrl = model.ImageUrl!
        };
    }
}
