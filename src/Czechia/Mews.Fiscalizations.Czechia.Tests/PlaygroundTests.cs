namespace Mews.Fiscalizations.Czechia.Tests;

/// <summary>
/// Sends real messages to the EET 2.0 playground (pg.trzbyeet.gov.cz) with its shared test certificates.
/// </summary>
[Category("Integration")]
public class PlaygroundTests
{
    private static readonly HttpClient HttpClient = new() { Timeout = TimeSpan.FromSeconds(30) };

    [Test]
    [Retry(3)]
    [TestCaseSource(typeof(Playground), nameof(Playground.TaxIds))]
    public async Task SendRevenue(string taxId)
    {
        var client = new EetClient(HttpClient, await Playground.GetCertificateAsync(taxId), EetEnvironment.Playground);
        var result = await client.SendRevenueAsync(TestData.CreateRecord(taxId));

        Assert.That(result.IsSuccess, Is.True, result.ResponseXml);
        Assert.That(result.IsTest, Is.True);
        Assert.That(result.Success.Get().ConfirmationCode, Does.Match("^[0-9a-f]{8}-[0-9a-f]{4}-4[0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}-ff$"));
        Assert.That(result.Warnings, Is.Empty, result.ResponseXml);
        Assert.That(result.GlobalTransactionId.NonEmpty, Is.True);
    }

    [Test]
    [Retry(3)]
    public async Task SendRevenueWithAllOptionalItems()
    {
        var client = new EetClient(HttpClient, await Playground.GetCertificateAsync("CZ683555118"), EetEnvironment.Playground);
        var record = TestData.CreateRecord(
            "CZ683555118",
            total: -187.20m,
            depositToBeDrawn: 25m,
            drawnDeposit: 302m,
            delegatingTaxId: "CZ00000019",
            isOnBehalfOfMultipleTaxpayers: true
        );
        var result = await client.SendRevenueAsync(record, isFirstAttempt: false);

        Assert.That(result.IsSuccess, Is.True, result.ResponseXml);
        Assert.That(result.Warnings, Is.Empty, result.ResponseXml);
    }

    [Test]
    [Retry(3)]
    public async Task VerificationMode()
    {
        var client = new EetClient(HttpClient, await Playground.GetCertificateAsync("CZ00000019"), EetEnvironment.Playground);
        var result = await client.SendRevenueAsync(TestData.CreateRecord("CZ00000019"), mode: EetMode.Verification);

        Assert.That(result.IsSuccess, Is.False, result.ResponseXml);
        Assert.That(result.Error.Get().IsSuccessfulVerification, Is.True, result.ResponseXml);
        Assert.That(result.IsTest, Is.True);
    }

    [Test]
    [Retry(3)]
    public async Task TaxIdNotMatchingCertificateIsReportedAsWarning()
    {
        var client = new EetClient(HttpClient, await Playground.GetCertificateAsync("CZ00000019"), EetEnvironment.Playground);
        var result = await client.SendRevenueAsync(TestData.CreateRecord("CZ683555118"));

        Assert.That(result.IsSuccess, Is.True, result.ResponseXml);
        Assert.That(result.Warnings.Select(w => w.Code), Does.Contain(1), result.ResponseXml);
    }

    [Test]
    public async Task TimeoutWorks()
    {
        using var httpClient = new HttpClient { Timeout = TimeSpan.FromMilliseconds(1) };
        var client = new EetClient(httpClient, await Playground.GetCertificateAsync("CZ00000019"), EetEnvironment.Playground);
        Assert.ThrowsAsync<TaskCanceledException>(() => client.SendRevenueAsync(TestData.CreateRecord("CZ00000019")));
    }
}
