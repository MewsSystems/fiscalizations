using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.Management.Organizations;

internal sealed class CreateManagedOrganizationResponse
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }

    [JsonPropertyName("vat_id")]
    public string VatId { get; set; }

    [JsonPropertyName("contact_person_id")]
    public string ContactPersonId { get; set; }

    [JsonPropertyName("address_line1")]
    public string AddressLine1 { get; set; }

    [JsonPropertyName("address_line2")]
    public string AddressLine2 { get; set; }

    [JsonPropertyName("zip")]
    public string Zip { get; set; }

    [JsonPropertyName("town")]
    public string Town { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }

    [JsonPropertyName("tax_number")]
    public string TaxNumber { get; set; }

    [JsonPropertyName("economy_id")]
    public string EconomyId { get; set; }

    [JsonPropertyName("billing_address_id")]
    public string BillingAddressId { get; set; }

    [JsonPropertyName("managed_by_organization_id")]
    public string ManagedByOrganizationId { get; set; }

    [JsonPropertyName("created_by_user")]
    public string CreatedByUser { get; set; }
}

