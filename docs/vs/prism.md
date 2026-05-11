---
NoTitle: true
Title: PRISM
Order: 2
---
## Prism vs. (and alongside) ReactiveUI

[Prism](https://github.com/PrismLibrary/Prism) is a mature MVVM framework focused on **navigation, modularity, and region management**. It supports WPF, MAUI, Uno, and Avalonia (via community packages), and ships its own container abstraction (DryIoc by default, with adapters for Unity, MSDI, etc.).

This is not an either/or comparison: **Prism and ReactiveUI are commonly used together**. The interesting question is "which library owns which concern", not "which library wins".

### What Prism is good at

- **Region navigation**: declarative `RegionManager`, named regions in XAML, navigation journals, and `INavigationAware` for parameter passing. Especially strong on WPF where region-based composition is the dominant pattern.
- **`DelegateCommand` / `AsyncDelegateCommand`** with `ObservesProperty` and `ObservesCanExecute` for declarative re-evaluation against `INotifyPropertyChanged`.
- **Modularity**: `IModule` discovery, loading, and lifecycle.
- **`IEventAggregator`** for decoupled pub/sub between modules.
- **Container abstraction** (`IContainerProvider` / `IContainerRegistry`) so module code stays container-agnostic.
- **Prism.Maui** for navigation that's more flexible than raw Shell (modal stacks, complex back-stack manipulation).

### What ReactiveUI is good at

- **Reactive composition** built on Rx — `WhenAnyValue`, `Throttle`, `CombineLatest`, async coordination.
- **`ReactiveCommand`** with `IsExecuting`, `ThrownExceptions`, `CanExecute` exposed as observables (not just an `ICommand`).
- **View-model activation** (`WhenActivated`) to set up and tear down per-view subscriptions cleanly.
- **Strongly-typed bindings** (`this.Bind`, `BindCommand`) that survive renames.
- **Optional view-model-first router** (`IScreen` + `RoutingState`, or [Sextant](../documentation/handbook/sextant/index.md)).

### Using them together

A very common combination is **Prism for navigation + ReactiveUI for view-model logic**. Prism's `INavigationAware` / `IRegionManager` / `IDestructible` lifecycle plays nicely with `ReactiveObject` view-models:

- Inherit your view-models from `ReactiveObject` (or use `ReactiveUI.SourceGenerators`' `[Reactive]` / `[ReactiveCommand]`) and **also** implement Prism's `INavigationAware` if the view-model needs navigation parameters.
- Use Prism's `IRegionManager.RequestNavigate(...)` (or `Prism.Maui`'s `INavigationService.NavigateAsync(...)`) for moving between views. ReactiveUI's `RoutingState` / Sextant is only needed if you want view-model-first routing on top of a non-Prism stack.
- Use `ReactiveCommand` where you want async-aware commanding with `IsExecuting` / `ThrownExceptions`; use `DelegateCommand` where Prism's `ObservesProperty` ergonomics are more convenient.
- Use ReactiveUI's `WhenAnyValue` / `Throttle` / `CombineLatest` for derived state, validation flows, and async coordination inside the view-models; Prism stays out of that layer.

```csharp
public partial class MainViewModel : ReactiveObject, INavigationAware
{
    private readonly INavigationService _nav;

    public MainViewModel(INavigationService nav)
    {
        _nav = nav;

        // ReactiveUI for command + reactive composition
        GoToDetails = ReactiveCommand.CreateFromTask<string>(id =>
            _nav.NavigateAsync($"DetailsView?id={id}").ToTask());

        // Reactive validation
        _isValidHelper = this.WhenAnyValue(x => x.Name, name => !string.IsNullOrWhiteSpace(name))
            .ToProperty(this, nameof(IsValid));
    }

    [Reactive] private string _name = string.Empty;

    private readonly ObservableAsPropertyHelper<bool> _isValidHelper;
    public bool IsValid => _isValidHelper.Value;

    public ReactiveCommand<string, Unit> GoToDetails { get; }

    public void OnNavigatedTo(INavigationParameters parameters)
    {
        if (parameters.TryGetValue("name", out string n)) Name = n;
    }

    public void OnNavigatedFrom(INavigationParameters parameters) { }
    public bool IsNavigationTarget(INavigationParameters parameters) => true;
}
```

### When you might pick one without the other

- **Prism only** — if you want a fully prescribed application shell (regions, modules, region adapters) and your view-models are simple enough that you don't miss reactive composition.
- **ReactiveUI only** — if you want minimal opinions about app structure, want Rx-based view-model logic, and either don't need navigation or are happy with `RoutingState` / Sextant.
- **Both** — if you want Prism's navigation/modularity story and ReactiveUI's reactive composition + async commands. This is the most common combination on serious WPF apps.

### Overlap to watch for

Prism and ReactiveUI both have opinions about a few things — pick one as the owner per row:

| Concern | If Prism owns | If ReactiveUI owns |
|---------|---------------|---------------------|
| Container | `IContainerProvider` / `IContainerRegistry` | Splat (`AppLocator`) + adapter packages |
| Commands | `DelegateCommand` / `AsyncDelegateCommand` | `ReactiveCommand<TParam, TResult>` |
| Navigation | `IRegionManager` / Prism.Maui `INavigationService` | `RoutingState` / Sextant |
| Pub/sub | `IEventAggregator` | `MessageBus` / `Interaction<TInput,TOutput>` |

You don't have to pick the same owner across every row — many teams use Prism for navigation/container and ReactiveUI for commands and view-model composition.
