using Mews.Fiscalizations.Verifactu.Models;
using NUnit.Framework;

namespace Mews.Fiscalizations.Verifactu.Tests;

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
        await DisableResources();
    }

    [OneTimeTearDown]
    public async Task CleanupTestDataAsync()
    {
        await DisableResources();
    }

    private async Task DisableResources()
    {
        var httpClient = new HttpClient();
        var fiskalyClient = new FiskalyClient(httpClient, FiskalyEnvironment.Test, "", "");
        var accessTokenResult = (await fiskalyClient.GetAccessTokenAsync()).Success.Get();

        var allSigners = await fiskalyClient.GetAllSignersAsync(accessTokenResult);
        foreach (var signer in allSigners.Success.Get())
        {
            await fiskalyClient.DisableSignerAsync(accessTokenResult, signer.Id);
        }

        var allClients = await fiskalyClient.GetAllClientsAsync(accessTokenResult);
        foreach (var client in allClients.Success.Get())
        {
            await fiskalyClient.DisableClientAsync(accessTokenResult, client.ClientId);
        }
    }
}