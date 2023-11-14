using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.ViewModels.AccountViewModels;

public class ForgotPasswordViewModel
{
    [Display(Name = "E-Mail*")]
    [Required(ErrorMessage = "You must provide an e-mail")]
    public string Email { get; set; } = null!;


}
