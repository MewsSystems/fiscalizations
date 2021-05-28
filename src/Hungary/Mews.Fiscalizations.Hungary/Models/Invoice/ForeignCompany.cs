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
            var validatedTaxpayerId = taxpayerId.ToOption().Where(i => !i.Country.Equals(Countries.Hungary)).ToTry(
                _ => Error.Create($"{nameof(TaxpayerIdentificationNumber)} must be a foreign (non-Hungarian) taxpayer number.")
            );
            return Try.Aggregate(
                validatedTaxpayerId,
                ObjectValidations.NotNull(name),
                ObjectValidations.NotNull(address),
                (i, n, a) => new ForeignCompany(n, a, i)
            );
        }
    }
}
