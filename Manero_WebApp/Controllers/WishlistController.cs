using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
