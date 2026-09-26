---
Order: 6
---
# Windows Forms

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/platform-winforms/platform-winforms.csproj).

> [!WARNING]
> ReactiveUI drops a target framework after Microsoft ends support for it. .NET 8 and .NET 9 reach
> [end of support](https://learn.microsoft.com/dotnet/core/releases-and-support) on November 10, 2026, and .NET
> Framework 4.6.2 reaches [end of support](https://learn.microsoft.com/lifecycle/end-of-support/end-of-support-2027)
> on January 12, 2027. Plan to move new and existing apps to .NET 10 or later, or to .NET Framework 4.7.2 or later.

Add `ReactiveUI.WinForms` to your Windows Forms application project. It brings `ReactiveUI` and, through it,
[ReactiveUI.Binding](../../../binding/index.md).

```bash
dotnet add package ReactiveUI.WinForms
```

`ReactiveUI.WinForms` also ships as `ReactiveUI.WinForms.Reactive`, built from the same source, for an app that
uses System.Reactive.

## Configure ReactiveUI at startup

Call `WithWinForms` on the app builder once, in `Main`, before you run your first form. `WithWinFormsScheduler`
sets only the WinForms main-thread sequencer, for an app that registers the platform module another way.

```csharp
IReactiveUIBuilder builder = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWinForms()
    .WithWinFormsScheduler();

_ = builder.BuildApp();
```

`WithWinForms` registers the WinForms platform module, its activation fetcher and its main-thread sequencer.
[RxAppBuilder](../../handbook/rxappbuilder.md) covers the builder itself, and
[Windows Forms](../../handbook/platforms/winforms.md) covers everything you build with the package: a `Form` or
`UserControl` that implements `IViewFor<TViewModel>`, `RoutedControlHost` and `ViewModelControlHost` for showing
a router's current page or previewing a view model, and binding with `Bind`, `OneWayBind` and `BindCommand`.

The [compelling example](../compelling-example.md) walks through a first view model and view. There is no
`ReactiveUI.Validation` code in this installation's example project; see [Validation](../../../validation.md)
for validation rules on a view model.
