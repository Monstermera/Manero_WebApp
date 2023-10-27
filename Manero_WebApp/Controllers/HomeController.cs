using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Manero_WebApp.Services;
using Manero_WebApp.ViewModels.HomeViewModels;
using Manero_WebApp.Models.Schemas;
using Manero_WebApp.Helpers.Services.ProductServices;

namespace Manero_WebApp.Controllers
{


    public class HomeController : Controller
    {
        private readonly GetAllProductsService _getAllProductsService;
        private readonly GetOneProductService _getOneProductService;

        public HomeController(GetAllProductsService getAllProductsService, GetOneProductService getOneProductService)
        {
            _getAllProductsService = getAllProductsService;
            _getOneProductService = getOneProductService;
        }

        public async Task<IActionResult> Index()
        {
            if (IsFirstVisit())
            {
                SetVisitedCookie();
                return View("WelcomeOnboarding");

            }
            var products = await _getAllProductsService.GetAllAsync();

            var viewModel = new HomePageViewModel
            {
                AllProducts = products
            };

            return View(viewModel);
        }


        private bool IsFirstVisit()
        {
            var visitedCookie = Request.Cookies["Visited"];
            return string.IsNullOrEmpty(visitedCookie);
        }

        private void SetVisitedCookie()
        {
            if (!Request.Cookies.ContainsKey("Visited"))
            {
                var visitedCookie = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(2) 
                };

                Response.Cookies.Append("Visited", "true", visitedCookie);
            }
        }

        [HttpGet("product/{articleNumber}")]
        public async Task<IActionResult> ProductDetails(Guid articleNumber)
        {
            var product = await _getOneProductService.GetOneProductAsync(articleNumber);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
