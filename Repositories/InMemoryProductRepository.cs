using System.Collections.Generic;
using System.Linq;
using BlinkItSOLIDPrinciples.Models;


namespace BlinkItSOLIDPrinciples.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly Dictionary<string, Product> _store = new Dictionary<string, Product>();


        public void Add(Product product) => _store[product.Id] = product;


        public Product? GetById(string id) => _store.TryGetValue(id, out var p) ? Clone(p) : null;


        public IEnumerable<Product> GetAll() => _store.Values.Select(Clone).ToList();


        public void Update(Product product)
        {
            if (_store.ContainsKey(product.Id)) _store[product.Id] = product;
        }


        // Return a copy to avoid callers mutating repository objects directly
        private Product Clone(Product p) => new Product { Id = p.Id, Name = p.Name, Price = p.Price, Stock = p.Stock };
    }
}