namespace Mews.Fiscalization.Core.Model
{
    public sealed class EuropeanCountry : Country
    {
        public EuropeanCountry(string alpha2Code) 
            : base(alpha2Code)
        {
            Check.Condition(IsValid(alpha2Code), $"Invalid European country code.");
        }

        public new static bool IsValid(string alpha2Code)
        {
            return Country.IsValid(alpha2Code) && CountryInfo.EuropeanCountryCodes.Contains(alpha2Code);
        }
    }
}
