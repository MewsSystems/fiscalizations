using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Hungary.Models;
using NUnit.Framework;
using System;

namespace Mews.Fiscalizations.Hungary.Tests
{
    public static class TestFixture
    {
        public static readonly TaxpayerIdentificationNumber TaxPayerId = TaxpayerIdentificationNumber.Create(Countries.Hungary, Environment.GetEnvironmentVariable("hungarian_tax_payer_id") ?? "INSERT_TAX_PAYER_ID", isCountryCodePrefixAllowed: false).Success.Get();
        private static readonly Login Login = Login.Create(Environment.GetEnvironmentVariable("hungarian_login") ?? "INSERT_LOGIN").Success.Get();
        private static readonly string Password = Environment.GetEnvironmentVariable("hungarian_password") ?? "INSERT_PASSWORD";
        private static readonly SigningKey SigningKey = SigningKey.Create(Environment.GetEnvironmentVariable("hungarian_signing_key") ?? "INSERT_SIGNING_KEY").Success.Get();
        private static readonly EncryptionKey EncryptionKey = EncryptionKey.Create(Environment.GetEnvironmentVariable("hungarian_encryption_key") ?? "INSERT_ENCRYPTION_KEY").Success.Get();

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

        public static void AssertResponse<TResult, TCode>(ResponseResult<TResult, TCode> responseResult)
            where TResult : class
            where TCode : struct
        {
            Assert.IsNotNull(responseResult.SuccessResult);
            Assert.IsNull(responseResult.GeneralErrorResult);
            Assert.IsNull(responseResult.OperationalErrorResult);
        }
    }
}
