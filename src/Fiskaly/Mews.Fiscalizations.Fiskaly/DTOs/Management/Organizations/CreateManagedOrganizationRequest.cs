using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.Management.Organizations;

internal sealed class CreateManagedOrganizationRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("address_line1")]
    public string AddressLine1 { get; set; }
    
    [JsonPropertyName("zip")]
    public string Zip { get; set; }
    
    [JsonPropertyName("town")]
    public string Town { get; set; }
    
    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }
    
    [JsonPropertyName("managed_by_organization_id")]
    public string ManagedByOrganizationId { get; set; }
}