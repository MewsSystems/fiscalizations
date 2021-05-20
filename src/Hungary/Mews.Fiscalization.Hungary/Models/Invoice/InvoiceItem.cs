using Mews.Fiscalization.Core.Model;
using System;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class InvoiceItem
    {
        public InvoiceItem(
            DateTime consumptionDate,
            ItemAmounts totalAmounts,
            ItemAmounts unitAmounts,
            MeasurementUnit measurementUnit,
            Description description,
            int quantity,
            ExchangeRate exchangeRate = null,
            bool isDeposit = false)
        {
            ConsumptionDate = consumptionDate;
            TotalAmounts = Check.IsNotNull(totalAmounts, nameof(totalAmounts));
            UnitAmounts = Check.IsNotNull(unitAmounts, nameof(unitAmounts));
            MeasurementUnit = measurementUnit;
            Description = Check.IsNotNull(description, nameof(description));
            Quantity = quantity;
            ExchangeRate = exchangeRate;
            IsDeposit = isDeposit;
        }

        public DateTime ConsumptionDate { get; }

        public ItemAmounts TotalAmounts { get; }

        public ItemAmounts UnitAmounts { get; }

        public MeasurementUnit MeasurementUnit { get; }

        public Description Description { get; }

        public int Quantity { get; }

        public ExchangeRate ExchangeRate { get; }

        public bool IsDeposit { get; }
    }
}
