﻿using System.Security.Cryptography.X509Certificates;
using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Spain.Model;
using Mews.Fiscalizations.Spain.Model.Request;
using Mews.Fiscalizations.Spain.Nif;
using NUnit.Framework;

namespace Mews.Fiscalizations.Spain.Tests.IssuedInvoices;

public class Basics
{
    private static readonly X509Certificate2 Certificate = new(
        rawData: Convert.FromBase64String(System.Environment.GetEnvironmentVariable("spanish_certificate_data") ?? "INSERT_CERTIFICATE_DATA"),
        password: System.Environment.GetEnvironmentVariable("spanish_certificate_password") ?? "INSERT_CERTIFICATE_PASSWORD",
        keyStorageFlags: X509KeyStorageFlags.DefaultKeySet
    );

    private static readonly Client Client = new(Certificate, Environment.Test, httpTimeout: TimeSpan.FromSeconds(30));

    private static readonly LocalCounterParty Issuer = LocalCounterParty.Create(
        name: Name.CreateUnsafe("Issuing company"),
        nifVat: System.Environment.GetEnvironmentVariable("spanish_issuer_tax_number") ?? "INSERT_ISSUER_TAX_NUMBER"
    ).Success.Get();

    private static readonly LocalCounterParty ReceivingCompany = LocalCounterParty.Create(
        name: Name.CreateUnsafe("Receiving company"),
        nifVat: System.Environment.GetEnvironmentVariable("spanish_receiver_tax_number") ?? "INSERT_RECEIVER_TAX_NUMBER"
    ).Success.Get();

    private static readonly TaxExemptItem[] UntaxedItems =
    [
        new( amount: Amount.Create(20m).Success.Get(), cause: CauseOfExemption.OtherGrounds)
    ];

    private static readonly TaxRateSummary[] TaxedItems =
    [
        GetTaxRateSummary(vat: 21m, baseValue: 42.07M)
    ];

    private static readonly TaxRateSummary[] InvalidTaxedItems =
    [
        GetTaxRateSummary(vat: 0, baseValue: 42.07M)
    ];

    private const int RetryCount = 3;

    [Test]
    [Retry(RetryCount)]
    public async Task CheckNif()
    {
        var goodEntries = NonEmptyEnumerable.Create(
            new NifInfoEntry(Issuer.TaxpayerIdentificationNumber, Issuer.Name.Value),
            new NifInfoEntry(ReceivingCompany.TaxpayerIdentificationNumber, ReceivingCompany.Name.Value),
            new NifInfoEntry(Issuer.TaxpayerIdentificationNumber, "Wrong company name") // surprisingly, good company ID with bad company name is found
        );
        var badEntries = NonEmptyEnumerable.Create(
            new NifInfoEntry(TaxpayerIdentificationNumber.Create(Countries.Spain, "99999999R").Success.Get(), "Not Juan"),
            new NifInfoEntry(TaxpayerIdentificationNumber.Create(Countries.Spain, "12999999R").Success.Get(), "Non existent name for non existent ID.")
        );

        await AssertNifLookup(goodEntries, NifSearchResult.Found);
        await AssertNifLookup(badEntries, NifSearchResult.NotFound);

        // Surprisingly, this works for some reason.
        var serverModifiedEntry = new NifInfoEntry(TaxpayerIdentificationNumber.Create(Countries.Spain, "A08433179").Success.Get(), "Microsoft test company");
        await AssertNifLookup(serverModifiedEntry.ToEnumerable(), NifSearchResult.NotFoundBecauseNifModifiedByServer);
    }

    [Test]
    [Retry(RetryCount)]
    public async Task PostInvoice_WithValidData_Succeeds()
    {
        await PostInvoice(GetInvoice(issuer: Issuer, taxRateSummaries: TaxedItems, taxExemptItems: UntaxedItems), RegisterResult.Correct);
    }

    [Test]
    [Retry(RetryCount)]
    public async Task PostZeroVatInvoice_WithValidData_Succeeds()
    {
        await PostInvoice(GetInvoice(issuer: Issuer, taxExemptItems: UntaxedItems), RegisterResult.Correct);
    }

    [Test]
    [Retry(RetryCount)]
    public async Task PostingZeroVatItemsAsTaxedItems_Fails()
    {
        var invoice = GetInvoice(issuer: Issuer, taxRateSummaries: InvalidTaxedItems);
        await PostInvoice(invoice, RegisterResult.AllIncorrect);
    }

    private async Task PostInvoice(SimplifiedInvoice invoice, RegisterResult expectedResult)
    {
        var model = SimplifiedInvoicesToSubmit.Create(
            header: new Header(Issuer, CommunicationType.Registration),
            invoices: [invoice]
        ).Success.Get();

        var response = await Client.SubmitSimplifiedInvoiceAsync(model);

        var responseErrorMessages = response.Success.Get().Invoices.Select(i => i.ErrorMessage).Flatten();
        var errorMessage = String.Join(System.Environment.NewLine, responseErrorMessages);
        Assert.That(response.Success.Get().Result, Is.EqualTo(expectedResult), errorMessage);
    }

    private async Task AssertNifLookup(INonEmptyEnumerable<NifInfoEntry> entries, NifSearchResult expectedResult)
    {
        var validator = new NifValidator(Certificate, httpTimeout: TimeSpan.FromSeconds(30));
        var response = await validator.CheckNif(new Request(entries));

        Assert.That(response.Success.Get().Results.Count(), Is.EqualTo(entries.Count));
        foreach (var result in response.Success.Get().Results)
        {
            Assert.That(expectedResult, Is.EqualTo(result.Result));
        }
    }

    private SimplifiedInvoice GetInvoice(LocalCounterParty issuer, TaxRateSummary[] taxRateSummaries = null, TaxExemptItem[] taxExemptItems = null)
    {
        var nowUtc = DateTime.UtcNow;
        var issueDateUtc = nowUtc.Date;
        var invoiceNumber = $"Bill-{Guid.NewGuid()}";

        return new SimplifiedInvoice(
            taxPeriod: new TaxPeriod(Year.Create(issueDateUtc.Year).Success.Get(), (Month)(issueDateUtc.Month - 1)),
            id: new InvoiceId(issuer.TaxpayerIdentificationNumber, String1To60.CreateUnsafe(invoiceNumber), issueDateUtc),
            taxMode: TaxMode.GeneralTaxRegimeActivity,
            description: String0To500.CreateUnsafe("This is a test invoice."),
            taxBreakdown: new TaxBreakdown(TaxSummary.Create(taxExempt: taxExemptItems, taxed: taxRateSummaries).Success.Get()),
            issuedByThirdParty: true
        );
    }

    private static TaxRateSummary GetTaxRateSummary(decimal vat, decimal baseValue)
    {
        return new TaxRateSummary(Percentage.Create(vat).Success.Get(), Amount.Create(baseValue).Success.Get(), Amount.Create(Math.Round(baseValue * vat / 100, 2)).Success.Get());
    }
}