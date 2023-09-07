namespace Mews.Fiscalizations.Basque.Model;

public sealed class InvoiceItem
{
    public InvoiceItem(String1To250 description, decimal quantity, decimal unitAmount, decimal totalAmount, decimal? discount = null)
    {
        Description = Check.IsNotNull(description, nameof(description));
        Quantity = Check.IsNotNull(quantity, nameof(quantity));
        UnitAmount = Check.IsNotNull(unitAmount, nameof(unitAmount));
        TotalAmount = Check.IsNotNull(totalAmount, nameof(totalAmount));
        Discount = discount.ToOption();
    }

    public String1To250 Description { get; }

    public decimal Quantity { get; }

    public decimal UnitAmount { get; }

    public decimal TotalAmount { get; }

    public Option<decimal> Discount { get; }
}