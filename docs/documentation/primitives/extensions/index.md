---
Order: 14
---
# Extension helpers

The operator pages cover the core operators. `ReactiveUI.Primitives` also ships a set of **extension helpers**:
ready-made answers to jobs that come up again and again in apps. Retrying with a growing delay, running async work
for each value, spotting a stream that has gone quiet, and blocking a test until a value arrives are all one call.

The helpers are extension methods, like every other operator. They sit in their own namespace, so add one more
`using`:

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Extensions;
using ReactiveUI.Primitives.Signals;
```

They ship in the `ReactiveUI.Primitives` package, so there is nothing extra to install.

## Your first helper

You have a name box. You want to save the name when the user stops typing. You do not want to save the same name
twice, and when a new save starts, the result of an older one no longer matters.

**1. Make a stream of what the user types.** In an app this comes from the text box. Here a `Signal<T>` stands in
for it. A **signal** is a stream you push values into with `OnNext`.

```csharp
var nameBox = new Signal<string>();
```

**2. Wait for a pause, and skip repeats.** `ThrottleDistinct` waits until the stream has been quiet for the time
you give. It then sends the newest value, unless that value equals the last one it sent.

**3. Save, keeping only the newest result.** `SelectLatestAsync` runs an async method for each value. If a newer
value arrives while an older save is still running, the older result is dropped.

```csharp
IObservable<string> saved = nameBox
    .ThrottleDistinct(TimeSpan.FromMilliseconds(300))
    .SelectLatestAsync(static name => SaveAsync(name));

static async Task<string> SaveAsync(string name)
{
    await Task.Delay(100);
    return $"saved {name}";
}
```

The lambda is marked `static` because it captures nothing. See
[best practices](../best-practices.md#mark-lambdas-static).

**4. Subscribe, and keep the subscription so you can dispose it.**

```csharp
using IDisposable subscription = saved.Subscribe(
    static result => Console.WriteLine(result),
    static error => Console.WriteLine($"failed: {error.Message}"));
```

**5. Type.**

```csharp
nameBox.OnNext("A");
nameBox.OnNext("Ad");
nameBox.OnNext("Ada");     // the user pauses here
Thread.Sleep(600);
nameBox.OnNext("Ada");     // the same name again
Thread.Sleep(600);
nameBox.OnNext("Grace");
Thread.Sleep(600);
```

Output:

```text
saved Ada
saved Grace
```

Three key presses became one save. Typing the same name again saved nothing.

## The helper pages

| Page | What it covers |
|---|---|
| [Shaping values](values.md) | Filtering `null`, `bool` and text, pairing values, splitting a stream, picking a first match. |
| [Timing and threads](timing.md) | Batching on a pause, rate limits, spotting a quiet stream, shared timers, and choosing a thread. |
| [Errors and retries](errors.md) | Replacing an error with a value, logging, and retrying with a fixed or growing delay. |
| [Tasks and async work](tasks.md) | Running async work for each value, limiting how much runs at once, and turning a stream into a `Task`. |
| [State, property changes and tests](state-and-testing.md) | A current value, `INotifyPropertyChanged`, running code on subscribe or dispose, and blocking a test until a result arrives. |

## How these relate to the core operators

Some helpers do a job a core operator can also do, in a single named call. `CatchAndReturn(value)`, for example, is
[`Recover`](../error-handling.md) with a stream of one value. Use whichever reads better in your code. The helper
pages link to the core operator where one exists.

## The two package flavours

Every helper ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.Extensions`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Extensions.Reactive`. They behave the same in both.
