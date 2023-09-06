namespace Mews.Fiscalizations.Hungary.Models;

public sealed class ItemAmounts
{
    public ItemAmounts(Amount amount, Amount amountHUF, decimal? taxRatePercentage = null)
    {
        Amount = Check.IsNotNull(amount, nameof(amount));
        AmountHUF = Check.IsNotNull(amountHUF, nameof(amountHUF));
        TaxRatePercentage = taxRatePercentage.ToOption();

        if (taxRatePercentage.HasValue)
        {
            Check.In(taxRatePercentage.Value, TaxationInfo.PercentageTaxRates, nameof(taxRatePercentage));
        }
    }

    public Amount Amount { get; }

    public Amount AmountHUF { get; }

    public Option<decimal> TaxRatePercentage { get; }
}