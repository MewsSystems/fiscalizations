<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" src="https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png">
    </a>
    <br><br>
    <b>Mews.Fiscalizations.Austria</b> is a .NET library that allows you to generate QR codes compliant with Austrian fiscal law (RKSV2017). Currently offers a signer based on A-Trust WS RK.Online API.
    <b>Current supported version is 2.0.</b>
    <br><br>
    <a href="https://www.nuget.org/packages/Mews.Fiscalizations.Austria/">
        <img src="https://img.shields.io/nuget/v/Mews.Fiscalizations.Austria">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE">
        <img src="https://img.shields.io/github/license/MewsSystems/fiscalizations">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/publish-austria.yml">
        <img src="https://img.shields.io/github/actions/workflow/status/MewsSystems/fiscalizations/publish-austria.yml?branch=master&label=publish">
    </a>
    <a href="https://labs.a-trust.at/developer/pdf/asignRKHSM_basic_advanced_premium.pdf">
        <img src="https://img.shields.io/badge/v2.0-registrierkasse-lightgrey">
    </a>
</p>

## 📃 Description

This library uses the [A-Trust](https://www.a-trust.at/de/Registrierkasse/) to generate the QR codes, please check their [Documentation](https://labs.a-trust.at/developer/pdf/asignRKHSM_basic_advanced_premium.pdf).

## ⚙️ Installation

The library can be installed through NuGet packages or the command line as mentioned below:
```bash
Install-Package Mews.Fiscalizations.Austria
```

## 🎯 Features

-   Functional approach via [FuncSharp](https://github.com/siroky/FuncSharp).
-   No Austrian abbreviations.
-   Early data validation.
-   Asynchronous I/O.
-   All endpoints are covered with tests.
-   Intuitive immutable DTOs.
-   Pipelines that run on both Windows and Linux operating systems.
-   Cross platform (uses .NET 6).

## 📦 NuGet

We have published the library as [Mews.Fiscalizations.Austria](https://www.nuget.org/packages/Mews.Fiscalizations.Austria/).

## 👀 Code Examples

Listed below are some of the common examples. If you want to see more code examples, please check the [Tests](https://github.com/MewsSystems/fiscalizations/tree/master/src/Austria/Mews.Fiscalizations.Austria.Tests).

### Creating QR data from bills
```csharp
var qrData = new QrData(new Receipt(
    number: new ReceiptNumber("83469"),
    registerIdentifier: new RegisterIdentifier("DEMO-CASH-BOX817"),
    taxData: new TaxData(
        standartRate: new CurrencyValue(29.73m),
        lowerReducedRate: new CurrencyValue(36.41m),
        specialRate: new CurrencyValue(21.19m)
    ),
    turnover: new CurrencyValue(0.0m), 
    certificateSerialNumber: new CertificateSerialNumber("-3667961875706356849"),
    previousJwsRepresentation: new JwsRepresentation("..."),
    created: new LocalDateTime(
        new DateTime(2015, 11, 25, 19, 20, 11),
        LocalDateTime.AustrianTimezone
    )
);
```

### Using A-Trust RK.Online signer
```csharp
var signer = new ATrustSigner(Credentials, ATrustEnvironment.Test);
var output = signer.Sign(qrData);
```

### SignerOutput Usage
```csharp
fiscalRecord.JwsRepresentation = output.JwsRepresentation.Value;
fiscalRecord.QrData = output.SignedQrData.Value;
fiscalRecord.Signature = output.JwsRepresentation.Signature.Value;
```

## 🔐 Security protocol
- TLS 1.1 and TLS 1.2 protocols must be enabled, that can be achieved by adding the following line to your code:
```csharp
ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11
```
