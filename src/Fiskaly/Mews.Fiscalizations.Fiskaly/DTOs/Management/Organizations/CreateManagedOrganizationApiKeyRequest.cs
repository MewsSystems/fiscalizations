using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.Management.Organizations;

internal sealed class CreateManagedOrganizationApiKeyRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ApiKeyStatusEnum Status { get; set; }

    [JsonPropertyName("managed_by_organization_id")]
    public string ManagedByOrganizationId { get; set; }

}