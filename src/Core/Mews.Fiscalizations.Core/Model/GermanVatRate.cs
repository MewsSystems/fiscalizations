namespace Mews.Fiscalizations.Core.Model;

// Canonical German statutory VAT classification. The SIGN DE (Kassenbeleg) and DSFinV-K (cash point
// closing) wire formats are both derived from this, so a line is classified identically when it is
// signed and when it is reported in the closing.
public enum GermanVatRate
{
    Standard,        // 19% Regelsteuersatz
    Reduced,         // 7% ermaessigter Steuersatz
    AverageHigher,   // 10.7% Durchschnittssatz (Sec. 24 (1) no. 3 UStG)
    AverageLower,    // 5.5% Durchschnittssatz (Sec. 24, remaining cases)
    NotTaxable,      // 0% / nicht steuerbar
    TaxFree,         // umsatzsteuerfrei
    NotDeterminable, // Umsatzsteuer nicht ermittelbar
}
