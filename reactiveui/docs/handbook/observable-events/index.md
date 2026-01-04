# ObservableEvents

**ReactiveMarbles.ObservableEvents** is a source generator that converts .NET events into observable sequences, making them easier to compose, filter, and combine with other reactive operations.

## Overview

Instead of manually subscribing to events and managing event handlers, ObservableEvents automatically generates extension methods that convert events to `IObservable<T>` sequences. This allows you to use the full power of Rx operators with any .NET event.

## Installation

Install the package via NuGet:

```bash
dotnet add package ReactiveMarbles.ObservableEvents.SourceGenerator
```

Or via Package Manager:

```xml
<PackageReference Include="ReactiveMarbles.ObservableEvents.SourceGenerator" Version="*" />
```

## How It Works

ObservableEvents is a **source generator** that runs at compile-time. It analyzes your project's referenced assemblies and generates extension methods for all public events. These extension methods are named `Events()` and return an `IObservable<EventPattern<TEventArgs>>`.

### Generated Code Pattern

For any event like:

```csharp
public event EventHandler<MouseEventArgs> MouseMove;
```

The generator creates:

```csharp
public static IObservable<EventPattern<MouseEventArgs>> MouseMoveEvent(this YourClass source)
{
    return Observable.FromEventPattern<MouseEventArgs>(
        h => source.MouseMove += h,
        h => source.MouseMove -= h);
}
```

## Basic Usage

### Converting Events to Observables

```csharp
using ReactiveMarbles.ObservableEvents;

public partial class MainViewModel : ReactiveObject
{
    public MainViewModel()
    {
        // Subscribe to button click events
        var button = new Button();
        
        button.Events().Click
            .Subscribe(_ => Console.WriteLine("Button clicked!"));
    }
}
```

### With Reactive Extensions Operators

The real power comes from composing with Rx operators:

```csharp
// Throttle rapid clicks
button.Events().Click
    .Throttle(TimeSpan.FromMilliseconds(500))
    .Subscribe(_ => PerformAction());

// Take only the first 3 clicks
button.Events().Click
    .Take(3)
    .Subscribe(_ => Console.WriteLine("Click counted"));

// Filter based on event args
textBox.Events().TextChanged
    .Select(e => e.Sender as TextBox)
    .Where(tb => tb?.Text.Length > 3)
    .Subscribe(tb => ValidateInput(tb.Text));
```

## Platform-Specific Examples

### WPF

```csharp
using ReactiveMarbles.ObservableEvents;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            // Window events
            this.Events().Loaded
                .Subscribe(_ => Console.WriteLine("Window loaded"))
                .DisposeWith(disposables);
            
            this.Events().Closing
                .Subscribe(e => 
                {
                    var result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo);
                    e.EventArgs.Cancel = result == MessageBoxResult.No;
                })
                .DisposeWith(disposables);
            
            // Mouse events with throttling
            SearchTextBox.Events().KeyUp
                .Throttle(TimeSpan.FromMilliseconds(300))
                .Select(e => (e.Sender as TextBox)?.Text)
                .Where(text => !string.IsNullOrWhiteSpace(text))
                .Subscribe(text => ViewModel.Search(text))
                .DisposeWith(disposables);
        });
    }
}
```

### MAUI

```csharp
using ReactiveMarbles.ObservableEvents;

public partial class MainPage : ReactiveContentPage<MainViewModel>
{
    public MainPage()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            // Entry text changes
            SearchEntry.Events().TextChanged
                .Throttle(TimeSpan.FromMilliseconds(500))
                .Select(e => e.EventArgs.NewTextValue)
                .Subscribe(text => ViewModel.SearchText = text)
                .DisposeWith(disposables);
            
            // Button taps with debounce
            SubmitButton.Events().Clicked
                .Throttle(TimeSpan.FromSeconds(1))
                .Subscribe(_ => ViewModel.SubmitCommand.Execute().Subscribe())
                .DisposeWith(disposables);
        });
    }
}
```

### Avalonia

```csharp
using ReactiveMarbles.ObservableEvents;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            // Pointer events
            this.Events().PointerPressed
                .Subscribe(e => Console.WriteLine($"Clicked at: {e.EventArgs.GetPosition(this)}"))
                .DisposeWith(disposables);
            
            // Text input with validation
            UsernameTextBox.Events().TextChanged
                .Select(e => e.EventArgs.Text)
                .DistinctUntilChanged()
                .Subscribe(text => ViewModel.Username = text)
                .DisposeWith(disposables);
        });
    }
}
```

### Blazor

```csharp
using ReactiveMarbles.ObservableEvents;

public partial class CounterComponent : ReactiveComponentBase<CounterViewModel>
{
    protected override void OnInitialized()
    {
        ViewModel = new CounterViewModel();
        
        this.WhenActivated(disposables =>
        {
            // For Blazor, you typically use @onclick in markup
            // But you can still use ObservableEvents for custom components
            
            // Example with a custom component that has events
            CustomComponent.Events().ValueChanged
                .Subscribe(e => ViewModel.Value = e.EventArgs.NewValue)
                .DisposeWith(disposables);
        });
        
        base.OnInitialized();
    }
}
```

## Advanced Patterns

### Combining Multiple Events

```csharp
// Wait for both events before proceeding
var loaded = window.Events().Loaded;
var dataReady = dataService.Events().DataLoaded;

loaded.Zip(dataReady, (l, d) => Unit.Default)
    .Subscribe(_ => InitializeUI());
```

### Event Sequences with State

```csharp
// Track double-clicks
button.Events().Click
    .Buffer(TimeSpan.FromMilliseconds(300))
    .Where(clicks => clicks.Count == 2)
    .Subscribe(_ => HandleDoubleClick());
```

### Mouse Drag Pattern

```csharp
var mouseDown = canvas.Events().MouseDown;
var mouseMove = canvas.Events().MouseMove;
var mouseUp = canvas.Events().MouseUp;

var drag = from down in mouseDown
           from move in mouseMove.TakeUntil(mouseUp)
           select move.EventArgs.GetPosition(canvas);

drag.Subscribe(pos => UpdateDragPosition(pos));
```

### Keyboard Shortcuts

```csharp
this.Events().KeyDown
    .Where(e => e.EventArgs.Key == Key.S && 
                (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
    .Subscribe(_ => SaveDocument());
```

## Integration with ReactiveUI Bindings

ObservableEvents works seamlessly with ReactiveUI's binding system:

```csharp
public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            // Convert event to property
            SearchBox.Events().TextChanged
                .Select(e => (e.Sender as TextBox)?.Text)
                .BindTo(this, x => x.ViewModel.SearchText)
                .DisposeWith(disposables);
            
            // Use event with command
            SaveButton.Events().Click
                .InvokeCommand(ViewModel.SaveCommand)
                .DisposeWith(disposables);
        });
    }
}
```

## Performance Considerations

1. **Memory Management**: Always use `DisposeWith(disposables)` inside `WhenActivated` to prevent memory leaks
2. **Throttling**: Use `Throttle` or `Debounce` for high-frequency events like mouse moves or text changes
3. **Unsubscription**: The generator handles proper event unsubscription when you dispose the observable

## Common Patterns

### Form Validation

```csharp
var usernameValid = UsernameTextBox.Events().TextChanged
    .Select(e => e.EventArgs.Text)
    .Select(text => !string.IsNullOrWhiteSpace(text) && text.Length >= 3);

var passwordValid = PasswordTextBox.Events().TextChanged
    .Select(e => e.EventArgs.Text)
    .Select(text => text?.Length >= 6);

usernameValid.CombineLatest(passwordValid, (u, p) => u && p)
    .Subscribe(valid => SubmitButton.IsEnabled = valid);
```

### Search with Cancellation

```csharp
SearchBox.Events().TextChanged
    .Throttle(TimeSpan.FromMilliseconds(500))
    .Select(e => (e.Sender as TextBox)?.Text)
    .DistinctUntilChanged()
    .SelectMany(async text => await SearchAsync(text))
    .ObserveOn(RxSchedulers.MainThreadScheduler)
    .Subscribe(results => DisplayResults(results));
```

### Focus Management

```csharp
// Auto-focus next field
FirstNameTextBox.Events().KeyDown
    .Where(e => e.EventArgs.Key == Key.Enter)
    .Subscribe(_ => LastNameTextBox.Focus());
```

## Configuration

ObservableEvents can be configured via MSBuild properties in your project file:

```xml
<PropertyGroup>
    <!-- Generate events for specific assemblies only -->
    <ObservableEventsAssemblies>MyCustomControls</ObservableEventsAssemblies>
    
    <!-- Exclude specific types -->
    <ObservableEventsExclude>ObsoleteControl</ObservableEventsExclude>
</PropertyGroup>
```

## Troubleshooting

### Events Not Generated

If you don't see generated events:

1. **Rebuild the project** - Source generators run during compilation
2. **Check the assembly is referenced** - ObservableEvents only generates for referenced assemblies
3. **Verify event visibility** - Only public events are generated
4. **Update packages** - Ensure you have the latest version

### IDE Support

- **Visual Studio 2022+**: Full support with IntelliSense
- **Visual Studio Code**: Install C# Dev Kit for full support
- **Rider**: Supported in recent versions

### Viewing Generated Code

To see generated code:

1. In Solution Explorer, expand Dependencies ? Analyzers ? ReactiveMarbles.ObservableEvents.SourceGenerator
2. Look for generated files under the generator node

## Comparison with Manual Event Handling

### Traditional Approach

```csharp
public MainWindow()
{
    InitializeComponent();
    
    button.Click += Button_Click;
    textBox.TextChanged += TextBox_TextChanged;
}

private void Button_Click(object sender, EventArgs e)
{
    // Handle click
}

private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
{
    // Handle text change
}

protected override void OnClosed(EventArgs e)
{
    // Must manually unsubscribe
    button.Click -= Button_Click;
    textBox.TextChanged -= TextBox_TextChanged;
    base.OnClosed(e);
}
```

### ObservableEvents Approach

```csharp
public MainWindow()
{
    InitializeComponent();
    
    this.WhenActivated(disposables =>
    {
        button.Events().Click
            .Subscribe(_ => HandleClick())
            .DisposeWith(disposables);
        
        textBox.Events().TextChanged
            .Throttle(TimeSpan.FromMilliseconds(300))
            .Subscribe(e => HandleTextChange(e.EventArgs))
            .DisposeWith(disposables);
        
        // Automatic cleanup when disposed
    });
}
```

## Best Practices

1. **Always Dispose**: Use `WhenActivated` and `DisposeWith` to ensure proper cleanup
2. **Throttle High-Frequency Events**: Use `Throttle` or `Debounce` for events that fire frequently
3. **Use Strong Typing**: Leverage the typed `EventPattern<T>` for better IntelliSense
4. **Combine with Commands**: Use `InvokeCommand` to integrate with ReactiveCommand
5. **Handle Errors**: Use `Catch` or `OnErrorResumeNext` for resilient event handling

## Additional Resources

- [ObservableEvents GitHub Repository](https://github.com/reactivemarbles/ObservableEvents)
- [ReactiveUI Documentation](~/docs/index.md)
- [Reactive Extensions Documentation](http://reactivex.io/)
- [Sample Applications](~/docs/resources/samples.md)

## Related Topics

- [Data Binding](~/docs/handbook/data-binding/index.md)
- [WhenActivated](~/docs/handbook/when-activated.md)
- [Reactive Extensions](~/docs/reactive-programming/index.md)
- [Commands](~/docs/handbook/commands/index.md)
