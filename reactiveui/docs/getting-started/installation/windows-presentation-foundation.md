# Windows Presentation Foundation (WPF)

## Package Installation

Install the following packages for ReactiveUI with WPF:

```xml
<!-- In your WPF application project -->
<PackageReference Include="ReactiveUI.WPF" Version="*" />
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
- MyCoolApp (netstandard/net8.0 library - shared code)
- MyCoolApp.WPF (WPF application)
- MyCoolApp.UnitTests (test project)
```

### Framework Requirements

Ensure you are targeting at least .NET 8.0 with Windows 10.0.19041.0:

```xml
<TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
<!-- Or for .NET 9/10 -->
<TargetFramework>net10.0-windows10.0.19041.0</TargetFramework>
```

## Getting Started with RxAppBuilder (Recommended)

The modern way to initialize ReactiveUI in WPF uses **RxAppBuilder** for dependency injection and platform setup.

### 1. Configure App.xaml.cs with RxAppBuilder

```csharp
using ReactiveUI;
using Splat;
using System.Reflection;
using System.Windows;

namespace MyCoolApp.WPF;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Initialize ReactiveUI with RxAppBuilder
        var app = RxAppBuilder.CreateReactiveUIBuilder()
            .WithWpf()
            .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
            .WithRegistration(locator =>
            {
                // Register your services here
                locator.RegisterLazySingleton<IScreen>(() => new MainViewModel());
                locator.RegisterLazySingleton<INavigationService>(() => new NavigationService());
                locator.RegisterLazySingleton<IDataService>(() => new DataService());
            })
            .BuildApp();

        // Optional: Store schedulers for later use
        // var mainScheduler = app.MainThreadScheduler;
        // var taskScheduler = app.TaskpoolScheduler;

        // Create and show main window
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}
```

### 2. Create ViewModels with SourceGenerators

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

        ClearCommand = ReactiveCommand.Create(
            () => SearchText = string.Empty);

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
        
        // Simulate search operation
        await Task.Delay(1000);
        
        var results = new List<SearchResult>
        {
            new() { Title = SearchText, Description = "Sample result" }
        };
        
        StatusMessage = $"Found {results.Count} results";
        return results;
    }

    public ReactiveCommand<Unit, List<SearchResult>> SearchCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearCommand { get; }
}
```

### 3. Create Views that Implement IViewFor

**MainWindow.xaml:**
```xml
<rxui:ReactiveWindow 
    x:Class="MyCoolApp.WPF.MainWindow"
    x:TypeArguments="vm:MainViewModel"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:rxui="http://reactiveui.net"
    xmlns:vm="clr-namespace:MyCoolApp.ViewModels;assembly=MyCoolApp"
    Title="My Cool App" 
    Height="450" 
    Width="800">
    
    <Grid Margin="20">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        
        <!-- Search Input -->
        <DockPanel Grid.Row="0" Margin="0,0,0,10">
            <Button x:Name="ClearButton" 
                    DockPanel.Dock="Right" 
                    Content="Clear" 
                    Width="75" 
                    Margin="5,0,0,0"/>
            <TextBox x:Name="SearchTextBox" 
                     DockPanel.Dock="Left"/>
        </DockPanel>
        
        <!-- Busy Indicator -->
        <StackPanel Grid.Row="1" 
                    Orientation="Horizontal" 
                    Margin="0,0,0,10"
                    x:Name="BusyPanel">
            <TextBlock Text="Searching..." Margin="0,0,10,0"/>
            <ProgressBar Width="100" Height="20" IsIndeterminate="True"/>
        </StackPanel>
        
        <!-- Results List -->
        <ListBox x:Name="ResultsListBox" 
                 Grid.Row="2" 
                 Margin="0,0,0,10"/>
        
        <!-- Status Bar -->
        <TextBlock x:Name="StatusTextBlock" 
                   Grid.Row="3"/>
    </Grid>
</rxui:ReactiveWindow>
```

**MainWindow.xaml.cs:**
```csharp
using ReactiveUI;
using Splat;
using System.Reactive.Disposables;

namespace MyCoolApp.WPF;

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

            // Command bindings
            this.BindCommand(ViewModel,
                vm => vm.ClearCommand,
                v => v.ClearButton)
                .DisposeWith(disposables);

            // One-way bindings
            this.OneWayBind(ViewModel,
                vm => vm.IsBusy,
                v => v.BusyPanel.Visibility,
                isBusy => isBusy ? Visibility.Visible : Visibility.Collapsed)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                vm => vm.SearchResults,
                v => v.ResultsListBox.ItemsSource)
                .DisposeWith(disposables);

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
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // Register views manually
        Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<MainViewModel>));
        
        // Register view models
        Locator.CurrentMutable.RegisterLazySingleton(() => new MainViewModel(), typeof(MainViewModel));
        
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}
```

## Creating UserControls

For reusable components, use `ReactiveUserControl<TViewModel>`:

**SearchControl.xaml:**
```xml
<rxui:ReactiveUserControl
    x:Class="MyCoolApp.WPF.Controls.SearchControl"
    x:TypeArguments="vm:SearchControlViewModel"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:rxui="http://reactiveui.net"
    xmlns:vm="clr-namespace:MyCoolApp.ViewModels;assembly=MyCoolApp">
    
    <StackPanel>
        <TextBox x:Name="QueryTextBox" Margin="0,0,0,5"/>
        <Button x:Name="SearchButton" Content="Search"/>
    </StackPanel>
</rxui:ReactiveUserControl>
```

**SearchControl.xaml.cs:**
```csharp
using ReactiveUI;

namespace MyCoolApp.WPF.Controls;

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

## Dependency Injection with RxAppBuilder

RxAppBuilder integrates with Splat for dependency injection:

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWpf()
    .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
    .WithRegistration(locator =>
    {
        // Register services as singletons
        locator.RegisterLazySingleton<IApiService>(() => new ApiService());
        locator.RegisterLazySingleton<IDataRepository>(() => new DataRepository());
        
        // Register view models (transient)
        locator.Register<MainViewModel>(() => new MainViewModel());
        
        // Register view models as singletons
        locator.RegisterLazySingleton<SettingsViewModel>(() => new SettingsViewModel());
    })
    .BuildApp();
```

Then resolve services in your view models:

```csharp
public MainViewModel()
{
    var apiService = AppLocator.Current.GetService<IApiService>();
    var repository = AppLocator.Current.GetService<IDataRepository>();
}
```

## Routing in WPF

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

In your XAML:
```xml
<rxui:RoutedViewHost 
    x:Name="RoutedViewHost" 
    Router="{Binding Router}"
    HorizontalContentAlignment="Stretch"
    VerticalContentAlignment="Stretch"/>
```

See the [Routing Guide](~/docs/handbook/routing.md) for more details.

## Key Points

- **Use ReactiveWindow<TViewModel>** or **ReactiveUserControl<TViewModel>** base classes
- **Use ReactiveUI.SourceGenerators** for cleaner property and command declarations
- **Use RxAppBuilder** for modern dependency injection and platform setup
- **Always call DisposeWith(disposables)** inside WhenActivated to prevent memory leaks
- **Use ReactiveMarbles.ObservableEvents.SourceGenerator** for converting WPF events to observables

## Common Patterns

### Value Converters in Bindings

```csharp
this.OneWayBind(ViewModel,
    vm => vm.IsEnabled,
    v => v.MyButton.Visibility,
    isEnabled => isEnabled ? Visibility.Visible : Visibility.Collapsed)
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

- [WPF Documentation](https://learn.microsoft.com/dotnet/desktop/wpf/)
- [ReactiveUI Handbook](~/docs/handbook/index.md)
- [RxAppBuilder Guide](~/docs/handbook/rxappbuilder.md)
- [ReactiveUI.SourceGenerators](https://github.com/reactiveui/ReactiveUI.SourceGenerators)
- [Sample WPF Apps](~/docs/resources/samples.md)
- [Compelling Example](~/docs/getting-started/compelling-example.md)
