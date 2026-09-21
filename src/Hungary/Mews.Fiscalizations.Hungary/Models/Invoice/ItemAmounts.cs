namespace Mews.Fiscalizations.Hungary.Models;

public sealed class ItemAmounts
{
    public ItemAmounts(Amount amount, Amount amountHUF, VatRate vatRate)
    {
        Amount = Check.IsNotNull(amount, nameof(amount));
        AmountHUF = Check.IsNotNull(amountHUF, nameof(amountHUF));
        VatRate = Check.IsNotNull(vatRate, nameof(vatRate));
    }

    /// <summary>The amount in the currency of the invoice.</summary>
    public Amount Amount { get; }

    /// <summary>The same amount in HUF, which NAV reconciles against <see cref="Amount"/> and the rate.</summary>
    public Amount AmountHUF { get; }

    public VatRate VatRate { get; }
}
