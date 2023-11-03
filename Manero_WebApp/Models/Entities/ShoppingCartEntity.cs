using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Models
{
    public class ShoppingCartEntity
    {
        [Key]
        public int ShoppingCartId { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<CartItemEntity> CartItems { get; set; }

        public ShoppingCartEntity()
        {
            CartItems = new HashSet<CartItemEntity>();
        }
    }
}