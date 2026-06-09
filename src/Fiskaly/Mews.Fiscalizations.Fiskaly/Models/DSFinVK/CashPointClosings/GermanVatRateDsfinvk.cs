using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

// Maps the canonical German VAT classification onto the DSFinV-K vat_definition_export_id. The spec
// fixes ids 1-7 for the predefined rates; the wire field stays numeric (long) so these are the only
// values this mapping ever produces.
public static class GermanVatRateDsfinvk
{
    public static long ToVatDefinitionExportId(this GermanVatRate rate)
    {
        return rate switch
        {
            GermanVatRate.Standard => 1, // 19% Regelsteuersatz
            GermanVatRate.Reduced => 2, // 7% ermaessigter Steuersatz
            GermanVatRate.AverageHigher => 3, // 10.7% Durchschnittssatz (Sec. 24 (1) no. 3)
            GermanVatRate.AverageLower => 4, // 5.5% Durchschnittssatz (Sec. 24, remaining)
            GermanVatRate.NotTaxable => 5, // 0% / nicht steuerbar
            GermanVatRate.TaxFree => 6, // umsatzsteuerfrei
            GermanVatRate.NotDeterminable => 7, // Umsatzsteuer nicht ermittelbar
            _ => throw new ArgumentOutOfRangeException(nameof(rate), rate, "Unknown German VAT rate."),
        };
    }
}
