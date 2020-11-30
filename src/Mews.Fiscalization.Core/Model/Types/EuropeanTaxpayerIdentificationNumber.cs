using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Core.Model
{
    public sealed class EuropeanTaxpayerIdentificationNumber : TaxpayerIdentificationNumber
    {
        public EuropeanTaxpayerIdentificationNumber(Country country, string taxpayerNumber)
            : base(country, taxpayerNumber)
        {
        }

        public new static bool IsValid(Country country, string taxpayerNumber)
        {
            return Regex.IsMatch(taxpayerNumber, CountryInfo.EuropeanTaxpayerNumberPatterns[country.Value]);
        }
    }
}
