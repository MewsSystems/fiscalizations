using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Verifactu.DTOs;

internal sealed class SoftwareResponse
{
    [JsonPropertyName("content")]
    public SoftwareData Data { get; init; }
}