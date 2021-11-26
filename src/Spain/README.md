<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" src="https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png">
    </a>
    <br><br>
    <b>Mews.Fiscalizations.Spain</b> is a .NET library that was built to help reporting of e-invoices to the Spanish authorities (SII - Suministro Inmediato de Información del IVA).
    <b>Current supported version is 1.1.</b>
    <br><br>
    <a href="https://www.nuget.org/packages/Mews.Fiscalizations.Spain/">
        <img src="https://img.shields.io/nuget/v/Mews.Fiscalizations.Spain">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE">
        <img src="https://img.shields.io/github/license/MewsSystems/fiscalizations">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-spain-windows.yml">
        <img src="https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Spain%20(Windows)/master?label=windows%20build">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-spain-linux.yml">
        <img src="https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Spain%20(Linux)/master?label=linux%20build">
    </a>
    <a href="https://www.agenciatributaria.es/AEAT.internet/en_gb/SII.html">
        <img src="https://img.shields.io/badge/v1.1-SII-lightgrey">
    </a>
</p>

## 📃 Description

This library uses the [SII](https://www.agenciatributaria.es/AEAT.internet/en_gb/SII.html) to report e-invoices, please check their [Documentation](https://www.agenciatributaria.es/AEAT.internet/en_gb/SII.html).

SII stands for Suministro Inmediato de Información del IVA, which translates to Immediate Supply of Information on VAT.
It's an online API provided by Spanish authorities in a form of a SOAP Web Service.

## ⚙️ Installation

The library can be installed through NuGet packages or the command line as mentioned below:
```bash
Install-Package Mews.Fiscalizations.Spain
```

## 🎯 Features

-   Functional approach via [FuncSharp](https://github.com/siroky/FuncSharp).
-   No Spanish abbreviations.
-   Early data validation.
-   Asynchronous I/O.
-   All endpoints are covered with tests.
-   Intuitive immutable DTOs.
-   Pipelines that run on both Windows and Linux operating systems.
-   Cross platform (uses .NET Standard).
-   Logging support.

## ❗ Known issues
- We didn't implement deleting or modifying already existing records.

## 📦 NuGet

We have published the library as [Mews.Fiscalizations.Spain](https://www.nuget.org/packages/Mews.Fiscalizations.Spain/).

## 👀 Code Examples

Listed below are some of the common examples. If you want to see more code examples, please check the [Tests](https://github.com/MewsSystems/fiscalizations/tree/master/src/Spain/Mews.Fiscalizations.Spain.Tests).

**Creating the client**
There are 3 required properties that need to be provided when creating the client
1. Certificate: the certificate can be obtained from the SII website
2. Environment: production/test.
3. HttpTimeout: the timespan to wait before the request times out.

```csharp
var certificate = new X509Certificate2(.....);
var client = new Client(certificate, Environment.Test, httpTimeout: TimeSpan.FromSeconds(30));
```

**Creating the IssuingCompany (supplier/issuer)**
First step is to create the tax payer object:
```csharp
var taxpayerIdentificationNumber = TaxpayerIdentificationNumber.Create(Countries.Spain, "INSERT_ISSUER_TAX_NUMBER").Success.Get();
```

and to create the IssuingCompany object:
```csharp
var issuingCompany = new LocalCompany(
    name: Name.CreateUnsafe("Name of the issuing company"),
    taxpayerIdentificationNumber: taxpayerIdentificationNumber
);
```

**Creating the invoice**
```csharp
var issueDateUtc = nowUtc.Date;
var vat = 21m;
var baseValue = 42.07m;
var taxRateSummaries = new[] 
{
    new TaxRateSummary(
        taxRatePercentage: Percentage.Create(vat).Success.Get(),
        taxBaseAmount: Amount.Create(baseValue).Success.Get(),
        taxAmount: Amount.Create(Math.Round(baseValue * vat / 100, 2)).Success.Get()
    )
};
var taxExemptItems = new[] { new TaxExemptItem(Amount.Create(20m).Success.Get(), CauseOfExemption.OtherGrounds) };
var invoice = new SimplifiedInvoice(
    taxPeriod: new TaxPeriod(Year.Create(issueDateUtc.Year).Success.Get(), (Month)(issueDateUtc.Month - 1)),
    id: new InvoiceId(issuingCompany.TaxpayerIdentificationNumber, String1To60.CreateUnsafe("Invoice_number"), issueDateUtc),
    schemeOrEffect: SchemeOrEffect.GeneralTaxRegimeActivity,
    description: String0To500.CreateUnsafe("Invoice description."),
    taxBreakdown: new TaxBreakdown(TaxSummary.Create(taxExempt: taxExemptItems, taxed: taxRateSummaries).Success.Get()),
    issuedByThirdParty: true
);
```

**Submitting the invoice**
```csharp
var model = SimplifiedInvoicesToSubmit.Create(
    header: new Header(IssuingCompany, CommunicationType.Registration),
    invoices: new[] { invoice }
).Success.Get();

var response = await client.SubmitSimplifiedInvoiceAsync(model);
```

**Handling response result**
Client will return a coproduct of successful soap response and potential soap fault. You could read more about algebraic types [here](https://github.com/siroky/FuncSharp). Specifically, samples of handling Try coproduct are [here](https://github.com/siroky/FuncSharp/tree/master/src/FuncSharp.Examples/Try). Below is a reference of basic scenarios:

Get result or throw an exception in a case if service have returned soap fault response.
```
var receivedInvoices = response.MapError(e => new Exception($"code {e.Code} message {e.Message}")).Get();
```

Conditionally access successful response
```
if (response.IsSuccess)
{
    var soapSuccessResult = response.Success.Get();
}
```

Conditionally access fault response
```
if (response.IsError)
{
    var soapFaultResult = response.Error.Get();
}
```

## 🧑 Authors
Development: [@PavelKalandra](https://github.com/KaliCZ), [@MiroslavVeith](https://github.com/mveith)
