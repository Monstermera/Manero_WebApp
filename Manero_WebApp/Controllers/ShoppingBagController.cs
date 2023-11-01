using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers
{
    public class ShoppingBagController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Shopping bag";

            return View();
        }
    }
}
