namespace Mews.Fiscalizations.Hungary;

public static class TaxationInfo
{
    internal static HashSet<decimal> PercentageTaxRates { get; }

    internal static CurrencyCode DefaultCurrencyCode { get; }

    static TaxationInfo()
    {
        // Spec 3.3.2 item 14. 0.2 and 0.25 are left out: NAV only takes them for a delivery before 2013, which
        // no invoice reported through this library has.
        PercentageTaxRates = new HashSet<decimal>
        {
            // Accepted only for deliveries from 2024-01-01, which Invoice.Create checks.
            0m,
            0.05m,
            0.07m,
            0.12m,
            0.18m,
            0.27m,
        };
        DefaultCurrencyCode = CurrencyCode.HungarianForint();
    }
}