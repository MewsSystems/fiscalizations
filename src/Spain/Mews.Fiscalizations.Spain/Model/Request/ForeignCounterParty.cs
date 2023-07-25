using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request;

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
}