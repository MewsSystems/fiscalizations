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
    VatTypeEnum VatType,
    BillingSystemTypeEnum BillingSystemType
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

public enum BillingSystemTypeEnum
{
    REGULAR,
    SIMPLIFIED_REGIME,
    EQUIVALENCE_SURCHARGE,
    EXPORT,
    AGRICULTURE,
    ANTIQUES,
    TRAVEL_AGENCIES,
    TRAVEL_AGENCY_MEDIATORS,
    OTHER_TAX_IVA,
    OTHER_TAX_IGIC,
    OTHER_TAX_IPSI
}