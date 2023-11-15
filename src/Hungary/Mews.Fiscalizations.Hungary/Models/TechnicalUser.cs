namespace Mews.Fiscalizations.Hungary.Models;

public sealed class TechnicalUser
{
    public TechnicalUser(Login login, string password, SigningKey signingKey, LocalTaxpayerIdentificationNumber taxId, EncryptionKey encryptionKey)
    {
        Login = Check.IsNotNull(login, nameof(login));
        PasswordHash = Sha512.GetHash(Check.IsNotNull(password, nameof(password)));
        SigningKey = Check.IsNotNull(signingKey, nameof(signingKey));
        TaxId = Check.IsNotNull(taxId, nameof(taxId));
        EncryptionKey = Check.IsNotNull(encryptionKey, nameof(encryptionKey));
    }

    public Login Login { get; }

    public string PasswordHash { get; }

    public SigningKey SigningKey { get; }

    public LocalTaxpayerIdentificationNumber TaxId { get; }

    public EncryptionKey EncryptionKey { get; }
}