namespace Blinkit.SOLID.Discounts
{
    public class NoDiscount : DiscountPolicy
    {
        public override decimal Apply(decimal amount) => amount;
    }
}