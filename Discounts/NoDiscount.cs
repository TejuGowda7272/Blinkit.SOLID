namespace BlinkItSOLIDPrinciples.Discounts
{
    public class NoDiscount : DiscountPolicy
    {
        public override decimal Apply(decimal amount) => amount;
    }
}