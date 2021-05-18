using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class Amount
    {
        public Amount(AmountValue net, AmountValue gross, AmountValue tax)
        {
            Net = net;
            Gross = gross;
            Tax = tax;
        }

        public AmountValue Net { get; }

        public AmountValue Gross { get; }

        public AmountValue Tax { get; }

        internal static Amount Sum(IEnumerable<Amount> amounts)
        {
            return new Amount(
                net: new AmountValue(amounts.Sum(a => a.Net.Value)),
                gross: new AmountValue(amounts.Sum(a => a.Gross.Value)),
                tax: new AmountValue(amounts.Sum(a => a.Tax.Value))
            );
        }
    }
}
