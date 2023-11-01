---
NoTitle: true
---
ReactiveUI has been going through rapid changes since version 7. 

Here are some guides on how to upgrade to newer features.

## Net Standard 2.0

As of version 8.0 of ReactiveUI, support for [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) will be included. We recommend using .Net Standard in any utility libraries you develop that are meant to be cross platform.

Edit your .Net Standard project file and locate the following:

```xml
<PropertyGroup>
  <TargetFramework>netstandard2.0</TargetFramework>
</PropertyGroup>
<ItemGroup>
  <PackageReference Include="ReactiveUI" Version="9.9.1" />
</ItemGroup>
```

## Use PackageReference NuGet

Older projects need to upgrade from `packages.config` to the newer `PackageReference` format. This is because we rely on SemVer 2.0 features in our packages.

Follow the [Upgrade Guide](https://docs.microsoft.com/en-us/nuget/reference/migrate-packages-config-to-package-reference) on how to upgrade.

If you have any packages that rely on `System.Reactive` less than v4, make sure you include a `PackageReference` to `System.Reactive.Compatibility`.

A tip in the new PackageReference you no longer have to include dependencies such as `System.Reactive` unless you want to deliberately upgrade to a newer version then we have set. We recommend only including `ReactiveUI` and [Platform Packages](~/docs/getting-started/installation/index.md).

TIP: if the upgrade option does not show when right clicking on the `packages.config` file, make sure that PackageReference are your default choice under Tools -> Options -> NuGet Package Manager -> General, You can change the "Default package management format" to "PackageReference".

## Use the platform ReactiveUI NuGet packages for Xamarin Forms, WinForms, WPF, MAUI, Uno Platform, etc

We had to introduce a number of new NuGet packages for various platforms. This is because Visual Studio for Mac would be looking for Windows only symbols if we didn't exclude them from the main package.

See [Platform Package](~/docs/getting-started/installation/index.md) for more details.

NOTE: The Event packages also are separated for some platforms also.

## Use subscribe with ReactiveCommand.Execute()

`ReactiveCommand<TParam, TReturn>.Execute()` is now a IObservable. As such it is now lazy evaluated and won't trigger until you `Subscribe()`.

Before:

```csharp
someCommand.Execute();
```

After:

```csharp
someCommand.Execute(Unit.Default).Subscribe();
```

## Use Interactions instead of UserError

UserError were replaced by a much more generic mechanism called [Interactions](~/docs/handbook/interactions/index.md).


## Use DynamicData instead of ReactiveList

ReactiveList was starting to get rather buggy and a maintenance issue for maintainers.

We decided to recommend [DynamicData](~/docs/handbook/collections.md) as many of the maintainers were already using that library.
