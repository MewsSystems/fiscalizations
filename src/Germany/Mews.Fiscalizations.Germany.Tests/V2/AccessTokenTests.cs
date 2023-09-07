namespace Mews.Fiscalizations.Germany.Tests.V2;

[TestFixture]
public class AccessTokenTests
{
    [Test]
    public async Task GetAccessTokenSucceeds()
    {
        var client = TestFixture.FiskalyTestData.FiskalyClient;
        var accessToken = await client.GetAccessTokenAsync();

        Assert.IsTrue(accessToken.IsSuccess);
        Assert.AreEqual(accessToken.SuccessResult.Environment, FiskalyEnvironment.Test, "Production API keys are used for tests.");
    }
}