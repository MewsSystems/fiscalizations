using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Signer;

internal sealed class MultipleSignerResponse
{
    [JsonPropertyName("results")]
    public List<SignerResponse> Results { get; init; }
}