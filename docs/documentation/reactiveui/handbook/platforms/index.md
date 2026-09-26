---
Order: 16
---
# Platforms

ReactiveUI's core package holds view models, commands, routing and activation, and none of it depends on a UI
framework. A platform package connects that core to one UI framework. It adds:

- base classes for your windows, pages and controls, so each one is an `IViewFor<TViewModel>` with a `ViewModel`
  property the framework can bind to;
- hosts that show a view for a view model: a routed host that follows a router's navigation stack, and a view model host
  that shows whatever view model you give it;
- an activation fetcher, which tells [`WhenActivated`](../when-activated.md) when a view appears on screen and when it
  leaves;
- a main-thread sequencer for the framework's UI thread, so results reach the UI on the right thread
  (see [Scheduling](../scheduling.md));
- a `With<Platform>()` call for the [app builder](../rxappbuilder.md) that registers all of the above.

Each page below starts from a runnable example app. The examples build on any operating system. The Windows ones run on
Windows, and the Android one runs on a device or an emulator.

| Platform | Package | Example app |
| --- | --- | --- |
| [WPF](wpf.md) | `ReactiveUI.WPF`, plus `ReactiveUI.Blend` and `ReactiveUI.Drawing` | A university grade book |
| [Windows Forms](winforms.md) | `ReactiveUI.WinForms` | A library loans desk |
| [WinUI](winui.md) | `ReactiveUI.WinUI` | A weather-station dashboard |
| [.NET MAUI](maui.md) | `ReactiveUI.Maui` | A recipe book |
| [Blazor](blazor.md) | `ReactiveUI.Blazor` | A to-do list |
| [Android](android.md) | `ReactiveUI.AndroidX` | A school timetable |

Each package also ships as `ReactiveUI.<Platform>.Reactive`, built from the same source for apps that use
System.Reactive.

To add a package to a project, follow [Installation](../../getting-started/installation/index.md) for your platform.
