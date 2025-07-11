using Mews.Fiscalizations.Fiskaly.APIClients;
using Mews.Fiscalizations.Fiskaly.Models;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.SignES;

[TestFixture]
public class OffLineQRCodeTests
{
    private SignESApiClient _signEsApiClient;

    
    [Test]
    public void CreateOffLineQRCodeContentUrl()
    {
       var billDate = new DateTime(2023, 10, 1, 12, 0, 0);
        var qrContentString =  _signEsApiClient.GetOfflineQRCodeContent("A12345678", "1", "I", billDate, 100.00m);
        var expectedContentString = $"https://prewww2.aeat.es/wlpl/TIKE-CONT/ValidarQR?nif=A12345678&numserie=I1&fecha={billDate:yyyy-MM-dd}&importe=100.00";
        Assert.That(qrContentString, Is.Not.Null);
        Assert.That(qrContentString, Does.Contain("nif=A12345678"));
        Assert.That(qrContentString, Does.Contain("I1"));
        Assert.That(qrContentString, Is.EqualTo(expectedContentString));
        
    }
    
    [OneTimeSetUp]
    public void SetUp()
    {
        var httpClient = new HttpClient();
        _signEsApiClient = new SignESApiClient(httpClient, FiskalyEnvironment.Test, TestFixture.SignESApiKey, TestFixture.SignESApiSecret);
    }
}