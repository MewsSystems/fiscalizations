using Mews.Fiscalizations.Fiskaly.DTOs.Management.Organizations;
using Mews.Fiscalizations.Fiskaly.Models.Management.Organizations;

namespace Mews.Fiscalizations.Fiskaly.Mappers.Management.Organizations;

public static class OrganizationMapper
{
    internal static ManagedOrganization MapManagedOrganization(this CreateManagedOrganizationResponse response)
    {
        return new ManagedOrganization
        {
            Id = Guid.Parse(response.Id),
            AddressLine1 = response.AddressLine1,
            AddressLine2 = response.AddressLine2,
            CountryCode = response.CountryCode,
            Name = response.Name,
            Town = response.Town,
            ManagedByOrganizationId = Guid.Parse(response.ManagedByOrganizationId),
            Zip = response.Zip,
            State = response.State,
            DisplayName = response.DisplayName,
            EconomyId = response.EconomyId,
            TaxNumber = response.TaxNumber,
            VatId = response.VatId,
            BillingAddressId = string.IsNullOrWhiteSpace(response.BillingAddressId) ? null : Guid.Parse(response.BillingAddressId),
            ContactPersonId = string.IsNullOrWhiteSpace(response.ContactPersonId) ? null : Guid.Parse(response.ContactPersonId),
            CreatedByUser = string.IsNullOrWhiteSpace(response.CreatedByUser) ? null : Guid.Parse(response.CreatedByUser)
        };
    }
    
    internal static ManagedOrganizationApiKey MapManagedOrganizationApiKey(this CreateManagedOrganizationApiKeyResponse response)
    {
        return new ManagedOrganizationApiKey
        {
            Key = response.Key,
            Secret = response.Secret
        };
    }
}