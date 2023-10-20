using Manero_WebApp.Helpers.Services.AuthenticationServices;
using Manero_WebApp.Helpers.Services.UserServices;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers;

public class RegisterController : Controller
{
    private readonly RegisterService _registerService;
    private readonly CheckIfUserExistsService _checkIfUserExistsService;

    public RegisterController(RegisterService registerService, CheckIfUserExistsService checkIfUserExistsService)
    {
        _registerService = registerService;
        _checkIfUserExistsService = checkIfUserExistsService;
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
                return RedirectToAction("Index");
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
