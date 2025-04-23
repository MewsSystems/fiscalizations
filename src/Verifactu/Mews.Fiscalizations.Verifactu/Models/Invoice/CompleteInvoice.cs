namespace Mews.Fiscalizations.Verifactu.Models;

public sealed record CompleteInvoice(SimplifiedInvoice simplifiedInvoice, IEnumerable<Receiver> Receivers);