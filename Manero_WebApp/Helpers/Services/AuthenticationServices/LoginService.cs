using Manero_WebApp.Models.Entities;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Manero_WebApp.Helpers.Services.AuthenticationServices;

public class LoginService : ILoginService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInService;

    public LoginService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInService)
    {
        _userManager = userManager;
        _signInService = signInService;
    }

    //Login user
    public async Task<bool> LoginAsync(SignInViewModel model)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
        if (user != null)
        {
            var result = await _signInService.PasswordSignInAsync(user, model.Password, model.KeepMeSignedIn, false);
            return result.Succeeded;
        }
        return false;
    }
}
