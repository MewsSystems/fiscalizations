using Mews.Fiscalizations.Fiskaly.APIClients;
using Mews.Fiscalizations.Fiskaly.Models;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayers;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.SignES;

[TestFixture]
public class TaxpayerTests
{
    private SignESApiClient _signEsApiClient;
    private AccessToken _authToken;
    
    [Test]
    [CancelAfter(1000)]
    public async Task CreateTaxpayerIfNotExistsSpainSuccessAsync(CancellationToken token)
    {
        var taxpayerResult = await _signEsApiClient.GetTaxpayerAsync(_authToken, token);

        if (!taxpayerResult.IsSuccess)
        {
            taxpayerResult = await _signEsApiClient.CreateTaxpayerAsync(
                _authToken,
                "Test taxpayer",
                "A12345678",
                TaxpayerTerritory.SpainOther,
                token
            );
            
            Assert.That(taxpayerResult.IsSuccess);
        }

        taxpayerResult = await _signEsApiClient.GetTaxpayerAsync(_authToken, token);
        Assert.That(taxpayerResult.IsSuccess);
    }
    
    [Test]
    [CancelAfter(1000)]
    public async Task GetTaxpayerSuccessAsync(CancellationToken token)
    {
        var taxpayerResult = await _signEsApiClient.GetTaxpayerAsync(_authToken, token);
        
        Assert.That(taxpayerResult.IsSuccess);
    }
    
    [Test]
    [CancelAfter(1000)]
    public async Task DisableTaxpayerSuccessAsync(CancellationToken token)
    {
        var taxpayerResult = await _signEsApiClient.DisableTaxpayerAsync(_authToken, token);
        
        Assert.That(taxpayerResult.IsSuccess);
    }
    
    [OneTimeSetUp]
    public async Task SetUpAsync()
    {
        var httpClient = new HttpClient();
        _signEsApiClient = new SignESApiClient(httpClient, FiskalyEnvironment.Test, TestFixture.SignESApiKey, TestFixture.SignESApiSecret);
        
        _authToken = (await _signEsApiClient.GetAccessTokenAsync()).SuccessResult;
    }
    
    [OneTimeTearDown]
    public async Task TearDownAsync()
    {
        var taxpayerResult = await _signEsApiClient.GetTaxpayerAsync(_authToken);
        if (taxpayerResult.IsSuccess)
        {
            await _signEsApiClient.DisableTaxpayerAsync(_authToken);
        }
    }
    
}