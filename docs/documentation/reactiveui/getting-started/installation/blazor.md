---
Order: 3
---
# Blazor

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/platform-blazor/platform-blazor.csproj).

> [!WARNING]
> ReactiveUI drops a target framework after Microsoft ends support for it. .NET 8 and .NET 9 reach
> [end of support](https://learn.microsoft.com/dotnet/core/releases-and-support) on November 10, 2026. Plan to
> move new and existing apps to .NET 10 or later.

Add `ReactiveUI.Blazor` to your Blazor application project. It brings `ReactiveUI` and, through it,
[ReactiveUI.Binding](../../../binding/index.md).

```bash
dotnet add package ReactiveUI.Blazor
```

`ReactiveUI.Blazor` also ships as `ReactiveUI.Blazor.Reactive`, built from the same source, for an app that uses
System.Reactive.

## Configure ReactiveUI at startup

Call `WithBlazor` on a Blazor Server app's builder, or `WithBlazorWasm` on a Blazor WebAssembly app's builder,
once at startup. Each sets the main-thread sequencer for its hosting model and registers Blazor's platform
services.

```csharp
ReactiveUIBuilder builder = RxAppBuilder.CreateReactiveUIBuilder();

_ = builder.WithBlazor();
```

```csharp
ReactiveUIBuilder builder = RxAppBuilder.CreateReactiveUIBuilder();

_ = builder.WithBlazorWasm();
```

`WithBlazorScheduler` and `WithBlazorWasmScheduler` set only the main-thread sequencer, for an app that
registers the platform module another way. [RxAppBuilder](../../handbook/rxappbuilder.md) covers the builder
itself. [Blazor](../../handbook/platforms/blazor.md) covers everything you build with the package. It shows a
view model on a page with `ComponentBase`. It injects a view model instead of receiving it as a parameter, and
it builds a layout or owns a scoped view model around one.

The [compelling example](../compelling-example.md) walks through a first view model and view. There is no
`ReactiveUI.Validation` code in this installation's example project; see [Validation](../../../validation.md)
for validation rules on a view model.
