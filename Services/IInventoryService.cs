using BlinkItSOLIDPrinciples.Models;
using System.Collections.Generic;

namespace BlinkItSOLIDPrinciples.Services
{
    public interface IInventoryService
    {
        bool Reserve(IEnumerable<CartItem> items);
        void Release(IEnumerable<CartItem> items);
    }
}
