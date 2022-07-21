Title: Blazor
---

Ensure that you install `ReactiveUI.Blazor` into your application.

Your ViewModel should inherit from `ReactiveObject`

- [ReactiveObject](https://reactiveui.net/api/reactiveui/reactiveobject/)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](https://reactiveui.net/api/reactiveui/IActivatableViewModel/)
- [When Activated](https://reactiveui.net/docs/handbook/when-activated/)

Keep references to your subscriptions

- [Reactive programming#lifecycle](https://reactiveui.net/docs/reactive-programming/#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive programming#disposables](https://reactiveui.net/docs/reactive-programming/#disposables)

Use your normal Blazor concepts that you would usually use in Blazor development. There's also some extension methods which will make your life easier
