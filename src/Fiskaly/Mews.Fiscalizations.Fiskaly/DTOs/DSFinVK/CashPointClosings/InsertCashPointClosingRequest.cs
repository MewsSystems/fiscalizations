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

    // Required by the spec (the caller sends "0" on zero-transaction days).
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
    // Optional per spec.
    [JsonPropertyName("tx_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? TxId { get; init; }

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

    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; init; }

    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TransactionUser User { get; init; }

    [JsonPropertyName("buyer")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TransactionBuyer Buyer { get; init; }

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

internal sealed class TransactionUser
{
    [JsonPropertyName("user_export_id")]
    public string UserExportId { get; init; }

    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; init; }
}

internal sealed class TransactionBuyer
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("buyer_export_id")]
    public string BuyerExportId { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("address")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BuyerAddress Address { get; init; }

    [JsonPropertyName("vat_id_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string VatIdNumber { get; init; }
}

internal sealed class BuyerAddress
{
    [JsonPropertyName("street")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Street { get; init; }

    [JsonPropertyName("postal_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string PostalCode { get; init; }

    [JsonPropertyName("city")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string City { get; init; }

    [JsonPropertyName("country_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string CountryCode { get; init; }
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
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<TransactionLine> Lines { get; init; }

    [JsonPropertyName("amounts_per_vat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<AmountPerVat> AmountsPerVatId { get; init; }

    [JsonPropertyName("payment_types")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<PaymentTypeAmount> PaymentTypes { get; init; }

    [JsonPropertyName("notes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Notes { get; init; }
}

internal sealed class TransactionLine
{
    [JsonPropertyName("lineitem_export_id")]
    public string LineItemExportId { get; init; }

    [JsonPropertyName("storno")]
    public bool Storno { get; init; }

    [JsonPropertyName("business_case")]
    public BusinessCase BusinessCase { get; init; }

    [JsonPropertyName("in_house")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? InHouse { get; init; }

    [JsonPropertyName("references")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<TransactionReference> References { get; init; }

    [JsonPropertyName("voucher_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string VoucherId { get; init; }

    [JsonPropertyName("text")]
    public string ItemText { get; init; }

    [JsonPropertyName("item")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TransactionLineItem Item { get; init; }

    [JsonPropertyName("source_cash_register")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? SourceCashRegister { get; init; }
}

internal sealed class TransactionLineItem
{
    [JsonPropertyName("number")]
    public string Number { get; init; }

    [JsonPropertyName("gtin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Gtin { get; init; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; init; }

    [JsonPropertyName("quantity_factor")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? QuantityFactor { get; init; }

    [JsonPropertyName("quantity_measure")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string QuantityMeasure { get; init; }

    [JsonPropertyName("group_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string GroupId { get; init; }

    [JsonPropertyName("group_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string GroupName { get; init; }

    [JsonPropertyName("price_per_unit")]
    public decimal PricePerUnit { get; init; }

    [JsonPropertyName("base_amounts_per_vat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<VatAmountBreakdown> BaseAmountsPerVatId { get; init; }

    [JsonPropertyName("discounts_per_vat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<VatAmountBreakdown> DiscountsPerVatId { get; init; }

    [JsonPropertyName("extra_amounts_per_vat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<VatAmountBreakdown> ExtraAmountsPerVatId { get; init; }

    [JsonPropertyName("sub_items")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<TransactionSubItem> SubItems { get; init; }
}

internal sealed class VatAmountBreakdown
{
    [JsonPropertyName("vat_definition_export_id")]
    public long VatDefinitionExportId { get; init; }

    [JsonPropertyName("incl_vat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? InclVat { get; init; }

    [JsonPropertyName("excl_vat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? ExclVat { get; init; }

    [JsonPropertyName("vat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? Vat { get; init; }
}

internal sealed class TransactionSubItem
{
    [JsonPropertyName("number")]
    public string Number { get; init; }

    [JsonPropertyName("gtin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Gtin { get; init; }

    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; init; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; init; }

    [JsonPropertyName("quantity_factor")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? QuantityFactor { get; init; }

    [JsonPropertyName("quantity_measure")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string QuantityMeasure { get; init; }

    [JsonPropertyName("group_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string GroupId { get; init; }

    [JsonPropertyName("group_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string GroupName { get; init; }

    [JsonPropertyName("amount_per_vat_id")]
    public VatAmountBreakdown AmountPerVatId { get; init; }
}

internal sealed class BusinessCase
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; init; }

    [JsonPropertyName("purchaser_agency_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? PurchaserAgencyId { get; init; }

    [JsonPropertyName("amounts_per_vat_id")]
    public List<AmountPerVat> AmountsPerVatId { get; init; }
}

internal sealed class AmountPerVat
{
    [JsonPropertyName("vat_definition_export_id")]
    public long VatDefinitionExportId { get; init; }

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

    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; init; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    [JsonPropertyName("foreign_amount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? ForeignAmount { get; init; }

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
