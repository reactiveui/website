---
Order: 5
---
# Time

A time operator changes *when* values arrive. It can hold a value back, wait for things to calm down, send
values on a timer, gather them into batches, or give up when nothing happens for too long.

Most of these operators time things with a clock. By default that clock runs on a background thread. Every
operator also takes a **sequencer**, which decides the clock it times with and the thread that runs your code.
The last section of this page shows how to pass one to test time without waiting for real seconds.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
```

As on the combination page, the examples push values by hand into a `Signal<T>`, so you can see exactly what
happens at each moment.

## Your first time operator

You have a search box. You want to search when the user stops typing, not on every key they press.

**1. Make a stream of what the user types.**

```csharp
var typing = new Signal<string>();
```

**2. Wait for a pause.** `Calm` holds each value back. If a newer value arrives before the wait is over, it
throws the old one away and starts waiting again. It sends a value only once the stream has been quiet for the
time you give.

```csharp
IObservable<string> searches = typing.Calm(TimeSpan.FromMilliseconds(300));
```

**3. Subscribe.**

```csharp
searches.Subscribe(term => Console.WriteLine($"search for {term}"));
```

**4. Type quickly, then stop.**

```csharp
typing.OnNext("a");      // at 0 ms
typing.OnNext("ad");     // at 100 ms
typing.OnNext("ada");    // at 200 ms, then the user stops
```

Output:

```text
search for ada
```

Trace it against the clock:

| Time | The user types | What `Calm` does |
|---|---|---|
| 0 ms | `a` | Starts waiting 300 ms. |
| 100 ms | `ad` | Throws away `a`. Starts waiting again. |
| 200 ms | `ada` | Throws away `ad`. Starts waiting again. |
| 500 ms | nothing | 300 ms of quiet. Sends `ada`. |

You searched once, for the word the user finished typing, instead of three times.

## Holding values back

### `Shift`

`Shift` holds every value back by the same amount of time. The gaps between values stay the same.

Input: `1` at 0 ms, `2` at 100 ms

```csharp
var source = new Signal<int>();

source.Shift(TimeSpan.FromMilliseconds(300))
      .Subscribe(x => Console.WriteLine(x));

source.OnNext(1);   // at 0 ms
source.OnNext(2);   // at 100 ms
```

Output:

| Time | Output |
|---|---|
| 300 ms | `1` |
| 400 ms | `2` |

Completion is held back too. A stream that completes at 0 ms reports its completion at 300 ms, straight after its
last value.

### `Delay` until a set time

`Delay` takes a `DateTimeOffset` instead of a `TimeSpan`. It holds the first value until that time, and keeps the
gaps between the values after it.

Input: `1` at 0 ms, `2` at 100 ms, delayed until 500 ms after you subscribe

```csharp
DateTimeOffset startAt = DateTimeOffset.Now.AddMilliseconds(500);

source.Delay(startAt)
      .Subscribe(x => Console.WriteLine(x));
```

Output:

| Time | Output |
|---|---|
| 500 ms | `1` |
| 600 ms | `2` |

`2` arrived 100 ms after `1`, so it goes out 100 ms after `1` does.

## Delaying the start

### `DelayStart`

`DelayStart` waits before it subscribes to the source at all. Until then, it is not listening.

With a stream that starts its work when you subscribe, such as `Signal.Range`, nothing is lost. The work
just starts later.

Input: `Signal.Range(1, 2)`, with a 300 ms delay

```csharp
Signal.Range(1, 2)
      .DelayStart(TimeSpan.FromMilliseconds(300))
      .Subscribe(x => Console.WriteLine(x));
```

Output, all at 300 ms:

```text
1
2
```

### `DelaySubscription` until a set time

To subscribe at a set moment instead of after an amount of time, call `DelaySubscription` with a
`DateTimeOffset`.

Input: `Signal.Range(1, 2)`, subscribed 300 ms after you call `Subscribe`

```csharp
DateTimeOffset subscribeAt = DateTimeOffset.Now.AddMilliseconds(300);

Signal.Range(1, 2)
      .DelaySubscription(subscribeAt)
      .Subscribe(x => Console.WriteLine(x));
```

Output, all at 300 ms:

```text
1
2
```

A moment that has passed already subscribes at once.

### `Shift` against `DelayStart`

Both make values arrive later, and they are easy to confuse. The difference shows with a `Signal<T>`, which
sends values whether or not anyone is listening.

Input: `1` at 100 ms, `2` at 400 ms, and a 300 ms delay

| Operator | Output | What happened |
|---|---|---|
| `Shift` | `1` at 400 ms, `2` at 700 ms | It listened from the start and held each value back 300 ms. |
| `DelayStart` | `2` at 400 ms | It did not listen until 300 ms. It missed `1`, which was sent at 100 ms. |

Use `Shift` to slow values down. Use `DelayStart` to put off starting some work.

> [!WARNING]
> `DelayStart` can lose values from a stream that sends whether or not anyone is subscribed. Anything sent
> before the delay ends is gone.

## Waiting for a quiet moment

### `Calm`

`Calm` sends a value once no newer value has arrived for the time you give. The walkthrough at the top of this
page shows it step by step.

When the source completes, `Calm` sends the value it was holding straight away. It does not wait out the rest
of the quiet time.

Input: `1` at 0 ms, `2` at 100 ms, then the source completes at 100 ms

```csharp
var source = new Signal<int>();

source.Calm(TimeSpan.FromMilliseconds(300))
      .Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("done"));

source.OnNext(1);        // at 0 ms
source.OnNext(2);        // at 100 ms
source.OnCompleted();    // at 100 ms
```

Output, both at 100 ms:

```text
2
done
```

> [!NOTE]
> `Calm` only sends after a pause. A stream that sends a value more often than the wait, and never pauses,
> sends nothing through `Calm` until it completes. Use `Probe` if you need values to keep coming out.

### `EmitIfQuiet`

`EmitIfQuiet` works the same as `Calm`, with one difference. Give it a time of zero or less and it hands back
your stream unchanged, so every value goes straight through.

Input: `1`, `2` and `3`, all at 0 ms

```csharp
source.EmitIfQuiet(TimeSpan.Zero)
      .Subscribe(x => Console.WriteLine(x));
```

Output, all at 0 ms:

```text
1
2
3
```

Use it when the wait comes from a setting, and zero means "do not wait". You avoid writing an `if` around the
call.

## Sending the newest value on a timer

### `Probe`

`Probe` sends the newest value after a set time, and ignores the values in between.

It starts timing when a value arrives. When the time is up, it sends the newest value it has seen, then stops
timing. The next value that arrives starts the timer again.

Input: `1` at 0 ms, `2` at 50 ms, `3` at 100 ms, `4` at 410 ms, `5` at 460 ms

```csharp
var source = new Signal<int>();

source.Probe(TimeSpan.FromMilliseconds(200))
      .Subscribe(x => Console.WriteLine(x));
```

Output:

| Time | Output | Why |
|---|---|---|
| 200 ms | `3` | `1` started the timer at 0 ms. `3` was the newest value when it ran out. |
| 610 ms | `5` | `4` started the timer again at 410 ms. `5` was the newest value when it ran out. |

Nothing went out between 200 ms and 410 ms. No new value arrived, so `Probe` had nothing to send and was not
timing. It never sends the same value twice.

Because each timer starts from a value, the gaps between outputs depend on when values arrive. Under a steady
stream of values every 70 ms, a 200 ms `Probe` sends at 200 ms, 410 ms, 620 ms and so on. It does not tick on
a fixed schedule.

When the source completes, `Probe` sends the value it was holding straight away, then completes. It does not wait
for the timer to run out.

### `Calm` against `Probe`

Both cut down how many values get through, and both keep the newest one. They decide *when* to send in
opposite ways.

Input: `1` at 0 ms, `2` at 50 ms, `3` at 100 ms, `4` at 410 ms, `5` at 460 ms, each operator given 200 ms

| Operator | Output | Sends a value when |
|---|---|---|
| `Calm` | `3` at 300 ms, `5` at 660 ms | 200 ms pass with no new value. |
| `Probe` | `3` at 200 ms, `5` at 610 ms | 200 ms after the value that started its timer. |

`Calm` waited for the typing to stop. `Probe` did not wait for a pause. It sent as soon as 200 ms had passed.

Use `Calm` when you want the final value once things settle, such as a search term. Use `Probe` when you want
regular updates while things are still changing, such as a progress bar that should redraw every 200 ms rather
than on every tiny change.

## Gathering values into batches

### `Buffer` by count

`Buffer(count)` gathers values into lists of `count` values. It sends each list when it is full. When the
source completes, it sends whatever is left, even if the last list is short.

Input: `1` to `7`

```csharp
Signal.Range(1, 7)
      .Buffer(3)
      .Subscribe(batch => Console.Write($"[{string.Join(",", batch)}] "));
```

Output:

```text
[1,2,3] [4,5,6] [7]
```

The batches never overlap. Each value goes into exactly one list.

### `Buffer` by count, with a step

`Buffer(count, skip)` starts a new list every `skip` values instead of every `count`. Each list still holds up
to `count` values.

When the skip is smaller than the count, the lists overlap. That gives you a **sliding window**: each list is the
last few values, moved along by one step at a time.

Input: `1` to `5`, lists of 3, a new list every value

```csharp
Signal.Range(1, 5)
      .Buffer(3, 1)
      .Subscribe(batch => Console.Write($"[{string.Join(",", batch)}] "));
```

Output:

```text
[1,2,3] [2,3,4] [3,4,5] [4,5] [5]
```

Use it to work out a moving average, where each result covers the last three readings.

When the skip is larger than the count, some values fall in the gap between lists and are left out:

```csharp
Signal.Range(1, 7)
      .Buffer(2, 3)
      .Subscribe(batch => Console.Write($"[{string.Join(",", batch)}] "));
```

Output:

```text
[1,2] [4,5] [7]
```

`3` and `6` fell between lists. When the skip equals the count, the lists sit end to end, the same as
`Buffer(count)`.

When the source completes, `Buffer` sends every list that was still filling, which is why the lists at the end of
the first example are short.

### `Buffer` by time

`Buffer` with a `TimeSpan` gathers values into a list for that long, then sends the list.

The first value in a batch starts the timer. Every value that arrives before the time is up joins that batch.
When the time is up, `Buffer` sends the batch. The next value starts a new batch.

Input: `1` at 50 ms, `2` at 100 ms, `3` at 300 ms

```csharp
var source = new Signal<int>();

source.Buffer(TimeSpan.FromMilliseconds(200))
      .Subscribe(batch => Console.WriteLine($"[{string.Join(",", batch)}]"));
```

Output:

| Time | Output | Why |
|---|---|---|
| 250 ms | `[1,2]` | `1` started the batch at 50 ms. The time was up at 250 ms. |
| 500 ms | `[3]` | `3` started a new batch at 300 ms. The time was up at 500 ms. |

`Buffer` never sends an empty list. While no values arrive, it is not timing and sends nothing. When the source
completes, it sends the batch it was gathering straight away.

## Giving up when nothing happens

### `Expire`

`Expire` fails the stream with a `TimeoutException` when no value arrives in time. Each value that arrives
starts the wait again, so it only fails after a gap longer than the time you give.

Input: `1` at 100 ms, `2` at 250 ms, `3` at 400 ms, then nothing, with a 200 ms limit

```csharp
var source = new Signal<int>();

source.Expire(TimeSpan.FromMilliseconds(200))
      .Subscribe(
          x => Console.WriteLine(x),
          error => Console.WriteLine($"{error.GetType().Name}"));
```

Output:

| Time | Output |
|---|---|
| 100 ms | `1` |
| 250 ms | `2` |
| 400 ms | `3` |
| 600 ms | `TimeoutException` |

Every gap until `3` was shorter than 200 ms, so the stream kept going. After `3`, nothing arrived for 200 ms,
so `Expire` failed it at 600 ms.

The wait starts when you subscribe. A stream that sends nothing at all fails after the first 200 ms. A stream
that completes before any gap grows too long never fails.

Use it to notice a connection that has gone silent, such as a live feed that should send an update at least
once a second.

It is also available as a factory that takes the stream as its first argument: `Signal.Expire(source, time)`.

### `Timeout` at a set time

`Timeout` takes a `DateTimeOffset`. It is a **deadline**: the stream must complete before that time. Values
arriving do not move the deadline. If the stream is still running when the deadline passes, `Timeout` fails it.

Input: `1` at 100 ms, `2` at 250 ms, `3` at 400 ms, with a deadline 300 ms after you subscribe

```csharp
var source = new Signal<int>();
DateTimeOffset deadline = DateTimeOffset.Now.AddMilliseconds(300);

source.Timeout(deadline)
      .Subscribe(
          x => Console.WriteLine(x),
          error => Console.WriteLine(error.GetType().Name));
```

Output:

| Time | Output |
|---|---|
| 100 ms | `1` |
| 250 ms | `2` |
| 300 ms | `TimeoutException` |

`3` never arrived. The deadline came at 300 ms, even though a value had arrived only 50 ms before it.

### `Expire` against `Timeout` at a set time

They look alike and answer different questions.

Input: `1` at 100 ms, `2` at 250 ms, `3` at 400 ms, then nothing, with a limit of 300 ms

| Operator | Output | Fails when |
|---|---|---|
| `Expire(TimeSpan.FromMilliseconds(300))` | `1 2 3`, then `TimeoutException` at 700 ms | No value arrives for 300 ms. Each value starts the wait again. |
| `Timeout(start + 300 ms)` | `1 2`, then `TimeoutException` at 300 ms | The stream has not completed by the set moment. |

Use `Expire` to notice a stream that has gone quiet. Use `Timeout` with a `DateTimeOffset` to put a hard limit on
how long the whole job can take.

## Testing time without waiting

A test that waits 300 real milliseconds is slow, and on a busy machine it can fail for no reason. Pass a
`VirtualClock` as the sequencer instead. A `VirtualClock` only moves forward when you tell it to, so your test
controls time exactly and runs instantly.

```csharp
using ReactiveUI.Primitives.Concurrency;

var clock = new VirtualClock();
var searches = new Signal<string>();
var results = new List<string>();

searches.Calm(TimeSpan.FromMilliseconds(300), clock)
        .Subscribe(term => results.Add(term));

searches.OnNext("a");
searches.OnNext("ad");
searches.OnNext("ada");

clock.AdvanceBy(TimeSpan.FromMilliseconds(299));
// results is still empty: 300 ms have not passed

clock.AdvanceBy(TimeSpan.FromMilliseconds(1));
// results now holds "ada"
```

`AdvanceBy` moves the clock forward by an amount of time. `AdvanceTo` moves it to a set moment. Any work due
by then runs as you call it, in order.

Every operator on this page that works with time takes a sequencer as its last argument, so every one of them
can be tested this way. `Buffer` by count does not take one, because it does not use a clock.

## Time operators at a glance

| Operator | Second name | What it does |
|---|---|---|
| `Shift` | `Delay` | Holds every value back by the same time. |
| `Delay(DateTimeOffset)` | — | Holds values until a set moment, and keeps the gaps between them. |
| `DelayStart` | `DelaySubscription` | Waits before subscribing to the source. |
| `DelaySubscription(DateTimeOffset)` | — | Waits until a set moment before subscribing to the source. |
| `Calm` | `Throttle`, `Stabilize` | Sends a value once the stream has been quiet for the time you give. |
| `EmitIfQuiet` | — | Works like `Calm`, and passes every value through when the time is zero or less. |
| `Probe` | `Sample` | Sends the newest value a set time after a value starts its timer. |
| `Buffer(count)` | — | Gathers values into lists of a fixed size. |
| `Buffer(count, skip)` | — | Gathers lists of a fixed size, starting a new one every `skip` values. |
| `Buffer(TimeSpan)` | `Collect` | Gathers values into a list for a set time after the first one arrives. |
| `Expire` | `Timeout(TimeSpan)` | Fails the stream when no value arrives in time. |
| `Timeout(DateTimeOffset)` | — | Fails the stream if it has not completed by a set moment. |

## The types behind these operators

`ShiftSignal<T>`, `CalmSignal<T>`, `ProbeSignal<T>`, `ExpireSignal<T>`, `AbsoluteExpireSignal<T>` and
`CollectSignal<T>` are public classes in `ReactiveUI.Primitives.Advanced`. `AbsoluteExpireSignal<T>` is the type
behind `Timeout` at a set time, and `CollectSignal<T>` is behind `Buffer` by time. Each takes its source, its time
and a sequencer through the constructor. The operators let you leave the sequencer out. The constructors do not.

```csharp
using ReactiveUI.Primitives.Advanced;

// the operator
IObservable<string> a = searches.Calm(TimeSpan.FromMilliseconds(300), clock);

// the same thing, built directly
IObservable<string> b = new CalmSignal<string>(searches, TimeSpan.FromMilliseconds(300), clock);
```

Calling the operator is the normal path. Construct the type when you are writing an operator of your own and
want to place one inside it.

## The two package flavours

Every operator here ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.*`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Reactive.*`. They behave the same in both.
