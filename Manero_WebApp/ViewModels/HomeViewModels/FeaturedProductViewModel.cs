using Manero_WebApp.Models.Schemas;

namespace Manero_WebApp.ViewModels.HomeViewModels
{
    public class FeaturedProductViewModel
    {
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}
