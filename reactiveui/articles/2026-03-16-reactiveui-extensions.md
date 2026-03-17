---
NoTitle: true
IsBlog: true
Title: Mastering ReactiveUI.Extensions 
Tags: Article
Author: Chris Pulman
Published: 2026-03-16
---
# Mastering ReactiveUI.Extensions: A Comprehensive Guide to Async Reactive Programming in .NET

Author: Chris Pulman  
Published: March 16, 2026

ReactiveUI.Extensions represents a significant evolution in reactive programming for .NET, providing a **fully async-native observable framework** that bridges the gap between traditional `System.Reactive` (Rx.NET) and modern async/await patterns. This library introduces `IObservableAsync<T>`, `IObserverAsync<T>`, and a comprehensive suite of operators that work seamlessly with `ValueTask`, `CancellationToken`, and `IAsyncDisposable` throughout the entire reactive pipeline.

This article provides an extensive, in-depth technical exploration of ReactiveUI.Extensions, covering every public function, advanced multi-threading scenarios, and practical patterns for building robust, scalable reactive applications.

---

## Table of Contents

1. [Introduction to ReactiveUI.Extensions](#introduction)
2. [Understanding IObservableAsync<T> vs IObservable<T>](#observable-comparison)
3. [Understanding IObserverAsync<T> vs IObserver<T>](#observer-comparison)
4. [Core Async Interfaces and Types](#core-interfaces)
5. [Factory Methods and Observable Creation](#factory-methods)
6. [Transformation Operators](#transformation-operators)
7. [Filtering Operators](#filtering-operators)
8. [Combining and Merging Operators](#combining-operators)
9. [Error Handling and Resilience](#error-handling)
10. [Timing and Scheduling](#timing-scheduling)
11. [Aggregation and Terminal Operators](#aggregation-operators)
12. [Async Disposables and Resource Management](#async-disposables)
13. [Subjects and Multicasting](#subjects-multicasting)
14. [Bridging Classic and Async Observables](#bridging)
15. [Classic Reactive Extensions Operators](#classic-operators)
16. [Advanced Multi-Threading Examples](#advanced-examples)
17. [Performance Considerations](#performance)
18. [Best Practices](#best-practices)

---

## 1. Introduction to ReactiveUI.Extensions <a name="introduction"></a>

### Why ReactiveUI.Extensions?

Traditional `System.Reactive` (Rx.NET) was designed before the widespread adoption of `async`/`await` in C#. While Rx.NET provides excellent support for asynchronous data streams, it has several limitations in modern .NET development:

1. **Synchronous Observer Callbacks**: `IObserver<T>` methods (`OnNext`, `OnError`, `OnCompleted`) are synchronous, which can lead to thread pool starvation when performing async operations
2. **No Built-in Cancellation**: Traditional observables don't integrate naturally with `CancellationToken`
3. **Synchronous Disposal**: `IDisposable` doesn't support async cleanup operations
4. **Task Integration Friction**: Converting between `Task`, `IAsyncEnumerable`, and `IObservable` requires boilerplate

ReactiveUI.Extensions solves these problems by providing:

- **End-to-End Async**: Every notification (`OnNextAsync`, `OnErrorResumeAsync`, `OnCompletedAsync`) returns `ValueTask`
- **Cancellation-First Design**: Every operator accepts `CancellationToken`
- **Async Disposal**: Subscriptions return `IAsyncDisposable` for proper async resource cleanup
- **Seamless Interop**: Bidirectional bridging between `IObservable` and `IObservableAsync`
- **Modern .NET Support**: Requires .NET 8 or later, leveraging modern language features

### Installation

```bash
dotnet add package ReactiveUI.Extensions
```

**Supported Target Frameworks**: .NET 4.6.2, .NET 4.7.2, .NET 4.8.1, .NET 8, .NET 9, .NET 10

---

## 2. Understanding IObservableAsync<T> vs IObservable<T> <a name="observable-comparison"></a>

### IObservable<T> (Traditional Rx.NET)

```csharp
public interface IObservable<out T>
{
    IDisposable Subscribe(IObserver<T> observer);
}
```

**Characteristics**:
- Synchronous subscription
- Returns `IDisposable` for cleanup
- Observer callbacks are synchronous
- No built-in cancellation support
- Can block threads during async operations

### IObservableAsync<T> (ReactiveUI.Extensions)

```csharp
public interface IObservableAsync<T>
{
    ValueTask<IAsyncDisposable> SubscribeAsync(
        IObserverAsync<T> observer, 
        CancellationToken cancellationToken);
}
```

**Characteristics**:
- Asynchronous subscription (`ValueTask`)
- Returns `IAsyncDisposable` for async cleanup
- Observer callbacks are asynchronous (`ValueTask`)
- Built-in `CancellationToken` support
- Non-blocking throughout the pipeline

### Key Differences Table

| Feature | IObservable<T> | IObservableAsync<T> |
|---------|---------------|---------------------|
| Subscription | Synchronous | Asynchronous (`ValueTask`) |
| Disposal | `IDisposable` | `IAsyncDisposable` |
| OnNext | `void` | `ValueTask` |
| OnError | `void` | `ValueTask` |
| OnCompleted | `void` | `ValueTask` |
| Cancellation | Manual | Built-in `CancellationToken` |
| Thread Blocking | Possible | Never |
| .NET Version | Any | .NET 8+ |

### When to Use Each

**Use IObservableAsync<T> when**:
- Building new async-first applications
- Working with I/O-bound operations (network, database, file system)
- Needing proper cancellation support
- Requiring async resource cleanup
- Targeting .NET 8 or later

**Use IObservable<T> when**:
- Maintaining legacy code
- Working with CPU-bound operations
- Interoperating with existing Rx.NET libraries
- Targeting older .NET versions

---

## 3. Understanding IObserverAsync<T> vs IObserver<T> <a name="observer-comparison"></a>

### IObserver<T> (Traditional)

```csharp
public interface IObserver<in T>
{
    void OnNext(T value);
    void OnError(Exception error);
    void OnCompleted();
}
```

**Problems**:
1. **No Async Support**: Cannot await async operations in callbacks
2. **Exception Handling**: Exceptions in callbacks can crash the application
3. **Resource Cleanup**: No async disposal mechanism
4. **Backpressure**: Difficult to implement async backpressure

### IObserverAsync<T> (ReactiveUI.Extensions)

```csharp
public interface IObserverAsync<in T> : IAsyncDisposable
{
    ValueTask OnNextAsync(T value, CancellationToken cancellationToken);
    ValueTask OnErrorResumeAsync(Exception error, CancellationToken cancellationToken);
    ValueTask OnCompletedAsync(Result result);
}
```

**Advantages**:
1. **Full Async Support**: All callbacks are `ValueTask`-based
2. **Error Recovery**: `OnErrorResumeAsync` allows error recovery and continuation
3. **Async Disposal**: Implements `IAsyncDisposable` for proper cleanup
4. **Cancellation**: Every callback receives `CancellationToken`
5. **Result Tracking**: `OnCompletedAsync` receives `Result` indicating success/failure

### Practical Example: Traditional vs Async Observer

```csharp
// Traditional IObserver - Problem: Blocking async operations
public class TraditionalObserver : IObserver<string>
{
    public void OnNext(string value)
    {
        // BAD: Blocking async operation
        var result = SaveToDatabaseAsync(value).Result; // Thread pool starvation!
        Console.WriteLine($"Saved: {result}");
    }
    
    public void OnError(Exception error) => Console.WriteLine($"Error: {error}");
    public void OnCompleted() => Console.WriteLine("Completed");
}

// Async IObserverAsync - Solution: Non-blocking throughout
public class AsyncObserver : ObserverAsync<string>
{
    protected override async ValueTask OnNextAsyncCore(
        string value, 
        CancellationToken cancellationToken)
    {
        // GOOD: Non-blocking async operation
        var result = await SaveToDatabaseAsync(value, cancellationToken);
        Console.WriteLine($"Saved: {result}");
    }
    
    protected override async ValueTask OnErrorResumeAsyncCore(
        Exception error, 
        CancellationToken cancellationToken)
    {
        // Can attempt recovery
        await LogErrorAsync(error, cancellationToken);
        // Can choose to continue or stop
    }
    
    protected override async ValueTask OnCompletedAsyncCore(Result result)
    {
        await CleanupAsync();
        Console.WriteLine($"Completed: {result.IsSuccess}");
    }
    
    protected override ValueTask DisposeAsyncCore() => CleanupResourcesAsync();
}
```

### Error Recovery with OnErrorResumeAsync

One of the most powerful features of `IObserverAsync<T>` is the ability to recover from errors:

```csharp
public class ResilientObserver : ObserverAsync<int>
{
    private int _retryCount = 0;
    private const int MaxRetries = 3;
    
    protected override async ValueTask OnNextAsyncCore(
        int value, 
        CancellationToken cancellationToken)
    {
        try
        {
            await ProcessValueAsync(value, cancellationToken);
        }
        catch (Exception ex) when (ex is TimeoutException)
        {
            // Error will be sent to OnErrorResumeAsyncCore
            throw;
        }
    }
    
    protected override async ValueTask OnErrorResumeAsyncCore(
        Exception error, 
        CancellationToken cancellationToken)
    {
        if (_retryCount < MaxRetries)
        {
            _retryCount++;
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            // Continue processing - don't propagate error
            return;
        }
        
        // Max retries exceeded - log and continue
        await LogErrorAsync(error, cancellationToken);
    }
    
    protected override ValueTask OnCompletedAsyncCore(Result result)
    {
        Console.WriteLine($"Completed with {(result.IsSuccess ? "success" : "failure")}");
        return default;
    }
}
```

---

## 4. Core Async Interfaces and Types <a name="core-interfaces"></a>

### IObservableAsync<T>

```csharp
public interface IObservableAsync<out T>
{
    ValueTask<IAsyncDisposable> SubscribeAsync(
        IObserverAsync<T> observer, 
        CancellationToken cancellationToken);
}
```

**Purpose**: Represents an asynchronous push-based notification provider.

**Key Points**:
- Subscription is asynchronous (`ValueTask`)
- Returns `IAsyncDisposable` for async cleanup
- Supports cancellation via `CancellationToken`
- Thread-safe for concurrent subscriptions

### IObserverAsync<T>

```csharp
public interface IObserverAsync<in T> : IAsyncDisposable
{
    ValueTask OnNextAsync(T value, CancellationToken cancellationToken);
    ValueTask OnErrorResumeAsync(Exception error, CancellationToken cancellationToken);
    ValueTask OnCompletedAsync(Result result);
}
```

**Purpose**: Defines an asynchronous observer that receives notifications.

**Key Points**:
- All callbacks are asynchronous
- Implements `IAsyncDisposable` for cleanup
- `OnErrorResumeAsync` allows error recovery
- `Result` parameter indicates completion status

### ObservableAsync<T> (Abstract Base Class)

```csharp
public abstract class ObservableAsync<T> : IObservableAsync<T>
{
    public async ValueTask<IAsyncDisposable> SubscribeAsync(
        IObserverAsync<T> observer, 
        CancellationToken cancellationToken)
    {
        var subscription = await SubscribeAsyncCore(observer, cancellationToken);
        return subscription;
    }
    
    protected abstract ValueTask<IAsyncDisposable> SubscribeAsyncCore(
        IObserverAsync<T> observer, 
        CancellationToken cancellationToken);
}
```

**Purpose**: Base class for creating custom async observables.

**Example: Custom Async Observable**

```csharp
public class TickObservable : ObservableAsync<int>
{
    private readonly int _count;
    private readonly TimeSpan _interval;
    
    public TickObservable(int count, TimeSpan interval)
    {
        _count = count;
        _interval = interval;
    }
    
    protected override async ValueTask<IAsyncDisposable> SubscribeAsyncCore(
        IObserverAsync<int> observer, 
        CancellationToken cancellationToken)
    {
        var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        
        _ = Task.Run(async () =>
        {
            try
            {
                for (int i = 0; i < _count && !cts.Token.IsCancellationRequested; i++)
                {
                    await observer.OnNextAsync(i, cts.Token);
                    await Task.Delay(_interval, cts.Token);
                }
                
                await observer.OnCompletedAsync(Result.Success);
            }
            catch (OperationCanceledException)
            {
                await observer.OnCompletedAsync(Result.Success);
            }
            catch (Exception ex)
            {
                await observer.OnErrorResumeAsync(ex, cts.Token);
            }
        }, cts.Token);
        
        return new CancellationDisposable(cts);
    }
}

// Usage
var ticks = new TickObservable(10, TimeSpan.FromMilliseconds(100));
await using var subscription = await ticks.SubscribeAsync(
    async (value, ct) => Console.WriteLine($"Tick: {value}"),
    CancellationToken.None);
```

### ConnectableObservableAsync<T>

```csharp
public class ConnectableObservableAsync<T> : ObservableAsync<T>
{
    public ValueTask<IAsyncDisposable> ConnectAsync(CancellationToken cancellationToken);
}
```

**Purpose**: Extends `ObservableAsync` with deferred connection for multicasting.

**Use Case**: When you want to control when the source starts emitting (e.g., after multiple subscribers are ready).

### Result

```csharp
public readonly struct Result
{
    public bool IsSuccess { get; }
    public Exception? Exception { get; }
    
    public static Result Success { get; } = new(true, null);
    public static Result Failure(Exception exception) => new(false, exception);
}
```

**Purpose**: Carries completion status (success or failure) to `OnCompletedAsync`.

---

## 5. Factory Methods and Observable Creation <a name="factory-methods"></a>

### ObservableAsync.Create

Creates an observable from an async subscription delegate.

```csharp
public static IObservableAsync<T> Create<T>(
    Func<IObserverAsync<T>, CancellationToken, ValueTask<IAsyncDisposable>> subscribeAsync)
```

**Example**:

```csharp
var custom = ObservableAsync.Create<string>(async (observer, ct) =>
{
    await observer.OnNextAsync("Hello", ct);
    await observer.OnNextAsync("World", ct);
    await observer.OnCompletedAsync(Result.Success);
    return DisposableAsync.Empty;
});

await using var sub = await custom.SubscribeAsync(
    async (value, ct) => Console.WriteLine(value),
    CancellationToken.None);
```

### ObservableAsync.CreateAsBackgroundJob

Runs the subscription logic as a background task.

```csharp
public static IObservableAsync<T> CreateAsBackgroundJob<T>(
    Func<IObserverAsync<T>, CancellationToken, ValueTask> job,
    bool startSynchronously = false)
```

**Example**:

```csharp
var background = ObservableAsync.CreateAsBackgroundJob<int>(async (observer, ct) =>
{
    for (int i = 0; i < 5; i++)
    {
        await observer.OnNextAsync(i, ct);
        await Task.Delay(100, ct);
    }
    await observer.OnCompletedAsync(Result.Success);
});
```

### ObservableAsync.Return

Emits a single value and completes.

```csharp
public static IObservableAsync<T> Return<T>(T value)
```

**Example**:

```csharp
var single = ObservableAsync.Return(42);
```

### ObservableAsync.Empty

Completes immediately without emitting.

```csharp
public static IObservableAsync<T> Empty<T>()
```

### ObservableAsync.Never

Never emits and never completes.

```csharp
public static IObservableAsync<T> Never<T>()
```

### ObservableAsync.Throw

Completes immediately with an error.

```csharp
public static IObservableAsync<T> Throw<T>(Exception exception)
```

### ObservableAsync.Range

Emits a sequence of integers.

```csharp
public static IObservableAsync<long> Range(long start, long count)
```

**Example**:

```csharp
var numbers = ObservableAsync.Range(1, 10); // 1, 2, 3, ..., 10
```

### ObservableAsync.Interval

Emits incrementing values at regular intervals.

```csharp
public static IObservableAsync<long> Interval(
    TimeSpan period,
    TimeProvider? timeProvider = null)
```

**Example**:

```csharp
var timer = ObservableAsync.Interval(TimeSpan.FromSeconds(1));
```

### ObservableAsync.Timer

Emits a value after a delay.

```csharp
public static IObservableAsync<long> Timer(
    TimeSpan dueTime,
    TimeProvider? timeProvider = null)
```

**Example**:

```csharp
var delayed = ObservableAsync.Timer(TimeSpan.FromSeconds(5));
```

### ObservableAsync.Defer

Defers observable creation until subscription.

```csharp
public static IObservableAsync<T> Defer<T>(
    Func<IObservableAsync<T>> factory)
```

**Example**:

```csharp
var deferred = ObservableAsync.Defer(() => 
    ObservableAsync.Return(DateTime.Now.Second));
```

### ObservableAsync.FromAsync

Wraps an async function as an observable.

```csharp
public static IObservableAsync<T> FromAsync<T>(
    Func<CancellationToken, Task<T>> factory)
```

**Example**:

```csharp
var fromTask = ObservableAsync.FromAsync(async ct =>
{
    await Task.Delay(100, ct);
    return 42;
});
```

### Conversion Extensions

```csharp
// Task to IObservableAsync
public static IObservableAsync<T> ToObservableAsync<T>(this Task<T> task)

// IAsyncEnumerable to IObservableAsync
public static IObservableAsync<T> ToObservableAsync<T>(
    this IAsyncEnumerable<T> asyncEnumerable)

// IEnumerable to IObservableAsync
public static IObservableAsync<T> ToObservableAsync<T>(
    this IEnumerable<T> enumerable)

// IObservable to IObservableAsync (Bridge)
public static IObservableAsync<T> ToObservableAsync<T>(
    this IObservable<T> observable)

// IObservableAsync to IObservable (Bridge)
public static IObservable<T> ToObservable<T>(
    this IObservableAsync<T> asyncObservable)
```

**Example**:

```csharp
// From Task
var task = Task.FromResult(42);
var obs = task.ToObservableAsync();

// From IAsyncEnumerable
async IAsyncEnumerable<int> GenerateAsync()
{
    for (int i = 0; i < 5; i++)
    {
        await Task.Delay(50);
        yield return i;
    }
}

var fromAsyncEnum = GenerateAsync().ToObservableAsync();

// Bridge from classic IObservable
var classic = Observable.Interval(TimeSpan.FromMilliseconds(200)).Take(5);
var asyncVersion = classic.ToObservableAsync();
```

---

## 6. Transformation Operators <a name="transformation-operators"></a>

### Select (Sync)

Projects each element using a synchronous function.

```csharp
public static IObservableAsync<TResult> Select<TSource, TResult>(
    this IObservableAsync<TSource> source,
    Func<TSource, TResult> selector)
```

**Example**:

```csharp
var source = ObservableAsync.Range(1, 5);
var doubled = source.Select(x => x * 2); // 2, 4, 6, 8, 10
```

### Select (Async)

Projects each element using an async function.

```csharp
public static IObservableAsync<TResult> Select<TSource, TResult>(
    this IObservableAsync<TSource> source,
    Func<TSource, CancellationToken, ValueTask<TResult>> asyncSelector)
```

**Example**:

```csharp
var projected = source.Select(async (x, ct) =>
{
    await Task.Delay(10, ct);
    return x.ToString();
});
```

### SelectMany

Flattens nested async observables.

```csharp
public static IObservableAsync<TResult> SelectMany<TSource, TResult>(
    this IObservableAsync<TSource> source,
    Func<TSource, IObservableAsync<TResult>> selector)
```

**Example**:

```csharp
var flattened = source.SelectMany(x => 
    ObservableAsync.Range(x * 10, 3));
// For x=1: 10, 11, 12
// For x=2: 20, 21, 22
// etc.
```

### Scan (Sync)

Applies an accumulator over the sequence.

```csharp
public static IObservableAsync<TAccumulate> Scan<TSource, TAccumulate>(
    this IObservableAsync<TSource> source,
    TAccumulate seed,
    Func<TAccumulate, TSource, TAccumulate> accumulator)
```

**Example**:

```csharp
var runningTotal = source.Scan(0, (acc, x) => acc + x);
// 1, 3, 6, 10, 15
```

### Scan (Async)

Async accumulator.

```csharp
public static IObservableAsync<TAccumulate> Scan<TSource, TAccumulate>(
    this IObservableAsync<TSource> source,
    TAccumulate seed,
    Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> asyncAccumulator)
```

### Cast

Casts each element to the specified type.

```csharp
public static IObservableAsync<TResult> Cast<TSource, TResult>(
    this IObservableAsync<TSource> source)
```

### OfType

Filters elements assignable to the specified type.

```csharp
public static IObservableAsync<TResult> OfType<TSource, TResult>(
    this IObservableAsync<TSource> source)
```

### GroupBy

Groups elements by key.

```csharp
public static IObservableAsync<GroupedAsyncObservable<TKey, TSource>> GroupBy<TSource, TKey>(
    this IObservableAsync<TSource> source,
    Func<TSource, TKey> keySelector)
```

**Example**:

```csharp
var grouped = source.GroupBy(x => x % 2 == 0 ? "even" : "odd");
```

---

## 7. Filtering Operators <a name="filtering-operators"></a>

### Where (Sync)

Filters elements using a synchronous predicate.

```csharp
public static IObservableAsync<T> Where<T>(
    this IObservableAsync<T> source,
    Func<T, bool> predicate)
```

**Example**:

```csharp
var evens = source.Where(x => x % 2 == 0);
```

### Where (Async)

Filters using an async predicate.

```csharp
public static IObservableAsync<T> Where<T>(
    this IObservableAsync<T> source,
    Func<T, CancellationToken, ValueTask<bool>> asyncPredicate)
```

**Example**:

```csharp
var asyncFiltered = source.Where(async (x, ct) =>
{
    await Task.Delay(1, ct);
    return x > 5;
});
```

### Take

Takes the first N elements.

```csharp
public static IObservableAsync<T> Take<T>(
    this IObservableAsync<T> source,
    int count)
```

### Skip

Skips the first N elements.

```csharp
public static IObservableAsync<T> Skip<T>(
    this IObservableAsync<T> source,
    int count)
```

### TakeWhile

Takes elements while predicate holds.

```csharp
public static IObservableAsync<T> TakeWhile<T>(
    this IObservableAsync<T> source,
    Func<T, bool> predicate)
```

### SkipWhile

Skips elements while predicate holds.

```csharp
public static IObservableAsync<T> SkipWhile<T>(
    this IObservableAsync<T> source,
    Func<T, bool> predicate)
```

### TakeUntil (Multiple Overloads)

Takes elements until a signal.

```csharp
// Until another observable
public static IObservableAsync<T> TakeUntil<T>(
    this IObservableAsync<T> source,
    IObservableAsync<Unit> other)

// Until a task
public static IObservableAsync<T> TakeUntil<T>(
    this IObservableAsync<T> source,
    Task other)

// Until cancellation token
public static IObservableAsync<T> TakeUntil<T>(
    this IObservableAsync<T> source,
    CancellationToken cancellationToken)

// Until predicate
public static IObservableAsync<T> TakeUntil<T>(
    this IObservableAsync<T> source,
    Func<T, bool> predicate)
```

**Example**:

```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var bounded = ObservableAsync.Interval(TimeSpan.FromMilliseconds(100))
    .TakeUntil(cts.Token);
```

### Distinct

Emits only unique elements.

```csharp
public static IObservableAsync<T> Distinct<T>(
    this IObservableAsync<T> source)
```

### DistinctBy

Emits only elements with unique keys.

```csharp
public static IObservableAsync<T> DistinctBy<T, TKey>(
    this IObservableAsync<T> source,
    Func<T, TKey> keySelector)
```

### DistinctUntilChanged

Suppresses consecutive duplicates.

```csharp
public static IObservableAsync<T> DistinctUntilChanged<T>(
    this IObservableAsync<T> source)
```

### DistinctUntilChangedBy

Suppresses consecutive elements with same key.

```csharp
public static IObservableAsync<T> DistinctUntilChangedBy<T, TKey>(
    this IObservableAsync<T> source,
    Func<T, TKey> keySelector)
```

**Example**:

```csharp
var items = new[] { 1, 2, 2, 3, 1, 3 }.ToObservableAsync();
var unique = items.Distinct(); // 1, 2, 3
var noConsecDups = items.DistinctUntilChanged(); // 1, 2, 3, 1, 3
```

---

## 8. Combining and Merging Operators <a name="combining-operators"></a>

### Merge

Merges multiple observables, interleaving values.

```csharp
// Two sources
public static IObservableAsync<T> Merge<T>(
    this IObservableAsync<T> first,
    IObservableAsync<T> second)

// Multiple sources with concurrency limit
public static IObservableAsync<T> Merge<T>(
    this IObservableAsync<IObservableAsync<T>> sources,
    int maxConcurrency)
```

**Example**:

```csharp
var a = ObservableAsync.Range(1, 3); // 1, 2, 3
var b = ObservableAsync.Range(10, 3); // 10, 11, 12
var merged = ObservableAsync.Merge(a, b); // Interleaved: 1, 10, 2, 11, 3, 12 (order may vary)
```

### Concat

Concatenates observables sequentially.

```csharp
public static IObservableAsync<T> Concat<T>(
    this IObservableAsync<T> first,
    IObservableAsync<T> second)
```

**Example**:

```csharp
var sequential = ObservableAsync.Concat(a, b); // 1, 2, 3, 10, 11, 12
```

### Switch

Switches to the most recent inner observable.

```csharp
public static IObservableAsync<T> Switch<T>(
    this IObservableAsync<IObservableAsync<T>> sources)
```

**Use Case**: When you want to cancel previous operations when a new one arrives (e.g., search as you type).

### CombineLatest

Combines latest values from multiple sources.

```csharp
// Two sources
public static IObservableAsync<TResult> CombineLatest<T1, T2, TResult>(
    this IObservableAsync<T1> first,
    IObservableAsync<T2> second,
    Func<T1, T2, TResult> selector)

// Up to 8 sources
public static IObservableAsync<TResult> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
    IObservableAsync<T1> first,
    IObservableAsync<T2> second,
    // ... up to 8 sources
    Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> selector)
```

**Example**:

```csharp
var combined = a.CombineLatest(b, (x, y) => $"{x}+{y}");
// Emits whenever either source emits, with latest values from both
```

### Zip

Pairs elements by index.

```csharp
public static IObservableAsync<TResult> Zip<T1, T2, TResult>(
    this IObservableAsync<T1> first,
    IObservableAsync<T2> second,
    Func<T1, T2, TResult> selector)
```

**Example**:

```csharp
var zipped = a.Zip(b, (x, y) => x + y); // 11, 13, 15
```

### Prepend

Prepends a single value.

```csharp
public static IObservableAsync<T> Prepend<T>(
    this IObservableAsync<T> source,
    T value)
```

### StartWith

Prepends multiple values.

```csharp
public static IObservableAsync<T> StartWith<T>(
    this IObservableAsync<T> source,
    params T[] values)
```

**Example**:

```csharp
var withPrefix = a.Prepend(0); // 0, 1, 2, 3
var withMany = a.StartWith(-2, -1, 0); // -2, -1, 0, 1, 2, 3
```

---

## 9. Error Handling and Resilience <a name="error-handling"></a>

### Catch

Catches errors and switches to fallback.

```csharp
public static IObservableAsync<T> Catch<T>(
    this IObservableAsync<T> source,
    Func<Exception, IObservableAsync<T>> handler)
```

**Example**:

```csharp
var flaky = ObservableAsync.Throw<int>(new InvalidOperationException("Oops"));
var safe = flaky.Catch(ex => ObservableAsync.Return(-1)); // emits -1
```

### CatchAndIgnoreErrorResume

Suppresses error-resume notifications.

```csharp
public static IObservableAsync<T> CatchAndIgnoreErrorResume<T>(
    this IObservableAsync<T> source)
```

### Retry (Infinite)

Re-subscribes indefinitely on error.

```csharp
public static IObservableAsync<T> Retry<T>(
    this IObservableAsync<T> source)
```

### Retry (Count-Limited)

Re-subscribes up to N times.

```csharp
public static IObservableAsync<T> Retry<T>(
    this IObservableAsync<T> source,
    int count)
```

**Example**:

```csharp
var retried = flaky.Retry(3); // Retry up to 3 times
```

### OnErrorResumeAsFailure

Converts error-resume to failure completion.

```csharp
public static IObservableAsync<T> OnErrorResumeAsFailure<T>(
    this IObservableAsync<T> source)
```

---

## 10. Timing and Scheduling <a name="timing-scheduling"></a>

### Throttle

Suppresses rapid emissions (debounce).

```csharp
public static IObservableAsync<T> Throttle<T>(
    this IObservableAsync<T> source,
    TimeSpan timeSpan,
    TimeProvider? timeProvider = null)
```

**Example**:

```csharp
var source = ObservableAsync.Interval(TimeSpan.FromMilliseconds(50)).Take(10);
var throttled = source.Throttle(TimeSpan.FromMilliseconds(200));
```

### Delay

Delays each emission.

```csharp
public static IObservableAsync<T> Delay<T>(
    this IObservableAsync<T> source,
    TimeSpan timeSpan,
    TimeProvider? timeProvider = null)
```

### Timeout

Raises TimeoutException if no value arrives.

```csharp
// With exception
public static IObservableAsync<T> Timeout<T>(
    this IObservableAsync<T> source,
    TimeSpan timeSpan)

// With fallback
public static IObservableAsync<T> Timeout<T>(
    this IObservableAsync<T> source,
    TimeSpan timeSpan,
    IObservableAsync<T> fallback)
```

**Example**:

```csharp
var guarded = source.Timeout(TimeSpan.FromSeconds(2));
var withFallback = source.Timeout(
    TimeSpan.FromSeconds(2),
    ObservableAsync.Return(999L));
```

### ObserveOn

Shifts notifications to specified context.

```csharp
public static IObservableAsync<T> ObserveOn<T>(
    this IObservableAsync<T> source,
    AsyncContext context)

public static IObservableAsync<T> ObserveOn<T>(
    this IObservableAsync<T> source,
    SynchronizationContext context)

public static IObservableAsync<T> ObserveOn<T>(
    this IObservableAsync<T> source,
    TaskScheduler scheduler)

public static IObservableAsync<T> ObserveOn<T>(
    this IObservableAsync<T> source,
    IScheduler scheduler)
```

**Example**:

```csharp
var onContext = source.ObserveOn(
    new AsyncContext(SynchronizationContext.Current!));
```

### Yield

Yields control between notifications.

```csharp
public static IObservableAsync<T> Yield<T>(
    this IObservableAsync<T> source)
```

---

## 11. Aggregation and Terminal Operators <a name="aggregation-operators"></a>

### AggregateAsync

Applies accumulator and returns final result.

```csharp
public static ValueTask<TAccumulate> AggregateAsync<TSource, TAccumulate>(
    this IObservableAsync<TSource> source,
    TAccumulate seed,
    Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> accumulator,
    CancellationToken cancellationToken)
```

**Example**:

```csharp
var source = ObservableAsync.Range(1, 5);
int sum = await source.AggregateAsync(0, (a, x) => a + x, CancellationToken.None); // 15
```

### CountAsync

Returns element count.

```csharp
public static ValueTask<int> CountAsync<T>(
    this IObservableAsync<T> source,
    CancellationToken cancellationToken)
```

### LongCountAsync

Returns element count as long.

```csharp
public static ValueTask<long> LongCountAsync<T>(
    this IObservableAsync<T> source,
    CancellationToken cancellationToken)
```

### AnyAsync

Returns true if any elements exist.

```csharp
public static ValueTask<bool> AnyAsync<T>(
    this IObservableAsync<T> source,
    CancellationToken cancellationToken)

public static ValueTask<bool> AnyAsync<T>(
    this IObservableAsync<T> source,
    Func<T, bool> predicate,
    CancellationToken cancellationToken)
```

### AllAsync

Returns true if all elements match predicate.

```csharp
public static ValueTask<bool> AllAsync<T>(
    this IObservableAsync<T> source,
    Func<T, bool> predicate,
    CancellationToken cancellationToken)
```

### ContainsAsync

Returns true if sequence contains value.

```csharp
public static ValueTask<bool> ContainsAsync<T>(
    this IObservableAsync<T> source,
    T value,
    CancellationToken cancellationToken)
```

### FirstAsync

Returns first element or throws.

```csharp
public static ValueTask<T> FirstAsync<T>(
    this IObservableAsync<T> source,
    CancellationToken cancellationToken)
```

### FirstOrDefaultAsync

Returns first element or default.

```csharp
public static ValueTask<T> FirstOrDefaultAsync<T>(
    this IObservableAsync<T> source,
    CancellationToken cancellationToken)
```

### LastAsync / LastOrDefaultAsync

Returns last element.

```csharp
public static ValueTask<T> LastAsync<T>(
    this IObservableAsync<T> source,
    CancellationToken cancellationToken)

public static ValueTask<T> LastOrDefaultAsync<T>(
    this IObservableAsync<T> source,
    CancellationToken cancellationToken)
```

### SingleAsync / SingleOrDefaultAsync

Returns single element or throws.

```csharp
public static ValueTask<T> SingleAsync<T>(
    this IObservableAsync<T> source,
    CancellationToken cancellationToken)

public static ValueTask<T> SingleOrDefaultAsync<T>(
    this IObservableAsync<T> source,
    CancellationToken cancellationToken)
```

### ToListAsync

Collects all elements into List.

```csharp
public static ValueTask<List<T>> ToListAsync<T>(
    this IObservableAsync<T> source,
    CancellationToken cancellationToken)
```

**Example**:

```csharp
List<int> all = await source.ToListAsync(CancellationToken.None); // [1, 2, 3, 4, 5]
```

### ToDictionaryAsync

Collects into Dictionary.

```csharp
public static ValueTask<Dictionary<TKey, T>> ToDictionaryAsync<TSource, TKey>(
    this IObservableAsync<TSource> source,
    Func<TSource, TKey> keySelector,
    CancellationToken cancellationToken)
```

### ForEachAsync

Invokes async action for each element.

```csharp
public static ValueTask ForEachAsync<T>(
    this IObservableAsync<T> source,
    Func<T, CancellationToken, ValueTask> action,
    CancellationToken cancellationToken)
```

**Example**:

```csharp
await source.ForEachAsync(async (x, ct) =>
    Console.WriteLine($"Item: {x}"), CancellationToken.None);
```

### WaitCompletionAsync

Awaits completion without capturing values.

```csharp
public static ValueTask WaitCompletionAsync<T>(
    this IObservableAsync<T> source,
    CancellationToken cancellationToken)
```

---

## 12. Async Disposables and Resource Management <a name="async-disposables"></a>

### DisposableAsync

Provides async disposable utilities.

```csharp
public static class DisposableAsync
{
    public static IAsyncDisposable Empty { get; }
    
    public static IAsyncDisposable Create(Func<ValueTask> disposeAsync);
}
```

### CompositeDisposableAsync

Manages multiple async disposables.

```csharp
public sealed class CompositeDisposableAsync : IAsyncDisposable
{
    public CompositeDisposableAsync();
    public CompositeDisposableAsync(int capacity);
    public CompositeDisposableAsync(params IAsyncDisposable[] disposables);
    public CompositeDisposableAsync(IEnumerable<IAsyncDisposable> disposables);
    
    public bool IsDisposed { get; }
    public int Count { get; }
    
    public ValueTask AddAsync(IAsyncDisposable item);
    public ValueTask<bool> RemoveAsync(IAsyncDisposable item);
    public ValueTask ClearAsync();
    public bool Contains(IAsyncDisposable item);
    public ValueTask DisposeAsync();
}
```

**Example**:

```csharp
var composite = new CompositeDisposableAsync();
await composite.AddAsync(someResource1);
await composite.AddAsync(someResource2);

// All resources disposed together
await composite.DisposeAsync();
```

### SerialDisposableAsync

Manages a single async disposable that can be replaced.

```csharp
public class SerialDisposableAsync : IAsyncDisposable
{
    public ValueTask SetDisposableAsync(IAsyncDisposable? value);
    public ValueTask DisposeAsync();
}
```

**Use Case**: When you need to replace a resource and automatically dispose the old one.

**Example**:

```csharp
var serial = new SerialDisposableAsync();

// Set initial resource
await serial.SetDisposableAsync(resource1);

// Replace - resource1 is automatically disposed
await serial.SetDisposableAsync(resource2);

// Dispose - resource2 is disposed
await serial.DisposeAsync();
```

### SingleAssignmentDisposableAsync

Allows single assignment of async disposable.

```csharp
public sealed class SingleAssignmentDisposableAsync : IAsyncDisposable
{
    public bool IsDisposed { get; }
    public IAsyncDisposable? GetDisposable();
    public ValueTask SetDisposableAsync(IAsyncDisposable? value);
    public ValueTask DisposeAsync();
}
```

**Use Case**: When a resource must be assigned exactly once.

---

## 13. Subjects and Multicasting <a name="subjects-multicasting"></a>

### SubjectAsync

Async subject for multicasting.

```csharp
public static class SubjectAsync
{
    public static ISubjectAsync<T> Create<T>();
    public static ISubjectAsync<T> CreateBehavior<T>(T initialValue);
    public static ISubjectAsync<T> CreateReplayLatest<T>();
}
```

### ISubjectAsync<T>

```csharp
public interface ISubjectAsync<T> : IObservableAsync<T>, IObserverAsync<T>
{
    IObservableAsync<T> Values { get; }
}
```

### ConcurrentSubjectAsync

Forwards notifications to observers concurrently.

```csharp
public sealed class ConcurrentSubjectAsync<T> : BaseSubjectAsync<T>
{
    // Observers notified in parallel for high throughput
}
```

### ConcurrentReplayLatestSubjectAsync

Replays latest value to new subscribers with concurrent notification.

### ConcurrentStatelessSubjectAsync

Stateless subject with concurrent notification.

### ConcurrentStatelessReplayLatestSubjectAsync

Stateless replay-latest subject with concurrent notification.

### Multicasting Operators

```csharp
// Publish with serial subject
public static ConnectableObservableAsync<T> Publish<T>(
    this IObservableAsync<T> source)

// Publish with stateless subject
public static ConnectableObservableAsync<T> StatelessPublish<T>(
    this IObservableAsync<T> source)

// Replay latest value
public static ConnectableObservableAsync<T> ReplayLatest<T>(
    this IObservableAsync<T> source)

// Auto-connect on first subscriber
public static IObservableAsync<T> RefCount<T>(
    this ConnectableObservableAsync<T> source)

// Custom subject factory
public static ConnectableObservableAsync<T> Multicast<T, TSubject>(
    this IObservableAsync<T> source,
    Func<TSubject> subjectFactory)
    where TSubject : ISubjectAsync<T>
```

**Example**:

```csharp
var source = ObservableAsync.Interval(TimeSpan.FromMilliseconds(100)).Take(5);

// Publish + explicit connect
var published = source.Publish();
await using var sub1 = await published.SubscribeAsync(
    async (v, ct) => Console.WriteLine($"Sub1: {v}"), CancellationToken.None);
await using var sub2 = await published.SubscribeAsync(
    async (v, ct) => Console.WriteLine($"Sub2: {v}"), CancellationToken.None);
await using var connection = await published.ConnectAsync(CancellationToken.None);

// RefCount: auto-connect/disconnect
var shared = source.Publish().RefCount();

// ReplayLatest: new subscribers get most recent value
var replayed = source.ReplayLatest().RefCount();
```

---

## 14. Bridging Classic and Async Observables <a name="bridging"></a>

### ObservableBridgeExtensions

Provides bidirectional conversion.

```csharp
// IObservable to IObservableAsync
public static IObservableAsync<T> ToObservableAsync<T>(
    this IObservable<T> observable)

// IObservableAsync to IObservable
public static IObservable<T> ToObservable<T>(
    this IObservableAsync<T> asyncObservable)

// IAsyncEnumerable to IObservableAsync
public static IObservableAsync<T> ToObservableAsync<T>(
    this IAsyncEnumerable<T> asyncEnumerable)

// IObservableAsync to IAsyncEnumerable
public static IAsyncEnumerable<T> ToAsyncEnumerable<T>(
    this IObservableAsync<T> asyncObservable)
```

**Example**:

```csharp
// Bridge from classic IObservable
var classic = Observable.Interval(TimeSpan.FromMilliseconds(200)).Take(5);
var asyncVersion = classic.ToObservableAsync();

// Bridge back to classic
var backToClassic = asyncVersion.ToObservable();

// From IAsyncEnumerable
async IAsyncEnumerable<int> GenerateAsync()
{
    for (int i = 0; i < 5; i++)
    {
        await Task.Delay(50);
        yield return i;
    }
}

var fromAsyncEnum = GenerateAsync().ToObservableAsync();
```

---

## 15. Classic Reactive Extensions Operators <a name="classic-operators"></a>

ReactiveUI.Extensions also provides valuable operators for traditional `IObservable<T>` that don't ship with System.Reactive.

### Null & Signal Helpers

```csharp
// Filter nulls
public static IObservable<T> WhereIsNotNull<T>(
    this IObservable<T?> source)
    where T : class

// Convert to Unit signal
public static IObservable<Unit> AsSignal<T>(
    this IObservable<T> source)
```

### Timing & Scheduling

```csharp
// Shared timer (one underlying timer per TimeSpan)
public static IObservable<long> SyncTimer(TimeSpan period)

// Schedule single value
public static IObservable<T> Schedule<T>(
    this T value, 
    TimeSpan dueTime, 
    IScheduler scheduler)

// Safe scheduling (handles null scheduler)
public static IDisposable ScheduleSafe(
    this IScheduler? scheduler, 
    Action action)

// ThrottleFirst: allow first item per window
public static IObservable<T> ThrottleFirst<T>(
    this IObservable<T> source, 
    TimeSpan window, 
    IScheduler? scheduler = null)

// ThrottleDistinct: throttle but only emit on change
public static IObservable<T> ThrottleDistinct<T>(
    this IObservable<T> source, 
    TimeSpan throttle, 
    IScheduler? scheduler = null)

// DebounceImmediate: emit first immediately then debounce
public static IObservable<T> DebounceImmediate<T>(
    this IObservable<T> source, 
    TimeSpan dueTime, 
    IScheduler? scheduler = null)
```

### Inactivity / Liveness

```csharp
// Heartbeat during quiet periods
public static IObservable<IHeartbeat<T>> Heartbeat<T>(
    this IObservable<T> source, 
    TimeSpan interval, 
    IScheduler? scheduler = null)

// Detect stale data
public static IObservable<IStale<T>> DetectStale<T>(
    this IObservable<T> source, 
    TimeSpan staleThreshold, 
    IScheduler? scheduler = null)

// Buffer until inactive
public static IObservable<IList<T>> BufferUntilInactive<T>(
    this IObservable<T> source, 
    TimeSpan inactivityPeriod, 
    IScheduler? scheduler = null)
```

### Error Handling

```csharp
// Ignore all errors
public static IObservable<T> CatchIgnore<T>(
    this IObservable<T> source)

// Return fallback on error
public static IObservable<T> CatchAndReturn<T>(
    this IObservable<T> source, 
    T fallback)

// Retry with error handler
public static IObservable<T> OnErrorRetry<T, TException>(
    this IObservable<T> source, 
    Action<TException> onError, 
    int retryCount = int.MaxValue, 
    TimeSpan delay = default, 
    IScheduler? delayScheduler = null)
    where TException : Exception

// Retry with exponential backoff
public static IObservable<T> RetryWithBackoff<T>(
    this IObservable<T> source, 
    int maxRetries, 
    TimeSpan initialDelay, 
    double backoffFactor = 2.0, 
    TimeSpan? maxDelay = null, 
    IScheduler? scheduler = null)
```

### Combining & Aggregation

```csharp
// All values are true
public static IObservable<bool> CombineLatestValuesAreAllTrue(
    this IEnumerable<IObservable<bool>> sources)

// All values are false
public static IObservable<bool> CombineLatestValuesAreAllFalse(
    this IEnumerable<IObservable<bool>> sources)

// Get max value
public static IObservable<T> GetMax<T>(
    this IObservable<T> source)

// Get min value
public static IObservable<T> GetMin<T>(
    this IObservable<T> source)

// Partition into two streams
public static (IObservable<T> True, IObservable<T> False) Partition<T>(
    this IObservable<T> source, 
    Func<T, bool> predicate)
```

### Logical / Boolean

```csharp
// Boolean negation
public static IObservable<bool> Not(
    this IObservable<bool> source)

// Filter true values
public static IObservable<bool> WhereTrue(
    this IObservable<bool> source)

// Filter false values
public static IObservable<bool> WhereFalse(
    this IObservable<bool> source)
```

### Async / Task Integration

```csharp
// Sequential async projection
public static IObservable<TResult> SelectAsyncSequential<TSource, TResult>(
    this IObservable<TSource> source, 
    Func<TSource, Task<TResult>> selector)

// Latest only (cancels previous)
public static IObservable<TResult> SelectLatestAsync<TSource, TResult>(
    this IObservable<TSource> source, 
    Func<TSource, Task<TResult>> selector)

// Limited parallelism
public static IObservable<TResult> SelectAsyncConcurrent<TSource, TResult>(
    this IObservable<TSource> source, 
    Func<TSource, Task<TResult>> selector, 
    int maxConcurrency)

// Async subscription
public static IDisposable SubscribeAsync<T>(
    this IObservable<T> source, 
    Func<T, Task> onNext)

// Synchronous gate
public static IDisposable SubscribeSynchronous<T>(
    this IObservable<T> source, 
    Func<T, Task> onNext)

// Convert to hot Task
public static Task<T> ToHotTask<T>(
    this IObservable<T> source)
```

### Backpressure

```csharp
// Conflate bursty updates
public static IObservable<T> Conflate<T>(
    this IObservable<T> source, 
    TimeSpan minimumPeriod, 
    IScheduler? scheduler = null)
```

### Filtering / Conditional

```csharp
// Filter strings by regex
public static IObservable<string> Filter(
    this IObservable<string> source, 
    string regexPattern)

// Take until predicate (inclusive)
public static IObservable<T> TakeUntil<T>(
    this IObservable<T> source, 
    Func<T, bool> predicate)

// Wait for first match
public static IObservable<T> WaitUntil<T>(
    this IObservable<T> source, 
    Func<T, bool> predicate)

// Sample latest on trigger
public static IObservable<T> SampleLatest<T>(
    this IObservable<T> source, 
    IObservable<Unit> trigger)

// Fallback if empty
public static IObservable<T> SwitchIfEmpty<T>(
    this IObservable<T> source, 
    IObservable<T> fallback)

// Drop if busy
public static IObservable<T> DropIfBusy<T>(
    this IObservable<T> source, 
    Func<T, Task> asyncAction)
```

### Buffering & Transformation

```csharp
// Buffer until delimiter
public static IObservable<IList<T>> BufferUntil<T>(
    this IObservable<T> source, 
    T startDelimiter, 
    T endDelimiter)

// Buffer until idle
public static IObservable<IList<T>> BufferUntilIdle<T>(
    this IObservable<T> source, 
    TimeSpan idlePeriod)

// Emit consecutive pairs
public static IObservable<(T Previous, T Current)> Pairwise<T>(
    this IObservable<T> source)

// Scan with initial value
public static IObservable<TAccumulate> ScanWithInitial<TSource, TAccumulate>(
    this IObservable<TSource> source, 
    TAccumulate initial, 
    Func<TAccumulate, TSource, TAccumulate> accumulator)

// Shuffle arrays in-place
public static IObservable<T[]> Shuffle<T>(
    this IObservable<T[]> source)
```

### Subscription / Side Effects

```csharp
// Action on subscribe
public static IObservable<T> DoOnSubscribe<T>(
    this IObservable<T> source, 
    Action action)

// Action on dispose
public static IObservable<T> DoOnDispose<T>(
    this IObservable<T> source, 
    Action disposeAction)
```

### Utility & Miscellaneous

```csharp
// ForEach with low allocations
public static IObservable<T> ForEach<T>(
    this IObservable<IEnumerable<T>> source)

// Using helper
public static IObservable<TResult> Using<TDisposable, TResult>(
    this TDisposable obj, 
    Func<TDisposable, TResult> function, 
    IScheduler? scheduler = null)
    where TDisposable : IDisposable

// While loop
public static IObservable<Unit> While(
    Func<bool> condition, 
    Action action, 
    IScheduler? scheduler = null)

// OnNext with params
public static void OnNext<T>(
    this IObserver<T> observer, 
    params T[] values)

// Read-only BehaviorSubject
public static (IObservable<T> Observable, IObserver<T> Observer) ToReadOnlyBehavior<T>(
    T initialValue)

// Property change observable
public static IObservable<TProperty> ToPropertyObservable<TSource, TProperty>(
    this TSource source, 
    Expression<Func<TSource, TProperty>> propertyExpression)
    where TSource : INotifyPropertyChanged
```

---

## 16. Advanced Multi-Threading Examples <a name="advanced-examples"></a>

### Example 1: Parallel Data Processing Pipeline

```csharp
using ReactiveUI.Extensions.Async;
using ReactiveUI.Extensions.Async.Subjects;

public class ParallelDataProcessor
{
    private readonly int _maxConcurrency;
    
    public ParallelDataProcessor(int maxConcurrency = 4)
    {
        _maxConcurrency = maxConcurrency;
    }
    
    public async Task ProcessDataStreamAsync(
        IAsyncEnumerable<string> input,
        Func<string, CancellationToken, Task<string>> processor,
        CancellationToken cancellationToken)
    {
        // Convert to async observable
        var observable = input.ToObservableAsync();
        
        // Process with limited concurrency
        var processed = observable
            .SelectMany(async (item, ct) =>
            {
                try
                {
                    return await processor(item, ct);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {item}: {ex.Message}");
                    return null;
                }
            })
            .Where(x => x != null);
        
        // Subscribe and process results
        await processed.ForEachAsync(async (result, ct) =>
        {
            await SaveResultAsync(result!, ct);
        }, cancellationToken);
    }
    
    private async Task SaveResultAsync(string result, CancellationToken ct)
    {
        // Simulate async I/O
        await Task.Delay(10, ct);
        Console.WriteLine($"Saved: {result}");
    }
}

// Usage
var processor = new ParallelDataProcessor(maxConcurrency: 4);
var data = GenerateDataAsync(); // IAsyncEnumerable<string>

await processor.ProcessDataStreamAsync(
    data,
    async (item, ct) =>
    {
        await Task.Delay(100, ct); // Simulate processing
        return item.ToUpperInvariant();
    },
    CancellationToken.None);
```

### Example 2: Real-Time Data Aggregation with Multiple Sources

```csharp
public class RealTimeAggregator
{
    public async Task AggregateMultipleSourcesAsync(
        CancellationToken cancellationToken)
    {
        // Create multiple data sources
        var source1 = ObservableAsync.Interval(TimeSpan.FromMilliseconds(100))
            .Take(50)
            .Select(x => $"Source1-{x}");
        
        var source2 = ObservableAsync.Interval(TimeSpan.FromMilliseconds(150))
            .Take(50)
            .Select(x => $"Source2-{x}");
        
        var source3 = ObservableAsync.Interval(TimeSpan.FromMilliseconds(200))
            .Take(50)
            .Select(x => $"Source3-{x}");
        
        // Merge all sources with concurrency limit
        var merged = ObservableAsync.Merge(
            new[] { source1, source2, source3 },
            maxConcurrency: 3);
        
        // Buffer and process in batches
        var batches = merged
            .Buffer(TimeSpan.FromMilliseconds(500))
            .Where(batch => batch.Count > 0);
        
        // Process batches in parallel
        await batches.ForEachAsync(async (batch, ct) =>
        {
            Console.WriteLine($"Processing batch of {batch.Count} items");
            
            // Process batch items in parallel
            var tasks = batch.Select(async item =>
            {
                await Task.Delay(10, ct);
                return item.ToUpperInvariant();
            });
            
            var results = await Task.WhenAll(tasks);
            
            foreach (var result in results)
            {
                Console.WriteLine($"  {result}");
            }
        }, cancellationToken);
    }
}
```

### Example 3: Cancellation-Cooperative Long-Running Operation

```csharp
public class CancellableDataFetcher
{
    public async Task FetchDataWithCancellationAsync(
        string url,
        CancellationToken cancellationToken)
    {
        var fetchObservable = ObservableAsync.Create<string>(async (observer, ct) =>
        {
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct, cancellationToken);
            
            try
            {
                for (int i = 0; i < 100 && !linkedCts.Token.IsCancellationRequested; i++)
                {
                    // Simulate async HTTP request
                    var data = await FetchPageAsync(url, i, linkedCts.Token);
                    await observer.OnNextAsync(data, linkedCts.Token);
                    
                    await Task.Delay(100, linkedCts.Token);
                }
                
                await observer.OnCompletedAsync(Result.Success);
            }
            catch (OperationCanceledException)
            {
                await observer.OnCompletedAsync(Result.Success);
            }
            catch (Exception ex)
            {
                await observer.OnErrorResumeAsync(ex, linkedCts.Token);
            }
            finally
            {
                linkedCts.Dispose();
            }
            
            return DisposableAsync.Empty;
        });
        
        // Subscribe with cancellation
        await using var subscription = await fetchObservable.SubscribeAsync(
            async (data, ct) =>
            {
                await ProcessDataAsync(data, ct);
            },
            cancellationToken);
    }
    
    private async Task<string> FetchPageAsync(string url, int page, CancellationToken ct)
    {
        await Task.Delay(50, ct); // Simulate network
        return $"Page {page} data";
    }
    
    private async Task ProcessDataAsync(string data, CancellationToken ct)
    {
        await Task.Delay(10, ct); // Simulate processing
        Console.WriteLine($"Processed: {data}");
    }
}
```

### Example 4: Thread-Safe State Management with Async Observables

```csharp
public class ThreadSafeStateManager<T>
{
    private readonly ConcurrentSubjectAsync<T> _subject = new();
    private T? _currentState;
    private readonly SemaphoreSlim _lock = new(1, 1);
    
    public IObservableAsync<T> State => _subject;
    
    public async ValueTask UpdateStateAsync(T newState, CancellationToken cancellationToken)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            _currentState = newState;
            await _subject.OnNextAsync(newState, cancellationToken);
        }
        finally
        {
            _lock.Release();
        }
    }
    
    public async ValueTask<T> GetStateAsync(CancellationToken cancellationToken)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            return _currentState!;
        }
        finally
        {
            _lock.Release();
        }
    }
    
    public async ValueTask DisposeAsync()
    {
        await _lock.WaitAsync();
        try
        {
            await _subject.OnCompletedAsync(Result.Success);
            await _subject.DisposeAsync();
        }
        finally
        {
            _lock.Release();
            _lock.Dispose();
        }
    }
}

// Usage
var stateManager = new ThreadSafeStateManager<int>();

// Subscribe from multiple threads
var tasks = Enumerable.Range(0, 5).Select(async i =>
{
    await using var sub = await stateManager.State.SubscribeAsync(
        async (state, ct) =>
        {
            Console.WriteLine($"Thread {i}: State = {state}");
        },
        CancellationToken.None);
    
    await Task.Delay(1000);
});

await Task.WhenAll(tasks);

// Update state
await stateManager.UpdateStateAsync(42, CancellationToken.None);
```

### Example 5: Backpressure Handling with Conflation

```csharp
public class BackpressureHandler
{
    public async Task HandleHighFrequencyDataAsync(
        CancellationToken cancellationToken)
    {
        // High-frequency source (1000 events/second)
        var highFrequency = ObservableAsync.Interval(TimeSpan.FromMilliseconds(1))
            .Take(10000);
        
        // Conflate to 100 events/second (keep latest)
        var conflated = highFrequency
            .Publish(shared => shared
                .Throttle(TimeSpan.FromMilliseconds(10))
                .Merge(shared.TakeLast(1)));
        
        // Process at manageable rate
        await conflated.ForEachAsync(async (value, ct) =>
        {
            await Task.Delay(5, ct); // Simulate processing
            Console.WriteLine($"Processed: {value}");
        }, cancellationToken);
    }
}
```

---

## 17. Performance Considerations <a name="performance"></a>

### ValueTask vs Task

ReactiveUI.Extensions uses `ValueTask` throughout for better performance:

```csharp
// ValueTask avoids heap allocation for synchronous completions
public ValueTask OnNextAsync(T value, CancellationToken cancellationToken)
```

**Benefits**:
- No heap allocation when operation completes synchronously
- Reduced GC pressure
- Better performance for hot paths

### Allocation-Aware Operators

Many operators are designed to minimize allocations:

```csharp
// ForEach with low allocations
public static IObservable<T> ForEach<T>(
    this IObservable<IEnumerable<T>> source)
```

### Threading and Concurrency

**Best Practices**:

1. **Use ConcurrentSubjectAsync for high throughput**:
```csharp
var subject = new ConcurrentSubjectAsync<string>();
// Observers notified in parallel
```

2. **Limit concurrency with Merge**:
```csharp
var merged = sources.Merge(maxConcurrency: 4);
```

3. **Use ObserveOn for context switching**:
```csharp
var onUiThread = source.ObserveOn(SynchronizationContext.Current!);
```

### Memory Management

**Dispose Properly**:

```csharp
// Always use await using for async disposables
await using var subscription = await observable.SubscribeAsync(...);

// Or use CompositeDisposableAsync for multiple resources
var composite = new CompositeDisposableAsync();
await composite.AddAsync(subscription1);
await composite.AddAsync(subscription2);
await composite.DisposeAsync();
```

---

## 18. Best Practices <a name="best-practices"></a>

### 1. Always Pass CancellationToken

```csharp
// GOOD
await observable.ForEachAsync(async (item, ct) =>
{
    await ProcessAsync(item, ct);
}, cancellationToken);

// BAD - No cancellation support
await observable.ForEachAsync(async item =>
{
    await ProcessAsync(item, CancellationToken.None);
});
```

### 2. Use Async Disposables Properly

```csharp
// GOOD
await using var subscription = await observable.SubscribeAsync(...);

// BAD - Synchronous disposal of async resources
using (await observable.SubscribeAsync(...)) { }
```

### 3. Handle Errors in OnErrorResumeAsync

```csharp
public class ResilientObserver : ObserverAsync<T>
{
    protected override async ValueTask OnErrorResumeAsyncCore(
        Exception error, 
        CancellationToken cancellationToken)
    {
        // Log error
        await LogErrorAsync(error, cancellationToken);
        
        // Decide whether to continue or stop
        // Don't rethrow unless you want to terminate
    }
}
```

### 4. Use Appropriate Subjects

```csharp
// For simple multicasting
var subject = SubjectAsync.Create<T>();

// For high-throughput scenarios
var subject = new ConcurrentSubjectAsync<T>();

// For replaying latest value
var subject = SubjectAsync.CreateReplayLatest<T>();
```

### 5. Limit Concurrency

```csharp
// GOOD - Limited concurrency
var processed = source.SelectMany(
    async item => await ProcessAsync(item),
    maxConcurrency: 4);

// BAD - Unlimited concurrency
var processed = source.SelectMany(
    async item => await ProcessAsync(item));
```

### 6. Use ObserveOn for UI Updates

```csharp
// Ensure UI updates happen on UI thread
var uiUpdates = source.ObserveOn(SynchronizationContext.Current!)
    .SubscribeAsync(async (value, ct) =>
    {
        UpdateUI(value);
    });
```

### 7. Bridge Carefully Between Classic and Async

```csharp
// Convert classic to async when needed
var asyncObservable = classicObservable.ToObservableAsync();

// Convert back when interoperating with Rx.NET libraries
var classic = asyncObservable.ToObservable();
```

### 8. Test with TestScheduler

```csharp
// Use TestScheduler for deterministic testing
var testScheduler = new TestScheduler();
var source = ObservableAsync.Interval(TimeSpan.FromSeconds(1), testScheduler);

// Advance time deterministically
testScheduler.AdvanceBy(TimeSpan.FromSeconds(5).Ticks);
```

---

## Conclusion

ReactiveUI.Extensions represents a significant advancement in reactive programming for .NET, providing a fully async-native framework that addresses the limitations of traditional Rx.NET in modern async/await scenarios. By embracing `ValueTask`, `CancellationToken`, and `IAsyncDisposable` throughout the entire pipeline, it enables:

- **True end-to-end async** without thread blocking
- **Proper cancellation support** at every level
- **Async resource cleanup** for I/O-bound operations
- **Seamless interop** with both classic Rx.NET and modern async patterns
- **High performance** through allocation-aware design

The library's comprehensive operator set, combined with advanced features like concurrent subjects, async disposables, and bidirectional bridging, makes it an essential tool for building robust, scalable reactive applications in .NET 8 and beyond.

Whether you're building real-time data processing pipelines, handling high-frequency event streams, or simply need better async integration in your reactive code, ReactiveUI.Extensions provides the tools and patterns to succeed.

---

## Additional Resources

- **GitHub Repository**: https://github.com/reactiveui/Extensions
- **System.Reactive Documentation**: https://github.com/dotnet/reactive
- **ReactiveUI Documentation**: https://www.reactiveui.net/docs/
- **Introduction to Rx.NET eBook**: Free 2nd Edition available

---

*This article covers all public functions within the ReactiveUI.Extensions library as of version 2.2.x. For the most up-to-date information, please refer to the official GitHub repository.*
