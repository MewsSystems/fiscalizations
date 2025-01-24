using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

public sealed class Srv4posBasicErrorResponse
{
    [JsonPropertyName("error")]
    public string Error { get; set; }
}