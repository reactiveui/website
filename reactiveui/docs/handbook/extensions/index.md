# ReactiveUI Extensions

The **ReactiveUI Extensions** repository contains a collection of useful extensions, helpers, and utilities that complement the core ReactiveUI framework. These extensions provide additional functionality for common scenarios in reactive application development.

## Overview

ReactiveUI Extensions includes various packages that extend ReactiveUI's capabilities across different areas:

- Platform-specific helpers
- Additional reactive patterns
- Testing utilities
- Integration helpers
- Community contributions

## Available Extensions

### Core Extensions

Extensions that work across all platforms and enhance core ReactiveUI functionality.

#### Reactive Property Extensions

Additional extension methods for working with reactive properties:

```csharp
// Property chaining helpers
this.WhenAnyValue(x => x.FirstName, x => x.LastName)
    .CombineLatest((f, l) => $"{f} {l}")
    .ToProperty(this, x => x.FullName, out _fullName);

// Conditional property updates
this.WhenAnyValue(x => x.IsEnabled)
    .WhereTrue()
    .Subscribe(_ => EnableFeature());

this.WhenAnyValue(x => x.IsEnabled)
    .WhereFalse()
    .Subscribe(_ => DisableFeature());
```

#### Observable Extensions

Enhanced observable operations:

```csharp
// Retry with delay
observable
    .RetryWithDelay(3, TimeSpan.FromSeconds(1))
    .Subscribe(result => ProcessResult(result));

// Timeout with fallback
observable
    .TimeoutOrDefault(TimeSpan.FromSeconds(5), defaultValue)
    .Subscribe(result => HandleResult(result));

// Buffer until condition
observable
    .BufferUntil(condition)
    .Subscribe(batch => ProcessBatch(batch));
```

### Platform Extensions

#### WPF Extensions

Additional helpers for WPF applications:

```csharp
// Dispatcher helpers
observable
    .ObserveOnDispatcher()
    .Subscribe(UpdateUI);

// Window management
this.WhenClosed()
    .Subscribe(_ => Cleanup());

// Dependency property binding
this.BindDependencyProperty(
    DependencyPropertyName,
    this.WhenAnyValue(x => x.ViewModel.Property));
```

#### MAUI Extensions

MAUI-specific utilities:

```csharp
// Navigation helpers
await this.NavigateToAsync<DetailViewModel>();
await this.NavigateBackAsync();

// Platform-specific code
if (DeviceInfo.Platform == DevicePlatform.iOS)
{
    // iOS-specific code
}

// Lifecycle events
this.WhenAppearing()
    .Subscribe(_ => LoadData());

this.WhenDisappearing()
    .Subscribe(_ => SaveState());
```

#### Avalonia Extensions

Avalonia-specific helpers:

```csharp
// Style helpers
control.ObserveStyleChanges()
    .Subscribe(style => ApplyStyle(style));

// Gesture recognition
control.ObserveGestures()
    .OfType<TapGesture>()
    .Subscribe(gesture => HandleTap(gesture));
```

## Testing Extensions

### Test Helpers

Utilities for testing reactive code:

```csharp
using ReactiveUI.Extensions.Testing;

[Fact]
public async Task ViewModel_ShouldLoadData()
{
    // Arrange
    var scheduler = new TestScheduler();
    var vm = new MyViewModel(scheduler);
    
    // Act
    vm.LoadDataCommand.Execute().Subscribe();
    scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
    
    // Assert
    vm.Data.Should().NotBeNull();
}
```

### Mock Extensions

Helpers for creating mocks:

```csharp
var mockService = MockRepository.Create<IDataService>();
mockService
    .SetupObservable(x => x.GetDataAsync())
    .Returns(Observable.Return(testData));
```

## Collection Extensions

Enhanced collection operations:

```csharp
// Reactive collection transformations
var derivedList = sourceList
    .Connect()
    .Transform(item => new ItemViewModel(item))
    .Filter(vm => vm.IsVisible)
    .Sort(SortExpressionComparer<ItemViewModel>.Ascending(x => x.Name))
    .Bind(out _items)
    .Subscribe();

// Collection change tracking
sourceCollection
    .ObserveCollectionChanges()
    .Subscribe(change => HandleChange(change));
```

## Validation Extensions

Additional validation helpers:

```csharp
using ReactiveUI.Extensions.Validation;

public class RegisterViewModel : ReactiveValidationObject
{
    [Reactive]
    private string _email;
    
    [Reactive]
    private string _password;
    
    [Reactive]
    private string _confirmPassword;
    
    public RegisterViewModel()
    {
        // Email validation with custom rule
        this.ValidationRule(
            vm => vm.Email,
            email => email.IsValidEmail(),
            "Please enter a valid email address");
        
        // Password strength validation
        this.ValidationRule(
            vm => vm.Password,
            password => password.MeetsPasswordRequirements(),
            "Password must be at least 8 characters and contain upper, lower, and digits");
        
        // Confirmation match validation
        this.ValidationRule(
            vm => vm.ConfirmPassword,
            vm => vm.Password,
            (confirm, password) => confirm == password,
            "Passwords must match");
    }
}

// Extension method implementations
public static class ValidationExtensions
{
    public static bool IsValidEmail(this string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
    
    public static bool MeetsPasswordRequirements(this string password)
    {
        return password?.Length >= 8 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(char.IsDigit);
    }
}
```

## Command Extensions

Enhanced ReactiveCommand functionality:

```csharp
// Command with progress
public ReactiveCommand<Unit, Unit> LongRunningCommand { get; }

public MyViewModel()
{
    LongRunningCommand = ReactiveCommand.CreateFromTask(async () =>
    {
        var progress = new Progress<int>(percent => 
            ProgressPercentage = percent);
        
        await LongRunningOperation(progress);
    });
    
    // Track command execution state
    LongRunningCommand.IsExecuting
        .ToProperty(this, x => x.IsLoading, out _isLoading);
    
    // Log command errors
    LongRunningCommand.ThrownExceptions
        .Subscribe(ex => LogError(ex));
}
```

## Logging Extensions

Enhanced logging capabilities:

```csharp
using ReactiveUI.Extensions.Logging;

public class MyViewModel : ReactiveObject, IEnableLogger
{
    public MyViewModel()
    {
        // Structured logging
        this.Log().Info("ViewModel created with {UserId}", userId);
        
        // Performance logging
        using (this.Log().MeasurePerformance("LoadData"))
        {
            LoadData();
        }
        
        // Exception logging with context
        try
        {
            PerformOperation();
        }
        catch (Exception ex)
        {
            this.Log().ErrorException("Operation failed", ex, new { UserId, Operation = "Save" });
        }
    }
}
```

## Serialization Extensions

Helpers for serialization:

```csharp
using ReactiveUI.Extensions.Serialization;

// JSON serialization with reactive properties
public class MyViewModel : ReactiveObject
{
    [Reactive]
    [JsonProperty("user_name")]
    private string _userName;
    
    public string ToJson()
    {
        return this.SerializeToJson();
    }
    
    public static MyViewModel FromJson(string json)
    {
        return json.DeserializeFromJson<MyViewModel>();
    }
}
```

## Scheduler Extensions

Additional scheduler utilities:

```csharp
// Conditional scheduler selection
var scheduler = isTestMode 
    ? (IScheduler)new TestScheduler() 
    : RxSchedulers.MainThreadScheduler;

// Throttled scheduler
observable
    .ObserveOn(ThrottledScheduler.Default)
    .Subscribe(UpdateUI);

// Batch scheduler for efficiency
observable
    .ObserveOn(BatchScheduler.Create(TimeSpan.FromMilliseconds(100)))
    .Subscribe(ProcessBatch);
```

## Installation

Install extensions via NuGet:

```bash
# Core extensions
dotnet add package ReactiveUI.Extensions

# Platform-specific extensions
dotnet add package ReactiveUI.Extensions.WPF
dotnet add package ReactiveUI.Extensions.MAUI
dotnet add package ReactiveUI.Extensions.Avalonia

# Testing extensions
dotnet add package ReactiveUI.Extensions.Testing
```

## Usage with RxAppBuilder

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWpf()
    .WithReactiveUIExtensions() // Add extensions
    .WithViewsFromAssembly(typeof(App).Assembly)
    .BuildApp();
```

## Best Practices

1. **Choose Extensions Wisely**: Only include extensions you actually need
2. **Test Thoroughly**: Extensions may have different maturity levels
3. **Check Compatibility**: Ensure extensions are compatible with your ReactiveUI version
4. **Read Documentation**: Each extension may have specific usage patterns
5. **Contribute Back**: Consider contributing your own extensions

## Custom Extensions

### Creating Your Own Extensions

```csharp
public static class MyCustomExtensions
{
    // Extension method for IObservable
    public static IObservable<T> LogAndContinue<T>(
        this IObservable<T> source, 
        string operationName)
    {
        return source.Do(
            item => Debug.WriteLine($"[{operationName}] Item: {item}"),
            ex => Debug.WriteLine($"[{operationName}] Error: {ex.Message}"),
            () => Debug.WriteLine($"[{operationName}] Completed"));
    }
    
    // Extension method for ReactiveObject
    public static IDisposable WhenPropertyChanges<T, TProperty>(
        this T source,
        Expression<Func<T, TProperty>> property,
        Action<TProperty> action)
        where T : ReactiveObject
    {
        return source.WhenAnyValue(property)
            .Skip(1) // Skip initial value
            .Subscribe(action);
    }
}

// Usage
observable
    .LogAndContinue("DataLoad")
    .Subscribe();

this.WhenPropertyChanges(x => x.SelectedItem, 
    item => LoadDetails(item));
```

## Community Extensions

The ReactiveUI community has created various extensions:

- **ReactiveUI.Events.Roslyn**: Event-to-observable via Roslyn
- **ReactiveUI.AndroidSupport**: Legacy Android support helpers
- **Custom Platform Helpers**: Various community-contributed helpers

Check the [Extensions Repository](https://github.com/reactiveui/Extensions) for the latest community contributions.

## Migration Guide

### From Legacy Extensions

If you're using older extension patterns:

```csharp
// Old way
RxApp.DependencyResolver.Register(() => new MyService());

// New way with extensions
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithRegistration(locator =>
    {
        locator.RegisterLazySingleton<IMyService>(() => new MyService());
    })
    .BuildApp();
```

## Troubleshooting

### Extension Not Found

1. **Verify Package**: Ensure the extension package is installed
2. **Check Namespace**: Import the correct namespace
3. **Update Packages**: Extensions may require specific ReactiveUI versions

### Conflicts with Core

1. **Check Priorities**: Extensions may override core functionality
2. **Review Dependencies**: Ensure compatible versions
3. **Explicit Imports**: Use full namespace if ambiguous

## Contributing

To contribute your own extensions:

1. Fork the [Extensions Repository](https://github.com/reactiveui/Extensions)
2. Create a new package for your extension
3. Add comprehensive tests
4. Submit a pull request with documentation

## Additional Resources

- [Extensions GitHub Repository](https://github.com/reactiveui/Extensions)
- [ReactiveUI Documentation](~/docs/index.md)
- [Community Slack](https://join.slack.com/t/reactivex/shared_invite/zt-lt48skpz-G5WDYOAuzA80_MByZrLT0g)
- [Sample Applications](~/docs/resources/samples.md)

## Related Topics

- [Reactive Programming](~/docs/reactive-programming/index.md)
- [Testing](~/docs/handbook/testing.md)
- [Logging](~/docs/handbook/logging/index.md)
- [Data Binding](~/docs/handbook/data-binding/index.md)
