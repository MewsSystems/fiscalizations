using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayers;

internal sealed class TaxpayerResponse
{
    [JsonPropertyName("issuer")]
    public TaxpayerIssuer Issuer { get; init; }

    [JsonPropertyName("territory")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Territory Territory { get; init; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TaxpayerType Type { get; init; }

    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TaxpayerState? State { get; init; }
}
