using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models.Entities
{
    public class ProductColorsEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;
        public int ColorId { get; set; }
        public ColorsEntity Color { get; set; } = null!;
    }
}
