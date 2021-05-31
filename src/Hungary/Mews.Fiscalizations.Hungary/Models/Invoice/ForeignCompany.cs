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
            var optionalForeignTaxPayerId = taxpayerId.ToOption().ToOption().Where(i => i.Match(
                identifier => identifier.Country.Equals(Countries.Hungary),
                _ => true
            ));
            return optionalForeignTaxPayerId.ToTry(_ => Error.Create($"{nameof(TaxpayerIdentificationNumber)} must be a foreign (non-Hungarian) taxpayer number.")).Map(
                i => new ForeignCompany(name, address, i.GetOrNull())
            );
        }
    }
}
