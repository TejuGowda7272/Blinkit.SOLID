using Blinkit.SOLID.Audit;
using Blinkit.SOLID.Models;
using Blinkit.SOLID.Repositories;
using Blinkit.SOLID.Audit;
using System.Collections.Generic;
using System.Linq;

namespace Blinkit.SOLID.Services
{
    // SRP: manages stock only; depends on repo + audit (DIP)
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _repo;
        private readonly IAuditService _audit;

        public InventoryService(IProductRepository repo, IAuditService audit)
        {
            _repo = repo;
            _audit = audit;
        }

        public bool Reserve(IEnumerable<CartItem> items)
        {
            var list = items.ToList();

            // 1) check availability
            foreach (var it in list)
            {
                var product = _repo.GetById(it.ProductId);
                if (product == null || product.Stock < it.Quantity)
                {
                    _audit.Record($"Inventory reservation failed for product {it.ProductId}");
                    return false;
                }
            }

            // 2) deduct stock
            foreach (var it in list)
            {
                var product = _repo.GetById(it.ProductId)!; // safe: checked above
                product.Stock -= it.Quantity;
                _repo.Update(product);
                _audit.Record($"Reserved {it.Quantity} of {product.Id}. New stock: {product.Stock}");
            }

            return true;
        }

        public void Release(IEnumerable<CartItem> items)
        {
            foreach (var it in items)
            {
                var product = _repo.GetById(it.ProductId);
                if (product != null)
                {
                    product.Stock += it.Quantity;
                    _repo.Update(product);
                    _audit.Record($"Released {it.Quantity} of {product.Id}. New stock: {product.Stock}");
                }
            }
        }

        // optional helper if you need to read product info later
        public Product? GetProduct(string productId) => _repo.GetById(productId);
    }
}
