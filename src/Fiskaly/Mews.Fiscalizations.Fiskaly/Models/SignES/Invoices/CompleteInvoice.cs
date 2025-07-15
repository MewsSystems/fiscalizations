namespace Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices;

public sealed record CompleteInvoice(SimplifiedInvoice SimplifiedInvoice, List<Receiver> Receivers);