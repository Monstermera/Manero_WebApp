using Manero_WebApp.Helpers.Services.ProductServices;
using Manero_WebApp.ViewModels.HomeViewModels;
using Manero_WebApp.ViewModels.WishlistViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers
{
    public class WishlistController : Controller
    {
        private readonly WishlistProductService _wishlistProductService;

        public WishlistController(WishlistProductService wishlistProductService)
        {
            _wishlistProductService = wishlistProductService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _wishlistProductService.GetAll();

            return View(new WishlistViewModel
            {
                Products = products
            });
        }

        public async Task<IActionResult> Add(Guid articleNumber)
        {
            var added = await _wishlistProductService.Add(articleNumber);

            return added ? Ok(articleNumber) : NotFound(articleNumber);
        }

        public async Task<IActionResult> Remove(Guid articleNumber)
        {
            await _wishlistProductService.Remove(articleNumber);

            // The result doesn't matter, either way it doesn't exist.
            return Ok(articleNumber);
        }
    }
}
