using System;
using System.Collections.Generic;


namespace Blinkit.SOLID.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = string.Empty;
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}