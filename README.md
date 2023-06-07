# NET6 Custom Library
Collection of tools mostly used in my private and/or work projects thus avoiding the duplication of repetitive code.

[![NuGet](https://img.shields.io/nuget/v/NET6CustomLibrary.svg?style=for-the-badge)](https://www.nuget.org/packages/NET6CustomLibrary)
[![NuGet](https://img.shields.io/nuget/dt/NET6CustomLibrary.svg?style=for-the-badge)](https://www.nuget.org/packages/NET6CustomLibrary)
[![GitHub Repo stars](https://img.shields.io/github/stars/angelodotnet/NET6CustomLibrary?style=for-the-badge)](https://github.com/AngeloDotNet/NET6CustomLibrary)
[![GitHub Forks](https://img.shields.io/github/forks/angelodotnet/NET6CustomLibrary?style=for-the-badge)](https://github.com/AngeloDotNet/NET6CustomLibrary)
[![GitHub Issues](https://img.shields.io/github/issues/angelodotnet/NET6CustomLibrary?style=for-the-badge)](https://github.com/AngeloDotNet/NET6CustomLibrary)
[![GitHub Pull Requests](https://img.shields.io/github/issues-pr/angelodotnet/NET6CustomLibrary?style=for-the-badge)](https://github.com/AngeloDotNet/NET6CustomLibrary)
[![GitHub License](https://img.shields.io/github/license/AngeloDotNet/NET6CustomLibrary?style=for-the-badge)](https://github.com/AngeloDotNet/NET6CustomLibrary/blob/main/LICENSE)

## :star: Give a star
If you found this Implementation helpful or used it in your Projects, do give it a :star: on Github. Thanks!

## :dvd: Installation
The library is available on [NuGet](https://www.nuget.org/packages/NET6CustomLibrary) or run the following command in the .NET CLI:

```bash
dotnet add package NET6CustomLibrary
```

## :memo: Library documentation
The extensions methods available regarding:

- [x] Date and Time Only<br>
- [x] DBContext generic methods <b>DEPRECATED</b><br>
- [x] DBContext Pool for different databases <b>DEPRECATED</b><br>
- [x] Health Checks for different databases (without UI)<br>
- [x] Json<br>
- [x] MailKit<br>
- [x] Multi language support<br>
- [x] Redis Cache<br>
- [x] Serilog (save to text file and save to SEQ)<br>
- [x] Swagger UI (different types of configuration)<br>
- [ ] MediatR<br>
- [ ] Scrutor<br>
- [ ] Policy Cors

<b>Note:</b> The methods marked as deprecated will be removed in the next version of the library but they can be used with an updated implementation,
using this library (CustomLibrary.EFCore) already available on [Nuget](https://www.nuget.org/packages/CustomLibrary.EFCore).

The available method interfaces:

- [ ] Fluent Validation<br>
- [ ] Custom Response<br>
- [ ] RabbitMQ<br>
- [ ] Upload Files

The documentation is divided for each extension method, and can be consulted by clicking [here](https://github.com/AngeloDotNet/NET6CustomLibrary/blob/main/src/NET6CustomLibrary/Docs/).
In addition to the implementations listed above, the library includes dependencies to the MassTransit, AutoMapper and SequentialGuid packages.

## :muscle: Contributing
Contributions and/or suggestions are always welcome.

## :beginner: Badges

[![Build and Test](https://github.com/AngeloDotNet/NET6CustomLibrary/actions/workflows/build.yml/badge.svg)](https://github.com/AngeloDotNet/NET6CustomLibrary/actions/workflows/build.yml)
[![Build and Pack on Github](https://github.com/AngeloDotNet/NET6CustomLibrary/actions/workflows/dotnet-github.yml/badge.svg)](https://github.com/AngeloDotNet/NET6CustomLibrary/actions/workflows/dotnet-github.yml)
[![Build and Pack on Nuget](https://github.com/AngeloDotNet/NET6CustomLibrary/actions/workflows/dotnet-nuget.yml/badge.svg)](https://github.com/AngeloDotNet/NET6CustomLibrary/actions/workflows/dotnet-nuget.yml)
