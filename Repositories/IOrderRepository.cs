using BlinkItSOLIDPrinciples.Models;
using System.Collections.Generic;


namespace BlinkItSOLIDPrinciples.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);
        IEnumerable<Order> GetAll();
    }
}