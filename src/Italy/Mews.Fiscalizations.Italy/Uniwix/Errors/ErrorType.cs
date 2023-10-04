namespace Mews.Fiscalizations.Italy.Uniwix.Errors;

public enum ErrorType
{
    Connection,
    Unauthorized,
    Validation,
    Unknown,
    InvoiceNotFound,
    FileExistsInQueue,
    InsufficientCredit,
    InvoiceStatusNotFound,
    FileNotAvailable
}