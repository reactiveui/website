# .NET MAUI

## Package Installation

Install the following packages into your MAUI application:
- **MyApp.Maui**: ReactiveUI.Maui
- **Shared core library**: ReactiveUI, ReactiveUI.SourceGenerators
- **Test project**: ReactiveUI.Testing

```xml
<PackageReference Include="ReactiveUI.Maui" Version="*" />
<PackageReference Include="ReactiveUI.SourceGenerators" Version="*" PrivateAssets="all" />
```

## Getting Started with RxAppBuilder (Recommended)

The modern way to initialize ReactiveUI in MAUI uses **RxAppBuilder** for dependency injection and platform setup:

### 1. Configure MauiProgram with RxAppBuilder

```csharp
using ReactiveUI;
using ReactiveUI.Maui;
using Splat;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureReactiveUI();

        // Initialize ReactiveUI with RxAppBuilder
        var app = RxAppBuilder.CreateReactiveUIBuilder()
            .WithMaui()
            .WithViewsFromAssembly(typeof(App).Assembly)
            .WithRegistration(locator =>
            {
                // Register your services
                locator.RegisterLazySingleton<INavigationService>(() => new NavigationService());
                locator.RegisterLazySingleton<IDataService>(() => new DataService());
            })
            .BuildApp();

        // Optional: Register additional MAUI-specific services
        builder.Services.AddSingleton(app.MainThreadScheduler);
        builder.Services.AddSingleton(app.TaskpoolScheduler);

        return builder.Build();
    }
}
```

### 2. Create ViewModels with SourceGenerators

Use ReactiveUI.SourceGenerators for cleaner, compile-time generated reactive properties:

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class MainViewModel : ReactiveObject
{
    [Reactive]
    private string _name = string.Empty;

    [ReactiveCommand]
    private void SayHello() => Console.WriteLine($"Hello {Name}");
    
    [ReactiveCommand]
    private async Task LoadData()
    {
        // Async operations are supported
        await Task.Delay(1000);
    }
}
```

### 3. Create Views that Implement IViewFor

```xml
<rxui:ReactiveContentPage 
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:rxui="clr-namespace:ReactiveUI.Maui;assembly=ReactiveUI.Maui"
    xmlns:vm="clr-namespace:MyApp.ViewModels"
    x:Class="MyApp.Views.MainPage"
    x:TypeArguments="vm:MainViewModel">
    <VerticalStackLayout Padding="20" Spacing="10">
        <Label Text="Enter your name:" />
        <Entry x:Name="NameEntry" Placeholder="Your name" />
        <Button x:Name="HelloButton" Text="Say Hello" />
        <Button x:Name="LoadButton" Text="Load Data" />
    </VerticalStackLayout>
</rxui:ReactiveContentPage>
```

### 4. Wire up Bindings in Code-Behind

```csharp
using ReactiveUI;
using ReactiveUI.Maui;

namespace MyApp.Views;

public partial class MainPage : ReactiveContentPage<MainViewModel>
{
    public MainPage()
    {
        InitializeComponent();
        
        // ViewModel can be resolved from DI container or created directly
        ViewModel = AppLocator.Current.GetService<MainViewModel>() ?? new MainViewModel();

        this.WhenActivated(disposables =>
        {
            // Two-way binding for Entry
            this.Bind(ViewModel, 
                vm => vm.Name, 
                v => v.NameEntry.Text)
                .DisposeWith(disposables);

            // Command binding for buttons
            this.BindCommand(ViewModel, 
                vm => vm.SayHelloCommand, 
                v => v.HelloButton)
                .DisposeWith(disposables);
                
            this.BindCommand(ViewModel, 
                vm => vm.LoadDataCommand, 
                v => v.LoadButton)
                .DisposeWith(disposables);
        });
    }
}
```

## Alternative: Traditional Setup (Legacy)

If you prefer not to use RxAppBuilder, you can initialize ReactiveUI manually:

```csharp
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        // Register views and view models manually
        AppLocator.CurrentMutable.Register(() => new MainPage(), typeof(IViewFor<MainViewModel>));
        
        // Optional: Register ILogger implementation
        AppLocator.CurrentMutable.RegisterConstant(new SplatLogger(), typeof(ILogger));

        return builder.Build();
    }
}
```

## Routing in MAUI

For navigation, you can use ReactiveUI's routing system or MAUI's built-in Shell navigation. Here's an example using ReactiveUI routing:

```csharp
public class AppViewModel : ReactiveObject, IScreen
{
    public RoutingState Router { get; }

    public AppViewModel()
    {
        Router = new RoutingState();
        
        // Navigate to the first page
        Router.Navigate.Execute(new MainViewModel()).Subscribe();
    }
}
```

## Key Points

- **Use ReactiveContentPage<TViewModel>** base class to enable WhenActivated and reactive bindings
- **Use ReactiveUI.SourceGenerators** for cleaner property and command declarations
- **Use RxAppBuilder** for modern dependency injection and platform setup
- **Always call DisposeWith(disposables)** inside WhenActivated to prevent memory leaks
- **Use IScreen and RoutingState** for view model-based navigation

## Additional Resources

- [MAUI Documentation](https://learn.microsoft.com/dotnet/maui/)
- [ReactiveUI Handbook](~/docs/handbook/index.md)
- [RxAppBuilder Guide](~/docs/handbook/rxappbuilder.md)
- [ReactiveUI.SourceGenerators](https://github.com/reactiveui/ReactiveUI.SourceGenerators)
- [Sample MAUI Apps](~/docs/resources/samples.md)
