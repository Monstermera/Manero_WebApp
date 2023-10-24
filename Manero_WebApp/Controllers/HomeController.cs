using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Categories()
        {
            return View();
        }
    }
}
