using Mews.Fiscalizations.Germany.V2;
using Mews.Fiscalizations.Germany.V2.Model;
using NUnit.Framework;

namespace Mews.Fiscalizations.Germany.Tests.V2;

/// <summary>
/// Fiskaly limits creation of data in the test environment to 5.
/// So we're trying to delete the data after creating them in tests to make sure we don't hit the limit.
/// </summary>
[SetUpFixture]
public class FiskalyTestFixture
{
    [OneTimeSetUp]
    public async Task SetUpFiskalyTestDataAsync()
    {
        var fiskalyClient = new FiskalyClient(TestFixture.ApiKey, TestFixture.ApiSecret);
        var accessToken = (await fiskalyClient.GetAccessTokenAsync()).SuccessResult;
        var allTsses = (await fiskalyClient.GetAllEnabledTSSsAsync(accessToken)).SuccessResult;

        var firstInitalizedTss = allTsses.FirstOption(t => t.State == TssState.Initialized);
        TestFixture.FiskalyTestData =  await firstInitalizedTss.Match(
            async t =>
            {
                var firstRegisteredClient = (await fiskalyClient.GetRegisteredTssClientsAsync(accessToken, t.Id)).SuccessResult.FirstOption();
                var client = await firstRegisteredClient.Match(
                    c => Task.FromResult(c),
                    async _ =>
                    {
                        await fiskalyClient.AdminLoginAsync(accessToken, t.Id, TestFixture.AdminPin);
                        return (await fiskalyClient.CreateClientAsync(accessToken, t.Id)).SuccessResult;
                    }
                );
                return new FiskalyTestData(fiskalyClient, t, client);
            },
            async _ =>
            {
                var tss = (await fiskalyClient.CreateTssAsync(accessToken)).SuccessResult;
                await fiskalyClient.UpdateTssAsync(accessToken, tss.Id, TssState.Uninitialized);
                await fiskalyClient.ChangeAdminPinAsync(accessToken, tss.Id, tss.AdminPuk, TestFixture.AdminPin);
                await fiskalyClient.AdminLoginAsync(accessToken, tss.Id, TestFixture.AdminPin);
                await fiskalyClient.UpdateTssAsync(accessToken, tss.Id, TssState.Initialized);

                var client = (await fiskalyClient.CreateClientAsync(accessToken, tss.Id)).SuccessResult;
                await fiskalyClient.AdminLogoutAsync(tss.Id);

                return new FiskalyTestData(fiskalyClient, tss, client);
            }
        );
    }

    [OneTimeTearDown]
    public async Task CleanupTestDataAsync()
    {
        var data = TestFixture.FiskalyTestData;
        if (data is not null)
        {
            var fiskalyClient = data.FiskalyClient;
            var adminPin = TestFixture.AdminPin;
            var accessToken = (await fiskalyClient.GetAccessTokenAsync()).SuccessResult;

            // Disabling created TSS.
            var tssId = data.Tss.Id;
            await fiskalyClient.AdminLoginAsync(accessToken, tssId, adminPin);
            await fiskalyClient.UpdateTssAsync(accessToken, tssId, TssState.Disabled);
            await fiskalyClient.AdminLogoutAsync(tssId);

            // Disabling created Client.
            var client = data.Client;
            var clientTssId = client.TssId;
            await fiskalyClient.AdminLoginAsync(accessToken, clientTssId, adminPin);
            await fiskalyClient.UpdateClientAsync(accessToken, clientTssId, client.Id, ClientState.Deregistered);
            await fiskalyClient.AdminLogoutAsync(clientTssId);
        }
    }
}