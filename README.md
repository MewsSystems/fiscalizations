<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" height="100px" src="https://user-images.githubusercontent.com/435787/129971779-2c64348e-05a3-49d0-b026-91913ffd68dc.png">
    </a>
</p>

[![Build](https://img.shields.io/github/actions/workflow/status/MewsSystems/fiscalizations/build-and-test-all.yml?branch=master&label=build%20and%20tests)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-all.yml)
[![Build](https://img.shields.io/github/actions/workflow/status/MewsSystems/fiscalizations/publish-all.yml?branch=master&label=publish)](https://github.com/MewsSystems/fiscalizations/actions/workflows/publish-all.yml)
[![License](https://img.shields.io/github/license/MewsSystems/fiscalizations)](https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE)

**Mews.Fiscalizations** is a .NET library designed to support reporting of e-invoices to government authorities in multiple countries.

## 📃 Overview

This repository contains a set of projects, each focused on facilitating e-invoice reporting to the appropriate governmental authority in different regions. Refer to the documentation in each project folder for details on specific implementations.

>Disclaimer: These libraries were developed for internal use, so additional customization may be necessary to meet your specific needs. We cannot guarantee the accuracy of data produced by the libraries. Contributions are welcome if you'd like to extend functionality.

## ⚙️ Installation

To use a fiscalization package for a specific country, install it via NuGet

Here’s how to get started with the Spanish fiscalization package, for example:
```bash
Install-Package Mews.Fiscalizations.Spain
```

To install the package that covers all supported countries:
```bash
Install-Package Mews.Fiscalizations.All
```

## 🎯 Key Features

-   Functional programming approach using [FuncSharp](https://github.com/MewsSystems/FuncSharp). with an emphasis on 'Option' and 'Try'.
-   Early validation of data inputs.
-   Asynchronous I/O operations.
-   Comprehensive test coverage for all endpoints.
-   Intuitive and immutable Data Transfer Objects (DTOs).
-   CI/CD pipelines compatible with both Windows and Linux.
-   Code examples provided within each project.
-   Cross-platform compatibility (.NET 8).
-   Support for fiscalizations across six countries and the Basque region.
-   Logging capabilities for selected implementations.

## ⚠ Important Notes
Our production environments run on .NET 8, which doesn’t require `ConfigureAwait` since there’s no `SynchronizationContext`. However, if you rely on `ConfigureAwait` in your application, consider creating a separate branch to accommodate this.

## 👀 Examples

Each fiscalization project includes code examples. Refer to the project-specific documentation for practical implementations and use cases.

## 🧬 Projects

| **Project** | **Nuget Package** | **Description** |
| ----------- | ----------------- | --------- |
| All | [Mews.Fiscalizations.All](https://www.nuget.org/packages/Mews.Fiscalizations.All) | A package for all supported countries |
| [Core](https://github.com/MewsSystems/fiscalizations/tree/master/src/Core) | [Mews.Fiscalizations.Core](https://www.nuget.org/packages/Mews.Fiscalizations.Core) | Core library supporting shared functionality |
| [Austria](https://github.com/MewsSystems/fiscalizations/tree/master/src/Austria) | [Mews.Fiscalizations.Austria](https://www.nuget.org/packages/Mews.Fiscalizations.Austria) | Austria-specific fiscalization library (RKSV) |
| [Germany](https://github.com/MewsSystems/fiscalizations/tree/master/src/Germany) | [Mews.Fiscalizations.Germany](https://www.nuget.org/packages/Mews.Fiscalizations.Germany) | Germany-specific fiscalization library (KassenSichV) |
| [Hungary](https://github.com/MewsSystems/fiscalizations/tree/master/src/Hungary) | [Mews.Fiscalizations.Hungary](https://www.nuget.org/packages/Mews.Fiscalizations.Hungary) | Hungary-specific fiscalization library (NAV) |
| [Italy](https://github.com/MewsSystems/fiscalizations/tree/master/src/Italy) | [Mews.Fiscalizations.Italy](https://www.nuget.org/packages/Mews.Fiscalizations.Italy) | Italy-specific fiscalization library (SDI) |
| [Spain](https://github.com/MewsSystems/fiscalizations/tree/master/src/Spain) | [Mews.Fiscalizations.Spain](https://www.nuget.org/packages/Mews.Fiscalizations.Spain) | Spain-specific fiscalization library (SII) |
| [Basque](https://github.com/MewsSystems/fiscalizations/tree/master/src/Basque) | [Mews.Fiscalizations.Basque](https://www.nuget.org/packages/Mews.Fiscalizations.Basque) | Basque region-specific fiscalization library (TicketBAI) |

## 🧑 Authors
<table>
  <tr>
    <td align="center"><a href="https://github.com/KaliCZ"><img src="https://avatars.githubusercontent.com/u/12395130?v=4" width="100px;" alt=""/><br /><sub><b>Pavel Kalandra</b></sub></a><br /></td>
    <td align="center"><a href="https://github.com/abdallahbeshi"><img src="https://avatars.githubusercontent.com/u/51375082?v=4" width="100px;" alt=""/><br /><sub><b>Abdallah Altrabeishi</b></sub></a><br /></td>
    <td align="center"><a href="https://github.com/marektresnak"><img src="https://avatars.githubusercontent.com/u/12021177?v=4" width="100px;" alt=""/><br /><sub><b>Marek Tresnak</b></sub></a><br /></td>
  </tr>
</table>

## 👍 Contributing

We welcome contributions! If you’d like to support the project, please create a pull request with a clear description of your changes and ensure that tests are included if applicable.

## ☕ Donate

If you’d like to show support, please star this project on GitHub! While no monetary donations are needed, we do accept gummy bears at Mews Systems s.r.o., Náměstí IP Pavlova 5, Vinohrady 120 00 Prague. 🍬

## 🏢 About us

We’re building transformational technology for millions of hospitality professionals and their guests.

**Hoteliers**

The hoteliers who choose Mews share our passion for innovation and they don’t accept the status quo. They’re using our technology to rethink physical spaces, services, and guest experiences.

**Guests**

Hospitality brands are heavily scrutinized. The day a guest checks-out, their rating goes online. Ultimately we’re designing Mews for our customer’s customer because every guest experience must be remarkable.

For more, visit https://www.mews.com/en/about-us

## ⚠️ License

Licensed under the [MIT](https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE)
