using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Basque.Model
{
    public sealed class SoftwareDeveloper : Coproduct2<LocalSoftwareDeveloper, ForeignSoftwareDeveloper>
    {
        public SoftwareDeveloper(LocalSoftwareDeveloper local)
            : base(local)
        {
            Check.IsNotNull(local, nameof(local));
        }

        public SoftwareDeveloper(ForeignSoftwareDeveloper foreign)
            : base(foreign)
        {
            Check.IsNotNull(foreign, nameof(foreign));
        }
    }
}