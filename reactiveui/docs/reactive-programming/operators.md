# Reactive Operators

## Overview

Reactive operators are methods that transform, filter, combine, and manipulate observable streams. They are the building blocks of reactive programming and allow you to compose complex asynchronous operations declaratively.

## Transformation Operators

### Select (Map)

Transforms each element in the stream:

```csharp
// Transform numbers to strings
Observable.Range(1, 5)
    .Select(x => $"Number: {x}")
    .Subscribe(s => Console.WriteLine(s));

// Transform events
textBox.Events().TextChanged
    .Select(e => e.EventArgs.Text)
    .Select(text => text.ToUpper())
    .Subscribe(upper => label.Text = upper);
```

### SelectMany (FlatMap)

Projects each element to an observable and flattens the result:

```csharp
// Async operations
searchBox.Events().TextChanged
    .Select(e => e.EventArgs.Text)
    .SelectMany(text => SearchAsync(text))
    .Subscribe(results => DisplayResults(results));

// Nested sequences
Observable.Range(1, 3)
    .SelectMany(x => Observable.Range(x, 3))
    .Subscribe(n => Console.WriteLine(n));
```

### Scan

Accumulates values over time:

```csharp
// Running total
Observable.Range(1, 5)
    .Scan((acc, x) => acc + x)
    .Subscribe(total => Console.WriteLine($"Total: {total}"));
// Output: 1, 3, 6, 10, 15

// State machine
clicks.Scan(0, (count, _) => count + 1)
    .Subscribe(count => label.Text = $"Clicks: {count}");
```

## Filtering Operators

### Where (Filter)

Filters elements based on a predicate:

```csharp
// Only even numbers
Observable.Range(1, 10)
    .Where(x => x % 2 == 0)
    .Subscribe(x => Console.WriteLine(x));

// Valid input
searchBox.Events().TextChanged
    .Select(e => e.EventArgs.Text)
    .Where(text => !string.IsNullOrWhiteSpace(text))
    .Where(text => text.Length >= 3)
    .Subscribe(text => PerformSearch(text));
```

### DistinctUntilChanged

Only emits when the value changes:

```csharp
// Avoid duplicate searches
searchBox.Events().TextChanged
    .Select(e => e.EventArgs.Text)
    .DistinctUntilChanged()
    .Subscribe(text => PerformSearch(text));
```

### Take / TakeUntil

Limits the number of emissions:

```csharp
// First 5 items
observable.Take(5)
    .Subscribe(x => Console.WriteLine(x));

// Until a condition
clicks.TakeUntil(stopButton.Events().Click)
    .Subscribe(_ => Console.WriteLine("Click"));
```

### Skip

Skips specified number of items:

```csharp
// Skip first (ignore initial value)
this.WhenAnyValue(x => x.Property)
    .Skip(1)
    .Subscribe(value => HandleChange(value));
```

## Combining Operators

### CombineLatest

Combines latest values from multiple streams:

```csharp
// Form validation
var usernameValid = this.WhenAnyValue(x => x.Username)
    .Select(u => !string.IsNullOrWhiteSpace(u));

var passwordValid = this.WhenAnyValue(x => x.Password)
    .Select(p => p?.Length >= 6);

usernameValid.CombineLatest(passwordValid, (u, p) => u && p)
    .Subscribe(valid => submitButton.IsEnabled = valid);
```

### Merge

Merges multiple streams into one:

```csharp
// Multiple button clicks
var button1Clicks = button1.Events().Click;
var button2Clicks = button2.Events().Click;

button1Clicks.Merge(button2Clicks)
    .Subscribe(_ => HandleClick());
```

### Zip

Pairs elements from multiple streams:

```csharp
// Wait for both
var loaded = window.Events().Loaded;
var dataReady = dataService.Events().DataLoaded;

loaded.Zip(dataReady, (l, d) => Unit.Default)
    .Subscribe(_ => Initialize());
```

### Concat

Concatenates streams sequentially:

```csharp
// Sequential operations
firstOperation
    .Concat(secondOperation)
    .Concat(thirdOperation)
    .Subscribe(result => HandleResult(result));
```

## Time-Based Operators

### Throttle

Emits latest value after a period of inactivity:

```csharp
// Throttle search input
searchBox.Events().TextChanged
    .Throttle(TimeSpan.FromMilliseconds(300))
    .Select(e => e.EventArgs.Text)
    .Subscribe(text => PerformSearch(text));
```

### Debounce

Same as Throttle (alias):

```csharp
textBox.Events().TextChanged
    .Debounce(TimeSpan.FromMilliseconds(500))
    .Subscribe(e => HandleTextChange(e));
```

### Sample

Samples the stream at intervals:

```csharp
// Sample every second
mouseMove.Sample(TimeSpan.FromSeconds(1))
    .Subscribe(pos => UpdatePosition(pos));
```

### Delay

Delays emission by specified time:

```csharp
// Delayed notification
errorOccurred
    .Delay(TimeSpan.FromSeconds(3))
    .Subscribe(_ => HideErrorMessage());
```

### Timeout

Throws if no value within timespan:

```csharp
// Timeout after 5 seconds
longRunningOperation
    .Timeout(TimeSpan.FromSeconds(5))
    .Catch(Observable.Return(defaultValue))
    .Subscribe(result => HandleResult(result));
```

## Error Handling Operators

### Catch

Handles errors gracefully:

```csharp
// Provide fallback
observable
    .Catch<Data, Exception>(ex => 
    {
        LogError(ex);
        return Observable.Return(defaultData);
    })
    .Subscribe(data => DisplayData(data));
```

### Retry

Retries on error:

```csharp
// Retry 3 times
networkRequest
    .Retry(3)
    .Subscribe(
        data => ProcessData(data),
        error => ShowError(error));

// Retry with delay
networkRequest
    .RetryWhen(errors => errors
        .SelectMany((ex, attempt) => 
            Observable.Timer(TimeSpan.FromSeconds(attempt + 1))))
    .Subscribe(data => ProcessData(data));
```

### OnErrorResumeNext

Continues with another stream on error:

```csharp
primarySource
    .OnErrorResumeNext(fallbackSource)
    .Subscribe(data => ProcessData(data));
```

## Utility Operators

### Do

Side effects without altering the stream:

```csharp
observable
    .Do(x => Console.WriteLine($"Value: {x}"))
    .Do(x => LogValue(x))
    .Subscribe(x => ProcessValue(x));
```

### Materialize / Dematerialize

Wraps/unwraps notifications:

```csharp
observable
    .Materialize()
    .Subscribe(notification => 
    {
        if (notification.Kind == NotificationKind.OnNext)
            Console.WriteLine($"Value: {notification.Value}");
        else if (notification.Kind == NotificationKind.OnError)
            Console.WriteLine($"Error: {notification.Exception}");
    });
```

### ObserveOn

Specifies where to observe values:

```csharp
// Background work, UI updates
Observable.Start(() => ExpensiveOperation())
    .ObserveOn(RxSchedulers.MainThreadScheduler)
    .Subscribe(result => UpdateUI(result));
```

### SubscribeOn

Specifies where subscription occurs:

```csharp
observable
    .SubscribeOn(RxSchedulers.TaskpoolScheduler)
    .ObserveOn(RxSchedulers.MainThreadScheduler)
    .Subscribe(value => UpdateUI(value));
```

## Aggregation Operators

### Count

Counts elements:

```csharp
observable.Count()
    .Subscribe(count => Console.WriteLine($"Count: {count}"));
```

### Sum / Average / Min / Max

Aggregate operations:

```csharp
Observable.Range(1, 10)
    .Sum()
    .Subscribe(sum => Console.WriteLine($"Sum: {sum}"));

Observable.Range(1, 10)
    .Average()
    .Subscribe(avg => Console.WriteLine($"Average: {avg}"));
```

### Reduce / Aggregate

Custom aggregation:

```csharp
Observable.Range(1, 5)
    .Aggregate((acc, x) => acc * x)
    .Subscribe(product => Console.WriteLine($"Product: {product}"));
```

## Conditional Operators

### All / Any

Check conditions:

```csharp
// All elements positive
Observable.Range(1, 10)
    .All(x => x > 0)
    .Subscribe(allPositive => Console.WriteLine(allPositive));

// Any element even
Observable.Range(1, 10)
    .Any(x => x % 2 == 0)
    .Subscribe(anyEven => Console.WriteLine(anyEven));
```

### Contains

Checks for specific value:

```csharp
observable.Contains(42)
    .Subscribe(contains => Console.WriteLine($"Contains 42: {contains}"));
```

## Buffer and Window Operators

### Buffer

Collects elements into lists:

```csharp
// Buffer every 5 items
observable.Buffer(5)
    .Subscribe(batch => ProcessBatch(batch));

// Buffer by time
observable.Buffer(TimeSpan.FromSeconds(1))
    .Subscribe(batch => ProcessBatch(batch));
```

### Window

Projects into nested observables:

```csharp
observable.Window(TimeSpan.FromSeconds(1))
    .Subscribe(window => 
        window.Subscribe(x => Console.WriteLine(x)));
```

## ReactiveUI-Specific Operators

### ToProperty

Creates an ObservableAsPropertyHelper:

```csharp
this.WhenAnyValue(x => x.FirstName, x => x.LastName,
        (f, l) => $"{f} {l}")
    .ToProperty(this, x => x.FullName, out _fullName);
```

### InvokeCommand

Invokes a ReactiveCommand:

```csharp
searchBox.Events().TextChanged
    .Throttle(TimeSpan.FromMilliseconds(300))
    .InvokeCommand(SearchCommand);
```

### WhenAnyValue

Observes property changes:

```csharp
this.WhenAnyValue(x => x.SearchText)
    .Where(text => !string.IsNullOrEmpty(text))
    .Subscribe(text => PerformSearch(text));
```

## Composition Example

Combining multiple operators:

```csharp
searchBox.Events().TextChanged
    .Select(e => e.EventArgs.Text)           // Extract text
    .DistinctUntilChanged()                   // Ignore duplicates
    .Throttle(TimeSpan.FromMilliseconds(300)) // Wait for pause
    .Where(text => text.Length >= 3)          // Minimum length
    .SelectMany(text => SearchAsync(text))    // Async search
    .Catch(Observable.Return(emptyResults))   // Error handling
    .ObserveOn(RxSchedulers.MainThreadScheduler)     // UI thread
    .Subscribe(results => DisplayResults(results));
```

## Best Practices

1. **Chain Operators**: Build complex logic by chaining simple operators
2. **Error Handling**: Always include error handling operators
3. **Thread Management**: Use ObserveOn/SubscribeOn appropriately
4. **Performance**: Use appropriate operators (Throttle vs Sample)
5. **Readability**: Break complex chains into named intermediates

## Operator Categories Quick Reference

| Category | Operators |
|----------|-----------|
| **Transform** | Select, SelectMany, Scan |
| **Filter** | Where, DistinctUntilChanged, Take, Skip |
| **Combine** | CombineLatest, Merge, Zip, Concat |
| **Time** | Throttle, Debounce, Sample, Delay, Timeout |
| **Error** | Catch, Retry, OnErrorResumeNext |
| **Utility** | Do, ObserveOn, SubscribeOn |
| **Aggregate** | Count, Sum, Average, Min, Max, Reduce |
| **Conditional** | All, Any, Contains |
| **Buffer** | Buffer, Window |

## Resources

- [ReactiveX Operators](http://reactivex.io/documentation/operators.html)
- [RxMarbles (Visual Guide)](https://rxmarbles.com/)
- [Intro to Rx](http://introtorx.com/)
- [ReactiveUI Documentation](~/docs/index.md)

## Related Topics

- [Observables](~/docs/reactive-programming/observables.md)
- [Error Handling](~/docs/reactive-programming/error-handling.md)
- [Testing](~/docs/reactive-programming/testing.md)
- [Schedulers](~/docs/reactive-programming/schedulers.md)
