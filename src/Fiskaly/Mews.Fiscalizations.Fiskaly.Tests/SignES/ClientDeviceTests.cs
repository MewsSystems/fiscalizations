using Mews.Fiscalizations.Fiskaly.APIClients;
using Mews.Fiscalizations.Fiskaly.Models;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayers;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.SignES;

[TestFixture]
public class ClientDeviceTests
{
    private SignESApiClient _signEsApiClient;
    private AccessToken _authToken;

    [Test]
    [CancelAfter(1000)]
    [Ignore("Because taxpayer is disabled.")]
    public async Task CreateClientDeviceSuccessAsync(CancellationToken token)
    {
        var clientId = Guid.NewGuid();
        var signerResult = await _signEsApiClient.CreateClientAsync(_authToken, clientId, token);
        Assert.That(signerResult.IsSuccess);
    }
    
    [Test]
    [CancelAfter(1000)]
    public async Task GetAllClientDevicesSuccessAsync(CancellationToken token)
    {
        var responseResult = await _signEsApiClient.GetAllClientsAsync(_authToken, token);
        Assert.That(responseResult.IsSuccess);
    }
    
    [Test]
    [Ignore("Because disabled client devices can not be reactivated.")]
    [CancelAfter(1000)]
    public async Task DisableClientDevicesSuccessAsync(CancellationToken token)
    {
        // Given
        var clientId = Guid.NewGuid();
        var createSignerResult = await _signEsApiClient.CreateClientAsync(_authToken, clientId, token);
        
        // When
        var clientDevicesResult = await _signEsApiClient.GetAllClientsAsync(_authToken, token);
        
        // Then
        Assert.That(createSignerResult.IsSuccess);
        Assert.That(clientDevicesResult.IsSuccess);
        Assert.That(clientDevicesResult.SuccessResult, Has.Member(createSignerResult.SuccessResult.ClientId));
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