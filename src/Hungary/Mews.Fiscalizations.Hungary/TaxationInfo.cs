using System.Collections.Generic;
using Mews.Fiscalizations.Hungary.Models;

namespace Mews.Fiscalizations.Hungary;

public static class TaxationInfo
{
    internal static HashSet<decimal> PercentageTaxRates { get; }

    internal static CurrencyCode DefaultCurrencyCode { get; }

    static TaxationInfo()
    {
        PercentageTaxRates = new HashSet<decimal>
        {
            0.05m,
            0.07m,
            0.12m,
            0.18m,
            0.2m,
            0.25m,
            0.27m,
        };
        DefaultCurrencyCode = CurrencyCode.HungarianForint();
    }
}