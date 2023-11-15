namespace Manero_WebApp.Helpers.Repositories;

using Manero_WebApp.Models.Schemas;

public class WishlistRepo
{
    private HashSet<ProductModel> _wishlist;

    public WishlistRepo()
    {
        _wishlist = new HashSet<ProductModel>();
    }

    public async Task<bool> Add(ProductModel model)
    {
        if (model != null)
        {
            return _wishlist.Add(model);
        }

        return false;
    }

    public async Task<bool> Remove(ProductModel model)
    {
        if (model != null)
        {
            return _wishlist.Remove(model);
        }

        return false;
    }

    public async Task<IEnumerable<ProductModel>> GetAll()
    {
        return _wishlist.ToList();
    }
}
