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

See the [ReactiveUI.Avalonia repository](https://github.com/reactiveui/ReactiveUI.Avalonia) for setup and its
own examples. See [Data Binding in AvaloniaUI with ReactiveUI](../../handbook/data-binding/avalonia.md) for
binding a view to its view model with `WhenActivated`.
