# Operators in ReactiveUI

An **operator** is a method that makes a new stream from an existing one: it filters values, changes them, combines
streams, or waits. You chain operators the way you chain LINQ methods. ReactiveUI adds a few of its own that connect
streams to properties and commands.

The examples use these namespaces:

```csharp
using ReactiveUI;
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
```

## Your first pipeline

A search box should run a search once the user stops typing, and only for two characters or more.

```csharp
public sealed class SearchViewModel : ReactiveObject
{
    private string _searchText = "";

    public SearchViewModel()
    {
        Search = ReactiveCommand.Create<string>(text => Console.WriteLine($"searching for {text}"));

        this.WhenAnyValue(x => x.SearchText)
            .Calm(TimeSpan.FromMilliseconds(300))
            .Where(text => text.Length >= 2)
            .InvokeCommand(Search);
    }

    public string SearchText { get => _searchText; set => this.RaiseAndSetIfChanged(ref _searchText, value); }

    public ReactiveCommand<string, RxVoid> Search { get; }
}
```

**1. Start from a property.** `WhenAnyValue` sends `SearchText` each time it changes.

**2. Wait for a pause.** `Calm` sends a value only after 300 ms pass with no newer one.

**3. Filter.** `Where` drops text shorter than two characters.

**4. Run the command.** `InvokeCommand` executes `Search` with each value, when the command can run.

```csharp
var search = new SearchViewModel();

search.SearchText = "rx";
search.SearchText = "reactiveui";
```

Output, 300 ms later:

```text
searching for reactiveui
```

`rx` never reached the command: `reactiveui` replaced it within 300 ms.

## ReactiveUI's operators

### `ToProperty`

`ToProperty` turns a stream into a read-only property. It stores the latest value in an
`ObservableAsPropertyHelper<T>` and raises `PropertyChanged` for the property each time the value changes.

```csharp
public sealed class NameViewModel : ReactiveObject
{
    private readonly ObservableAsPropertyHelper<string> _fullName;
    private string _firstName = "";
    private string _lastName = "";

    public NameViewModel() =>
        _fullName = this.WhenAnyValue(x => x.FirstName, x => x.LastName, (first, last) => $"{first} {last}")
                        .ToProperty(this, x => x.FullName);

    public string FirstName { get => _firstName; set => this.RaiseAndSetIfChanged(ref _firstName, value); }

    public string LastName { get => _lastName; set => this.RaiseAndSetIfChanged(ref _lastName, value); }

    public string FullName => _fullName.Value;
}
```

```csharp
var named = new NameViewModel();
named.FirstName = "Grace";
named.LastName = "Hopper";

Console.WriteLine(named.FullName);
```

Output:

```text
Grace Hopper
```

The source generators can write this for you with `[ObservableAsProperty]`. See
[ObservableAsPropertyHelper](../handbook/observable-as-property-helper.md).

### `InvokeCommand`

`InvokeCommand` executes a command with each value from a stream, and skips values that arrive while the command
cannot run. [Your first pipeline](#your-first-pipeline) shows it. See [commands](../handbook/commands/index.md).

### `WhenAnyValue` and `WhenAnyObservable`

`WhenAnyValue` makes a stream from properties. `WhenAnyObservable` follows a property that itself holds a stream, such
as a command on a child view model. See [WhenAny](../handbook/when-any.md).

## Every other operator

The rest come from ReactiveUI.Primitives. Each page covers every operator in its group, with an example.

| You want to | Operators | Page |
|---|---|---|
| Change each value, or flatten a stream of streams | `Select`, `SelectMany`, `Fold`, `SwitchTo` | [Transformation](../primitives/transformation.md) |
| Drop values you do not want | `Where`, `Unique`, `Take`, `Skip`, `TakeUntil` | [Filtering](../primitives/filtering.md) |
| Join streams | `SyncLatest`, `Blend`, `Zip`, `Concat`, `Race` | [Combination](../primitives/combination.md) |
| Wait, batch, sample or time out | `Calm`, `Shift`, `Buffer`, `Probe`, `Expire` | [Time](../primitives/time.md) |
| Recover from a failure | `Recover`, `Retry`, `Reattempt`, `Finally` | [Error handling](../primitives/error-handling.md) |
| Reduce a stream to one answer | `Aggregate`, `Count`, `Any`, `ToList` | [Aggregation and results](../primitives/aggregation.md) |
| Peek at values, or choose a thread | `Tap`, `WitnessOn`, `Serialize` | [Utility](../primitives/utility.md) |
| Share one run between subscribers | `Publish`, `Replay` | [Sharing one subscription](../primitives/sharing.md) |

For a search that also cancels a stale request, see
[a search box, two ways](../primitives/why-primitives.md#a-search-box-two-ways).

## Related topics

- [Streams in ReactiveUI](observables.md)
- [Best practices](../primitives/best-practices.md)
- [ReactiveUI.Primitives and System.Reactive](../primitives/system-reactive.md), for the System.Reactive names of these
  operators.
