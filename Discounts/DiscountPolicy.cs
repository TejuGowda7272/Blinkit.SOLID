namespace Blinkit.SOLID.Discounts
{
    // OCP/LSP: base class for different discount strategies
    public abstract class DiscountPolicy
    {
        public abstract decimal Apply(decimal amount);
    }
}