namespace Mews.Fiscalization.Core.Model
{
    public class NonNegativeInt : LimitedInt
    {
        private static readonly RangeLimitation<int> Limitation = new RangeLimitation<int>(min: 0);

        public NonNegativeInt(int value)
            : base(value, Limitation)
        {
        }

        public static bool IsValid(int value)
        {
            return IsValid(value, Limitation);
        }
    }
}