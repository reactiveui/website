---
Order: 3
---
# Migration Guide: RxAppBuilder

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/rxappbuilder/rxappbuilder.csproj).

Older ReactiveUI code set up an app by assigning static properties on `RxApp` and registering services one by one
on Splat's `Locator`. Both are gone. `RxAppBuilder` replaces them: a fluent builder that configures schedulers,
services, views and view models in one place, then applies everything with a single `BuildApp()` call. This guide
maps the old calls to the new ones. [RxAppBuilder](../handbook/rxappbuilder.md) covers the builder in full, and
[Registration](../handbook/registration.md) covers the dependency resolver and the lifetimes a service can have.

## What moved where

| Old | New |
|---|---|
| `RxApp.MainThreadScheduler`, `RxApp.TaskpoolScheduler` | `RxSchedulers.MainThreadScheduler`, `RxSchedulers.TaskpoolScheduler`, or `.WithMainThreadScheduler(...)` / `.WithTaskPoolScheduler(...)` on the builder |
| `RxApp.EnsureInitialized()` | `RxAppBuilder.EnsureInitialized()`, called any time after `BuildApp()` has run |
| `RxApp.DefaultExceptionHandler` | `RxState.DefaultExceptionHandler`, set with `.WithExceptionHandler(...)` |
| `Locator.CurrentMutable`, `Locator.Current` | `AppLocator.CurrentMutable`, `AppLocator.Current` (Splat) |
| `Locator.CurrentMutable.Register<IViewFor<T>>(...)` one by one | `.RegisterView<TView, TViewModel>()`, `.RegisterSingletonView<TView, TViewModel>()`, or `.WithViewsFromAssembly(...)` to scan a whole assembly |
| `Locator.CurrentMutable.RegisterLazySingleton<T>(...)` for a view model | `.RegisterViewModel<TViewModel>()`, `.RegisterSingletonViewModel<TViewModel>()`, `.RegisterConstantViewModel<TViewModel>()`, or `.WithRegistration(resolver => ...)` for anything else |
| Manual `DispatcherScheduler`, platform detection code | A platform extension such as `.WithWpf()`, `.WithMaui()`, `.WithBlazor()`, `.WithBlazorWasm()` |

## Start the app

**Before**

```csharp
public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        RxApp.MainThreadScheduler = new DispatcherScheduler(Dispatcher.CurrentDispatcher);

        Locator.CurrentMutable.RegisterLazySingleton<IScreen>(() => new MainViewModel());
        Locator.CurrentMutable.Register<IViewFor<MainViewModel>>(() => new MainWindow());

        MainWindow = new MainWindow();
    }
}
```

**After**

A WPF app's `OnStartup` calls `WithWpf()`. It registers WPF's binding converters, points the main-thread scheduler
at the dispatcher, and applies the task-pool scheduler. `BuildApp()` then applies it all:

```csharp
_ = RxAppBuilder.CreateReactiveUIBuilder().WithWpf().BuildApp();
```

`CreateReactiveUIBuilder()` builds against Splat's own resolver, the one `AppLocator` reads from by default.
MAUI, Blazor Server and Blazor WebAssembly have the same shape: `.WithMaui()`, `.WithBlazor()`, and
`.WithBlazorWasm()` each configure that platform's registrations module and its scheduler:

```csharp
IReactiveUIBuilder builder = RxAppBuilder.CreateReactiveUIBuilder()
    .WithMaui(new ImmediateDispatcher());

Console.WriteLine(builder is not null);

// Output:
// True
```

```csharp
ReactiveUIBuilder builder = RxAppBuilder.CreateReactiveUIBuilder();

_ = builder.WithBlazor();

Console.WriteLine(ReferenceEquals(builder.MainThreadScheduler, BlazorReactiveUIBuilderExtensions.BlazorMainThreadScheduler));
```

A MAUI app calls `WithMaui()` (or the whole thing through `MauiAppBuilder.UseReactiveUI`) from `MauiProgram`
instead of an `App` constructor. [Migration Guide: Xamarin to .NET MAUI](xamarin-to-maui.md) covers that startup
in full. Avalonia keeps configuring ReactiveUI through its own `AppBuilder.UseReactiveUI(...)`, which runs the
same `RxAppBuilder` chain underneath; see Avalonia's own documentation for that call, since it ships outside this
repository.

A platform with no dedicated `With<Platform>()` extension starts from the same `CreateReactiveUIBuilder()` call,
shown in the next section. It then adds `.WithPlatformServices()` for the platform module Splat already
detected, plus its own scheduler and converters.

## Fail fast before the app builds

`RxApp.EnsureInitialized()` used to be a no-op most of the time. `RxAppBuilder.EnsureInitialized()` throws until
`BuildApp()` has actually run, so a missing call surfaces at start-up instead of as a null service later:

```csharp
try
{
    RxAppBuilder.EnsureInitialized();
}
catch (InvalidOperationException exception)
{
    Console.WriteLine(exception.GetType().Name);
}

// Output:
// InvalidOperationException
```

```text
InvalidOperationException
```

## Register views, view models and services in one chain

Scattered `Locator.CurrentMutable.RegisterLazySingleton` calls move onto the builder. `WithRegistration` runs a
plain `IWantsToRegisterStuff` module's registration immediately. `WithRegistrationOnBuild` defers a registration
until `BuildApp()` runs. `RegisterView`, `RegisterSingletonView`, `RegisterViewModel`, `RegisterConstantViewModel`
and `RegisterSingletonViewModel` each register one type, with a chosen lifetime. A real app chains as many of
these as it needs, alongside plain Splat modules through `UsingSplatModule`:

```csharp
ReactiveUIBuilder builder = RxAppBuilder.CreateReactiveUIBuilder();
_ = builder.WithRegistration(new PantryRegistrations());

MessageBus kitchenEvents = new();
using IDisposable subscription = kitchenEvents.Listen<string>().Subscribe(Console.WriteLine);

_ = builder
    .WithPlatformServices()
    .WithMainThreadScheduler(Sequencer.Immediate)
    .WithTaskPoolScheduler(TaskPoolSequencer.Default, false)
    .WithExceptionHandler(Witness.Create<Exception>(static error => Console.WriteLine($"error: {error.Message}")))
    .WithMessageBus(kitchenEvents)
    .WithCacheSizes(32, 128)
    .WithSuspensionHost<RecipeBookState>()
    .UsingSplatModule(new PantryModule())
    .UsingSplatBuilder(static appBuilder => appBuilder.UsingModule(new SpiceRackModule()))
    .WithRegistrationOnBuild(static resolver => resolver.RegisterConstant<IPantryClock>(new PantryClock()))
    .RegisterView<RecipeBookView, RecipeBookViewModel>()
    .RegisterSingletonView<ShoppingListView, ShoppingListViewModel>()
    .RegisterViewModel<IngredientListViewModel>()
    .RegisterConstantViewModel<AppSettingsViewModel>()
    .RegisterSingletonViewModel<PantryViewModel>()
    .ConfigureViewLocator(static locator => locator.Map<RecipeBookViewModel, PrintableRecipeBookView>("Print"))
    .ConfigureSuspensionDriver(static driver =>
    {
        using IDisposable invalidated = driver.InvalidateState().Subscribe();
    })
    .BuildApp();

RxState.DefaultExceptionHandler.OnNext(new InvalidOperationException("Burnt the toast"));
kitchenEvents.SendMessage("Sunday roast is in the oven");

// Output:
// error: Burnt the toast
// Sunday roast is in the oven
```

`RxState.DefaultExceptionHandler` is the replacement for `RxApp.DefaultExceptionHandler`: the handler that
receives an error when an observable pipeline breaks and nothing else observes it.

## Register every view in an assembly

**Before**

```csharp
Locator.CurrentMutable.Register<IViewFor<MainViewModel>>(() => new MainView());
Locator.CurrentMutable.Register<IViewFor<DetailsViewModel>>(() => new DetailsView());
Locator.CurrentMutable.Register<IViewFor<SettingsViewModel>>(() => new SettingsView());
```

**After**

`WithViewsFromAssembly` runs the same reflection scan as `RegisterViewsForViewModels`, but as part of the
builder chain while the app starts:

```csharp
ReactiveUIBuilder builder = resolver.CreateReactiveUIBuilder();
_ = builder.WithCoreServices().BuildApp();
_ = builder.WithViewsFromAssembly(typeof(MusicPlayerScreen).Assembly);
```

Neither call is safe to trim, because both find views by scanning types at run time. `RegisterView<TView,
TViewModel>()` registers one view without scanning anything, and is the trim- and AOT-safe alternative. A view
whose class declaration implements `IViewFor<T>` also needs no registration at all: ReactiveUI.Binding's source
generator finds it at compile time. [Migration Guide: Bindings on ReactiveUI.Binding](reactiveui-binding-migration.md#view-location)
covers that generated lookup, and a service-locator registration still wins over it when you need one, for
example for a view whose constructor takes arguments.

## Build against your own dependency resolver

A plugin host or a test fixture wants its registrations isolated from the app-wide resolver `RxAppBuilder` uses
by default. Call `CreateReactiveUIBuilder()` on any `IMutableDependencyResolver` and every registration on that
builder reads back from the same resolver, with no `BuildApp()` needed:

```csharp
using ModernDependencyResolver resolver = new();
ReactiveUIBuilder builder = resolver.CreateReactiveUIBuilder();

_ = builder
    .WithRegistration(static mutable => mutable.RegisterConstant<IPantryClock>(new PantryClock()))
    .WithPlatformModule<PantryRegistrations>()
    .WithSuspensionHost();
```

This is also the pattern for a unit test: build a scoped resolver instead of reaching for the process-wide one,
so nothing a test registers leaks into the next test. [Testing](../handbook/testing.md) covers ReactiveUI's test
scheduler and how a test fixture disposes what it builds.

## Wire the builder into an existing container

An app that already builds its services through `Microsoft.Extensions.DependencyInjection`, Autofac or DryIoc
points `AppLocator` at that container instead of standing up a second one. Bind the Splat adapter for the
container first, then drive `RxAppBuilder` through the resolver-bound `CreateReactiveUIBuilder()` extension
shown above, against `AppLocator.CurrentMutable`.
[custom-dependency-inversion](../../splat/dependency-injection/custom-dependency-inversion.md) covers that
pattern for the generic host, Autofac and DryIoc in full, with a verification checklist for containers that
distinguish "configure" from "built".

## Where to go next

- [RxAppBuilder](../handbook/rxappbuilder.md)
- [Registration](../handbook/registration.md)
- [Testing](../handbook/testing.md)
