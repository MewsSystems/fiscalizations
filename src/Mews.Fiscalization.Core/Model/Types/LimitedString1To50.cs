namespace Mews.Fiscalization.Core.Model
{
    public sealed class LimitedString1To50 : LimitedString
    {
        private static readonly Limitation<int> Limitation = Limitation<int>.Create(minimum: 1, maximum: 50);

        public LimitedString1To50(string value)
            : base(value, Limitation)
        {
        }

        public static bool IsValid(string value)
        {
            return IsValid(value, Limitation);
        }
    }
}