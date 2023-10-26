using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Manero_WebApp.Services;
using Manero_WebApp.ViewModels.HomeViewModels;
using Manero_WebApp.Models.Schemas;

namespace Manero_WebApp.Controllers
{


    public class HomeController : Controller
    {
        private readonly ProductServices _productServices;

        public HomeController(ProductServices productServices)
        {
            _productServices = productServices;
        }

        public async Task <IActionResult> Index()
        {
            if (IsFirstVisit())
            {
                SetVisitedCookie();
                return View("WelcomeOnboarding");

            }
            var bestSellersViewModel = await BestSellers();
            var featuredProductsViewModel = await FeaturedProducts();

            var homeViewModel = new HomePageViewModel
            {
                BestSellers = bestSellersViewModel,
                FeaturedProducts = featuredProductsViewModel
            };

            return View(homeViewModel);
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

        /// <summary>
        /// Hämtar en produkt. Finns den inte så = 404.
        /// </summary>
        /// <param name="articleNumber"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProductDetails(Guid articleNumber)
        {
            var product = await _productServices.GetProductAsync(articleNumber);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductDetailsViewModel
            {
                ArticleNumber = product.ArticleNumber,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrls = product.ImageUrl,
                Categories = product.Category,
                Tags = product.Tags,
                Sizes = product.Sizes,
                Colours = product.Colours,
                InStock = product.InStock
            };

            return View(viewModel);
        }

        /// <summary>
        /// HÄMTAR TILL BEST SELLERS. Genererar tom lista om inget finns.
        /// </summary>
        /// <returns></returns>
        public async Task<BestSellerViewModel> BestSellers()
        {
            var bestSellerProducts = await _productServices.GetProductsByTagsAsync(new List<string> { "Best Seller" });

            if (bestSellerProducts == null)
            {
                bestSellerProducts = new List<ProductModel>();
            }

            var viewModel = new BestSellerViewModel { Products = bestSellerProducts.ToList() };
            return viewModel;
        }
        /// <summary>
        /// HÄMTAR TILL FEATURED PRODUCTS. Genererar tom lista om inget finns.
        /// </summary>
        /// <returns></returns>
        public async Task<FeaturedProductViewModel> FeaturedProducts()
        {
            var featuredProducts = await _productServices.GetProductsByTagsAsync(new List<string> { "Featured Product" });

            if (featuredProducts == null)
            {
                featuredProducts = new List<ProductModel>();
            }

            var viewModel = new FeaturedProductViewModel { Products = featuredProducts.ToList() };
            return viewModel;
        }
    }
}
