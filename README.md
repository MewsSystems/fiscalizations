<p align="center">
    <a href="https://mews.com">
        <img alt="Mews" height="100px" src="https://user-images.githubusercontent.com/435787/129971779-2c64348e-05a3-49d0-b026-91913ffd68dc.png">
    </a>
</p>

**Mews.Fiscalizations** is a .NET library that was built to help reporting of e-invoices to different government authorities.

## 📃 Description

This repository contains multiple projects. Each project supports reporting of e-invoices to the corresponding country's government authority. Each project folder has it's own documentation. Use the link in the table below to get there.

**Please note that we're not responsible for how these libraries are used and if the data provided is correct or not.**
**Also, it is important to note that these libraries were created to meet our usages, so they might be missing some data that can be important in your case (feel free to extend any library and create a PR).**

## ⚙️ Installation

For the reporting of e-invoices to a specifc authority, install the package that corresponds to that country through NuGet or using the following commands.

For example, installing the Spanish fiscalization package in order to report invoices to the Spanish authorities (SII).
```bash
Install-Package Mews.Fiscalizations.Spain
```

To install the package that supports reporting e-invoices for all the supported countries.
```bash
Install-Package Mews.Fiscalizations.All
```

## 🎯 Features

-   Functional approach via [FuncSharp](https://github.com/siroky/FuncSharp). Definitely check-out the examples of usage, so you're not surprised. At the very least IOptions and ITries.
-   Early data validation.
-   Asynchronous I/O.
-   All endpoints are covered with tests.
-   Intuitive immutable DTOs.
-   Pipelines that run on both Windows and Linux operating systems.
-   Code examples for each project.
-   Cross platform (uses .NET Standard).
-   6 countries + Basque region supported.
-   Logging support for some fiscalizations.

## ⚠ Warning
Since our production servers are now on .NET Core, we no longer need to use ConfigureAwait when awaiting tasks. as .NET Core does not have a SynchronizationContext, so it won't matter if we use ConfigureAwait or not. But if you need to, you may need to create another branch and include this.

## 👀 Examples

Please visit the desired fiscalization project to see its code examples. 

## 🧬 Projects

| **Project** | **Nuget Package** | **Windows Build** | **Linux Build** | **Notes** |
| ----------- | ----------------- | ----------------- | --------------- | --------- |
| All                                                                              | [Mews.Fiscalizations.All](https://www.nuget.org/packages/Mews.Fiscalizations.All) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20All%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-all-windows.yml)         | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20All%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-all-linux.yml)         |
| [Core](https://github.com/MewsSystems/fiscalizations/tree/master/src/Core)       | [Mews.Fiscalizations.Core](https://www.nuget.org/packages/Mews.Fiscalizations.Core) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Core%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-core-windows.yml)       | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Core%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-core-linux.yml)       |
| [Austria](https://github.com/MewsSystems/fiscalizations/tree/master/src/Austria) | [Mews.Fiscalizations.Austria](https://www.nuget.org/packages/Mews.Fiscalizations.Austria) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Austria%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-austria-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Austria%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-austria-linux.yml) |
| [Czechia](https://github.com/MewsSystems/fiscalizations/tree/master/src/Czechia) | [Mews.Fiscalizations.Czechia](https://www.nuget.org/packages/Mews.Fiscalizations.Czechia) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Czechia%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-czechia-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Czechia%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-czechia-linux.yml) |
| [Germany](https://github.com/MewsSystems/fiscalizations/tree/master/src/Germany) | [Mews.Fiscalizations.Germany](https://www.nuget.org/packages/Mews.Fiscalizations.Germany) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Germany%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-germany-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Germany%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-germany-linux.yml) |
| [Hungary](https://github.com/MewsSystems/fiscalizations/tree/master/src/Hungary) | [Mews.Fiscalizations.Hungary](https://www.nuget.org/packages/Mews.Fiscalizations.Hungary) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Hungary%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-hungary-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Hungary%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-hungary-linux.yml) |
| [Italy](https://github.com/MewsSystems/fiscalizations/tree/master/src/Italy)     | [Mews.Fiscalizations.Italy](https://www.nuget.org/packages/Mews.Fiscalizations.Italy) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Italy%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-italy-windows.yml)     | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Italy%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-italy-linux.yml)     |
| [Spain](https://github.com/MewsSystems/fiscalizations/tree/master/src/Spain)     | [Mews.Fiscalizations.Spain](https://www.nuget.org/packages/Mews.Fiscalizations.Spain) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Spain%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-spain-windows.yml)     | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Spain%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-spain-linux.yml)     | [Mews.Fiscalizations.Basque](https://www.nuget.org/packages/Mews.Fiscalizations.Basque) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Basque%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-basque-windows.yml)     | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Basque%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-basque-linux.yml)

## 🧑 Authors
<table>
  <tr>
    <td align="center"><a href="https://github.com/KaliCZ"><img src="https://avatars.githubusercontent.com/u/12395130?v=4" width="100px;" alt=""/><br /><sub><b>Pavel Kalandra</b></sub></a><br /></td>
    <td align="center"><a href="https://github.com/abdallahbeshi"><img src="https://avatars.githubusercontent.com/u/51375082?v=4" width="100px;" alt=""/><br /><sub><b>Abdallah Altrabeishi</b></sub></a><br /></td>
    <td align="center"><a href="https://github.com/marektresnak"><img src="https://avatars.githubusercontent.com/u/12021177?v=4" width="100px;" alt=""/><br /><sub><b>Marek Tresnak</b></sub></a><br /></td>
  </tr>
</table>

## 👍 Contribute

If you want to support the development of `Mews.Fiscalizations`, feel free to create a PR with a clear description on what was fixed or introduced.
Also, please make sure to introduce tests when applicable.

## ☕ Donate

There is no need to donate the project, but thanks for considering it! Instead, if you like the project, star it here on GitHub :-) Thanks!

If you still insist on donating, we accept gummy bears at Mews Systems s.r.o., Náměstí IP Pavlova 5, Vinohrady 120 00 Prague. This project was, of course, powered by a huge pile of gummy bears ;-)

## 🏢 About us

We’re building transformational technology for millions of hospitality professionals and their guests.

**Hoteliers**

The hoteliers who choose Mews share our passion for innovation and they don’t accept the status quo. They’re using our technology to rethink physical spaces, services and guest experiences.

**Guests**

Hospitality brands are heavily scrutinized. The day a guest checks-out, their rating goes online. Ultimately we’re designing Mews for our customer’s customer because every guest experience must be remarkable.

More information on https://www.mews.com/en/about-us

## ⚠️ License

[MIT](https://github.com/MewsSystems/fiscalizations/blob/master/LICENSE)
