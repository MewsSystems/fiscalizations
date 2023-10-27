namespace Mews.Fiscalizations.Basque.Model;

public sealed class SendInvoiceResponse
{
    public SendInvoiceResponse(
        string xmlRequestContent,
        string xmlResponseContent,
        string qrCodeUri,
        string tbaiIdentifier,
        DateTime received,
        InvoiceState state,
        string description,
        String1To100 signatureValue,
        IEnumerable<SendInvoiceValidationResult> validationResults = null)
    {
        XmlRequestContent = xmlRequestContent;
        XmlResponseContent = xmlResponseContent;
        QrCodeUri = qrCodeUri;
        TBAIIdentifier = tbaiIdentifier;
        Received = received;
        State = state;
        Description = description;
        SignatureValue = signatureValue;
        ValidationResults = validationResults.ToOption();
    }

    public string XmlRequestContent { get; }

    public string XmlResponseContent { get; }

    public string QrCodeUri { get; }

    public string TBAIIdentifier { get; }

    public DateTime Received { get; }

    public InvoiceState State { get; }

    public string Description { get; }

    public String1To100 SignatureValue { get; }

    public Option<IEnumerable<SendInvoiceValidationResult>> ValidationResults { get; }
}