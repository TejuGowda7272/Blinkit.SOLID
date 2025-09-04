using BlinkItSOLIDPrinciples.Audit;
using BlinkItSOLIDPrinciples.Logging;
using BlinkItSOLIDPrinciples.Models;
using BlinkItSOLIDPrinciples.Notifications;
using BlinkItSOLIDPrinciples.Payments;
using BlinkItSOLIDPrinciples.Repositories;
using BlinkItSOLIDPrinciples.Services;
using BlinkItSOLIDPrinciples.Discounts;
using System.Globalization;
using System;
using System.Collections.Generic;

namespace BlinkItSOLIDPrinciples
{
    class Program
    {
        static void Main()
        {
            // --- Setup (Manual Dependency Injection) ---
            var productRepo = new InMemoryProductRepository();

            // Seed warehouse products (real-world grocery items)
            productRepo.Add(new Product { Id = "p1", Name = "Amul Milk 1L", Price = 52m, Stock = 10 });
            productRepo.Add(new Product { Id = "p2", Name = "Britannia Bread", Price = 35m, Stock = 5 });
            productRepo.Add(new Product { Id = "p3", Name = "Farm Fresh Eggs (6 pack)", Price = 65m, Stock = 2 });
            productRepo.Add(new Product { Id = "p4", Name = "Fortune Sunflower Oil 1L", Price = 160m, Stock = 8 });

            var logger = new ConsoleLogger();
            var audit = new AuditService(logger);

            var inventory = new InventoryService(productRepo, audit);
            var cardPayment = new CardPayment(logger);
            var walletPayment = new WalletPayment(logger);

            var orderRepo = new InMemoryOrderRepository();
            var email = new EmailSender(logger);
            var sms = new SmsSender(logger);


            // Force Indian culture for ₹
            CultureInfo culture = new CultureInfo("en-IN");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;


            // OCP: switch discount policy easily
            DiscountPolicy discount = new FestiveDiscount(0.15m); // 15% off for festival season

            // High-level checkout (DIP in action)
            var checkout = new CheckoutService(
                inventory, cardPayment, discount,
                orderRepo, email, sms, logger, audit
            );

            // --- Simulate User 1 cart ---
            var cart1 = new List<CartItem>
            {
                new CartItem { ProductId = "p1", Quantity = 2 }, // Milk
                new CartItem { ProductId = "p2", Quantity = 1 }  // Bread
            };

            Console.WriteLine("=== User 1 placing order with Card Payment ===\n");
            checkout.PlaceOrder("user_123", cart1);

            // --- Simulate User 2 cart with Wallet Payment ---
            var checkoutWithWallet = new CheckoutService(
                inventory, walletPayment, discount,
                orderRepo, email, sms, logger, audit
            );

            var cart2 = new List<CartItem>
            {
                new CartItem { ProductId = "p3", Quantity = 1 }, // Eggs
                new CartItem { ProductId = "p4", Quantity = 1 }  // Oil
            };

            Console.WriteLine("\n=== User 2 placing order with Wallet Payment ===\n");
            checkoutWithWallet.PlaceOrder("user_456", cart2);

            // --- Audit Trail ---
            Console.WriteLine("\n=== Audit Trail ===");
            foreach (var entry in audit.Entries)
            {
                Console.WriteLine(entry);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
