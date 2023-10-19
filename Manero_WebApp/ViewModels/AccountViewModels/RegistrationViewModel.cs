using Manero_WebApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.ViewModels.AccountViewModels;

public class RegistrationViewModel
{
    [Display(Name = "First Name*")]
    [Required(ErrorMessage = "Please fill in your first name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name*")]
    [Required(ErrorMessage = "Please fill in your last name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Mobile (Optional)")]
    public string? PhoneNumber { get; set; } = null!;

    [Display(Name = "E-mail*")]
    [Required(ErrorMessage = "Please fill in your email adress")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please provide an valid email adress.")]
    public string Email { get; set; } = null!;


    [Display(Name = "Passwords*")]
    [Required(ErrorMessage = "Please fill in a password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password*")]
    [Required(ErrorMessage = "Please confirm the password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The confirmation password do not match the original password.")]
    public string ConfirmPassword { get; set; } = null!;



    public static implicit operator UserEntity(RegistrationViewModel model)
    {
        return new UserEntity
        {
            UserName = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
        };
    }

}
