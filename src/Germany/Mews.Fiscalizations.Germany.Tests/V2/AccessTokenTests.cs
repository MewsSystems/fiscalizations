namespace Mews.Fiscalizations.Germany.Tests.V2;

[TestFixture]
public class AccessTokenTests
{
    [Test]
    public async Task GetAccessTokenSucceeds()
    {
        var client = TestFixture.FiskalyTestData.FiskalyClient;
        var accessToken = await client.GetAccessTokenAsync();

        Assert.That(accessToken.IsSuccess);
        Assert.That(accessToken.SuccessResult.Environment, Is.EqualTo(FiskalyEnvironment.Test), "Production API keys are used for tests.");
    }
}