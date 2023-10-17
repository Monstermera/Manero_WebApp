using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
