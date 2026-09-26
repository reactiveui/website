---
Order: 3
---
# A Compelling Example

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/getting-started/getting-started.csproj).

This page builds a small search screen: a text box, a label that shows whether results are available, and a list
of matches. The reader types a repository name, the screen waits for a pause, then searches GitHub and shows what
it finds. Each result has an **Open** button. The screen is small, but it uses a **view model** (a plain class that
holds a screen's state and logic, with no control in sight) and a stream-driven search. It also uses an **output
property** (a read-only property whose value comes from a stream). A **command** runs an action for a button, with
its own running state and errors. Then a view binds to all of them. The
[view models](../handbook/view-models/index.md),
[commands](../handbook/commands/index.md), [data binding](../handbook/data-binding/index.md) and
[scheduling](../handbook/scheduling.md) pages in the handbook cover each of these in depth; this page only walks
through the one screen.

The example project stands in for a real UI. `TextBox`, `Label`, `ListBox<T>` and `Button` are small classes that
behave like their WPF, MAUI or WinForms namesakes. The same code and the same output work on any platform, and in a
console. `InMemoryGitHubApi` stands in for `api.github.com`, so the walkthrough runs without a network.

## 1. Declare the screen's state

**`AppViewModel` derives from `ReactiveObject`.** That base class raises a change notification each time one of
its properties changes, so a bound view knows to update. `SearchTerm` is a plain read-write property: the view sets
it as the reader types.

`SearchResults`, `IsAvailable` and `ErrorMessage` are output properties instead. Each one is backed by an
`ObservableAsPropertyHelper<T>` field, which keeps the latest value a stream produced and raises the change
notification for you.

```csharp
    /// <summary>The subscriptions the view model owns, disposed with it.</summary>
    private readonly MultipleDisposable _subscriptions = [];

    /// <summary>Backs <see cref="SearchResults"/>.</summary>
    private readonly ObservableAsPropertyHelper<IReadOnlyList<RepositoryDetailsViewModel>> _searchResults;

    /// <summary>Backs <see cref="IsAvailable"/>.</summary>
    private readonly ObservableAsPropertyHelper<bool> _isAvailable;
```

```csharp
    /// <summary>Gets or sets the text in the search box.</summary>
    public string SearchTerm
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = string.Empty;

    /// <summary>Gets the repositories the last search found.</summary>
    public IReadOnlyList<RepositoryDetailsViewModel> SearchResults => _searchResults.Value;

    /// <summary>Gets a value indicating whether search results are available to show.</summary>
    public bool IsAvailable => _isAvailable.Value;

    /// <summary>Gets the message from the last failed search, or an empty string.</summary>
    public string ErrorMessage
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    } = string.Empty;
```

`_subscriptions` is a `MultipleDisposable`, a container that holds several subscriptions so the view model can
dispose all of them together when it is disposed itself. [Disposables](../../primitives/disposables.md) covers it
and the other containers `ReactiveUI.Primitives` offers.

## 2. Build the search pipeline

`WhenAnyValue(x => x.SearchTerm)` turns the `SearchTerm` property into a **stream**: a source of values that
arrive over time, here every value `SearchTerm` ever holds, starting with its current one.
[Observing](../../binding/observing.md) covers `WhenAnyValue` and the other ways to get a stream from a property.
An **operator** is a method that takes a stream and returns another one; the pipeline below chains six of them
before landing on `SearchResults`.

- **`Calm`** waits for a quiet period. It holds a value back, and if a newer one arrives before the wait is over,
  it drops the old one and starts waiting again. It only lets a value through once the reader has stopped typing
  for the `throttle` span. [Time operators](../../primitives/time.md) covers it.
- **`Select`** then trims the term, and **`Unique`** compares it only with the term before it, so typing the same
  text again (after trimming) starts no new search. [Filtering](../../primitives/filtering.md) covers `Unique`.
- **`Where`** drops an empty term, so clearing the box searches for nothing.
- **`Select`** turns each settled term into `Signal.FromAsync(...)`, a stream that runs `SearchRepositoriesAsync`
  and hands it a `CancellationToken`.
- **`SwitchTo`** follows only the newest of those streams. When the reader types again before a search answers,
  `SwitchTo` cancels the one still running and switches to the new search.
  [Transformation](../../primitives/transformation.md) covers it.
- **`WitnessOn`** moves delivery onto a **sequencer**, an object that decides where a piece of work runs.
  `Calm` and everything after it run on a background thread; `RxSchedulers.MainThreadScheduler` is the sequencer
  ReactiveUI keeps for the UI thread, so the result reaches `SearchResults` there.
  [Scheduling](../handbook/scheduling.md) covers sequencers, and [Utility](../../primitives/utility.md#witnesson)
  covers `WitnessOn`.

`ToProperty` ends the chain. It takes `this`, the property's `nameof(...)` and, here, an initial value the property
holds before the first search answers, and it returns the `ObservableAsPropertyHelper<T>` the field stores.

```csharp
        _searchResults = this.WhenAnyValue(x => x.SearchTerm)
            .Calm(throttle)
            .Select(static term => term.Trim())
            .Unique()
            .Where(static term => !string.IsNullOrWhiteSpace(term))
            .Select(term => Signal.FromAsync(token => SearchRepositoriesAsync(term, token)))
            .SwitchTo()
            .WitnessOn(RxSchedulers.MainThreadScheduler)
            .ToProperty(this, nameof(SearchResults), []);
```

```mermaid
%%{init: {"theme": "base", "themeVariables": {"fontFamily": "Roboto, Helvetica, Arial, sans-serif", "fontSize": "15px", "primaryColor": "#DCE9FF", "primaryBorderColor": "#6C8EC4", "primaryTextColor": "#0B2447", "secondaryColor": "#E3F2E8", "secondaryBorderColor": "#7FA88C", "secondaryTextColor": "#12301C", "tertiaryColor": "#F3E5F5", "tertiaryBorderColor": "#A98BB0", "tertiaryTextColor": "#2E1437", "lineColor": "#7B8699", "textColor": "#1B1F27", "noteBkgColor": "#FFF4D6", "noteBorderColor": "#C9A94F", "noteTextColor": "#3A2A00", "actorBkg": "#DCE9FF", "actorBorder": "#6C8EC4", "actorTextColor": "#0B2447", "signalColor": "#7B8699", "signalTextColor": "#1B1F27", "labelBoxBkgColor": "#F1F3F8", "labelBoxBorderColor": "#A7AEBB", "edgeLabelBackground": "#F7F9FC", "clusterBkg": "#F7F9FC", "clusterBorder": "#C9D1DE"}}}%%
flowchart LR
    classDef view fill:#DCE9FF,stroke:#6C8EC4,color:#0B2447
    classDef vm fill:#E3F2E8,stroke:#7FA88C,color:#12301C
    classDef neutral fill:#F1F3F8,stroke:#A7AEBB,color:#1B1F27
    Box(["Search box"]):::view -- "types a term" --> Calm(["Calm: wait for a pause"]):::neutral
    Calm -- "settled term" --> Unique(["Unique: skip repeats"]):::neutral
    Unique -- "new term" --> Search(["Search: cancels the older search"]):::vm
    Search -- "results" --> List(["Results list"]):::view
```

Only a settled, changed, non-empty term starts a search, and only the newest search can finish it. `Calm` and
`Unique` on their own show what they do to typed text: the third search term below matches the second once it is
trimmed, so `Unique` drops it and the request count stays at one.

```csharp
        InMemoryGitHubApi api = new();
        using AppViewModel viewModel = new(api, TimeSpan.FromMilliseconds(50));

        viewModel.SearchTerm = "r";
        viewModel.SearchTerm = "re";
        viewModel.SearchTerm = "react";
        await Task.Delay(150);
        Console.WriteLine(api.RequestCount);

        // Trimmed, this is the same text as the last search, so Unique drops it and no new search runs.
        viewModel.SearchTerm = " react ";
        await Task.Delay(150);
        Console.WriteLine(api.RequestCount);
```

```text
1
1
```

`SwitchTo` protects the results from a slow, stale search. Below, the first search takes 200 ms to answer; the
reader types again after 50 ms, and only the second search's results ever reach `SearchResults`.

```csharp
        InMemoryGitHubApi api = new() { Latency = TimeSpan.FromMilliseconds(200) };
        using AppViewModel viewModel = new(api, TimeSpan.FromMilliseconds(20));

        viewModel.SearchTerm = "reactiveui";
        await Task.Delay(50);
        viewModel.SearchTerm = "refit";
        await Task.Delay(400);

        Console.WriteLine(viewModel.SearchResults.Count);
        Console.WriteLine(viewModel.SearchResults[0].FullName);
```

```text
1
reactiveui/refit
```

## 3. Track availability from the results

`IsAvailable` is a second output property, built from the first one. It watches `SearchResults` and reports
whether the last search found anything.

```csharp
        _isAvailable = this.WhenAnyValue(x => x.SearchResults)
            .Select(static results => results.Count > 0)
            .ToProperty(this, nameof(IsAvailable));
```

`IsAvailable` starts `false`, since no search has run yet, and turns `true` once a search answers with at least
one result. A view can bind it to a spinner, a panel, or the visibility of the results list.

```csharp
        InMemoryGitHubApi api = new();
        using AppViewModel viewModel = new(api, TimeSpan.FromMilliseconds(20));
        Console.WriteLine(viewModel.IsAvailable);

        viewModel.SearchTerm = "splat";
        await Task.Delay(100);

        Console.WriteLine(viewModel.IsAvailable);
```

```text
False
True
```

## 4. Report errors with ThrownExceptions

`SearchRepositoriesAsync` can throw, for example when the GitHub API refuses a request. `ObservableAsPropertyHelper`
never lets an error from its stream reach the property. Instead, it marshals the error to a separate stream,
`ThrownExceptions`. The view model subscribes to that stream once, in its constructor.

```csharp
        _subscriptions.Add(_searchResults.ThrownExceptions.Subscribe(error => ErrorMessage = error.Message));
```

Without that subscription, an error would still be dropped rather than crash the app, but nothing would tell the
reader why the search failed. Here, a rate-limited API turns into a message the view can show.

```csharp
        InMemoryGitHubApi api = new() { RemainingRequests = 0 };
        using AppViewModel viewModel = new(api, TimeSpan.FromMilliseconds(20));

        viewModel.SearchTerm = "akavache";
        await Task.Delay(100);

        Console.WriteLine(viewModel.ErrorMessage);
```

```text
API rate limit exceeded.
```

## 5. Give a result its own view model and command

Each row in the results list is a `RepositoryDetailsViewModel`, built from one `Repository`. `OpenPage` is a
`ReactiveCommand<RxVoid, RxVoid>`: it takes no parameter and produces no result, so both generic arguments are
`RxVoid`, a type that carries no data. `ReactiveCommand.Create` builds one from a plain delegate.

```csharp
    public RepositoryDetailsViewModel(Repository repository)
    {
        FullName = repository.FullName;
        Description = repository.Description;
        ProjectUrl = new Uri($"https://github.com/{repository.FullName}");

        // ReactiveCommand lets us run logic without exposing the implementation to the view. We take no input and
        // return no output, so both generic parameters are RxVoid, a value that carries no data.
        OpenPage = ReactiveCommand.Create(() => Console.WriteLine($"Opening {ProjectUrl}"));
    }

    /// <summary>Gets the owner and name, such as <c>reactiveui/ReactiveUI</c>.</summary>
    public string FullName { get; }

    /// <summary>Gets the one-line description.</summary>
    public string Description { get; }

    /// <summary>Gets the repository's page.</summary>
    public Uri ProjectUrl { get; }

    /// <summary>Gets the command that opens <see cref="ProjectUrl"/>.</summary>
    public ReactiveCommand<RxVoid, RxVoid> OpenPage { get; }
```

`ReactiveCommand` is itself a stream: calling `Execute()` returns one that runs the command and delivers its
result, so `Subscribe()` both starts it and lets you know when it finishes. The
[commands](../handbook/commands/index.md) page covers the command's running state and how it reports its own
errors.

```csharp
        Repository repository = new("reactiveui/ReactiveUI", "An advanced, composable, functional reactive MVVM framework", 8400);
        using RepositoryDetailsViewModel details = new(repository);

        using IDisposable execution = details.OpenPage.Execute().Subscribe();

        Console.WriteLine(details.FullName);
```

```text
Opening https://github.com/reactiveui/ReactiveUI
reactiveui/ReactiveUI
```

## 6. Bind the views

`AppView` and `RepositoryDetailsView` are the screen's two views. Each implements `IViewFor<TViewModel>`: a
`ViewModel` property of the view model's type, so the binding calls below know which object to read from.

```csharp
public sealed class AppView : ReactiveObject, IViewFor<AppViewModel>
{
    /// <summary>Gets the box the user types a search into.</summary>
    public TextBox SearchBox { get; } = new();

    /// <summary>Gets the label shown while search results are available.</summary>
    public Label AvailableLabel { get; } = new();

    /// <summary>Gets the list of repositories found.</summary>
    public ListBox<RepositoryDetailsViewModel> ResultList { get; } = new();

    /// <summary>Gets or sets the view model the window shows.</summary>
    public AppViewModel? ViewModel
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    /// <inheritdoc/>
    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (AppViewModel?)value;
    }
}
```

`Bind` connects `SearchTerm` to the search box both ways, so typing in the box sets the property and setting the
property updates the box. `OneWayBind` connects `SearchResults` and `IsAvailable` one way only, from the view
model to the view, since nothing in the view ever writes to them. Each call returns an `IReactiveBinding<...>`, a
disposable, and a real view disposes these when it goes away. The
[data binding](../handbook/data-binding/index.md) page covers `WhenActivated`, the usual place to create and
dispose bindings together.

```csharp
        InMemoryGitHubApi api = new();
        using AppViewModel viewModel = new(api, TimeSpan.FromMilliseconds(20));
        AppView view = new() { ViewModel = viewModel };

        using IReactiveBinding<AppView, BindingChange> searchBinding = view.Bind(viewModel, x => x.SearchTerm, v => v.SearchBox.Text);
        using IReactiveBinding<AppView, IReadOnlyList<RepositoryDetailsViewModel>> resultsBinding =
            view.OneWayBind(viewModel, x => x.SearchResults, v => v.ResultList.Items);
        using IReactiveBinding<AppView, bool> availableBinding =
            view.OneWayBind(viewModel, x => x.IsAvailable, v => v.AvailableLabel.IsVisible);

        view.SearchBox.Text = "akavache";
        Console.WriteLine(viewModel.SearchTerm);

        await Task.Delay(100);

        Console.WriteLine(view.ResultList.Items.Count);
        Console.WriteLine(view.AvailableLabel.IsVisible);
```

```text
akavache
1
True
```

`RepositoryDetailsView` shows one result. `OneWayBind` again copies the title and description to their labels, and
`BindCommand` connects `OpenPage` to a button: it runs the command when the button is clicked, and it can also
disable the button while the command cannot run, which the [commands](../handbook/commands/index.md) page covers.

```csharp
        Repository repository = new("reactiveui/refit", "The automatic type-safe REST library for .NET", 8900);
        using RepositoryDetailsViewModel details = new(repository);
        RepositoryDetailsView view = new() { ViewModel = details };

        using IReactiveBinding<RepositoryDetailsView, string> titleBinding = view.OneWayBind(details, x => x.FullName, v => v.TitleLabel.Text);
        using IReactiveBinding<RepositoryDetailsView, string> descriptionBinding = view.OneWayBind(details, x => x.Description, v => v.DescriptionLabel.Text);
        using IDisposable commandBinding = view.BindCommand(details, x => x.OpenPage, v => v.OpenButton);

        Console.WriteLine(view.TitleLabel.Text);
        view.OpenButton.PerformClick();
```

```text
reactiveui/refit
Opening https://github.com/reactiveui/refit
```

`ReactiveUI.Binding` and `ReactiveUI.Primitives`, the packages behind the calls on this page, also ship as
`ReactiveUI.Binding.Reactive` and `ReactiveUI.Primitives.Reactive`. Both are built from the same source, for apps
that already schedule with System.Reactive's `IScheduler`.

## At a glance

| Member | What it does |
| --- | --- |
| `WhenAnyValue` | Turns a property into a stream of the values it holds. |
| `Calm` | Waits for a quiet period before letting a value through. |
| `Unique` | Drops a value equal to the one immediately before it. |
| `SwitchTo` | Follows only the newest of a stream of streams, dropping the rest. |
| `WitnessOn` | Delivers values through a sequencer, such as the UI thread. |
| `ToProperty` | Backs a read-only property with the latest value a stream produced. |
| `ThrownExceptions` | The stream an output property's own errors arrive on. |
| `ReactiveCommand.Create` | Builds a command from a plain delegate. |
| `Bind` | Keeps a view-model property and a control property equal, both ways. |
| `OneWayBind` | Copies a view-model property to a control property. |
| `BindCommand` | Runs a command when a control is activated. |

Every full-sized screen you write with ReactiveUI is built from these same pieces. A view model turns its input
into a pipeline, output properties hold what the pipeline produces, commands run what the reader asks for, and
bindings keep the view in step. The [handbook](../handbook/index.md) covers each piece on its own page, with more
operators, more binding calls and the platform-specific detail this page left out.
