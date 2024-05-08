namespace Mews.Fiscalizations.Basque.Model;

public sealed class Invoice
{
    private Invoice(InvoiceHeader header, InvoiceData invoiceData, TaxBreakdown taxBreakdown)
    {
        Header = header;
        InvoiceData = invoiceData;
        TaxBreakdown = taxBreakdown;
    }

    public InvoiceHeader Header { get; }

    public InvoiceData InvoiceData { get; }

    public TaxBreakdown TaxBreakdown { get; }

    public static Invoice CreateSimplified(
        InvoiceData invoiceData,
        TaxSummary taxSummary,
        String1To20 number,
        DateTime issued,
        bool? issuedInSubstitutionOfSimplifiedInvoice = null,
        String1To20 series = null,
        CorrectingInvoice correctingInvoice = null,
        IEnumerable<CorrectedInvoice> correctedInvoices = null)
    {
        return new Invoice(
            header: new InvoiceHeader(
                number: number,
                issued: issued,
                isSimplified: true,
                issuedInSubstitutionOfSimplifiedInvoice: issuedInSubstitutionOfSimplifiedInvoice,
                series: series,
                correctingInvoice: correctingInvoice,
                correctedInvoices: correctedInvoices
            ),
            invoiceData: invoiceData,
            taxBreakdown: new TaxBreakdown(taxSummary)
        );
    }

    public static Invoice CreateComplete(
        InvoiceData invoiceData,
        TaxBreakdown taxBreakdown,
        String1To20 number,
        DateTime issued,
        String1To20 series = null,
        bool? issuedInSubstitutionOfSimplifiedInvoice = null,
        CorrectingInvoice correctingInvoice = null,
        IEnumerable<CorrectedInvoice> correctedInvoices = null)
    {
        return new Invoice(
            header: new InvoiceHeader(
                number: number,
                issued: issued,
                isSimplified: false,
                series: series,
                issuedInSubstitutionOfSimplifiedInvoice: issuedInSubstitutionOfSimplifiedInvoice,
                correctingInvoice: correctingInvoice,
                correctedInvoices: correctedInvoices
            ),
            invoiceData: invoiceData,
            taxBreakdown: taxBreakdown
        );
    }
}