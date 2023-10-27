using Azure;
using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.Models.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Manero_WebApp.Helpers.Services.ProductServices;

public class AddProductService
{
    #region constructors & private fields

    private readonly DataContext _context;
    private readonly ProductDbRepo _productDbRepo;

    public AddProductService(DataContext context, ProductDbRepo productDbRepo)
    {
        _context = context;
        _productDbRepo = productDbRepo;
    }

    #endregion

    public async Task<ProductModel> AddAsync(ProductEntity entity)
    {
        var _entity = await _productDbRepo.GetAsync(x => x.ArticleNumber == entity.ArticleNumber);
        if (_entity == null)
        {
            _entity = await _productDbRepo.AddAsync(entity);
            if (_entity != null)
                return _entity;
        }
        return null!;
    }

}
