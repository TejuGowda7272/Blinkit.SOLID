using BlinkItSOLIDPrinciples.Logging;
using System;


namespace BlinkItSOLIDPrinciples.Payments
{
    // LSP/OCP: additional payment processors can be added without changing consumers
    public class CardPayment : IPaymentProcessor
    {
        private readonly ILogger _logger;
        public CardPayment(ILogger logger) { _logger = logger; }
        public bool ProcessPayment(string userId, decimal amount)
        {
            _logger.Log($"Processing card payment for {userId} amount {amount:C}");
            // Simulate success
            return true;
        }
    }
}