using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.Management.Organizations;

internal sealed class CreateManagedOrganizationApiKeyResponse
{
    [JsonPropertyName("key")]
    public string Key { get; set; }
    
    [JsonPropertyName("secret")]
    public string Secret { get; set; }
}