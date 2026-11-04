<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" src="https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png">
    </a>
    <br><br>
    <b>Mews.Fiscalizations.Czechia</b> is a .NET client for EET 2.0, the Czech electronic registration of sales (Elektronická evidence tržeb) that starts on 1 January 2027.
    <br><br>
    <a href="https://www.nuget.org/packages/Mews.Fiscalizations.Czechia/">
        <img src="https://img.shields.io/nuget/v/Mews.Fiscalizations.Czechia">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE">
        <img src="https://img.shields.io/github/license/MewsSystems/fiscalizations">
    </a>
</p>

## Description

EET 2.0 is a SOAP web service operated by the Czech Financial Administration. Every sale is sent as a signed data message (`Trzba`) and the service answers with a confirmation code (POK) that is printed on the receipt.
The library implements data interface version 4 ("Formát a struktura údajů o evidované tržbě a popis datového rozhraní", v1.2a) published at [eet.gov.cz](https://eet.gov.cz/cs/pro-vyvojare/dokumenty-ke-stazeni).

Compared to the original EET (interface v3, removed from this repository in 2023), EET 2.0 no longer uses PKP/BKP security codes or VAT breakdowns. The message only identifies the sale (taxpayer, registrating unit, point of sale, receipt number, time) and its total, and is signed with WS-Security.

## Installation

```bash
Install-Package Mews.Fiscalizations.Czechia
```

## Key Features

-   Functional approach using [FuncSharp](https://github.com/MewsSystems/FuncSharp) and early validation of every data item against the official XSD patterns.
-   WS-Security signing (exclusive C14N, RSA-SHA256, X509v3 BinarySecurityToken) of the SOAP body only, as required by chapter 5.2 of the interface.
-   Operational and verification (`overeni`) modes, repeated sending (`prvni_zaslani`), delegated (`eic_poverujiciho`) and multi-taxpayer (`povereni_vice_popl`) sales, deposits (`urceno_cerp_zuct`, `cerp_zuct`).
-   Typed acknowledgements, errors (temporary errors and successful verification are flagged) and warnings, plus the `X-Global-Transaction-Id` header for troubleshooting.
-   PKCS#12 certificates are loaded in memory without importing the private key into the OS key store.
-   Integration tests against the official playground (`pg.trzbyeet.gov.cz`); its shared test certificates are downloaded from eet.gov.cz at test time.

## Usage

```csharp
var certificate = Certificate.CreateUnsafe(File.ReadAllBytes("CZ00000019.p12"), password);
var client = new EetClient(httpClient, certificate, EetEnvironment.Playground);

var record = new RevenueRecord(
    taxpayerTaxId: TaxIdentifier.CreateUnsafe("CZ00000019"),
    registrationUnitId: RegistrationUnitIdentifier.CreateUnsafe(243),
    registryId: RegistryIdentifier.CreateUnsafe("POS-1"),
    billNumber: BillNumber.CreateUnsafe("2027/000123"),
    created: DateTimeOffset.Now,
    revenue: new Revenue(total: CurrencyValue.CreateUnsafe(3264.50m))
);

var result = await client.SendRevenueAsync(record);
result.Outcome.Match(
    success => Console.WriteLine($"POK: {success.ConfirmationCode}"),
    error => Console.WriteLine($"{error.Code}: {error.Message} (resend later: {error.IsTemporary})")
);
```

When an acknowledgement is not received (timeout, temporary error), send the same record again with `isFirstAttempt: false`; every attempt gets a new message UUID.
More examples are in the [tests](https://github.com/MewsSystems/fiscalizations/tree/master/src/Czechia/Mews.Fiscalizations.Czechia.Tests).

## Known limitations

-   The signature of acknowledgement messages (chapter 5.3) is not verified yet; the response is trusted based on the TLS connection to the tax authority.
