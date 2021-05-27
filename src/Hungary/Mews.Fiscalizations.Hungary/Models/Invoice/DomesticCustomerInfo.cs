using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Collections.Generic;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class DomesticCustomerInfo
    {
        public DomesticCustomerInfo(TaxpayerIdentificationNumber taxpayerId, Name name, SimpleAddress address)
        {
            TaxpayerId = taxpayerId;
            Name = name;
            Address = address;
        }

        public TaxpayerIdentificationNumber TaxpayerId { get; }

        public Name Name { get; }

        public SimpleAddress Address { get; }

        public static ITry<DomesticCustomerInfo, IEnumerable<Error>> Create(TaxpayerIdentificationNumber taxpayerId, Name name, SimpleAddress address)
        {
            var result = Try.Aggregate(
                ObjectValidations.NotNull(taxpayerId),
                ObjectValidations.NotNull(name),
                ObjectValidations.NotNull(address),
                (i, n, a) => IsValidTaxpayerNumber(i).ToTry(
                    t => new DomesticCustomerInfo(i, n, a),
                    f => new Error($"{nameof(TaxpayerIdentificationNumber)} must be a Hungarian taxpayer number.").ToEnumerable()
                )
            );

            return result.FlatMap(r => r);
        }

        private static bool IsValidTaxpayerNumber(TaxpayerIdentificationNumber number)
        {
            return number.Country.Alpha2Code.Equals(Countries.Hungary.Alpha2Code);
        }
    }
}
