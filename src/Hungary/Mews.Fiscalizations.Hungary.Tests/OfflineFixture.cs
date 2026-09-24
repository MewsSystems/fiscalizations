namespace Mews.Fiscalizations.Hungary.Tests;

/// <summary>
/// Placeholder credentials that only have to satisfy the formats, for tests that never reach NAV. Kept apart
/// from <see cref="TestFixture"/>, whose live credentials fail to initialize when they are not configured.
/// </summary>
public static class OfflineFixture
{
    public static readonly TechnicalUser User = new(
        login: Login.Create("abcdefghijklmno").Success.Get(),
        password: "password",
        signingKey: SigningKey.Create("ab-cdef-ghijklmnopqrstuvwxyz0123").Success.Get(),
        taxId: LocalTaxpayerIdentificationNumber.Create("12345678").Success.Get(),
        encryptionKey: EncryptionKey.Create("abcdefghijklmnop").Success.Get()
    );

    public static readonly SoftwareIdentification Software = new(
        id: "123456789123456789",
        name: "Test",
        type: SoftwareType.LocalSoftware,
        mainVersion: "1.0",
        developerName: "Test",
        developerContact: "test@test.com"
    );
}
