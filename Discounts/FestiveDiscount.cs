namespace Blinkit.SOLID.Discounts
{
    public class FestiveDiscount : DiscountPolicy
    {
        private readonly decimal _pct;
        public FestiveDiscount(decimal pct) { _pct = pct; }
        public override decimal Apply(decimal amount) => amount - (amount * _pct);
    }
}