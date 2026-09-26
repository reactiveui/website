# Put this on the left of WhenAny

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/guidelines/guidelines.csproj).

`WhenAny` is an extension method, so you can call it on `this` or on any object it points to through a
property. Call it on `this`, reading through to the dependency's property in the selector, rather than calling
it on the dependency directly.

## Read through this, not through the dependency

`PreferLessonReminderViewModel` takes a `Timetable` dependency and reads its `NextClass` property through
`this.WhenAny`:

```csharp
public PreferLessonReminderViewModel(Timetable timetable)
{
    Timetable = timetable;
    _nextClass = this.WhenAny(x => x.Timetable.NextClass, static change => change.Value)
        .ToProperty(this, nameof(NextClass));
}
```

`AvoidTheDependencyOnTheLeft` calls `WhenAny` on `timetable` itself instead:

```csharp
public AvoidLessonReminderViewModel(Timetable timetable) =>
    _nextClass = timetable.WhenAny(x => x.NextClass, static change => change.Value)
        .ToProperty(this, nameof(NextClass));
```

Both view models report the same value here, since the example's `Timetable` is short-lived:

```csharp
Timetable timetable = new() { NextClass = "Robotics, 9am" };
using PreferLessonReminderViewModel viewModel = new(timetable);

Console.WriteLine(viewModel.NextClass);

timetable.NextClass = "Chess, 11am";
Console.WriteLine(viewModel.NextClass);
```

```text
Robotics, 9am
Chess, 11am
```

## Why the left side matters

`WhenAny` subscribes to change notifications on whatever sits on its left. Put `this` there, and the
subscription belongs to the view model, so it goes away with the view model. Put the dependency there instead,
and the subscription belongs to the dependency, which now holds a reference to the view model's selector and
everything that selector closes over.

That difference has no visible effect on a dependency as short-lived as the example's `Timetable`. It matters
once a dependency is a singleton or another long-lived service. Reading through it on the left keeps every view
model that ever called `WhenAny` on it reachable for as long as the singleton lives, even after its screen has
gone.

## Still dispose your subscriptions

Reading through `this` avoids that leak, but it does not remove the need to manage a subscription's lifetime.
Subscribe, bind, or invoke a command against a stream rooted in a longer-lived dependency, and the dependency
still holds a reference to that subscription until you dispose it.

`PreferDisposingEvenWithThisOnTheLeft` adds the subscription to a `MultipleDisposable`, and disposes it once the
screen is done with it:

```csharp
MultipleDisposable disposables = [];
timetable.WhenAnyValue(x => x.NextClass)
    .Subscribe(announcements.Add)
    .DisposeWith(disposables);

timetable.NextClass = "Chess, 11am";
disposables.Dispose();
timetable.NextClass = "Art, 1pm";
```

The announcement made after `Dispose` never arrives:

```text
Robotics, 9am, Chess, 11am
```

Tie a subscription like this to `WhenActivated` and `DisposeWith`, so it disposes together with the rest of the
view model's subscriptions. See [dispose your subscriptions](dispose-your-subscriptions.md).

`ToProperty(this, ...)` is the one common exception: the `ObservableAsPropertyHelper<T>` it returns has its own
lifetime, tied to the field you store it in, and dies when you dispose that field along with the view model.

## At a glance

| Do | Instead of | Why |
|---|---|---|
| `this.WhenAny(x => x.Dependency.Property, ...)` | `dependency.WhenAny(x => x.Property, ...)` | The subscription belongs to the view model, not to a dependency that may outlive it. |
