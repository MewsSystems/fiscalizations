using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class Receiver : Coproduct2<LocalReceiver, ForeignReceiver>
    {
        public Receiver(LocalReceiver localReceiver)
            : base(localReceiver)
        {
            Check.IsNotNull(localReceiver, nameof(localReceiver));
        }

        public Receiver(ForeignReceiver foreignReceiver)
            : base(foreignReceiver)
        {
            Check.IsNotNull(foreignReceiver, nameof(foreignReceiver));
        }
    }
}
