# Blazor

`ReactiveUI.Blazor` connects ReactiveUI to Blazor. Add it by following
[Installation](../../getting-started/installation/blazor.md). `ReactiveUI.Blazor` also ships as
`ReactiveUI.Blazor.Reactive`, built from the same source, for an app that uses System.Reactive. See
[Blazor](../../handbook/platforms/blazor.md) for a full walkthrough, built around a to-do list.

## Guidelines

- **View models inherit from `ReactiveObject`,** or from `ReactiveValidationObject` when a view model needs
  [validation](../../../validation.md).
- **Pick the component base class by how the view model reaches the page.** `ReactiveComponentBase<T>` takes its
  view model as a component parameter or lets the page instantiate one directly.
  `ReactiveInjectableComponentBase<T>` resolves its view model from the dependency injection container instead.
  `ReactiveLayoutComponentBase<T>` gives the same shape to a layout that wraps other pages.
  `ReactiveOwningComponentBase<T>` also owns a scoped service provider, disposed with the component, for a view
  model that needs a scoped dependency.
- **Use `IActivatableViewModel` and `WhenActivated` for lifecycle.** See [When Activated](../../handbook/when-activated.md).
- **Keep every subscription disposed.** See [Cleaning up subscriptions](../../../reactive-programming/observables.md#cleaning-up)
  and [Disposables](../../../primitives/disposables.md).

## Further reading

- [ReactiveUI On The Web with Blazor](../../../../articles/2020-07-12-article-blazor-compelling-example.md)
