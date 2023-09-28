namespace Mews.Fiscalizations.Bizkaia.Model;

public sealed class CorrectingInvoice
{
    public CorrectingInvoice(CorrectingInvoiceCode code, CorrectingInvoiceType type, CorrectingInvoiceAmount amount = null)
    {
        Code = code;
        Type = type;
        Amount = amount.ToOption();
    }

    public CorrectingInvoiceCode Code { get; }

    public CorrectingInvoiceType Type { get; }

    public Option<CorrectingInvoiceAmount> Amount { get; }
}