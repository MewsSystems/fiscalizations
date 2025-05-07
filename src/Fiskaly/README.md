<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" src="https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png">
    </a>
    <br><br>
    <b>Mews.Fiscalizations.Verifactu</b> is a .NET library designed for reporting e-invoices to Spain tax authorities (BSI - Bundesamt für Sicherheit in der Informationstechnik) via the <a href="https://developer.fiskaly.com/api/kassensichv/v2/">Fiskaly KassenSichV API V2</a>.
    <br><br>
    <a href="https://www.nuget.org/packages/Mews.Fiscalizations.Verifactu/">
        <img src="https://img.shields.io/nuget/v/Mews.Fiscalizations.Verifactu">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE">
        <img src="https://img.shields.io/github/license/MewsSystems/fiscalizations">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/publish-verifactu.yml">
        <img src="https://img.shields.io/github/actions/workflow/status/MewsSystems/fiscalizations/publish-verifactu.yml?branch=master&label=publish">
    </a>
    <a href="https://developer.fiskaly.com/api/kassensichv/v1/">
        <img src="https://img.shields.io/badge/v2-Fiskaly-lightgrey">
    </a>
</p>


## 📃 Description

This library enables fiscal reporting through the Fiskaly API. For more information, consult the official [documentation](https://developer.fiskaly.com/api/kassensichv/v2/).

## ⚙️ Installation

Install via NuGet or the command line:
```bash
Install-Package Mews.Fiscalizations.Verifactu
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

Available on NuGet as [Mews.Fiscalizations.Verifactu](https://www.nuget.org/packages/Mews.Fiscalizations.Verifactu/).

## 👀 Code Examples

Below are common usage examples. For additional code samples, see the [Tests](https://github.com/MewsSystems/fiscalizations/tree/master/src/Verifactu/Mews.Fiscalizations.Verifactu.Tests).

### Setup
Create a Fiskaly client using `ApiKey` and `ApiSecret`:

```csharp
var client = new FiskalyClient(ApiKey, ApiSecret);
```

All endpoints require providing a valid `accessToken`

```csharp
var accessToken = await client.GetAccessTokenAsync();
```

In order to report an invoice to the Spanish authorities, we would have to start a transaction and then finish it (change the state to **FINISHED** and provide the invoice to be reported.).

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
var puk = tss.SuccessResult.AdminPuk; // Store it. It's needed for setting or changing admin Pin.
```

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
