using Blinkit.SOLID.Models;
using System.Collections.Generic;

namespace Blinkit.SOLID.Services
{
    public interface IInventoryService
    {
        bool Reserve(IEnumerable<CartItem> items);
        void Release(IEnumerable<CartItem> items);
    }
}
