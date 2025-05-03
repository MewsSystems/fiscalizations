using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayer;

internal sealed class CreateTaxpayerRequest
{
    [JsonPropertyName("content")]
    public TaxpayerDataRequest Data { get; init; }
}

internal sealed class TaxpayerDataRequest
{
    [JsonPropertyName("issuer")]
    public TaxpayerIssuer Issuer { get; init; }

    [JsonPropertyName("territory")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Territory Territory { get; init; }
}
