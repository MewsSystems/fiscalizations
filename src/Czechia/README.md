<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" src="https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png">
    </a>
    <br><br>
    <b>Mews.Fiscalizations.Czechia (EET)</b> is a .NET library that was built to help reporting of e-invoices to the Czech authorities (EET - Elektronická Evidence Tržeb).
    <b>Current supported version is 3.0.</b>
    <br><br>
    <a href="https://www.nuget.org/packages/Mews.Fiscalizations.Czechia/">
        <img src="https://img.shields.io/nuget/v/Mews.Fiscalizations.Czechia">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE">
        <img src="https://img.shields.io/github/license/MewsSystems/fiscalizations">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/publish-czechia.yml">
        <img src="https://img.shields.io/github/actions/workflow/status/MewsSystems/fiscalizations/publish-czechia.yml?branch=master&label=publish">
    </a>
    <a href="https://www.etrzby.cz/assets/cs/prilohy/EET_pristupove_provozni_informace_playground_3.1.pdf">
        <img src="https://img.shields.io/badge/v3.0-EET-lightgrey">
    </a>
</p>

## ❗ Please be aware that the EET fiscalization has been deprecated since the 1st of January 2023. [Official announcement](https://www.mfcr.cz/cs/aktualne/tiskove-zpravy/2022/poslanecka-snemovna-schvalila-uplne-zrus-46800/)

## 📃 Description

EET stands for Elektronická Evidence Tržeb, which is the Czech version of Fiscal Printers.
It's an online API provided by the Ministry of Finance in a form of a SOAP Web Service.

## ⚙️ Installation

The library can be installed through NuGet packages or the command line as mentioned below:
```bash
Install-Package Mews.Fiscalizations.Czechia
```

## 🎯 Features

-   Functional approach via [FuncSharp](https://github.com/siroky/FuncSharp).
-   No Czech abbreviations.
-   Early data validation.
-   Asynchronous I/O.
-   All endpoints are covered with tests.
-   Intuitive immutable DTOs.
-   Cross platform (uses .NET Standard).
-   Logging support
-   SOAP communication (including WS-Security signing).
-   [PKP](doc/data.md) and [BKP](doc/data.md) security code computation.

## ❗ Known issues
- [8](https://github.com/MewsSystems/eet/issues/8): As the communication is done fully via HTTPS, we postponed the implementation of response signature verification. It's a potential security risk that will be addressed in upcoming releases.

## 🔐 Security protocol
- TLS 1.2 protocol must be enabled, that can be achieved by adding the following line to your code:
```csharp
ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12
```
## 📦 NuGet

We have published the library as [Mews.Fiscalizations.Czechia](https://www.nuget.org/packages/Mews.Fiscalizations.Czechia/).

## 👀 Code Examples

Listed below are some of the common examples. If you want to see more code examples, please check the [Tests](https://github.com/MewsSystems/fiscalizations/tree/master/src/Czechia/Mews.Fiscalizations.Czechia.Tests).

```csharp
var certificate = new Certificate(
    password: "certificatePassword",
    data: certificateContentsByteArray
);

var record = new RevenueRecord(
    identification: new Identification(
        taxPayerIdentifier: new TaxIdentifier("CZ1234567890"),
        registryIdentifier: new RegistryIdentifier("01"),
        premisesIdentifier: new PremisesIdentifier(1),
        certificate: certificate
    ),
    revenue: new Revenue(
        gross: new CurrencyValue(1234.00m)
    ),
    billNumber: new BillNumber("2016-321")
);

var securityCode = record.SecurityCode; // BKP
var signature = record.Signature; // PKP

var client = new EetClient(certificate);
var response = await client.SendRevenueAsync(record);
if (response.IsSuccess)
{
    var fiscalCode = response.Success.FiscalCode;
}
```

- More examples are presented [here](doc/examples.md).
- Some of the data items are explained [here](doc/data.md).

## 🏭 Who uses the library in production?
- [Mews](https://mews.com) - Property Management Solution for the 21st century.
- [Hlídač EET](http://hlidaceet.cz) - Watchdog of the EET API endpoint and related services.
- Keszh, Neszh - [Hart](http://hartphp.com.pl/) Internal ERP System
- [Ankerdata EasiPOS](http://easipos.ankerdata.com/) - POS system
- [sySOft](http://www.sysoft.cz/) - POS system

## 👍 Credits
Development: [@jirihelmich](https://github.com/jirihelmich)

Code review: [@siroky](https://github.com/siroky), [@onashackem](https://github.com/onashackem)

Contribution: [@tomasdeml](https://github.com/tomasdeml): [PR#3](https://github.com/MewsSystems/eet/pull/3/files)
