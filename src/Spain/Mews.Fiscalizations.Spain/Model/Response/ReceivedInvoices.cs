using FuncSharp;

namespace Mews.Fiscalizations.Spain.Model.Response;

public sealed class ReceivedInvoices
{
    public ReceivedInvoices(Header header, RegisterResult result, InvoiceResult[] invoices, string secureVerificationCode = null)
    {
        Header = header;
        Result = result;
        Invoices = invoices;
        SuccessfulRequestId = secureVerificationCode.ToOption();
    }

    public Header Header { get; }

    public RegisterResult Result { get; }

    public InvoiceResult[] Invoices { get; }

    public Option<string> SuccessfulRequestId { get; }
}