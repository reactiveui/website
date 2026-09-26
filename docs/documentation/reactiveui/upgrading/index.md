---
Order: 4
---
# Upgrading ReactiveUI

ReactiveUI has changed a lot since version 7. This page collects the small, general upgrade steps. Four changes
are big enough for a guide of their own:

- [Migration Guide: RxAppBuilder](rxappbuilder-migration.md) — replacing `RxApp` and manual `Locator` calls with
  the builder pattern.
- [Migration Guide: Bindings on ReactiveUI.Binding](reactiveui-binding-migration.md) — `WhenAnyValue`, `Bind` and
  `ToProperty` moving to a source-generated engine.
- [Migration Guide: Xamarin to .NET MAUI](xamarin-to-maui.md) — moving a Xamarin.Forms app, and its ReactiveUI
  code, onto .NET MAUI.
- [Migration Guide: Routing](routing-migration.md) — `NavigationChanges` giving way to plain streams of values, and
  the `ReactiveUI.Routing` packages going away.

## Supported target frameworks

The core ReactiveUI packages target **.NET 8.0, .NET 9.0, .NET 10.0 and .NET 11.0**, and **.NET Framework
4.6.2 – 4.8.1** on Windows. Some platform packages start at a later version: the MAUI and Android packages target
.NET 10.0 and later. [Minimum versions](../getting-started/minimum-versions.md) lists each package's targets, and
which targets reach end of support soon. The .NET Framework binaries publish as `net462`, `net472` and `net481`, and run on
every release in that range: `net462`, `net47`, `net471`, `net472`, `net48` and `net481` apps all work. NuGet picks
the closest matching target framework moniker at restore time, and .NET Framework's own forward-compatibility does
the rest. ReactiveUI dropped .NET Standard and other older targets. A utility library
that depends on ReactiveUI multi-targets the monikers it needs from that list.

```xml
<PropertyGroup>
  <TargetFrameworks>net8.0;net9.0;net10.0;net11.0;net462;net472;net481</TargetFrameworks>
</PropertyGroup>
<ItemGroup>
  <PackageReference Include="ReactiveUI" Version="*" />
</ItemGroup>
```

## Use PackageReference

A project still on the old `packages.config` format needs to move to `PackageReference`, because ReactiveUI's
packages rely on SemVer 2.0 features that `packages.config` cannot express. Follow Microsoft's
[migration guide](https://docs.microsoft.com/en-us/nuget/reference/migrate-packages-config-to-package-reference)
for the mechanics.

`PackageReference` no longer needs an explicit entry for every dependency. Reference `ReactiveUI` and the
[platform package](../getting-started/installation/index.md) your app needs, and let NuGet pull in the rest.

TIP: if Visual Studio does not offer to upgrade a `packages.config` file, set "Default package management format"
to "PackageReference" under Tools → Options → NuGet Package Manager → General.

## Reference the platform package for your UI framework

WPF, WinForms, WinUI, MAUI, Avalonia and Uno Platform each need their own ReactiveUI package alongside the core
`ReactiveUI` package. Each platform's activation, scheduler and binding-converter code lives apart from the shared
core. [Platform packages](../getting-started/installation/index.md) lists them.

## Subscribe to a command before it runs

`ReactiveCommand<TParam, TResult>.Execute(parameter)` returns an `IObservable<TResult>`. A stream, the type
behind reactive programming, does no work until something subscribes to it. Calling `Execute` builds the
stream; it does not run the command by itself.

**Before**

```csharp
someCommand.Execute();
```

**After**

```csharp
using IDisposable execution = details.OpenPage.Execute().Subscribe();
```

A view's command binding, `BindCommand` and `this.Command = ...` bindings, subscribes for you. Call `Execute`
directly only outside a binding, such as from a test or from another command's implementation.

## Use Interactions instead of UserError

`UserError` is gone. [Interactions](../handbook/interactions/index.md) replace it with a general mechanism for a
view model to ask a view a question and await the answer, such as confirming a destructive action.

## Use DynamicData instead of ReactiveList

`ReactiveList<T>` is gone. [DynamicData](../handbook/collections.md) covers the observable collections that
replace it, including a change set stream you can filter, sort and transform the way you would an `IObservable<T>`.
