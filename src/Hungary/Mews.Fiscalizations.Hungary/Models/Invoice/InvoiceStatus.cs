using Mews.Fiscalizations.Core.Xml;

namespace Mews.Fiscalizations.Hungary.Models;

public sealed class InvoiceStatus
{
    public InvoiceStatus(InvoiceState status, IEnumerable<InvoiceValidationResult> validationResults, string invoiceNumber = null)
    {
        Status = status;
        ValidationResults = validationResults;
        InvoiceNumber = invoiceNumber.ToOption();
    }

    public InvoiceState Status { get; }

    public IEnumerable<InvoiceValidationResult> ValidationResults { get; }

    /// <summary>
    /// The invoice number NAV holds for this entry, available only when the status was queried with
    /// returnOriginalRequest. It is how a transaction recovered from /queryTransactionList is matched back
    /// to the invoice it carried, when the id itself was lost to a timeout.
    /// </summary>
    public Option<string> InvoiceNumber { get; }

    internal static Indexed<InvoiceStatus> Map(Dto.ProcessingResultType result)
    {
        return new Indexed<InvoiceStatus>(
            index: result.index,
            value: new InvoiceStatus(
                status: (InvoiceState)result.invoiceStatus,
                validationResults: InvoiceValidationResult.Map(result.businessValidationMessages, result.technicalValidationMessages),
                invoiceNumber: GetInvoiceNumber(result)
            )
        );
    }

    /// <summary>
    /// The original request comes back as the base64 invoice data NAV received, so the number has to be
    /// read out of it. A compressed or unreadable payload yields nothing rather than failing the status
    /// query, which has to keep working even when the original cannot be parsed.
    /// </summary>
    private static string GetInvoiceNumber(Dto.ProcessingResultType result)
    {
        if (result.originalRequest is null || result.originalRequest.Length == 0 || result.compressedContentIndicator)
        {
            return null;
        }

        try
        {
            var xml = ServiceInfo.Encoding.GetString(result.originalRequest);
            return XmlSerializer.Deserialize<Dto.InvoiceData>(xml).invoiceNumber;
        }
        catch
        {
            return null;
        }
    }
}
