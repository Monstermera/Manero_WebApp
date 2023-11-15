using Manero_WebApp.Helpers.Services.AuthenticationServices;
using Manero_WebApp.Helpers.Services.UserServices;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.Models.Schemas;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers;

public class RegisterController : Controller
{
    private readonly IRegisterService _registerService;
    private readonly ICheckIfUserExistsService _checkIfUserExistsService;
    private readonly LoginService _loginService;

    public RegisterController(IRegisterService registerService, ICheckIfUserExistsService checkIfUserExistsService, LoginService loginService)
    {
        _registerService = registerService;
        _checkIfUserExistsService = checkIfUserExistsService;
        _loginService = loginService;
    }


    //Register
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index(RegistrationViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (await _checkIfUserExistsService.UserExistsAsync(x => x.Email == model.Email))
            {
                ModelState.AddModelError("", "A user with the same email already exists");
                return View(model);
            }

            if (await _registerService.RegisterAsync(model))
            {
                if (await _loginService.LoginAsync(new SignInViewModel { Email = model.Email, Password = model.Password, KeepMeSignedIn = true }))
                {
                    return RedirectToAction("Success");
                }
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Success()
    {
        return View();
    }
}
