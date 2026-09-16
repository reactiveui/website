---
Order: 10
---
# Events

A view raises events: a button is clicked, a key is released, a page loads. To use them in a view model pipeline, you
turn each event into a stream. Then you can filter it, combine it with other streams, and feed it to a command.

The `ReactiveUI.Primitives.ObservableEvents` package does that for you. It is a **source generator**: a part of the
compiler that writes extra C# code into your project when it builds. For every public event on a type, it writes a
strongly typed stream, so your IDE suggests the events and the compiler checks their names.

## Your first event stream

**1. Add the package** to the project that holds your views:

```xml
<PackageReference Include="ReactiveUI.Primitives.ObservableEvents" Version="*" PrivateAssets="all" />
```

**2. Call `Events()`** on the object that raises the event, and pick the event:

```csharp
using ReactiveUI.Primitives.ObservableEvents;

IObservable<RoutedEventArgs> clicks = RefreshButton.Events().Click;
```

**3. Shape the stream and use it.** This view runs the view model's `Refresh` command on every click:

```csharp
this.WhenActivated(disposables =>
{
    RefreshButton
        .Events().Click
        .Select(args => RxVoid.Default)
        .InvokeCommand(this, x => x.ViewModel.Refresh)
        .DisposeWith(disposables);
});
```

`RxVoid` is a value that carries no data: the command only needs to know that a click happened. Dispose the
subscription when the view deactivates. A view model that outlives its view, or the other way round, otherwise keeps
the other alive. See [WhenActivated](when-activated.md).

The generator works with ReactiveUI.Primitives, ReactiveUI.Primitives.Reactive, or System.Reactive. It uses whichever
your project references. For how it maps each event's delegate to a value type, and for static events, see
[observable events](observable-events/index.md).

## Composing events

Because each event is a stream, operators can do what would take fields and flags with a plain event handler. This
view watches for the Konami code: ten keys in a row.

```csharp
var codes = new[] { Key.Up, Key.Up, Key.Down, Key.Down, Key.Left, Key.Right, Key.Left, Key.Right, Key.A, Key.B };

this.Events().KeyUp
    .Select(args => args.Key)
    .Buffer(10, 1)
    .Where(keys => keys.SequenceEqual(codes))
    .Subscribe(_ => Debug.WriteLine("Konami sequence"));
```

- `Select` takes the key from each event.
- `Buffer(10, 1)` sends the last ten keys each time a key is released.
- `Where` keeps the lists that match the code.

See [time](../primitives/time.md) for `Buffer`, and the other [operator pages](../primitives/index.md#the-operator-pages).

## Events instead of XAML behaviors

XAML behaviors can bind any event to a command, but the markup is long, the IDE cannot check the event name, and
changing how the view model reacts means writing a new behavior:

```xml
<interactivity:Interaction.Behaviors>
    <core:EventTriggerBehavior EventName="Tapped">
        <core:InvokeCommandAction Command="{x:Bind ViewModel.Refresh}" />
    </core:EventTriggerBehavior>
</interactivity:Interaction.Behaviors>
```

The same thing as a stream is checked by the compiler, and any operator can sit in the middle:

```csharp
this.Events().Tapped
    .Select(args => RxVoid.Default)
    .InvokeCommand(this, x => x.ViewModel.Refresh);
```

## Turning an event into a stream yourself

Without the generator, `Signal` has two methods that do the same job. Each takes a lambda that attaches the handler
and one that removes it.

`Signal.FromEventPattern` works with events whose delegate takes a sender and an `EventArgs`. It sends an
`EventPattern<TEventArgs>`, with `Sender` and `EventArgs` properties:

```csharp
using ReactiveUI.Primitives.Core;
using ReactiveUI.Primitives.Signals;

IObservable<EventPattern<RoutedEventArgs>> passwordChanged =
    Signal.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
        handler => PasswordBox.PasswordChanged += handler,
        handler => PasswordBox.PasswordChanged -= handler);
```

`Signal.FromEvent` works with any delegate type. The first lambda builds a handler of the event's delegate type that
passes the value on:

```csharp
IObservable<KeyPressEventArgs> keyPresses =
    Signal.FromEvent<KeyPressEventHandler, KeyPressEventArgs>(
        handler => (sender, e) => handler(e),
        handler => KeyPress += handler,
        handler => KeyPress -= handler);
```

Neither method looks events up by name, so both work with trimming and Native AOT. See
[from an event](../primitives/creation-factories.md#from-an-event).
