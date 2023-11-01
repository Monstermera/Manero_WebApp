using Manero_WebApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Manero_WebApp.Helpers.Services.AuthenticationServices
{
    public class RolesService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesService(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<IdentityResult> AddRoleAsync(UserEntity userModel)
        {
            var roleName = "customer";

            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
                await _roleManager.CreateAsync(new IdentityRole("customer"));
            }

            if (await _userManager.Users.CountAsync() == 1)
            {
                roleName = "admin";
            }

            return await _userManager.AddToRoleAsync(userModel, roleName);
        }
    }
}
