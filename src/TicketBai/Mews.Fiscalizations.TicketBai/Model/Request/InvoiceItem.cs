namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class InvoiceItem
    {
        public InvoiceItem(String1To250 description, decimal quantity, decimal unitAmount, decimal discount, decimal totalAmount)
        {
            Description = description;
            Quantity = quantity;
            UnitAmount = unitAmount;
            Discount = discount;
            TotalAmount = totalAmount;
        }

        public String1To250 Description { get; }

        public decimal Quantity { get; }

        public decimal UnitAmount { get; }

        public decimal Discount { get; }

        public decimal TotalAmount { get; }
    }
}
