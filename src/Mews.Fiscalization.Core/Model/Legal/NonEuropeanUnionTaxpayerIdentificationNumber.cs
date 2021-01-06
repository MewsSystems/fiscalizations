using FuncSharp;
using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public class NonEuropeanUnionTaxpayerIdentificationNumber
    {
        private NonEuropeanUnionTaxpayerIdentificationNumber(NonEuropeanUnionCountry country, string taxpayerNumber)
        {
            Country = country;
            TaxpayerNumber = taxpayerNumber;
        }

        public NonEuropeanUnionCountry Country { get; }

        public string TaxpayerNumber { get; }

        public static ITry<NonEuropeanUnionTaxpayerIdentificationNumber, IEnumerable<Error>> Create(NonEuropeanUnionCountry country, string taxpayerNumber)
        {
            return ObjectValidations.NotNull(country).FlatMap(c =>
            {
                var nonEmptyNumber = StringValidations.NonEmpty(taxpayerNumber);
                return nonEmptyNumber.Map(n => new NonEuropeanUnionTaxpayerIdentificationNumber(c, n));
            });
        }
    }
}
