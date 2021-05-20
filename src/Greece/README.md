![Build Status](https://github.com/MewsSystems/fiscalization-greece/workflows/Build%20and%20test/badge.svg)

# Mews.Fiscalization.Greece

## Description
This library is providing a client to submit data to greek fiscal authorities. For more info see https://www.aade.gr/mydata.
This is still work in progress as it appears that not all use-cases are covered by the API. Therefore we do not recommend using this in production environments.

## Dependencies
This package uses [Mews.Fiscalization.Core](https://github.com/MewsSystems/fiscalization-core/) for data types, validations and other types of functionality shared across our fiscalization libraries.

## Key features
- No Greek abbreviations.
- Early data validation.
- Immutable DTOs.
- Logging support

## Usage
We tend to use immutable DTOs wherever possible, especially to ensure data validity.
We want the library to throw an error as soon as possible, i.e. when constructing corresponding data structures.
That is why we even introduce wrappers for simple datatypes.

### Simplest usage example
```csharp
var invoices = SequentialEnumerable.FromPreordered(
    new RetailSalesReceipt(
        issuer: new LocalCounterpart(new GreekTaxIdentifier({UserVatNumber})),
        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
        revenueItems: new List<NonNegativeRevenue>
        {
            new NonNegativeRevenue(new NonNegativeAmount(53.65m), new NonNegativeAmount(12.88m), TaxType.Vat6, RevenueType.Products),
            new NonNegativeRevenue(new NonNegativeAmount(53.65m), new NonNegativeAmount(12.88m), TaxType.Vat6, RevenueType.Services),
            new NonNegativeRevenue(new NonNegativeAmount(53.65m), new NonNegativeAmount(12.88m), TaxType.Vat6, RevenueType.Other)
        },
        payments: new List<NonNegativePayment>
        {
            new NonNegativePayment(new NonNegativeAmount(133.06m), PaymentType.DomesticPaymentsAccountNumber),
            new NonNegativePayment(new NonNegativeAmount(66.53m), PaymentType.Cash)
        }
));

var client = new AadeClient({UserId}, {UserSubscriptionKey});
var response = await client.SendInvoicesAsync(invoices);
```

# NuGet

We have published the library as [Mews.Fiscalization.Greece](https://www.nuget.org/packages/Mews.Fiscalization.Greece/).
