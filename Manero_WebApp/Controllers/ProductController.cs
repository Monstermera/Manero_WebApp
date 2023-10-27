using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers
{
	public class ProductController : Controller
	{
        #region constructors & private fields

        #endregion


        public async Task<IActionResult> Index()
		{
			return View();
		}
	}
}
