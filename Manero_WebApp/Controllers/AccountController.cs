using Manero_WebApp.Helpers.Services.AuthenticationServices;
using Manero_WebApp.Helpers.Services.UserServices;
using Manero_WebApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers;

public class AccountController : Controller
{

    private readonly SignInManager<UserEntity> _signInManager;


    public AccountController(SignInManager<UserEntity> signInManager)
    {
        _signInManager = signInManager;
    }
    public IActionResult Index()
	{
		return View();
	}

    //Logout
    public async Task<IActionResult> Logout()
    {
        if (_signInManager.IsSignedIn(User))
        {
            await _signInManager.SignOutAsync();
        }

        return LocalRedirect("/");
    }
}
