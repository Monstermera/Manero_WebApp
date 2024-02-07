using Manero_WebApp.Helpers.Services.ShoppingCartServices;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Manero_WebApp.Controllers;
[Authorize]
public class ShoppingCartController : Controller
{

    private readonly ShoppingCartService _shoppingCartService;

    public ShoppingCartController(ShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }
    public IActionResult Index()
    {
        return View();
    }

    public string GetCurrentUser()
    {
        ClaimsPrincipal currentUser = User;
        var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

        if (currentUserID != null)
        {
            return currentUserID;
        }

        return string.Empty;
    }

    [HttpPost]
    public IActionResult AddProduct(LocalProductViewModel model)
    {
        var currentUserID = GetCurrentUser();

        if (model != null && currentUserID != null)
        {
            _shoppingCartService.AddToCartAsync(currentUserID, model.ArticleNumber);
        }

        return RedirectToAction("Index", "Home");
    }
}