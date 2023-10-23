using Manero_WebApp.Models.Schemas;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models.Entities;

public class UserEntity : IdentityUser
{
    [StringLength(50)]
    public string FullName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public ICollection<UserAdressEntity> Address { get; set; } = new List<UserAdressEntity>();
    public ICollection<ReviewsEntity> Reviews { get; set; } = new List<ReviewsEntity>();

    public static implicit operator UserModel(UserEntity model)
    {
        return new UserModel
        {
            Id = model.Id,
            FullName = model.FullName,
            Email = model.Email!,
            PhoneNumber = model.PhoneNumber!,
            ProfileImgUrl = model.ImageUrl!
        };
    }
}
