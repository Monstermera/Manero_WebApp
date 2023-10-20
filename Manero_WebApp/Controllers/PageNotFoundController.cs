using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers
{
    public class PageNotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
