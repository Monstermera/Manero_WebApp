namespace Manero_WebApp.Helpers.Services.ProductServices;

using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Models.Schemas;

public class WishlistProductService
{
    private readonly WishlistRepo _wishlistRepo;
    private readonly IGetOneProductService _getOneProductService;

    public WishlistProductService(WishlistRepo wishlistRepo, IGetOneProductService getOneProductService)
	{
        _wishlistRepo = wishlistRepo;
        _getOneProductService = getOneProductService;
    }

    public async Task<bool> Add(Guid articleNumber)
    {
        var product = await _getOneProductService.GetOneProductAsync(articleNumber);

        return await Add(product);
    }

    public async Task<bool> Add(ProductModel model)
	{
		if (model != null)
		{
            return await _wishlistRepo.Add(model);
        }

        return false;
    }

    public async Task<bool> Remove(Guid articleNumber)
    {
        var product = await _getOneProductService.GetOneProductAsync(articleNumber);

        return await Remove(product);
    }

    public async Task<bool> Remove(ProductModel model)
    {
        if (model != null)
        {
            return await _wishlistRepo.Remove(model);
        }

        return false;
    }

    public async Task<IEnumerable<ProductModel>> GetAll()
    {
        return await _wishlistRepo.GetAll();
    }
 }