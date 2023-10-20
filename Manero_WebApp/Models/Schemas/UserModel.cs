using Manero_WebApp.Models.Entities;

namespace Manero_WebApp.Models.Schemas;

public class UserModel
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? ProfileImgUrl { get; set; }


    public static implicit operator UserEntity(UserModel model)
    {
        return new UserEntity
        {
            Id = model.Id,
            UserName = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber
        };
    }
}
