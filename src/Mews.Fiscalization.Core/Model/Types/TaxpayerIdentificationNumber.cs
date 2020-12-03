using System;
using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public class TaxpayerIdentificationNumber : LimitedString
    {
        private static readonly StringLimitation Limitation = new StringLimitation(allowEmptyOrWhiteSpace: false);

        public TaxpayerIdentificationNumber(Country country, string taxpayerNumber)
            : base(taxpayerNumber, Limitation)
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

            return IsValid(taxpayerNumber, Limitation.ToEnumerable());
        }

        [Obsolete("This will be removed later.")]
        public static bool IsValid(string taxpayerNumber)
        {
            throw new Exception("Use IsValid that takes country and tapayerNumber as a parameter.");
        }

        public new static bool IsValid(string taxpayerNumber, IEnumerable<StringLimitation> limitations)
        {
            return LimitedString.IsValid(taxpayerNumber, Limitation.Concat(limitations));
        }
    }
}
