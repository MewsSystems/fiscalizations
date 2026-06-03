namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

// vat_definition_export_id is an integer up to 9_999_999_999 per spec, so it must be a long.
public sealed record AmountPerVat(
    long VatDefinitionExportId,
    decimal GrossAmount,
    decimal NetAmount,
    decimal TaxAmount
);
