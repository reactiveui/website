---
NoTitle: true
Title: CommunityToolkit.Mvvm
Order: 1
---
## CommunityToolkit.Mvvm vs. ReactiveUI

`CommunityToolkit.Mvvm` (the package formerly known as Microsoft.Toolkit.Mvvm) is the modern, Microsoft-published successor to MvvmLight. It is the default lightweight MVVM choice in many .NET app templates and is the framework most teams compare ReactiveUI against today.

### What CommunityToolkit.Mvvm gives you

A small set of well-engineered primitives, mostly delivered through Roslyn source generators:

- `[ObservableProperty]` on a private field generates a public property with `INotifyPropertyChanged`/`INotifyPropertyChanging` plumbing.
- `[RelayCommand]` on a method generates an `IRelayCommand` (or `IAsyncRelayCommand`) and exposes it as a property.
- `ObservableObject` / `ObservableRecipient` base classes for `INotifyPropertyChanged` and `IMessenger` integration.
- `ObservableValidator` for `INotifyDataErrorInfo` based validation.
- A pub/sub `IMessenger`.

That's the whole surface area. There is no router, no view location, no built-in DI, no reactive composition, and no platform packages.

### ReactiveUI.SourceGenerators — feature-parity code-gen, the ReactiveUI way

[`ReactiveUI.SourceGenerators`](https://github.com/reactiveui/ReactiveUI.SourceGenerators) was originally derived from the CommunityToolkit.Mvvm generator design and offers a comparable surface area, expressed in ReactiveUI idiom:

| CommunityToolkit.Mvvm | ReactiveUI.SourceGenerators | Notes |
|-----------------------|------------------------------|-------|
| `[ObservableProperty]` on a field | `[Reactive]` on a field (or partial property) | RxUI version raises `INotifyPropertyChanging`/`INotifyPropertyChanged` via `ReactiveObject.RaiseAndSetIfChanged`, so the property participates in `WhenAnyValue` and the rest of Rx-aware ReactiveUI machinery. |
| `[RelayCommand]` on a method | `[ReactiveCommand]` on a method | Generates a `ReactiveCommand<TParam, TResult>` (async-aware, with `IsExecuting`, `ThrownExceptions`, `CanExecute` as observables) rather than an `IRelayCommand`. |
| `[NotifyPropertyChangedFor]` / `[NotifyCanExecuteChangedFor]` | `[Reactive(nameof(OtherProperty))]` and `CanExecute` overloads on `[ReactiveCommand]` | Same intent, ReactiveUI semantics. |
| `[ObservableValidator]` + `[NotifyDataErrorInfo(...)]` | `ReactiveValidationObject` + `ValidationRule(...)` in `ReactiveUI.Validation` | RxUI's validation flows through observables; the gen-attribute approach is one option of several. |
| (no equivalent) | `[ObservableAsProperty]` on a method/field/property | Generates an `ObservableAsPropertyHelper<T>` from an `IObservable<T>` — there is no CT.Mvvm equivalent because OAPH is a ReactiveUI concept. |
| (no equivalent) | `[IViewFor(typeof(VM))]` on a view class | Generates the `IViewFor<TViewModel>` plumbing on Windows/MAUI/Avalonia/WinForms views. |

So if the appeal of CT.Mvvm is "I want `[ObservableProperty]` and `[RelayCommand]` and almost no other code", `ReactiveUI.SourceGenerators` gets you the same boilerplate-elimination story while keeping the rest of ReactiveUI available when you need it.

### What ReactiveUI gives you that CommunityToolkit.Mvvm doesn't

- **Reactive composition** — `WhenAnyValue`, `Throttle`, `CombineLatest`, `Select`, `Where`, `Buffer`, `DistinctUntilChanged` and the rest of Rx.NET. Anything you can express as a stream of values composes cleanly.
- **Async-aware commands** — `ReactiveCommand` exposes `IsExecuting`, `ThrownExceptions`, `CanExecute` as observables you can pipe into other commands or into the view.
- **View-model navigation** — `IScreen` + `RoutingState` for a view-model-first router (with [Sextant](../documentation/handbook/sextant/index.md) as a higher-level alternative on top).
- **Service location built in** — Splat ships out of the box and has adapters for every common DI container.
- **Activation lifecycle** — `WhenActivated` / `IActivatableViewModel` for binding setup/teardown.
- **Cross-platform views** — `ReactiveUserControl<T>`, `ReactivePage<T>`, `ReactiveWindow<T>`, `ReactiveContentPage<T>`, `ReactiveActivity<T>`, `ReactiveViewController<T>`, etc.

### What CommunityToolkit.Mvvm does better

- **Smaller surface area to learn.** If your view-models are mostly `T Property { get; set; }` and `void DoTheThing()` then `[ObservableProperty]` + `[RelayCommand]` will be enough and you won't need to learn Rx.
- **No transitive Rx.NET dependency.** ReactiveUI requires `System.Reactive`; CT.Mvvm is dependency-free.
- **Native Visual Studio template integration.** It's in the default WinUI/MAUI templates.

### Mixing them

Nothing about these libraries is mutually exclusive — both target `INotifyPropertyChanged`. You can:

- Use `[ObservableProperty]` for trivial properties and `ReactiveUI.SourceGenerators`' `[Reactive]` for properties where you want Rx ergonomics.
- Use `RelayCommand` for fire-and-forget UI buttons and `ReactiveCommand` for anything that benefits from `IsExecuting` / `ThrownExceptions` / `CanExecute` composition.
- Use CT.Mvvm's `IMessenger` for pub/sub and ReactiveUI's `WhenAnyObservable` / interactions for everything else.

### When to pick which

- Pick **CommunityToolkit.Mvvm** when your view-models are small, you don't need reactive composition, and you want the lowest possible learning curve.
- Pick **ReactiveUI** when you have any of: multiple async sources, derived state, throttling/debouncing, validation that flows from observables, async commands that you compose with each other, or a non-trivial navigation graph.
- Pick **both** when you want code-gen boilerplate elimination for simple properties *and* Rx where it pays off.
