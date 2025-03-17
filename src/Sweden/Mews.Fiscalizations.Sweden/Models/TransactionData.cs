using FuncSharp;

namespace Mews.Fiscalizations.Sweden.Models;

// TODO: Handle refunds either by having a different model or some validations (all numbers must be positive)
public sealed class TransactionData(
    DateTime dateTime,
    long organizationNumber,
    string organizationRegisterId,
    string registerFullAddress,
    int sequenceNumber,
    TransactionType transactionType,
    TaxAmount? twentyFivePercentTax = null,
    TaxAmount? twelvePercentTax = null,
    TaxAmount? sixPercentTax = null,
    TaxAmount? zeroPercentTax = null,
    decimal? saleAmount = null,
    decimal? refundAmount = null,
    DateTime? copyDateTime = null,
    int? copySequenceNumber = null)
{
    public DateTime DateTime { get; } = dateTime;

    public long OrganizationNumber { get; } = organizationNumber;

    public string OrganizationRegisterID { get; } = organizationRegisterId;

    public string RegisterFullAddress { get; } = registerFullAddress;

    public int SequenceNumber { get; } = sequenceNumber;

    public TransactionType TransactionType { get; } = transactionType;

    public TaxAmount? TwentyFivePercentTax { get; } = twentyFivePercentTax;

    public TaxAmount? TwelvePercentTax { get; } = twelvePercentTax;

    public TaxAmount? SixPercentTax { get; } = sixPercentTax;

    public TaxAmount? ZeroPercentTax { get; } = zeroPercentTax;

    public Option<decimal> SaleAmount { get; } = saleAmount.ToOption();

    public Option<decimal> RefundAmount { get; } = refundAmount.ToOption();

    public Option<DateTime> CopyDateTime { get; } = copyDateTime.ToOption();

    public Option<int> CopySequenceNumber { get; } = copySequenceNumber.ToOption();
}

public sealed record TaxAmount(decimal Amount = 0.00m, decimal SubtotalAmount = 0.00m);

public enum TransactionType
{
    Sale,
    Copy,
    Proforma,
    Training
}