# RxAppBuilder in ReactiveUI - introduced in V21.0.1

The `RxAppBuilder` class in ReactiveUI is responsible for initializing and configuring the ReactiveUI framework within your application.
It provides a fluent API to set up various components and services that ReactiveUI relies on.
It extends the capabilities of the Splat Builder and Splat AppLocator to register platform-specific services, views, view models, and other dependencies required for ReactiveUI to function correctly.

## Key Features of RxAppBuilder
- **Fluent Configuration**: The `RxAppBuilder` class allows you to chain configuration methods to set up your application in a readable and maintainable way.
- **Service Registration**: You can register services and dependencies that your application will use, such as view models, views, and other services.
- **Platform Integration**: It provides methods to integrate ReactiveUI with different platforms, ensuring that the framework works seamlessly across various environments.
- **Customization**: You can customize the behavior of ReactiveUI by configuring options and settings that suit your application's needs.
- **Initialization**: The `RxAppBuilder` class includes methods to initialize the ReactiveUI framework, ensuring that all necessary components are set up before your application starts.

## Example Usage
```csharp
var rxuiInstance = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWpf() // Register WPF platform services
    .WithViewsFromAssembly(typeof(App).Assembly) // Register views and view models
    .WithRegistration(locator =>
    {
        // Register IScreen as a singleton so all resolutions share the same Router
        locator.RegisterLazySingleton<IScreen>(static () => new AppBootstrapper());
    })
    .BuildApp();
    
var mainUIThreadScheduler = rxuiInstance.MainThreadScheduler;
var taskpoolScheduler = rxuiInstance.TaskpoolScheduler;
```

## Platform Support
The `RxAppBuilder` class supports various platforms, including WPF, Xamarin.Forms, and others. You can use platform-specific methods to ensure that ReactiveUI is properly configured for the target environment.

ReactiveUI.AndroidX - `WithAndroidX()`
ReactiveUI.WPF - `WithWpf()`
ReactiveUI.Maui - `WithMaui()`
ReactiveUI.Blazor - `WithBlazor()`
ReactiveUI.WinForms - `WithWinForms()`
ReactiveUI.WinUI - `WithWinUI()`

## Registering Views and ViewModels
You can register views and view models from specific assemblies using the `WithViewsFromAssembly` method. This allows ReactiveUI to automatically discover and register your views and view models.
```csharp
.WithViewsFromAssembly(typeof(App).Assembly)
```
You can also register individual views and view models using the `WithView` and `WithViewModel` methods.
```csharp
.RegisterView<MainView, MainViewModel>()
.RegisterSingletonView<SettingsView, SettingsViewModel>()
.RegisterViewModel<SettingsViewModel>()
.RegisterSingletonViewModel<SettingsViewModel>()
```

## Custom Service Registration
You can register custom services and dependencies using the `WithPlatformModule` method. This allows you to integrate your own platform-specific services into the ReactiveUI framework. Where T is a class that implements IWantsToRegisterStuff.
```csharp
.WithPlatformModule<Services.MyPlatformModule>()
```
## Configure Schedulers
You can configure the main thread and task pool schedulers used by ReactiveUI using the `WithMainThreadScheduler` and `WithTaskpoolScheduler` methods.
```csharp
.WithMainThreadScheduler(DispatcherScheduler.Current)
.WithTaskpoolScheduler(TaskPoolScheduler.Default)
```

The default scheduler extensions register the RxSchedulers.MainThreadScheduler and RxSchedulers.TaskpoolScheduler to use the appropriate schedulers for the platform.
This can be overridden by calling the above methods with `setRxApp = false` to prevent setting the RxApp static properties.

```csharp
.WithMainThreadScheduler(DispatcherScheduler.Current, setRxApp: false)
.WithTaskpoolScheduler(TaskPoolScheduler.Default, setRxApp: false)
```

Each platform module also registers the appropriate default schedulers for that platform.
Custom schedulers can be provided as needed.
Each platform can be custom configured as needed.
By default the platform registers the appropriate `Schedulers` for that platform, the relevant `PlatformServices`, and the relevant `PlatformModule`.

## Registration Options
The `RxAppBuilder` class provides various options for registering views, view models, and services. You can choose to register them as singletons or transient instances based on your application's requirements.

One option is to use an immediate registration approach, where services are registered directly within the builder configuration.
```csharp
.WithRegistration(locator =>
{
    // Register IScreen as a singleton so all resolutions share the same Router, this is applied immediately
    locator.RegisterLazySingleton<IScreen>(static () => new AppBootstrapper());
})
```
Another option is to use a deferred registration approach, where services are registered as the builder is built.
```csharp
.WithRegistrationOnBuild(locator =>
{
    // Register IScreen as a singleton so all resolutions share the same Router, this is applied after BuildApp is called
    locator.RegisterLazySingleton<IScreen>(static () => new AppBootstrapper());
})
```

## Using Splat Modules
You can also use Splat modules to register services and dependencies. This allows you to leverage existing Splat modules or create your own for better organization.
```csharp
.WithSplatModule(new MyCustomSplatModule())
.WithSplatModule<Services.MySplatModule>()
```

## Building the App
Once you have configured the `RxAppBuilder`, you can build the ReactiveUI application instance using the `BuildApp` method. This finalizes the configuration and prepares the application for use.
```csharp
var rxuiInstance = rxAppBuilder.BuildApp();
var mainUIThreadScheduler = rxuiInstance.MainThreadScheduler;
var taskpoolScheduler = rxuiInstance.TaskpoolScheduler;
```

## WithInstance usage
This allows you to get instances from the underlying Splat service locator.
```csharp
rxuiInstance.WithInstance<IScreen>(screen =>
{    // Use the registered IScreen instance for other services that depend on it.
    screen.Router.Navigate.Execute(new MainViewModel(screen));
})
```

Up to 16 registered instances from Splat.AppLocator can be provided in a single WithInstance call.
```csharp
rxuiInstance.WithInstance<IScreen, IDataService, ILogger>((screen, dataService, logger) =>
{
    // Use the registered instances for other services that depend on them.
})
```
