using System.Collections.Generic;
using System.Linq;
using BlinkItSOLIDPrinciples.Models;

namespace BlinkItSOLIDPrinciples.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();
        public void Save(Order order) => _orders.Add(order);
        public IEnumerable<Order> GetAll() => _orders.ToList();
    }
}