---
Order: 2
---
# Installation

> [!WARNING]
> ReactiveUI drops a target framework after Microsoft ends support for it. .NET 8 and .NET 9 reach
> [end of support](https://learn.microsoft.com/dotnet/core/releases-and-support) on November 10, 2026, and .NET
> Framework 4.6.2 reaches [end of support](https://learn.microsoft.com/lifecycle/end-of-support/end-of-support-2027)
> on January 12, 2027. Plan to move new and existing apps to .NET 10 or later, or to .NET Framework 4.7.2 or later.

Install the ReactiveUI package for your UI framework. It brings the core `ReactiveUI` package,
[ReactiveUI.Binding](../../../binding/index.md) for bindings and
[ReactiveUI.SourceGenerators](../../../source-generators/index.md) for properties and commands. One reference is enough
for an app project. A class library that holds only view models references `ReactiveUI`.

```bash
dotnet add package ReactiveUI.WPF
```

The [suite installation guide](../../../getting-started/installation.md) covers the other libraries, and how to pick
between the default packages and the `.Reactive` packages for an app that uses System.Reactive.

## Packages by platform

Each platform page shows how to install the package and start ReactiveUI when your app starts.

| Platform | ReactiveUI package | It brings | NuGet |
|---|---|---|---|
| Class libraries | [ReactiveUI][CoreDoc] | `ReactiveUI.Binding` | [![CoreBadge]][Core] |
| WPF | [ReactiveUI.WPF][WpfDoc] | `ReactiveUI.Binding.Wpf` | [![WpfBadge]][Wpf] |
| Windows Forms | [ReactiveUI.WinForms][WinDoc] | `ReactiveUI.Binding.WinForms` | [![WinBadge]][Win] |
| MAUI | [ReactiveUI.Maui][MauiDoc] | `ReactiveUI.Binding.Maui` | [![MauiBadge]][Maui] |
| WinUI | [ReactiveUI.WinUI][WinUiDoc] | `ReactiveUI.Binding` | [![WinUiBadge]][WinUi] |
| Android (AndroidX) | [ReactiveUI.AndroidX][DroDoc] | `ReactiveUI.Binding` | [![DroXBadge]][DroX] |
| Blazor | [ReactiveUI.Blazor][BlazDoc] | `ReactiveUI.Binding` | [![BlazBadge]][Blaz] |
| Avalonia | [ReactiveUI.Avalonia][AvaDoc] | `ReactiveUI.Binding` | [![AvaBadge]][Ava] |
| Uno Platform | [ReactiveUI.Uno][UnoDoc] | `ReactiveUI.Binding` | [![UnoBadge]][Uno] |
| Unit tests | [ReactiveUI.Testing][TestDoc] | | [![TestBadge]][Test] |

`ReactiveUI.Avalonia` and `ReactiveUI.Uno` live in their own repositories,
[reactiveui/ReactiveUI.Avalonia](https://github.com/reactiveui/ReactiveUI.Avalonia) and
[reactiveui/ReactiveUI.Uno](https://github.com/reactiveui/ReactiveUI.Uno). The other packages come from
[reactiveui/ReactiveUI](https://github.com/reactiveui/ReactiveUI).

[Core]: https://www.nuget.org/packages/ReactiveUI/
[CoreBadge]: https://img.shields.io/nuget/v/ReactiveUI.svg
[CoreDoc]: ../index.md

[Test]: https://www.nuget.org/packages/ReactiveUI.Testing/
[TestBadge]: https://img.shields.io/nuget/v/ReactiveUI.Testing.svg
[TestDoc]: ../../handbook/testing.md

[Wpf]: https://www.nuget.org/packages/ReactiveUI.WPF/
[WpfBadge]: https://img.shields.io/nuget/v/ReactiveUI.WPF.svg
[WpfDoc]: windows-presentation-foundation.md

[WinUi]: https://www.nuget.org/packages/ReactiveUI.WinUI/
[WinUiBadge]: https://img.shields.io/nuget/v/ReactiveUI.WinUI.svg
[WinUiDoc]: winui.md

[Maui]: https://www.nuget.org/packages/ReactiveUI.Maui/
[MauiBadge]: https://img.shields.io/nuget/v/ReactiveUI.Maui.svg
[MauiDoc]: maui.md

[Win]: https://www.nuget.org/packages/ReactiveUI.WinForms/
[WinBadge]: https://img.shields.io/nuget/v/ReactiveUI.WinForms.svg
[WinDoc]: windows-forms.md

[DroX]: https://www.nuget.org/packages/ReactiveUI.AndroidX/
[DroXBadge]: https://img.shields.io/nuget/v/ReactiveUI.AndroidX.svg
[DroDoc]: androidx.md

[Uno]: https://www.nuget.org/packages/ReactiveUI.Uno/
[UnoBadge]: https://img.shields.io/nuget/v/ReactiveUI.Uno.svg
[UnoDoc]: https://platform.uno/blog/getting-started-with-uno-platform-and-reactiveui/

[Blaz]: https://www.nuget.org/packages/ReactiveUI.Blazor/
[BlazBadge]: https://img.shields.io/nuget/v/ReactiveUI.Blazor.svg
[BlazDoc]: blazor.md

[Ava]: https://www.nuget.org/packages/ReactiveUI.Avalonia/
[AvaBadge]: https://img.shields.io/nuget/v/ReactiveUI.Avalonia.svg
[AvaDoc]: avalonia.md

## Bindings come from ReactiveUI.Binding

ReactiveUI runs `WhenAnyValue`, `Bind`, `OneWayBind`, `BindCommand`, `ToProperty` and view location on
ReactiveUI.Binding. You do not add a Binding package yourself. The ReactiveUI package also imports the
`ReactiveUI.Binding` namespace into your project, so your binding calls compile with no new `using`.

ReactiveUI.Binding writes the code for each binding while your project builds. Build once after you install, and read
any warning that starts with `RXUIBIND`. Each one names a binding that gets no generated code. The
[analyzer messages](../../../binding/setup.md#read-the-analyzer-messages) list the fixes.

Moving from an earlier version? The [ReactiveUI.Binding migration guide](../../upgrading/reactiveui-binding-migration.md)
walks through the upgrade.

## Source generators come with ReactiveUI

A **source generator** is a compiler add-on that writes C# code while your project builds.
[ReactiveUI.SourceGenerators](../../../source-generators/index.md) writes reactive properties and commands for you.
ReactiveUI 24.4 and later bring it through `ReactiveUI.Core`, so you do not install it yourself. Every project that
references `ReactiveUI`, `ReactiveUI.Reactive` or a platform package gets its generators and analyzers.

What you get:

- Five attributes: `[Reactive]`, `[ReactiveCommand]`, `[ReactiveCollection]`, `[BindableDerivedList]` and
  `[IReactiveObject]`. Add `using ReactiveUI.SourceGenerators;` to each file that uses them.
- Code that matches the package you reference. A `ReactiveUI` project gets `ReactiveCommand<RxVoid, T>`. A
  `ReactiveUI.Reactive` project gets `ReactiveUI.Reactive.ReactiveCommand<Unit, T>`, with the `Unit` type from
  System.Reactive.
- Analyzers that report an attribute the generators cannot handle.

`[ObservableAsProperty]` and view registration come from ReactiveUI.Binding, which ReactiveUI also brings.

A project that references a ReactiveUI.SourceGenerators version older than 4.0.0 fails to restore with error NU1605.
Remove that `PackageReference`, or set it to 4.0.0 or later.

## Related packages

These packages add to ReactiveUI. Each has its own repository and its own section.

| Package | What it adds |
|---|---|
| [ReactiveUI.Validation](../../../validation.md) | Validation rules for view models |
| [Sextant](../../../sextant.md) | Navigation that starts from the view model |
| [ReactiveUI.Maui.Plugins.Popup](../../../maui-plugins-popup.md) | Reactive popup pages for MAUI |

Xamarin is no longer supported. See [Xamarin to MAUI](../../upgrading/xamarin-to-maui.md).

## A typical solution

```text
.
├── MyApp.Core (class library)
│   └── ReactiveUI
├── MyApp.Wpf
│   └── ReactiveUI.WPF
├── MyApp.WinUI
│   └── ReactiveUI.WinUI
├── MyApp.Maui
│   └── ReactiveUI.Maui
├── MyApp.Avalonia
│   └── ReactiveUI.Avalonia
└── MyApp.Tests
    └── ReactiveUI.Testing
```

[Minimum versions](../minimum-versions.md) lists the platform versions ReactiveUI needs.
