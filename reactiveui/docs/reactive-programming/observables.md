# Observables Introduction

## What are Observables?

An **Observable** is a stream of data that can emit multiple values over time. Think of it as a promise that can return multiple values instead of just one. Observables are the foundation of reactive programming and ReactiveUI.

## Key Concepts

### Observable Streams

An observable stream represents a sequence of values that arrive over time:

```csharp
// A stream of button clicks
button.Events().Click
    .Subscribe(click => Console.WriteLine("Button clicked!"));

// A stream of text changes
textBox.Events().TextChanged
    .Select(e => e.EventArgs.Text)
    .Subscribe(text => Console.WriteLine($"Text: {text}"));
```

### Push vs Pull

Traditional collections are **pull-based** - you request data when you need it:

```csharp
// Pull-based (traditional)
var items = GetItems();
foreach (var item in items)
{
    Process(item);
}
```

Observables are **push-based** - data is pushed to you when available:

```csharp
// Push-based (reactive)
GetItemsAsync()
    .Subscribe(item => Process(item));
```

## Creating Observables

### From Events

```csharp
using ReactiveMarbles.ObservableEvents;

// Convert events to observables
var clicks = button.Events().Click;
var textChanges = textBox.Events().TextChanged;
```

### From Property Changes

```csharp
// Watch a property
this.WhenAnyValue(x => x.SearchText)
    .Subscribe(text => Console.WriteLine($"Search: {text}"));

// Watch multiple properties
this.WhenAnyValue(
        x => x.FirstName,
        x => x.LastName,
        (first, last) => $"{first} {last}")
    .Subscribe(fullName => Console.WriteLine(fullName));
```

### From Tasks

```csharp
// Convert async operations to observables
Observable.FromAsync(() => LoadDataAsync())
    .Subscribe(data => DisplayData(data));
```

### From Collections

```csharp
// Create from enumerable
Observable.Range(1, 10)
    .Subscribe(n => Console.WriteLine(n));

// Create from values
Observable.Return(42)
    .Subscribe(value => Console.WriteLine(value));

// Create from multiple values
Observable.Create<int>(observer =>
{
    observer.OnNext(1);
    observer.OnNext(2);
    observer.OnNext(3);
    observer.OnCompleted();
    return Disposable.Empty;
});
```

## Observable Lifecycle

### OnNext

Emits a value in the stream:

```csharp
observable.Subscribe(
    value => Console.WriteLine($"Next: {value}"));
```

### OnError

Signals an error has occurred:

```csharp
observable.Subscribe(
    value => Console.WriteLine($"Next: {value}"),
    error => Console.WriteLine($"Error: {error.Message}"));
```

### OnCompleted

Signals the stream has finished:

```csharp
observable.Subscribe(
    value => Console.WriteLine($"Next: {value}"),
    error => Console.WriteLine($"Error: {error.Message}"),
    () => Console.WriteLine("Completed"));
```

## Hot vs Cold Observables

### Cold Observables

Start producing values when subscribed to:

```csharp
// Each subscription gets its own sequence
var cold = Observable.Range(1, 5);

cold.Subscribe(x => Console.WriteLine($"Sub1: {x}"));
cold.Subscribe(x => Console.WriteLine($"Sub2: {x}"));

// Output:
// Sub1: 1, Sub1: 2, Sub1: 3, Sub1: 4, Sub1: 5
// Sub2: 1, Sub2: 2, Sub2: 3, Sub2: 4, Sub2: 5
```

### Hot Observables

Share a single execution among all subscribers:

```csharp
// All subscribers share the same sequence
var hot = Observable.Interval(TimeSpan.FromSeconds(1))
    .Publish();

hot.Subscribe(x => Console.WriteLine($"Sub1: {x}"));
Thread.Sleep(2000);
hot.Subscribe(x => Console.WriteLine($"Sub2: {x}"));

hot.Connect(); // Start emitting
```

## Subscription and Disposal

### Basic Subscription

```csharp
var subscription = observable.Subscribe(
    value => Console.WriteLine(value));

// Cleanup
subscription.Dispose();
```

### Using WhenActivated

In ReactiveUI, always use `WhenActivated` for automatic disposal:

```csharp
this.WhenActivated(disposables =>
{
    this.WhenAnyValue(x => x.SearchText)
        .Subscribe(text => UpdateResults(text))
        .DisposeWith(disposables);
});
```

## Common Patterns

### Throttling User Input

```csharp
searchBox.Events().TextChanged
    .Throttle(TimeSpan.FromMilliseconds(300))
    .Select(e => e.EventArgs.Text)
    .DistinctUntilChanged()
    .Subscribe(text => PerformSearch(text));
```

### Combining Streams

```csharp
var firstName = this.WhenAnyValue(x => x.FirstName);
var lastName = this.WhenAnyValue(x => x.LastName);

firstName.CombineLatest(lastName, (f, l) => $"{f} {l}")
    .Subscribe(fullName => DisplayName = fullName);
```

### Handling Errors

```csharp
observable
    .Catch<int, Exception>(ex => Observable.Return(-1))
    .Subscribe(value => Console.WriteLine(value));
```

## Best Practices

1. **Always Dispose**: Prevent memory leaks by disposing subscriptions
2. **Use WhenActivated**: Let ReactiveUI manage lifecycle
3. **Avoid Blocking**: Don't use `.Wait()` or `.Result` on observables
4. **Handle Errors**: Always provide error handlers
5. **Be Mindful of Threads**: Use schedulers for thread management

## Schedulers

Control where work happens:

```csharp
// Background work
Observable.Start(() => ExpensiveOperation(), RxSchedulers.TaskpoolScheduler)
    .ObserveOn(RxSchedulers.MainThreadScheduler)
    .Subscribe(result => UpdateUI(result));
```

## Testing Observables

```csharp
[Fact]
public void TestObservableSequence()
{
    var scheduler = new TestScheduler();
    
    var source = scheduler.CreateHotObservable(
        OnNext(100, 1),
        OnNext(200, 2),
        OnNext(300, 3),
        OnCompleted<int>(400));
    
    var results = scheduler.Start(() => source, 0, 0, 500);
    
    results.Messages.AssertEqual(
        OnNext(100, 1),
        OnNext(200, 2),
        OnNext(300, 3),
        OnCompleted<int>(400));
}
```

## Resources

- [ReactiveX Documentation](http://reactivex.io/)
- [Intro to Rx](http://introtorx.com/)
- [ReactiveUI Documentation](~/docs/index.md)
- [Operators Guide](~/docs/reactive-programming/operators.md)

## Related Topics

- [Operators](~/docs/reactive-programming/operators.md)
- [Error Handling](~/docs/reactive-programming/error-handling.md)
- [Testing](~/docs/reactive-programming/testing.md)
- [Schedulers](~/docs/reactive-programming/schedulers.md)
