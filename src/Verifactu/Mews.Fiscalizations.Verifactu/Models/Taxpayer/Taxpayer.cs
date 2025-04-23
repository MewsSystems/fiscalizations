namespace Mews.Fiscalizations.Verifactu.Models;

public sealed record Taxpayer(string LegalName, string TaxIdentifier, TaxpayerTerritory Territory, TaxpayerState State, TaxpayerType Type = TaxpayerType.Company);

public enum TaxpayerState
{
    Enabled,
    Disabled
}