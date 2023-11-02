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
    private readonly AddProductService _addProductService;

    public ProductsController(ProductService productService, DataContext context, UpdateProductService updateProductService, AddProductService addProductService)
    {
        _productService = productService;
        _context = context;
        _updateProductService = updateProductService;
        _addProductService = addProductService;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Products";
        return View();
    }


    //Add or Edit Page
    [HttpGet]
    public async Task<IActionResult> AddOrEdit(Guid Id)
    {
        ProductModel product = await _productService.GetAsync(Id);      

        ViewBag.Tags = await _productService.GetProductPropertiesAsync(_context.Tags, tag => tag.TagName, tag => tag.Id.ToString());
        ViewBag.Sizes = await _productService.GetProductPropertiesAsync(_context.Sizes, size => size.SizeName, size => size.Id.ToString());
        ViewBag.Colors = await _productService.GetProductPropertiesAsync(_context.Colors, color => color.ColorName, color => color.Id.ToString());
        ViewBag.Categories = await _productService.GetProductPropertiesAsync(_context.Categories, category => category.CategoryName, category => category.Id.ToString());
       
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrEdit(ProductModel updatedProduct)
    {
        ProductModel result = null!;
        if (updatedProduct.ArticleNumber == Guid.Empty)
        {
            result = await _addProductService.AddAsync(updatedProduct);
        } else
        {
            result = await _updateProductService.UpdateAsync(updatedProduct);
        }

       
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

    //Categories Page
    public IActionResult Categories()
    {
        return View();
    }
}
