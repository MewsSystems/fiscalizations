using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class InvoiceItem
    {
        public InvoiceItem(String1To250 description, decimal quantity, decimal unitAmount, decimal discount, decimal totalAmount)
        {
            Description = Check.IsNotNull(description, nameof(description));
            Quantity = Check.IsNotNull(quantity, nameof(quantity));
            UnitAmount = Check.IsNotNull(unitAmount, nameof(unitAmount));
            Discount = Check.IsNotNull(discount, nameof(discount));
            TotalAmount = Check.IsNotNull(totalAmount, nameof(totalAmount));
        }

        public String1To250 Description { get; }

        public decimal Quantity { get; }

        public decimal UnitAmount { get; }

        public decimal Discount { get; }

        public decimal TotalAmount { get; }
    }
}