using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayers;

internal sealed class SignedTaxpayerAgreementResponse
{
    [JsonPropertyName("document_url")]
    public string DocumentUrl { get; init; }
    
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }
}