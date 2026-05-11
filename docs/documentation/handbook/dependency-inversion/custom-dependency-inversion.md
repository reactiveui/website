---
Order: 2
---
# Custom Dependency Inversion Container

ReactiveUI uses [Splat](https://github.com/reactiveui/splat) for service location. Splat ships ready-made adapter packages that bridge popular DI containers to ReactiveUI's `IMutableDependencyResolver`, and the modern entry point for wiring everything together is `RxAppBuilder`. The same builder works against every adapter — the only thing that changes is which `Use<Container>DependencyResolver()` extension you call first.

## The recipe

For any of the supported containers the flow is the same:

1. Build the container as you normally would.
2. Call `Use<Container>DependencyResolver(...)` so `AppLocator` forwards through it.
3. Pass the resulting `IMutableDependencyResolver` to `CreateReactiveUIBuilder()` and chain your platform / view / registration steps.
4. Call `BuildApp()`.

```csharp
resolver.CreateReactiveUIBuilder()
    .WithWpf()                                  // or WithMaui / WithBlazor / WithWinUI / WithWinForms / WithAndroidX / ForCustomPlatform
    .WithViewsFromAssembly(typeof(App).Assembly)
    .WithRegistration(r =>
    {
        r.RegisterLazySingleton<IScreen>(() => new AppBootstrapper());
        // ...your own services
    })
    .BuildApp();
```

The container is the source of truth — every `WithRegistration(r => ...)` callback writes into the resolver you passed in, so resolutions through your container, through `AppLocator.Current`, and through `IViewLocator` all see the same registrations.

## Splat adapter packages

| Container | Package | Adapter extension |
|-----------|---------|--------------------|
| Autofac | `Splat.Autofac` | `ContainerBuilder.UseAutofacDependencyResolver()` |
| DryIoc | `Splat.DryIoc` | `IContainer.UseDryIocDependencyResolver()` |
| Microsoft.Extensions.DependencyInjection | `Splat.Microsoft.Extensions.DependencyInjection` | `IServiceCollection.UseMicrosoftDependencyResolver()` / `IServiceProvider.UseMicrosoftDependencyResolver()` |
| Ninject | `Splat.Ninject` | `IKernel.UseNinjectDependencyResolver()` |
| SimpleInjector | `Splat.SimpleInjector` | `Container.UseSimpleInjectorDependencyResolver()` |

Install the matching package, then follow the per-container example below.

### Autofac

`Splat.Autofac` exposes `UseAutofacDependencyResolver()` on `ContainerBuilder`. Build your registrations first, swap the resolver in, then hand it to `RxAppBuilder`:

```csharp
using Autofac;
using ReactiveUI.Builder;
using Splat;
using Splat.Autofac;

var builder = new ContainerBuilder();
builder.RegisterType<MainViewModel>();
builder.RegisterType<MainView>().As<IViewFor<MainViewModel>>();
// ...your other registrations

// Swap Splat's AppLocator over to Autofac.
var autofacResolver = builder.UseAutofacDependencyResolver();

// Drive ReactiveUI registration through Autofac.
autofacResolver.CreateReactiveUIBuilder()
    .WithWpf()
    .WithViewsFromAssembly(typeof(App).Assembly)
    .BuildApp();

// Autofac is immutable from v5+; the adapter needs the built container's lifetime scope.
var container = builder.Build();
autofacResolver.SetLifetimeScope(container);
```

### DryIoc

```csharp
using DryIoc;
using ReactiveUI.Builder;
using Splat;
using Splat.DryIoc;

var container = new Container();
container.Register<MainViewModel>();
container.Register<IViewFor<MainViewModel>, MainView>();

container.UseDryIocDependencyResolver();

((IMutableDependencyResolver)AppLocator.CurrentMutable)
    .CreateReactiveUIBuilder()
    .WithWpf()
    .WithViewsFromAssembly(typeof(App).Assembly)
    .BuildApp();
```

### Microsoft.Extensions.DependencyInjection

`Splat.Microsoft.Extensions.DependencyInjection` ships two overloads: one for `IServiceCollection` (used while configuring) and one for `IServiceProvider` (used once you've built the provider — important when something else, like the Generic Host, owns the build step).

```csharp
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI.Builder;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<MainViewModel>();
services.AddTransient<IViewFor<MainViewModel>, MainView>();

// Make AppLocator forward into the ServiceCollection while we configure.
services.UseMicrosoftDependencyResolver();

((IMutableDependencyResolver)AppLocator.CurrentMutable)
    .CreateReactiveUIBuilder()
    .WithWpf()
    .WithViewsFromAssembly(typeof(App).Assembly)
    .BuildApp();

// Build the provider and re-bind AppLocator to it.
var provider = services.BuildServiceProvider();
provider.UseMicrosoftDependencyResolver();
```

Re-binding via the `IServiceProvider` overload is mandatory after `BuildServiceProvider()` — otherwise resolutions go through the old `IServiceCollection`-backed resolver and miss anything the provider configured.

## Implementing your own `IMutableDependencyResolver`

If no adapter exists for your container, implement `IMutableDependencyResolver` yourself and call `AppLocator.SetLocator(...)` to install it. Then drive ReactiveUI through `RxAppBuilder` exactly like the adapter examples.

<details><summary>Skeleton (Autofac-flavoured, illustrative)</summary>

```csharp
public class MyResolver : IMutableDependencyResolver
{
    public object? GetService(Type serviceType, string? contract = null) { /* ... */ }
    public IEnumerable<object> GetServices(Type serviceType, string? contract = null) { /* ... */ }
    public void Register(Func<object> factory, Type serviceType, string? contract = null) { /* ... */ }
    public IDisposable ServiceRegistrationCallback(Type serviceType, string? contract, Action<IDisposable> callback)
        => throw new NotImplementedException(); // not used by ReactiveUI
    public void Dispose() { /* ... */ }
}
```

</details>

```csharp
AppLocator.SetLocator(new MyResolver(myContainer));

((IMutableDependencyResolver)AppLocator.CurrentMutable)
    .CreateReactiveUIBuilder()
    .WithWpf()
    .BuildApp();
```

## Integrating into an existing application

If your app already has its own bootstrap (its own host builder, its own DI container, its own scheduler setup), you do **not** need to rip any of it out. The pattern is to fit `RxAppBuilder` into the seams you already have.

### 1. Identify the resolver you already have

Most existing apps fall into one of two camps:

- **You have an `IServiceCollection` / `IServiceProvider`** (ASP.NET Core, Generic Host, MAUI, .NET 8 hosted apps). Use `Splat.Microsoft.Extensions.DependencyInjection`.
- **You have a third-party container** (Autofac / DryIoc / Ninject / SimpleInjector). Use the matching Splat adapter.

Either way, the goal is the same: route `AppLocator` through your container so ReactiveUI's view-location, view-model registration, and platform services land in the container you already trust.

### 2. Hook the resolver as early as practical

Call the `Use<Container>DependencyResolver()` extension during your existing bootstrap, *before* the first ReactiveUI type is resolved. For a Generic Host app that means inside `ConfigureServices`; for a plain `App.xaml.cs` it means before `MainWindow`'s constructor runs.

### 3. Drive ReactiveUI initialization through the builder

Pick the platform method that matches your UI stack and let `RxAppBuilder` perform the platform registrations. `WithRegistration(r => ...)` runs immediately against your container, and `WithRegistrationOnBuild(r => ...)` defers registration until `BuildApp()` — handy if the registration depends on something else the builder is about to add.

```csharp
((IMutableDependencyResolver)AppLocator.CurrentMutable)
    .CreateReactiveUIBuilder()
    .WithMaui()                                  // pick the platform extension your app uses
    .WithViewsFromAssembly(typeof(App).Assembly) // optional but recommended
    .WithRegistration(r =>
    {
        // Register IScreen / IRoutableViewModel / your services here if not already in the container.
    })
    .BuildApp();
```

### 4. (If applicable) re-bind after the container is built

This step only matters for containers that distinguish "configuration" from "built" (`Microsoft.Extensions.DependencyInjection`, Autofac, SimpleInjector). After the build step, call the adapter's `Use...DependencyResolver` overload that takes the built form so subsequent resolutions hit the live container.

### 5. Verify

In an existing app it is worth proving the wiring with a one-liner during startup:

```csharp
var screen = AppLocator.Current.GetService<IScreen>();
Debug.Assert(screen is not null, "IScreen should resolve through the application's container.");
```

If that returns `null` but works when you resolve `IScreen` directly through your container, the `Use...DependencyResolver` call hasn't been re-bound after the container was built (step 4).

### Tips

- Don't call `RxAppBuilder.CreateReactiveUIBuilder()` (the parameter-less overload) in an existing app — that one points at Splat's default `AppLocator`, not your container. Use the `IMutableDependencyResolver.CreateReactiveUIBuilder()` extension instead.
- Splat modules you already use (`UsingSplatModule<T>(...)`) continue to work; the builder will register them through your container.
- For platforms that aren't covered by a `With<Platform>()` extension (a custom widget toolkit, server-side runtime, etc.), use `ForCustomPlatform(scheduler, configure)` and register the platform pieces yourself.
- You can still inspect the schedulers ReactiveUI ended up with via the value returned by `BuildApp()` (`app.MainThreadScheduler`, `app.TaskpoolScheduler`) — capture the return only if you need it.
