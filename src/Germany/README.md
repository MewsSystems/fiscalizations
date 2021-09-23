<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" src="https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png">
    </a>
    <br><br>
    <b>Mews.Fiscalizations.Germany</b> is a .NET library that was built to help reporting of e-invoices to the German authorities (BSI - Bundesamt für Sicherheit in der Informationstechnik) using <a href="https://developer.fiskaly.com/api/kassensichv/v2/">Fiskaly KassenSichV API V2-certified</a>.
    <br><br>
    <a href="https://www.nuget.org/packages/Mews.Fiscalizations.Germany/">
        <img src="https://img.shields.io/nuget/v/Mews.Fiscalizations.Germany">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE">
        <img src="https://img.shields.io/github/license/MewsSystems/fiscalizations">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-germany-windows.yml">
        <img src="https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Germany%20(Windows)/master?label=windows%20build">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-germany-linux.yml">
        <img src="https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Germany%20(Linux)/master?label=linux%20build">
    </a>
    <a href="https://developer.fiskaly.com/api/kassensichv/v1/">
        <img src="https://img.shields.io/badge/v2-Fiskaly-lightgrey">
    </a>
</p>


## 📃 Description

The library uses Fiskaly API to report the invoices, for more information, please check their [Documentation](https://developer.fiskaly.com/api/kassensichv/v2/).

## ⚙️ Installation

The library can be installed through NuGet packages or the command line as mentioned below:
```bash
Install-Package Mews.Fiscalizations.Germany
```

## 🎯 Features

-   Functional approach via [FuncSharp](https://github.com/siroky/FuncSharp).
-   No German abbreviations.
-   Early data validation.
-   Asynchronous I/O.
-   All endpoints are covered with tests.
-   Intuitive immutable DTOs.
-   Pipelines that run on both Windows and Linux operating systems.
-   Cross platform (uses .NET Standard).

## 📦 NuGet

We have published the library as [Mews.Fiscalizations.Germany](https://www.nuget.org/packages/Mews.Fiscalizations.Germany/).

## 👀 Code Examples

P.S Please note that we keep files of the older API versions, but we run the tests on the latest supported API version and not older ones.

Listed below are some of the common examples. If you want to see more code examples, please check the [Tests](https://github.com/MewsSystems/fiscalizations/tree/master/src/Germany/Mews.Fiscalizations.Germany.Tests).

Fiskaly Client can be created using the **ApiKey** and **ApiSecret** which can be created through Fiskaly dashboard.

```csharp
var client = new FiskalyClient(ApiKey, ApiSecret);
```

All endpoints require providing a valid **accessToken**

```csharp
var accessToken = await client.GetAccessTokenAsync();
```

In order to report an invoice to the German authorities, we would have to start a transaction and then finish it (change the state to **FINISHED** and provide the invoice to be reported.).

**Creation of a new invoice**

There are 3 required properties for creating an invoice:
1. **Invoice type** (invoice or receipt).
2. **Payments** which is a list of the amounts paid, type of payment, and the currency.
3. **Items** which is a list of the item amounts and the VAT rate ("NORMAL" "REDUCED_1" "SPECIAL_RATE_1" "SPECIAL_RATE_2" "NULL") -> ("19" "7" "10.7" "5.5" "0").

```csharp
var bill = new Bill(
    type: BillType.Receipt,
    payments: new List<Payment>() { new Payment(25, PaymentType.Cash, "EUR") },
    items: new List<Item>() { new Item(25, VatRateType.Normal) }
);
```

1. To start a transaction, we would need to provide valid **ClientId** and **TssId** which can be created through fiskaly dashboard or by calling **CreateClientAsync** for creating the client and **CreateTssAsync** for creating the TSS which will be described below, and a unique **id** for the transaction.
2. To finish a transaction, we would need to provide the **ClientId**, **TssId**, and the invoice to be reported and the transaction id that we specified in the step above.

Example:
```csharp
var transactionId = Guid.NewGuid();
var startedTransaction = await client.StartTransactionAsync(accessToken, clientId, tssId, transactionId);
var endedTransaction = await client.FinishTransactionAsync(accessToken, clientId, tssId, InvoiceToReport, transactionId);
```

**Creation of a new client id**
```csharp
var client = await client.CreateClientAsync(accessToken, TssId);
var clientId = client.SuccessResult.Id;
```

**Creation of a new Tss id**
```csharp
var tss = await client.CreateTssAsync(accessToken);
var tssId = tss.SuccessResult.Id;
```

After the creation of a new TSS, it is necessary to save the Admin PUK code that is returned in the response, the PUK code will be used later (for admin authentication).

Since the created above TSS will be created with state = "Created", it cannot be used yet, so we should update the state to Uninitialized.

**Updating Tss state**
```csharp
var tss = await client.UpdateTssAsync(accessToken, tssToUpdateId, TssState.Uninitialized);
```

After updating the TSS state to Uninitialized, It will be possible to change the admin PIN using the PUK code we recieved in the response above.

**Changing admin PIN**
```csharp
await client.ChangeAdminPinAsync(accessToken, tssId, tss.AdminPuk, newAdminPin: "123123");
```

**Authenticate using the PIN created above**
```csharp
await client.AdminLoginAsync(accessToken, tssId, "123123");
```

After authenticating with the admin PIN, we can update the state of the TSS from Uninitialized to Initialized (using the update as described above).
