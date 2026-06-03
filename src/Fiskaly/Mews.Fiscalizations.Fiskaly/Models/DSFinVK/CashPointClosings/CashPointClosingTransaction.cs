namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record CashPointClosingTransaction(
    Guid TxId,
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
    Guid? ClosingClientId = null,
    IEnumerable<TransactionReference> References = null,
    IEnumerable<string> AllocationGroups = null
);
