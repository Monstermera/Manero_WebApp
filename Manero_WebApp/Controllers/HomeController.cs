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

        public async Task<IActionResult> Index()
        {
            var productModels = await _productServices.GetAllProductsAsync();

            var productCardViewModels = productModels.Select(pm => new ProductCardViewModel
            {
                ArticleNumber = pm.ArticleNumber,
                Name = pm.Name,
                Price = pm.Price,
                NumberOfReviews = pm.Reviews.Count,
                StarRating = pm.Reviews.Any() ? (int)Math.Round(pm.Reviews.Average(r => r.Rating)) : 0,

            }).ToList();

            var homeViewModel = new HomePageViewModel
            {
                BestSellers = new ProductListViewModel
                {
                    Products = productCardViewModels
                }
            };

            // Your existing logic for first visit and setting cookies
            if (IsFirstVisit())
            {
                SetVisitedCookie();
                return View("WelcomeOnboarding");
            }

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
            var product = await _productServices.GetProductWithReviewsAsync(articleNumber);
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
                ImageUrls = product.ImageUrl.Select(c => c.ImageUrl).ToList(),
                Categories = product.Categories.Select(c => c.CategoryName).ToList(),
                Tags = product.Tags.Select(c => c.TagName).ToList(),
                Sizes = product.Sizes.Select(c => c.SizeName).ToList(),
                Colours = product.Colors.Select(c => c.ColorName).ToList(),
                InStock = product.AmountInStock,
                Reviews = product.Reviews.Select(r => new ReviewViewModel
                {
                    Username = r.User?.FullName ?? "Unknown User",
                    Content = r.Review,
                    Rating = r.Rating,
                    DatePosted = r.DateCreated
                }).ToList()
            };

            return View(viewModel);
        }


    }
}
