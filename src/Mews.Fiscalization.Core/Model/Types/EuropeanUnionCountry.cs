namespace Mews.Fiscalization.Core.Model
{
    public sealed class EuropeanUnionCountry : Country
    {
        public EuropeanUnionCountry(string alpha2Code) 
            : base(alpha2Code)
        {
            Check.Condition(IsValid(alpha2Code), $"Invalid European Union country code.");
        }

        public new static bool IsValid(string alpha2Code)
        {
            return Country.IsValid(alpha2Code) && CountryInfo.EuropeanUnionCountryCodes.Contains(alpha2Code);
        }
    }
}
