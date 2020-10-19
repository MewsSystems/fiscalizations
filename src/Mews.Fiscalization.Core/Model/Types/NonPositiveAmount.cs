namespace Mews.Fiscalization.Core.Model
{
    public class NonPositiveAmount : Amount
    {
        private static readonly DecimalLimitation Limitation = new DecimalLimitation(max: 0);
        public NonPositiveAmount(decimal value)
            : base(value, Limitation)
        {
        }

        public static bool IsValid(decimal value)
        {
            return Amount.IsValid(value, Limitation);
        }
    }
}