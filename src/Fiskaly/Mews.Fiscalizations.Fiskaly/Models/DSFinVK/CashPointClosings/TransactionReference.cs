namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public enum ReferenceType
{
    InternalTransaction,   // InterneTransaktion
    Transaction,           // Transaktion
    ExternalBill,          // ExterneRechnung
    ExternalDeliveryNote,  // ExternerLieferschein
    ExternalOther          // ExterneSonstige
}

// Polymorphic DSFinV-K reference; only the fields of the chosen Type are sent.
// Required per type: InternalTransaction -> TxId; Transaction -> CashPointClosingExportId,
// CashRegisterExportId, TransactionExportId; ExternalBill/ExternalDeliveryNote -> ExternalExportId;
// ExternalOther -> ExternalOtherExportId, Name. Date is optional everywhere.
public sealed record TransactionReference(
    ReferenceType Type,
    Guid? TxId = null,
    long? CashPointClosingExportId = null,
    string CashRegisterExportId = null,
    string TransactionExportId = null,
    string ExternalExportId = null,
    string ExternalOtherExportId = null,
    string Name = null,
    DateTimeOffset? Date = null
);
