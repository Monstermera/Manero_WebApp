using Manero_WebApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Manero_WebApp.Helpers.Services.UserServices;

public class CheckIfUserExistsService : ICheckIfUserExistsService
{
    private readonly UserManager<UserEntity> _userManager;

    public CheckIfUserExistsService(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    //Check if user already exists
    public async Task<bool> UserExistsAsync(Expression<Func<UserEntity, bool>> expression)
    {
        return await _userManager.Users.AnyAsync(expression);
    }
}
