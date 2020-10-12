namespace Mews.Fiscalization.Core.Model
{
    public class LimitedString1To50 : LimitedString
    {
        private static readonly StringLimitation Limitation = new StringLimitation(minLength: 1, maxLength: 50);

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