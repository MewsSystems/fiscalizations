namespace Mews.Fiscalizations.Fiskaly.Models.SignES.Invoice;

public sealed record CompleteInvoice(SimplifiedInvoice simplifiedInvoice, IEnumerable<Receiver> Receivers);