using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Verifactu.DTOs;

internal sealed class UpdateClientRequest
{
    [JsonPropertyName("content")]
    public UpdateClientRequestData Data { get; init; }
}

internal sealed class UpdateClientRequestData
{
    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ClientState State { get; init; }
}