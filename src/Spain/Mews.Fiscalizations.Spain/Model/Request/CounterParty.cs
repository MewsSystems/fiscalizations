using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public sealed class CounterParty : Coproduct2<LocalCounterParty, ForeignCounterParty>
    {
        public CounterParty(LocalCounterParty companyTitle)
            : base(companyTitle)
        {
            Check.IsNotNull(companyTitle, nameof(companyTitle));
        }

        public CounterParty(ForeignCounterParty foreignCompany)
            : base(foreignCompany)
        {
            Check.IsNotNull(foreignCompany, nameof(foreignCompany));
        }
    }
}
