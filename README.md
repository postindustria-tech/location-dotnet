# 51Degrees Geo-Location Engines

![51Degrees](https://51degrees.com/DesktopModules/FiftyOne/Distributor/Logo.ashx?utm_source=github&utm_medium=repository&utm_content=readme_main&utm_campaign=dotnet-open-source "Data rewards the curious") **Pipeline API**

[Developer Documentation](https://51degrees.com/location-dotnet/4.2/index.html?utm_source=github&utm_medium=repository&utm_content=documentation&utm_campaign=dotnet-open-source "developer documentation")
## Introduction

This repository contains engines for the .NET implementation of the Pipeline API 
that provide location services.
Currently, [reverse geocoding](https://en.wikipedia.org/wiki/Reverse_geocoding) 
is supported through the 51Degrees cloud service.

## Pre-requesites

Visual Studio 2019 or later is recommended. Although Visual Studio Code can be used for working with most of the projects.

The Pipeline engines are written in C# and target .NET Standard 2.0.3
Test and example projects target .NET Core 2.1 or 3.1.

## Solutions and projects

- **FiftyOne.GeoLocation** - Geo-location engines and related projects.
  - **FiftyOne.GeoLocation.Core** - Shared classes used by all geo-location engines.
  - **FiftyOne.GeoLocation** - Contains the geo-location pipeline builders.
  - **FiftyOne.GeoLocation.Cloud** - A .NET engine which uses the 51Degrees cloud service to perform reverse geocoding.
  
## Installation

You can either reference the projects in this repository or you can reference the [NuGet][nuget] packages in your project:

```
Install-Package FiftyOne.GeoLocation
```

## Examples

Examples can be found in the `FiftyOne.GeoLocation/Examples/` folder. See below for a list of examples.

|Example|Description|
|-------|-----------|
|GettingStarted|This example uses the 51Degrees cloud to determine the country from a longitude and latidude.|
|CombiningServices|This example uses geo-location alongside device detection to determine the country and device.|

## Tests

Tests can be found in the `Tests/` folder. These can all be run from within Visual Studio.

## Project documentation

For complete documentation on the Pipeline API and associated engines, see the [51Degrees documentation site][Documentation].

[Documentation]: https://51degrees.com/documentation/4.2/index.html
[nuget]: https://www.nuget.org/packages/FiftyOne.GeoLocation/

