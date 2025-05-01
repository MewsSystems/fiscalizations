using Mews.Fiscalizations.Verifactu.Models;
using NUnit.Framework;

namespace Mews.Fiscalizations.Verifactu.Tests;

/// <summary>
/// Fiskaly limits creation of data in the test environment to 5.
/// So we're trying to delete the data after creating them in tests to make sure we don't hit the limit.
/// </summary>
[SetUpFixture]
public class SignESTestFixture
{
    [OneTimeSetUp]
    public async Task SetUpSignESTestDataAsync()
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
        var signEsApiClient = new SignESApiClient(httpClient, FiskalyEnvironment.Test, "", "");
        var accessTokenResult = (await signEsApiClient.GetAccessTokenAsync()).SuccessResult;

        var allSigners = await signEsApiClient.GetAllSignersAsync(accessTokenResult);
        foreach (var signer in allSigners.SuccessResult)
        {
            await signEsApiClient.DisableSignerAsync(accessTokenResult, signer.Id);
        }

        var allClients = await signEsApiClient.GetAllClientsAsync(accessTokenResult);
        foreach (var client in allClients.SuccessResult)
        {
            await signEsApiClient.DisableClientAsync(accessTokenResult, client.ClientId);
        }
    }
}