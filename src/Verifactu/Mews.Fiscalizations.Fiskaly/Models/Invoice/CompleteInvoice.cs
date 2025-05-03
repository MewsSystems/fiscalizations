namespace Mews.Fiscalizations.Fiskaly.Models;

public sealed record CompleteInvoice(SimplifiedInvoice simplifiedInvoice, IEnumerable<Receiver> Receivers);