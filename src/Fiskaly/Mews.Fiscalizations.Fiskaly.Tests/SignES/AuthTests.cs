using Mews.Fiscalizations.Fiskaly.APIClients;
using Mews.Fiscalizations.Fiskaly.Models;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.SignES;

[TestFixture]
public class AuthTests
{
    private SignESApiClient _signEsApiClient;

    [Test]
    public async Task GetAccessTokenSucceeds()
    {
        var accessToken = await _signEsApiClient.GetAccessTokenAsync();

        Assert.That(accessToken.IsSuccess);
        Assert.That(accessToken.SuccessResult.Environment, Is.EqualTo(FiskalyEnvironment.Test), "Production API keys are used for tests.");
    }
    
    [OneTimeSetUp]
    public void SetUp()
    {
        var httpClient = new HttpClient();
        _signEsApiClient = new SignESApiClient(httpClient, FiskalyEnvironment.Test, TestFixture.SignESApiKey, TestFixture.SignESApiSecret);
    }
}