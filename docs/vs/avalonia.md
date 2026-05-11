---
NoTitle: true
Title: Avalonia
Order: 7
---
## ReactiveUI alongside Avalonia

[Avalonia](https://avaloniaui.net) is a cross-platform XAML UI framework — Windows / macOS / Linux / browser (WASM) / iOS / Android. Like MAUI, it is a UI toolkit, not an MVVM library, and ReactiveUI runs on top of it through the [`ReactiveUI.Avalonia`](https://github.com/reactiveui/ReactiveUI.Avalonia) package.

### What Avalonia provides on its own

- **Avalonia XAML and code-first views** (`Window`, `UserControl`, `Control`).
- **Bindings** based on `DataContext` / `INotifyPropertyChanged`, with compiled bindings (`x:CompileBindings="True"`) for refactor safety.
- **Routing infrastructure** in the box only via raw `ContentControl`-style swapping; serious navigation is typically supplied by an MVVM layer or community templates like [Avalonia.ReactiveUI templates](https://github.com/AvaloniaUI/avalonia-samples).
- **DI**: no opinion. You bring your own (often `Microsoft.Extensions.DependencyInjection`).
- **Styling**: Fluent / Simple themes, theme-aware controls.
- **Templates** — the official `avalonia.app` template offers an "Avalonia App (ReactiveUI)" variant out of the box; the non-ReactiveUI variant uses `CommunityToolkit.Mvvm`.

If your app is small and your view-models are mostly properties + commands, Avalonia + `CommunityToolkit.Mvvm` is a complete, supported stack.

### What adding ReactiveUI on top gives you

- **`ReactiveWindow<TViewModel>` / `ReactiveUserControl<TViewModel>`** base classes that already implement `IViewFor<TViewModel>` and give you `WhenActivated` for setup/teardown.
- **`.UseReactiveUI()` AppBuilder extension** that integrates with the modern `RxAppBuilder` to register views, view-models, schedulers and Avalonia-specific services in one place.
- **`RoutedViewHost`** as an Avalonia control — drop it in your XAML and bind to `IScreen.Router` to get view-model-first navigation without rolling your own.
- **Reactive composition** — Avalonia bindings are very capable on their own, but they don't help once you need to throttle a property, merge two observables, or react to async results without piping them back through INPC properties.
- **Async-aware commands** — `ReactiveCommand` with `IsExecuting` / `ThrownExceptions` / `CanExecute` composed as observables.

### How they layer

```text
   View (AXAML / code)             <- Avalonia controls, layouts, styling
   ----------------------------------
   View base class                  <- ReactiveWindow<TViewModel>, ReactiveUserControl<TViewModel>
   ----------------------------------
   Navigation host                  <- RoutedViewHost (optional)
   ----------------------------------
   View-model                       <- ReactiveObject + [Reactive] / [ReactiveCommand] / [ObservableAsProperty]
   ----------------------------------
   DI / bootstrap                   <- AppBuilder.Configure<App>().UseReactiveUI(rxBuilder => rxBuilder.WithAvalonia()...)
                                       (Splat by default; adapter packages for Autofac / DryIoc / MSDI / Ninject / SimpleInjector)
   ----------------------------------
   Platform services                <- Avalonia.Themes.Fluent, Avalonia.Controls, .NET libraries
```

### Partial adoption patterns

- **Just the view-models**: use `ReactiveObject` (and source generators) but stick with vanilla Avalonia `Window`s and the platform's `DataContext` bindings.
- **Just the navigation**: keep your CT.Mvvm view-models, but use `RoutedViewHost` + `IScreen` to manage the navigation stack.
- **Everything**: `.UseReactiveUI()` + `ReactiveWindow<T>` / `ReactiveUserControl<T>` + `ReactiveCommand` + `RoutingState`.

### When to add ReactiveUI to an Avalonia app

Add it when you have any of:

- Multiple async data sources that need to merge into one view-model property.
- Search-as-you-type / debounced validation flows.
- Async commands whose `IsExecuting` / `ThrownExceptions` should influence other commands or controls.
- View-model-first navigation across many screens (where the alternative is hand-rolling a `ContentControl` swapper).

Skip it on small apps where Avalonia bindings + `CommunityToolkit.Mvvm` already cover what you need.

### Templates and samples

The Avalonia team ships ReactiveUI variants in the official templates:

```sh
dotnet new install Avalonia.Templates
dotnet new avalonia.app -o MyApp -p ReactiveUI
```

The [Avalonia samples repository](https://github.com/AvaloniaUI/avalonia-samples) has worked examples for the ReactiveUI integration, and [Camelotia](https://github.com/worldbeater/Camelotia) is a substantial open-source app combining Avalonia + ReactiveUI.
