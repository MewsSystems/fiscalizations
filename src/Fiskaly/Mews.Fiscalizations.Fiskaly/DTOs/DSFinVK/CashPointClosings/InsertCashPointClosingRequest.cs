using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.CashPointClosings;

internal sealed class InsertCashPointClosingRequest
{
    [JsonPropertyName("client_id")]
    public Guid ClientId { get; init; }

    [JsonPropertyName("cash_point_closing_export_id")]
    public long CashPointClosingExportId { get; init; }

    [JsonPropertyName("head")]
    public CashPointClosingHead Head { get; init; }

    [JsonPropertyName("transactions")]
    public List<CashPointClosingTransaction> Transactions { get; init; }

    [JsonPropertyName("cash_statement")]
    public CashStatement CashStatement { get; init; }
}

internal sealed class CashPointClosingHead
{
    [JsonPropertyName("export_creation_date")]
    public long ExportCreationDate { get; init; }

    [JsonPropertyName("business_date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string BusinessDate { get; init; }

    // Required by the spec (sent even on zero-transaction days; the mapper defaults them to "0").
    [JsonPropertyName("first_transaction_export_id")]
    public string FirstTransactionExportId { get; init; }

    [JsonPropertyName("last_transaction_export_id")]
    public string LastTransactionExportId { get; init; }
}

internal sealed class CashPointClosingTransaction
{
    [JsonPropertyName("head")]
    public TransactionHead Head { get; init; }

    [JsonPropertyName("data")]
    public TransactionData Data { get; init; }

    [JsonPropertyName("security")]
    public TransactionSecurity Security { get; init; }
}

internal sealed class TransactionHead
{
    [JsonPropertyName("tx_id")]
    public Guid TxId { get; init; }

    [JsonPropertyName("transaction_export_id")]
    public string TransactionExportId { get; init; }

    [JsonPropertyName("number")]
    public long Number { get; init; }

    [JsonPropertyName("timestamp_start")]
    public long TimestampStart { get; init; }

    [JsonPropertyName("timestamp_end")]
    public long TimestampEnd { get; init; }

    [JsonPropertyName("storno")]
    public bool Storno { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("closing_client_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? ClosingClientId { get; init; }

    [JsonPropertyName("references")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<TransactionReference> References { get; init; }

    [JsonPropertyName("allocation_groups")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string> AllocationGroups { get; init; }
}

internal sealed class TransactionReference
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("tx_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? TxId { get; init; }

    [JsonPropertyName("cash_point_closing_export_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? CashPointClosingExportId { get; init; }

    [JsonPropertyName("cash_register_export_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string CashRegisterExportId { get; init; }

    [JsonPropertyName("transaction_export_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string TransactionExportId { get; init; }

    [JsonPropertyName("external_export_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string ExternalExportId { get; init; }

    [JsonPropertyName("external_other_export_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string ExternalOtherExportId { get; init; }

    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; init; }

    [JsonPropertyName("date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Date { get; init; }
}

internal sealed class TransactionData
{
    [JsonPropertyName("full_amount_incl_vat")]
    public decimal FullAmountInclVat { get; init; }

    [JsonPropertyName("lines")]
    public List<TransactionLine> Lines { get; init; }

    [JsonPropertyName("amounts_per_vat_id")]
    public List<AmountPerVat> AmountsPerVatId { get; init; }

    [JsonPropertyName("payment_types")]
    public List<PaymentTypeAmount> PaymentTypes { get; init; }
}

internal sealed class TransactionLine
{
    [JsonPropertyName("lineitem_export_id")]
    public string LineItemExportId { get; init; }

    [JsonPropertyName("storno")]
    public bool Storno { get; init; }

    [JsonPropertyName("business_case")]
    public BusinessCase BusinessCase { get; init; }

    [JsonPropertyName("text")]
    public string ItemText { get; init; }

    [JsonPropertyName("item")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TransactionLineItem Item { get; init; }
}

internal sealed class TransactionLineItem
{
    [JsonPropertyName("number")]
    public string Number { get; init; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; init; }

    [JsonPropertyName("price_per_unit")]
    public decimal PricePerUnit { get; init; }
}

internal sealed class BusinessCase
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("amounts_per_vat_id")]
    public List<AmountPerVat> AmountsPerVatId { get; init; }
}

internal sealed class AmountPerVat
{
    [JsonPropertyName("vat_definition_export_id")]
    public int VatDefinitionExportId { get; init; }

    [JsonPropertyName("incl_vat")]
    public decimal GrossAmount { get; init; }

    [JsonPropertyName("excl_vat")]
    public decimal NetAmount { get; init; }

    [JsonPropertyName("vat")]
    public decimal TaxAmount { get; init; }
}

internal sealed class PaymentTypeAmount
{
    [JsonPropertyName("type")]
    public string PaymentType { get; init; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    // Required by the spec.
    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; init; }
}

internal sealed class TransactionSecurity
{
    [JsonPropertyName("tss_tx_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? TssTxId { get; init; }

    [JsonPropertyName("error_message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string ErrorMessage { get; init; }
}

internal sealed class CashStatement
{
    [JsonPropertyName("business_cases")]
    public List<BusinessCaseSummary> BusinessCases { get; init; }

    [JsonPropertyName("payment")]
    public CashStatementPayment Payment { get; init; }
}

internal sealed class BusinessCaseSummary
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("amounts_per_vat_id")]
    public List<AmountPerVat> AmountsPerVatId { get; init; }
}

internal sealed class CashStatementPayment
{
    [JsonPropertyName("full_amount")]
    public decimal FullAmount { get; init; }

    [JsonPropertyName("cash_amount")]
    public decimal CashAmount { get; init; }

    [JsonPropertyName("cash_amounts_by_currency")]
    public List<CashAmountByCurrency> CashAmountsByCurrency { get; init; }

    [JsonPropertyName("payment_types")]
    public List<PaymentTypeAmount> PaymentTypes { get; init; }
}

internal sealed class CashAmountByCurrency
{
    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; init; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }
}
