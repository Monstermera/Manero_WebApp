namespace Manero_WebApp.Models.Entities
{
    public class ProductImageUrlEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ImageUrl { get; set; } = null!;
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;
    }
}
