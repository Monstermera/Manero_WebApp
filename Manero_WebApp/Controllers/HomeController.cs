using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Manero_WebApp.ViewModels.HomeViewModels;
using Manero_WebApp.Models.Schemas;
using Manero_WebApp.Helpers.Services.ProductServices;

namespace Manero_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetAllProductsService _getAllProductsService;
        private readonly IGetOneProductService _getOneProductService;

        public HomeController(IGetAllProductsService getAllProductsService, IGetOneProductService getOneProductService)
        {
            _getAllProductsService = getAllProductsService;
            _getOneProductService = getOneProductService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Home";
            if (IsFirstVisit())
            {
                SetVisitedCookie();
                return View("WelcomeOnboarding");
            }

            var products = await _getAllProductsService.GetAllAsync();
            var viewModel = CreateHomePageViewModel(products);

            ViewBag.LocalProductViewModel = new LocalProductViewModel
            {
                Products = products
            };
            return View(viewModel);
        }

        private HomePageViewModel CreateHomePageViewModel(IEnumerable<ProductModel> products)
        {
            // JUSTERA || med vilka tags som ska visas
            var bestSellers = products.Where(p => p.Tags.Contains("top") || p.Tags.Contains("new") || p.Tags.Contains("sale"));
            var featuredProducts = products.Where(p => p.Tags.Contains("new"));

            return new HomePageViewModel
            {
                BestSellers = bestSellers,
                FeaturedProducts = featuredProducts
            };
        }

        
        public bool IsFirstVisit()
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
                    Expires = DateTime.Now.AddYears(1)
                };

                Response.Cookies.Append("Visited", "true", visitedCookie);
            }
        }

        [HttpGet("product/{articleNumber}")]
        public async Task<IActionResult> ProductDetails(int articleNumber)
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
