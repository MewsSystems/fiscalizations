using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.ClientDevices;

internal sealed class ClientResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ClientState State { get; init; }

    [JsonPropertyName("signer")]
    public Signer Signer { get; init; }
}

internal sealed class Signer
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
}

internal enum ClientState
{
    ENABLED,
    DISABLED
}
