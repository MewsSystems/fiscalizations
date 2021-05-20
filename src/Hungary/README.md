# Mews.Fiscalization.Hungary

## Description
This library uses https://onlineszamla.nav.gov.hu/ API [v2.0](https://onlineszamla.nav.gov.hu/dokumentaciok) to report invoices to the Hungarian authorities.

## Key features
- No Hungarian abbreviations.
- Early data validation.
- Immutable DTOs.

## Usage
We tend to use immutable DTOs wherever possible, especially to ensure data validity.
We want the library to throw an error as soon as possible, i.e. when constructing corresponding data structures.
That is why we even introduce wrappers for simple datatypes.

# NuGet

We have published the library as [Mews.Fiscalization.Hungary](https://www.nuget.org/packages/Mews.Fiscalization.Hungary/).
