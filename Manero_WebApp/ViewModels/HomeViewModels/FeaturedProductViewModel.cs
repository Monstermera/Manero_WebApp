using Manero_WebApp.Models.Schemas;

namespace Manero_WebApp.ViewModels.HomeViewModels
{
    public class FeaturedProductViewModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
    }
}
