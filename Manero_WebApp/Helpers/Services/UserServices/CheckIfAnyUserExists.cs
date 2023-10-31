using Manero_WebApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Manero_WebApp.Helpers.Services.AuthenticationServices;

public class CheckIfAnyUserExists
{
    private readonly UserManager<UserEntity> _userManager;

    public CheckIfAnyUserExists(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CheckIfAnyUserExistsAsync()
    {
        if (!await _userManager.Users.AnyAsync())
        {
            return true;
        } return false;
    }
}