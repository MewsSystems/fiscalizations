using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK;

internal sealed class DsfinvkErrorResponse
{
    [JsonPropertyName("status_code")]
    public int? StatusCode { get; init; }

    [JsonPropertyName("error")]
    public string Error { get; init; }

    [JsonPropertyName("code")]
    public string Code { get; init; }

    [JsonPropertyName("message")]
    public string Message { get; init; }
}
