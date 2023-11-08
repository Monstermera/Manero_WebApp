using Manero_WebApp.Helpers.Services.UserServices;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers;

public class AddressController : Controller
{
    private readonly AddressService _addressService;
    private readonly UserManager<UserEntity> _userManager;

    public AddressController(AddressService addressService, UserManager<UserEntity> userManager)
    {
        _addressService = addressService;
        _userManager = userManager;
    }



    public IActionResult AddAddress()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddAddress(AddressViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var result = await _addressService.AddAddressAsync(model, user);
                if (result.result == IdentityResult.Success)
                {
                    return RedirectToAction("MyAdresses", "Account");
                }
                ModelState.AddModelError("", result.message);
                return View(model);
            }
            ModelState.AddModelError("", "The user account was not found.");
            return View(model);
        }
        return View(model);
    }
}
