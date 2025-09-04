using Blinkit.SOLID.Logging;


namespace Blinkit.SOLID.Payments
{
    public class WalletPayment : IPaymentProcessor
    {
        private readonly ILogger _logger;
        public WalletPayment(ILogger logger) { _logger = logger; }
        public bool ProcessPayment(string userId, decimal amount)
        {
            _logger.Log($"Processing wallet payment for {userId} amount {amount:C}");
            // Simulate insufficient balance for demo when amount > 100
            if (amount > 100m)
            {
                _logger.Log("Wallet payment failed: insufficient funds");
                return false;
            }
            return true;
        }
    }
}