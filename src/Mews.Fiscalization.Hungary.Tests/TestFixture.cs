using Mews.Fiscalization.Core.Model;
using Mews.Fiscalization.Hungary.Models;
using System;

namespace Mews.Fiscalization.Hungary.Tests
{
    public static class TestFixture
    {
        private static readonly Login Login = Login.Create(Environment.GetEnvironmentVariable("login") ?? "INSERT_LOGIN").Success.Get();
        private static readonly string Password = Environment.GetEnvironmentVariable("password") ?? "INSERT_PASSWORD";
        private static readonly SigningKey SigningKey = SigningKey.Create(Environment.GetEnvironmentVariable("signing_key") ?? "INSERT_SIGNING_KEY").Success.Get();
        private static readonly TaxpayerIdentificationNumber TaxPayerId = TaxpayerIdentificationNumber.Create(Countries.Hungary, Environment.GetEnvironmentVariable("tax_payer_id") ?? "INSERT_TAX_PAYER_ID").Success.Get();
        private static readonly EncryptionKey EncryptionKey = EncryptionKey.Create(Environment.GetEnvironmentVariable("encryption_key") ?? "INSERT_ENCRYPTION_KEY").Success.Get();

        public static NavClient GetNavClient()
        {
            var technicalUser = new TechnicalUser(
                login: Login,
                password: Password,
                signingKey: SigningKey,
                taxId: TaxPayerId,
                encryptionKey: EncryptionKey
            );
            var softwareIdentification = new SoftwareIdentification(
                id: "123456789123456789",
                name: "Test",
                type: SoftwareType.LocalSoftware,
                mainVersion: "1.0",
                developerName: "Test",
                developerContact: "test@test.com"
            );
            return new NavClient(technicalUser, softwareIdentification, NavEnvironment.Test);
        }
    }
}
