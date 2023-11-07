using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers;

public class AddressController : Controller
{
    public IActionResult AddAdress(AddressViewModel model, Guid userId)
    {


        return View();
    }
}
