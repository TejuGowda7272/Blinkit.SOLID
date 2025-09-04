namespace Blinkit.SOLID.Models
{
    // SRP: simple data holder for a product
    public class Product
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}