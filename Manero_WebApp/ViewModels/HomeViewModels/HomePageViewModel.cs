using Manero_WebApp.Models.Schemas;

namespace Manero_WebApp.ViewModels.HomeViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<ProductModel> BestSellers { get; set; }
        public IEnumerable<ProductModel> FeaturedProducts { get; set; }
        public List<ProductModel> Products { get; set; }

    }

}
