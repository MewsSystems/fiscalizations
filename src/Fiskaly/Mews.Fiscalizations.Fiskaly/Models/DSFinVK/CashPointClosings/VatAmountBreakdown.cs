namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

// Per-VAT amount breakdown for item base/discount/extra amounts and sub-items.
// The spec accepts {ExclVat + Vat}, {InclVat}, or all three; send the combination that applies.
public sealed record VatAmountBreakdown(
    long VatDefinitionExportId,
    decimal? InclVat = null,
    decimal? ExclVat = null,
    decimal? Vat = null
);
