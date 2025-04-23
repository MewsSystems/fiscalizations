using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Verifactu.DTOs;

internal sealed class MultipleSignerResponse
{
    [JsonPropertyName("results")]
    public List<SignerResponse> Results { get; init; }
}