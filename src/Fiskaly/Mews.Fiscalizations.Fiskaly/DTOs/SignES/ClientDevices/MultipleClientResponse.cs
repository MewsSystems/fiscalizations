using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.ClientDevices;

internal sealed class MultipleClientResponse
{
    [JsonPropertyName("results")]
    public List<ContentWrapper<ClientResponse>> Results { get; init; }
}