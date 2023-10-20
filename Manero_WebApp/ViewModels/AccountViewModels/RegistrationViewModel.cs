using Manero_WebApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.ViewModels.AccountViewModels;

public class RegistrationViewModel
{

    [Display(Name = "Name*")]
    [Required(ErrorMessage = "Please fill in your name")]
    public string FullName { get; set; } = null!;


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
            FullName = model.FullName,
            Email = model.Email,
        };
    }

}
