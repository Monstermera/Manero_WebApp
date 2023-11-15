using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.Models.Schemas;
using Manero_WebApp.ViewModels.HomeViewModels;
using Manero_WebApp.ViewModels.ProductsViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Manero_WebApp.Helpers.Services.ProductServices;

public class ProductService
{
    #region constructors & private fields

    private readonly DataContext _context;
    private readonly ProductDbRepo _productDbRepo;
    private readonly IGetAllProductsService _getAllProductsService;

    public ProductService(DataContext context, ProductDbRepo productDbRepo, IGetAllProductsService getAllProductsService)
    {
        _context = context;
        _productDbRepo = productDbRepo;
        _getAllProductsService = getAllProductsService;
    }

    #endregion

    //public async Task<ProductModel> AddAsync(ProductEntity entity)
    //{
    //    var _entity = await _productDbRepo.GetAsync(x => x.ArticleNumber == entity.ArticleNumber);
    //    if (_entity == null)
    //    {
    //        _entity = await _productDbRepo.AddAsync(entity);
    //        if (_entity != null)
    //            return _entity;
    //    }

    //    return null!;
    //}

    public async Task<ProductModel> GetAsync(Guid Id)
    {
        var _entity = await _productDbRepo.GetAsync(x => x.ArticleNumber == Id);
        if (_entity != null)
        {
            return _entity;
        }
        return null!;
           
    }

    public async Task<IEnumerable<SelectListItem>> GetProductPropertiesAsync<TEntity>(
        DbSet<TEntity> dbSet,
        Expression<Func<TEntity, string>> textSelector,
        Expression<Func<TEntity, string>> valueSelector)
        where TEntity : class
    {
        try
        {
            var items = new List<SelectListItem>();

            foreach (var item in await dbSet.ToListAsync())
            {
                items.Add(new SelectListItem
                {
                    Value = valueSelector.Compile()(item),
                    Text = textSelector.Compile()(item),
                });
            }

            return items;
        }
        catch
        {
            return null!;
        }
    }

    public async Task<CategoriesViewModel> PopulateCategoryViewModel(string category)
    {
        List<CategoryTileViewModel> categories = new();

        var products = await _getAllProductsService.GetAllAsync();
        var categoryEntities = await _context.Categories.ToListAsync();

        foreach (var item in categoryEntities)
        {
            var productsWithCategory = products.Where(p => p.Categories.Contains(item.CategoryName));
            var productWithCategory = productsWithCategory.FirstOrDefault();

            var categoryTile = new CategoryTileViewModel
            {
                Title = item.CategoryName,
                ImageUrl = productWithCategory?.ImageUrl?.FirstOrDefault() ?? "https://placehold.co/200x250?text=No%20image"
            };

            categories.Add(categoryTile);
        }

        var model = new CategoriesViewModel
        {
            Products = products.Where(p => p.Categories.Contains(category)).ToList(),
            Categories = categories
        };

        return model;
    }

    //public async Task<IEnumerable<SelectListItem>> GetSelectedPropertiesForProduct(
    //    DbSet<ProductSizesEntity> productSizeDbSet,
    //    DbSet<SizesEntity> sizeDbSet,
    //    Guid productId)
    //{
    //    try
    //    {
    //        var selectedSizeIds = await productSizeDbSet
    //            .Where(ps => ps.ProductId == productId)
    //            .Select(ps => ps.SizeId)
    //            .ToListAsync();

    //        var items = new List<SelectListItem>();
    //        foreach (var size in await sizeDbSet.ToListAsync())
    //        {
    //            items.Add(new SelectListItem
    //            {
    //                Value = size.Id.ToString() ?? "DefaultId",
    //                Text = size.SizeName ?? "DefaultSizeName",
    //                Selected = selectedSizeIds.Contains(size.Id),
    //            });
    //        }

    //        return items;
    //    }
    //    catch
    //    {
    //        return null!;
    //    }
    //}
}