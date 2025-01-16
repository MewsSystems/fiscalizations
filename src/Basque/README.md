<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" src="https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png">
    </a>
    <br><br>
    <b>Mews.Fiscalizations.Basque</b> A .NET library for reporting invoices to the Basque Country tax authorities via TicketBAI (TBAI) compliance.
    <br><br>
    <a href="https://www.nuget.org/packages/Mews.Fiscalizations.Basque/">
        <img src="https://img.shields.io/nuget/v/Mews.Fiscalizations.Basque">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE">
        <img src="https://img.shields.io/github/license/MewsSystems/fiscalizations">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/publish-basque.yml">
        <img src="https://img.shields.io/github/actions/workflow/status/MewsSystems/fiscalizations/publish-basque.yml?branch=master&label=publish">
    </a>
    <a href="https://www.gipuzkoa.eus/es/web/ogasuna/ticketbai">
        <img src="https://img.shields.io/badge/TicketBai-lightgrey">
    </a>
</p>


## 📃 Description

The `Mews.Fiscalizations.Basque` library integrates with the TicketBai API, enabling compliant invoice reporting to tax authorities in all three Basque Country regions: Araba, Gipuzkoa, and Bizkaia. For further details, refer to the official documentation of each region: [Araba](https://web.araba.eus/es/hacienda/ticketbai/documentacion-tecnica), [Gipuzkoa](https://www.gipuzkoa.eus/es/web/ogasuna/ticketbai/documentacion-y-normativa), and [Bizkaia](https://www.batuz.eus/es/documentacion-tecnica).  

## ⚙️ Installation

You can install the library via NuGet Package Manager or the command line:
```bash
Install-Package Mews.Fiscalizations.Basque
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

Available on NuGet as [Mews.Fiscalizations.Basque](https://www.nuget.org/packages/Mews.Fiscalizations.Basque/).

## 👀 Code Examples

Please refer to the [Tests](https://github.com/MewsSystems/fiscalizations/tree/master/src/Basque/Mews.Fiscalizations.Basque.Tests) for detailed code examples and usage scenarios.

>Note: A valid certificate is required to access the API. Please consult the documentation for guidance on obtaining and configuring certificates.
