namespace Mews.Fiscalizations.Basque.Model;

public sealed class InvoiceData
{
    public InvoiceData(
        String1To250 description,
        INonEmptyEnumerable<InvoiceItem> items,
        decimal totalAmount,
        INonEmptyEnumerable<TaxMode> taxModes,
        decimal? supportWithheldAmount = null,
        decimal? tax = null,
        DateTime? transactionDate = null)
    {
        Description = description;
        Items = items;
        TotalAmount = totalAmount;
        TaxModes = taxModes;
        SupportWithheldAmount = supportWithheldAmount.ToOption();
        Tax = tax.ToOption();
        TransactionDate = transactionDate.ToOption();
    }

    public String1To250 Description { get; }

    public INonEmptyEnumerable<InvoiceItem> Items { get; }

    public decimal TotalAmount { get; }

    public INonEmptyEnumerable<TaxMode> TaxModes { get; }

    public Option<decimal> SupportWithheldAmount { get; }

    public Option<decimal> Tax { get; }

    public Option<DateTime> TransactionDate { get; }
}