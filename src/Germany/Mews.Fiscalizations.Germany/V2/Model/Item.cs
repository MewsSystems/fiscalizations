using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Germany.V2.Model;

public sealed class Item
{
    public Item(decimal amount, GermanVatRate vatRate)
    {
        Amount = amount;
        VatRate = vatRate;
    }

    public decimal Amount { get; }

    public GermanVatRate VatRate { get; }
}
