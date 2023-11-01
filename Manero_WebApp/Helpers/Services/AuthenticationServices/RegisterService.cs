using Manero_WebApp.Models.Entities;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Manero_WebApp.Helpers.Services.AuthenticationServices
{
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
                var rolesAdded = await _rolesService.AddRoleAsync(user);
                if (rolesAdded.Succeeded)
                {
                    return true;
                }
                else { 
                    await _userManager.DeleteAsync(user);
                    return false; 
                }
            }
            return false;
        }
    }
}
