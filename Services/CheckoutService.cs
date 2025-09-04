using Blinkit.SOLID.Discounts;
using Blinkit.SOLID.Logging;
using Blinkit.SOLID.Models;
using Blinkit.SOLID.Notifications;
using Blinkit.SOLID.Payments;
using Blinkit.SOLID.Repositories;
using Blinkit.SOLID.Services;
using Blinkit.SOLID.Audit;
using System.Linq;

namespace BlinkitSOLID.Services
{
    // High-level service: depends only on abstractions (DIP)
    public class CheckoutService : ICheckoutService
    {
        private readonly IInventoryService _inventory;
        private readonly IPaymentProcessor _payment;
        private readonly DiscountPolicy _discount;
        private readonly IOrderRepository _orderRepo;
        private readonly IEmailSender _email;
        private readonly ISmsSender _sms;
        private readonly ILogger _logger;
        private readonly IAuditService _audit;

        public CheckoutService(
            IInventoryService inventory,
            IPaymentProcessor payment,
            DiscountPolicy discount,
            IOrderRepository orderRepo,
            IEmailSender email,
            ISmsSender sms,
            ILogger logger,
            IAuditService audit)
        {
            _inventory = inventory;
            _payment = payment;
            _discount = discount;
            _orderRepo = orderRepo;
            _email = email;
            _sms = sms;
            _logger = logger;
            _audit = audit;
        }

        public void PlaceOrder(string userId, IEnumerable<CartItem> cart)
        {
            var items = cart.ToList();
            _logger.Log($"Checkout started for {userId}");

            // 1. Reserve stock
            if (!_inventory.Reserve(items))
            {
                _logger.Log("❌ Inventory not available!");
                _audit.Record("Checkout failed: inventory shortage");
                return;
            }

            // 2. Calculate price (fetch product price from repository in real app)
            decimal subtotal = items.Sum(i => i.Quantity * 50m); // simplified pricing
            var finalAmount = _discount.Apply(subtotal);

            _logger.Log($"Subtotal = {subtotal:C}, Final after discount = {finalAmount:C}");

            // 3. Process payment
            if (!_payment.ProcessPayment(userId, finalAmount))
            {
                _inventory.Release(items);
                _logger.Log("❌ Payment failed!");
                _audit.Record("Checkout failed: payment error");
                return;
            }

            // 4. Save order
            var order = new Order { UserId = userId, Items = items, Amount = finalAmount };
            _orderRepo.Save(order);
            _audit.Record($"✅ Order {order.Id} saved for {userId}");

            // 5. Send notifications
            _email.Send($"{userId}@example.com", "Order Confirmed", $"Your order {order.Id} is confirmed.");
            _sms.Send(userId, $"Order {order.Id} placed successfully. Amount = {finalAmount:C}");

            // 6. Log success
            _logger.Log($"✅ Checkout completed. OrderId: {order.Id}");
            _audit.Record($"Checkout completed for {userId}. OrderId: {order.Id}");
        }
    }
}
