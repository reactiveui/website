---
Order: 2
---
# Avalonia

`ReactiveUI.Avalonia` lives in its own repository, [reactiveui/ReactiveUI.Avalonia](https://github.com/reactiveui/ReactiveUI.Avalonia),
not in `reactiveui/ReactiveUI`. It has its own target frameworks, release schedule and documentation.

Add `ReactiveUI.Avalonia` to your Avalonia application project.

```bash
dotnet add package ReactiveUI.Avalonia
```

A dependency-injection container of your choice adds one more package: `ReactiveUI.Avalonia.Autofac`,
`ReactiveUI.Avalonia.DryIoc`, `ReactiveUI.Avalonia.Microsoft.Extensions.DependencyInjection` or
`ReactiveUI.Avalonia.Ninject`. Each also ships as a `.Reactive` package, built from the same source, for an app
that uses System.Reactive.

See the [ReactiveUI.Avalonia repository](https://github.com/reactiveui/ReactiveUI.Avalonia) for setup and its
own examples, and [Data Binding in AvaloniaUI with ReactiveUI](../../handbook/data-binding/avalonia.md) for
binding a view to its view model with `WhenActivated`.
