using Manero_WebApp.Models.Schemas;
using Manero_WebApp.ViewModels.HomeViewModels;

namespace Manero_WebApp.ViewModels.ProductsViewModels;

public class CategoriesViewModel
{
    public List<CategoryTileViewModel>? Categories { get; set; }
    public IEnumerable<ProductModel>? Products { get; set; }
}
