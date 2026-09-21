namespace Mews.Fiscalizations.Hungary.Models;

public sealed class InvoiceItem
{
    public InvoiceItem(
        DateTime deliveryDate,
        ItemAmounts totalAmounts,
        ItemAmounts unitAmounts,
        UnitOfMeasure unitOfMeasure,
        Description description,
        decimal quantity,
        ExchangeRate lineExchangeRate = null,
        bool isAdvance = false,
        AdvancePaymentData advancePaymentData = null)
    {
        DeliveryDate = deliveryDate;
        TotalAmounts = Check.IsNotNull(totalAmounts, nameof(totalAmounts));
        UnitAmounts = Check.IsNotNull(unitAmounts, nameof(unitAmounts));
        UnitOfMeasure = Check.IsNotNull(unitOfMeasure, nameof(unitOfMeasure));
        Description = Check.IsNotNull(description, nameof(description));
        Quantity = quantity;
        LineExchangeRate = lineExchangeRate.ToOption();
        IsAdvance = isAdvance;
        AdvancePaymentData = advancePaymentData.ToOption();
    }

    /// <summary>
    /// When this item was supplied. On an aggregate invoice NAV reports it per line
    /// (aggregateInvoiceLineData/lineDeliveryDate), and the invoice delivery date is the latest of them.
    /// </summary>
    public DateTime DeliveryDate { get; }

    public ItemAmounts TotalAmounts { get; }

    public ItemAmounts UnitAmounts { get; }

    public UnitOfMeasure UnitOfMeasure { get; }

    public Description Description { get; }

    /// <summary>
    /// NAV QuantityType allows 10 decimal places, so this is a decimal: a fractional quantity must not be
    /// silently rounded to a whole unit.
    /// </summary>
    public decimal Quantity { get; }

    /// <summary>
    /// The rate for this item, required on an aggregate invoice in a foreign currency because each supply
    /// has its own rate at its own delivery date.
    /// </summary>
    public Option<ExchangeRate> LineExchangeRate { get; }

    /// <summary>
    /// NAV advanceData/advanceIndicator. Distinct from depositIndicator, which in NAV means a
    /// bottle or container deposit.
    /// </summary>
    public bool IsAdvance { get; }

    public Option<AdvancePaymentData> AdvancePaymentData { get; }
}
