using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Services.ProductServices;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.Models.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Manero_WebApp.Controllers;

public class ProductsController : Controller
{
    private readonly ProductService _productService;
    private readonly DataContext _context;
    private readonly UpdateProductService _updateProductService;

    public ProductsController(ProductService productService, DataContext context, UpdateProductService updateProductService)
    {
        _productService = productService;
        _context = context;
        _updateProductService = updateProductService;
    }

    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> Edit(Guid Id)
    {
        ProductModel product = await _productService.GetAsync(Id);      

        ViewBag.Tags = await _productService.GetProductPropertiesAsync(_context.Tags, tag => tag.TagName, tag => tag.Id.ToString());
        ViewBag.Sizes = await _productService.GetProductPropertiesAsync(_context.Sizes, size => size.SizeName, size => size.Id.ToString());
        ViewBag.Colors = await _productService.GetProductPropertiesAsync(_context.Colors, color => color.ColorName, color => color.Id.ToString());
        ViewBag.Categories = await _productService.GetProductPropertiesAsync(_context.Categories, category => category.CategoryName, category => category.Id.ToString());
       
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductModel updatedProduct)
    {
        var result = await _updateProductService.UpdateAsync(updatedProduct);
       
        if (result != null)
        {
            ViewBag.Tags = await _productService.GetProductPropertiesAsync(_context.Tags, tag => tag.TagName, tag => tag.Id.ToString());
            ViewBag.Sizes = await _productService.GetProductPropertiesAsync(_context.Sizes, size => size.SizeName, size => size.Id.ToString());
            ViewBag.Colors = await _productService.GetProductPropertiesAsync(_context.Colors, color => color.ColorName, color => color.Id.ToString());
            ViewBag.Categories = await _productService.GetProductPropertiesAsync(_context.Categories, category => category.CategoryName, category => category.Id.ToString());
            
            ProductModel model = result;
           
            return View(model);
        }

        return View();
    }
}
