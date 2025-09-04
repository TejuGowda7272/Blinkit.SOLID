using Blinkit.SOLID.Audit;
using Blinkit.SOLID.Discounts;
using Blinkit.SOLID.Logging;
using Blinkit.SOLID.Models;
using Blinkit.SOLID.Notifications;
using Blinkit.SOLID.Payments;
using Blinkit.SOLID.Repositories;
using Blinkit.SOLID.Services;
using BlinkitSOLID.Services;
using System;
using System.Collections.Generic;


namespace Blinkit.SOLID
{
    class Program
    {
        static void Main()
        {
            // --- Setup (Manual "DI") ---
            var productRepo = new InMemoryProductRepository();


            // Seed products
            productRepo.Add(new Product { Id = "p1", Name = "Milk 1L", Price = 50m, Stock = 10 });
            productRepo.Add(new Product { Id = "p2", Name = "Bread", Price = 30m, Stock = 5 });
            productRepo.Add(new Product { Id = "p3", Name = "Eggs (6)", Price = 60m, Stock = 2 });


            var logger = new ConsoleLogger();
            var audit = new AuditService(logger);


            var inventory = new InventoryService(productRepo, audit);
            var cardPayment = new CardPayment(logger);
            var walletPayment = new WalletPayment(logger);


            var orderRepo = new InMemoryOrderRepository();
            var email = new EmailSender(logger);
            var sms = new SmsSender(logger);


            // OCP: switch discount implementation without changing CheckoutService
            DiscountPolicy discount = new FestiveDiscount(0.10m);


            // High level checkout depends on abstractions (DIP)
            var checkout = new CheckoutService(inventory, cardPayment, discount, orderRepo, email, sms, logger, audit);


            // Simulate a user cart
            var cart = new List<CartItem>
{
new CartItem { ProductId = "p1", Quantity = 2 },
new CartItem { ProductId = "p2", Quantity = 1 }
};


            Console.WriteLine("--- Place order using Card Payment ---\n");
            checkout.PlaceOrder("user_123", cart);


            Console.WriteLine("\n--- Now place an order using Wallet Payment (demonstrating OCP/DIP swap) ---\n");
            var checkoutWithWallet = new CheckoutService(inventory, walletPayment, discount, orderRepo, email, sms, logger, audit);
            var cart2 = new List<CartItem> { new CartItem { ProductId = "p3", Quantity = 1 } };
            checkoutWithWallet.PlaceOrder("user_123", cart2);


            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}