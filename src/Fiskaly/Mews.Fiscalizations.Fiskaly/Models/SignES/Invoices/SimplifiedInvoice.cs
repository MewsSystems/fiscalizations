namespace Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices;

public sealed record SimplifiedInvoice(
    string InvoiceNumber,
    string InvoiceDescription,
    decimal FullAmount,
    IEnumerable<InvoiceItem> Items,
    DateTime IssuedAt,
    string Series = null
);

public sealed record InvoiceItem(
    string ItemDescription,
    decimal Quantity,
    decimal UnitAmount,
    decimal FullAmount,
    TaxExemptionReason TaxExemptionReason,
    decimal? TaxRate,
    VatTypeEnum VatType
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

public enum VatTypeEnum
{
    IVA,
    IPSI,
    IGIC,
    OTHER
}