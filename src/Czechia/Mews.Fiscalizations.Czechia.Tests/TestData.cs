namespace Mews.Fiscalizations.Czechia.Tests;

internal static class TestData
{
    public static readonly TimeSpan CzechSummerOffset = TimeSpan.FromHours(2);

    public static RevenueRecord CreateRecord(
        string taxId,
        decimal total = 1234.50m,
        decimal? depositToBeDrawn = null,
        decimal? drawnDeposit = null,
        string? delegatingTaxId = null,
        bool isOnBehalfOfMultipleTaxpayers = false,
        DateTimeOffset? created = null)
    {
        return new RevenueRecord(
            taxpayerTaxId: TaxIdentifier.CreateUnsafe(taxId),
            registrationUnitId: RegistrationUnitIdentifier.CreateUnsafe(243),
            registryId: RegistryIdentifier.CreateUnsafe("Mews/POS-1"),
            billNumber: BillNumber.CreateUnsafe($"#{Guid.NewGuid():N}".Substring(0, 25)),
            created: created ?? DateTimeOffset.Now,
            revenue: new Revenue(
                total: CurrencyValue.CreateUnsafe(total),
                depositToBeDrawn: depositToBeDrawn.HasValue ? CurrencyValue.CreateUnsafe(depositToBeDrawn.Value) : null,
                drawnDeposit: drawnDeposit.HasValue ? CurrencyValue.CreateUnsafe(drawnDeposit.Value) : null
            ),
            delegatingTaxpayerTaxId: delegatingTaxId is null ? null : TaxIdentifier.CreateUnsafe(delegatingTaxId),
            isOnBehalfOfMultipleTaxpayers: isOnBehalfOfMultipleTaxpayers
        );
    }
}
