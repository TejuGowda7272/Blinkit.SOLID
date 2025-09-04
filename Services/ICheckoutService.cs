using BlinkItSOLIDPrinciples.Models;
using System.Collections.Generic;


namespace BlinkItSOLIDPrinciples.Services
{
    public interface ICheckoutService
    {
        void PlaceOrder(string userId, IEnumerable<CartItem> cart);
    }
}