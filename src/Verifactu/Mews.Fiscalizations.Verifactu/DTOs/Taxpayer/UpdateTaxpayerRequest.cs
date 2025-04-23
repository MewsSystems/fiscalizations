using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Verifactu.DTOs;

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