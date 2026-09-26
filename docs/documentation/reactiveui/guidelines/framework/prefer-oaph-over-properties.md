# Prefer ObservableAsPropertyHelper over setting properties explicitly

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/guidelines/guidelines.csproj).

When a property's value only ever comes from another property, or from a stream, give it an
`ObservableAsPropertyHelper<T>` instead of a setter. Read [turn a stream back into a property with
ToProperty](../../handbook/view-models/index.md#turn-a-stream-back-into-a-property-with-toproperty) for how
`ToProperty` builds one.

## The problem with a settable property

A plain property with a public setter can be written to from anywhere: the constructor's subscription, another
method on the same class, or code in a completely different file. Nothing stops a second writer from
overwriting the value the first one computed.

```csharp
public bool RosterFetched
{
    get;
    set => this.RaiseAndSetIfChanged(ref field, value);
}

public bool RegistrarFree
{
    get;
    set => this.RaiseAndSetIfChanged(ref field, value);
}

public bool CanEnroll
{
    get;
    set => this.RaiseAndSetIfChanged(ref field, value);
}
```

`AvoidASettablePropertyAnyoneCanOverwrite` sets `CanEnroll` correctly at first, from a subscription that watches
`RosterFetched` and `RegistrarFree`. Then a second piece of code, standing in for anything else in the class or
codebase, writes to `CanEnroll` directly:

```csharp
using AvoidEnrollmentViewModel viewModel = new();
viewModel.RosterFetched = true;
viewModel.RegistrarFree = true;

Console.WriteLine(viewModel.CanEnroll);

// Some other code, far away in the codebase, writes to the same property directly.
viewModel.CanEnroll = false;

Console.WriteLine(viewModel.CanEnroll);
```

```text
True
False
```

`CanEnroll` is `false` even though both `RosterFetched` and `RegistrarFree` are still `true`. The property no
longer says what it claims to say.

## Compute the property instead of setting it

`PreferAnObservableAsPropertyHelper` keeps the same two source properties, but backs `CanEnroll` with an
`ObservableAsPropertyHelper<bool>` built from `WhenAny`:

```csharp
_canEnroll = this.WhenAny(x => x.RosterFetched, x => x.RegistrarFree, static (fetched, free) => fetched.Value && free.Value)
    .ToProperty(this, nameof(CanEnroll));
```

`CanEnroll` has no setter. It can only change when the pipeline above produces a new value:

```csharp
public bool CanEnroll => _canEnroll.Value;
```

Running the same two writes, followed by a change to one of the source properties instead of a direct write,
shows `CanEnroll` tracking its sources correctly:

```csharp
using PreferEnrollmentViewModel viewModel = new();
viewModel.RosterFetched = true;
viewModel.RegistrarFree = true;

Console.WriteLine(viewModel.CanEnroll);

// CanEnroll only changes when RosterFetched or RegistrarFree does; there is no other way to change it.
viewModel.RegistrarFree = false;

Console.WriteLine(viewModel.CanEnroll);
```

```text
True
False
```

The output looks the same as the settable version, but for a different reason: this time `CanEnroll` changed
because `RegistrarFree` did, and there is no other path that could have changed it.

## Why this matters

- **One source of change.** An `ObservableAsPropertyHelper<T>` is proof that a property has exactly one place
  that can change it: the stream passed to `ToProperty`. A settable property can be written to from anywhere,
  which invites bugs like the one above.
- **Change notification for free.** `ToProperty` raises the property's change notification for you. A
  hand-written settable property needs `RaiseAndSetIfChanged` at every call site that sets it.
- **WhenAny combines several properties.** `WhenAny` turns changes to several properties into one stream, so you
  can shape a single output from them, as `CanEnroll` does here. Name each parameter after what it means, the
  way `fetched` and `free` do above; see [use descriptive variables in
  WhenAny](use-descriptive-variables-with-whenany.md).
- **Scheduling and laziness come from the same call.** A `scheduler` parameter on `ToProperty` replaces a
  separate `WitnessOn` step. A `deferSubscription` parameter delays the subscription until the property's first
  read.

## At a glance

| Do | Instead of | Why |
|---|---|---|
| `WhenAny(...).ToProperty(this, nameof(Property))` | A settable property written to from a subscription | Nothing outside the pipeline can change the value. |
