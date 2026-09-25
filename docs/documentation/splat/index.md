---
Order: 8
---
# Splat

Splat gives every .NET platform the same service locator and the same logging API. A **service locator** is one
shared place that hands out services. You register a service when your app starts, then ask for it anywhere.

ReactiveUI, ReactiveUI.Binding and Akavache find their services through Splat. You can also use Splat on its own.

```bash
dotnet add package Splat
```

## In this section

- [Dependency injection](dependency-injection/index.md) covers registering and resolving services.
- [Custom containers](dependency-injection/custom-dependency-inversion.md) connects Splat to Autofac, DryIoc,
  Microsoft.Extensions.DependencyInjection and other containers.
- [Logging](logging/index.md) covers the logging API and the adapters for Serilog, NLog, log4net and
  Microsoft.Extensions.Logging.

The source and the full package list are in the [reactiveui/splat](https://github.com/reactiveui/splat) repository.
