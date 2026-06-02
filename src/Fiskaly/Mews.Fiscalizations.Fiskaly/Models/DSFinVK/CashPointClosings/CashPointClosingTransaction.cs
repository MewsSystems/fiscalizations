namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;

public sealed record CashPointClosingTransaction(
    Guid TxId,
    string TransactionExportId,
    long Number,
    DateTimeOffset TimestampStart,
    DateTimeOffset TimestampEnd,
    bool Storno,
    ProcessType ProcessType,
    Guid ClosingClientId,
    decimal FullAmountInclVat,
    IEnumerable<TransactionLine> Lines,
    IEnumerable<AmountPerVat> AmountsPerVat,
    IEnumerable<PaymentTypeAmount> PaymentTypes,
    TransactionSecurity Security,
    IEnumerable<TransactionReference> References = null
);
