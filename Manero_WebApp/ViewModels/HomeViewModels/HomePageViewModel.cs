namespace Manero_WebApp.ViewModels.HomeViewModels
{
    public class HomePageViewModel
    {
        public BestSellerViewModel BestSellers { get; set; } = new BestSellerViewModel();
        public FeaturedProductViewModel FeaturedProducts { get; set; } = new FeaturedProductViewModel();
    }
}
