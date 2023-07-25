namespace Mews.Fiscalizations.Basque.Model;

public sealed class SendInvoiceValidationResult
{
    public SendInvoiceValidationResult(ErrorCode errorCode, string description, string explanation)
    {
        ErrorCode = errorCode;
        Description = description;
        Explanation = explanation;
    }

    public ErrorCode ErrorCode { get; }

    public string Description { get; }

    public string Explanation { get; }
}