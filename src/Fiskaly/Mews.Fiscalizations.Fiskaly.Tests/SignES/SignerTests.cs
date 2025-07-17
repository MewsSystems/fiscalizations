using Mews.Fiscalizations.Fiskaly.APIClients;
using Mews.Fiscalizations.Fiskaly.Models;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayers;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.SignES;

[TestFixture]
public class SignerTests
{
    private SignESApiClient _signEsApiClient;
    private AccessToken _authToken;

    [Test]
    [CancelAfter(1000)]
    [Ignore("Because taxpayer is disabled.")]
    public async Task CreateSignerSuccessAsync(CancellationToken token)
    {
        var signerId = Guid.NewGuid();
        var signerResult = await _signEsApiClient.CreateSignerAsync(_authToken, signerId, token);
        Assert.That(signerResult.IsSuccess);
    }
    
    [Test]
    [CancelAfter(1000)]
    public async Task GetAllSignersSuccessAsync(CancellationToken token)
    {
        var signersResult = await _signEsApiClient.GetAllSignersAsync(_authToken, token);
        Assert.That(signersResult.IsSuccess);
    }
    
    [Test]
    [Ignore("Because disabled signers can not be reactivated.")]
    [CancelAfter(1000)]
    public async Task DisableSignerSuccessAsync(CancellationToken token)
    {
        // Given
        var signerId = Guid.NewGuid();
        var createSignerResult = await _signEsApiClient.CreateSignerAsync(_authToken, signerId, token);
        
        // When
        var signersResult = await _signEsApiClient.GetAllSignersAsync(_authToken, token);
        
        // Then
        Assert.That(createSignerResult.IsSuccess);
        Assert.That(signersResult.IsSuccess);
        Assert.That(signersResult.SuccessResult, Has.Member(createSignerResult.SuccessResult.Id));
    }

    [OneTimeSetUp]
    public async Task SetUpAsync()
    {
        var httpClient = new HttpClient();
        _signEsApiClient = new SignESApiClient(httpClient, FiskalyEnvironment.Test, TestFixture.SignESApiKey, TestFixture.SignESApiSecret);
        
        _authToken = (await _signEsApiClient.GetAccessTokenAsync()).SuccessResult;
        
        await _signEsApiClient.CreateTaxpayerAsync(
            _authToken,
            "Test taxpayer",
            "B12345678",
            new TaxpayerAddress(
                Municipality: "Madrid",
                City: "Madrid",
                Street: "Calle de Test",
                PostalCode: "28001",
                Number: "1",
                Country: "ES"
            ),
            TaxpayerTerritory.SpainOther
        );
    }
    
    [OneTimeTearDown]
    public async Task TearDownAsync()
    {
        var signersResult = await _signEsApiClient.GetAllSignersAsync(_authToken);

        if (signersResult.IsSuccess)
        {
            foreach (var signer in signersResult.SuccessResult)
            {
                await _signEsApiClient.DisableSignerAsync(_authToken, signer.Id);
            }
        }
    }
}