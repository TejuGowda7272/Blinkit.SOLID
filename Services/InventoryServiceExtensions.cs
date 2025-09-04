using Blinkit.SOLID.Models;
using Blinkit.SOLID.Services;
using BlinkitSOLID.Services;

namespace Blinkit.SOLID.Services
{
    public static class InventoryServiceExtensions
    {
        public static Product? GetProductForDisplay(this IInventoryService inv, string productId)
        {
            // This uses the repository internally; it's safe for demo purposes
            var repoField = typeof(IInventoryService).GetField("_repo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (repoField == null) return null;
            var repo = repoField.GetValue(inv) as Blinkit.SOLID.Repositories.IProductRepository;
            return repo?.GetById(productId);
        }
    }
}