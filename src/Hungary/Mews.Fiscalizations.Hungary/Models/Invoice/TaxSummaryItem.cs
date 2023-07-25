using FuncSharp;

namespace Mews.Fiscalizations.Hungary.Models;

public sealed class TaxSummaryItem
{
    public TaxSummaryItem(Amount amount, Amount amountHUF, decimal? taxRatePercentage = null)
    {
        Amount = amount;
        AmountHUF = amountHUF;
        TaxRatePercentage = taxRatePercentage.ToOption();
    }

    public Amount Amount { get; }

    public Amount AmountHUF { get; }

    public IOption<decimal> TaxRatePercentage { get; }
}