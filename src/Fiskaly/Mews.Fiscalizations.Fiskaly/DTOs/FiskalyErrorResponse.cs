using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs;

internal sealed class FiskalyErrorResponse
{
    [JsonPropertyName("status_code")]
    public int StatusCode { get; init; }

    [JsonPropertyName("error")]
    public string Error { get; init; }

    [JsonPropertyName("code")]
    public string Code { get; init; }

    [JsonPropertyName("message")]
    public string Message { get; init; }
}