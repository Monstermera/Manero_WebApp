using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.Models.Schemas;
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
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductService(DataContext context, ProductDbRepo productDbRepo, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _productDbRepo = productDbRepo;
        _webHostEnvironment = webHostEnvironment;
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