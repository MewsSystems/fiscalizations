<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" src="https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png">
    </a>
    <br><br>
    <b>Mews.Fiscalizations.Austria</b> A .NET library for generating QR codes that comply with Austrian fiscal law (RKSV2017), featuring A-Trust WS RK.Online API-based signing.
    <b>Current supported version: 2.0</b>   
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

The `Mews.Fiscalizations.Austria` library leverages [A-Trust](https://www.a-trust.at/de/Registrierkasse/) services to create QR codes compliant with Austrian fiscal regulations. For further details, refer to A-Trust’s [Documentation](https://labs.a-trust.at/developer/pdf/asignRKHSM_basic_advanced_premium.pdf).

## ⚙️ Installation

You can install the library via NuGet Package Manager or the command line:
```bash
Install-Package Mews.Fiscalizations.Austria
```

## 🎯 Key Features

-   Functional programming approach using [FuncSharp](https://github.com/MewsSystems/FuncSharp).
-   Early validation of data inputs.
-   Asynchronous I/O operations.
-   Comprehensive test coverage for all endpoints.
-   Intuitive and immutable Data Transfer Objects (DTOs).
-   CI/CD pipelines compatible with both Windows and Linux.
-   Cross-platform compatibility (.NET 8).

## 📦 NuGet Package

Available on NuGet as [Mews.Fiscalizations.Austria](https://www.nuget.org/packages/Mews.Fiscalizations.Austria/).

## 👀 Code Examples

Below are some sample use cases. For a full list, see the [test cases](https://github.com/MewsSystems/fiscalizations/tree/master/src/Austria/Mews.Fiscalizations.Austria.Tests).

### Generating QR Data for Receipts

Create QrData from a receipt, including standard rates, register identifier, and previous JWS representation:
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

### Using the A-Trust RK.Online Signer
Sign the generated QR data using A-Trust’s API:
```csharp
var signer = new ATrustSigner(Credentials, ATrustEnvironment.Test);
var output = await signer.SignAsync(qrData);
```

### Handling Signer Output
Use `SignerOutput` for setting fiscal record details:
```csharp
fiscalRecord.JwsRepresentation = output.JwsRepresentation.Value;
fiscalRecord.QrData = output.SignedQrData.Value;
fiscalRecord.Signature = output.JwsRepresentation.Signature.Value;
```

### ⚠️ License

Licensed under the [MIT](https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE)
