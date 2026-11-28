---
Order: 2
---
# Avalonia

`ReactiveUI.Avalonia` lives in its own repository, not in `reactiveui/ReactiveUI`. That repository is
[reactiveui/ReactiveUI.Avalonia](https://github.com/reactiveui/ReactiveUI.Avalonia). It has its own supported
.NET versions. It has its own release schedule and its own docs too.

Add `ReactiveUI.Avalonia` to your Avalonia application project.

```bash
dotnet add package ReactiveUI.Avalonia
```

Pick a dependency-injection container, and add one more package for it: `ReactiveUI.Avalonia.Autofac`,
`ReactiveUI.Avalonia.DryIoc`, `ReactiveUI.Avalonia.Microsoft.Extensions.DependencyInjection` or
`ReactiveUI.Avalonia.Ninject`. Each of these also ships as a `.Reactive` package, built from the same source,
for an app that uses System.Reactive.

## Register an application service

Configure the container on Avalonia's `AppBuilder` before the application starts. For example, register a clock with Microsoft dependency injection in your desktop startup code.

```csharp
using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI.Avalonia.Splat;

var builder = AppBuilder.Configure<App>()
    .UsePlatformDetect()
    .UseReactiveUIWithMicrosoftDependencyResolver(
        static services => services.AddSingleton(TimeProvider.System));
```

After Avalonia sets up platform services, `AppLocator.Current.GetService<TimeProvider>()` returns the registered clock. The container example starts a headless application and checks that registration. Run it from the repository's `src` directory. [Example source](https://github.com/reactiveui/ReactiveUI.Avalonia/blob/main/src/examples/ContainerStartup/Program.cs)

```sh
dotnet run --project examples/ContainerStartup -f net11.0 -- microsoft
```

| Container argument | Startup extension |
| --- | --- |
| `microsoft` | `UseReactiveUIWithMicrosoftDependencyResolver` |
| `autofac` | `UseReactiveUIWithAutofac` |
| `dryioc` | `UseReactiveUIWithDryIoc` |
| `ninject` | `UseReactiveUIWithNinject` |

For System.Reactive, use the matching `.Reactive` package and import `ReactiveUI.Avalonia.Reactive.Splat`. The companion project accepts the same four arguments. [System.Reactive example](https://github.com/reactiveui/ReactiveUI.Avalonia/tree/main/src/examples/ContainerStartup.Reactive)

The desktop showcase also demonstrates view activation, commands and navigation. [Desktop showcase](https://github.com/reactiveui/ReactiveUI.Avalonia/tree/main/src/examples/ReactiveUI.Avalonia.Example) · [Bind an Avalonia view](../../handbook/data-binding/avalonia.md)
