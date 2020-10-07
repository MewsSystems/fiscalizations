namespace Mews.Fiscalization.Core.Model
{
    public sealed class NonNegativeInt : LimitedInt
    {
        private static readonly Limitation<int> Limitation = new Limitation<int>(minimum: 0);

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