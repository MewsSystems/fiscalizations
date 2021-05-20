namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class TaxSummaryItem
    {
        public TaxSummaryItem(decimal? taxRatePercentage, Amount amount, Amount amountHUF)
        {
            TaxRatePercentage = taxRatePercentage;
            Amount = amount;
            AmountHUF = amountHUF;
        }

        public decimal? TaxRatePercentage { get; }

        public Amount Amount { get; }

        public Amount AmountHUF { get; }
    }
}
