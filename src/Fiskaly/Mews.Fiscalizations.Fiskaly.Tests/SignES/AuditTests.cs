using Mews.Fiscalizations.Fiskaly.APIClients;
using Mews.Fiscalizations.Fiskaly.Models;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.SignES;

[TestFixture]
public class AuditTests
{
    private SignESApiClient _signEsApiClient;
    private AccessToken _authToken;

    [Test]
    public async Task GetSoftwareSucceeds()
    {
        var softwareResult = await _signEsApiClient.GetSoftwareAuditDataAsync(_authToken);

        Assert.That(softwareResult.IsSuccess);
        Assert.That(softwareResult.SuccessResult, Is.Not.Null);
        Assert.That(softwareResult.SuccessResult.Version, Is.Not.Null);
    }
    
    [OneTimeSetUp]
    public async Task SetUp()
    {
        var httpClient = new HttpClient();
        _signEsApiClient = new SignESApiClient(httpClient, FiskalyEnvironment.Test, TestFixture.SignESApiKey, TestFixture.SignESApiSecret);
        _authToken = (await _signEsApiClient.GetAccessTokenAsync()).SuccessResult;
    }
}