using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayers;

internal sealed class CreateTaxpayerRequest
{
    [JsonPropertyName("issuer")]
    public TaxpayerIssuer Issuer { get; init; }

    [JsonPropertyName("territory")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Territory Territory { get; init; }

    [JsonPropertyName("address")]
    public Address Address { get; init; }
}
