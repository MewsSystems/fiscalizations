namespace Mews.Fiscalization.Core.Model
{
    public sealed class PositiveInt : LimitedInt
    {
        private static readonly Limitation<int> Limitation = new Limitation<int>(minimum: 1);

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