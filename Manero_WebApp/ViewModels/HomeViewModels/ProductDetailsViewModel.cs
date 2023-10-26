namespace Manero_WebApp.ViewModels.HomeViewModels
{
    public class ProductDetailsViewModel
    {
        public Guid ArticleNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<string>? ImageUrls { get; set; }
        public List<ReviewViewModel>? Reviews { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public List<string> Tags { get; set; } = new List<string>();
        public List<string> Sizes { get; set; } = new List<string>();
        public List<string> Colours { get; set; } = new List<string>();
        public int InStock { get; set; }

        public string FormattedPrice => $"${Price:N2}";
    }

    public class ReviewViewModel
    {
        public string Username { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
