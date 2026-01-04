# Blazor

## Package Installation

Install the following packages for ReactiveUI with Blazor:

```xml
<!-- In your Blazor application project -->
<PackageReference Include="ReactiveUI.Blazor" Version="*" />
<PackageReference Include="ReactiveUI.SourceGenerators" Version="*" PrivateAssets="all" />
<PackageReference Include="ReactiveMarbles.ObservableEvents.SourceGenerator" Version="*" PrivateAssets="all" />

<!-- In your shared .NET Standard library -->
<PackageReference Include="ReactiveUI" Version="*" />
<PackageReference Include="ReactiveUI.SourceGenerators" Version="*" PrivateAssets="all" />

<!-- In your test project -->
<PackageReference Include="ReactiveUI.Testing" Version="*" />
```

### Recommended Project Structure

```
- MyCoolApp (netstandard/net10.0 library - shared code)
- MyCoolApp.Blazor (Blazor application - Server or WebAssembly)
- MyCoolApp.UnitTests (test project)
```

## Getting Started with RxAppBuilder (Recommended)

The modern way to initialize ReactiveUI in Blazor uses **RxAppBuilder** for dependency injection and platform setup.

### 1. Configure Program.cs with RxAppBuilder

**For Blazor Server:**
```csharp
using ReactiveUI;
using Splat;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add Blazor services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Initialize ReactiveUI with RxAppBuilder
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithBlazor()
    .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
    .WithRegistration(locator =>
    {
        // Register your services here
        locator.RegisterLazySingleton<IDataService>(() => new DataService());
        locator.RegisterLazySingleton<INavigationService>(() => new NavigationService());
    })
    .BuildApp();

// Register ReactiveUI schedulers with DI container (optional)
builder.Services.AddSingleton(app.MainThreadScheduler);
builder.Services.AddSingleton(app.TaskpoolScheduler);

var webApp = builder.Build();

webApp.UseStaticFiles();
webApp.UseRouting();
webApp.MapBlazorHub();
webApp.MapFallbackToPage("/_Host");

webApp.Run();
```

**For Blazor WebAssembly:**
```csharp
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReactiveUI;
using Splat;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Initialize ReactiveUI with RxAppBuilder
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithBlazor()
    .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
    .WithRegistration(locator =>
    {
        // Register your services here
        locator.RegisterLazySingleton<IDataService>(() => new DataService());
        locator.RegisterLazySingleton<HttpClient>(() => 
            new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    })
    .BuildApp();

await builder.Build().RunAsync();
```

### 2. Create ViewModels with SourceGenerators

Use ReactiveUI.SourceGenerators for cleaner, compile-time generated reactive properties:

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive.Linq;

namespace MyCoolApp.ViewModels;

public partial class CounterViewModel : ReactiveObject
{
    [Reactive]
    private int _currentCount;

    [Reactive]
    private string _message = "Click the button!";

    [ObservableAsProperty]
    private bool _isEven;

    public CounterViewModel()
    {
        IncrementCommand = ReactiveCommand.Create(IncrementCount);

        // React to count changes
        this.WhenAnyValue(x => x.CurrentCount)
            .Select(count => count % 2 == 0)
            .ToProperty(this, x => x.IsEven);

        this.WhenAnyValue(x => x.CurrentCount)
            .Subscribe(count => Message = $"Current count: {count}");
    }

    [ReactiveCommand]
    private void IncrementCount()
    {
        CurrentCount++;
    }

    public ReactiveCommand<Unit, Unit> IncrementCommand { get; }
}
```

### 3. Create Blazor Components that Use ViewModels

**Counter.razor:**
```razor
@page "/counter"
@using ReactiveUI
@using ReactiveUI.Blazor
@using MyCoolApp.ViewModels
@inherits ReactiveComponentBase<CounterViewModel>

<PageTitle>Counter</PageTitle>

<h1>Counter</h1>

<p role="status">@ViewModel.Message</p>

<button class="btn btn-primary" @onclick="() => ViewModel.IncrementCommand.Execute().Subscribe()">
    Click me
</button>

@if (ViewModel.IsEven)
{
    <p class="text-success">The count is even!</p>
}
else
{
    <p class="text-warning">The count is odd!</p>
}

@code {
    protected override void OnInitialized()
    {
        // Initialize ViewModel
        ViewModel = AppLocator.Current.GetService<CounterViewModel>() ?? new CounterViewModel();
        
        // Set up reactive subscriptions
        this.WhenActivated(disposables =>
        {
            // Subscribe to property changes and trigger StateHasChanged
            this.WhenAnyValue(x => x.ViewModel.CurrentCount)
                .Subscribe(_ => InvokeAsync(StateHasChanged))
                .DisposeWith(disposables);
                
            this.WhenAnyValue(x => x.ViewModel.Message)
                .Subscribe(_ => InvokeAsync(StateHasChanged))
                .DisposeWith(disposables);
                
            this.WhenAnyValue(x => x.ViewModel.IsEven)
                .Subscribe(_ => InvokeAsync(StateHasChanged))
                .DisposeWith(disposables);
        });
        
        base.OnInitialized();
    }
}
```

### 4. Advanced Example with Data Fetching

**FetchDataViewModel.cs:**
```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive.Linq;

namespace MyCoolApp.ViewModels;

public partial class FetchDataViewModel : ReactiveObject
{
    private readonly IWeatherService _weatherService;

    [Reactive]
    private string _statusMessage = "Loading...";

    [ObservableAsProperty]
    private List<WeatherForecast> _forecasts;

    [ObservableAsProperty]
    private bool _isLoading;

    public FetchDataViewModel(IWeatherService weatherService)
    {
        _weatherService = weatherService;

        LoadDataCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            StatusMessage = "Loading weather data...";
            return await _weatherService.GetForecastAsync();
        });

        LoadDataCommand.IsExecuting
            .ToProperty(this, x => x.IsLoading);

        LoadDataCommand
            .Do(_ => StatusMessage = "Data loaded successfully")
            .ToProperty(this, x => x.Forecasts);

        LoadDataCommand.ThrownExceptions
            .Subscribe(ex => StatusMessage = $"Error: {ex.Message}");
    }

    public ReactiveCommand<Unit, List<WeatherForecast>> LoadDataCommand { get; }
}
```

**FetchData.razor:**
```razor
@page "/fetchdata"
@using ReactiveUI
@using ReactiveUI.Blazor
@using MyCoolApp.ViewModels
@inherits ReactiveComponentBase<FetchDataViewModel>

<PageTitle>Weather forecast</PageTitle>

<h1>Weather forecast</h1>

<p>@ViewModel.StatusMessage</p>

@if (ViewModel.IsLoading)
{
    <div class="spinner-border" role="status">
        <span class="visually-hidden">Loading...</span>
    </div>
}
else if (ViewModel.Forecasts != null)
{
    <table class="table">
        <thead>
            <tr>
                <th>Date</th>
                <th>Temp. (C)</th>
                <th>Summary</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var forecast in ViewModel.Forecasts)
            {
                <tr>
                    <td>@forecast.Date.ToShortDateString()</td>
                    <td>@forecast.TemperatureC</td>
                    <td>@forecast.Summary</td>
                </tr>
            }
        </tbody>
    </table>
}

@code {
    protected override void OnInitialized()
    {
        // Resolve ViewModel from DI
        var weatherService = AppLocator.Current.GetService<IWeatherService>();
        ViewModel = new FetchDataViewModel(weatherService);
        
        this.WhenActivated(disposables =>
        {
            // Load data when component is activated
            ViewModel.LoadDataCommand.Execute()
                .Subscribe()
                .DisposeWith(disposables);
                
            // Subscribe to property changes
            this.WhenAnyValue(
                x => x.ViewModel.IsLoading,
                x => x.ViewModel.Forecasts,
                x => x.ViewModel.StatusMessage)
                .Subscribe(_ => InvokeAsync(StateHasChanged))
                .DisposeWith(disposables);
        });
        
        base.OnInitialized();
    }
}
```

## Alternative: Traditional Setup (Legacy)

If you prefer not to use RxAppBuilder, you can initialize ReactiveUI manually:

```csharp
// In Program.cs
AppLocator.CurrentMutable.RegisterLazySingleton<IDataService>(() => new DataService());
AppLocator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
```

## Key Patterns for Blazor

### 1. Always Call StateHasChanged

Blazor doesn't automatically detect ReactiveUI property changes, so you need to manually trigger re-renders:

```csharp
this.WhenAnyValue(x => x.ViewModel.SomeProperty)
    .Subscribe(_ => InvokeAsync(StateHasChanged))
    .DisposeWith(disposables);
```

### 2. Use WhenActivated for Subscriptions

Always wrap subscriptions in `WhenActivated` to prevent memory leaks:

```csharp
this.WhenActivated(disposables =>
{
    // Your subscriptions here
    this.WhenAnyValue(x => x.ViewModel.Data)
        .Subscribe(_ => InvokeAsync(StateHasChanged))
        .DisposeWith(disposables);
});
```

### 3. Command Execution in Blazor

Execute ReactiveCommands in event handlers:

```razor
<button @onclick="() => ViewModel.MyCommand.Execute().Subscribe()">
    Click Me
</button>
```

Or with parameters:

```razor
<button @onclick="() => ViewModel.ProcessCommand.Execute(item).Subscribe()">
    Process Item
</button>
```

### 4. Form Binding

For two-way binding with form inputs:

```razor
<input type="text" 
       value="@ViewModel.SearchText" 
       @oninput="e => ViewModel.SearchText = e.Value?.ToString()" />
```

Or using ReactiveUI binding helpers in code:

```csharp
this.Bind(ViewModel,
    vm => vm.SearchText,
    v => v.SearchInput.Value)
    .DisposeWith(disposables);
```

## Key Points

- **Use ReactiveComponentBase<TViewModel>** as the base class for reactive Blazor components
- **Use ReactiveUI.SourceGenerators** for cleaner property and command declarations
- **Use RxAppBuilder** for modern dependency injection and platform setup
- **Always call InvokeAsync(StateHasChanged)** when reactive properties change
- **Always wrap subscriptions in WhenActivated** and call DisposeWith to prevent memory leaks
- **Execute commands with .Execute().Subscribe()** in Blazor event handlers

## Reactive Validation in Blazor

```csharp
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;

public partial class LoginViewModel : ReactiveValidationObject
{
    [Reactive]
    private string _username = string.Empty;

    [Reactive]
    private string _password = string.Empty;

    public LoginViewModel()
    {
        this.ValidationRule(
            vm => vm.Username,
            username => !string.IsNullOrWhiteSpace(username),
            "Username is required");

        this.ValidationRule(
            vm => vm.Password,
            password => password?.Length >= 6,
            "Password must be at least 6 characters");

        LoginCommand = ReactiveCommand.CreateFromTask(
            async () => await LoginAsync(),
            this.IsValid());
    }
    
    [ReactiveCommand]
    private async Task LoginAsync() { /* ... */ }
}
```

In the component:

```razor
@if (ViewModel.HasErrors)
{
    <div class="alert alert-danger">
        @foreach (var error in ViewModel.ValidationContext.Text)
        {
            <p>@error</p>
        }
    </div>
}

<button class="btn btn-primary" 
        disabled="@(!ViewModel.LoginCommand.CanExecute.FirstAsync().Wait())"
        @onclick="() => ViewModel.LoginCommand.Execute().Subscribe()">
    Login
</button>
```

## Additional Resources

- [Blazor Documentation](https://learn.microsoft.com/aspnet/core/blazor/)
- [ReactiveUI Handbook](~/docs/handbook/index.md)
- [RxAppBuilder Guide](~/docs/handbook/rxappbuilder.md)
- [ReactiveUI.SourceGenerators](https://github.com/reactiveui/ReactiveUI.SourceGenerators)
- [Sample Blazor Apps](~/docs/resources/samples.md)
