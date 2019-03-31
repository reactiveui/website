Title: Installation
Order: 10
---

ReactiveUI packages are now signed by the dotnet foundation. Only builds from their infrastructure carry this seal.  ReactiveUI uses Azure Dev Ops for our CI pipeline. Each platform has packages that extend the base ReactiveUI offerings for it's concerns.  Installing the correct packages for the correct platform can be somewhat of a pain.

| Platform          | ReactiveUI Package                  | NuGet                | [Events][EventsDocs] Package            |
| ----------------- | ----------------------------------- | -------------------- | --------------------------------------- |
| .NET Standard     | [ReactiveUI][CoreDoc]               | [![CoreBadge]][Core] | None                                    |
|                   | [ReactiveUI.Fody][FodyDoc]          | [![FodyBadge]][Fody] | None                                    |
| Unit Testing      | [ReactiveUI.Testing][TestDoc]       | [![TestBadge]][Test] | None                                    |
| Universal Windows | [ReactiveUI][UniDoc]                | [![CoreBadge]][Core] | [ReactiveUI.Events][CoreEvents]         |
| WPF               | [ReactiveUI.WPF][WpfDoc]            | [![WpfBadge]][Wpf]   | [ReactiveUI.Events.WPF][WpfEvents]      |
| Windows Forms     | [ReactiveUI.WinForms][WinDoc]       | [![WinBadge]][Win]   | [ReactiveUI.Events.WinForms][WinEvents] |
| Xamarin.Forms     | [ReactiveUI.XamForms][XamDoc]       | [![XamBadge]][Xam]   | [ReactiveUI.Events.XamForms][XamEvents] |
| Xamarin.Essentials| [ReactiveUI][XamDoc]                | [![CoreBadge]][Core] | [ReactiveUI.Events.XamEssentials][XamE] |
| Xamarin.Android   | [ReactiveUI.AndroidSupport][DroDoc] | [![DroBadge]][Dro]   | [ReactiveUI.Events][CoreEvents]         |
| Xamarin.iOS       | [ReactiveUI][IosDoc]                | [![CoreBadge]][Core] | [ReactiveUI.Events][CoreEvents]         |
| Xamarin.Mac       | [ReactiveUI][MacDoc]                | [![CoreBadge]][Core] | [ReactiveUI.Events][CoreEvents]         |
| Tizen             | [ReactiveUI][CoreDoc]               | [![CoreBadge]][Core] | [ReactiveUI.Events][CoreEvents]         |
| Avalonia          | [Avalonia.ReactiveUI][AvaDoc]       | [![AvaBadge]][Ava]   | None                                    |

[Core]: https://www.nuget.org/packages/ReactiveUI/
[CoreEvents]: https://www.nuget.org/packages/ReactiveUI.Events/
[CoreBadge]: https://img.shields.io/nuget/v/ReactiveUI.svg
[CoreDoc]: https://reactiveui.net/docs/getting-started/installation/

[Fody]: https://www.nuget.org/packages/ReactiveUI.Fody/
[FodyDoc]: https://reactiveui.net/docs/handbook/view-models/#managing-boilerplate-code
[FodyBadge]: https://img.shields.io/nuget/v/ReactiveUI.Fody.svg

[Test]: https://www.nuget.org/packages/ReactiveUI.Testing/
[TestBadge]: https://img.shields.io/nuget/v/ReactiveUI.Testing.svg
[TestDoc]: https://reactiveui.net/docs/handbook/testing/

[UniDoc]: https://reactiveui.net/docs/getting-started/installation/universal-windows-platform

[Wpf]: https://www.nuget.org/packages/ReactiveUI.WPF/
[WpfEvents]: https://www.nuget.org/packages/ReactiveUI.Events.WPF/
[WpfBadge]: https://img.shields.io/nuget/v/ReactiveUI.WPF.svg
[WpfDoc]: https://reactiveui.net/docs/getting-started/installation/windows-presentation-foundation

[Win]: https://www.nuget.org/packages/ReactiveUI.WinForms/
[WinEvents]: https://www.nuget.org/packages/ReactiveUI.Events.WinForms/
[WinBadge]: https://img.shields.io/nuget/v/ReactiveUI.WinForms.svg
[WinDoc]: https://reactiveui.net/docs/getting-started/installation/windows-forms

[Xam]: https://www.nuget.org/packages/ReactiveUI.XamForms/
[XamEvents]: https://www.nuget.org/packages/ReactiveUI.Events.XamForms/
[XamBadge]: https://img.shields.io/nuget/v/ReactiveUI.XamForms.svg
[XamDoc]: https://reactiveui.net/docs/getting-started/installation/xamarin-forms
[XamE]: https://www.nuget.org/packages/ReactiveUI.Events.XamEssentials/

[Dro]: https://www.nuget.org/packages/ReactiveUI.AndroidSupport/
[DroBadge]: https://img.shields.io/nuget/v/ReactiveUI.AndroidSupport.svg
[DroDoc]: https://reactiveui.net/docs/getting-started/installation/xamarin-android

[MacDoc]: https://reactiveui.net/docs/getting-started/installation/xamarin-mac
[IosDoc]: https://reactiveui.net/docs/getting-started/installation/xamarin-ios

[Ava]: https://www.nuget.org/packages/Avalonia.ReactiveUI/
[AvaBadge]: https://img.shields.io/nuget/v/Avalonia.ReactiveUI.svg
[AvaDoc]: https://reactiveui.net/docs/getting-started/installation/avalonia
[EventsDocs]: https://reactiveui.net/docs/handbook/events/

> **Note** ReactiveUI has packages for older .NET versions. Those packages are unlisted from NuGet and not supported, but you can still use them at your own risk to have ReactiveUI running on good old devices, such as Lumias, Surface Hubs, Windows XP, etc. See [Delisting of versions before 8.0.0 from NuGet](https://reactiveui.net/blog/2018/05/delisting-of-versions-before-8-0-0-from-nuget) blog post for more info.

# Release Packages

ReactiveUI is published to [NuGet.org](https://www.nuget.org/packages?q=ReactiveUI) when a release of the software is done. Get email notifications when new release is pushed over at https://libraries.io/nuget/reactiveui

- ReactiveUI
    - [ReactiveUI](https://www.nuget.org/packages/ReactiveUI/) is the base package that has the base platform implementations.
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

Pre-release nuget packages are uploaded to the Reactive UI [MyGet Feed](https://www.myget.org/F/reactiveui/api/v2/package/). You can use this feed to access hotfixes before they are released and to help the maintainers verify resolution to issues.

1. Visit https://www.visualstudio.com/en-us/docs/package/nuget/consume
2. Configure in the following address `https://www.myget.org/F/reactiveui/api/v3/index.json`
