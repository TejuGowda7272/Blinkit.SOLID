using BlinkItSOLIDPrinciples.Models;
using System.Collections.Generic;


namespace BlinkItSOLIDPrinciples.Repositories
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