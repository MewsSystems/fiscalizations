namespace Mews.Fiscalizations.Verifactu.Models;

public sealed record SimplifiedInvoice(
    string InvoiceNumber,
    string InvoiceDescription,
    decimal FullAmount,
    IEnumerable<InvoiceItem> Items
);

public sealed record InvoiceItem(
    string ItemDescription,
    decimal Quantity,
    decimal UnitAmount,
    decimal FullAmount,
    ItemTaxData TaxData
);

public sealed class ItemTaxData : Coproduct2<TaxedItem, UntaxedItem>
{
    public ItemTaxData(TaxedItem taxedItem)
        : base(taxedItem)
    {
    }

    public ItemTaxData(UntaxedItem untaxedItem)
        : base(untaxedItem)
    {
    }
}

public sealed record TaxedItem(decimal TaxRate);

public sealed record UntaxedItem(TaxExemptionReason Reason);

public enum TaxExemptionReason
{
    Article20,
    Article21,
    Article22,
    Article24,
    Article25,
    OtherGrounds
}