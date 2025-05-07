using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES;

public class ContentWrapper<T>
{
    [JsonPropertyName("content")]
    public T Content { get; set; }
}