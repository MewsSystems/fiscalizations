using Mews.Fiscalizations.Fiskaly.APIClients;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.DSFinVK;

[TestFixture]
public class AuthTests
{
    private DsfinvkApiClient _client;

    [OneTimeSetUp]
    public void SetUp()
    {
        _client = new DsfinvkApiClient(new HttpClient(), TestFixture.DsfinvkApiKey, TestFixture.DsfinvkApiSecret);
    }

    [Test]
    public async Task GetAccessTokenSucceeds()
    {
        var result = await _client.GetAccessTokenAsync();

        Assert.That(result.IsSuccess);
        Assert.That(result.SuccessResult, Is.Not.Null);
        Assert.That(result.SuccessResult.Value, Is.Not.Empty);
    }
}
