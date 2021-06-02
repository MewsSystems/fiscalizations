# Mews.Fiscalizations.Germany

This library uses https://developer.fiskaly.com/api/ KassenSichV API V1 to report invoices to the German authorities.

## Key features
- No German abbreviations.
- Early data validation.
- Immutable DTOs.

## Usage
We tend to use immutable DTOs wherever possible, especially to ensure data validity.
We want the library to throw an error as soon as possible, i.e. when constructing corresponding data structures.
That is why we even introduce wrappers for simple datatypes.

## Code examples

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
1. **invoice type** (invoice or reciept).
2. **payments** which contains the amount paid and type of payment and the currency.
3. **items** which contains the item amount and the VAT rate ("NORMAL" "REDUCED_1" "SPECIAL_RATE_1" "SPECIAL_RATE_2" "NULL") -> ("19" "7" "10.7" "5.5" "0").

```csharp
var bill = new Bill(
    type: BillType.Receipt,
    payments: new List<Payment>() { new Payment(25, PaymentType.Cash, "EUR") },
    items: new List<Item>() { new Item(25, VatRateType.Normal) }
);
```

1. To start a transaction, we would need to provide valid **ClientId** and **TssId** which can be created through fiskaly dashboard or by calling **CreateClientAsync** for creating the client and **CreateTssAsync** for creating the TSS which will be described below, and a unique **id** for the transaction.
2. To finish a transaction, we would need to provide the **ClientId**, **TssId**, and the invoice to be reported and the transaction id that we specified in the step above.
(lastRevision is used for the preservation of order of subsequent calls).

Example:
```csharp
var transactionId = Guid.NewGuid();
var startedTransaction = await client.StartTransactionAsync(accessToken, clientId, tssId, transactionId);
var endedTransaction = await client.FinishTransactionAsync(accessToken, clientId, tssId, InvoiceToReport, transactionId, lastRevision: "1");
```

**Creation of a new client id**
```csharp
var client = await client.CreateClientAsync(accessToken, TssId);
var clientId = client.SuccessResult.Id;
```

**Creation of a new Tss id**
```csharp
var tss = await client.CreateTssAsync(accessToken, TssState.Initialized, description: "Creating a test TSS.");
var tssId = tss.SuccessResult.Id;
```

More examples can be found in the [Tests](https://github.com/MewsSystems/fiscalizations/tree/master/src/Germany/Mews.Fiscalizations.Germany.Tests).

## Fiskaly documentation
https://developer.fiskaly.com/api/kassensichv/v1/

## NuGet

We have published the library as [Mews.Fiscalizations.Germany](https://www.nuget.org/packages/Mews.Fiscalizations.Germany/).
