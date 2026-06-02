namespace Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashRegisters;

public sealed record CashRegister(
    Guid ClientId,
    CashRegisterType Type,
    Guid TssId,
    string SerialNumber,
    string Brand,
    string Model,
    string SoftwareBrand,
    string SoftwareVersion,
    string BaseCurrencyCode
);

public enum CashRegisterType
{
    Master
}
