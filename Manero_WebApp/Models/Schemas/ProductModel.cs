namespace Manero_WebApp.Models.Schemas;

public class ProductModel
{
    public Guid ArticleNumber { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public List<string>? ImageUrl { get; set; } = new List<string>();
    public List<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
    public List<string> Category { get; set; } = new List<string>();
    public List<string> Tags { get; set; } = new List<string>();    
    public List<string> Sizes { get; set; } = new List<string>();
    public List<string> Colours { get; set; } = new List<string>();
    public int AmountInStock { get; set; }
}
