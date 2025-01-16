using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Sweden.DTOs;

internal sealed class CreateActivationRequest
{
    [Required]
    [JsonPropertyName("country")]
    public required string CountryCode { get; init; }

    [Required]
    [JsonPropertyName("corporateId")]
    public required string CorporateId { get; init; }

    [Required]
    [JsonPropertyName("cashRegisterName")]
    public required string CashRegisterName { get; init; }

    [JsonPropertyName("features")]
    public string[] Features { get; init; }

    [Required]
    [JsonPropertyName("controlUnitSerial")]
    public required string ControlUnitSerial { get; init; }

    [Required]
    [JsonPropertyName("controlUnitLocation")]
    public required string ControlUnitLocation { get; init; }

    [JsonPropertyName("validFrom")]
    public long? ValidFromUnix { get; init; }

    [JsonPropertyName("validTo")]
    public long? ValidToUnix { get; init; }

    [JsonPropertyName("registrationGeolocation")]
    public Address Address { get; init; }

    [Required]
    [JsonPropertyName("applicationNameAndVersion")]
    public required string ApplicationNameAndVersion { get; init; }

    [Required]
    [JsonPropertyName("applicationPackage")]
    public required string ApplicationPackage { get; init; }

    [JsonPropertyName("productionNumber")]
    public string ProductionNumber { get; init; }

    [JsonPropertyName("installationCreationInfo")]
    public InstallationCreationInfo InstallationCreationInfo { get; init; }

    [JsonPropertyName("contactInfo")]
    public ContactInfo ContactInfo { get; init; }
}

internal sealed class Address
{
    [Required]
    [JsonPropertyName("address")]
    public required string AddressLines { get; init; }

    [Required]
    [JsonPropertyName("city")]
    public required string City { get; init; }

    [Required]
    [JsonPropertyName("postalCode")]
    public required string PostalCode { get; init; }

    [Required]
    [JsonPropertyName("companyName")]
    public required string CompanyName { get; init; }
}

internal sealed class InstallationCreationInfo
{
    [Required]
    [JsonPropertyName("deviceId")]
    public required string DeviceId { get; init; }

    [Required]
    [JsonPropertyName("buildInfo")]
    public required IDictionary<string, string> BuildInfo { get; init; }

    [JsonPropertyName("programVersion")]
    public string ProgramVersion { get; init; }
}

internal sealed class ContactInfo
{
    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [Required]
    [JsonPropertyName("email")]
    public required string Email { get; init; }

    [Required]
    [JsonPropertyName("phone")]
    public required string Phone { get; init; }
}