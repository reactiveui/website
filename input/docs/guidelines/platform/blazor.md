Title: Blazor
---

### Project

Ensure that you install `ReactiveUI.Blazor` into your application.

### ViewModel

Your ViewModel should inherit from `ReactiveObject`

- [ReactiveObject](https://reactiveui.net/api/reactiveui/reactiveobject/)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](https://reactiveui.net/api/reactiveui/IActivatableViewModel/)
- [When Activated](https://reactiveui.net/docs/handbook/when-activated/)

Keep references to your subscriptions

- [Reactive programming#lifecycle](https://reactiveui.net/docs/reactive-programming/#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive programming#disposables](https://reactiveui.net/docs/reactive-programming/#disposables)

### View

Your Page (View) should inherit from

- [ReactiveComponentBase<T>](https://www.reactiveui.net/api/reactiveui.blazor/reactivecomponentbase_1/): If you want to pass your ViewModel to the page as a parameter or you want to instantiate it to the View, then set the ViewModel property in the Page's code behind.
- [ReactiveInjectableComponentBase<T>](https://www.reactiveui.net/api/reactiveui.blazor/reactiveinjectablecomponentbase_1/): If you want that your corresponding ViewModel should be injected by the dependency injection container.
- [ReactiveLayoutComponentBase](https://www.reactiveui.net/api/reactiveui.blazor/reactivelayoutcomponentbase_1/): If you only want to instantiate your ViewModel corresponding to the View, then just set the ViewModel property in the Page's code behind.


Use your normal Blazor concepts that you would usually use in Blazor development. There's also some extension methods which will make your life easier

Useful links
- [ReactiveUI On The Web with Blazor](https://www.reactiveui.net/blog/2020/07/article-blazor-compelling-example)
