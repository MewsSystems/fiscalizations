using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public sealed class ForeignCounterParty : Coproduct2<ForeignCustomer, ForeignCompany>
    {
        public ForeignCounterParty(ForeignCustomer customer)
            : base(customer)
        {
            Check.IsNotNull(customer, nameof(customer));
        }

        public ForeignCounterParty(ForeignCompany company)
            : base(company)
        {
            Check.IsNotNull(company, nameof(company));
        }

        public string Name => Match(
            customer => customer.Name.Value,
            company => company.Name.Value
        );

        public string Id => Match(
            customer => customer.IdNumber.Value,
            company => company.TaxpayerNumber.Value
        );

        public ResidenceCountryIdentificatorType IdentificatorType => Match(
            customer => customer.IdentificatorType,
            company => company.IdentificatorType
        );

        public Country Country => Match(
            customer => customer.Country,
            company => company.TaxpayerNumber.Country
        );
    }
}
