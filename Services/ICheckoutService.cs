using Blinkit.SOLID.Models;
using System.Collections.Generic;


namespace Blinkit.SOLID.Services
{
    public interface ICheckoutService
    {
        void PlaceOrder(string userId, IEnumerable<CartItem> cart);
    }
}