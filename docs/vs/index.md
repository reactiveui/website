---
NoTitle: true
ShowInSidebar: false
Title: ReactiveUI vs.
Order: 5
---

## Where ReactiveUI fits

ReactiveUI is an MVVM library built on top of **System.Reactive** (Rx.NET). It deliberately does *not* ship its own UI toolkit and is not a host or bootstrapper. Instead, it slots **alongside** the UI stack you already use:

- **WPF**, **WinUI 3**, **Windows Forms**, **UWP**
- **.NET MAUI** (including MAUI Shell)
- **Avalonia**
- **Uno Platform**
- **Blazor** (Server and WebAssembly)
- **.NET for Android / iOS / Mac Catalyst / Tizen**
- **.NET Framework 4.6.2 – 4.8.1** (WPF / WinForms)

You don't have to take ReactiveUI as an all-or-nothing dependency. It's a buffet, not a framework lock-in: bring in only `ReactiveObject` for property change notification, add `ReactiveCommand` where you want async-aware commands, layer in `WhenAnyValue` / `WhenActivated` when you need reactive composition, opt into `RoutingState` only if you want view-model-first navigation, and so on. Every piece is designed to coexist with the host platform's own APIs (DataTemplates, dependency injection, Shell routing, Avalonia bindings, Blazor parameters, etc.).

This section compares ReactiveUI against two things that often come up:

1. **Other MVVM libraries** that you would weigh against ReactiveUI when picking *an MVVM layer*. ReactiveUI is genuinely a peer of these.
2. **Raw platform stacks (MAUI Shell, Avalonia)** that already include MVVM-ish primitives. ReactiveUI is *not* a competitor to these — it runs on top of them — but the comparison is useful when deciding whether to add ReactiveUI on top of what the platform already gives you.

## MVVM library comparison

| Library | Reactive composition | View-model navigation | DI integration | Commands | Source generators | Active development |
|---------|:--:|:--:|:--:|:--:|:--:|:--:|
| [ReactiveUI](https://github.com/reactiveui/ReactiveUI) | First-class (Rx-based) | `IScreen` + `RoutingState`, optional [Sextant](../documentation/handbook/sextant/index.md) | Splat (built-in) + adapters for Autofac, DryIoc, MSDI, Ninject, SimpleInjector | `ReactiveCommand<TParam, TResult>` (async-aware, exposes `IsExecuting`, `ThrownExceptions`, `CanExecute`) | [`ReactiveUI.SourceGenerators`](https://github.com/reactiveui/ReactiveUI.SourceGenerators) (`[Reactive]`, `[ObservableAsProperty]`, `[ReactiveCommand]`, `[IViewFor]`) | Yes |
| [CommunityToolkit.Mvvm](community-toolkit-mvvm.md) | Manual (no Rx integration) | None built in | None built in (works with MSDI) | `RelayCommand` / `AsyncRelayCommand` | Built in (`[ObservableProperty]`, `[RelayCommand]`) | Yes |
| [Prism](prism.md) | Limited (event aggregator) | Region navigation (XAML-region-based) | Container abstraction (Unity / DryIoc) | `DelegateCommand` / `AsyncDelegateCommand` | Some via Prism.SourceGenerators | Yes |
| [MvvmCross](mvvmcross.md) | Limited (Mvx.Messenger) | View-model-first | Built-in Mvx IoC | `MvxCommand` / `MvxAsyncCommand` | No | Maintenance mode |
| [Caliburn.Micro](caliburn-micro.md) | None built in | Conductors / Screens / Coroutines | Bootstrapper-driven | `ICommand` via convention or `Task` methods | No | Yes |

> The table is meant to surface differences, not declare a winner. Real projects often mix-and-match — e.g. Prism for region navigation and ReactiveUI for reactive property composition; CommunityToolkit.Mvvm for code-gen and ReactiveUI's `WhenAnyValue` only where you need it.

## ReactiveUI alongside platform stacks

The platform stacks below already give you bindings, navigation, and DI. They are not MVVM libraries you'd pick *instead of* ReactiveUI — they are the host. Each page below explains what the platform provides on its own and what adding ReactiveUI on top gains you.

- [.NET MAUI (with Shell)](maui.md)
- [Avalonia](avalonia.md)

## Conclusion

Pick whichever combination matches the shape of your problem. If you mostly need property-change boilerplate gone and a couple of commands, `CommunityToolkit.Mvvm` is usually enough. If you have multiple async data sources, derived state, throttling/debouncing, validation, or any non-trivial coordination — that's where ReactiveUI starts to pay for itself. And in either case, the host platform's own primitives (Shell routing, Avalonia bindings, MSDI, MAUI Essentials) sit underneath; ReactiveUI is additive.
