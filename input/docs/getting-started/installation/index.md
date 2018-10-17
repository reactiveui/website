Title: Installation
Order: 10
---

# ReactiveUI Packages

ReactiveUI packages are now signed by the dotnet foundation. Only builds from their infrastructure carry this seal.  ReactiveUI uses Azure Dev Ops for our CI pipeline.

# Install Packages

Each platform has packages that extend the base ReactiveUI offerings for it's concerns.  Installing the correct packages for the correct platform can be somewhat of a pain.

| Target Platform                 | Required ReactiveUI Packages | <a href="https://reactiveui.net/docs/handbook/events/">Events</a> Packages   |
| ------------------------------- | ---------------------------- | ---------------------------- |
| <a href="/">Class library</a> | `ReactiveUI` | None |  
| <a href="universal-windows-platform">Universal Windows Platform</a> | `ReactiveUI` | `ReactiveUI.Events` |
| <a href="windows-presentation-foundation">Windows Presentation Foundation</a> | `ReactiveUI.WPF` | `ReactiveUI.Events.WPF` |
| <a href="windows-forms">Windows Forms</a> | `ReactiveUI.WinForms` | `ReactiveUI.Events.WinForms` |
| <a href="xamarin-forms">Xamarin Forms</a> | `ReactiveUI.XamForms` | `ReactiveUI.Events.XamForms` |
| <a href="xamarin-ios">Xamarin.iOS</a> | `ReactiveUI` | `ReactiveUI.Events` |
| <a href="xamarin-android">Xamarin.Android</a> | `ReactiveUI.AndroidSupport`  | `ReactiveUI.Events`          |
| <a href="xamarin-mac">Xamarin.Mac</a> | `ReactiveUI` | `ReactiveUI.Events` |
| <a href="avalonia">AvaloniaUI</a> | `Avalonia.ReactiveUI` | None |
| <a href="../../handbook/testing/">Unit testing library</a> | `ReactiveUI.Testing` | None |

# Release Packages

ReactiveUI is published to [NuGet.org](https://www.nuget.org/packages?q=ReactiveUI) when a release of the software is done.

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
    - [Reactiveui.Events.XamForms](https://www.nuget.org/packages/ReactiveUI.Events.XamForms/) this package provides Observable-based events API for Xamarin Forms UI controls/eventhandlers.\
- ReactiveUI.Fody
    - [ReactiveUI.Fody](https://www.nuget.org/packages/ReactiveUI.Fody/) this package is a Fody extension that will generate RaisePropertyChange notifications for properties and ObservableAsPropertyHelper properties.

# Development Packages

Pre-release nuget packages are uploaded to the Reactive UI [MyGet Feed](https://www.myget.org/F/reactiveui/api/v2/package/). You can use this feed to access hotfixes before they are released and to help the maintainers verify resolution to issues.

1. Visit https://www.visualstudio.com/en-us/docs/package/nuget/consume
2. Configure in the following address `https://www.myget.org/F/reactiveui/api/v3/index.json`
