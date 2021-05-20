using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Spain.Model.Request
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
