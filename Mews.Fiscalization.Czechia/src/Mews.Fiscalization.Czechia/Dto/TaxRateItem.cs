using System;
using Mews.Eet.Extensions;

namespace Mews.Eet.Dto
{
    public class TaxRateItem
    {
        public TaxRateItem(CurrencyValue net, CurrencyValue tax, CurrencyValue goods)
        {
            Net = net;
            Tax = tax;
            Goods = goods;

            if (Net.IsDefined() != tax.IsDefined())
            {
                throw new ArgumentException("Both tax and net should be defined or undefined.");
            }
        }

        public CurrencyValue Net { get; }

        public CurrencyValue Tax { get; }

        public CurrencyValue Goods { get; }
    }
}
