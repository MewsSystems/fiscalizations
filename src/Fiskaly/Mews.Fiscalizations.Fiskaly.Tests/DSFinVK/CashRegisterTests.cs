using Mews.Fiscalizations.Fiskaly.APIClients;
using Mews.Fiscalizations.Fiskaly.Models;
using Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashRegisters;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.DSFinVK;

[TestFixture]
public class CashRegisterTests
{
    private DsfinvkApiClient _client;
    private AccessToken _accessToken;

    [OneTimeSetUp]
    public async Task SetUp()
    {
        _client = new DsfinvkApiClient(new HttpClient(), TestFixture.DsfinvkApiKey, TestFixture.DsfinvkApiSecret);
        var tokenResult = await _client.GetAccessTokenAsync();
        _accessToken = tokenResult.SuccessResult;

        // Ensure the cash register exists so GetCashRegisterSucceeds does not depend on test execution order.
        await _client.UpsertCashRegisterAsync(_accessToken, TestFixture.DsfinvkTestClientId, CreateCashRegister());
    }

    [Test]
    public async Task UpsertCashRegisterSucceeds()
    {
        var result = await _client.UpsertCashRegisterAsync(_accessToken, TestFixture.DsfinvkTestClientId, CreateCashRegister());

        Assert.That(result.IsSuccess, result.ErrorResult?.Message);
        Assert.That(result.SuccessResult.ClientId, Is.EqualTo(TestFixture.DsfinvkTestClientId));
        Assert.That(result.SuccessResult.Type, Is.EqualTo(CashRegisterType.Master));
    }

    [Test]
    public async Task GetCashRegisterSucceeds()
    {
        var result = await _client.GetCashRegisterAsync(_accessToken, TestFixture.DsfinvkTestClientId);

        Assert.That(result.IsSuccess, result.ErrorResult?.Message);
        Assert.That(result.SuccessResult.ClientId, Is.EqualTo(TestFixture.DsfinvkTestClientId));

        // Callers read the cash register to decide whether the upsert is still needed, so the response has to
        // carry back what was sent, not just the client id.
        Assert.That(result.SuccessResult.TssId, Is.EqualTo(TestFixture.DsfinvkTestTssId));
        Assert.That(result.SuccessResult.SoftwareVersion, Is.EqualTo(CreateCashRegister().SoftwareVersion));
    }

    private static CashRegister CreateCashRegister()
    {
        return new CashRegister(
            ClientId: TestFixture.DsfinvkTestClientId,
            Type: CashRegisterType.Master,
            TssId: TestFixture.DsfinvkTestTssId,
            SerialNumber: null,
            Brand: "Mews",
            Model: "Mews PMS",
            SoftwareBrand: "Mews",
            SoftwareVersion: "1.0.0",
            BaseCurrencyCode: "EUR"
        );
    }
}
