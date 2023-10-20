namespace Manero_WebApp.Models.Schemas;

public class ReviewModel
{
    public string FullName { get; set; } = null!;
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string ProfileImageUrl { get; set; } = null!;
    public int StarRating { get; set; }
    public string ReviewDescription { get; set; } = null!;
}
