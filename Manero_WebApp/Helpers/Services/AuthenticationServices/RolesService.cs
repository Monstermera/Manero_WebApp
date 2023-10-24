using Manero_WebApp.Models.Entities;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Manero_WebApp.Helpers.Services.AuthenticationServices;

public class RolesService 
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesService(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<UserEntity> AddRoleAsync(UserEntity userModel)
    {
        //UserEntity user = viewModel;
        string roleName = "customer";

        if (!await _roleManager.Roles.AnyAsync())
        {
            await _roleManager.CreateAsync(new IdentityRole("admin"));
            await _roleManager.CreateAsync(new IdentityRole("customer"));

        }

        if (!await _userManager.Users.AnyAsync())
        {
            roleName = "admin";

        }
        else roleName = "customer";

        IdentityResult roleresult = await _userManager.AddToRoleAsync(userModel, roleName);
        //await _userManager.AddToRoleAsync(user, roleName);
        return userModel;
        
    }



}
