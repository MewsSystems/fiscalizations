using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Collections.Generic;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class LocalCompany
    {
        private LocalCompany(TaxpayerIdentificationNumber taxpayerId, Name name, SimpleAddress address)
        {
            TaxpayerId = taxpayerId;
            Name = name;
            Address = address;
        }

        public TaxpayerIdentificationNumber TaxpayerId { get; }

        public Name Name { get; }

        public SimpleAddress Address { get; }

        public static ITry<LocalCompany, IEnumerable<Error>> Create(TaxpayerIdentificationNumber taxpayerId, Name name, SimpleAddress address)
        {
            var result = Try.Aggregate(
                ObjectValidations.NotNull(taxpayerId),
                ObjectValidations.NotNull(name),
                ObjectValidations.NotNull(address),
                (i, n, a) => IsLocalTaxpayerNumber(i).ToTry(
                    t => new LocalCompany(i, n, a),
                    f => new Error($"{nameof(TaxpayerIdentificationNumber)} must be a Hungarian taxpayer number.").ToEnumerable()
                )
            );

            return result.FlatMap(r => r);
        }

        private static bool IsLocalTaxpayerNumber(TaxpayerIdentificationNumber number)
        {
            return number.Country.Equals(Countries.Hungary);
        }
    }
}
