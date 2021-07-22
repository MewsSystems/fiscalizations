<p align="center">
    <a href="https://gofiber.io">
        <img alt="Mews" src="https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png">
    </a>
    <br><br>
    <b>Mews.Fiscalizations.Hungary</b> is a .NET library that was built to help reporting of e-invoices to the Hungarian authorities (Online Szamla - Nemzeti Adó- és Vámhivatal).
    <b>Current supported version is 3.0.</b>
    <br><br>
    <a href="https://www.nuget.org/packages/Mews.Fiscalizations.Hungary/">
        <img src="https://img.shields.io/nuget/v/Mews.Fiscalizations.Hungary">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE">
        <img src="https://img.shields.io/github/license/MewsSystems/fiscalizations">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-hungary-windows.yml">
        <img src="https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Hungary%20(Windows)/master">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-hungary-linux.yml">
        <img src="https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Hungary%20(Linux)/master">
    </a>
    <a href="https://onlineszamla.nav.gov.hu/api/files/container/download/Online%20Szamla_Interfesz%20specifikacio_EN_v3.0.pdf">
        <img src="https://img.shields.io/badge/3.0-NAV-brightgreen">
    </a>
</p>

## 📃 Description

This library uses the [NAV Online Invoice System](https://onlineszamla.nav.gov.hu) to report e-invoices, please check their [Documentation](https://onlineszamla.nav.gov.hu/dokumentaciok).

<b>Useful links:</b>
[API documentation](https://onlineszamla.nav.gov.hu/api/files/container/download/Online%20Szamla_Interfesz%20specifikacio_EN_v3.0.pdf)
[Test environment](https://onlineszamla-test.nav.gov.hu/)
[Production environment](https://onlineszamla.nav.gov.hu/)

## ⚙️ Installation

The library can be installed through NuGet packages or the command line as mentioned below:
```bash
Install-Package Mews.Fiscalizations.Hungary
```

## 🎯 Features

-   Functional approach via [FuncSharp](https://github.com/siroky/FuncSharp).
-   No Hungarian abbreviations.
-   Early data validation.
-   Asynchronous I/O.
-   All endpoints are covered with tests.
-   Intuitive immutable DTOs.
-   Pipelines that run on both Windows and Linux operating systems.
-   Cross platform (uses .NET Standard).

## 📦 NuGet

We have published the library as [Mews.Fiscalizations.Hungary](https://www.nuget.org/packages/Mews.Fiscalizations.Hungary/).

## 👀 Code Examples

Listed below are some of the common examples. If you want to see more code examples, please check the [Tests](https://github.com/MewsSystems/fiscalizations/tree/master/src/Hungary/Mews.Fiscalizations.Hungary.Tests).

**Creating NAV client**
There are 3 required properties that need to be provided when creating the NAV client
1. Technical user: technical user credentials can be obtained from the [NAV website](https://onlineszamla.nav.gov.hu) dashboard
2. Software identification: Contains information regarding the software performing data reporting
3. NAV Environment: (Test/Production)

```csharp
var technicalUser = new TechnicalUser(
    login: Login,
    password: Password,
    signingKey: SigningKey,
    taxId: TaxPayerId,
    encryptionKey: EncryptionKey
);
var softwareIdentification = new SoftwareIdentification(
    id: "123456789123456789",
    name: "Test",
    type: SoftwareType.LocalSoftware,
    mainVersion: "1.0",
    developerName: "Test",
    developerContact: "test@test.com"
);
var navClient = new NavClient(technicalUser, softwareIdentification, NavEnvironment.Test);
```

**Obtaining the Exchange token which is required for each request**

```csharp
var exchangeToken = await client.GetExchangeTokenAsync();
```

**Creating the supplier info**

```csharp
var supplierInfo = new SupplierInfo(
    taxpayerId: supplierTaxpayerId,
    vatCode: VatCode.Create("2").Success.Get() // check the documentation for more information about each VatCode.,
    name: Name.Create("supplier name").Success.Get(),
    address: supplierAddress
);
```

**Creating invoice items**

```csharp
var itemAmount = new Amount(net: new AmountValue(1694.92m), gross: new AmountValue(2000), tax: new AmountValue(305.08m));
var unitAmount = new ItemAmounts(itemAmount, itemAmount, 0.18m);
var items = new[]
{
    new InvoiceItem(
        consumptionDate: DateTime.UtcNow.Date,
        totalAmounts: new ItemAmounts(itemAmount, itemAmount, 0.18m),
        description: Description.Create("Item 1 description").Success.Get(),
        measurementUnit: MeasurementUnit.Night,
        quantity: 1,
        unitAmounts: unitAmount,
        exchangeRate: ExchangeRate.Create(1).Success.Get()
    )
};
```

**Creating an invoice**

```csharp
var invoice = new Invoice(
    number: InvoiceNumber.Create("Invoice-10").Success.Get(),
    issueDate: DateTime.UtcNow.Date,
    supplierInfo: supplierInfo,
    receiver: receiver, // B2B or B2C, each have its own requirements.
    items: Sequence.FromPreordered(items, startIndex: 1).Get(),
    paymentDate: DateTime.UtcNow.Date,
    currencyCode: CurrencyCode.Create("EUR").Success.Get()
);
```

**Reporting invoices**

```csharp
return await client.SendInvoicesAsync(
    token: exchangeToken.SuccessResult,
    invoices: Sequence.FromPreordered(new[] { invoice }, startIndex: 1).Get()
);
```

**In case of rebating/modifying/changing an invoice**
It is possible to report the modified invoice through ```SendModificationDocumentsAsync``` API.

More examples can be found in the [Tests](https://github.com/MewsSystems/fiscalizations/tree/master/src/Hungary/Mews.Fiscalizations.Hungary.Tests).