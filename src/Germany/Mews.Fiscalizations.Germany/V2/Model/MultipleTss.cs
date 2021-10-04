using System.Collections.Generic;

namespace Mews.Fiscalizations.Germany.V2.Model
{
    public sealed class MultipleTss
    {
        public MultipleTss(IEnumerable<Tss> tssList)
        {
            TssList = tssList;
        }

        public IEnumerable<Tss> TssList { get; }
    }
}