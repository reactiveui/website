---
NoTitle: true
Title: MvvmCross
Order: 3
---
## MvvmCross vs. ReactiveUI

[MvvmCross](https://www.mvvmcross.com/) is a long-running cross-platform MVVM framework, historically the dominant choice for Xamarin native (iOS/Android/macOS/Windows) before MAUI. It's still maintained today and supports MAUI + WPF + Avalonia in addition to the platforms it grew up on.

### Approach

MvvmCross is **opinionated and prescriptive**: it owns the app's bootstrap (`MvxApplication`, `MvxSetup`), the navigation model (view-model-first via `IMvxNavigationService`), and the container (its own `Mvx.IoCProvider`). View bindings use MvvmCross's own fluent binding DSL or a convention-based tag syntax in XAML/AXML.

ReactiveUI, by contrast, is unopinionated about app structure. It plugs into whatever host you already have (MAUI's `MauiAppBuilder`, Avalonia's `AppBuilder`, WPF's `App.OnStartup`) and gives you reactive composition + commands on top.

### Differences at a glance

| Aspect | MvvmCross | ReactiveUI |
|--------|-----------|------------|
| Scope | Full app shell + navigation + container + binding | MVVM building blocks on top of the host platform |
| Container | Built-in `Mvx.IoCProvider` | Splat by default; adapters for Autofac / DryIoc / MSDI / Ninject / SimpleInjector |
| Navigation | `IMvxNavigationService` (view-model-first) | `IScreen` + `RoutingState`, or [Sextant](../documentation/handbook/sextant/index.md); or use the host's native navigation |
| Binding | Fluent / convention-based MvvmCross bindings | `this.Bind`, `BindCommand`, `OneWayBind` (strongly-typed Expressions); plus the host platform's bindings |
| Reactive composition | Not first-class (some Rx hooks exist) | First-class via Rx.NET |
| Commands | `MvxCommand` / `MvxAsyncCommand` | `ReactiveCommand<TParam, TResult>` with `IsExecuting` / `ThrownExceptions` |
| Activity / lifecycle | `IMvxViewModel`'s `Start`, `ViewAppearing`, etc. | `WhenActivated` + `IActivatableViewModel` |
| Native platforms | Strong (Xamarin native heritage) | Strong via `ReactiveUI` + `ReactiveUI.AndroidX` + native iOS/MacCatalyst support |

### Using them together

MvvmCross is "all the way down" — it owns navigation, container, and bindings. Mixing it with ReactiveUI is possible but rarely worth the impedance mismatch, because both libraries want to own commands and (optionally) navigation.

If a team has an existing MvvmCross codebase and wants reactive composition, the lightest-touch combination is:

- Keep MvvmCross for bootstrap / navigation / container / bindings.
- Use `System.Reactive` directly inside view-models for derived state, throttling, async coordination.
- Skip `ReactiveCommand` and `RoutingState` to avoid two parallel command/navigation stacks.

For greenfield projects today, the more common pairings are **ReactiveUI on raw MAUI/Avalonia** or **ReactiveUI + Prism** rather than ReactiveUI + MvvmCross.

### When to pick MvvmCross

- You have an existing MvvmCross codebase that works well.
- You want a single framework that prescribes app structure end-to-end (bootstrap, navigation, container, bindings).
- You're targeting native Xamarin-iOS / Xamarin-Android (now .NET for iOS / .NET for Android) without a cross-platform UI layer and want a battle-tested navigation story.

### When to pick ReactiveUI

- You want to keep the host platform's own bootstrap, container, and navigation, and only add an MVVM layer for reactive composition and async commands.
- You're using MAUI, Avalonia, WPF, WinUI, Blazor, or Uno and either don't need a custom router or are happy with `RoutingState` / Sextant.
- You want reactive composition (`WhenAnyValue`, `Throttle`, `CombineLatest`) as a first-class citizen.
