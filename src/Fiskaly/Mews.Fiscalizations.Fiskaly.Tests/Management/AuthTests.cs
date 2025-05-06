using Mews.Fiscalizations.Fiskaly.APIClients;
using Mews.Fiscalizations.Fiskaly.Models;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.Management;

[TestFixture]
public class AuthTests
{
    private ManagementApiClient _managementApiClient;

    [Test]
    public async Task GetAccessTokenSucceeds()
    {
        var accessToken = await _managementApiClient.AuthenticateAsync();

        Assert.That(accessToken.IsSuccess);
        Assert.That(accessToken.SuccessResult.Environment, Is.EqualTo(FiskalyEnvironment.Test), "Production API keys are used for tests.");
    }
    
    [OneTimeSetUp]
    public void SetUp()
    {
        var httpClient = new HttpClient();
        _managementApiClient = new ManagementApiClient(httpClient, TestFixture.ManagementApiKey, TestFixture.ManagementApiSecret);
    }
}