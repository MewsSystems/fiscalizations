using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayers;

internal sealed class UpdateTaxpayerRequest
{
    [JsonPropertyName("content")]
    public UpdateTaxpayerRequestData Data { get; init; }
}

internal sealed class UpdateTaxpayerRequestData
{
    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TaxpayerState State { get; init; }
}