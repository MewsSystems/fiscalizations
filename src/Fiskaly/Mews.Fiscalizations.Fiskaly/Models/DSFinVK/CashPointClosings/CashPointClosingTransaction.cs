namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record CashPointClosingTransaction(
    string TransactionExportId,
    long Number,
    DateTimeOffset TimestampStart,
    DateTimeOffset TimestampEnd,
    bool Storno,
    ProcessType ProcessType,
    decimal FullAmountInclVat,
    IEnumerable<TransactionLine> Lines,
    IEnumerable<AmountPerVat> AmountsPerVat,
    IEnumerable<PaymentTypeAmount> PaymentTypes,
    TransactionSecurity Security,
    // Optional per spec: should match the SIGN DE tx_id when one exists, otherwise user-definable.
    Guid? TxId = null,
    string Name = null,
    TransactionUser User = null,
    TransactionBuyer Buyer = null,
    Guid? ClosingClientId = null,
    IEnumerable<TransactionReference> References = null,
    IEnumerable<string> AllocationGroups = null,
    // data.notes
    string Notes = null
);
