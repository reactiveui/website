# Override Default Depenedency Inversion Container

We understand that some developers would prefer to use their favorite dependency inversion container.  ReactiveUI allows for this by implementing `IMutableDependencyResolver`.  Once this class has been implemented you simply need to set it to the `Locator`.

*Note*: This implementation is against Autofac, but can be used with most DI containers.

## Implement an `IMutableDependencyResolver`

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

## Register services

```csharp
public class AutofacDependencyRegistrar
{
    protected IContainer Container;

    public AutofacDependencyRegistrar(IContainer container)
    {
        Container = container;
        RegisterViews();
        RegisterViewModels();
        RegisterServices();
        RegisterScreen();
    }

    public void RegisterViews() { }

    public void RegisterViewModels() { }

    public void RegisterServices() { }

    public void RegisterScreen() { }
}
```


## Set the Locator.Current to your implementation

```csharp
container.UseAutofacDependencyResolver();
var resolver = Locator.CurrentMutable;
// These Initialize methods will add ReactiveUI platform registrations to your container
// They MUST be present if you override the default Locator
resolver.InitializeSplat();
resolver.InitializeReactiveUI();
Locator.Current = resolver;
```

From this point on calls `Locator.Current` will go against your custom implementation!

Note: Some DI engines get locked away after they're being built and become immutable. For such services, the `Use<MatchingService>DependencyResolver` extension method (`UseAutofacDependencyResolver` in Autofac) must be called again on the locked container. See AutoFac [docs](https://github.com/reactiveui/splat/tree/main/src/Splat.Autofac).

In Microsoft DI, the built container is a completely different type. You should recall `UseMicrosoftDependencyResolver` on the `IServiceProvider` once it's ready, in case you are using an external framework to manage DI (i.e. [Generic Host](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2#configureservices)), see the dedicated MS DI [docs](https://github.com/reactiveui/splat/tree/main/src/Splat.Microsoft.Extensions.DependencyInjection).
