namespace Mews.Fiscalization.Core.Model
{
    public class TaxpayerIdentificationNumber : NonEmptyString
    {
        public TaxpayerIdentificationNumber(Country country, string taxpayerNumber)
            : base(taxpayerNumber)
        {
            Check.Condition(IsValid(country, taxpayerNumber), "Invalid taxpayer identification number.");
        }

        public static bool IsValid(Country country, string taxpayerNumber)
        {
            Check.IsNotNull(country, $"{nameof(country)} cannot be null.");
            if (CountryInfo.EuropeanCountryCodes.Contains(country.Value))
            {
                return EuropeanTaxpayerIdentificationNumber.IsValid(country, taxpayerNumber);
            }

            return IsValid(taxpayerNumber);
        }
    }
}
