using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayers;

internal sealed class SignedTaxpayerAgreementRequest
{
    [JsonPropertyName("binary")]
    public string Document { get; init; }
}