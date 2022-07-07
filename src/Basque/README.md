<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" src="https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png">
    </a>
    <br><br>
    <b>Mews.Fiscalizations.Basque</b> is a .NET library that was built to help reporting invoices to the Basque countries tax authorities (TicketBai - TBAI).
    <br><br>
    <a href="https://www.nuget.org/packages/Mews.Fiscalizations.Basque/">
        <img src="https://img.shields.io/nuget/v/Mews.Fiscalizations.Basque">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE">
        <img src="https://img.shields.io/github/license/MewsSystems/fiscalizations">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-basque-windows.yml">
        <img src="https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20TicketBai%20(Windows)/master?label=windows%20build">
    </a>
    <a href="https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-basque-linux.yml">
        <img src="https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20TicketBai%20(Linux)/master?label=linux%20build">
    </a>
    <a href="https://www.gipuzkoa.eus/es/web/ogasuna/ticketbai">
        <img src="https://img.shields.io/badge/TicketBai-lightgrey">
    </a>
</p>


## 📃 Description

The library uses TicketBai (Gipuzkoa) API to report the invoices, for more information, please check the [Documentation](https://www.gipuzkoa.eus/es/web/ogasuna/ticketbai/documentacion-y-normativa).

## ⚙️ Installation

The library can be installed through NuGet packages or the command line as mentioned below:
```bash
Install-Package Mews.Fiscalizations.Basque
```

## 🎯 Features

-   Functional approach via [FuncSharp](https://github.com/siroky/FuncSharp).
-   No Spanish/Basque abbreviations.
-   Early data validation.
-   Asynchronous I/O.
-   All endpoints are covered with tests.
-   Intuitive immutable DTOs.
-   Pipelines that run on both Windows and Linux operating systems.
-   Cross platform (uses .NET Standard).

## 📦 NuGet

We have published the library as [Mews.Fiscalizations.Basque](https://www.nuget.org/packages/Mews.Fiscalizations.Basque/).

## 👀 Code Examples

For code examples, please check the [Tests](https://github.com/MewsSystems/fiscalizations/tree/master/src/Basque/Mews.Fiscalizations.Basque.Tests).

Please note that you will need a valid certificate in order to use the API, please check the documentation.
