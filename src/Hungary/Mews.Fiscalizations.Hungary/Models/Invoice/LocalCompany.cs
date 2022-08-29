using FuncSharp;
using Mews.Fiscalizations.Core.Model;

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

        public static ITry<LocalCompany, Error> Create(string taxpayerId, Name name, SimpleAddress address)
        {
            return TaxpayerIdentificationNumber.Create(Countries.Hungary, taxpayerId, isCountryCodePrefixAllowed: false).Map(n => new LocalCompany(n, name, address));
        }
    }
}
