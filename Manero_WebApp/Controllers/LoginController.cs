using Manero_WebApp.Helpers.Services.AuthenticationServices;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        private readonly SignInManager<UserEntity> _signInManager;

        public LoginController(LoginService loginService, SignInManager<UserEntity> signInManager)
        {
            _loginService = loginService;
            _signInManager = signInManager;
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
    }
}
