# Mews.Fiscalization.Italy

[![Build Status](https://mews.visualstudio.com/Mews/_apis/build/status/MewsSystems.fiscalization-italy?branchName=master)](https://mews.visualstudio.com/Mews/_build/latest?definitionId=2&branchName=master)

## Description
A client library for reporting invoices through SDI (Sistema di interscambio). Here are the main parts of the library:
- **SDI Client** that handles communication with the SDI.
- **DTOs** that can be serialized into XML conforming to the FatturaPA format (the official format in which all invoices need to be reported).
- **DTOs** for handling messages sent by the SDI.

## Key features
- No Italian abbreviations.
- Early data validation.
- Immutable DTOs.

## Usage
We tend to use immutable DTOs wherever possible, especially to ensure data validity.
We want the library to throw an error as soon as possible, i.e. when constructing corresponding data structures.
That is why we even introduce wrappers for simple datatypes.

## Security protocol
- TLS 1.0 protocol must be enabled, that can be achieved by adding the following line to your code:
```csharp
ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls;
```

# NuGet

We have published the library as [Mews.Fiscalization.Italy](https://www.nuget.org/packages/Mews.Fiscalization.Italy/).
