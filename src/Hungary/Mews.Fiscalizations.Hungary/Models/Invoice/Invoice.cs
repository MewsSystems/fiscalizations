using System;
using System.Collections.Generic;
using System.Linq;
using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models;

public class Invoice
{
    public Invoice(
        InvoiceNumber number,
        DateTime issueDate,
        DateTime paymentDate,
        SupplierInfo supplierInfo,
        Receiver receiver,
        CurrencyCode currencyCode,
        ISequence<InvoiceItem> items,
        bool isSelfBilling = false,
        bool isCashAccounting = false,
        PaymentMethod? paymentMethod = null)
    {
        Number = number;
        IssueDate = issueDate;
        PaymentDate = paymentDate;
        SupplierInfo = supplierInfo;
        Receiver = receiver;
        CurrencyCode = currencyCode;
        Items = items;
        DeliveryDate = Items.Values.Max(i => i.Value.ConsumptionDate);
        ExchangeRate = GetExchangeRate(items).Success.Get();
        TaxSummary = GetTaxSummary(items);
        IsSelfBilling = isSelfBilling;
        IsCashAccounting = isCashAccounting;
        PaymentMethod = paymentMethod.ToOption();

        CheckConsistency(this);
    }

    public InvoiceNumber Number { get; }

    public DateTime DeliveryDate { get; }

    public DateTime IssueDate { get; }

    public DateTime PaymentDate { get; }

    public IOption<PaymentMethod> PaymentMethod { get; }

    public SupplierInfo SupplierInfo { get; }

    public Receiver Receiver { get; }

    public CurrencyCode CurrencyCode { get; }

    public ExchangeRate ExchangeRate { get; }

    public List<TaxSummaryItem> TaxSummary { get; }

    public ISequence<InvoiceItem> Items { get; }

    public bool IsSelfBilling { get; }

    public bool IsCashAccounting { get; }

    private ITry<ExchangeRate, Error> GetExchangeRate(ISequence<InvoiceItem> items)
    {
        var totalGrossHuf = items.Values.Sum(i => Math.Abs(i.Value.TotalAmounts.AmountHUF.Gross.Value));
        var totalGross = items.Values.Sum(i => Math.Abs(i.Value.TotalAmounts.Amount.Gross.Value));
        return totalGross.Match(
            0, _ => ExchangeRate.Create(1),
            _ => ExchangeRate.Rounded(totalGrossHuf / totalGross)
        );
    }

    private List<TaxSummaryItem> GetTaxSummary(ISequence<InvoiceItem> indexedItems)
    {
        var itemsByTaxRate = indexedItems.Values.GroupBy(i => i.Value.TotalAmounts.TaxRatePercentage);
        var taxSummaryItems = itemsByTaxRate.Select(g => new TaxSummaryItem(
            amount: Amount.Sum(g.Select(i => i.Value.TotalAmounts.Amount)),
            amountHUF: Amount.Sum(g.Select(i => i.Value.TotalAmounts.AmountHUF)),
            taxRatePercentage: g.Key.ToNullable()
        ));
        return taxSummaryItems.AsList();
    }

    private static void CheckConsistency(Invoice invoice)
    {
        var nonDefaultCurrency = !invoice.CurrencyCode.Equals(TaxationInfo.DefaultCurrencyCode);
        var hasRequiredTaxRates = nonDefaultCurrency.Implies(() => invoice.Items.Values.All(i => i.Value.ExchangeRate != null));
        if (!hasRequiredTaxRates)
        {
            throw new InvalidOperationException("Exchange rate needs to be specified for all items.");
        }
    }
}