namespace Mews.Fiscalizations.Germany.Tests.V2;

[TestFixture]
public class TssTests
{
    [Test]
    public async Task CreateTssSucceeds()
    {
        var client = TestFixture.FiskalyTestData.FiskalyClient;
        var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;
        var createdTss = await client.CreateTssAsync(accessToken);

        // After the creation of a new TSS, it is necessary to save the Admin PUK code that is returned in the response
        // The PUK code will be used later for setting/changing the admin PIN and for admin authentication.
        var adminPuk = createdTss.SuccessResult.AdminPuk;

        AssertTss(createdTss.IsSuccess, createdTss.SuccessResult.Id);

        await DisableTss(client, accessToken, createdTss.SuccessResult, adminPuk);
    }

    [Test]
    public async Task GetTssSucceeds()
    {
        var data = TestFixture.FiskalyTestData;
        var client = data.FiskalyClient;
        var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;
        var tss = await client.GetTssAsync(accessToken, data.Tss.Id);

        AssertTss(tss.IsSuccess, tss.SuccessResult.Id);
    }

    [Test]
    public async Task GetAllTSSsSucceeds()
    {
        var client = TestFixture.FiskalyTestData.FiskalyClient;
        var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;
        var createdTss = (await client.CreateTssAsync(accessToken)).SuccessResult;
        var allTSSs = (await client.GetAllEnabledTSSsAsync(accessToken)).SuccessResult;

        Assert.IsTrue(allTSSs.Select(t => t.Id).Contains(createdTss.Id));

        await DisableTss(client, accessToken, createdTss, adminPuk: createdTss.AdminPuk);
    }

    private void AssertTss(bool isSuccess, object value)
    {
        Assert.IsTrue(isSuccess);
        Assert.IsNotNull(value);
    }

    private async Task DisableTss(FiskalyClient client, AccessToken accessToken, CreateTssResult tss, string adminPuk)
    {
        // In order to disable a TSS, it must be in Uninitialized state.
        await client.UpdateTssAsync(accessToken, tss.Id, TssState.Uninitialized);

        // Changing the admin pin can be done only after updating the TSS state to uninitialized.
        await SetAdminPinAndLogin(client, accessToken, tss.Id, adminPuk);

        // Disabling the TSS after creation so we don't exceed the test environment limit.
        await client.UpdateTssAsync(accessToken, tss.Id, TssState.Disabled);
        await client.AdminLogoutAsync(tss.Id);
    }

    private async Task SetAdminPinAndLogin(FiskalyClient client, AccessToken accessToken, Guid tssId, string adminPuk)
    {
        var newPin = "1234567890";
        await client.ChangeAdminPinAsync(accessToken, tssId, adminPuk, newPin);
        await client.AdminLoginAsync(accessToken, tssId, newPin);
    }
}