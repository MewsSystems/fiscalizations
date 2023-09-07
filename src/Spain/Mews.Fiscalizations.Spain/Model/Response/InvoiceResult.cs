namespace Mews.Fiscalizations.Spain.Model.Response;

public sealed class InvoiceResult
{
    public InvoiceResult(
        InvoiceId id,
        InvoiceRegisterResult result,
        int? errorCode = null,
        string errorMessage = null,
        string secureVerificationCodeForOriginalInvoice = null)
    {
        Id = id;
        Result = result;
        ErrorCode = errorCode.ToOption();
        ErrorMessage = errorMessage.ToOption();
        OriginalInvoiceRequestId = secureVerificationCodeForOriginalInvoice.ToOption();
    }

    public InvoiceId Id { get; }

    public InvoiceRegisterResult Result { get; }

    public Option<int> ErrorCode { get; }

    public Option<string> ErrorMessage { get; }

    public Option<string> OriginalInvoiceRequestId { get; }
}