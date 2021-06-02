# Mews fiscalizations

This repository contains a list of projects where each project supports reporting of e-invoices to the corrosponding country's government authority.
For now, we support reporting of e-invoices in the following countries: Austria, Czech Republic, Germany, Spain, Hungary and Italy.

**Please note that we're not responsible for how these libraries are used and if the data provided is correct or not.**
**Also, it is important to note that these libraries were created to meet our usages, so they might be missing some data that can be important in your case (feel free to extend any library and create a PR).**

P.S Each project folder has it's own documentation (as described below in section **Projects**).

## Builds and tests

| **Project** | **Windows** | **Linux** | **Notes** |
| ----------- | ----------- | --------- | --------- |
| All | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20All%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-all-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20All%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-all-linux.yml) | Runs on ubuntu-18.04 because our Spanish test certificate is not secure enough to run on Ubuntu-latest. Hungarian tests were disabled. |
| Core | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Core%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-core-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Core%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-core-linux.yml) |
| Austria | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Austria%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-austria-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Austria%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-austria-linux.yml) |
| Czechia | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Czechia%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-czechia-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Czechia%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-czechia-linux.yml) |
| Germany | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Germany%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-germany-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Germany%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-germany-linux.yml) |
| Hungary | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Hungary%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-hungary-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Hungary%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-hungary-linux.yml) | Library uses API v2.0, needs to be updated to API v3.0. Tests were disabled. |
| Italy | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Italy%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-italy-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Italy%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-italy-linux.yml) |
| Spain | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Spain%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-spain-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Spain%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-spain-linux.yml) | Runs on ubuntu-18.04 because our test certificate is not secure enough to run on Ubuntu-latest. |

## Projects

[Core](https://github.com/MewsSystems/fiscalizations/tree/master/src/Core)

[Austria](https://github.com/MewsSystems/fiscalizations/tree/master/src/Austria)

[Czechia](https://github.com/MewsSystems/fiscalizations/tree/master/src/Czechia)

[Germany](https://github.com/MewsSystems/fiscalizations/tree/master/src/Germany)

[Hungary](https://github.com/MewsSystems/fiscalizations/tree/master/src/Hungary)

[Italy](https://github.com/MewsSystems/fiscalizations/tree/master/src/Italy)

[Spain](https://github.com/MewsSystems/fiscalizations/tree/master/src/Spain)

## NuGet packages

[Mews.Fiscalizations.All](https://www.nuget.org/packages/Mews.Fiscalizations.All)

[Mews.Fiscalizations.Core](https://www.nuget.org/packages/Mews.Fiscalizations.Core)

[Mews.Fiscalizations.Austria](https://www.nuget.org/packages/Mews.Fiscalizations.Austria)

[Mews.Fiscalizations.Czechia](https://www.nuget.org/packages/Mews.Fiscalizations.Czechia)

[Mews.Fiscalizations.Germany](https://www.nuget.org/packages/Mews.Fiscalizations.Germany)

[Mews.Fiscalizations.Hungary](https://www.nuget.org/packages/Mews.Fiscalizations.Hungary)

[Mews.Fiscalizations.Italy](https://www.nuget.org/packages/Mews.Fiscalizations.Italy)

[Mews.Fiscalizations.Spain](https://www.nuget.org/packages/Mews.Fiscalizations.Spain)

## About us

![Mews](https://user-images.githubusercontent.com/51375082/120492363-4b530f00-c3ba-11eb-8568-6805c3eb7aca.png) 
![Mews](https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png)

We’re building transformational technology for millions of hospitality professionals and their guests.

**Hoteliers**

The hoteliers who choose Mews share our passion for innovation and they don’t accept the status quo. They’re using our technology to rethink physical spaces, services and guest experiences.

**Guests**

Hospitality brands are heavily scrutinized. The day a guest checks-out, their rating goes online. Ultimately we’re designing Mews for our customer’s customer because every guest experience must be remarkable.

More information on https://www.mews.com/en/about-us
