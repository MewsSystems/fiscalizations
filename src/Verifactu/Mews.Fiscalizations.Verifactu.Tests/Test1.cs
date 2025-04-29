using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Verifactu.Models;
using NUnit.Framework;

namespace Mews.Fiscalizations.Verifactu.Tests;

[TestFixture]
public class Test1
{
    [Test]
    public async Task Test1Async()
    {
        var httpClient = new HttpClient();
        var signEsApiClient = new SignESApiClient(httpClient, FiskalyEnvironment.Test, "", "");
        var accessTokenResult = (await signEsApiClient.GetAccessTokenAsync()).Success.Get();

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

        var signerById = await signEsApiClient.GetSignerByIdAsync(accessTokenResult, signerResult.Success.Get().Id);
        Assert.That(signerById.IsSuccess);

        var allSigners = await signEsApiClient.GetAllSignersAsync(accessTokenResult);
        Assert.That(allSigners.IsSuccess);

        var clientResult = await signEsApiClient.CreateClientAsync(accessTokenResult);
        Assert.That(clientResult.IsSuccess);

        var clientById = await signEsApiClient.GetClientByIdAsync(accessTokenResult, clientResult.Success.Get().ClientId);
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
                    TaxData: new ItemTaxData(new UntaxedItem(TaxExemptionReason.OtherGrounds))
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
                        TaxData: new ItemTaxData(new UntaxedItem(TaxExemptionReason.OtherGrounds))
                    )
                ]
            ),
            Receivers:
            [
                Receiver.CreateLocal("local", "A12345678", "Barcelona, Spain", "12345")
            ]
        );

        var invoice2 = await signEsApiClient.SendCompleteInvoiceAsync(accessTokenResult, completeLocalInvoice, clientResult.Success.Get().ClientId, Guid.NewGuid());
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
                        TaxData: new ItemTaxData(new UntaxedItem(TaxExemptionReason.OtherGrounds))
                    )
                ]
            ),
            Receivers:
            [
                Receiver.CreateForeign("local", ForeignerDocumentType.Passport, "A12345678", Countries.Germany, "Berlin, Germany", "12345")
            ]
        );

        var invoice3 = await signEsApiClient.SendCompleteInvoiceAsync(accessTokenResult, completeForeignInvoice, clientResult.Success.Get().ClientId, Guid.NewGuid());
        Assert.That(invoice3.IsSuccess);
    }
}