using Manero_WebApp.Helpers.Services.UserServices;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers;

public class AddressController : Controller
{
    private readonly AddressService _addressService;

    public AddressController(AddressService addressService)
    {
        _addressService = addressService;
    }



    public IActionResult AddAdress()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddAdress(AddressViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _addressService.AddAddressAsync(model);
            if (result.result == IdentityResult.Success)
            {
                RedirectToAction("MyAdresses", "Account");
            }
            ModelState.AddModelError("", result.message);
            return View(model);
        }
        return View(model);
    }
}
