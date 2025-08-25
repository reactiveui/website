# Blazor

## Project

Ensure that you install `ReactiveUI.Blazor` into your application.

## ViewModel

Your ViewModel should inherit from `ReactiveObject` or 'ReactiveValidationObject' if you wish to use [ReactiveUI.Validation](~/docs/handbook/user-input-validation.md)

- [ReactiveObject](~/api/ReactiveUI.ReactiveObject.yml)
- [ReactiveValidationObject](~/api/reactiveui.validation.helpers.reactivevalidationobject.yml)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](~/api/reactiveui.IActivatableViewModel.yml)
- [When Activated](~/docs/handbook/when-activated.md)

Keep references to your subscriptions

- [Reactive programming#lifecycle](~/docs/reactive-programming/index.md#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive programming#disposables](~/docs/reactive-programming/index.md#disposables)

## View

Your Page (View) should inherit from

- [ReactiveComponentBase<T>](~/api/ReactiveUI.Blazor.ReactiveComponentBase-1.yml): If you want to pass your ViewModel to the page as a parameter or you want to instantiate it to the View, then set the ViewModel property in the Page's code behind.
- [ReactiveInjectableComponentBase<T>](~/api/ReactiveUI.Blazor.ReactiveInjectableComponentBase-1.yml): If you want that your corresponding ViewModel should be injected by the dependency injection container.
- [ReactiveLayoutComponentBase](~/api/ReactiveUI.Blazor.ReactiveLayoutComponentBase-1.yml): If you only want to instantiate your ViewModel corresponding to the View, then just set the ViewModel property in the Page's code behind.


Use your normal Blazor concepts that you would usually use in Blazor development. There's also some extension methods which will make your life easier

Useful links
- [ReactiveUI On The Web with Blazor](~/articles/2020-07-12-article-blazor-compelling-example.md)
