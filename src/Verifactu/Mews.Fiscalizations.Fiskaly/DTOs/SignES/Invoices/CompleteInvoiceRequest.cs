using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Invoices;

internal sealed class CompleteInvoiceRequest
{
    [JsonPropertyName("content")]
    public CompleteInvoiceData Content { get; init; }
}

internal sealed class CompleteInvoiceData
{
    [JsonPropertyName("type")]
    public string Type => "COMPLETE";

    [JsonPropertyName("data")]
    public SimplifiedInvoiceData Data { get; init; }

    [JsonPropertyName("recipients")]
    public List<RecipientRequest> Recipients { get; init; }
}

internal sealed class RecipientRequest
{
    [JsonPropertyName("id")]
    public object Id { get; init; }

    [JsonPropertyName("address_line")]
    public string AddressLine { get; init; }

    [JsonPropertyName("postal_code")]
    public string PostalCode { get; init; }
}

internal sealed class NationalIdentification
{
    [JsonPropertyName("legal_name")]
    public string LegalName { get; init; }

    [JsonPropertyName("tax_number")]
    public string TaxNumber { get; init; }
}

internal sealed class InternationalIdentification
{
    [JsonPropertyName("legal_name")]
    public string LegalName { get; init; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public IdentificationType Type { get; init; }

    [JsonPropertyName("number")]
    public string Number { get; init; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; init; }
}

internal enum IdentificationType
{
    TAX_NUMBER,
    PASSPORT,
    DOCUMENT,
    CERTIFICATE,
    OTHER
}