using Manero_WebApp.Helpers.Services.AuthenticationServices;
using Manero_WebApp.Helpers.Services.UserServices;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Manero_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager; // Add UserManager to retrieve user information
        private readonly AddressService _addressService;
        public AccountController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, AddressService addressService)
        {
            _signInManager = signInManager;
            _userManager = userManager; // Inject UserManager
            _addressService = addressService;
        }

        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                var fullName = user?.FullName;
                var email = user?.Email;

                ViewData["FullName"] = fullName;
                ViewData["Email"] = email;

            }

            return View();
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            return LocalRedirect("/");
        }


        //Account Addresses
        public async Task<IActionResult> MyAddress()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var userResult = await _userManager.Users.Include(x => x.Addresses).SingleOrDefaultAsync(x => x.Id == user.Id);
                    if (userResult != null)
                    {
                        return View(userResult.Addresses);
                    }
                    return View();
                }
            }
            return LocalRedirect("/");
        }

        //Add new address page
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
                        return RedirectToAction("MyAddress");
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
}
