using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public sealed class CounterPartyCompany : Coproduct2<LocalCompany, ForeignCompany>
    {
        public CounterPartyCompany(LocalCompany companyTitle)
            : base(companyTitle)
        {
            Check.IsNotNull(companyTitle, nameof(companyTitle));
        }

        public CounterPartyCompany(ForeignCompany foreignCompany)
            : base(foreignCompany)
        {
            Check.IsNotNull(foreignCompany, nameof(foreignCompany));
        }
    }
}
