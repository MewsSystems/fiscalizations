namespace Mews.Fiscalizations.Hungary.Models;

/// <summary>
/// One NAV summaryByVatRate entry: the totals of every line sharing a VAT rate.
/// </summary>
public sealed class TaxSummaryItem
{
    public TaxSummaryItem(Amount amount, Amount amountHUF, VatRate vatRate)
    {
        Amount = amount;
        AmountHUF = amountHUF;
        VatRate = vatRate;
    }

    public Amount Amount { get; }

    public Amount AmountHUF { get; }

    public VatRate VatRate { get; }
}
