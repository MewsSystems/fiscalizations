using FuncSharp;

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

    public IOption<int> ErrorCode { get; }

    public IOption<string> ErrorMessage { get; }

    public IOption<string> OriginalInvoiceRequestId { get; }
}