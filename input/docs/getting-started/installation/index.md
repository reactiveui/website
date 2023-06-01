Title: Installation
Order: 10
---

ReactiveUI packages are now signed by the dotnet foundation. Only builds from their infrastructure carry this seal.  ReactiveUI uses Azure Dev Ops for our CI pipeline. Each platform has packages that extend the base ReactiveUI offerings for it's concerns.  Installing the correct packages for the correct platform can be somewhat of a pain.


| Platform          | ReactiveUI Package                  | NuGet                  |
| ----------------- | ----------------------------------- | ---------------------- |
| .NET Standard     | [ReactiveUI][CoreDoc]               | [![CoreBadge]][Core]   |
|                   | [ReactiveUI.Fody][FodyDoc]          | [![FodyBadge]][Fody]   |
| .NET 6            | [ReactiveUI][CoreDoc]               | [![CoreBadge]][Core]   |
|                   | [ReactiveUI.Fody][FodyDoc]          | [![FodyBadge]][Fody]   |
| Unit Testing      | [ReactiveUI.Testing][TestDoc]       | [![TestBadge]][Test]   |
| WPF               | [ReactiveUI.WPF][WpfDoc]            | [![WpfBadge]][Wpf]     |
| UWP               | [ReactiveUI.Uwp][UwpDoc]            | [![UwpBadge]][Uwp]     |
| WinUI             | [ReactiveUI.WinUI][WinUiDoc]        | [![WinUiBadge]][WinUi] |
| MAUI              | [ReactiveUI.Maui][MauiDoc]          | [![MauiBadge]][Maui]   |
| Windows Forms     | [ReactiveUI.WinForms][WinDoc]       | [![WinBadge]][Win]     |
| Xamarin.Forms     | [ReactiveUI.XamForms][XamDoc]       | [![XamBadge]][Xam]     |
| Xamarin.Essentials| [ReactiveUI][XamDoc]                | [![CoreBadge]][Core]   |
| AndroidX (Xamarin)| [ReactiveUI.AndroidX][DroDoc]       | [![DroXBadge]][DroX]   |
| Xamarin.Android   | [ReactiveUI.AndroidSupport][DroDoc] | [![DroBadge]][Dro]     |
| Xamarin.iOS       | [ReactiveUI][IosDoc]                | [![CoreBadge]][Core]   |
| Xamarin.Mac       | [ReactiveUI][MacDoc]                | [![CoreBadge]][Core]   |
| Tizen             | [ReactiveUI][CoreDoc]               | [![CoreBadge]][Core]   |
| Blazor            | [ReactiveUI.Blazor][BlazDoc]        | [![BlazBadge]][Blaz]   |
| Uno               | [ReactiveUI.Uno][UnoDoc]            | [![UnoBadge]][Uno]     |
| Uno WinUI         | [ReactiveUI.Uno.WinUI][UnoWinUiDoc] | [![UnoWinUiBadge]][UnoWinUi] |
| Avalonia          | [Avalonia.ReactiveUI][AvaDoc]       | [![AvaBadge]][Ava]     |
| Any               | [ReactiveUI.Validation][ValDocs]    | [![ValBadge]][ValCore] |

[Core]: https://www.nuget.org/packages/ReactiveUI/
[CoreBadge]: https://img.shields.io/nuget/v/ReactiveUI.svg
[CoreDoc]: https://reactiveui.net/docs/getting-started/installation/

[Fody]: https://www.nuget.org/packages/ReactiveUI.Fody/
[FodyDoc]: https://reactiveui.net/docs/handbook/view-models/boilerplate-code
[FodyBadge]: https://img.shields.io/nuget/v/ReactiveUI.Fody.svg

[Test]: https://www.nuget.org/packages/ReactiveUI.Testing/
[TestBadge]: https://img.shields.io/nuget/v/ReactiveUI.Testing.svg
[TestDoc]: https://reactiveui.net/docs/handbook/testing/

[Wpf]: https://www.nuget.org/packages/ReactiveUI.WPF/
[WpfBadge]: https://img.shields.io/nuget/v/ReactiveUI.WPF.svg
[WpfDoc]: https://reactiveui.net/docs/getting-started/installation/windows-presentation-foundation

[Uwp]: https://www.nuget.org/packages/ReactiveUI.Uwp/
[UwpBadge]: https://img.shields.io/nuget/v/ReactiveUI.Uwp.svg
[UwpDoc]: https://reactiveui.net/docs/getting-started/installation/universal-windows-platform

[WinUi]: https://www.nuget.org/packages/ReactiveUI.WinUI/
[WinUiBadge]: https://img.shields.io/nuget/v/ReactiveUI.WinUI.svg
[WinUiDoc]: https://reactiveui.net/docs/getting-started/installation/universal-windows-platform

[Maui]: https://www.nuget.org/packages/ReactiveUI.Maui/
[MauiBadge]: https://img.shields.io/nuget/v/ReactiveUI.Maui.svg
[MauiDoc]: https://blog.jetbrains.com/dotnet/2020/09/18/xamarin-maui-and-the-reactive-mvvm-between-them-webinar-recording/

[Win]: https://www.nuget.org/packages/ReactiveUI.WinForms/
[WinEvents]: https://www.nuget.org/packages/ReactiveUI.Events.WinForms/
[WinBadge]: https://img.shields.io/nuget/v/ReactiveUI.WinForms.svg
[WinDoc]: https://reactiveui.net/docs/getting-started/installation/windows-forms

[Xam]: https://www.nuget.org/packages/ReactiveUI.XamForms/
[XamEvents]: https://www.nuget.org/packages/ReactiveUI.Events.XamForms/
[XamBadge]: https://img.shields.io/nuget/v/ReactiveUI.XamForms.svg
[XamDoc]: https://reactiveui.net/docs/getting-started/installation/xamarin-forms
[Dro]: https://www.nuget.org/packages/ReactiveUI.AndroidSupport/
[DroBadge]: https://img.shields.io/nuget/v/ReactiveUI.AndroidSupport.svg
[DroDoc]: https://reactiveui.net/docs/getting-started/installation/xamarin-android

[DroX]: https://www.nuget.org/packages/ReactiveUI.AndroidX/
[DroXBadge]: https://img.shields.io/nuget/v/ReactiveUI.AndroidX.svg

[MacDoc]: https://reactiveui.net/docs/getting-started/installation/xamarin-mac
[IosDoc]: https://reactiveui.net/docs/getting-started/installation/xamarin-ios

[Uno]: https://www.nuget.org/packages/ReactiveUI.Uno/
[UnoBadge]: https://img.shields.io/nuget/v/ReactiveUI.Uno.svg
[UnoDoc]: https://platform.uno/blog/getting-started-with-uno-platform-and-reactiveui/
[UnoWinUi]: https://www.nuget.org/packages/ReactiveUI.Uno.WinUI/
[UnoWinUiBadge]: https://img.shields.io/nuget/v/ReactiveUI.Uno.WinUI.svg
[UnoWinUiDoc]: https://platform.uno/docs/articles/uwp-vs-winui3.html

[Blaz]: https://www.nuget.org/packages/ReactiveUI.Blazor/
[BlazBadge]: https://img.shields.io/nuget/v/ReactiveUI.Blazor.svg
[BlazDoc]: https://reactiveui.net/docs/getting-started/installation/blazor

[Ava]: https://www.nuget.org/packages/Avalonia.ReactiveUI/
[AvaBadge]: https://img.shields.io/nuget/v/Avalonia.ReactiveUI.svg
[AvaDoc]: https://reactiveui.net/docs/getting-started/installation/avalonia
[EventsDocs]: https://reactiveui.net/docs/handbook/events/

[ValCore]: https://www.nuget.org/packages/ReactiveUI.Validation/
[ValBadge]: https://img.shields.io/nuget/v/ReactiveUI.Validation.svg
[ValDocs]: https://reactiveui.net/docs/handbook/user-input-validation/

> **Note** ReactiveUI has packages for older .NET versions. Those packages are unlisted from NuGet and not supported, but you can still use them at your own risk to have ReactiveUI running on good old devices, such as Lumias, Surface Hubs, Windows XP, etc. See [Delisting of versions before 8.0.0 from NuGet](https://reactiveui.net/blog/2018/05/delisting-of-versions-before-8-0-0-from-nuget) blog post for more info.

## Example

The following isn't an exhausitve example, as some solutions will follow a different project structure or only use a subset of the platforms, but it should be enough to help with which packages to install on each platform project. **Note** non-platform specific packages (Reactive.Fody etc.) can be installed on any platform; Fody is often included in the core library to automatically generate the INotifyPropertyChanged boilerplate needed in the ViewModels.

<pre>
    .
    ├── {NAME}.Tests.Unit
        ├── ReactiveUI.Testing
    ├── {NAME}.Core (.NetStandard)
        ├── ReactiveUI
        ├── ReactiveUI.Fody
    ├── {NAME}.Droid
        ├── ReactiveUI.AndroidSupport
    ├── {NAME}.AndroidX
        ├── ReactiveUI.AndroidX
    ├── {NAME}.iOS
        ├── ReactiveUI
    ├── {NAME}.Mac
        ├── ReactiveUI
    ├── {NAME}.Forms (Xamarin Forms project)
        ├── ReactiveUI.XamForms
    ├── {NAME}.UWP
        ├── ReactiveUI
    ├── {NAME}.WPF
        ├── ReactiveUI.WPF
    ├── {NAME}.WindowsForms
        ├── ReactiveUI.WinForms
    ├── {NAME}.Tizen
        ├── ReactiveUI
    ├── {NAME}.Uno
        ├── ReactiveUI.Uno
    └── {NAME}.Avalonia
        └── Avalonia.ReactiveUI
</pre>

# Release Packages

ReactiveUI is published to [NuGet.org](https://www.nuget.org/packages?q=ReactiveUI) when a release of the software is done. Get email notifications when new release is pushed over at [reactiveui libraries](https://libraries.io/nuget/reactiveui)

- ReactiveUI
    - [ReactiveUI](https://www.nuget.org/packages/ReactiveUI/) is the base package that has the base platform implementations.
 - ReactiveUI.Blazor
    - [ReactiveUI.Blazor](https://www.nuget.org/packages/ReactiveUI.Blazor/) this package has ReactiveUI platform specific extensions for Blazor
- ReactiveUI.Events
    - [ReactiveUI.Events](https://www.nuget.org/packages/ReactiveUI.Events/) is the base package for ReactiveUI Events as Observables API.
- ReactiveUI.WinForms
    - [ReactiveUI.WinForms](https://www.nuget.org/packages/ReactiveUI.WinForms/) this package has ReactiveUI platform specific extensions for Windows Forms
    - [Reactiveui.Events.WinForms](https://www.nuget.org/packages/ReactiveUI.Events.WinForms/) this package provides Observable-based events API for Win Forms UI controls/eventhandlers.
- ReactiveUI.WPF
    - [ReactiveUI.WPF](https://www.nuget.org/packages/ReactiveUI.WPF/) this package has ReactiveUI platform specific extensions for WPF
    - [Reactiveui.Events.WPF](https://www.nuget.org/packages/ReactiveUI.Events.WPF/) this package provides Observable-based events API for WPF UI controls/eventhandlers.
- ReactiveUI.XamForms
    - [ReactiveUI.XamForms](https://www.nuget.org/packages/ReactiveUI.XamForms/) this package has ReactiveUI platform specific extensions for Xamarin Forms
    - [Reactiveui.Events.XamForms](https://www.nuget.org/packages/ReactiveUI.Events.XamForms/) this package provides Observable-based events API for Xamarin Forms UI controls/eventhandlers.
- ReactiveUI.Fody
    - [ReactiveUI.Fody](https://www.nuget.org/packages/ReactiveUI.Fody/) this package is a Fody extension that will generate RaisePropertyChange notifications for properties and ObservableAsPropertyHelper properties.

# Development Packages

Pre-release nuget packages are uploaded to the Reactive UI [GitHub Packages](https://github.com/orgs/reactiveui/packages). You can use this feed to access hotfixes before they are released and to help the maintainers verify resolution to issues.

1. Visit [visualstudio consume](https://www.visualstudio.com/en-us/docs/package/nuget/consume)
2. Configure in the following address `https://nuget.pkg.github.com/reactiveui/index.json`
