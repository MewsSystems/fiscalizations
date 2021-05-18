using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class NegativeRevenue
    {
        private NegativeRevenue(NegativeAmount netValue, NonPositiveAmount vatValue, RevenueInfo info)
        {
            NetValue = netValue;
            VatValue = vatValue;
            Info = info;
        }

        public NegativeAmount NetValue { get; }

        public NonPositiveAmount VatValue { get; }

        public RevenueInfo Info { get; }

        public static ITry<NegativeRevenue, IEnumerable<Error>> Create(NegativeAmount netValue, NonPositiveAmount vatValue, RevenueInfo info)
        {
            return Try.Aggregate(
                ObjectValidations.NotNull(netValue),
                ObjectValidations.NotNull(vatValue),
                ObjectValidations.NotNull(info),
                (n, v, i) => new NegativeRevenue(n, v, i)
            );
        }
    }
}
