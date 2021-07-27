<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" src="https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png">
    </a>
    <br><br>
    <b>Mews.Fiscalizations.Italy</b> is a .NET library that was built to help reporting of e-invoices to the Italian authorities (SDI - Sistema di Interscambio) using <a href="https://www.uniwix.com/">Uniwix API.</a>.
    <br><br>
    <a href="https://www.nuget.org/packages/Mews.Fiscalizations.Italy/">
        <img src="https://img.shields.io/nuget/v/Mews.Fiscalizations.Italy">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE">
        <img src="https://img.shields.io/github/license/MewsSystems/fiscalizations">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-italy-windows.yml">
        <img src="https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Italy%20(Windows)/master?label=windows%20build">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-italy-linux.yml">
        <img src="https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Italy%20(Linux)/master?label=linux%20build">
    </a>
</p>

## 📃 Description

A client library for reporting invoices through SDI (Sistema di interscambio). Here are the main parts of the library:
- **SDI Client** that handles communication with the SDI.
- **DTOs** that can be serialized into XML conforming to the FatturaPA format (the official format in which all invoices need to be reported).
- **DTOs** for handling messages sent by the SDI.

## ⚙️ Installation

The library can be installed through NuGet packages or the command line as mentioned below:
```bash
Install-Package Mews.Fiscalizations.Italy
```

## 🎯 Features

-   Functional approach via [FuncSharp](https://github.com/siroky/FuncSharp).
-   No Italian abbreviations.
-   Early data validation.
-   Asynchronous I/O.
-   All endpoints are covered with tests.
-   Intuitive immutable DTOs.
-   Cross platform (uses .NET Standard).

## 📦 NuGet

We have published the library as [Mews.Fiscalizations.Italy](https://www.nuget.org/packages/Mews.Fiscalizations.Italy/).

## 🔐 Security protocol
- TLS 1.0 protocol must be enabled, that can be achieved by adding the following line to your code:
```csharp
ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls;
```

## 👀 Code Examples
Listed below are some of the common examples. If you want to see more code examples, please check the [Tests](https://github.com/MewsSystems/fiscalizations/tree/master/src/Italy/Mews.Fiscalizations.Italy.Tests).

Uniwix Client can be created using the **Username** and **Password** which can be created through Uniwix website.

1. Invoices can be reported to the SDI using ```SendInvoiceAsync``` API which requires the ```ElectronicInvoice``` (the invoice to be reported) as a parameter.
2. Invoice can be retrieved using ```SendInvoiceAsync``` API which requires the ```fileId``` of the invoice that was already submitted as a parameter.
3. It is possible to confirm that the credentials are valid using ```VerifyCredentialsAsync``` API which would return a flag that indicates if the credentials are valid or not.

**Create Electronic invoice**

```csharp
var invoice = new ElectronicInvoice
{
    Version = VersioneSchemaType.FPR12,
    Header = invoiceHeader,
    Body = new[] { invoiceBody }
};
```

**Create invoice header**

```csharp
var header = new ElectronicInvoiceHeader
{
    TransmissionData = new TransmissionData
    {
        SequentialNumber = "1",
        DestinationCode = "1234567",
        TransmitterId = senderId,
        TransmissionFormat = TransmissionFormat.FPR12,
    },
    Provider = new Provider
    {
        IdentificationData = new IdentificationData
        {
            VatTaxId = senderId,
            Identity = new Identity
            {
                CompanyName = "Italian company ltd."
            },
            FiscalRegime = FiscalRegime.Ordinary
        },
        OfficeAddress = address
    },
    Buyer = new Buyer
    {
        IdentityData = new SimpleIdentityData
        {
            Identity = new Identity
            {
                FirstName = "John",
                LastName = "Smith"
            },
            TaxCode = "SDASDA96L27H501H"
        },
        OfficeAddress = address
    }
};
```

**Create sender id**

```csharp
var senderId = new SenderId
{
    CountryCode = Countries.Italy.Alpha2Code,
    TaxCode = "1234567"
};
```

**Create address**

```csharp
var address = new Address
{
    Street = "Roma Street",
    City = "Rome",
    CountryCode = Countries.Italy.Alpha2Code,
    ProvinceCode = "RM",
    Zip = "00031"
};
```

**Create invoice body**

```csharp
var invoiceBody = new ElectronicInvoiceBody
{
    GeneralData = new GeneralData
    {
        GeneralDocumentData = new GeneralDocumentData
        {
            DocumentType = DocumentType.Invoice,
            CurrencyCode = "EUR",
            IssueDate = DateTime.UtcNow,
            DocumentNumber = "1",
            TotalAmount = 100m
        }
    },
    ServiceData = new ServiceData
    {
        InvoiceLines = new[] { invoiceLine },
        TaxSummary = new[] { taxSummary }
    },
    PaymentData = new[] { paymentData }
};
```

**Create invoice line**

```csharp
var invoiceLine = new InvoiceLine
{
    LineNumber = "1",
    Description = "Item 1",
    UnitCount = 1m,
    PeriodStartingDate = DateTime.UtcNow,
    PeriodClosingDate = DateTime.UtcNow,
    UnitPrice = 100m,
    TotalPrice = 100m,
    VatRate = 10m
};
```

**Create tax rate summary**

```csharp
var taxSummary = new TaxRateSummary
{
    VatRate = 10m,
    TaxAmount = 9m,
    TaxableAmount = 90m,
    VatDueDate = VatDueDate.Immediate
};
```

**Create payment data**

```csharp
var paymentData = new PaymentData
{
    PaymentDetails = new [] { paymentDetail },
    PaymentTerms = PaymentTerms.LumpSum
};
```

**Create payment detail**

```csharp
var paymentDetail = new PaymentDetail
{
    PaymentMethod = PaymentMethod.Cash,
    PaymentAmount = 100m
};
```