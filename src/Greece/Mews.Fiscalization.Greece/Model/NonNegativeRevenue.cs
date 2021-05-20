using FuncSharp;
using Mews.Fiscalization.Core.Model;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class NonNegativeRevenue
    {
        private NonNegativeRevenue(NonNegativeAmount netValue, NonNegativeAmount vatValue, RevenueInfo info)
        {
            NetValue = netValue;
            VatValue = vatValue;
            Info = info;
        }

        public NonNegativeAmount NetValue { get; }

        public NonNegativeAmount VatValue { get; }

        public RevenueInfo Info { get; }

        public static ITry<NonNegativeRevenue, IEnumerable<Error>> Create(NonNegativeAmount netValue, NonNegativeAmount vatValue, RevenueInfo info)
        {
            return Try.Aggregate(
                ObjectValidations.NotNull(netValue),
                ObjectValidations.NotNull(vatValue),
                ObjectValidations.NotNull(info),
                (n, v, i) => new NonNegativeRevenue(n, v, i)
            );
        }
    }
}
