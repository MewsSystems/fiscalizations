namespace Mews.Fiscalization.Core.Model
{
    public abstract class LimitedInt
    {
        protected LimitedInt(int value, RangeLimitation<int> limitation)
        {
            limitation.CheckValidity(value, label: "value");
            Value = value;
        }

        public int Value { get; }

        protected static bool IsValid(int value, RangeLimitation<int> limitation)
        {
            return limitation.IsValid(value);
        }
    }
}