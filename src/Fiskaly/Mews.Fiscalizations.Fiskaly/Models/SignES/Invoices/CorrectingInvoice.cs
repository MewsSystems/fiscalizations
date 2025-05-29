namespace Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices;

public record CorrectingSimplifiedInvoice(Guid InvoiceId, SimplifiedInvoice Invoice);

public record CorrectingCompleteInvoice(Guid InvoiceId, CompleteInvoice Invoice);