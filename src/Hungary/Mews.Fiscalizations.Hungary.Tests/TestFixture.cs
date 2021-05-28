using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Hungary.Models;
using NUnit.Framework;
using System;

namespace Mews.Fiscalizations.Hungary.Tests
{
    public static class TestFixture
    {
        public static readonly TaxpayerIdentificationNumber TaxPayerId = TaxpayerIdentificationNumber.Create(Countries.Hungary, Environment.GetEnvironmentVariable("tax_payer_id") ?? "14750636").Success.Get();
        private static readonly Login Login = Login.Create(Environment.GetEnvironmentVariable("login") ?? "w07wwgv843rnwso").Success.Get();
        private static readonly string Password = Environment.GetEnvironmentVariable("password") ?? "MewsTest459";
        private static readonly SigningKey SigningKey = SigningKey.Create(Environment.GetEnvironmentVariable("signing_key") ?? "f8-a9d1-73227923aeb7281AVZXJHZM8").Success.Get();
        private static readonly EncryptionKey EncryptionKey = EncryptionKey.Create(Environment.GetEnvironmentVariable("encryption_key") ?? "6090281AVZXM3GM9").Success.Get();

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
