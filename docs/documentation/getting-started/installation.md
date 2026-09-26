---
Order: 1
---
# Installation

Every ReactiveUI library ships as NuGet packages. Each library lives in its own repository in the
[reactiveui GitHub organization](https://github.com/reactiveui) and releases on its own schedule. You install only
the libraries you use.

## Install your first package

1. **Pick the libraries you need.** The [package table](#packages-by-library) lists every library, its repository and
   its packages. Most apps start with ReactiveUI and the package for their UI framework.

2. **Pick one stream flavor for the whole app.** A **stream** is a series of values that arrive over time, such as
   each new value of a property. The libraries build their streams on
   [ReactiveUI.Primitives](../primitives/index.md) by default. If your app already uses System.Reactive, install the
   package with `.Reactive` on the end of its name instead. See [Two flavors of each package](#two-flavors-of-each-package).

3. **Add the package for your UI framework.** The UI package brings the core packages with it. For a WPF app:

    ```bash
    dotnet add package ReactiveUI.WPF
    ```

    That one reference brings `ReactiveUI`, `ReactiveUI.Binding`, `ReactiveUI.Binding.Wpf`, `ReactiveUI.Primitives`
    and the [ReactiveUI.SourceGenerators](../source-generators/index.md) generators.

4. **Add the packages for your other projects.** A class library that holds your view models needs only `ReactiveUI`.
   A test project adds `ReactiveUI.Testing`.

    ```bash
    dotnet add package ReactiveUI
    ```

5. **Start the library when your app starts.** Each library has its own setup page. For ReactiveUI, follow the
   [installation guide for your platform](../reactiveui/getting-started/installation/index.md).

## Packages by library

Each row is one GitHub repository. The docs link goes to that library's section of this site.

| Library | Repository | Main package | Other packages | Docs |
|---|---|---|---|---|
| ReactiveUI | [reactiveui/ReactiveUI](https://github.com/reactiveui/ReactiveUI) | `ReactiveUI` | `ReactiveUI.WPF`, `ReactiveUI.WinForms`, `ReactiveUI.WinUI`, `ReactiveUI.Maui`, `ReactiveUI.AndroidX`, `ReactiveUI.Blazor`, `ReactiveUI.Blend`, `ReactiveUI.Drawing`, `ReactiveUI.Testing` | [ReactiveUI](../reactiveui/index.md) |
| ReactiveUI for Avalonia | [reactiveui/ReactiveUI.Avalonia](https://github.com/reactiveui/ReactiveUI.Avalonia) | `ReactiveUI.Avalonia` | `ReactiveUI.Avalonia.Autofac`, `ReactiveUI.Avalonia.DryIoc`, `ReactiveUI.Avalonia.Microsoft.Extensions.DependencyInjection`, `ReactiveUI.Avalonia.Ninject` | [Avalonia](../reactiveui/getting-started/installation/avalonia.md) |
| ReactiveUI for Uno | [reactiveui/ReactiveUI.Uno](https://github.com/reactiveui/ReactiveUI.Uno) | `ReactiveUI.Uno` | | [Uno Platform](https://platform.uno/blog/getting-started-with-uno-platform-and-reactiveui/) |
| ReactiveUI.Binding | [reactiveui/ReactiveUI.Binding.SourceGenerators](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators) | `ReactiveUI.Binding` | `ReactiveUI.Binding.Wpf`, `ReactiveUI.Binding.WinForms`, `ReactiveUI.Binding.Maui` | [ReactiveUI.Binding](../binding/index.md) |
| ReactiveUI.SourceGenerators | [reactiveui/ReactiveUI.SourceGenerators](https://github.com/reactiveui/ReactiveUI.SourceGenerators) | `ReactiveUI.SourceGenerators` | | [ReactiveUI.SourceGenerators](../source-generators/index.md) |
| ReactiveUI.Validation | [reactiveui/ReactiveUI.Validation](https://github.com/reactiveui/ReactiveUI.Validation) | `ReactiveUI.Validation` | `ReactiveUI.Validation.AndroidX` | [ReactiveUI.Validation](../validation.md) |
| Sextant | [reactiveui/Sextant](https://github.com/reactiveui/Sextant) | `Sextant` | `Sextant.Maui`, `Sextant.Avalonia`, `Sextant.Plugins.Popup` | [Sextant](../sextant.md) |
| ReactiveUI.Maui.Plugins.Popup | [reactiveui/Maui.Plugins.Popup](https://github.com/reactiveui/Maui.Plugins.Popup) | `ReactiveUI.Maui.Plugins.Popup` | | [Popups](../maui-plugins-popup.md) |
| Splat | [reactiveui/splat](https://github.com/reactiveui/splat) | `Splat` | Adapters for dependency injection containers and logging frameworks, such as `Splat.Microsoft.Extensions.DependencyInjection` and `Splat.Serilog` | [Splat](../splat/index.md) |
| ReactiveUI.Primitives | [reactiveui/Primitives](https://github.com/reactiveui/Primitives) | `ReactiveUI.Primitives` | `ReactiveUI.Primitives.Async`, `ReactiveUI.Primitives.ObservableEvents`, `ReactiveUI.Disposables`, and a package per UI framework such as `ReactiveUI.Primitives.Wpf` | [ReactiveUI.Primitives](../primitives/index.md) |
| Refit | [reactiveui/refit](https://github.com/reactiveui/refit) | `Refit` | `Refit.HttpClientFactory`, `Refit.Newtonsoft.Json`, `Refit.Xml`, `Refit.Testing` | [Refit](../refit/index.md) |
| Akavache | [reactiveui/Akavache](https://github.com/reactiveui/Akavache) | `Akavache.Sqlite3` | A serializer package, such as `Akavache.SystemTextJson`, and `Akavache.Settings`, `Akavache.Drawing`, `Akavache.EncryptedSqlite3` | [Akavache installation](../akavache/installation.md) |
| Punchclock | [reactiveui/punchclock](https://github.com/reactiveui/punchclock) | `Punchclock` | | [README](https://github.com/reactiveui/punchclock#readme) |
| Fusillade | [reactiveui/Fusillade](https://github.com/reactiveui/Fusillade) | `Fusillade` | | [README](https://github.com/reactiveui/Fusillade#readme) |

## ReactiveUI and ReactiveUI.Binding

ReactiveUI runs its bindings on [ReactiveUI.Binding](../binding/index.md). That covers `WhenAnyValue`, `Bind`,
`OneWayBind`, `BindCommand`, `ToProperty` and view location. ReactiveUI.Binding has its own repository and its own
packages, but you do not add them by hand. Each ReactiveUI package brings the matching Binding package:

| You reference | It brings |
|---|---|
| `ReactiveUI` | `ReactiveUI.Binding` |
| `ReactiveUI.WPF` | `ReactiveUI.Binding.Wpf` |
| `ReactiveUI.WinForms` | `ReactiveUI.Binding.WinForms` |
| `ReactiveUI.Maui` | `ReactiveUI.Binding.Maui` |

`ReactiveUI.Avalonia`, `ReactiveUI.Uno`, `ReactiveUI.WinUI`, `ReactiveUI.AndroidX` and `ReactiveUI.Blazor` bring
`ReactiveUI.Binding` through `ReactiveUI`.

The binding methods live in the `ReactiveUI.Binding` namespace. The ReactiveUI package adds that namespace to your
global usings. If your project turns off implicit usings with `<ImplicitUsings>disable</ImplicitUsings>`, add this
line once:

```csharp
global using ReactiveUI.Binding;
```

ReactiveUI.Binding writes the code for each binding while your project builds. A **source generator** is the part of
the compiler that writes it. The generator also reports a build warning, starting with `RXUIBIND`, for a binding it
cannot handle. Read those warnings after you install or upgrade.

### Upgrading from an earlier ReactiveUI

Your binding code mostly compiles as it is. Some types moved to the `ReactiveUI.Binding` namespace, and a few calls
behave differently. The [ReactiveUI.Binding migration guide](../reactiveui/upgrading/reactiveui-binding-migration.md)
lists every change in five steps.

`[ObservableAsProperty]` comes from ReactiveUI.Binding. For code written against the ReactiveUI.SourceGenerators 3.x
attribute, the migration guide's
[ReactiveUI.SourceGenerators section](../reactiveui/upgrading/reactiveui-binding-migration.md#reactiveuisourcegenerators)
shows the change.

### Using ReactiveUI.Binding without ReactiveUI

ReactiveUI.Binding works on its own, with any class that raises `PropertyChanged`. Add the core package and, for WPF,
WinForms or MAUI, the platform package:

```bash
dotnet add package ReactiveUI.Binding
dotnet add package ReactiveUI.Binding.Wpf
```

Then follow [Setup](../binding/setup.md) to start the library when your app starts.

## ReactiveUI and ReactiveUI.SourceGenerators

[ReactiveUI.SourceGenerators](../source-generators/index.md) writes reactive properties and commands for you while
your project builds. ReactiveUI 24.4 and later bring it through `ReactiveUI.Core`. Every project that references
`ReactiveUI`, `ReactiveUI.Reactive` or a platform package gets its generators and analyzers. You do not add it by hand.

- **What you get:** the `[Reactive]`, `[ReactiveCommand]`, `[ReactiveCollection]`, `[BindableDerivedList]` and
  `[IReactiveObject]` attributes. Add `using ReactiveUI.SourceGenerators;` to each file that uses them. The generators
  write code for the flavor your project references.
- **Remove your own reference.** A project that references a ReactiveUI.SourceGenerators version older than 4.0.0
  fails to restore with error NU1605. Remove that `PackageReference`, or set it to 4.0.0 or later.

## Two flavors of each package

Most packages come in two builds. The default build uses ReactiveUI.Primitives for its streams. The build with
`.Reactive` on the end of its name uses System.Reactive instead. Both expose the same members.

| Default | For an app that uses System.Reactive |
|---|---|
| `ReactiveUI` | `ReactiveUI.Reactive` |
| `ReactiveUI.WPF` | `ReactiveUI.WPF.Reactive` |
| `ReactiveUI.Binding` | `ReactiveUI.Binding.Reactive` |
| `ReactiveUI.Binding.Wpf` | `ReactiveUI.Binding.Wpf.Reactive` |
| `ReactiveUI.Primitives` | `ReactiveUI.Primitives.Reactive` |
| `Akavache.Sqlite3` | `Akavache.Sqlite3.Reactive` |

The same pattern holds for the other ReactiveUI, Binding, Primitives, Avalonia, Uno, Akavache, Punchclock and
Fusillade packages. A `.Reactive` ReactiveUI package brings the `.Reactive` Binding package.

Reference one flavor across the whole app, never both. Both together put two copies of each extension method in
scope, so your calls stop compiling.

## A typical solution

This solution keeps its view models in a class library and has one app project per platform:

```text
.
├── MyApp.Core (class library)
│   └── ReactiveUI
├── MyApp.Wpf
│   └── ReactiveUI.WPF
├── MyApp.Maui
│   └── ReactiveUI.Maui
├── MyApp.Avalonia
│   └── ReactiveUI.Avalonia
└── MyApp.Tests
    └── ReactiveUI.Testing
```

## Supported frameworks

ReactiveUI and ReactiveUI.Binding target .NET 8 to .NET 11, and .NET Framework 4.6.2 to 4.8.1 on Windows. The MAUI
packages start at .NET 10. The other libraries list their own targets on their pages and in their repositories.
[Minimum versions](../reactiveui/getting-started/minimum-versions.md) covers the platform versions ReactiveUI needs.

## Where to go next

- [Get started with ReactiveUI](../reactiveui/getting-started/index.md) and install it for your platform.
- [Set up ReactiveUI.Binding](../binding/setup.md) on its own.
- [Start with ReactiveUI.Primitives](../primitives/index.md) if you only need streams.
