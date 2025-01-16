<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" height="100px" src="https://user-images.githubusercontent.com/435787/129971779-2c64348e-05a3-49d0-b026-91913ffd68dc.png">
    </a>
</p>

[![Build](https://img.shields.io/github/actions/workflow/status/MewsSystems/fiscalizations/build-and-test-all.yml?branch=master&label=build%20and%20tests)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-all.yml)
[![Build](https://img.shields.io/github/actions/workflow/status/MewsSystems/fiscalizations/publish-all.yml?branch=master&label=publish)](https://github.com/MewsSystems/fiscalizations/actions/workflows/publish-all.yml)
[![License](https://img.shields.io/github/license/MewsSystems/fiscalizations)](https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE)

**Mews.Fiscalizations** is a .NET library that was built to help reporting of e-invoices to different government authorities.

## 📃 Description

This repository contains multiple projects. Each project supports reporting of e-invoices to the corresponding country's government authority. Each project folder has its own documentation. Use the link in the table below to get there.

**Please note that we're not responsible for how these libraries are used and whether the data provided is correct or not.**
**Also, it is important to note that these libraries were created to meet our usages, so they might be missing some data that can be important in your case (feel free to extend any library and create a PR).**

## ⚙️ Installation

For the reporting of e-invoices to a specific authority, install the package that corresponds to that country through NuGet or by using the following commands.

For example, installing the Spanish fiscalization package in order to report invoices to the Spanish authorities (SII).
```bash
Install-Package Mews.Fiscalizations.Spain
```

To install the package that supports reporting e-invoices for all the supported countries.
```bash
Install-Package Mews.Fiscalizations.All
```

## 🎯 Features

-   Functional approach via [FuncSharp](https://github.com/MewsSystems/FuncSharp). Definitely check out the examples of usage, so you're not surprised. At the very least IOptions and ITries.
-   Early data validation.
-   Asynchronous I/O.
-   All endpoints are covered with tests.
-   Intuitive immutable DTOs.
-   Pipelines that run on both Windows and Linux operating systems.
-   Code examples for each project.
-   Cross-platform (uses .NET 8).
-   6 countries + Basque region supported.
-   Logging support for some fiscalizations.

## ⚠ Warning
Since our production servers are now on .NET Core, we no longer need to use ConfigureAwait when awaiting tasks. as .NET Core does not have a SynchronizationContext, so it won't matter if we use ConfigureAwait or not. But if you need to, you may need to create another branch and include this.

## 👀 Examples

Please visit the desired fiscalization project to see its code examples. 

## 🧬 Projects

| **Project**                                                                      | **Nuget Package**                                                                         | **Notes** |
|----------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------| --------- |
| All                                                                              | [Mews.Fiscalizations.All](https://www.nuget.org/packages/Mews.Fiscalizations.All)         |
| [Core](https://github.com/MewsSystems/fiscalizations/tree/master/src/Core)       | [Mews.Fiscalizations.Core](https://www.nuget.org/packages/Mews.Fiscalizations.Core)       |
| [Austria](https://github.com/MewsSystems/fiscalizations/tree/master/src/Austria) | [Mews.Fiscalizations.Austria](https://www.nuget.org/packages/Mews.Fiscalizations.Austria) |
| [Germany](https://github.com/MewsSystems/fiscalizations/tree/master/src/Germany) | [Mews.Fiscalizations.Germany](https://www.nuget.org/packages/Mews.Fiscalizations.Germany) |
| [Hungary](https://github.com/MewsSystems/fiscalizations/tree/master/src/Hungary) | [Mews.Fiscalizations.Hungary](https://www.nuget.org/packages/Mews.Fiscalizations.Hungary) |
| [Italy](https://github.com/MewsSystems/fiscalizations/tree/master/src/Italy)     | [Mews.Fiscalizations.Italy](https://www.nuget.org/packages/Mews.Fiscalizations.Italy)     |
| [Spain](https://github.com/MewsSystems/fiscalizations/tree/master/src/Spain)     | [Mews.Fiscalizations.Spain](https://www.nuget.org/packages/Mews.Fiscalizations.Spain)     |
| [Basque](https://github.com/MewsSystems/fiscalizations/tree/master/src/Basque)   | [Mews.Fiscalizations.Basque](https://www.nuget.org/packages/Mews.Fiscalizations.Basque)   |
| [Sweden](https://github.com/MewsSystems/fiscalizations/tree/master/src/Sweden)   | [Mews.Fiscalizations.Sweden](https://www.nuget.org/packages/Mews.Fiscalizations.Sweden)   |

## 🧑 Authors
<table>
  <tr>
    <td align="center"><a href="https://github.com/KaliCZ"><img src="https://avatars.githubusercontent.com/u/12395130?v=4" width="100px;" alt=""/><br /><sub><b>Pavel Kalandra</b></sub></a><br /></td>
    <td align="center"><a href="https://github.com/abdallahbeshi"><img src="https://avatars.githubusercontent.com/u/51375082?v=4" width="100px;" alt=""/><br /><sub><b>Abdallah Altrabeishi</b></sub></a><br /></td>
    <td align="center"><a href="https://github.com/marektresnak"><img src="https://avatars.githubusercontent.com/u/12021177?v=4" width="100px;" alt=""/><br /><sub><b>Marek Tresnak</b></sub></a><br /></td>
  </tr>
</table>

## 👍 Contribute

If you want to support the development of `Mews.Fiscalizations`, feel free to create a PR with a clear description of what was fixed or introduced.
Also, please make sure to introduce tests when applicable.

## ☕ Donate

There is no need to donate to the project, but thanks for considering it! Instead, if you like the project, star it here on GitHub :-) Thanks!

If you still insist on donating, we accept gummy bears at Mews Systems s.r.o., Náměstí IP Pavlova 5, Vinohrady 120 00 Prague. This project was, of course, powered by a huge pile of gummy bears ;-)

## 🏢 About us

We’re building transformational technology for millions of hospitality professionals and their guests.

**Hoteliers**

The hoteliers who choose Mews share our passion for innovation and they don’t accept the status quo. They’re using our technology to rethink physical spaces, services, and guest experiences.

**Guests**

Hospitality brands are heavily scrutinized. The day a guest checks-out, their rating goes online. Ultimately we’re designing Mews for our customer’s customer because every guest experience must be remarkable.

More information on https://www.mews.com/en/about-us

## ⚠️ License

[MIT](https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE)
