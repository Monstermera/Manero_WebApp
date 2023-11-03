using Microsoft.AspNetCore.Mvc;
using Manero_WebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Manero_WebApp.Contexts;

namespace Manero_WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly DataContext _context;

        public ShoppingCartController(DataContext context)
        {
            _context = context;
        }

        // Get the cart items for a specific cart
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartItems(int cartId)
        {
            var cartItems = await _context.CartItems
                .Where(ci => ci.ShoppingCartEntityId == cartId)
                .Include(ci => ci.Product)
                .ToListAsync();

            var totalPrice = cartItems.Sum(ci => ci.Quantity * ci.Product.Price);

            return Ok(new { Items = cartItems, TotalPrice = totalPrice });
        }

        // Add item to the cart
        [HttpPost("add")]
        public async Task<IActionResult> AddItemToCart(int cartId, Guid productArticleNumber, int quantity)
        {
            var cart = await _context.ShoppingCarts.FindAsync(cartId);
            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            var product = await _context.Products.FindAsync(productArticleNumber);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            var cartItem = new CartItemEntity
            {
                ShoppingCartEntityId = cartId,
                ProductArticleNumber = productArticleNumber,
                Quantity = quantity,
                Product = product 
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return Ok(cartItem);
        }

        // Remove item from the cart
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveItemFromCart(int cartId, Guid productArticleNumber)
        {
            var cartItem = await _context.CartItems
                .Where(ci => ci.ShoppingCartEntityId == cartId && ci.ProductArticleNumber == productArticleNumber)
                .FirstOrDefaultAsync();

            if (cartItem == null)
            {
                return NotFound("Item not found in the cart");
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok(cartItem);
        }
    }
}
