using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Fiskaly.APIClients;
using Mews.Fiscalizations.Fiskaly.Models;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayers;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.SignES;

[TestFixture]
public class Test1
{
    [Test]
    [Ignore("Ignore test because it creates data in the test environment.")]
    public async Task Test1Async()
    {
        var httpClient = new HttpClient();
        var signEsApiClient = new SignESApiClient(httpClient, FiskalyEnvironment.Test, "", "");
        var accessTokenResult = (await signEsApiClient.GetAccessTokenAsync()).SuccessResult;

        try
        {
            var taxpayerResult = await signEsApiClient.CreateTaxpayerAsync(
                accessTokenResult,
                "Test taxpayer",
                "A12345678",
                TaxpayerTerritory.SpainOther
            );
            Assert.That(taxpayerResult.IsSuccess);
        }
        catch (Exception)
        {
            // Ignore.
        }

        var getTaxpayer = await signEsApiClient.GetTaxpayerAsync(accessTokenResult);
        Assert.That(getTaxpayer.IsSuccess);

        var signerResult = await signEsApiClient.CreateSignerAsync(accessTokenResult);
        Assert.That(signerResult.IsSuccess);

        var signerById = await signEsApiClient.GetSignerByIdAsync(accessTokenResult, signerResult.SuccessResult.Id);
        Assert.That(signerById.IsSuccess);

        var allSigners = await signEsApiClient.GetAllSignersAsync(accessTokenResult);
        Assert.That(allSigners.IsSuccess);

        var clientResult = await signEsApiClient.CreateClientAsync(accessTokenResult);
        Assert.That(clientResult.IsSuccess);

        var clientById = await signEsApiClient.GetClientByIdAsync(accessTokenResult, clientResult.SuccessResult.ClientId);
        Assert.That(clientById.IsSuccess);

        var allClients = await signEsApiClient.GetAllClientsAsync(accessTokenResult);
        Assert.That(allClients.IsSuccess);

        var software = await signEsApiClient.GetSoftwareAuditDataAsync(accessTokenResult);
        Assert.That(software.IsSuccess);

        var simplifiedInvoice = new SimplifiedInvoice(
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
            ]
        );
        //var invoice = await fiskalyClient.SendSimplifiedInvoiceAsync(accessTokenResult, simplifiedInvoice, clientResult.Success.Get().ClientId, Guid.NewGuid());
        //Assert.That(invoice.IsSuccess);

        var completeLocalInvoice = new CompleteInvoice(
            simplifiedInvoice: new SimplifiedInvoice(
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
                ]
            ),
            Receivers:
            [
                Receiver.CreateLocal("local", "A12345678", "Barcelona, Spain", "12345")
            ]
        );

        var invoice2 = await signEsApiClient.SendCompleteInvoiceAsync(accessTokenResult, completeLocalInvoice, clientResult.SuccessResult.ClientId, Guid.NewGuid());
        Assert.That(invoice2.IsSuccess);

        var completeForeignInvoice = new CompleteInvoice(
            simplifiedInvoice: new SimplifiedInvoice(
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
                ]
            ),
            Receivers:
            [
                Receiver.CreateForeign("local", ForeignerDocumentType.Passport, "A12345678", Countries.Germany.Alpha2Code, "Berlin, Germany", "12345")
            ]
        );

        var invoice3 = await signEsApiClient.SendCompleteInvoiceAsync(accessTokenResult, completeForeignInvoice, clientResult.SuccessResult.ClientId, Guid.NewGuid());
        Assert.That(invoice3.IsSuccess);
    }
}