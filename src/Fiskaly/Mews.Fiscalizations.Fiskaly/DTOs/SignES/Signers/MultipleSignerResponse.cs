using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Signers;

internal sealed class MultipleSignerResponse
{
    [JsonPropertyName("results")]
    public List<ContentWrapper<SignerDataResponse>> Results { get; init; }
}