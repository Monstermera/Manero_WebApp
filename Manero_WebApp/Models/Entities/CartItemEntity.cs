using Manero_WebApp.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manero_WebApp.Models
{
    public class CartItemEntity
    {
        public int Id { get; set; }
        public Guid ProductArticleNumber { get; set; } 
        public int Quantity { get; set; }

        // Navigation properties
        public ProductEntity? Product { get; set; }

        [ForeignKey("ShoppingCartEntity")]
        public int ShoppingCartEntityId { get; set; }
        public ShoppingCartEntity? ShoppingCartEntity { get; set; }
    }
}