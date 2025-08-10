using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Invoices;

internal class SimplifiedInvoiceRequest
{
    [JsonPropertyName("type")]
    public string Type => "SIMPLIFIED";

    [JsonPropertyName("number")]
    public string Number { get; init; }

    [JsonPropertyName("text")]
    public string Text { get; init; }

    [JsonPropertyName("full_amount")]
    public string FullAmount { get; init; }

    [JsonPropertyName("items")]
    public List<Item> Items { get; init; }
    
    [JsonPropertyName("series")]
    public string Series { get; init; }
    
    [JsonPropertyName("issued_at")]
    public string IssuedAt { get; init; }
}

internal sealed class Item
{
    [JsonPropertyName("text")]
    public string Text { get; init; }

    [JsonPropertyName("quantity")]
    public string Quantity { get; init; }

    [JsonPropertyName("unit_amount")]
    public string UnitAmount { get; init; }

    [JsonPropertyName("full_amount")]
    public string FullAmount { get; init; }

    [JsonPropertyName("system")]
    public System System { get; init; }
    
    [JsonPropertyName("vat_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VatTypeEnum VatType { get; init; }
}

internal sealed class System
{
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BillingSystemType Type { get; init; }

    [JsonPropertyName("category")]
    public Category Category { get; init; }
}

internal sealed class Category
{
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TaxCategoryType Type { get; init; }

    [JsonPropertyName("rate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Rate { get; init; }

    [JsonPropertyName("cause")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TaxExemptionReason? Cause { get; init; }
}

internal enum TaxCategoryType
{
    NO_VAT,
    VAT
}

internal enum TaxExemptionReason
{
    TAXABLE_EXEMPT_1,
    TAXABLE_EXEMPT_2,
    TAXABLE_EXEMPT_3,
    TAXABLE_EXEMPT_4,
    TAXABLE_EXEMPT_5,
    TAXABLE_EXEMPT_6
}

internal enum VatTypeEnum
{
    IVA,
    IPSI,
    IGIC,
    OTHER
}

internal enum BillingSystemType
{
    REGULAR,
    SIMPLIFIED_REGIME,
    EQUIVALENCE_SURCHARGE,
    EXPORT,
    AGRICULTURE,
    ANTIQUES,
    TRAVEL_AGENCIES,
    TRAVEL_AGENCY_MEDIATORS,
    OTHER_TAX_IVA,
    OTHER_TAX_IGIC,
    OTHER_TAX_IPSI
}
