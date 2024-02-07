using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero_WebApp.Tests.Enhettester;

public class Enhettest1
{
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
