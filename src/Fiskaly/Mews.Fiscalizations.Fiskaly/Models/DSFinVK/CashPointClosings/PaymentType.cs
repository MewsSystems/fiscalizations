namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

// DSFinV-K payment types (BMF Anhang B "Zahlungsart"). The spec lists "GuthabenKarte" and
// "Guthabenkarte" as separate strings (a Fiskaly casing quirk); we expose a single VoucherCard
// and serialize the canonical "Guthabenkarte".
public enum PaymentType
{
    Cash, // Bar
    NonCash, // Unbar
    DebitCard, // ECKarte
    CreditCard, // Kreditkarte
    ElectronicPaymentServiceProvider, // ElZahlungsdienstleister
    VoucherCard, // Guthabenkarte
    None, // Keine
}
