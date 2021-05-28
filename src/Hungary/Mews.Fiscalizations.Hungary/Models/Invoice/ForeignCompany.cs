using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using System.Collections.Generic;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class ForeignCompany
    {
        private ForeignCompany(Name name, SimpleAddress address, TaxpayerIdentificationNumber taxpayerId = null)
        {
            Name = name;
            Address = address;
            TaxpayerId = taxpayerId.ToOption();
        }

        public Name Name { get; }

        public SimpleAddress Address { get; }

        public IOption<TaxpayerIdentificationNumber> TaxpayerId { get; }

        public static ITry<ForeignCompany, IEnumerable<Error>> Create(Name name, SimpleAddress address, TaxpayerIdentificationNumber taxpayerId = null)
        {
            var result = Try.Aggregate(
                ObjectValidations.NotNull(name),
                ObjectValidations.NotNull(address),
                (n, a) => taxpayerId.ToOption().Match(
                    i => IsForeignTaxpayerNumber(i).ToTry(
                        t => new ForeignCompany(n, a, i),
                        f => new Error($"{nameof(TaxpayerIdentificationNumber)} must be a foreign (non-Hungarian) taxpayer number.").ToEnumerable()
                    ),
                    _ => Try.Success<ForeignCompany, IEnumerable<Error>>(new ForeignCompany(n, a))
                )
            );

            return result.FlatMap(r => r);
        }

        private static bool IsForeignTaxpayerNumber(TaxpayerIdentificationNumber number)
        {
            return !number.Country.Alpha2Code.Equals(Countries.Hungary.Alpha2Code);
        }
    }
}
