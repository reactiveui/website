---
NoTitle: true
Title: .NET MAUI (with Shell)
Order: 6
---
## ReactiveUI alongside .NET MAUI

.NET MAUI is a UI toolkit, not an MVVM library. ReactiveUI is not a competitor to MAUI — it runs on top of MAUI through the `ReactiveUI.Maui` package and complements what the platform already gives you.

### What MAUI provides on its own

- **XAML and code-first views** (`ContentPage`, `ContentView`, `Shell`).
- **`Shell` routing**: URI-based navigation, flyouts, tabs, route registration via `Routing.RegisterRoute(...)` and navigation via `Shell.Current.GoToAsync("//route?param=value")`.
- **Bindings** based on `BindingContext` and `INotifyPropertyChanged` — including string-path bindings, value converters, and compiled XAML bindings.
- **Dependency injection** through `MauiAppBuilder.Services` (Microsoft.Extensions.DependencyInjection).
- **Essentials** (`Preferences`, `SecureStorage`, `Connectivity`, etc.).
- **CommunityToolkit.Mvvm / Maui** packages from Microsoft that fill in `[ObservableProperty]`, `[RelayCommand]`, behaviors, and converters.

If your view-models are mostly `T Property { get; set; }` with a few `[RelayCommand]` methods, and your navigation fits Shell's URI model, you do not need ReactiveUI at all. Plain MAUI + `CommunityToolkit.Mvvm` is a perfectly good stack.

### What adding ReactiveUI on top gives you

- **Reactive composition** — `WhenAnyValue`, `Throttle`, `CombineLatest`, `DistinctUntilChanged`, etc. Anything you'd express as "when X changes, recompute Y after a debounce, combined with Z" stops needing event handlers.
- **Async-aware commands** — `ReactiveCommand` exposes `IsExecuting`, `ThrownExceptions`, and `CanExecute` as observables. Disabling a button while another command is running is one line.
- **Activation lifecycle** — `WhenActivated` (via `ReactiveContentPage<TViewModel>` / `ReactiveFlyoutPage<TViewModel>` / `ReactiveShell`) gives you a single place to wire up bindings that auto-dispose when the page goes away — without writing `OnAppearing` / `OnDisappearing` boilerplate.
- **Strongly-typed bindings** — `this.Bind`, `this.OneWayBind`, `this.BindCommand` take Expressions, so a rename in the view-model fails the build.
- **View-model-first navigation when Shell doesn't fit** — for flows that don't map well to URIs (modal stacks, popups, complex back-stack manipulation), [Sextant](../documentation/handbook/sextant/index.md) (with `Sextant.Maui` and optionally `Sextant.Plugins.Popup`) gives you a view-model-first router that interoperates with Shell.

### How they layer

```text
   View (XAML / code)              <- MAUI pages, layouts, controls
   ----------------------------------
   View base class                  <- ReactiveContentPage<TViewModel>, ReactiveFlyoutPage<TViewModel>, ReactiveShell, ...
   ----------------------------------
   View-model                       <- ReactiveObject + [Reactive] / [ReactiveCommand] / [ObservableAsProperty]
   ----------------------------------
   DI / bootstrap                   <- MauiAppBuilder.Services + UseMicrosoftDependencyResolver()
                                       + RxAppBuilder.CreateReactiveUIBuilder()...WithMaui().BuildApp()
   ----------------------------------
   Platform services                <- MAUI Essentials, Shell, .NET libraries
```

You can opt in to as much or as little of this stack as you want. Common partial-adoption patterns:

- Only `ReactiveObject` + `[Reactive]` for property change notification, leave Shell to do navigation.
- Everything from view-model down (reactive properties, commands, OAPH), but keep Shell routing as the source of truth for which page is shown.
- Full ReactiveUI navigation via Sextant when Shell's URI routing isn't a good fit (e.g. modal stacks, dynamic back-stack edits).

### Shell-specific notes

- `Shell.Current.GoToAsync(...)` plays nicely with `ReactiveContentPage<TViewModel>`s registered via `Routing.RegisterRoute(...)`. ReactiveUI doesn't intercept Shell routing — you keep using `GoToAsync`.
- Shell's query parameter injection (`[QueryProperty]`) works on a `ReactiveObject` view-model — declare the attribute on the page or the view-model as usual.
- If you mix Sextant for some flows and Shell for others, agree on which one owns the back-stack for a given screen. The two routers do not coordinate automatically.

### Migrating from Xamarin.Forms

If you're coming from a Xamarin.Forms + ReactiveUI app, the migration is mostly a `xmlns` / package rename plus the `MauiProgram.cs` bootstrap pattern. See the dedicated [Xamarin to MAUI](../documentation/upgrading/xamarin-to-maui.md) guide.

### When to add ReactiveUI to a MAUI app

Add it when one or more of these are true:

- You have **derived state** (Property C depends on A and B and an async fetch).
- You need **debouncing / throttling** on inputs (search-as-you-type, validation that hits a server).
- You have **async commands that interact** (one disables the other while it's running; failure of one feeds into another).
- You have **non-trivial navigation** that doesn't fit Shell's URI model.

Skip it when your view-models are mostly setter-only properties and ICommand-shaped buttons.
