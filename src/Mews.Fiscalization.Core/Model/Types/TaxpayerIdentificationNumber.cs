namespace Mews.Fiscalization.Core.Model
{
    public class TaxpayerIdentificationNumber : NonEmptyString
    {
        public TaxpayerIdentificationNumber(Country country, string taxpayerNumber)
            : base(taxpayerNumber)
        {
            Check.IsNotNull(country, nameof(country));
            Check.Condition(IsValid(country, taxpayerNumber), "Invalid taxpayer identification number.");
        }

        public static bool IsValid(Country country, string taxpayerNumber)
        {
            if (country.IsNull())
            {
                return false;
            }
            if (country is EuropeanUnionCountry europeanCountry)
            {
                return EuropeanUnionTaxpayerIdentificationNumber.IsValid(europeanCountry, taxpayerNumber);
            }

            return IsValid(taxpayerNumber);
        }
    }
}
