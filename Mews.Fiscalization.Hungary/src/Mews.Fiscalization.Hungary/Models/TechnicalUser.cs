using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Hungary.Models
{
    public sealed class TechnicalUser
    {
        public TechnicalUser(Login login, string password, SigningKey signingKey, TaxpayerIdentificationNumber taxId, EncryptionKey encryptionKey)
        {
            Login = Check.IsNotNull(login, nameof(login));
            PasswordHash = Utils.Sha512.GetHash(Check.IsNotNull(password, nameof(password)));
            SigningKey = Check.IsNotNull(signingKey, nameof(signingKey));
            TaxId = Check.IsNotNull(taxId, nameof(taxId));
            EncryptionKey = Check.IsNotNull(encryptionKey, nameof(encryptionKey));
        }

        public Login Login { get; }

        public string PasswordHash { get; }

        public SigningKey SigningKey { get; }

        public TaxpayerIdentificationNumber TaxId { get; }

        public EncryptionKey EncryptionKey { get; }
    }
}