namespace Mews.Fiscalization.Core.Model
{
    public abstract class LimitedDecimal : ValueWrapper<decimal>
    {
        protected LimitedDecimal(decimal value, DecimalLimitation limitation)
            : base(value)
        {
            limitation.CheckValidity(value);
        }

        protected static bool IsValid(decimal value, DecimalLimitation limitation)
        {
            return limitation.IsValid(value);
        }
    }
}