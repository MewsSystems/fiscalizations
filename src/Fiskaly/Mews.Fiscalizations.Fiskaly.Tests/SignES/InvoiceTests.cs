using Mews.Fiscalizations.Fiskaly.APIClients;
using Mews.Fiscalizations.Fiskaly.Models;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayers;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.SignES;

[TestFixture]
public class InvoiceTests
{
    private SignESApiClient _signEsApiClient;
    private AccessToken _authToken;
    private Guid _clientId;
    
    [Test]
    [CancelAfter(1000)]
    [Ignore("Ignore test because Taxpayer is disabled")]
    public async Task CreateSimplifiedInvoiceSuccessfulAsync(CancellationToken token)
    {
        var SimplifiedInvoice = new SimplifiedInvoice(
            InvoiceNumber: "4",
            InvoiceDescription: "Test invoice",
            FullAmount: 100,
            Items:
            [
                new InvoiceItem(
                    ItemDescription: "Test item",
                    Quantity: 1,
                    UnitAmount: 100,
                    FullAmount: 100,
                    TaxExemptionReason: TaxExemptionReason.OtherGrounds,
                    TaxRate: null
                )
            ],
            DateTime.UtcNow
        );
        var invoice = await _signEsApiClient.SendSimplifiedInvoiceAsync(_authToken, SimplifiedInvoice, _clientId, Guid.NewGuid(), token);
        Assert.That(invoice.IsSuccess);
    }
    
    [Test]
    [CancelAfter(1000)]
    [Ignore("Ignore test because Taxpayer is disabled")]
    public async Task CreateCompleteInvoiceSuccessfulAsync(CancellationToken token)
    {
        var completeLocalInvoice = new CompleteInvoice(
            SimplifiedInvoice: new SimplifiedInvoice(
                InvoiceNumber: "5",
                InvoiceDescription: "Test invoice",
                FullAmount: 1000,
                Items:
                [
                    new InvoiceItem(
                        ItemDescription: "Test item",
                        Quantity: 1,
                        UnitAmount: 1000,
                        FullAmount: 1000,
                        TaxExemptionReason: TaxExemptionReason.OtherGrounds,
                        TaxRate: null
                    )
                ],
                DateTime.UtcNow
            ),
            Receivers:
            [
                Receiver.CreateLocal("local", "A12345678", "Barcelona, Spain", "12345")
            ]
        );

        var responseResult = await _signEsApiClient.SendCompleteInvoiceAsync(_authToken, completeLocalInvoice, _clientId, Guid.NewGuid(), token);
        Assert.That(responseResult.IsSuccess);
    }
    
    [Test]
    [CancelAfter(1000)]
    [Ignore("Ignore test because Taxpayer is disabled")]
    public async Task CreateCompleteInvoiceWithForeignDocumentSuccessfulAsync(CancellationToken token)
    {
        var completeForeignInvoice = new CompleteInvoice(
            SimplifiedInvoice: new SimplifiedInvoice(
                InvoiceNumber: "6",
                InvoiceDescription: "Test invoice",
                FullAmount: 1000,
                Items:
                [
                    new InvoiceItem(
                        ItemDescription: "Test item",
                        Quantity: 1,
                        UnitAmount: 1000,
                        FullAmount: 1000,
                        TaxExemptionReason: TaxExemptionReason.OtherGrounds,
                        TaxRate: null
                    )
                ],
                DateTime.UtcNow
            ),
            Receivers:
            [
                Receiver.CreateForeign("foreign", ForeignerDocumentType.Passport, "A12345678", "DE", "Berlin, Germany", "12345")
            ]
        );

        var responseResult = await _signEsApiClient.SendCompleteInvoiceAsync(_authToken, completeForeignInvoice, _clientId, Guid.NewGuid(), token);
        Assert.That(responseResult.IsSuccess);
    }
    
    [Test]
    [CancelAfter(1000)]
    [Ignore("Ignore test because Taxpayer is disabled")]
    public async Task CancelInvoiceSuccessfulAsync(CancellationToken token)
    {
        var completeForeignInvoice = new CompleteInvoice(
            SimplifiedInvoice: new SimplifiedInvoice(
                InvoiceNumber: "6",
                InvoiceDescription: "Test invoice",
                FullAmount: 1000,
                Items:
                [
                    new InvoiceItem(
                        ItemDescription: "Test item",
                        Quantity: 1,
                        UnitAmount: 1000,
                        FullAmount: 1000,
                        TaxExemptionReason: TaxExemptionReason.OtherGrounds,
                        TaxRate: null
                    )
                ],
                DateTime.UtcNow
            ),
            Receivers:
            [
                Receiver.CreateForeign("foreign", ForeignerDocumentType.Passport, "A12345678", "DE", "Berlin, Germany", "12345")
            ]
        );

        var createdInvoice = await _signEsApiClient.SendCompleteInvoiceAsync(_authToken, completeForeignInvoice, _clientId, Guid.NewGuid(), token);
        Assert.That(createdInvoice.IsSuccess);
        
        var canceledInvoiceResponse = await _signEsApiClient.CancelInvoiceAsync(_authToken, _clientId, createdInvoice.SuccessResult.InvoiceId, token);
        Assert.That(canceledInvoiceResponse.IsSuccess);
    }
    
    [Test]
    [CancelAfter(1000)]
    [Ignore("Ignore test because Taxpayer is disabled")]
    public async Task GetInvoiceSuccessfulAsync(CancellationToken token)
    {
        var completeForeignInvoice = new CompleteInvoice(
            SimplifiedInvoice: new SimplifiedInvoice(
                InvoiceNumber: "6",
                InvoiceDescription: "Test invoice",
                FullAmount: 1000,
                Items:
                [
                    new InvoiceItem(
                        ItemDescription: "Test item",
                        Quantity: 1,
                        UnitAmount: 1000,
                        FullAmount: 1000,
                        TaxExemptionReason: TaxExemptionReason.OtherGrounds,
                        TaxRate: null
                    )
                ],
                DateTime.UtcNow
            ),
            Receivers:
            [
                Receiver.CreateForeign("foreign", ForeignerDocumentType.Passport, "A12345678", "DE", "Berlin, Germany", "12345")
            ]
        );

        var createdInvoice = await _signEsApiClient.SendCompleteInvoiceAsync(_authToken, completeForeignInvoice, _clientId, Guid.NewGuid(), token);
        Assert.That(createdInvoice.IsSuccess);
        
        var retrievedInvoiceResponse = await _signEsApiClient.GetInvoiceAsync(_authToken, _clientId, createdInvoice.SuccessResult.InvoiceId, token);
        Assert.That(retrievedInvoiceResponse.IsSuccess);
    }
    
    [Test]
    [CancelAfter(1000)]
    [Ignore("Ignore test because Taxpayer is disabled")]
    public async Task CorrectCompleteInvoiceSuccessfulAsync(CancellationToken token)
    {
        var completeForeignInvoice = new CompleteInvoice(
            SimplifiedInvoice: new SimplifiedInvoice(
                InvoiceNumber: "6",
                InvoiceDescription: "Test invoice",
                FullAmount: 1000,
                Items:
                [
                    new InvoiceItem(
                        ItemDescription: "Test item",
                        Quantity: 1,
                        UnitAmount: 1000,
                        FullAmount: 1000,
                        TaxExemptionReason: TaxExemptionReason.OtherGrounds,
                        TaxRate: null
                    )
                ],
                DateTime.UtcNow
            ),
            Receivers:
            [
                Receiver.CreateForeign("foreign", ForeignerDocumentType.Passport, "A12345678", "DE", "Berlin, Germany", "12345")
            ]
        );

        var createdInvoice = await _signEsApiClient.SendCompleteInvoiceAsync(_authToken, completeForeignInvoice, _clientId, Guid.NewGuid(), token);
        Assert.That(createdInvoice.IsSuccess);
        
        var retrievedInvoiceResponse = await _signEsApiClient.GetInvoiceAsync(_authToken, _clientId, createdInvoice.SuccessResult.InvoiceId, token);
        Assert.That(retrievedInvoiceResponse.IsSuccess);
        
        var correctedInvoice = new CompleteInvoice(
            SimplifiedInvoice: new SimplifiedInvoice(
                InvoiceNumber: "6",
                InvoiceDescription: "Test invoice corrected",
                FullAmount: 100,
                Items:
                [
                    new InvoiceItem(
                        ItemDescription: "Test item",
                        Quantity: 1,
                        UnitAmount: 100,
                        FullAmount: 100,
                        TaxExemptionReason: TaxExemptionReason.OtherGrounds,
                        TaxRate: null
                    )
                ],
                DateTime.UtcNow
            ),
            Receivers:
            [
                Receiver.CreateForeign("foreign", ForeignerDocumentType.Passport, "A12345678", "DE", "Berlin, Germany", "12345")
            ]
        );
        
        var correctingInvoice = new CorrectingCompleteInvoice(
            InvoiceId: retrievedInvoiceResponse.SuccessResult.InvoiceId,
            Invoice: correctedInvoice
        );
        
        var correctionResponse = await _signEsApiClient.SendCorrectingCompleteInvoiceAsync(
            _authToken,
            correctingInvoice,
            _clientId,
            retrievedInvoiceResponse.SuccessResult.InvoiceId,
            token
        );
        
        Assert.That(correctionResponse.IsSuccess);
    }
    
    [Test]
    [CancelAfter(1000)]
    [Ignore("Ignore test because Taxpayer is disabled")]
    public async Task CorrectSimplifiedInvoiceSuccessfulAsync(CancellationToken token)
    {
        var SimplifiedInvoice = new SimplifiedInvoice(
                InvoiceNumber: "6",
                InvoiceDescription: "Test invoice",
                FullAmount: 1000,
                Items:
                [
                    new InvoiceItem(
                        ItemDescription: "Test item",
                        Quantity: 1,
                        UnitAmount: 1000,
                        FullAmount: 1000,
                        TaxExemptionReason: TaxExemptionReason.OtherGrounds,
                        TaxRate: null
                    )
                ],
                DateTime.UtcNow
        );

        var createdInvoice = await _signEsApiClient.SendSimplifiedInvoiceAsync(_authToken, SimplifiedInvoice, _clientId, Guid.NewGuid(), token);
        Assert.That(createdInvoice.IsSuccess);
        
        var retrievedInvoiceResponse = await _signEsApiClient.GetInvoiceAsync(_authToken, _clientId, createdInvoice.SuccessResult.InvoiceId, token);
        Assert.That(retrievedInvoiceResponse.IsSuccess);
        
        var correctedSimplifiedInvoice = new SimplifiedInvoice(
                InvoiceNumber: "6",
                InvoiceDescription: "Test invoice corrected",
                FullAmount: 100,
                Items:
                [
                    new InvoiceItem(
                        ItemDescription: "Test item",
                        Quantity: 1,
                        UnitAmount: 100,
                        FullAmount: 100,
                        TaxExemptionReason: TaxExemptionReason.OtherGrounds,
                        TaxRate: null
                    )
                ],
                DateTime.UtcNow
        );
        
        var correctingInvoice = new CorrectingSimplifiedInvoice(
            InvoiceId: retrievedInvoiceResponse.SuccessResult.InvoiceId,
            Invoice: correctedSimplifiedInvoice
        );
        
        var correctionResponse = await _signEsApiClient.SendCorrectingSimplifiedInvoiceAsync(
            _authToken,
            correctingInvoice,
            _clientId,
            retrievedInvoiceResponse.SuccessResult.InvoiceId,
            token
        );
        
        Assert.That(correctionResponse.IsSuccess);
    }
    
    [OneTimeSetUp]
    public async Task SetUpAsync()
    {
        var httpClient = new HttpClient();
        _signEsApiClient = new SignESApiClient(httpClient, FiskalyEnvironment.Test, TestFixture.SignESApiKey, TestFixture.SignESApiSecret);
        
        _authToken = (await _signEsApiClient.GetAccessTokenAsync()).SuccessResult;
        
        _clientId = (await _signEsApiClient.GetAllClientsAsync(_authToken, CancellationToken.None)).SuccessResult?.FirstOrDefault()?.ClientId ?? Guid.NewGuid();
    }
}