using FuncSharp;

namespace Mews.Fiscalizations.Sweden.Models;

public sealed class SendDataRequest(
    decimal grossAmount,
    IDictionary<decimal, decimal> totalTaxByVatRate,
    bool isRefund,
    PrintType printType,
    DateTime saleDate,
    int? receiptNumber = null)
{
    /// <summary>
    /// Total gross amount.
    /// </summary>
    public decimal GrossAmount { get; } = grossAmount;

    /// <summary>
    /// Key: VAT rate (ex: 0.25), Value: Total tax amount for the VAT rate.
    /// </summary>
    public IReadOnlyDictionary<decimal, decimal> TotalTaxByVatRate { get; } = totalTaxByVatRate.AsReadOnly();

    /// <summary>
    /// It is "false" for normal receipt and "true" for refund receipt,
    /// Please note: the field "brutto" is positive for both cases
    /// </summary>
    public bool IsRefund { get; } = isRefund;

    /// <summary>
    /// The type of receipt print. One of the following values: 'Normal' (Normal receipt), 'Copy' (Receipt copy), 'Proforma'.
    /// </summary>
    public PrintType PrintType { get; } = printType;

    /// <summary>
    /// Date and time of sale.
    /// </summary>
    public DateTime SaleDate { get; } = saleDate;

    /// <summary>
    /// Autoincrement receipt number within one POS terminal.
    /// </summary>
    public Option<int> ReceiptNumber { get; } = receiptNumber.ToOption();
}

public enum PrintType
{
    Normal = 0,
    Copy = 1,
    Proforma = 2
}