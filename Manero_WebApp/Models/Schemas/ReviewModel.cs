namespace Manero_WebApp.Models.Schemas;

public class ReviewModel
{
    public Guid Id { get; set; }
    public UserModel User { get; set; } = null!;
    public Guid ProductId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public int Rating { get; set; }
    public string ReviewDescription { get; set; } = null!;
}
