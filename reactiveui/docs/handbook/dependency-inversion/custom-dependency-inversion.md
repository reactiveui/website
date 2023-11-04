---
NoTitle: true
---
## Override Default Depenedency Inversion Container

We understand that some developers would prefer to use their favorite dependency inversion container. ReactiveUI allows for this by implementing `IMutableDependencyResolver`. Once this class has been implemented you simply need to assign it to `Locator` via `Locator.SetLocator()`. 

## Use a Dependency Resolver Package

ReactiveUI uses [Splat](https://github.com/reactiveui/splat) for service location, and Splat can be thought of as a ReactiveUI utility extracted into a separate package. Splat includes ready to use [packages containing dependency resolver implementations](https://github.com/reactiveui/splat#dependency-resolver-packages) for `Autofac`, `DryIoc`, `Microsoft.Extensions.DependencyInjection`, `Ninject`, and `SimpleInjector`, see [the documentation page](https://github.com/reactiveui/splat#dependency-resolver-packages) for more info.

Now let's consider a simple example. Assume we wish to use `Splat.Autofac` dependency resolver package. We are going to replace the default container used by ReactiveUI with `Autofac`. To get started, we install the `Splat.Autofac` package via NuGet package manager or via .NET Core CLI:

```sh
dotnet add package Splat.Autofac
```

Next, we build a new `Autofac` container and register all the required dependencies into it. Next, we add a call to `UseAutofacDependencyResolver` which is an extension method inside the `Splat.Autofac` namespace. This method becomes available once we install the `Splat.Autofac` package into our project. We write the following code:

```cs
// Create a new Autofac container builder.
var builder = new ContainerBuilder();
builder.RegisterType<MainPage>().As<IViewFor<MainViewModel>>();
builder.RegisterType<MainViewModel>();
// etc.

// Register the Adapter to Splat.
// Creates and sets the Autofac resolver as the Locator.
var autofacResolver = builder.UseAutofacDependencyResolver();

// Register the resolver in Autofac so it can be later resolved.
builder.RegisterInstance(autofacResolver);

// Initialize ReactiveUI components.
autofacResolver.InitializeReactiveUI();

// If you need to override any service (such as the ViewLocator), register it after InitializeReactiveUI.
// https://autofaccn.readthedocs.io/en/latest/register/registration.html#default-registrations
// builder.RegisterType<MyCustomViewLocator>().As<IViewLocator>().SingleInstance();
```

> **Note** Call `Locator.CurrentMutable.InitializeReactiveUI()` *only if you are going to override* the default Splat locator. Otherwise *you don't need to explicitly call the method*, as ReactiveUI calls the method implicitly for you.

Set Autofac Locator's lifetime after the ContainerBuilder has been built:

```cs
var autofacResolver = container.Resolve<AutofacDependencyResolver>();

// Set a lifetime scope (either the root or any of the child ones) to Autofac resolver.
// This is needed because Autofac became immutable since version 5+.
// https://github.com/autofac/Autofac/issues/811
autofacResolver.SetLifetimeScope(container);`
```

## Implement a Custom `IMutableDependencyResolver`

If you are going to write a custom dependency resolver for Splat, then implement the `IMutableDependencyResolver` interface and add a call to `Locator.SetLocator`. This will replace the default locator implementation with your own.

<details><summary>IMutableDependencyResolver implementation against Autofac (example)</summary>

```csharp
public class AutofacDependencyResolver : IMutableDependencyResolver
{
    private readonly IContainer _container;

    public AutofacDependencyResolver(IContainer container)
    {
        _container = container;
    }

    public object GetService(Type serviceType, string contract = null)
    {
        try
        {
            return string.IsNullOrEmpty(contract)
                ? _container.Resolve(serviceType)
                : _container.ResolveNamed(contract, serviceType);
        }
        catch (DependencyResolutionException)
        {
            return null;
        }
    }

    public IEnumerable<object> GetServices(Type serviceType, string contract = null)
    {
        try
        {
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            object instance = string.IsNullOrEmpty(contract)
                ? _container.Resolve(enumerableType)
                : _container.ResolveNamed(contract, enumerableType);
            return ((IEnumerable)instance).Cast<object>();
        }
        catch (DependencyResolutionException)
        {
            return null;
        }
    }

    public void Register(Func<object> factory, Type serviceType, string contract = null)
    {
        var builder = new ContainerBuilder();
        if (string.IsNullOrEmpty(contract))
        {
            builder.Register(x => factory()).As(serviceType).AsImplementedInterfaces();
        }
        else
        {
            builder.Register(x => factory()).Named(contract, serviceType).AsImplementedInterfaces();
        }

        builder.Update(_container);
    }

    public IDisposable ServiceRegistrationCallback(Type serviceType, string contract, Action<IDisposable> callback)
    {
        // this method is not used by RxUI
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _container.Dispose();
    }
}
```
</details>

```csharp
// Build a new Autofac container.
var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterType<MainPage>().As<IViewFor<MainViewModel>>();

// Use your own IMutableDependencyResolver implementation.
Locator.SetLocator(new AutofacDependencyResolver(containerBuilder.Build()));

// These InitializeX() methods will add ReactiveUI platform 
// registrations to your container. They MUST be present if
// you override the default Locator.
Locator.CurrentMutable.InitializeSplat();
Locator.CurrentMutable.InitializeReactiveUI();
``` 

From this point on calls `Locator.Current` will go against your custom implementation!

> **Note** Some DI engines get locked away after they're being built and become immutable. For such services, the `Use<MatchingService>DependencyResolver` extension method (`UseAutofacDependencyResolver` in Autofac) must be called again on the locked container. See AutoFac [docs](https://github.com/reactiveui/splat/tree/main/src/Splat.Autofac).

> **Note** In Microsoft DI, the built container is a completely different type. You should recall `UseMicrosoftDependencyResolver` on the `IServiceProvider` once it's ready, in case you are using an external framework to manage DI (i.e. [Generic Host](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2#configureservices)), see the dedicated MS DI [docs](https://github.com/reactiveui/splat/tree/main/src/Splat.Microsoft.Extensions.DependencyInjection).
