namespace Manero_WebApp.Models.Entities;

public class ReviewsEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Review { get; set; } = null!;
    public int Rating { get; set; }
    public DateTime DateCreated { get; set; }
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;
    public string UserId { get; set; }
    public UserEntity User { get; set; } = null!;
}
