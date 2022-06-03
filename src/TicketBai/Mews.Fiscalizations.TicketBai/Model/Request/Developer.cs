using FuncSharp;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class Developer : Coproduct2<LocalDeveloper, ForeignDeveloper>
    {
        public Developer(LocalDeveloper local)
            : base(local)
        {
        }

        public Developer(ForeignDeveloper foreign)
            : base(foreign)
        {
        }
    }
}
