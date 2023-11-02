using Manero_WebApp.Helpers.Services.AuthenticationServices;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace Manero_WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RolesService _rolesService;

        public LoginController(LoginService loginService, SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, RolesService rolesService)
        {
            _loginService = loginService;
            _signInManager = signInManager;
            _userManager = userManager;
            _rolesService = rolesService;
        }

        //Login
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _loginService.LoginAsync(model))
                    return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Incorrect email or password");
            return View(model);
        }





        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "Login");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo loginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
                return RedirectToAction("Index", "Home");

            var result = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, false);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
            {
                UserEntity user = new UserEntity
                {
                    Email = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value,
                    FullName = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value,
                    ImageUrl = loginInfo.Principal.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri")
            };

                IdentityResult identResult = await _userManager.CreateAsync(user);

                if (identResult.Succeeded)
                {
                    var userEntity = await _userManager.FindByIdAsync(user.Id);
                    if (userEntity != null)
                    {
                        var roleResult = await _rolesService.AddRoleAsync(userEntity);
                        if (roleResult.Succeeded)
                        {
                            identResult = await _userManager.AddLoginAsync(user, loginInfo);
                            if (identResult.Succeeded)
                            {
                                await _signInManager.SignInAsync(user, false);
                                return RedirectToAction("Index", "Home");
                            }
                        }                        
                    }                    
                }
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
