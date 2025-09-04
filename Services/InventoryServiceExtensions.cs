using BlinkItSOLIDPrinciples.Services;
using BlinkItSOLIDPrinciples.Models;
using BlinkItSOLIDPrinciples.Repositories;

namespace BlinkItSOLIDPrinciples.Services
{
    public static class InventoryServiceExtensions
    {
        public static Product? GetProductForDisplay(this IInventoryService inv, string productId)
        {
            // This uses the repository internally; it's safe for demo purposes
            var repoField = typeof(IInventoryService).GetField("_repo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (repoField == null) return null;
            var repo = repoField.GetValue(inv) as IProductRepository;
            return repo?.GetById(productId);
        }
    }
}