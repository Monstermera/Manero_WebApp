using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (IsFirstVisit())
            {
                SetVisitedCookie();
                return View("WelcomeOnboarding");

            }
            return View();
        }


        private bool IsFirstVisit()
        {
            var visitedCookie = Request.Cookies["Visited"];
            return string.IsNullOrEmpty(visitedCookie);
        }

        private void SetVisitedCookie()
        {
            var visitedCookie = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(10)
            };

            Response.Cookies.Append("Visited", "true", visitedCookie);
        }

    }  
}




