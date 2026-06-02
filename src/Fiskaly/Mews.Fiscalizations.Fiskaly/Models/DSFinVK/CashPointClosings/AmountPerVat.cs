namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record AmountPerVat(
    int VatDefinitionExportId,
    decimal GrossAmount,
    decimal NetAmount,
    decimal TaxAmount
);
