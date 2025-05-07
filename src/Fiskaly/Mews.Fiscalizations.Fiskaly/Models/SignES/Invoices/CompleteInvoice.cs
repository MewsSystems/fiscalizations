namespace Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices;

public sealed record CompleteInvoice(SimplifiedInvoice simplifiedInvoice, IEnumerable<Receiver> Receivers);