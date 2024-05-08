namespace Mews.Fiscalizations.Basque.Model;

public sealed class SendInvoiceRequest
{
    private SendInvoiceRequest(Subject subject, Invoice invoice, InvoiceFooter invoiceFooter)
    {
        Subject = subject;
        Invoice = invoice;
        InvoiceFooter = invoiceFooter;
    }

    public Subject Subject { get; }

    public Invoice Invoice { get; }

    public InvoiceFooter InvoiceFooter { get; }

    public static SendInvoiceRequest CreateSimplifiedInvoiceRequest(
        Issuer issuer,
        InvoiceFooter invoiceFooter,
        InvoiceData invoiceData,
        TaxSummary taxSummary,
        String1To20 number,
        DateTime issued,
        bool? issuedInSubstitutionOfSimplifiedInvoice = null,
        String1To20 series = null,
        CorrectingInvoice correctingInvoice = null,
        IEnumerable<CorrectedInvoice> correctedInvoices = null,
        IssuerType? issuerType = null)
    {
        return new SendInvoiceRequest(
            subject: new Subject(issuer, issuerType: issuerType),
            invoice: Invoice.CreateSimplified(
                invoiceData: invoiceData,
                taxSummary: taxSummary,
                number: number,
                issued: issued,
                issuedInSubstitutionOfSimplifiedInvoice: issuedInSubstitutionOfSimplifiedInvoice,
                series: series,
                correctingInvoice: correctingInvoice,
                correctedInvoices: correctedInvoices
            ),
            invoiceFooter: invoiceFooter
        );
    }

    public static SendInvoiceRequest CreateCompleteLocalReceiverInvoiceRequest(
        Issuer issuer,
        InvoiceFooter invoiceFooter,
        INonEmptyEnumerable<Receiver> receivers,
        InvoiceData invoiceData,
        TaxSummary taxSummary,
        String1To20 number,
        DateTime issued,
        String1To20 series = null,
        bool? issuedInSubstitutionOfSimplifiedInvoice = null,
        CorrectingInvoice correctingInvoice = null,
        IEnumerable<CorrectedInvoice> correctedInvoices = null,
        IssuerType? issuerType = null)
    {
        return new SendInvoiceRequest(
            subject: new Subject(issuer, receivers, issuerType),
            invoice: Invoice.CreateComplete(
                invoiceData: invoiceData,
                taxBreakdown: new TaxBreakdown(taxSummary),
                number: number,
                issued: issued,
                series: series,
                issuedInSubstitutionOfSimplifiedInvoice: issuedInSubstitutionOfSimplifiedInvoice,
                correctingInvoice: correctingInvoice,
                correctedInvoices: correctedInvoices
            ),
            invoiceFooter: invoiceFooter
        );
    }

    public static SendInvoiceRequest CreateCompleteForeignReceiverInvoiceRequest(
        Issuer issuer,
        InvoiceFooter invoiceFooter,
        INonEmptyEnumerable<Receiver> receivers,
        InvoiceData invoiceData,
        OperationTypeTaxBreakdown taxBreakdown,
        String1To20 number,
        DateTime issued,
        String1To20 series = null,
        CorrectingInvoice correctingInvoice = null,
        IEnumerable<CorrectedInvoice> correctedInvoices = null,
        IssuerType? issuerType = null)
    {
        return new SendInvoiceRequest(
            subject: new Subject(issuer, receivers, issuerType),
            invoice: Invoice.CreateComplete(
                invoiceData: invoiceData,
                taxBreakdown: new TaxBreakdown(taxBreakdown),
                number: number,
                issued: issued,
                series: series,
                correctingInvoice: correctingInvoice,
                correctedInvoices: correctedInvoices
            ),
            invoiceFooter: invoiceFooter
        );
    }
}