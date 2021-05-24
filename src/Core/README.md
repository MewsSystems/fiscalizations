# Mews.Fiscalizations.Core

Core library for implementing other fiscalization libraries.

## Content

There is a Check class which has various methods for validating conditions. In case a condition is not met, an exception is thrown.

There are various data types that serve for early validation. For example an instance of `LimitedString1To50` is always guaranteed to have a string that has length between 1 and 50 characters. You can use this to create a strongly-typed model that guarantees it is valid on creation.

# NuGet

We have published the library as [Mews.Fiscalizations.Core](https://www.nuget.org/packages/Mews.Fiscalizations.Core/).
