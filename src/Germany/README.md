# Mews.Fiscalization.Germany
[![Build and test - Germany](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-germany.yml/badge.svg)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-germany.yml)

## Description
This library uses https://developer.fiskaly.com/api/ KassenSichV API V1 to report invoices to the German authorities.

## Key features
- No German abbreviations.
- Early data validation.
- Immutable DTOs.

## Usage
We tend to use immutable DTOs wherever possible, especially to ensure data validity.
We want the library to throw an error as soon as possible, i.e. when constructing corresponding data structures.
That is why we even introduce wrappers for simple datatypes.

# NuGet

We have published the library as [Mews.Fiscalization.Germany](https://www.nuget.org/packages/Mews.Fiscalization.Germany/).
