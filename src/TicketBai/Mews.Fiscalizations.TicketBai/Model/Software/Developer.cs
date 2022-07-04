using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class Developer : Coproduct2<LocalDeveloper, ForeignDeveloper>
    {
        public Developer(LocalDeveloper local)
            : base(local)
        {
            Check.IsNotNull(local, nameof(local));
        }

        public Developer(ForeignDeveloper foreign)
            : base(foreign)
        {
            Check.IsNotNull(foreign, nameof(foreign));
        }
    }
}