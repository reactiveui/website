---
Order: 4
---
# .NET MAUI

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/platform-maui/platform-maui.csproj).

Add `ReactiveUI.Maui` to your MAUI application project. It brings `ReactiveUI` and, through it,
[ReactiveUI.Binding](../../../binding/index.md).

```bash
dotnet add package ReactiveUI.Maui
```

`ReactiveUI.Maui` also ships as `ReactiveUI.Maui.Reactive`, built from the same source, for an app that uses
System.Reactive.

## Configure ReactiveUI at startup

Call `UseReactiveUI` on the `MauiAppBuilder` in `MauiProgram.CreateMauiApp`. Pass it a delegate that configures
the `IReactiveUIBuilder`, the same way `RxAppBuilder` would elsewhere. `WithMauiConverters` here registers
MAUI's boolean-to-visibility converters. A real app's delegate also registers its views and services.

```csharp
_ = mauiBuilder.UseReactiveUI(builder =>
{
    _ = builder.WithMauiConverters();
    delegateRan = true;
});
```

`UseReactiveUI` registers MAUI's platform module, converters and dispatcher-backed main-thread sequencer.
[RxAppBuilder](../../handbook/rxappbuilder.md) covers the builder itself. [.NET MAUI](../../handbook/platforms/maui.md)
covers everything you build with the package. That includes `ReactiveContentPage<T>` and the other reactive page
bases, binding a page to its view model, showing a router's current page, and navigating between pages.

A view model that implements `IScreen` gets a router to navigate with. See [Routing](../../handbook/routing.md).
The [compelling example](../compelling-example.md) walks through a first view model and view. This
installation's example project has no `ReactiveUI.Validation` code. See [Validation](../../../validation.md)
for validation rules on a view model.
