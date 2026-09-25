---
Order: 1
---
# Getting Started

ReactiveUI is a family of .NET libraries. Each one solves one job, and you can use them together or on their own.
This page helps you pick where to start. [Installation](installation.md) lists every package.

## Pick a starting point

| You want to | Start with |
|---|---|
| Build an app with the model-view-viewmodel (MVVM) pattern | [ReactiveUI](../reactiveui/getting-started/index.md) |
| Keep a view and a view model in step, with or without ReactiveUI | [ReactiveUI.Binding](../binding/index.md) |
| Write less property and command code in a view model | [ReactiveUI.SourceGenerators](../source-generators/index.md) |
| Check user input in a view model | [ReactiveUI.Validation](../validation.md) |
| Navigate by naming view models | [Sextant](../sextant.md) |
| Register services and write logs on every platform | [Splat](../splat/index.md) |
| Work with values that arrive over time, such as events and timers | [ReactiveUI.Primitives](../primitives/index.md) |
| Call a REST API from a C# interface | [Refit](../refit/index.md) |
| Cache data and settings on the device | [Akavache](../akavache/index.md) |

## How the libraries fit together

ReactiveUI.Primitives and Splat sit at the bottom. Several libraries use streams from ReactiveUI.Primitives, and
ReactiveUI finds its services through Splat. A **stream** is a series of values that arrive over time.
[Reactive programming](../reactive-programming/index.md) explains streams from the start.

ReactiveUI builds on both. It uses ReactiveUI.Binding for bindings and view location, and it installs ReactiveUI.Binding
for you. ReactiveUI.SourceGenerators, ReactiveUI.Validation and Sextant add to ReactiveUI.

Refit and Akavache do not need ReactiveUI. You can use them in any .NET app.

## Next steps

1. [Install the packages](installation.md) you need.
2. Follow the getting started page of the library you picked.
3. Browse the [samples](../resources/samples.md) to see complete apps.
4. Ask questions in the [ReactiveUI Slack](https://join.slack.com/t/reactivex/shared_invite/zt-lt48skpz-G5WDYOAuzA80_MByZrLT0g).
