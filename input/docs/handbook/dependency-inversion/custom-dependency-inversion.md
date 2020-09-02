# Override Default Depenedency Inversion Container

We understand that some developers would prefer to use their favorite dependency inversion container. ReactiveUI allows for this by implementing `IMutableDependencyResolver`. Once this class has been implemented you simply need to assign it to `Locator` via `Locator.SetLocator()`. [Splat](https://github.com/reactiveui/splat#dependency-resolver-packages) includes ready to use packages containing dependency resolver implementations for `Autofac`, `DryIoc`, `Microsoft.Extensions.DependencyInjection`, `Ninject`, and `SimpleInjector`, see [the documentation](https://github.com/reactiveui/splat#dependency-resolver-packages) for more info.

### Implement a custom `IMutableDependencyResolver`

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

### Set the Locator.Current to Your Implementation

```csharp
var container = new ContainerBuilder();
// Register things to the Autofac container...
container.UseAutofacDependencyResolver();
Locator.CurrentMutable.InitializeSplat();
Locator.CurrentMutable.InitializeReactiveUI();
// These InitializeX() methods will add ReactiveUI platform 
// registrations to your container. They MUST be present if
// you override the default Locator.
``` 

From this point on calls `Locator.Current` will go against your custom implementation!

> **Note** Some DI engines get locked away after they're being built and become immutable. For such services, the `Use<MatchingService>DependencyResolver` extension method (`UseAutofacDependencyResolver` in Autofac) must be called again on the locked container. See AutoFac [docs](https://github.com/reactiveui/splat/tree/main/src/Splat.Autofac).

> **Note** In Microsoft DI, the built container is a completely different type. You should recall `UseMicrosoftDependencyResolver` on the `IServiceProvider` once it's ready, in case you are using an external framework to manage DI (i.e. [Generic Host](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2#configureservices)), see the dedicated MS DI [docs](https://github.com/reactiveui/splat/tree/main/src/Splat.Microsoft.Extensions.DependencyInjection).
