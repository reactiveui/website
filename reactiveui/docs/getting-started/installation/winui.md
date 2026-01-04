# WinUI 3

## Package Installation

Install the following packages for ReactiveUI with WinUI 3:

```xml
<!-- In your WinUI 3 application project -->
<PackageReference Include="ReactiveUI.WinUI" Version="*" />
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
- MyCoolApp.WinUI (WinUI 3 application)
- MyCoolApp.UnitTests (test project)
```

## Getting Started with RxAppBuilder (Recommended)

The modern way to initialize ReactiveUI in WinUI 3 uses **RxAppBuilder** for dependency injection and platform setup.

### 1. Configure App.xaml.cs with RxAppBuilder

```csharp
using Microsoft.UI.Xaml;
using ReactiveUI;
using ReactiveUI.WinUI;
using Splat;
using System.Reflection;

namespace MyCoolApp.WinUI;

public partial class App : Application
{
    private Window? _mainWindow;

    public App()
    {
        InitializeComponent();
        
        // Initialize ReactiveUI with RxAppBuilder
        var app = RxAppBuilder.CreateReactiveUIBuilder()
            .WithWinUI()
            .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
            .WithRegistration(locator =>
            {
                // Register your services here
                locator.RegisterLazySingleton<IScreen>(() => new MainViewModel());
                locator.RegisterLazySingleton<INavigationService>(() => new NavigationService());
            })
            .BuildApp();

        // Store schedulers for later use if needed
        // var mainScheduler = app.MainThreadScheduler;
        // var taskScheduler = app.TaskpoolScheduler;
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _mainWindow = new MainWindow();
        _mainWindow.Activate();
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

    public MainViewModel()
    {
        // Create reactive commands
        SearchCommand = ReactiveCommand.CreateFromTask(
            async () => await PerformSearch(),
            this.WhenAnyValue(x => x.SearchText, text => !string.IsNullOrWhiteSpace(text)));

        // Wire up IsBusy from command execution
        SearchCommand.IsExecuting
            .ToProperty(this, x => x.IsBusy);

        // React to search text changes
        this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(500))
            .Where(text => !string.IsNullOrWhiteSpace(text))
            .InvokeCommand(SearchCommand);
    }

    [ReactiveCommand]
    private async Task PerformSearch()
    {
        StatusMessage = "Searching...";
        await Task.Delay(1000); // Simulate search
        StatusMessage = $"Found results for: {SearchText}";
    }

    public ReactiveCommand<Unit, Unit> SearchCommand { get; }
}
```

### 3. Create Views that Implement IViewFor

**MainWindow.xaml:**
```xml
<rxui:ReactiveWindow
    x:Class="MyCoolApp.WinUI.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:rxui="using:ReactiveUI.WinUI"
    xmlns:vm="using:MyCoolApp.ViewModels"
    x:TypeArguments="vm:MainViewModel"
    Title="My Cool App">
    
    <Grid Padding="20">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        
        <TextBox x:Name="SearchTextBox" 
                 Grid.Row="0"
                 PlaceholderText="Search..." 
                 Margin="0,0,0,10"/>
        
        <ProgressRing x:Name="BusyIndicator" 
                      Grid.Row="1"
                      Margin="0,0,0,10"/>
        
        <TextBlock x:Name="StatusTextBlock" 
                   Grid.Row="2"
                   TextWrapping="Wrap"/>
    </Grid>
</rxui:ReactiveWindow>
```

**MainWindow.xaml.cs:**
```csharp
using Microsoft.UI.Xaml;
using ReactiveUI;
using ReactiveUI.WinUI;
using Splat;

namespace MyCoolApp.WinUI;

public sealed partial class MainWindow : ReactiveWindow<MainViewModel>
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
                v => v.BusyIndicator.IsActive)
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
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        // Register views manually
        Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<MainViewModel>));
        
        // Register view models
        Locator.CurrentMutable.RegisterLazySingleton(() => new MainViewModel(), typeof(MainViewModel));
    }
}
```

## Creating UserControls

For reusable components, use `ReactiveUserControl<TViewModel>`:

**MyControl.xaml:**
```xml
<rxui:ReactiveUserControl
    x:Class="MyCoolApp.WinUI.Controls.MyControl"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:rxui="using:ReactiveUI.WinUI"
    xmlns:vm="using:MyCoolApp.ViewModels"
    x:TypeArguments="vm:MyControlViewModel">
    
    <StackPanel>
        <TextBlock x:Name="TitleTextBlock" FontSize="20"/>
        <Button x:Name="ActionButton" Content="Click Me"/>
    </StackPanel>
</rxui:ReactiveUserControl>
```

**MyControl.xaml.cs:**
```csharp
using ReactiveUI;
using ReactiveUI.WinUI;

namespace MyCoolApp.WinUI.Controls;

public sealed partial class MyControl : ReactiveUserControl<MyControlViewModel>
{
    public MyControl()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                vm => vm.Title,
                v => v.TitleTextBlock.Text)
                .DisposeWith(disposables);
                
            this.BindCommand(ViewModel,
                vm => vm.ActionCommand,
                v => v.ActionButton)
                .DisposeWith(disposables);
        });
    }
}
```

## Dependency Injection with RxAppBuilder

RxAppBuilder integrates with Splat for dependency injection:

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWinUI()
    .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
    .WithRegistration(locator =>
    {
        // Register services
        locator.RegisterLazySingleton<IApiService>(() => new ApiService());
        locator.RegisterLazySingleton<IDataRepository>(() => new DataRepository());
        
        // Register view models
        locator.Register<MainViewModel>(() => new MainViewModel());
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

## Key Points

- **Use ReactiveWindow<TViewModel>** or **ReactiveUserControl<TViewModel>** base classes
- **Use ReactiveUI.SourceGenerators** for cleaner property and command declarations
- **Use RxAppBuilder** for modern dependency injection and platform setup
- **Always call DisposeWith(disposables)** inside WhenActivated to prevent memory leaks
- **Use ReactiveMarbles.ObservableEvents.SourceGenerator** for converting WinUI events to observables

## Common Patterns

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
    }
}
```

## Additional Resources

- [WinUI 3 Documentation](https://learn.microsoft.com/windows/apps/winui/)
- [ReactiveUI Handbook](~/docs/handbook/index.md)
- [RxAppBuilder Guide](~/docs/handbook/rxappbuilder.md)
- [ReactiveUI.SourceGenerators](https://github.com/reactiveui/ReactiveUI.SourceGenerators)
- [Sample WinUI Apps](~/docs/resources/samples.md)

