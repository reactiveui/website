---
Order: 2
---
# Use an existing dependency injection container

When an app already owns its services, connect its container to Splat during startup. ReactiveUI can then find the same view models, views and shared services as the rest of the app.

## The recipe

For an app that uses Microsoft dependency injection, install `Splat.Microsoft.Extensions.DependencyInjection`. Register services with the collection, configure ReactiveUI and then give Splat the provider that the app uses.

```csharp
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI.Builder;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<DispatchQueue>();
services.UseMicrosoftDependencyResolver();
AppLocator.CurrentMutable.CreateReactiveUIBuilder().WithWpf().BuildApp();

using var provider = services.BuildServiceProvider();
provider.UseMicrosoftDependencyResolver();
var queue = provider.GetRequiredService<DispatchQueue>();
Debug.Assert(ReferenceEquals(queue, AppLocator.Current.GetService<DispatchQueue>()));
```

Here `DispatchQueue` is the application's service. Use your platform's builder extension in place of `WithWpf()`. If a host builds the provider, pass its existing provider to `UseMicrosoftDependencyResolver()` after the host builds.

## Splat adapter packages

| Container | Package | How to connect it |
| --- | --- | --- |
| Autofac | `Splat.Autofac` | Keep the resolver returned by the container builder and attach the built lifetime scope. |
| DryIoc | `Splat.DryIoc` | Call `UseDryIocDependencyResolver()` on the container. |
| Microsoft dependency injection | `Splat.Microsoft.Extensions.DependencyInjection` | Connect the service collection during registration and the provider after build. |
| Ninject | `Splat.Ninject` | Call `UseNinjectDependencyResolver()` on the kernel. |
| Simple Injector | `Splat.SimpleInjector` | Collect registrations in a `SimpleInjectorInitializer` and pass it to the container adapter. |
| Prism | `Splat.Prism` | Use `SplatContainerExtension` as Prism's container. Its `Instance` is the Splat resolver. |

### Autofac

Register services with the builder before you build the container. Attach the scope that owns those services before resolving them.

```csharp
var builder = new ContainerBuilder();
builder.RegisterType<DispatchQueue>().SingleInstance();
var resolver = builder.UseAutofacDependencyResolver();
resolver.CreateReactiveUIBuilder().WithWpf().BuildApp();
using var container = builder.Build();
resolver.SetLifetimeScope(container);
```

### DryIoc

```csharp
using var container = new DryIoc.Container();
container.RegisterInstance(new DispatchQueue());
container.UseDryIocDependencyResolver();
AppLocator.CurrentMutable.CreateReactiveUIBuilder().WithWpf().BuildApp();
```

### Microsoft.Extensions.DependencyInjection

The [startup example](#the-recipe) uses both adapter overloads. The same sequence works when your app registers more services through `WithRegistration` or discovers views through `WithViewsFromAssembly`.

### Ninject and Simple Injector

In Ninject, bind the service to the kernel and call `kernel.UseNinjectDependencyResolver()`. In Simple Injector, use the initializer to prepare Splat registrations before the first resolve locks the container.

```csharp
using var container = new SimpleInjector.Container();
using var initializer = new SimpleInjectorInitializer();
initializer.RegisterConstant(new DispatchQueue());
container.UseSimpleInjectorDependencyResolver(initializer);
var queue = AppLocator.Current.GetService<DispatchQueue>();
```

### Prism

Prism can use Splat to store services. A name passed to Prism's `RegisterInstance` becomes the contract used to look up that service in Splat.

```csharp
using var prism = new SplatContainerExtension();
var queue = new DispatchQueue();
prism.RegisterInstance(typeof(DispatchQueue), queue, "north");
Debug.Assert(ReferenceEquals(queue, prism.Instance.GetService<DispatchQueue>("north")));
```

The dispatch sample registers and resolves a queue with each adapter. [Container examples](https://github.com/reactiveui/splat/blob/main/src/examples/DispatchAdapters/DispatchContainers.cs)

## Implementing your own `IMutableDependencyResolver`

`AppLocator.SetLocator` takes an `IDependencyResolver`. This interface combines registration, lookup and disposal. You can implement it directly or use `FuncDependencyResolver` to delegate to an existing container. Preserve named contracts and multiple registrations when your app uses them.

```csharp
AppLocator.SetLocator(myResolver);
AppLocator.CurrentMutable.CreateReactiveUIBuilder().WithWpf().BuildApp();
```

## Integrating into an existing application

Connect the adapter before constructing the first view. Use `WithRegistration` to register immediately, or `WithRegistrationOnBuild` to register when `BuildApp()` runs. For a toolkit with its own scheduler, use `ForCustomPlatform`. After startup, resolve a known singleton through the container and through Splat. Both lookups should return the same instance.
