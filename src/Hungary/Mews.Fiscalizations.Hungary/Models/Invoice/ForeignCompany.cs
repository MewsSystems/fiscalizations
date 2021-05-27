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
            return Try.Aggregate(
                ObjectValidations.NotNull(name),
                ObjectValidations.NotNull(address),
                (n, a) => new ForeignCompany(n, a, taxpayerId)
            );
        }
    }
}
