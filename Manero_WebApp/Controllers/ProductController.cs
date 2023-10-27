using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Services.ProductServices;
using Manero_WebApp.Models.Schemas;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers
{
	public class ProductController : Controller
	{
        #region constructors & private fields
        private readonly GetAllProductsService _allProductService;

		public ProductController(GetAllProductsService allProductService)
		{
			_allProductService = allProductService;
		}


		#endregion

		public async Task<IActionResult> Index()
		{
			var result = await _allProductService.GetAllAsync();

			return View(result);
        }
	}
}
