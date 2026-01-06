# Avalonia

<div class="youtube-video-container"><iframe src="https://www.youtube.com/embed/q6uWPtKw3UQ" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>

## Package Installation

Install the following packages for ReactiveUI with Avalonia:

```xml
<!-- In your Avalonia application project -->
<PackageReference Include="ReactiveUI.Avalonia" Version="11.3.8" />
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
- MyCoolApp.Avalonia (Avalonia application)
- MyCoolApp.UnitTests (test project)
```

## Getting Started with Avalonia and ReactiveUI

The modern way to initialize ReactiveUI with Avalonia uses the `.UseReactiveUI()` extension method in your application builder.

### 1. Configure Program.cs or App.axaml.cs

Use RxAppBuilder for service registration and view configuration.
In your `Program.cs` file, add `.UseReactiveUI()` to the AppBuilder:

```csharp
using Avalonia;
using Avalonia.ReactiveUI;

namespace MyCoolApp.Avalonia;

class Program
{
    public static void Main(string[] args)
    {
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI(rxAppBuilder =>
            { 
              // Enable ReactiveUI
              rxAppBuilder
                .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
                .WithRegistration(locator =>
                {
                    // Register your services here
                    locator.RegisterLazySingleton<IScreen>(() => new MainViewModel());
                    locator.RegisterLazySingleton<INavigationService>(() => new NavigationService());
                });
            }).RegisterReactiveUIViewsFromEntryAssembly();
}
```

### 2. Configure the Startup View in App.axaml.cs


```csharp
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Splat;
using System.Reflection;

namespace MyCoolApp.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = AppLocator.Current.GetService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
```

### 3. Create ViewModels with SourceGenerators

Use ReactiveUI.SourceGenerators for cleaner, compile-time generated reactive properties:

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive.Linq;

namespace MyCoolApp.ViewModels;

public partial class MainViewModel : ReactiveObject
{
    [Reactive]
    private string _searchText = string.Empty;

    [Reactive]
    private string _statusMessage = string.Empty;

    [ObservableAsProperty]
    private bool _isBusy;

    [ObservableAsProperty]
    private List<SearchResult> _searchResults;

    public MainViewModel()
    {
        // Create reactive commands with automatic CanExecute
        SearchCommand = ReactiveCommand.CreateFromTask(
            async () => await PerformSearchAsync(),
            this.WhenAnyValue(x => x.SearchText, text => !string.IsNullOrWhiteSpace(text)));

        // Wire up IsBusy from command execution
        SearchCommand.IsExecuting
            .ToProperty(this, x => x.IsBusy);

        // Wire up search results
        SearchCommand
            .ToProperty(this, x => x.SearchResults);

        // React to search text changes with debouncing
        this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(500))
            .Where(text => !string.IsNullOrWhiteSpace(text))
            .InvokeCommand(SearchCommand);

        // Handle errors
        SearchCommand.ThrownExceptions
            .Subscribe(ex => StatusMessage = $"Error: {ex.Message}");
    }

    [ReactiveCommand]
    private async Task<List<SearchResult>> PerformSearchAsync()
    {
        StatusMessage = "Searching...";
        await Task.Delay(1000); // Simulate search
        StatusMessage = "Search complete";
        return new List<SearchResult>();
    }

    public ReactiveCommand<Unit, List<SearchResult>> SearchCommand { get; }
}
```

### 4. Create Views that Implement IViewFor

**MainWindow.axaml:**
```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:vm="using:MyCoolApp.ViewModels"
        x:Class="MyCoolApp.Avalonia.Views.MainWindow"
        x:DataType="vm:MainViewModel"
        Title="My Cool App"
        Width="800"
        Height="600">
    
    <StackPanel Margin="20" Spacing="10">
        <TextBox x:Name="SearchTextBox" 
                 Watermark="Search..." />
        
        <ProgressBar x:Name="BusyIndicator"
                     IsIndeterminate="True"
                     Height="4"/>
        
        <ListBox x:Name="SearchResultsListBox"
                 Height="400"/>
        
        <TextBlock x:Name="StatusTextBlock"/>
    </StackPanel>
</Window>
```

**MainWindow.axaml.cs:**
```csharp
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;

namespace MyCoolApp.Avalonia.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        // Resolve ViewModel from DI container or create directly
        ViewModel = AppLocator.Current.GetService<MainViewModel>() ?? new MainViewModel();

        this.WhenActivated(disposables =>
        {
            // Two-way binding for search text
            this.Bind(ViewModel,
                vm => vm.SearchText,
                v => v.SearchTextBox.Text)
                .DisposeWith(disposables);

            // One-way binding for busy indicator
            this.OneWayBind(ViewModel,
                vm => vm.IsBusy,
                v => v.BusyIndicator.IsVisible)
                .DisposeWith(disposables);

            // One-way binding for search results
            this.OneWayBind(ViewModel,
                vm => vm.SearchResults,
                v => v.SearchResultsListBox.ItemsSource)
                .DisposeWith(disposables);

            // One-way binding for status message
            this.OneWayBind(ViewModel,
                vm => vm.StatusMessage,
                v => v.StatusTextBlock.Text)
                .DisposeWith(disposables);
        });
    }
}
```

## Alternative: Traditional Setup (Legacy)

If you prefer not to use RxAppBuilder, you can initialize ReactiveUI manually:

```csharp
public override void OnFrameworkInitializationCompleted()
{
    // Register views manually
    AppLocator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<MainViewModel>));
    
    // Register view models
    AppLocator.CurrentMutable.RegisterLazySingleton(() => new MainViewModel(), typeof(MainViewModel));
    
    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
        desktop.MainWindow = new MainWindow();
    }

    base.OnFrameworkInitializationCompleted();
}
```

## Creating UserControls

For reusable components, use `ReactiveUserControl<TViewModel>`:

**SearchControl.axaml:**
```xml
<UserControl xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:vm="using:MyCoolApp.ViewModels"
             x:Class="MyCoolApp.Avalonia.Controls.SearchControl"
             x:DataType="vm:SearchControlViewModel">
    
    <StackPanel Spacing="5">
        <TextBox x:Name="QueryTextBox" Watermark="Enter query"/>
        <Button x:Name="SearchButton" Content="Search"/>
    </StackPanel>
</UserControl>
```

**SearchControl.axaml.cs:**
```csharp
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace MyCoolApp.Avalonia.Controls;

public partial class SearchControl : ReactiveUserControl<SearchControlViewModel>
{
    public SearchControl()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel,
                vm => vm.Query,
                v => v.QueryTextBox.Text)
                .DisposeWith(disposables);
                
            this.BindCommand(ViewModel,
                vm => vm.SearchCommand,
                v => v.SearchButton)
                .DisposeWith(disposables);
        });
    }
}
```

## Routing in Avalonia

For navigation, use ReactiveUI's routing system with `RoutedViewHost`:

```csharp
public class MainViewModel : ReactiveObject, IScreen
{
    public RoutingState Router { get; }

    public MainViewModel()
    {
        Router = new RoutingState();
        
        // Navigate to the first page
        Router.Navigate.Execute(new FirstViewModel(this)).Subscribe();
    }
}
```

In your AXAML:
```xml
<rxui:RoutedViewHost Router="{Binding Router"
                      HorizontalAlignment="Stretch"
                      VerticalAlignment="Stretch"
                      xmlns:rxui="http://reactiveui.net"/>
```

See the [Routing Guide](~/docs/handbook/routing.md) for more details.

## Key Points

- **Use ReactiveWindow<TViewModel>** or **ReactiveUserControl<TViewModel>** base classes
- **Use ReactiveUI.SourceGenerators** for cleaner property and command declarations
- **Call .UseReactiveUI()** in your AppBuilder to enable ReactiveUI support
- **Use RxAppBuilder** for modern dependency injection and service registration
- **Always call DisposeWith(disposables)** inside WhenActivated to prevent memory leaks
- **Use ReactiveMarbles.ObservableEvents.SourceGenerator** for converting Avalonia events to observables

## Common Patterns

### Value Converters in Bindings

```csharp
this.OneWayBind(ViewModel,
    vm => vm.IsEnabled,
    v => v.MyButton.IsVisible,
    isEnabled => isEnabled)
    .DisposeWith(disposables);
```

### Reactive Validation

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

### Loading Data on Activation

```csharp
public MainViewModel()
{
    this.WhenActivated(disposables =>
    {
        // Load data when the view model is activated
        LoadDataCommand.Execute().Subscribe().DisposeWith(disposables);
    });
}
```

## Additional Resources

- [Avalonia Documentation](https://docs.avaloniaui.net/)
- [ReactiveUI Avalonia Guide](https://docs.avaloniaui.net/docs/guides/development-guides/reactiveui)
- [ReactiveUI Handbook](~/docs/handbook/index.md)
- [RxAppBuilder Guide](~/docs/handbook/rxappbuilder.md)
- [ReactiveUI.SourceGenerators](https://github.com/reactiveui/ReactiveUI.SourceGenerators)
- [Sample Avalonia Apps](~/docs/resources/samples.md)
