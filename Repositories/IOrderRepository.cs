using Blinkit.SOLID.Models;
using System.Collections.Generic;


namespace Blinkit.SOLID.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);
        IEnumerable<Order> GetAll();
    }
}