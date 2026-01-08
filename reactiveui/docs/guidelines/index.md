# Guidelines

## Overview

These guidelines provide best practices and recommendations for building modern ReactiveUI applications. Following these patterns will help you create maintainable, testable, and performant reactive applications.

## Modern Development Patterns

### Use RxAppBuilder for Application Initialization

**RxAppBuilder** is the modern way to initialize ReactiveUI applications. It provides a fluent API for configuring dependency injection, schedulers, and platform-specific features.

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWpf() // or .WithMaui(), .WithBlazor(), etc.
    .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
    .WithRegistration(locator =>
    {
        // Register your services
        locator.RegisterLazySingleton<IDataService>(() => new DataService());
        locator.RegisterLazySingleton<INavigationService>(() => new NavigationService());
    })
    .BuildApp();
```

**Benefits:**
- Centralized configuration
- Type-safe service registration
- Platform-specific optimizations
- Simplified testing setup
- Clear dependency graph

### Use ReactiveUI.SourceGenerators

**ReactiveUI.SourceGenerators** eliminates boilerplate code through compile-time code generation. Always prefer source generators over manual property implementation or Fody.

#### Reactive Properties

```csharp
public partial class MyViewModel : ReactiveObject
{
    // Old way (manual)
    private string _name;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    
    // New way (source generators) ✅
    [Reactive]
    private string _name = string.Empty;
}
```

#### Observable As Property Helper

```csharp
public partial class MyViewModel : ReactiveObject
{
    // Old way (manual)
    private readonly ObservableAsPropertyHelper<string> _fullName;
    public string FullName => _fullName.Value;
    
    public MyViewModel()
    {
        _fullName = this.WhenAnyValue(x => x.FirstName, x => x.LastName, 
                (f, l) => $"{f} {l}")
            .ToProperty(this, x => x.FullName);
    }
    
    // New way (source generators) ✅
    [Reactive]
    private string _firstName = string.Empty;
    
    [Reactive]
    private string _lastName = string.Empty;
    
    [ObservableAsProperty]
    private string _fullName = string.Empty;
    
    public MyViewModel()
    {
        _fullNameHelper = this.WhenAnyValue(x => x.FirstName, x => x.LastName,
                (f, l) => $"{f} {l}")
            .ToProperty(this, nameof(FullName));
    }
}
```

#### Reactive Commands

```csharp
public partial class MyViewModel : ReactiveObject
{
    // Old way (manual)
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    
    public MyViewModel()
    {
        SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync);
    }
    
    private async Task SaveAsync() { /* ... */ }
    
    // New way (source generators) ✅
    [ReactiveCommand]
    private async Task Save()
    {
        // Your save logic
    }
}
```

**Benefits:**
- Less boilerplate code
- Compile-time validation
- Better IDE support
- Easier to read and maintain
- No IL weaving complexity

### Use ObservableEvents for Event Handling

Instead of manually subscribing to events, use **ReactiveMarbles.ObservableEvents.SourceGenerator** to convert events to observables.

```csharp
// Old way ❌
button.Click += Button_Click;
protected override void OnClosed(EventArgs e)
{
    button.Click -= Button_Click;
}

// New way ✅
this.WhenActivated(disposables =>
{
    button.Events().Click
        .Throttle(TimeSpan.FromMilliseconds(500))
        .Subscribe(_ => HandleClick())
        .DisposeWith(disposables);
});
```

### Always Use WhenActivated

**WhenActivated** ensures proper lifecycle management and prevents memory leaks.

```csharp
public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            // All subscriptions here are automatically disposed
            this.Bind(ViewModel, vm => vm.Name, v => v.NameTextBox.Text)
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel, vm => vm.SaveCommand, v => v.SaveButton)
                .DisposeWith(disposables);
        });
    }
}
```

**Rules:**
- ✅ Always wrap subscriptions in WhenActivated
- ✅ Always call DisposeWith(disposables)
- ❌ Never subscribe without disposing
- ❌ Don't create subscriptions in constructors (outside WhenActivated)

## Architecture Guidelines

### ViewModel Design

#### Keep ViewModels Platform-Agnostic

ViewModels should not reference platform-specific types or UI frameworks.

```csharp
// Bad ❌
public class MyViewModel : ReactiveObject
{
    private readonly Window _window; // Platform-specific
    public void ShowDialog() => _window.Show();
}

// Good ✅
public class MyViewModel : ReactiveObject
{
    private readonly IDialogService _dialogService; // Interface
    public async Task ShowDialog() => await _dialogService.ShowAsync();
}
```

#### Single Responsibility

Each ViewModel should have one clear responsibility.

```csharp
// Bad - too many responsibilities ❌
public class MainViewModel
{
    public void LoadData() { }
    public void SaveData() { }
    public void ExportToExcel() { }
    public void SendEmail() { }
    public void GenerateReport() { }
}

// Good - focused responsibility ✅
public class MainViewModel
{
    private readonly IDataService _dataService;
    private readonly INavigationService _navigation;
    
    public ReactiveCommand<Unit, Unit> LoadDataCommand { get; }
    public ReactiveCommand<Unit, Unit> NavigateToSettingsCommand { get; }
}
```

#### Use Reactive Properties Appropriately

```csharp
public partial class SearchViewModel : ReactiveObject
{
    [Reactive]
    private string _searchText = string.Empty;
    
    [ObservableAsProperty]
    private bool _isSearching;
    
    [ObservableAsProperty]
    private List<SearchResult> _results;
    
    public SearchViewModel()
    {
        // Derived state from commands
        SearchCommand = ReactiveCommand.CreateFromTask(
            async () => await PerformSearchAsync(SearchText),
            this.WhenAnyValue(x => x.SearchText, text => !string.IsNullOrWhiteSpace(text)));
        
        SearchCommand.IsExecuting
            .ToProperty(this, x => x.IsSearching);
        
        SearchCommand
            .ToProperty(this, x => x.Results);
    }
    
    public ReactiveCommand<Unit, List<SearchResult>> SearchCommand { get; }
}
```

### Dependency Injection

#### Register Services with RxAppBuilder

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWpf()
    .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
    .WithRegistration(locator =>
    {
        // Singletons for stateful services
        locator.RegisterLazySingleton<IAuthService>(() => new AuthService());
        locator.RegisterLazySingleton<ISettingsService>(() => new SettingsService());
        
        // Transient for ViewModels
        locator.Register<MainViewModel>(() => new MainViewModel());
        locator.Register<DetailsViewModel>(() => new DetailsViewModel());
    })
    .BuildApp();
```

#### Constructor Injection Pattern

```csharp
public class MainViewModel : ReactiveObject
{
    private readonly IDataService _dataService;
    private readonly INavigationService _navigation;
    
    // Constructor injection ✅
    public MainViewModel(
        IDataService dataService,
        INavigationService navigation)
    {
        _dataService = dataService;
        _navigation = navigation;
    }
    
    // Alternative: Service locator (use sparingly)
    public MainViewModel()
    {
        _dataService = AppLocator.Current.GetService<IDataService>();
        _navigation = AppLocator.Current.GetService<INavigationService>();
    }
}
```

## Performance Guidelines

### Use Throttle and Debounce

Prevent excessive operations on high-frequency events.

```csharp
// Search as user types
this.WhenAnyValue(x => x.SearchText)
    .Throttle(TimeSpan.FromMilliseconds(500))
    .DistinctUntilChanged()
    .Where(text => !string.IsNullOrWhiteSpace(text))
    .SelectMany(async text => await SearchAsync(text))
    .ObserveOn(RxSchedulers.MainThreadScheduler)
    .Subscribe(results => Results = results);
```

### Use DynamicData for Collections

For reactive collections, always use DynamicData instead of ObservableCollection.

```csharp
public partial class ItemListViewModel : ReactiveObject
{
    private readonly SourceCache<Item, int> _itemsCache;
    private readonly ReadOnlyObservableCollection<ItemViewModel> _items;
    
    public ReadOnlyObservableCollection<ItemViewModel> Items => _items;
    
    public ItemListViewModel()
    {
        _itemsCache = new SourceCache<Item, int>(x => x.Id);
        
        _itemsCache.Connect()
            .Transform(item => new ItemViewModel(item))
            .Filter(vm => vm.IsVisible)
            .Sort(SortExpressionComparer<ItemViewModel>.Ascending(x => x.Name))
            .ObserveOn(RxSchedulers.MainThreadScheduler)
            .Bind(out _items)
            .Subscribe();
    }
}
```

### Optimize Observable Chains

```csharp
// Bad - multiple subscriptions ❌
this.WhenAnyValue(x => x.Property1).Subscribe(/* ... */);
this.WhenAnyValue(x => x.Property1).Subscribe(/* ... */);
this.WhenAnyValue(x => x.Property1).Subscribe(/* ... */);

// Good - shared observable ✅
var sharedObservable = this.WhenAnyValue(x => x.Property1)
    .Publish()
    .RefCount();

sharedObservable.Subscribe(/* ... */);
sharedObservable.Subscribe(/* ... */);
sharedObservable.Subscribe(/* ... */);
```

## Testing Guidelines

### Use ReactiveUI.Testing

```csharp
[Fact]
public void ViewModel_LoadsData_WhenCommandExecuted()
{
    // Arrange
    new TestScheduler().With(scheduler =>
    {
        var mockService = Substitute.For<IDataService>();
        mockService.GetDataAsync().Returns(Observable.Return(testData));
        
        var vm = new MainViewModel(mockService);
        
        // Act
        vm.LoadDataCommand.Execute().Subscribe();
        scheduler.AdvanceBy(TimeSpan.FromSeconds(1).Ticks);
        
        // Assert
        vm.Data.Should().NotBeNull();
        vm.IsLoading.Should().BeFalse();
    });
}
```

### Test ViewModels in Isolation

```csharp
[Fact]
public async Task SearchCommand_FiltersResults_BasedOnSearchText()
{
    // Arrange
    var vm = new SearchViewModel();
    vm.SearchText = "test";
    
    // Act
    await vm.SearchCommand.Execute();
    
    // Assert
    vm.Results.Should().NotBeEmpty();
    vm.Results.Should().OnlyContain(r => r.Name.Contains("test"));
}
```

## Error Handling

### Use ThrownExceptions

```csharp
public MyViewModel()
{
    SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync);
    
    // Handle errors gracefully
    SaveCommand.ThrownExceptions
        .Subscribe(ex =>
        {
            // Log the error
            this.Log().Error(ex, "Failed to save");
            
            // Show user-friendly message
            ErrorMessage = "Unable to save. Please try again.";
        });
}
```

### Validate User Input

```csharp
public partial class LoginViewModel : ReactiveValidationObject
{
    [Reactive]
    private string _username = string.Empty;
    
    [Reactive]
    private string _password = string.Empty;
    
    public LoginViewModel()
    {
        // Validation rules
        this.ValidationRule(
            vm => vm.Username,
            username => !string.IsNullOrWhiteSpace(username),
            "Username is required");
        
        this.ValidationRule(
            vm => vm.Password,
            password => password?.Length >= 6,
            "Password must be at least 6 characters");
        
        // Command only executes when valid
        LoginCommand = ReactiveCommand.CreateFromTask(
            LoginAsync,
            this.IsValid());
    }
    
    [ReactiveCommand]
    private async Task Login() { /* ... */ }
}
```

## Platform-Specific Guidelines

### WPF

```csharp
// Use ReactiveWindow<T>
public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        
        this.WhenActivated(disposables =>
        {
            // Bindings here
        });
    }
}
```

### MAUI

```csharp
// Use ReactiveContentPage<T>
public partial class MainPage : ReactiveContentPage<MainViewModel>
{
    public MainPage()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        
        this.WhenActivated(disposables =>
        {
            // Bindings here
        });
    }
}
```

### Blazor

```csharp
// Use ReactiveComponentBase<T> and call StateHasChanged
public partial class CounterComponent : ReactiveComponentBase<CounterViewModel>
{
    protected override void OnInitialized()
    {
        ViewModel = new CounterViewModel();
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel.Count)
                .Subscribe(_ => InvokeAsync(StateHasChanged))
                .DisposeWith(disposables);
        });
        
        base.OnInitialized();
    }
}
```

## Common Anti-Patterns to Avoid

### ❌ Don't Use Static RxApp

```csharp
// Bad ❌
RxApp.MainThreadScheduler = _myCustomScheduler;
RxApp.DefaultExceptionHandler = /* ... */;

// Good ✅
// Use RxAppBuilder to configure schedulers
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithCustomScheduler(/* ... */)
    .WithDefaultExceptionHandler(/* ... */)
    .BuildApp();
```

### ❌ Don't Forget to Dispose

```csharp
// Bad ❌
this.WhenAnyValue(x => x.Property).Subscribe(/* ... */);

// Good ✅
this.WhenActivated(disposables =>
{
    this.WhenAnyValue(x => x.Property)
        .Subscribe(/* ... */)
        .DisposeWith(disposables);
});
```

### ❌ Don't Mix MVVM Patterns

```csharp
// Bad - code-behind in view ❌
private void Button_Click(object sender, EventArgs e)
{
    // Business logic here
}

// Good - command in ViewModel ✅
[ReactiveCommand]
private void ExecuteAction()
{
    // Business logic here
}
```

### ❌ Don't Block on Async

```csharp
// Bad ❌
var result = asyncOperation.Result;
asyncOperation.Wait();

// Good ✅
var result = await asyncOperation;
```

## Migration Path

If you're upgrading from older ReactiveUI versions:

1. **Replace Fody with SourceGenerators** - See [Migration Guide](~/docs/upgrading/fody-to-sourcegenerators.md)
2. **Adopt RxAppBuilder** - See [Migration Guide](~/docs/upgrading/rxappbuilder-migration.md)
3. **Update to Modern Patterns** - Follow this guide's recommendations
4. **Migrate from Xamarin** - See [Xamarin to MAUI Guide](~/docs/upgrading/xamarin-to-maui.md)

## Additional Resources

- [Getting Started](~/docs/getting-started/index.md)
- [Handbook](~/docs/handbook/index.md)
- [Reactive Programming Basics](~/docs/reactive-programming/index.md)
- [Testing Guide](~/docs/handbook/testing.md)
- [Sample Applications](~/docs/resources/samples.md)

## Platform-Specific Guidelines

For detailed platform-specific guidance, see:

- [Framework Guidelines](~/docs/guidelines/framework/toc.yml)
- [Platform Guidelines](~/docs/guidelines/platform/toc.yml)
- [Debugging Guidelines](~/docs/guidelines/debugging/toc.yml)

