namespace Mews.Fiscalizations.Hungary.Models;

/// <summary>
/// NAV advancePaymentData - the advance invoice that a final invoice settles (spec 2.9).
/// </summary>
public sealed class AdvancePaymentData
{
    public AdvancePaymentData(InvoiceNumber originalInvoiceNumber, DateTime paymentDate, ExchangeRate exchangeRate)
    {
        OriginalInvoiceNumber = Check.IsNotNull(originalInvoiceNumber, nameof(originalInvoiceNumber));
        PaymentDate = paymentDate;
        ExchangeRate = Check.IsNotNull(exchangeRate, nameof(exchangeRate));
    }

    public InvoiceNumber OriginalInvoiceNumber { get; }

    public DateTime PaymentDate { get; }

    public ExchangeRate ExchangeRate { get; }
}
