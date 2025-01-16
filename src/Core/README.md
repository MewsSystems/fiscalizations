# Mews.Fiscalizations.Core

A foundational .NET library for building fiscalization solutions, providing core components for data validation and early error detection.

## ? Overview
**Mews.Fiscalizations.Core** offers essential functionality to help you build fiscalization libraries with confidence and ease. It includes robust validation tools and strongly-typed data structures, ensuring data integrity at every step.

## ? Features

**Validation with the Check Class**
The Check class provides methods to enforce specific conditions within your application. If a condition is not met, an exception is thrown immediately, helping you catch issues early in the development cycle.

**Strongly-Typed Data Models**
Various data types, such as LimitedString1To50, ensure early validation by enforcing specific constraints. For example, an instance of LimitedString1To50 is always guaranteed to contain a string between 1 and 50 characters in length. These types enable the creation of models that are valid by design, reducing the likelihood of runtime errors.

## ? Installation

You can install the library from NuGet: [Mews.Fiscalizations.Core](https://www.nuget.org/packages/Mews.Fiscalizations.Core/).
