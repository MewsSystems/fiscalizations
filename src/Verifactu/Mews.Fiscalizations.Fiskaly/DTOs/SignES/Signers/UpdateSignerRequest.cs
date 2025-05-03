using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Signers;

internal sealed class UpdateSignerRequest
{
    [JsonPropertyName("content")]
    public UpdateSignerRequestData Data { get; init; }
}

internal sealed class UpdateSignerRequestData
{
    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SignerState State { get; init; }
}