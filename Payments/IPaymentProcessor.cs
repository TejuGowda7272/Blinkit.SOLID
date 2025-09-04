using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkItSOLIDPrinciples.Payments
{
    // DIP: checkout depends on this abstraction, not concrete payment implementations
    public interface IPaymentProcessor
    {
        bool ProcessPayment(string userId, decimal amount);
    }
}