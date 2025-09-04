using Blinkit.SOLID.Models;
using System.Collections.Generic;


namespace Blinkit.SOLID.Repositories
{
    // ISP/OCP: small repo interface — can be extended with new implementations
    public interface IProductRepository
    {
        Product? GetById(string id);
        void Update(Product product);
        IEnumerable<Product> GetAll();
        void Add(Product product);
    }
}