---
Order: 3
---
# Properties backed by observables

[Run the complete page example](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/properties/properties.csproj).

A view model often needs a read-only property whose value comes from a stream: a full name built from two other
properties, a count of unfinished items, a label that depends on several fields. `ToProperty` turns a stream into
that kind of property. It returns an `ObservableAsPropertyHelper<T>`, a helper that keeps the latest value and
raises your type's own change notification each time a new one arrives.

## Back a property with a stream

**1. Get a stream.** [Observing](observing.md) covers `WhenChanged` and the other ways to get one.

**2. Call `ToProperty`.** Pass the object that declares the property and a lambda that names it, of the form
`x => x.Property`. Keep the returned helper in a field.

**3. Read the helper's `Value`.** The property getter returns it.

```csharp
_titleHelper = item.WhenChanged(static x => x.Title).ToProperty(this, static x => x.Title);
```

```csharp
public string Title => _titleHelper.Value;
```

## When to use a backed property

Use a backed property for a read-only value that a stream computes: a value derived from other properties, a
running count, or a status label. Give the object a settable property instead when a view or a command writes
to it directly; a backed property only ever changes because its source produced a new value.

A backed property composes with the rest of the library the same way any other stream does. Chain `WhenChanged`
with `Where` and `Select` before `ToProperty` to filter or transform the values, and combine two properties with
`WhenChanged(x => x.A, x => x.B, (a, b) => ...)` before backing a third property with the result.

```csharp
_fullNameHelper = this.WhenChanged(static x => x.FirstName, static x => x.LastName, static (first, last) => $"{first} {last}")
    .ToProperty(this, static x => x.FullName);
```

A source that only produces a value when something asks for it, such as a hot connection that starts on first
subscribe, pairs well with `deferSubscription: true`: nothing runs until a view first reads the property.

The helper delivers the source's current value as soon as it subscribes, then a new value each time the source
produces one. A value equal to the one already stored is skipped.

## How the generator raises your notification

A type can only raise its own change notifications, so `ToProperty` needs a way in. The generator uses the first
of these your type offers.

| Your type | How the generator raises the notification |
| --- | --- |
| A public or internal `RaisePropertyChanged`, `OnPropertyChanged`, `NotifyPropertyChanged` or `NotifyOfPropertyChange` method | Calls that method directly. The type does not have to be `partial`. |
| A `partial` type | Adds one small member to your type. The member invokes your own `PropertyChanged` event, or calls a `protected` raise method your base class declares. |

Both routes prefer an overload that takes `PropertyChangedEventArgs` over one that takes a name, because the
generator can then pass one cached instance per property: raising a notification allocates nothing.

The example project shows all three shapes side by side.

### A type with its own event

`PartialOwnEventViewModel` declares `PropertyChanged` itself. Only code inside the type can invoke a field-like
event, so the type has to stay `partial`: the generator adds the member that invokes it on the type's behalf.

```csharp
public sealed partial class PartialOwnEventViewModel : INotifyPropertyChanged
{
    private readonly ObservableAsPropertyHelper<string> _notesHelper;
    private readonly ObservableAsPropertyHelper<string> _titleHelper;

    public PartialOwnEventViewModel(TodoItem item)
    {
        _titleHelper = item.WhenChanged(static x => x.Title).ToProperty(this, static x => x.Title);
        _ = item.WhenChanged(static x => x.Notes).ToProperty(this, static x => x.Notes, out _notesHelper);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Title => _titleHelper.Value;

    public string Notes => _notesHelper.Value;
}
```

`Notes` uses the overload that returns the helper through an `out` parameter instead of the method's own return
value, so you can assign a `readonly` field from an expression body or a field initializer that never runs the
constructor body.

```csharp
TodoItem item = new TodoItem { Title = "Renew car registration", Notes = "Bring the insurance certificate" };
PartialOwnEventViewModel viewModel = new PartialOwnEventViewModel(item);

Console.WriteLine(viewModel.Title);
Console.WriteLine(viewModel.Notes);

item.Title = "Renew car registration online";

Console.WriteLine(viewModel.Title);
```

```text
Renew car registration
Bring the insurance certificate
Renew car registration online
```

### A type with a public raise method

`RaiseMethodViewModel` inherits `RaisePropertyChanged` from a base class, of the kind CommunityToolkit.Mvvm and
Prism declare. The generator calls it directly, so the type itself does not need to be `partial`.

```csharp
public sealed class RaiseMethodViewModel : RaiseMethodBase
{
    public static readonly string LoadingLabel = "Loading…";

    private readonly ObservableAsPropertyHelper<string> _remainingLabelHelper;
    private readonly ObservableAsPropertyHelper<TodoPriority> _priorityHelper;

    public RaiseMethodViewModel(TodoItem item)
    {
        _remainingLabelHelper = item.WhenChanged(static x => x.IsDone)
            .Select(static done => done ? "Done" : "Open")
            .ToProperty(this, static x => x.RemainingLabel, static () => LoadingLabel);
        _priorityHelper = item.WhenChanged(static x => x.Priority).ToProperty(this, nameof(Priority), TodoPriority.Normal);
    }

    public string RemainingLabel => _remainingLabelHelper.Value;

    public TodoPriority Priority => _priorityHelper.Value;
}
```

`RemainingLabel` names its property with a selector and an initial-value factory: the factory runs only if the
helper needs a value before the stream produces one. `Priority` instead names its property with a constant,
`nameof(Priority)`, and a plain initial value. Name a property this way when you build the property name from a
setting, or simply prefer not to repeat the selector.

```csharp
TodoItem item = new TodoItem { IsDone = false, Priority = TodoPriority.High };
RaiseMethodViewModel viewModel = new RaiseMethodViewModel(item);

Console.WriteLine(viewModel.RemainingLabel);
Console.WriteLine(viewModel.Priority);

item.IsDone = true;

Console.WriteLine(viewModel.RemainingLabel);
```

```text
Open
High
Done
```

### A type with a protected base raise method

`PartialProtectedBaseViewModel` inherits `ObservableObject`, an example base class whose `RaisePropertyChanged`
is `protected`. Code outside the type cannot call a protected member, so the generator falls back to the partial
route: it adds a member to the type that calls the base method on the type's own behalf.

```csharp
public sealed partial class PartialProtectedBaseViewModel : ObservableObject
{
    public static readonly string NoDueDateLabel = "No due date";

    private readonly ObservableAsPropertyHelper<bool> _isDoneHelper;
    private readonly ObservableAsPropertyHelper<string> _dueDateLabelHelper;

    public PartialProtectedBaseViewModel(TodoItem item, ISequencer scheduler)
    {
        _isDoneHelper = item.WhenChanged(static x => x.IsDone).ToProperty(this, static x => x.IsDone, deferSubscription: true);
        _dueDateLabelHelper = item.WhenChanged(static x => x.DueDate)
            .Select(static due => due is { } d ? d.ToString("yyyy-MM-dd") : NoDueDateLabel)
            .ToProperty(this, static x => x.DueDateLabel, scheduler);
    }

    public bool IsDone => _isDoneHelper.Value;

    public string DueDateLabel => _dueDateLabelHelper.Value;
}
```

`IsDone` passes `deferSubscription: true`, so the helper does not follow `item` until something first reads
`IsDone`. `DueDateLabel` passes a scheduler, so its notifications wait for that scheduler to run; here the
scheduler is a `VirtualClock`, a sequencer you step by hand, which the [Threading](threading.md) page covers.

```csharp
TodoItem item = new TodoItem { IsDone = false, DueDate = RegistrationDue };
VirtualClock scheduler = new();
PartialProtectedBaseViewModel viewModel = new PartialProtectedBaseViewModel(item, scheduler);

item.IsDone = true;

// The first read subscribes. WhenChanged delivers the item's current value on subscribe, so it is not missed.
Console.WriteLine(viewModel.IsDone);

item.DueDate = null;

// The scheduler has not run yet, so DueDateLabel still holds its default value.
Console.WriteLine(viewModel.DueDateLabel);

scheduler.Start();

Console.WriteLine(viewModel.DueDateLabel);
```

```text
True

No due date
```

Deferred subscription only delays *when* the helper starts following the source. Reading `IsDone` for the first
time subscribes it, and `WhenChanged` delivers the item's current value as soon as anything subscribes, so the
read still sees the up-to-date `True`. `DueDateLabel`, on the other hand, subscribed in the constructor but
raises its notifications through the scheduler, so its value stays at its default (an empty line, since no
initial value was given) until `scheduler.Start()` runs the queued work.

## Name the property directly

Name the property with a lambda of the form `x => x.Property`, or with a constant such as `nameof(Property)`.
The overloads that take an initial value, an initial-value factory, `deferSubscription`, a scheduler, or the
`out` parameter for the helper all work the same way; combine as many of them as you need. RXUIBIND012 warns you
when the generator has no way to raise your type's notifications. RXUIBIND013 warns you when it cannot read the
property's name. Either way the call generates nothing and throws when it runs. For those properties, call
[`ToPropertyUnsafe`](unsafe.md#back-a-property-with-topropertyunsafe), which finds the name and the raise member by
reflection.

Below C# 13, name the initial value of a `string` property when you name the property with a lambda:
`ToProperty(this, x => x.Title, initialValue: "(untitled)")`. Without the name the compiler cannot choose an overload,
and RXUIBIND014 reports the argument to name. From C# 13, and with a `nameof` name at any version, the plain argument
works.

## Declare the property with an attribute

`[ObservableAsProperty]` writes the property's body and its backing field for you. Declare the property as
`partial` and get-only, so every generator in the build can see it. The generator writes a field named
`_{name}Helper`, which you assign in the constructor with `ToProperty`.

```csharp
public sealed partial class SummaryViewModel : INotifyPropertyChanged
{
    public SummaryViewModel(TodoItem item)
    {
        _titleHelper = item.WhenChanged(static x => x.Title).ToProperty(this, static x => x.Title);
        _isDoneHelper = item.WhenChanged(static x => x.IsDone).ToProperty(this, static x => x.IsDone);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [ObservableAsProperty]
    public partial string Title { get; }

    [ObservableAsProperty]
    public partial bool IsDone { get; }
}
```

```csharp
TodoItem item = new TodoItem { Title = "Renew car registration" };
SummaryViewModel summary = new SummaryViewModel(item);

Console.WriteLine(summary.Title);
Console.WriteLine(summary.IsDone);

item.IsDone = true;

Console.WriteLine(summary.IsDone);
```

```text
Renew car registration
False
True
```

`[ObservableAsProperty]` needs C# 13 or newer, because partial properties are a C# 13 feature.

## Construct the helper directly

Most of the time `ToProperty` is all you need. Construct `ObservableAsPropertyHelper<T>` yourself when you
already have callbacks that raise a change notification, or when a property has no source at all.

```csharp
TodoItem item = new TodoItem { Title = "Renew car registration" };
IObservable<string> titles = item.WhenChanged(x => x.Title);

using ObservableAsPropertyHelper<string> helper = new ObservableAsPropertyHelper<string>(
    titles,
    onChanged: static value => Console.WriteLine($"Now: {value}"),
    onChanging: static value => Console.WriteLine($"Was: {value}"));

item.Title = "Renew car registration online";
```

```text
Was: 
Now: 
Was: Renew car registration
Now: Renew car registration
Was: Renew car registration online
Now: Renew car registration online
```

Starting the helper delivers its default value as a first notification (the blank `Was:`/`Now:` pair), then
follows the source, whose own current value arrives as a second notification. `onChanging` runs just before the
new value is stored, and `onChanged` runs just after; pass `null` for `onChanging` when you only need the value
after it changes.

With no scheduler, or with the immediate one, a helper delivers a value on the thread that produced it. A value
produced while an earlier one is still being delivered waits its turn, so notifications never overlap and always
arrive in order.

### Read errors instead of losing them

A source that fails sends its error to `ThrownExceptions` instead of ending the helper. Nothing observes that
stream by default, so the error is rethrown on the thread that produced it. Subscribe to see it instead.

```csharp
TodoItem item = new TodoItem { Title = "Renew car registration" };
IObservable<string> titles = item.WhenChanged(x => x.Title);
InvalidOperationException failure = new InvalidOperationException("The title source failed.");
IObservable<string> withFailure = Signal.Concat(titles, Signal.Fail<string>(failure));

ObservableAsPropertyHelper<string> helper = new ObservableAsPropertyHelper<string>(withFailure, Console.WriteLine);

using IDisposable subscription = helper.ThrownExceptions.Subscribe(static ex => Console.WriteLine(ex.Message));

item.Title = "Renew car registration online";

helper.Dispose();
```

```text
Renew car registration
Renew car registration online
Not connected
```

### A helper that never changes

`ObservableAsPropertyHelper<T>.Default()` returns a helper that holds a fixed value and never subscribes to
anything. Use it for a property that has no source yet, such as a design-time or disconnected view model.

```csharp
using ObservableAsPropertyHelper<string> helper = ObservableAsPropertyHelper<string>.Default("Not connected");

Console.WriteLine(helper.Value);
Console.WriteLine(helper.IsSubscribed);
```

```text
Not connected
False
```

## Where this differs from ReactiveUI

ReactiveUI's `ToProperty` only accepts an `IReactiveObject`. Here it accepts any class the generator can raise
notifications for, including a plain `INotifyPropertyChanged` type. A ReactiveUI `ReactiveObject` still raises
through ReactiveUI, so its `Changed` stream and suppressed notifications behave as before.

ReactiveUI reads the property name from an expression tree while your code runs, and builds two delegates and a
closure for every helper. Here the selector is a plain lambda of the form `x => x.Property`: the generator reads
it when you build, so creating the property builds no expression tree, uses no reflection, and the helper takes
one cached delegate per notification.

ReactiveUI's helper raises its notifications through the current-thread scheduler. Here a helper with no
scheduler raises them on the thread that produced the value, in order, one at a time. ReactiveUI sends a
helper's error to its default exception handler, which throws on the main thread; here the error goes to
`ThrownExceptions`, and is rethrown where it happened only when nothing observes that stream.

The helper type has ReactiveUI's name, `ObservableAsPropertyHelper<T>`, in the `ReactiveUI.Binding` namespace. A
file that imports both `ReactiveUI` and `ReactiveUI.Binding` has to name one of them in full, or give it an
alias.

## API reference

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`ToProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.ToProperty.cs) | Backs a read-only property with an observable's latest value. | Extension method on `IObservable<TRet>`. Takes the declaring object and a selector `Func<TObj, TRet>` of the form `x => x.Property`, or the property's name as a constant string. Optional: an initial value or a `Func<TRet>` factory for one, `deferSubscription`, an `ISequencer?` scheduler, and an `out ObservableAsPropertyHelper<TRet>` that also returns the helper. Returns [`ObservableAsPropertyHelper<TRet>`](#observableaspropertyhelpert). | The selector has to be written inline at the call. A call the generator cannot read throws `InvalidOperationException` when it runs. RXUIBIND012 and RXUIBIND013 report why. |
| [`ObservableAsPropertyAttribute`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/ObservableAsPropertyAttribute.cs) | Marks a partial, get-only property as backed by an `ObservableAsPropertyHelper<T>`. | Attribute, usable once per property. | Needs C# 13 or newer. The generator writes the property body and a field named `_{name}Helper`, which you assign with `ToProperty`. |

### `ObservableAsPropertyHelper<T>`

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `sealed class ObservableAsPropertyHelper<T> : IDisposable` | [Backs a read-only "output property" with an observable.](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/ObservableAsPropertyHelper.cs) | `T` is the type of the property value. | — |

**Constructors**

| Declaration | Description | Parameters | Notes |
| --- | --- | --- | --- |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged)` | Follows an observable and calls `onChanged` after each new value is stored. | `observable`, `onChanged`. | The property holds `default(T)` before the first value arrives. |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, T? initialValue)` | As above, with an initial value. | Adds `initialValue`. | — |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, T? initialValue, ISequencer? scheduler)` | As above, with a scheduler for the callback. | Adds `scheduler`. | `null` raises the callback on the producing thread. |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, T? initialValue, bool deferSubscription)` | As above, with deferred subscription. | Adds `deferSubscription`. | Subscribes the first time `Value` is read. |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, T? initialValue, bool deferSubscription, ISequencer? scheduler)` | Combines an initial value, deferred subscription and a scheduler. | All four. | — |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, Action<T?>? onChanging)` | Adds a callback run just before each new value is stored. | `onChanging` may be `null`. | — |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, Action<T?>? onChanging, T? initialValue)` | As above, with an initial value. | — | — |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, Action<T?>? onChanging, T? initialValue, bool deferSubscription)` | As above, with deferred subscription. | — | — |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, Action<T?>? onChanging, T? initialValue, bool deferSubscription, ISequencer? scheduler)` | As above, with a scheduler. | — | — |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, Action<T?>? onChanging, Func<T?>? getInitialValue)` | Reads the initial value from a factory instead of a constant. | `getInitialValue` may be `null`, which reads `default(T)`. | — |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, Action<T?>? onChanging, Func<T?>? getInitialValue, bool deferSubscription)` | As above, with deferred subscription. | — | — |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, Func<T?> getInitialValue, bool deferSubscription)` | As above, with no `onChanging` callback. | — | — |
| `ObservableAsPropertyHelper(IObservable<T?> observable, Action<T?> onChanged, Action<T?>? onChanging, Func<T?>? getInitialValue, bool deferSubscription, ISequencer? scheduler)` | Combines a factory, deferred subscription and a scheduler. | — | — |

**Properties**

| Declaration | Description | Notes |
| --- | --- | --- |
| `T Value { get; }` | The current value of the property. | Subscribes first when subscription was deferred. |
| `bool IsSubscribed { get; }` | Whether the helper has subscribed to its observable. | Stays `false` with deferred subscription until `Value` is first read. |
| `IObservable<Exception> ThrownExceptions { get; }` | An observable that reports each error the source produces. | An error that arrives while nothing observes this stream is rethrown on the thread that produced it. |

**Methods**

| Declaration | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `static ObservableAsPropertyHelper<T> Default()` | Creates a helper that holds the default value and never changes. | None. | The helper. |
| `static ObservableAsPropertyHelper<T> Default(T? initialValue)` | Creates a helper that holds one value and never changes. | `initialValue`. | The helper. |
| `static ObservableAsPropertyHelper<T> Default(T? initialValue, ISequencer? scheduler)` | As above, with a scheduler. | `initialValue`, `scheduler`. | The helper. |
| `void Dispose()` | Stops following the observable. | None. | The property keeps its last value. |

`Create` is `[EditorBrowsable(EditorBrowsableState.Never)]`: generated `ToProperty` code calls it, but it is not
meant to be called by hand.
