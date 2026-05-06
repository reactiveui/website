# Thread Troubleshooting

Working with asynchronous data streams in a UI environment often leads to the most common pitfall in ReactiveUI development: accessing or updating the UI from a background thread. This guide outlines how to identify, troubleshoot, and prevent threading issues in your application.

## The Symptoms

Threading issues typically manifest in two ways:
1. **Immediate Crashes**: On most XAML-based platforms (WPF, WinUI, WinForms), attempting to update a UI property from a background thread will throw an `InvalidOperationException` or a "Cross-thread access not valid" error.
2. **"Weird Shit"**: On some platforms (notably Apple-based ones like iOS and macOS), threading issues may not crash immediately. Instead, they cause erratic UI behavior, silent failures, or race conditions that are difficult to reproduce.

## Identifying the Source

When your application crashes with a threading exception, the source can sometimes be obscured by the framework's internal dispatching logic.

### 1. Disable "Just My Code"
In Visual Studio, go to **Tools -> Options -> Debugging -> General** and uncheck **Enable Just My Code**. This allows you to see the full call stack, including framework-level calls. Often, the threading exception is thrown inside a property setter or a binding update. By seeing the full stack, you can trace back to exactly which observable triggered the update.

### 2. Trace the Stack
If the crash occurs in a View's setter, it usually means the ViewModel was updated from a background thread, and the binding system attempted to push that change to the View immediately. Go back up the stack until you find your ViewModel code.

## Troubleshooting Strategies

### The "Shotgun" Approach (Not Recommended)
A common instinct is to add `.ObserveOn(RxSchedulers.MainThreadScheduler)` to every observable pipeline until the crash stops. While this works, it adds unnecessary overhead and makes the code harder to read. It's better to be surgical.

### Surgical Precision
Identify the boundary where your data transitions from a background operation (like a network request or database query) to a UI update.

```csharp
// DON'T: Updating properties from whatever thread the task finished on
SomeCommand = ReactiveCommand.CreateFromTask(async () => {
    var data = await _service.FetchData();
    this.SomeProperty = data; // Could be on a background thread!
});

// DO: Use RxSchedulers.MainThreadScheduler at the boundary
_searchResults = this
    .WhenAnyValue(x => x.SearchTerm)
    .Throttle(TimeSpan.FromMilliseconds(800))
    .SelectMany(FetchDataAsync)
    .ObserveOn(RxSchedulers.MainThreadScheduler) // Transition to UI thread here
    .ToProperty(this, x => x.SearchResults);
```

## Advanced: Global Thread Validation

For complex applications where threading issues are frequent, you can implement a global check. Since `ReactiveObject` implements `INotifyPropertyChanging`, you can hook into this globally during debugging to ensure all property changes are initiated on the UI thread.

```csharp
#if DEBUG
// In your App initialization
MessageBus.Current.Listen<IReactivePropertyChangedEventArgs<IReactiveObject>>()
    .Subscribe(x => {
        // Platform-specific check for UI thread
        if (!IsOnUIThread()) 
        {
            throw new Exception($"Property {x.PropertyName} on {x.Sender.GetType().Name} changed off the UI thread!");
        }
    });
#endif
```

> [!NOTE]
> This approach has a performance cost and should generally be restricted to `#if DEBUG` builds. It is also platform-dependent, as the definition of "UI thread" varies.

## Best Practices

- **Prefer `RxSchedulers` over `RxApp`**: For modern, AOT-friendly code, prefer using `RxSchedulers.MainThreadScheduler`. It avoids the reflection overhead and `RequiresUnreferencedCode` attributes associated with `RxApp`.
- **Observe at the Boundary**: Only use `ObserveOn` when you are about to update a property that is bound to the UI.
- **Commands are Your Friend**: `ReactiveCommand` is designed to handle thread marshaling for you. The results of a command created via `CreateFromTask` or `CreateFromObservable` are automatically observed on the `MainThreadScheduler` by default.

```csharp
// Results of 'Execute' will arrive on the MainThreadScheduler automatically
LoadData = ReactiveCommand.CreateFromTask(() => _service.FetchData());

LoadData.Subscribe(data => this.Data = data); // Safe!
```
