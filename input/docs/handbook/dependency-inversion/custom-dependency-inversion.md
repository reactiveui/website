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
    
    public void UnregisterCurrent(Type serviceType, string contract = null)
    {
        throw new NotImplementedException();
    }

    public void UnregisterAll(Type serviceType, string contract = null)
    {
        throw new NotImplementedException();
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

*Note*: Technically, Autofac doesn't provide unregister functionality, but there are ways of getting around it. If interested, refer to [the following SO thread](https://stackoverflow.com/questions/5091101/is-it-possible-to-remove-an-existing-registration-from-autofac-container-builder) for one possibility.

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
var resolver = new AutofacDependencyResolver(container);
// Assigning your custom resolver to Locator.Current will automatically take care of
// adding all default Splat and ReactiveUI registrations to your container.
Locator.Current = resolver;
```

From this point on calls `Locator.Current` will go against your custom implementation!

## Bonus

The following is a simple example of using a FuncDependencyResolver with NInject:

```csharp
var kernel = new StandardKernel();
var customResolver = new FuncDependencyResolver(
    getAllServices: (service, contract) =>
        {
            if (contract != null)
            {
                return kernel.GetAll(service, contract);
            }
            var items = kernel.GetAll(service);
            var list = items.ToList();
            return list;
        },
    register: (factory, service, contract) =>
        {
            var binding = kernel.Bind(service).ToMethod(_ => factory());
            if (contract != null)
            {
                binding.Named(contract);
            }
        },
    unregisterCurrent: null,
    unregisterAll: null);
```
