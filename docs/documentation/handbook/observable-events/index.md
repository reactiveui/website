---
Order: 26
---
# ObservableEvents

Handling a .NET event means writing a handler, attaching it with `+=`, and remembering the matching `-=`. A handler
cannot be filtered, delayed or combined with another event, and a forgotten `-=` leaks.

`ReactiveUI.Primitives.ObservableEvents` turns events into streams instead. It is a **source generator**: a part of
the compiler that writes extra C# code into your project when it builds. For each public event, it writes a strongly
typed `IObservable<T>`. Subscribing attaches the handler, and disposing the subscription removes it.

## Your first generated event

**1. Add the package.** It generates code only, so mark it `PrivateAssets="all"`:

```xml
<PackageReference Include="ReactiveUI.Primitives.ObservableEvents" Version="*" PrivateAssets="all" />
```

**2. Call `Events()`** on the object that declares the event. The generator writes a wrapper with one property per
public event:

```csharp
using ReactiveUI.Primitives.ObservableEvents;

public sealed class ChangedEventArgs(int length) : EventArgs
{
    public int Length { get; } = length;
}

public sealed class Editor
{
    public event EventHandler<ChangedEventArgs>? Changed;

    public void Type(int length) => Changed?.Invoke(this, new ChangedEventArgs(length));
}
```

```csharp
var editor = new Editor();

using IDisposable subscription = editor.Events().Changed
    .Subscribe(args => Console.WriteLine($"changed: {args.Length}"));

editor.Type(42);
```

Output:

```text
changed: 42
```

**3. Dispose the subscription** when you no longer need it. In a view, subscribe inside `WhenActivated` and add each
subscription to its disposables with `DisposeWith`. See [WhenActivated](../when-activated.md).

The generator works out which stream library your project uses, and writes code for the first it finds:

| Your project references | Streams are built with | A delegate with no parameters sends |
|---|---|---|
| `ReactiveUI.Primitives` | `ReactiveUI.Primitives.Signals.Signal` | `RxVoid` |
| `ReactiveUI.Primitives.Reactive` | `ReactiveUI.Primitives.Reactive.Signals.Signal` | `System.Reactive.Unit` |
| System.Reactive only | `System.Reactive.Linq.Observable` | `System.Reactive.Unit` |

## What each stream sends

The value a stream sends depends on the event's delegate:

| Delegate | The stream sends |
|---|---|
| No parameters, such as `Action` | `RxVoid`, a value that carries no data |
| One parameter, such as `Action<string>` | That parameter |
| `(object sender, TEventArgs args)`, such as `EventHandler<T>` | The `args` |
| Any other set of parameters | A named tuple of the parameters |

Delegates that return `void`, `Task` or `ValueTask` all work.

```csharp
public delegate void MovedHandler(int line, int column);

public sealed class Document
{
    public event Action? Saved;
    public event Action<string>? Renamed;
    public event MovedHandler? Moved;

    public void Raise()
    {
        Saved?.Invoke();
        Renamed?.Invoke("notes.txt");
        Moved?.Invoke(3, 7);
    }
}
```

```csharp
var document = new Document();

document.Events().Saved.Subscribe(_ => Console.WriteLine("saved"));
document.Events().Renamed.Subscribe(name => Console.WriteLine($"renamed to {name}"));
document.Events().Moved.Subscribe(position => Console.WriteLine($"moved to {position.line}:{position.column}"));

document.Raise();
```

Output:

```text
saved
renamed to notes.txt
moved to 3:7
```

## Static events

A static event has no object to call `Events()` on. Name the type in an assembly attribute instead:

```csharp
[assembly: GenerateStaticEventObservables(typeof(AppEvents))]

public static class AppEvents
{
    public static event Action<string>? Message;

    public static void Send(string text) => Message?.Invoke(text);
}
```

The generator adds static properties to a class named `RxEvents`, in the namespace of the type that declares the
events. Each property name puts the length of each name in front of it: `T9AppEvents` for the type `AppEvents`, then
`7Message` for the event `Message`. That way two different types and events can never produce the same name.

```csharp
RxEvents.T9AppEvents7Message.Subscribe(message => Console.WriteLine($"static: {message}"));
AppEvents.Send("hello");
```

Output:

```text
static: hello
```

## Composing events

Each event is a stream, so any operator can shape it. Wait for typing to stop, then search:

```csharp
this.WhenActivated(disposables =>
{
    SearchBox.Events().TextChanged
        .Calm(TimeSpan.FromMilliseconds(300))
        .Select(_ => SearchBox.Text)
        .Unique()
        .InvokeCommand(this, x => x.ViewModel.Search)
        .DisposeWith(disposables);
});
```

Track a drag, from the pointer going down until it comes up:

```csharp
IObservable<Point> drag =
    from down in canvas.Events().MouseDown
    from move in canvas.Events().MouseMove.TakeUntil(canvas.Events().MouseUp)
    select move.GetPosition(canvas);
```

See the [operator pages](../../primitives/index.md#the-operator-pages) and [events](../events.md) for more.

## When the generator reports a problem

| Diagnostic | Cause |
|---|---|
| `RXOE001` | The project references none of the three stream libraries. |
| `RXOE002` | `Events()` or `GenerateStaticEventObservables` names a type with no public events the generator supports. |
| `RXOE003` | An event cannot be a stream: its delegate takes a `ref` or `out` parameter, a pointer or a `ref struct`, returns something other than `void`, `Task` or `ValueTask`, or belongs to a generic type with static events. |

If `Events()` does not show up, build the project once: the generator runs as part of the build. Only public events
are generated. To see the generated code in Visual Studio, expand **Dependencies**, **Analyzers**,
**ReactiveUI.Primitives.ObservableEvents** in Solution Explorer.

## Related topics

- [Events](../events.md)
- [WhenActivated](../when-activated.md)
- [Commands](../commands/index.md)
- [Streams in ReactiveUI](../../reactive-programming/observables.md)
