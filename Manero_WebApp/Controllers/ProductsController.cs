using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Manero_WebApp.Controllers;

public class ProductsController : Controller
{
    private readonly ProductService _productService;
    private readonly DataContext _context;

    public ProductsController(ProductService productService, DataContext context)
    {
        _productService = productService;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> Edit(Guid Id)
    {
        var product = await _productService.GetAsync(Id);

        ViewBag.Tags = await _productService.GetProductPropertiesAsync(_context.Tags, tag => tag.TagName, tag => tag.Id.ToString());
        ViewBag.Sizes = await _productService.GetProductPropertiesAsync(_context.Sizes, size => size.SizeName, size => size.Id.ToString());
        ViewBag.Colors = await _productService.GetProductPropertiesAsync(_context.Colors, color => color.ColorName, color => color.Id.ToString());
        ViewBag.Categories = await _productService.GetProductPropertiesAsync(_context.Categories, category => category.CategoryName, category => category.Id.ToString());
        return View(product);
    }
}
