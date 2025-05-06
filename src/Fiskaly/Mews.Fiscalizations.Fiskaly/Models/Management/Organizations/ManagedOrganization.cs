namespace Mews.Fiscalizations.Fiskaly.Models.Management.Organizations;

public class ManagedOrganization
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string VatId { get; set; }

    public Guid ContactPersonId { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string Zip { get; set; }

    public string Town { get; set; }

    public string State { get; set; }

    public string CountryCode { get; set; }

    public string TaxNumber { get; set; }

    public string EconomyId { get; set; }

    public Guid BillingAddressId { get; set; }

    public Guid ManagedByOrganizationId { get; set; }

    public Guid CreatedByUser { get; set; }
}