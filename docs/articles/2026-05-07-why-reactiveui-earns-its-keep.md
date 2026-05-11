---
NoTitle: true
IsBlog: true
Title: Why ReactiveUI Earns Its Keep
Tags: Article
Author: Glenn Watson
Published: 2026-05-07
Order: 7
---
# Why ReactiveUI Earns Its Keep

In 2017 Eric Sink wrote a piece with the deliberately sardonic title [*"I have become a huge fan of ReactiveUI"*](https://ericsink.com/entries/dont_use_rxui.html) — and the thesis still holds nine years later:

> The effort to learn Rx and ReactiveUI is worth the trouble. My claim is based on this notion that ReactiveUI shines as complexity increases, but also on my belief that most people underestimate the complexity of their app.

This article makes that thesis concrete. No marketing bullets — just side-by-side comparisons of the kind of code you'd actually write.

## When you don't need it

For a five-control form with two text boxes and a `Click` handler, ReactiveUI is overkill. Hand-rolled `INotifyPropertyChanged`, an `ICommand` field, and an event handler will ship faster.

We're not pretending otherwise. The honest question isn't "should I use ReactiveUI?" — it's "what does my app look like in twelve months?"

## Where it earns its keep

The moment you have *any* of the following, the boilerplate-and-bug-budget tilts:

- A search query that hits a remote API after the user stops typing.
- A "busy" indicator while a request is in flight, cancelled when the user types again.
- A retry / backoff policy on failure.
- A unit test that asserts something timing-sensitive without actually waiting.
- A view-model that needs to live across navigation, but stop work when the user leaves the screen.
- The same view-model running on WPF *and* Avalonia *and* MAUI.

Each one of these is two-to-five lines in ReactiveUI and a small disaster in the alternative. Five examples, in increasing weight.

### 1. Property change → property change

A common pattern: when `FirstName` or `LastName` changes, recompute `FullName`.

**Without ReactiveUI** — typical hand-rolled MVVM:

```csharp
private string _firstName = string.Empty;
public string FirstName
{
    get => _firstName;
    set
    {
        if (_firstName == value) return;
        _firstName = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(FullName)); // don't forget!
    }
}

// repeat for LastName, with the same easy-to-miss OnPropertyChanged(nameof(FullName))

public string FullName => $"{FirstName} {LastName}".Trim();
```

The `OnPropertyChanged(nameof(FullName))` line is invisible at code-review time and silent at runtime. Forget it once and the UI just stops updating until someone files a bug six weeks later.

**With ReactiveUI:**

```csharp
[Reactive] public partial string FirstName { get; set; } = string.Empty;
[Reactive] public partial string LastName  { get; set; } = string.Empty;

public string FullName => _fullName.Value;
private readonly ObservableAsPropertyHelper<string> _fullName;

public PersonViewModel()
{
    _fullName = this.WhenAnyValue(x => x.FirstName, x => x.LastName,
                                   (f, l) => $"{f} {l}".Trim())
                    .ToProperty(this, x => x.FullName);
}
```

The dependency graph is **declared**. Add a third input and the compiler walks you through every site that needs updating. Remove an input and any stale code breaks at compile time.

### 2. Commands with real CanExecute

`ICommand.CanExecute` is a polled boolean. ReactiveUI's `ReactiveCommand` takes a *stream* of booleans, so "the button enables when the form is valid AND we're not already saving AND we're online" is one expression, not three event handlers and a flag:

```csharp
var canSave = this.WhenAnyValue(
    x => x.IsValid,
    x => x.IsBusy,
    x => x.IsOnline,
    (valid, busy, online) => valid && !busy && online);

Save = ReactiveCommand.CreateFromTask(SaveAsync, canSave);
```

`Save.IsExecuting` is itself an `IObservable<bool>` you can pipe straight to the busy indicator. No `IsBusy = true; try { … } finally { IsBusy = false; }` boilerplate, and no race conditions when the user double-clicks during the await.

### 3. Search-as-you-type, the right way

The "search box that hits an API" example from the start of the article. The hand-rolled version typically grows into 60-80 lines with a `CancellationTokenSource`, a `_lastQuery` field, a `Stopwatch`, and at least one bug where a slow response from query N arrives after a fast response from query N+1 and overwrites the screen.

The ReactiveUI version:

```csharp
this.WhenAnyValue(x => x.SearchQuery)
    .Throttle(TimeSpan.FromMilliseconds(800), RxApp.TaskpoolScheduler)
    .Select(q => q?.Trim())
    .DistinctUntilChanged()
    .Where(q => !string.IsNullOrWhiteSpace(q))
    .SelectMany(q => SearchAsync(q).TakeUntil(this.WhenAnyValue(x => x.SearchQuery).Skip(1)))
    .ObserveOn(RxApp.MainThreadScheduler)
    .Subscribe(results => Results = results);
```

`Throttle` debounces. `DistinctUntilChanged` skips no-op edits. `SelectMany` + `TakeUntil` cancels the in-flight request when the input changes. `ObserveOn(MainThreadScheduler)` marshals the result back to the UI thread. **One pipeline. No race condition.**

### 4. Time you can rewind

The 800 ms throttle in the search example. How do you test it without a `Thread.Sleep(900)` in your unit test?

```csharp
new TestScheduler().With(scheduler =>
{
    var vm = new SearchViewModel(scheduler);
    vm.SearchQuery = "hello";

    scheduler.AdvanceBy(TimeSpan.FromMilliseconds(799).Ticks);
    Assert.Empty(vm.Results); // not yet

    scheduler.AdvanceBy(TimeSpan.FromMilliseconds(2).Ticks);
    Assert.NotEmpty(vm.Results); // now
});
```

Tests run in microseconds. The same `RxApp.TaskpoolScheduler` your production code uses is *the same abstraction* the test injects a fake into. There's no separate "test mode" branch in the code under test.

### 5. Activation and the leak you didn't write

A view-model subscribes to a global event bus. The user navigates away. The view-model never unsubscribes because nobody told it to. The event bus holds a reference forever; so does every captured local; so does the view it once owned.

```csharp
public MainViewModel()
{
    this.WhenActivated(disposables =>
    {
        Bus.Listen<UserChanged>()
           .Subscribe(HandleUserChanged)
           .DisposeWith(disposables);

        // ... every other subscription, .DisposeWith(disposables)
    });
}
```

`disposables` is a `CompositeDisposable` the framework owns. When the view detaches, every subscription is disposed. The pattern is uniform — `.DisposeWith(disposables)` at the end of every subscription, and your leak budget drops to zero. No `IDisposable` plumbing in the view-model itself.

## And then: the same view-model on every platform

ReactiveUI runs on .NET (WPF, WinForms, WinUI), MAUI, Avalonia, Uno, and through ReactiveUI.Maui on iOS and Android. The view-model layer above is **identical** across all of them. The view layer changes; the property pipelines, command streams, and tests don't.

That's not unique to ReactiveUI — but the cost-of-portability is. Your `WhenAnyValue` / `ToProperty` / `ReactiveCommand` code compiles unchanged. The view binding hooks plug into each platform's idioms (XAML's `{Binding}`, MAUI bindings, Avalonia bindings) without re-deriving the view-models.

## So when *should* you use it?

If your app is a wizard with five forms and a "Save" button, you're fine without it.

If your app has a search box, a notifications drawer, an offline mode, two view-models that talk to each other, or a release cycle longer than three months, the calculation changes. The lines of code don't shrink dramatically — but the *sites where bugs hide* shrink dramatically.

Sink's line about "most people underestimate the complexity of their app" is the part of his post that has aged best. The boilerplate you don't write today is the bug you don't ship next quarter.

## Where to start

- [Compelling example](../documentation/getting-started/compelling-example.md) — the search-box pattern from §3, fully implemented.
- [WhenActivated handbook](../documentation/handbook/when-activated.md) — the lifecycle pattern from §5.
- [Testing](../documentation/handbook/testing.md) — `TestScheduler` and friends.
- [Routing](../documentation/handbook/routing.md) — view-model-first navigation.
- [Installation](../documentation/getting-started/installation/index.md) — pick your platform.

ReactiveUI doesn't make trivial apps simpler. It makes complex apps *possible to keep working on*. That's the trade — and after a year of either code-base, the difference isn't subtle.
