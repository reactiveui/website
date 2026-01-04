# Migration Guide: RxAppBuilder

This guide helps you migrate from legacy ReactiveUI initialization patterns to the modern **RxAppBuilder** pattern.

## Why Migrate?

### Benefits of RxAppBuilder

✅ **Centralized Configuration**: All setup in one place  
✅ **Type-Safe**: Compile-time validation of configuration  
✅ **Platform-Specific Optimizations**: Automatic scheduler setup  
✅ **Dependency Injection**: Built-in service registration  
✅ **Testability**: Easy to mock and configure for tests  
✅ **Modern Pattern**: Aligns with .NET best practices  

### Legacy Limitations

❌ **Scattered Configuration**: Setup code spread across multiple files  
❌ **Static Dependencies**: Hard to test and override  
❌ **Manual Scheduler Setup**: Error-prone configuration  
❌ **No DI Integration**: Manual service location  
❌ **Platform Detection**: Manual platform-specific code  

## Prerequisites

- ReactiveUI 22.2.1+
- .NET 8.0+ or .NET Standard 2.0+
- Splat 17.1.1+ (included with ReactiveUI)

## Migration Patterns

### Pattern 1: Basic Initialization

#### Legacy Approach

```csharp
// In App.xaml.cs or Program.cs
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        // Manual scheduler setup
        RxApp.MainThreadScheduler = new DispatcherScheduler(Dispatcher.CurrentDispatcher);
        
        // Manual service registration
        Locator.CurrentMutable.RegisterLazySingleton<IDataService>(() => new DataService());
        Locator.CurrentMutable.RegisterLazySingleton<IScreen>(() => new MainViewModel());
        
        // Manual view registration
        Locator.CurrentMutable.Register<IViewFor<MainViewModel>>(() => new MainView());
        
        MainWindow = new MainWindow();
    }
}
```

#### RxAppBuilder Approach ✅

```csharp
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        var app = RxAppBuilder.CreateReactiveUIBuilder()
            .WithWpf() // Automatically sets up DispatcherScheduler
            .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
            .WithRegistration(locator =>
            {
                locator.RegisterLazySingleton<IDataService>(() => new DataService());
                locator.RegisterLazySingleton<IScreen>(() => new MainViewModel());
            })
            .BuildApp();
        
        MainWindow = new MainWindow();
    }
}
```

### Pattern 2: Platform-Specific Setup

#### WPF

```csharp
// Legacy ❌
RxApp.MainThreadScheduler = new DispatcherScheduler(Dispatcher.CurrentDispatcher);

// RxAppBuilder ✅
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWpf() // Automatic scheduler setup
    .BuildApp();
```

#### MAUI

```csharp
// Legacy ❌ (Not available)
// Manual platform detection and setup required

// RxAppBuilder ✅
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();
    
    builder.UseMauiApp<App>();
    
    // Initialize ReactiveUI with RxAppBuilder
    var app = RxAppBuilder.CreateReactiveUIBuilder()
        .WithMaui()
        .WithViewsFromAssembly(typeof(App).Assembly)
        .WithRegistration(locator =>
        {
            locator.RegisterLazySingleton<IDataService>(() => new DataService());
        })
        .BuildApp();
    
    return builder.Build();
}
```

#### Blazor (Server)

```csharp
// Legacy ❌
// Complex manual setup required

// RxAppBuilder ✅
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithBlazor()
    .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
    .WithRegistration(locator =>
    {
        locator.RegisterLazySingleton<IDataService>(() => new DataService());
    })
    .BuildApp();

var webApp = builder.Build();
// Configure and run...
```

#### Avalonia

```csharp
// Legacy ❌
public override void OnFrameworkInitializationCompleted()
{
    Locator.CurrentMutable.Register<IScreen>(() => new MainViewModel());
    Locator.CurrentMutable.Register<IViewFor<MainViewModel>>(() => new MainWindow());
    
    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
        desktop.MainWindow = new MainWindow();
    }
}

// RxAppBuilder ✅
public override void OnFrameworkInitializationCompleted()
{
    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
        // Note: Avalonia handles ReactiveUI init via .UseReactiveUI(rxuiBuilder => {/* ... */})
        AppLocator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
        AppLocator.CurrentMutable.RegisterLazySingleton<IScreen>(() => new MainViewModel());
        
        desktop.MainWindow = new MainWindow
        {
            DataContext = AppLocator.Current.GetService<MainViewModel>()
        };
    }
    
    base.OnFrameworkInitializationCompleted();
}
```

### Pattern 3: View Registration

#### Legacy Manual Registration

```csharp
// One by one ❌
Locator.CurrentMutable.Register<IViewFor<MainViewModel>>(() => new MainView());
Locator.CurrentMutable.Register<IViewFor<DetailsViewModel>>(() => new DetailsView());
Locator.CurrentMutable.Register<IViewFor<SettingsViewModel>>(() => new SettingsView());
```

#### RxAppBuilder Auto-Registration ✅

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWpf()
    .WithViewsFromAssembly(Assembly.GetExecutingAssembly()) // Registers all IViewFor<T>
    .BuildApp();
```

### Pattern 4: Custom Schedulers

#### Legacy Approach

```csharp
// Manual scheduler configuration ❌
RxApp.MainThreadScheduler = CustomScheduler.Instance;
RxApp.TaskpoolScheduler = CustomTaskScheduler.Instance;
```

#### RxAppBuilder Approach ✅

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWpf()
    .WithCustomScheduler(mainThreadScheduler: CustomScheduler.Instance)
    .WithCustomScheduler(taskpoolScheduler: CustomTaskScheduler.Instance)
    .BuildApp();

// Access configured schedulers
var mainScheduler = app.MainThreadScheduler;
var taskScheduler = app.TaskpoolScheduler;

// Optional: Register in locator if needed
AppLocator.CurrentMutable.RegisterConstant<IScheduler>(mainScheduler, "MainThread");
AppLocator.CurrentMutable.RegisterConstant<IScheduler>(taskScheduler, "Taskpool");
```

### Pattern 5: Service Registration

#### Legacy Locator Pattern

```csharp
// Scattered throughout app ❌
Locator.CurrentMutable.RegisterLazySingleton<IAuthService>(() => new AuthService());
Locator.CurrentMutable.RegisterLazySingleton<IDataService>(() => new DataService());
Locator.CurrentMutable.Register<MainViewModel>(() => new MainViewModel());
```

#### RxAppBuilder Centralized ✅

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWpf()
    .WithRegistration(locator =>
    {
        // All services in one place
        locator.RegisterLazySingleton<IAuthService>(() => new AuthService());
        locator.RegisterLazySingleton<IDataService>(() => new DataService());
        locator.Register<MainViewModel>(() => new MainViewModel());
        
        // With dependencies
        locator.Register<DetailsViewModel>(() => 
            new DetailsViewModel(
                Locator.Current.GetService<IDataService>()));
    })
    .BuildApp();
```

## Platform-Specific Migration

### WPF Migration

```csharp
// BEFORE - App.xaml.cs
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        RxApp.MainThreadScheduler = new DispatcherScheduler(Dispatcher.CurrentDispatcher);
        
        Locator.CurrentMutable.RegisterLazySingleton<IScreen>(() => new MainViewModel());
        Locator.CurrentMutable.Register<IViewFor<MainViewModel>>(() => new MainWindow());
        
        new MainWindow().Show();
    }
}

// AFTER - App.xaml.cs
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var app = RxAppBuilder.CreateReactiveUIBuilder()
            .WithWpf()
            .WithViewsFromAssembly(typeof(App).Assembly)
            .WithRegistration(locator =>
            {
                locator.RegisterLazySingleton<IScreen>(() => new MainViewModel());
            })
            .BuildApp();
        
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}
```

### MAUI Migration

```csharp
// BEFORE - MauiProgram.cs
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        
        // Manual setup scattered
        Locator.CurrentMutable.RegisterLazySingleton<IDataService>(() => new DataService());
        
        return builder.Build();
    }
}

// AFTER - MauiProgram.cs
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        
        // Centralized setup
        var app = RxAppBuilder.CreateReactiveUIBuilder()
            .WithMaui()
            .WithViewsFromAssembly(typeof(App).Assembly)
            .WithRegistration(locator =>
            {
                locator.RegisterLazySingleton<IDataService>(() => new DataService());
                locator.RegisterLazySingleton<INavigationService>(() => new NavigationService());
            })
            .BuildApp();
        
        return builder.Build();
    }
}
```

### Blazor Server Migration

```csharp
// BEFORE - Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Manual ReactiveUI setup
Locator.CurrentMutable.RegisterLazySingleton<IDataService>(() => new DataService());

var app = builder.Build();
// ... configure and run

// AFTER - Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// RxAppBuilder setup
var rxApp = RxAppBuilder.CreateReactiveUIBuilder()
    .WithBlazor()
    .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
    .WithRegistration(locator =>
    {
        locator.RegisterLazySingleton<IDataService>(() => new DataService());
    })
    .BuildApp();

var app = builder.Build();
// ... configure and run
```

## Testing with RxAppBuilder

### Legacy Test Setup

```csharp
[Fact]
public void Test_ViewModel()
{
    // Manual scheduler setup for each test ❌
    var scheduler = new TestScheduler();
    RxApp.MainThreadScheduler = scheduler;
    RxApp.TaskpoolScheduler = scheduler;
    
    // Manual service setup
    Locator.CurrentMutable.Register<IDataService>(() => mockService);
    
    var vm = new MainViewModel();
    // ... test
}
```

### RxAppBuilder Test Setup ✅

```csharp
public class TestFixture : IDisposable
{
    private readonly IResolverScope _scope;
    
    public TestFixture()
    {
        var scheduler = new TestScheduler();
        
        var app = RxAppBuilder.CreateReactiveUIBuilder()
            .WithCustomScheduler(mainThreadScheduler: scheduler)
            .WithCustomScheduler(taskpoolScheduler: scheduler)
            .WithRegistration(locator =>
            {
                locator.RegisterLazySingleton<IDataService>(() => mockService);
            })
            .BuildApp();
        
        _scope = Locator.Current.GetService<IResolverScope>();
    }
    
    public void Dispose()
    {
        _scope?.Dispose();
    }
}

[Fact]
public void Test_ViewModel()
{
    using var fixture = new TestFixture();
    var vm = new MainViewModel();
    // ... test
}
```

## Common Migration Scenarios

### Scenario 1: Existing Large Application

**Strategy**: Incremental migration

```csharp
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // New: RxAppBuilder for new code
        var app = RxAppBuilder.CreateReactiveUIBuilder()
            .WithWpf()
            .WithViewsFromAssembly(typeof(App).Assembly)
            .WithRegistration(locator =>
            {
                // New services here
                locator.RegisterLazySingleton<INewService>(() => new NewService());
            })
            .BuildApp();
        
        // Old: Keep existing registrations until migrated
        Locator.CurrentMutable.RegisterLazySingleton<ILegacyService>(() => new LegacyService());
        
        new MainWindow().Show();
    }
}
```

### Scenario 2: Multiple Projects/Assemblies

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWpf()
    .WithViewsFromAssembly(typeof(App).Assembly)           // UI assembly
    .WithViewsFromAssembly(typeof(SharedView).Assembly)    // Shared assembly
    .WithRegistration(locator =>
    {
        // Register services from multiple assemblies
        RegisterCoreServices(locator);
        RegisterUIServices(locator);
    })
    .BuildApp();

void RegisterCoreServices(IMutableDependencyResolver locator)
{
    locator.RegisterLazySingleton<IDataService>(() => new DataService());
}

void RegisterUIServices(IMutableDependencyResolver locator)
{
    locator.Register<MainViewModel>(() => new MainViewModel());
}
```

### Scenario 3: Dynamic Configuration

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWpf()
    .WithViewsFromAssembly(typeof(App).Assembly)
    .WithRegistration(locator =>
    {
        // Configure based on environment
        if (IsProduction())
        {
            locator.RegisterLazySingleton<IDataService>(() => new ProductionDataService());
        }
        else
        {
            locator.RegisterLazySingleton<IDataService>(() => new DevelopmentDataService());
        }
    })
    .BuildApp();
```

## Migration Checklist

### Phase 1: Preparation
- [ ] Update to ReactiveUI 22.2.1+
- [ ] Review current initialization code
- [ ] Identify all service registrations
- [ ] Document custom scheduler usage

### Phase 2: Implementation
- [ ] Install/update required packages
- [ ] Add RxAppBuilder initialization
- [ ] Migrate service registrations
- [ ] Update view registrations
- [ ] Configure platform-specific settings

### Phase 3: Testing
- [ ] Update unit tests
- [ ] Test application startup
- [ ] Verify all views resolve
- [ ] Validate scheduler behavior
- [ ] Test dependency injection

### Phase 4: Cleanup
- [ ] Remove legacy initialization code
- [ ] Remove manual scheduler setup
- [ ] Clean up scattered registrations
- [ ] Update documentation

## Troubleshooting

### Issue: Views Not Resolving

**Problem**: `IViewFor<TViewModel>` not found

**Solution**: Ensure `WithViewsFromAssembly()` includes all assemblies with views

```csharp
.WithViewsFromAssembly(Assembly.GetExecutingAssembly())
.WithViewsFromAssembly(typeof(SharedView).Assembly)
```

### Issue: Scheduler Not Working

**Problem**: UI updates not happening on main thread

**Solution**: Verify platform-specific method called

```csharp
// Ensure you're using the right platform method
.WithWpf()      // For WPF
.WithMaui()     // For MAUI
.WithBlazor()   // For Blazor
```

### Issue: Services Not Found

**Problem**: `GetService<T>()` returns null

**Solution**: Check registration is in `WithRegistration` block

```csharp
.WithRegistration(locator =>
{
    locator.RegisterLazySingleton<IMyService>(() => new MyService());
})
```

## Benefits After Migration

### Development
- ✅ Clearer application structure
- ✅ Easier onboarding for new developers
- ✅ Better IDE support
- ✅ Compile-time validation

### Testing
- ✅ Simplified test setup
- ✅ Better isolation
- ✅ Easier mocking
- ✅ Consistent test configuration

### Maintenance
- ✅ Single source of truth for configuration
- ✅ Easier to update dependencies
- ✅ Clear dependency graph
- ✅ Better error messages

## Additional Resources

- [RxAppBuilder Documentation](~/docs/handbook/rxappbuilder.md)
- [Guidelines](~/docs/guidelines/index.md)
- [Testing Guide](~/docs/handbook/testing.md)
- [Sample Applications](~/docs/resources/samples.md)

## Getting Help

If you encounter issues during migration:

1. Check [GitHub Discussions](https://github.com/reactiveui/ReactiveUI/discussions)
2. Ask on [Slack](https://join.slack.com/t/reactivex/shared_invite/zt-lt48skpz-G5WDYOAuzA80_MByZrLT0g)
3. Review [Sample Code](https://github.com/reactiveui/ReactiveUI.Samples)

---

**Migration Time Estimate**: 2-8 hours depending on application size
**Difficulty**: Easy to Moderate
**Recommended**: Yes - Modern pattern with better maintainability
