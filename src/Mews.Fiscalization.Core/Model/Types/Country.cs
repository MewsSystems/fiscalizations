namespace Mews.Fiscalization.Core.Model
{
    public class Country : NonEmptyString
    {
        private static readonly StringLimitation Limitation = new StringLimitation(allowedValues: CountryInfo.AllCountryCodes);

        public Country(string alpha2Code)
            : base(alpha2Code, Limitation)
        {
        }

        public new static bool IsValid(string alpha2Code)
        {
            return IsValid(alpha2Code, Limitation);
        }
    }
}
