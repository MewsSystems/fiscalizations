namespace Mews.Fiscalization.Core.Model
{
    public sealed class NonEmptyString : LimitedString
    {
        private static readonly StringLimitation Limitation = new StringLimitation(allowEmptyOrWhiteSpace: false);

        public NonEmptyString(string value)
            : base(value, Limitation)
        {
        }

        public static bool IsValid(string value)
        {
            return Limitation.IsValid(value);
        }
    }
}