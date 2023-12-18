namespace Mews.Fiscalizations.Hungary.Tests;

public static class TestFixture
{
    public static readonly LocalTaxpayerIdentificationNumber TaxPayerId = LocalTaxpayerIdentificationNumber.Create(Environment.GetEnvironmentVariable("hungarian_tax_payer_id") ?? "INSERT_TAX_PAYER_ID").Success.Get();
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
        Assert.That(responseResult.ResponseXml, Is.Not.Empty);
        Assert.That(responseResult.RequestXml, Is.Not.Empty);
        Assert.That(responseResult.SuccessResult, Is.Not.Null);
        Assert.That(responseResult.GeneralErrorResult, Is.Null);
        Assert.That(responseResult.OperationalErrorResult, Is.Null);
    }
}