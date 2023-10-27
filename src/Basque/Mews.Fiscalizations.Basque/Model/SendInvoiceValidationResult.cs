namespace Mews.Fiscalizations.Basque.Model;

public sealed class SendInvoiceValidationResult
{
    public SendInvoiceValidationResult(ErrorCode errorCode, string description)
    {
        ErrorCode = errorCode;
        Description = description;
    }

    public ErrorCode ErrorCode { get; }

    public string Description { get; }
}