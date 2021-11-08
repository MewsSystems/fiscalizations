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

        public static ITry<LocalCompany, Error> Create(TaxpayerIdentificationNumber taxpayerId, Name name, SimpleAddress address)
        {
            var localTaxPayerId = taxpayerId.ToOption().Where(i => i.Country.Equals(Countries.Hungary));
            return localTaxPayerId.ToTry(_ => new Error($"{nameof(TaxpayerIdentificationNumber)} must be a Hungarian taxpayer number.")).Map(
                i => new LocalCompany(i, name, address)
            );
        }
    }
}
