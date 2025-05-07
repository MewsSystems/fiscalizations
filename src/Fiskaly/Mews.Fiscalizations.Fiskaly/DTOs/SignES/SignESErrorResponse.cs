using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES;

internal sealed class SignESErrorResponse
{
    [JsonPropertyName("status")]
    public int StatusCode { get; init; }

    [JsonPropertyName("error")]
    public string Error { get; init; }

    [JsonPropertyName("code")]
    public string Code { get; init; }

    [JsonPropertyName("message")]
    public string Message { get; init; }
}