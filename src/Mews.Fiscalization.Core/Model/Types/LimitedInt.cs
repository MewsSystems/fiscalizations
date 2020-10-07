namespace Mews.Fiscalization.Core.Model
{
    public abstract class LimitedInt : ValueWrapper<int>
    {
        protected LimitedInt(int value, RangeLimitation<int> limitation)
            : base(value)
        {
            limitation.CheckValidity(value, label: "value");
        }

        protected static bool IsValid(int value, RangeLimitation<int> limitation)
        {
            return limitation.IsValid(value);
        }
    }
}