namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

// DSFinV-K predefined vat_definition_export_id values. The spec fixes ids 1-7, reserves 8-999,
// and leaves 1000-99999999 for free use, so the wire field stays numeric (long); this enum only
// names the predefined values so consumers don't hard-code the magic numbers.
public enum DsfinvkVatDefinition
{
    GeneralRate = 1, // 19% Regelsteuersatz
    ReducedRate = 2, // 7% ermaessigter Steuersatz
    AverageRateForestrySpecial = 3, // 10.7% Durchschnittssatz (Sec. 24 (1) no. 3 UStG)
    AverageRateForestryOther = 4, // 5.5% Durchschnittssatz (Sec. 24, remaining cases)
    NotTaxable = 5, // 0% / nicht steuerbar
    TaxFree = 6, // umsatzsteuerfrei
    NotDeterminable = 7, // Umsatzsteuer nicht ermittelbar
}
