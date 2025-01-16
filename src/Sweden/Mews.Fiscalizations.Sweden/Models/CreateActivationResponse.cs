using FuncSharp;

namespace Mews.Fiscalizations.Sweden.Models;

public sealed class CreateActivationResponse(
    string apiKey,
    int activationId,
    Address address,
    string productionNumber = null,
    string phone = null)
{
    public string ApiKey { get; } = apiKey;

    public int ActivationId { get; } = activationId;

    public Address Address { get; } = address;

    public Option<NonEmptyString> ProductionNumber { get; } = productionNumber.AsNonEmpty();

    public Option<NonEmptyString> Phone { get; } = phone.AsNonEmpty();
}