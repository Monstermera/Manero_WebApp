using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers;

public class ForgotPasswordController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
