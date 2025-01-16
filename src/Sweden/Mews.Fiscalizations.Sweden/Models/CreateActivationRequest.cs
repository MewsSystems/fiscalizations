using System.Collections.Immutable;
using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Sweden.Models;

public sealed class CreateActivationRequest(
    Country country,
    string corporateId,
    string cashRegisterName,
    IEnumerable<string> features,
    string controlUnitSerial,
    string applicationNameAndVersion,
    string applicationPackage,
    Address address,
    string productionNumber = null,
    InstallationCreationInfo installationCreationInfo = null,
    ContactInfo contactInfo = null,
    DateTime? validFrom = null,
    DateTime? validTo = null)
{
    public Country Country { get; } = country;

    public string CorporateId { get; } = corporateId;

    public string CashRegisterName { get; } = cashRegisterName;

    public IReadOnlyList<string> Features { get; } = features.ToReadOnlyList();

    public string ControlUnitSerial { get; } = controlUnitSerial;

    public string ControlUnitLocation => "REMOTE";

    public string ApplicationNameAndVersion { get; } = applicationNameAndVersion;

    public string ApplicationPackage { get; } = applicationPackage;

    public Address Address { get; } = address;

    public Option<NonEmptyString> ProductionNumber { get; } = productionNumber.AsNonEmpty();

    public Option<InstallationCreationInfo> InstallationCreationInfo { get; } = installationCreationInfo.ToOption();

    public Option<ContactInfo> ContactInfo { get; } = contactInfo.ToOption();

    public Option<DateTime> ValidFrom { get; } = validFrom.ToOption();

    public Option<DateTime> ValidTo { get; } = validTo.ToOption();
}

public sealed class InstallationCreationInfo(string deviceId, string programVersion = null, IDictionary<string, string> buildInfo = null)
{
    public string DeviceId { get; } = deviceId;

    public Option<NonEmptyString> ProgramVersion { get; } = programVersion.AsNonEmpty();

    public ImmutableDictionary<string, string> BuildInfo { get; } = buildInfo.OrEmptyIfNull().ToImmutableDictionary();
}

public sealed record ContactInfo(string Name, string Email, string Phone);

public sealed record Address(string AddressLines, string City, string PostalCode, string CompanyName);