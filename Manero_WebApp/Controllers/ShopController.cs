using Microsoft.AspNetCore.Mvc;

public class ShopController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}