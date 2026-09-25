---
Order: 0
---
# Why Primitives

C# already gives you events, `Task`, threads and locks. This page shows what streams add on top of them, when
they are the better tool, and why every ReactiveUI library is built on ReactiveUI.Primitives.

The examples on this page use these namespaces:

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
```

## LINQ for events

You already know LINQ. It works on an `IEnumerable<T>`: a collection you **pull** values out of, one at a time,
with `foreach`.

Reactive programming turns that around. An `IObservable<T>` **pushes** each value to you when it happens. That
is why libraries like this one are often called "LINQ for events". Operators that LINQ also has keep their LINQ
names and shapes, and query syntax works too:

```csharp
var clicks = new Signal<int>();

IObservable<string> doubleClicks =
    from x in clicks
    where x >= 2
    select $"{x} clicks";

doubleClicks.Subscribe(Console.WriteLine);

clicks.OnNext(1);   // prints nothing
clicks.OnNext(2);   // prints 2 clicks
```

| | You hold | Values come | You get them with |
|---|---|---|---|
| LINQ | `IEnumerable<T>` | All at once, when you ask | `foreach` |
| Events | An `event` field | Over time, when they happen | `+=` a handler |
| Streams | `IObservable<T>` | Over time, when they happen | `Subscribe`, then operators |

## How streams relate to what you know

### An event you can hold

An `event` is a way to get values over time, but you cannot do much with the event itself. You cannot store it
in a variable, pass it to a method, filter it, or combine it with another event. Each handler you attach with
`+=` needs a matching `-=`, or it leaks.

A stream is an event turned into an object:

- You can pass an `IObservable<T>` to a method, return it, and keep it in a field.
- Operators such as `Where`, `Select` and `Calm` make a new stream from it, the way LINQ makes a new query.
- `Subscribe` hands back an `IDisposable`. Disposing it removes the handler, so a `using` or a
  [group of disposables](disposables.md) takes the place of every `-=`.

`Signal.FromEvent` turns any existing event into a stream. See [from an event](creation-factories.md#from-an-event).

### A signal is an event you raise yourself

`Signal<T>` is the stream version of a class that declares an `event` and raises it. Call `OnNext` to raise it.
Unlike an event, a signal can also **complete**, to say no more values will come, or **fail**, with an
exception.

Signals do things events cannot. A `BehaviorSignal<T>` remembers its latest value and hands it to anyone who
subscribes later:

```csharp
var temperature = new BehaviorSignal<double>(20.5);
temperature.OnNext(21.0);

temperature.Subscribe(t => Console.WriteLine($"late subscriber sees {t}"));   // late subscriber sees 21
```

With an event, a late handler misses everything that happened before it attached. Other signals remember every
value, only the last few, or only the final result, or order values sent from many threads. See
[signals you push values into](signals.md).

### A task with more than one value

A `Task<T>` gives you **one** result, later. A stream gives you **any number** of results, later: progress
updates, each search result, every price change.

```csharp
var progress = new Signal<int>();
progress.Subscribe(p => Console.Write($"{p}% "), () => Console.WriteLine("finished"));

progress.OnNext(0);
progress.OnNext(50);
progress.OnNext(100);
progress.OnCompleted();   // 0% 50% 100% finished
```

When you do want one result, `await` works on a stream too. See [getting a result as a task](aggregation.md#getting-a-result-as-a-task).

## A search box, two ways

A search box has three rules:

- Wait until the user stops typing for 300 ms.
- Skip a search for the text it just searched for.
- Cancel a search that is still running when a newer one starts.

### With an event, a task and a field

```csharp
public sealed class SearchBox
{
    private CancellationTokenSource? _pending;
    private string? _lastSearched;

    public SearchBox(TextInput input) => input.TextChanged += OnTextChanged;

    private async void OnTextChanged(string text)
    {
        _pending?.Cancel();
        var cancel = _pending = new CancellationTokenSource();

        try
        {
            await Task.Delay(300, cancel.Token);

            if (text == _lastSearched)
            {
                return;
            }

            _lastSearched = text;
            string[] results = await SearchAsync(text, cancel.Token);
            Show(results);
        }
        catch (OperationCanceledException)
        {
        }
    }
}
```

It works, but the rules are spread across two fields, a `try`, and an `async void` method. Nothing removes the
handler. Nothing disposes the `CancellationTokenSource`. And testing the 300 ms wait means waiting 300 ms.

### With a stream

```csharp
IDisposable subscription = Signal.FromEvent<string>(
        handler => input.TextChanged += handler,
        handler => input.TextChanged -= handler)
    .Calm(TimeSpan.FromMilliseconds(300))
    .Unique()
    .Select(text => Signal.FromAsync(token => SearchAsync(text, token)))
    .SwitchTo()
    .WitnessOn(ui)
    .Subscribe(Show);
```

Each rule is one line:

- `Calm` waits for 300 ms of quiet.
- `Unique` skips text equal to the text before it.
- `Select` starts a search for each piece of text, and `SwitchTo` follows only the newest one, cancelling the
  search before it.
- `WitnessOn` shows the results on the UI thread.

Disposing `subscription` removes the event handler and cancels any running search. There is nothing else to
clean up.

Typing `r`, `rx`, a pause, then `rxu`, `rx`, a pause, then `rxui` gives:

```text
searching for rx
searching for rxui
cancelled search for rx
show ReactiveUI
```

The second pause settled on `rx` again, so `Unique` skipped it.

To test it, pass a `VirtualClock` to `Calm` and move time forward yourself. The test runs instantly. See
[testing with a virtual clock](scheduling.md#testing-with-a-virtual-clock).

## What else streams make easier

### Generating values

Factories build a stream from a rule, rather than a loop you write and run on a thread:

```csharp
Signal.Unfold(1, static n => n <= 100, static n => n * 2, static n => n)
      .Subscribe(n => Console.Write($"{n} "));   // 1 2 4 8 16 32 64
```

`Every` ticks on a timer, `Range` counts, and `Create` lets you write any source by hand. Because each one
takes a sequencer, the same code runs on real time in the app and virtual time in a test. See
[creation factories](creation-factories.md).

### Threads without locks

Moving work between threads with raw `Thread`, `Task.Run` and `lock` means deciding, at every call, which thread
you are on and what else might run at the same time.

With streams, you say it once in the chain. `WitnessOn` picks the thread your callback runs on. `Serialize`
makes sure your callback handles one value at a time. See [utility](utility.md) and
[sequencers and scheduling](scheduling.md).

### Combining sources

Joining two events by hand needs a field for each side's latest value and a lock around them. `SyncLatest`,
`Zip` and `Blend` do it in one call. See [combination](combination.md).

## When not to use a stream

Streams are not the answer to everything:

- **One async call with one result.** `await` a `Task`. A stream adds nothing.
- **A simple event with one handler, for the life of the app.** A plain `event` is fine.
- **A loop over data you already have.** Use LINQ on the collection.

Reach for a stream when values arrive over time **and** you need to shape, combine, time, or cancel them.

## What Primitives gives you

### Speed and fewer allocations

Each operator in Primitives has its own **sink**: the object that receives values from the source and passes
results on. A sink is written for one job, with the threading that job needs built in, and no general layer
between it and your code. That is a design choice. The library has more types, and in return each value costs
less time and memory to deliver. The [System.Reactive comparison](system-reactive.md#performance) has the
measured numbers.

### Native AOT and trimming

Every Primitives package is marked `IsAotCompatible`, and nothing in the library finds members by reflection.
Code you write against it works the same when you publish with Native AOT or trimming, on phones, in the browser,
and in small container images. See [Native AOT](system-reactive.md#native-aot).

### A signal for each job

Beyond the plain `Signal<T>`, there is a signal for each common job. Signals can hold a settable state, order
values from many threads, run a command, or let only a set number of values through at a time. See [signals](signals.md).

### Building blocks you can reach

The types behind each operator are public. Sequencers are yours to choose for anything that involves time or
threads. A stream is **cold** by default: each subscriber gets its own run, unless you choose to share it. See
[sharing one subscription](sharing.md).

That makes Primitives a little lower level than a library that hides these choices. It also means nothing happens
that you did not ask for.

## Why ReactiveUI uses Primitives

Streams are the base idea of every ReactiveUI library. Commands, bindings, `WhenAnyValue`, activation and
property helpers are all built from them. So the stream library underneath decides how fast, and how small, all of
them can be.

ReactiveUI was built on System.Reactive. ReactiveUI.Primitives gives those libraries a faster, lighter base, written
for modern .NET and AOT, while keeping the same interfaces and operator names.

Compatibility stays. ReactiveUI ships two sets of packages, both running on Primitives:

| Packages | Public reactive types |
|---|---|
| `ReactiveUI`, `ReactiveUI.Wpf`, `ReactiveUI.Maui` and the other platform packages | `RxVoid`, `ISequencer`, `Signal<T>` |
| `ReactiveUI.Reactive`, `ReactiveUI.Wpf.Reactive`, `ReactiveUI.Maui.Reactive` and so on | `Unit`, `IScheduler`, `Subject<T>` |

Choose the `.Reactive` packages when your app uses another library that needs System.Reactive's types, or to
upgrade existing code without changing it. See
[using both libraries together](system-reactive.md#using-both-libraries-together).

## Where to go next

- [The overview](index.md) walks through your first stream.
- [ReactiveUI.Primitives and System.Reactive](system-reactive.md) covers the differences in detail.
- [Best practices](best-practices.md) covers the habits that avoid leaks and frozen screens.
- [ReactiveUI's handbook](../reactiveui/handbook/index.md) shows streams at work in view models, commands and bindings.
