namespace Mews.Fiscalization.Core.Model
{
    public abstract class LimitedDecimal
    {
        protected LimitedDecimal(decimal value, DecimalLimitation limitation)
        {
            limitation.CheckValidity(value, label: "value");
            Value = value;
        }

        public decimal Value { get; }

        protected static bool IsValid(decimal value, DecimalLimitation limitation)
        {
            return limitation.IsValid(value);
        }
    }
}