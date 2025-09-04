using System;
using System.Collections.Generic;

namespace BlinkItSOLIDPrinciples.Models
{
    public class Order
    {
        private static int _counter = 1; // starting point for order numbers

        public string Id { get; private set; }
        public string UserId { get; set; }
        public IEnumerable<CartItem> Items { get; set; }
        public decimal Amount { get; set; }

        public Order()
        {
            string datePrefix = DateTime.Now.ToString("ddMMyyyy"); // e.g. 04092025
            Id = $"{datePrefix}-ORD{_counter++}";
        }
    }
}
