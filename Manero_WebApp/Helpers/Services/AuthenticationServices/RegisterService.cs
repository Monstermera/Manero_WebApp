
using Manero_WebApp.Models.Entities;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;

namespace Manero_WebApp.Helpers.Services.AuthenticationServices;

public class RegisterService
{
    private readonly UserManager<UserEntity> _userManager;

    public RegisterService(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> RegisterAsync(RegistrationViewModel registrationViewModel)
    {
        UserEntity user = registrationViewModel;

        var result = await _userManager.CreateAsync(user, registrationViewModel.Password);
        if (result.Succeeded)
        {
            return true;
        }
        return false;
    }
}
