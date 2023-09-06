using Mews.Fiscalizations.Germany.V2.Model;
using NUnit.Framework;

namespace Mews.Fiscalizations.Germany.Tests.V2;

[TestFixture]
public class ClientTests
{
    [Test]
    public async Task CreateClientSucceeds()
    {
        var data = TestFixture.FiskalyTestData;
        var client = data.FiskalyClient;
        var tssId = data.Tss.Id;
        var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;

        await client.AdminLoginAsync(accessToken, tssId, TestFixture.AdminPin);
        var createdClient = await client.CreateClientAsync(accessToken, tssId);

        AssertClient(createdClient.IsSuccess, createdClient.SuccessResult.Id);

        // Disabling the Client so we don't exceed the test environment limit.
        await client.UpdateClientAsync(accessToken, tssId, createdClient.SuccessResult.Id, ClientState.Deregistered);
    }

    [Test]
    public async Task GetClientSucceeds()
    {
        var data = TestFixture.FiskalyTestData;
        var client = data.FiskalyClient;
        var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;
        var result = await client.GetClientAsync(accessToken, data.Client.Id, data.Tss.Id);

        AssertClient(result.IsSuccess, result.SuccessResult.Id);
    }

    [Test]
    public async Task GetAllTssClientsSucceeds()
    {
        var data = TestFixture.FiskalyTestData;
        var client = data.FiskalyClient;
        var tssId = data.Tss.Id;
        var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;

        await client.AdminLoginAsync(accessToken, tssId, TestFixture.AdminPin);
        var createdClient = (await client.CreateClientAsync(accessToken, tssId)).SuccessResult;
        var result = await client.GetRegisteredTssClientsAsync(accessToken, tssId);

        Assert.IsTrue(result.SuccessResult.Select(r => r.Id).Contains(createdClient.Id));

        // Disabling the Client so we don't exceed the test environment limit.
        await client.UpdateClientAsync(accessToken, tssId, createdClient.Id, ClientState.Deregistered);
    }

    private void AssertClient(bool isSuccess, object value)
    {
        Assert.IsTrue(isSuccess);
        Assert.IsNotNull(value);
    }
}