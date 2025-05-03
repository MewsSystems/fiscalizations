namespace Mews.Fiscalizations.Fiskaly.Models.SignES.Invoice;

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
    TaxExemptionReason TaxExemptionReason,
    decimal? TaxRate
);

public enum TaxExemptionReason
{
    NotExempt,
    Article20,
    Article21,
    Article22,
    Article24,
    Article25,
    OtherGrounds
}