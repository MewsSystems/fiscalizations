using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request
{
    public sealed class CounterParty : Coproduct2<LocalCounterParty, ForeignCounterParty>
    {
        public CounterParty(LocalCounterParty localCounterParty)
            : base(localCounterParty)
        {
            Check.IsNotNull(localCounterParty, nameof(localCounterParty));
        }

        public CounterParty(ForeignCounterParty foreignCounterParty)
            : base(foreignCounterParty)
        {
            Check.IsNotNull(foreignCounterParty, nameof(foreignCounterParty));
        }
    }
}
