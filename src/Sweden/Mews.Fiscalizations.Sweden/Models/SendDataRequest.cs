using FuncSharp;

namespace Mews.Fiscalizations.Sweden.Models;

public sealed class SendDataRequest(
    decimal grossAmount,
    IDictionary<string, int> vatRateToSum,
    bool isRefund,
    PrintType printType,
    DateTime saleDate,
    int? receiptNumber = null)
{
    // Brutto – price with tax. You must use centestimal subdivision of Swedish krona - Öre.
    // For instance for the receipt with the full price 26.75 SEK the value of field "brutto" must be "2675".
    public int GrossAmount { get; } = (int)(grossAmount * 100);

    /// <summary>
    /// The field “vatToRate” indicates the amount of VAT for the rate.
    /// For example if your "brutto" contains two items
    /// Item1 with price(netto) 5000 and VAT rate 6%
    /// Item2 with price(netto) 7000 and VAT rate 12%
    /// You must send us the following value:
    /// "vatRateToSum":{"0":0,"6":300,"12":840,"25":0}
    /// </summary>
    public IReadOnlyDictionary<string, int> VatRateToSum { get; } = vatRateToSum.AsReadOnly();

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
    /// Autoincrement receipt number within one POS terminal.
    /// </summary>
    public Option<int> ReceiptNumber { get; } = receiptNumber.ToOption();

    /// <summary>
    /// Date and time of sale.
    /// </summary>
    public DateTime SaleDate { get; } = saleDate;
}

public enum PrintType
{
    Normal = 0,
    Copy = 1,
    Proforma = 2
}