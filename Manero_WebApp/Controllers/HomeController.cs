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

        public IActionResult Categories()
        {
            return View();
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

    }  
}




