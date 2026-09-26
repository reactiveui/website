---
Order: 8
---
# WinUI 3

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/platform-winui/platform-winui.csproj).

> [!WARNING]
> ReactiveUI drops a target framework after Microsoft ends support for it. .NET 8 and .NET 9 reach
> [end of support](https://learn.microsoft.com/dotnet/core/releases-and-support) on November 10, 2026. Plan to
> move new and existing apps to .NET 10 or later.

Add `ReactiveUI.WinUI` to your WinUI 3 application project. It brings `ReactiveUI` and, through it,
[ReactiveUI.Binding](../../../binding/index.md).

```bash
dotnet add package ReactiveUI.WinUI
```

`ReactiveUI.WinUI` also ships as `ReactiveUI.WinUI.Reactive`, built from the same source, for an app that uses
System.Reactive.

## Configure ReactiveUI at startup

Call `WithWinUI` on the app builder once, in your `Application`'s `OnLaunched`, before you create your main
window. `RegisterView<TView, TViewModel>` registers a view alongside it, so the router or a view host can
resolve it.

```csharp
_ = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWinUI()
    .RegisterView<StationListPageView, StationListPageViewModel>()
    .RegisterView<StationDetailPageView, StationDetailPageViewModel>()
    .RegisterView<AlertView, AlertViewModel>()
    .ConfigureViewLocator(static locator => locator.Map<SensorMaintenanceViewModel, SensorMaintenanceView>())
    .BuildApp();
```

`ConfigureViewLocator` maps a view to its view model by hand, for a view that implements only the non-generic
`IViewFor`.

`WithWinUI` registers the WinUI view hosts, the activation fetcher and the dispatcher-backed main-thread
sequencer. [RxAppBuilder](../../handbook/rxappbuilder.md) covers the builder and `RegisterView` in depth, and
[WinUI 3](../../handbook/platforms/winui.md) covers everything you build with the package: `ReactivePage<T>`
and `ReactiveUserControl<T>`, showing a router's current page, and binding with `Bind`, `OneWayBind` and
`BindCommand`.

The [compelling example](../compelling-example.md) walks through a first view model and view. There is no
`ReactiveUI.Validation` code in this installation's example project; see [Validation](../../../validation.md)
for validation rules on a view model.
