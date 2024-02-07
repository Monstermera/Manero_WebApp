using Manero_WebApp.Contexts;
using Manero_WebApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using SQLitePCL;

namespace Manero_WebApp.Helpers.Services.ShoppingCartServices;

public class ShoppingCartService 
{

    private readonly DataContext _context;
    public ShoppingCartService(DataContext context)
    {
        _context = context;
    }

    public void AddToCartAsync(string userId, int productId)
    {
        if (userId != null)
        {
            if (_context.ShoppingCarts.Any(x => x.ProductId == productId && x.UserId == userId))
            {
                return;
            }
            var shoppingCartEntity = new ShoppingCartEntity
            {
                UserId = userId,
                ProductId = productId
            };

            _context.ShoppingCarts.Add(shoppingCartEntity);
            _context.SaveChanges();
        }
    }
    public IEnumerable<ProductEntity> GetShoppingCart(string userId)
    {
        var cart = _context.ShoppingCarts
            .Where(x => x.UserId == userId)
            .Select(x => x.Product)
            .ToList();


        if (cart != null)
        {
            return cart;
        }

        return Enumerable.Empty<ProductEntity>();
    }

    public decimal OrderPrice(IEnumerable<ProductEntity> cart)
    {
        if (cart != null)
        {
            decimal orderPrice = 0;
            foreach (var product in cart)
            {
                orderPrice += product.Price;
            }
            return orderPrice;
        }
        else return 0;
    }
}

