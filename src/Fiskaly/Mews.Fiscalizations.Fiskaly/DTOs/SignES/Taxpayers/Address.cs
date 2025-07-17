using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayers;

internal sealed class Address
{
    [JsonPropertyName("municipality")]
    public string Municipality { get; init; }
    
    [JsonPropertyName("city")]
    public string City { get; init; }
    
    [JsonPropertyName("street")]
    public string Street { get; init; }

    [JsonPropertyName("postal_code")]
    public string PostalCode { get; init; }
    
    [JsonPropertyName("number")]
    public string Number { get; init; }

    [JsonPropertyName("country")]
    public string Country { get; init; }
}