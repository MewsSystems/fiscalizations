using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Verifactu.DTOs;

internal sealed class MultipleClientResponse
{
    [JsonPropertyName("results")]
    public List<ClientResponse> Results { get; init; }
}