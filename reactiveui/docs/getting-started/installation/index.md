---
NoTitle: true
Title: Installation
Order: 10
---

ReactiveUI packages are now signed by the dotnet foundation. Only builds from their infrastructure carry this seal.  ReactiveUI uses Azure Dev Ops for our CI pipeline. Each platform has packages that extend the base ReactiveUI offerings for it's concerns.  Installing the correct packages for the correct platform can be somewhat of a pain.


| Platform          | ReactiveUI Package                  | NuGet                  |
| ----------------- | ----------------------------------- | ---------------------- |
| .NET Standard     | [ReactiveUI][CoreDoc]               | [![CoreBadge]][Core]   |
|                   | [ReactiveUI.Fody][FodyDoc]          | [![FodyBadge]][Fody]   |
| .NET              | [ReactiveUI][CoreDoc]               | [![CoreBadge]][Core]   |
|                   | [ReactiveUI.Fody][FodyDoc]          | [![FodyBadge]][Fody]   |
| Unit Testing      | [ReactiveUI.Testing][TestDoc]       | [![TestBadge]][Test]   |
| WPF               | [ReactiveUI.WPF][WpfDoc]            | [![WpfBadge]][Wpf]     |
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
[CoreDoc]: ../../../docs/getting-started/installation/

[Fody]: https://www.nuget.org/packages/ReactiveUI.Fody/
[FodyBadge]: https://img.shields.io/nuget/v/ReactiveUI.Fody.svg
[FodyDoc]: ../../../docs/handbook/view-models/boilerplate-code

[Test]: https://www.nuget.org/packages/ReactiveUI.Testing/
[TestBadge]: https://img.shields.io/nuget/v/ReactiveUI.Testing.svg
[TestDoc]: ../../../docs/handbook/testing/

[Wpf]: https://www.nuget.org/packages/ReactiveUI.WPF/
[WpfBadge]: https://img.shields.io/nuget/v/ReactiveUI.WPF.svg
[WpfDoc]: ../../../docs/getting-started/installation/windows-presentation-foundation

[WinUi]: https://www.nuget.org/packages/ReactiveUI.WinUI/
[WinUiBadge]: https://img.shields.io/nuget/v/ReactiveUI.WinUI.svg
[WinUiDoc]: ../../../docs/getting-started/installation/universal-windows-platform

[Maui]: https://www.nuget.org/packages/ReactiveUI.Maui/
[MauiBadge]: https://img.shields.io/nuget/v/ReactiveUI.Maui.svg
[MauiDoc]: https://blog.jetbrains.com/dotnet/2020/09/18/xamarin-maui-and-the-reactive-mvvm-between-them-webinar-recording/

[Win]: https://www.nuget.org/packages/ReactiveUI.WinForms/
[WinEvents]: https://www.nuget.org/packages/ReactiveUI.Events.WinForms/
[WinBadge]: https://img.shields.io/nuget/v/ReactiveUI.WinForms.svg
[WinDoc]: ../../../docs/getting-started/installation/windows-forms

[Xam]: https://www.nuget.org/packages/ReactiveUI.XamForms/
[XamEvents]: https://www.nuget.org/packages/ReactiveUI.Events.XamForms/
[XamBadge]: https://img.shields.io/nuget/v/ReactiveUI.XamForms.svg
[XamDoc]: ../../../docs/getting-started/installation/xamarin-forms
[Dro]: https://www.nuget.org/packages/ReactiveUI.AndroidSupport/
[DroBadge]: https://img.shields.io/nuget/v/ReactiveUI.AndroidSupport.svg
[DroDoc]: ../../../docs/getting-started/installation/xamarin-android

[DroX]: https://www.nuget.org/packages/ReactiveUI.AndroidX/
[DroXBadge]: https://img.shields.io/nuget/v/ReactiveUI.AndroidX.svg

[MacDoc]: ../../../docs/getting-started/installation/xamarin-mac
[IosDoc]: ../../../docs/getting-started/installation/xamarin-ios

[Uno]: https://www.nuget.org/packages/ReactiveUI.Uno/
[UnoBadge]: https://img.shields.io/nuget/v/ReactiveUI.Uno.svg
[UnoDoc]: https://platform.uno/blog/getting-started-with-uno-platform-and-reactiveui/
[UnoWinUi]: https://www.nuget.org/packages/ReactiveUI.Uno.WinUI/
[UnoWinUiBadge]: https://img.shields.io/nuget/v/ReactiveUI.Uno.WinUI.svg
[UnoWinUiDoc]: https://platform.uno/docs/articles/uwp-vs-winui3.html

[Blaz]: https://www.nuget.org/packages/ReactiveUI.Blazor/
[BlazBadge]: https://img.shields.io/nuget/v/ReactiveUI.Blazor.svg
[BlazDoc]: ../../../docs/getting-started/installation/blazor

[Ava]: https://www.nuget.org/packages/Avalonia.ReactiveUI/
[AvaBadge]: https://img.shields.io/nuget/v/Avalonia.ReactiveUI.svg
[AvaDoc]: ../../../docs/getting-started/installation/avalonia
[EventsDocs]: ../../../docs/handbook/events/

[ValCore]: https://www.nuget.org/packages/ReactiveUI.Validation/
[ValBadge]: https://img.shields.io/nuget/v/ReactiveUI.Validation.svg
[ValDocs]: ../../../docs/handbook/user-input-validation/

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

## Release Packages

ReactiveUI is published to [NuGet.org](https://www.nuget.org/packages?q=ReactiveUI) when a release of the software is done. Get email notifications when new release is pushed over at [reactiveui libraries](https://libraries.io/nuget/reactiveui)

- Splat
    - [Splat](https://www.nuget.org/packages/Splat/) is a library to make things cross-platform that support .NET Standard 2.0
- Splat.Microsoft.Extensions.DependencyInjection
    - [Splat.Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Splat.Microsoft.Extensions.DependencyInjection/) is a library to make things cross-platform that support .NET Standard 2.0
- Splat.Micrsoft.Extensions.Logging
    - [Splat.Microsoft.Extensions.Logging](https://www.nuget.org/packages/Splat.Microsoft.Extensions.Logging) A library to extend the functionality of Splat  
- ReactiveUI
    - [ReactiveUI](https://www.nuget.org/packages/ReactiveUI/) is the base package that has the base platform implementations.
 - ReactiveUI.Blazor
    - [ReactiveUI.Blazor](https://www.nuget.org/packages/ReactiveUI.Blazor/) this package has ReactiveUI platform specific extensions for Blazor
- ReactiveUI.WinForms
    - [ReactiveUI.WinForms](https://www.nuget.org/packages/ReactiveUI.WinForms/) this package has ReactiveUI platform specific extensions for Windows Forms
- ReactiveUI.WPF
    - [ReactiveUI.WPF](https://www.nuget.org/packages/ReactiveUI.WPF/) this package has ReactiveUI platform specific extensions for WPF
- ReactiveUI.XamForms
    - [ReactiveUI.XamForms](https://www.nuget.org/packages/ReactiveUI.XamForms/) this package has ReactiveUI platform specific extensions for Xamarin Forms
- ReactiveUI.Fody
    - [ReactiveUI.Fody](https://www.nuget.org/packages/ReactiveUI.Fody/) this package is a Fody extension that will generate RaisePropertyChange notifications for properties and ObservableAsPropertyHelper properties.

