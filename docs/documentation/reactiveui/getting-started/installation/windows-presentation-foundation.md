---
Order: 7
---
# Windows Presentation Foundation (WPF)

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/platform-wpf/platform-wpf.csproj).

> [!WARNING]
> ReactiveUI drops a target framework after Microsoft ends support for it. .NET 8 and .NET 9 reach
> [end of support](https://learn.microsoft.com/dotnet/core/releases-and-support) on November 10, 2026, and .NET
> Framework 4.6.2 reaches [end of support](https://learn.microsoft.com/lifecycle/end-of-support/end-of-support-2027)
> on January 12, 2027. Plan to move new and existing apps to .NET 10 or later, or to .NET Framework 4.7.2 or later.

Add `ReactiveUI.WPF` to your WPF application project. It brings `ReactiveUI` and, through it,
[ReactiveUI.Binding](../../../binding/index.md).

```bash
dotnet add package ReactiveUI.WPF
```

`ReactiveUI.WPF` also ships as `ReactiveUI.WPF.Reactive`, built from the same source, for an app that uses
System.Reactive.

## Configure ReactiveUI at startup

Call `WithWpf` on the app builder once, in your `Application`'s startup, then show your main window.

```csharp
_ = RxAppBuilder.CreateReactiveUIBuilder().WithWpf().BuildApp();
```

```csharp
MainWindow = new MainWindow();
MainWindow.Show();
```

`WithWpf` registers the WPF view hosts, the activation fetcher and the dispatcher-backed main-thread sequencer.
[RxAppBuilder](../../handbook/rxappbuilder.md) covers the builder itself, and
[WPF](../../handbook/platforms/wpf.md) covers everything you build with the package: showing a router's current
page with `RoutedViewHost`, binding a view to its view model, previewing a view model without navigating to it,
and the pieces `WithWpf` configures for you. A window's or user control's view model implementing `IScreen`
gives it a router to navigate; see [Routing](../../handbook/routing.md).

The [compelling example](../compelling-example.md) walks through a first view model and view. There is no
`ReactiveUI.Validation` code in this installation's example project; see [Validation](../../../validation.md)
for validation rules on a view model.
