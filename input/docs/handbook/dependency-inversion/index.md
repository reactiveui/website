# Dependency Injection

Dependency resolution is very useful for moving logic that would normally have to be in platform-specific code, into the shared platform code. First, we need to define an Interface for something that we want to use - this example isn't a Best Practice, but it's illustrative.

Dependency resolution is a feature built into the core framework, which allows libraries and ReactiveUI itself to use classes that are in other libraries without taking a direct reference to them. This is quite useful for cross-platform applications, as it allows portable code to use non-portable APIs, as long as they can be described via an Interface.

ReactiveUI's use of dependency resolution can more properly be called the Service Locator pattern. If the locator pattern doesn't fit your situation then have a look at how to do composition root.

Since ReactiveUI 6, [Splat](https://github.com/reactiveui/splat) is used by ReactiveUI for service locator and dependency injection. Earlier versions included a RxUI resolver. If you come across samples for RxUI versions earlier than 6, you should replace references to `RxApp.DependencyResolver` with `Locator.Current` and `RxApp.MutableResolver` with `Locator.CurrentMutable`.

## Why Splat?

> "I generally think a lot of the advice around IoC/DI is pretty bad in the domain of 'cross-platform mobile applications', because you have to remember that a lot of their ideas were written for web apps, not mobile or desktop apps.
>
> For example, the vast majority of popular IoC containers concern themselves solely with resolution speed on a warm cache, while basically completely disregarding memory usage or startup time - this is 100% fine for server applications, because these things don't matter; but for a mobile app? Startup time is huge.
>
> Splat's Service Location solves a number of issues for RxUI:
>
> Service Location is fast, and has almost no overhead to set up.
It encapsulates several different common object lifetime models (i.e. 'create new every time', 'singleton', 'lazy'), just by writing the Func differently
It's Mono Linker friendly (generally)
Service Location allows us to register types in platform-specific code, but use them in PCL code."
>
> Anaïs Betts @ http://stackoverflow.com/a/26924067/496857


## Registration

Splat supports on-demand new'ing, constant and lazy registration of dependencies. 

```cs
// Create a new Toaster any time someone asks
Locator.CurrentMutable.Register(() => new Toaster(), typeof(IToaster));

// Register a singleton instance
Locator.CurrentMutable.RegisterConstant(new ExtraGoodToaster(), typeof(IToaster));

// Register a singleton which won't get created until the first user accesses it
Locator.CurrentMutable.RegisterLazySingleton(() => new LazyToaster(), typeof(IToaster));
```

It is recommended to register your dependencies all at one place. This can be achieved for example with an AppBootstrapper.

```csharp
public class AppBootstrapper : IEnableLogger 
{  
    public AppBootstrapper() 
    { 
        RegisterDependencies(); 
    }  

    public void RegisterDependencies() 
    { 
        Locator.CurrentMutable.RegisterConstant(new FeedService(), typeof(IFeedService)); 
    }  
} 
```

This code can be extended to have multiple methods to group the dependencies and make it better readable.


## Resolution

Splat provides methods to resolve dependencies to single or multiple instances. 
 
```csharp
var toaster = Locator.Current.GetService<IToaster>();
var allToasterImpls = Locator.Current.GetServices<IToaster>();
```

Recommended usage is:

```csharp
public FeedsViewModel(IBlobCache cache = null) 
{ 
	Cache = cache ?? Locator.Current.GetService<IBlobCache>();
}
```
 

## Advanced
Splat's dependency resolver, accessible using `Locator.Current` conceptually resembles the below:

```csharp
public interface IDependencyResolver
{
    // Returns the most recent service registered to this type and contract
    T GetService<T>(string contract = null);

    // Returns all of the services registerd to this type and contract
    IEnumerable<T> GetServices<T>(string contract = null)
}
```

Given a type T (usually an interface), you can now receive an implementation of T. If the T registered is very common ("string" for example), or you want to distinguish by a method other than type, you can use the "contract"
parameter which is an arbitrary key that you provide.

The current resolver that ReactiveUI itself will use (as well as what your app
should use as well), is provided by [Splat.ModernDependencyResolver](https://github.com/reactiveui/splat/blob/b833718d1b7940d1d02403e86864d03d2af5cea7/Splat/ServiceLocation.cs).

