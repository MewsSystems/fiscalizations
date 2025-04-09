namespace Mews.Fiscalizations.Sweden.Models;

public sealed class StatusEnrollmentData(string registerId)
{
    public string RegisterId { get; } = registerId;
}