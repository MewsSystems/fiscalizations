using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Invoices;

internal sealed class SimplifiedInvoiceRequest
{
    [JsonPropertyName("content")]
    public SimplifiedInvoiceData Content { get; init; }
}

internal sealed class SimplifiedInvoiceData
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
}

internal sealed class System
{
    [JsonPropertyName("type")]
    public string Type => "REGULAR";

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
