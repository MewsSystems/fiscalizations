# Mews fiscalizations

This repository contains multiple projects. Each project supports reporting of e-invoices to the corresponding country's government authority. Each project folder has it's own documentation. Use the link in the table below to get there.

**Please note that we're not responsible for how these libraries are used and if the data provided is correct or not.**
**Also, it is important to note that these libraries were created to meet our usages, so they might be missing some data that can be important in your case (feel free to extend any library and create a PR).**

## Projects

| **Project** | **Nuget Package** | **Windows Build** | **Linux Build** | **Notes** |
| ----------- | ----------------- | ----------------- | --------------- | --------- |
| All                                                                              | [Mews.Fiscalizations.All](https://www.nuget.org/packages/Mews.Fiscalizations.All) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20All%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-all-windows.yml)         | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20All%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-all-linux.yml)         | Linux build runs on ubuntu-18.04 because of Spanish project (see below). |
| [Core](https://github.com/MewsSystems/fiscalizations/tree/master/src/Core)       | [Mews.Fiscalizations.Core](https://www.nuget.org/packages/Mews.Fiscalizations.Core) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Core%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-core-windows.yml)       | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Core%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-core-linux.yml)       |
| [Austria](https://github.com/MewsSystems/fiscalizations/tree/master/src/Austria) | [Mews.Fiscalizations.Austria](https://www.nuget.org/packages/Mews.Fiscalizations.Austria) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Austria%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-austria-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Austria%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-austria-linux.yml) |
| [Czechia](https://github.com/MewsSystems/fiscalizations/tree/master/src/Czechia) | [Mews.Fiscalizations.Czechia](https://www.nuget.org/packages/Mews.Fiscalizations.Czechia) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Czechia%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-czechia-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Czechia%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-czechia-linux.yml) |
| [Germany](https://github.com/MewsSystems/fiscalizations/tree/master/src/Germany) | [Mews.Fiscalizations.Germany](https://www.nuget.org/packages/Mews.Fiscalizations.Germany) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Germany%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-germany-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Germany%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-germany-linux.yml) |
| [Hungary](https://github.com/MewsSystems/fiscalizations/tree/master/src/Hungary) | [Mews.Fiscalizations.Hungary](https://www.nuget.org/packages/Mews.Fiscalizations.Hungary) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Hungary%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-hungary-windows.yml) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Hungary%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-hungary-linux.yml) |
| [Italy](https://github.com/MewsSystems/fiscalizations/tree/master/src/Italy)     | [Mews.Fiscalizations.Italy](https://www.nuget.org/packages/Mews.Fiscalizations.Italy) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Italy%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-italy-windows.yml)     | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Italy%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-italy-linux.yml)     |
| [Spain](https://github.com/MewsSystems/fiscalizations/tree/master/src/Spain)     | [Mews.Fiscalizations.Spain](https://www.nuget.org/packages/Mews.Fiscalizations.Spain) | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Spain%20(Windows)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-spain-windows.yml)     | [![Build](https://img.shields.io/github/workflow/status/MewsSystems/fiscalizations/Build%20and%20test%20-%20Spain%20(Linux)/master)](https://github.com/MewsSystems/fiscalizations/actions/workflows/build-and-test-spain-linux.yml)     | Linux build runs on ubuntu-18.04 because our test certificate is not secure enough to run on Ubuntu-latest. |

## About us

![Mews](https://user-images.githubusercontent.com/51375082/120493257-16938780-c3bb-11eb-8cb5-0b56fd08240d.png)

We’re building transformational technology for millions of hospitality professionals and their guests.

**Hoteliers**

The hoteliers who choose Mews share our passion for innovation and they don’t accept the status quo. They’re using our technology to rethink physical spaces, services and guest experiences.

**Guests**

Hospitality brands are heavily scrutinized. The day a guest checks-out, their rating goes online. Ultimately we’re designing Mews for our customer’s customer because every guest experience must be remarkable.

More information on https://www.mews.com/en/about-us
