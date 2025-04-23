using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Verifactu.DTOs;

internal sealed class SoftwareRequest
{
    [JsonPropertyName("content")]
    public SoftwareData Data { get; init; }
}

internal sealed class Company
{
    [JsonPropertyName("legal_name")]
    public string LegalName { get; init; }

    [JsonPropertyName("tax_number")]
    public string TaxNumber { get; init; }
}

internal sealed class SoftwareData
{
    [JsonPropertyName("company")]
    public Company Company { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("license")]
    public string License { get; init; }

    [JsonPropertyName("version")]
    public string Version { get; init; }

    [JsonPropertyName("responsibility_declaration")]
    public string ResponsibilityDeclaration { get; init; }
}

