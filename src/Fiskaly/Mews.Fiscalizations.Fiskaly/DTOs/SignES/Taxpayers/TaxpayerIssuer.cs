using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayers;

internal sealed class TaxpayerIssuer
{
    [JsonPropertyName("legal_name")]
    public string LegalName { get; init; }

    [JsonPropertyName("tax_number")]
    public string TaxNumber { get; init; }
}