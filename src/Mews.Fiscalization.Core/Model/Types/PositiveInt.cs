namespace Mews.Fiscalization.Core.Model
{
    public sealed class PositiveInt : LimitedInt
    {
        private static readonly RangeLimitation<int> Limitation = new RangeLimitation<int>(min: 1);

        public PositiveInt(int value)
            : base(value, Limitation)
        {
        }

        public static bool IsValid(int value)
        {
            return IsValid(value, Limitation);
        }
    }
}