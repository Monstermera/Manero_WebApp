
using Manero_WebApp.Models.Entities;
using Manero_WebApp.Models.Schemas;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Manero_WebApp.Helpers.Services.AuthenticationServices;

public class RegisterService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RolesService _rolesService;

    public RegisterService(UserManager<UserEntity> userManager, RolesService rolesService)
    {
        _userManager = userManager;
        _rolesService = rolesService;
    }

    public async Task<bool> RegisterAsync(RegistrationViewModel registrationViewModel)
    {
        UserEntity user = registrationViewModel;

        var result = await _userManager.CreateAsync(user, registrationViewModel.Password);
        if (result.Succeeded)
        {
            var test = await _rolesService.AddRoleAsync(registrationViewModel);
            return true;
        }
        return false;
    }
}
