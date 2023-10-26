namespace Manero_WebApp.ViewModels.HomeViewModels
{
    public class ProductCardViewModel
    {
        public Guid ArticleNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int StarRating { get; set; }
        public int NumberOfReviews { get; set; }
        public bool IsOnSale { get; set; }
        public decimal? DiscountPrice { get; set; } // Nullable in case there is no discount
        public bool IsFavorite { get; set; }
    }
}
