---
Title: Xamarin iOS
---

Your viewmodels should inherit from `ReactiveObject`

- [ReactiveObject](~/api/ReactiveUI.ReactiveObject.yml)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](~/api/ReactiveUI.IActivatableViewModel.yml)
- [When Activated](~/docs/handbook/when-activated.md)

Keep references to your subscriptions

- [Reactive Programming#lifecycle](~/docs/reactive-programming/index.md#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive Programming#disposables](~/docs/reactive-programming/index.md#disposables)

Don't use eventhandlers, use the extension methods shipped in `ReactiveMarbles.ObservableEvents.SourceGenerator` instead

- [Events](~/docs/handbook/events.md)

Use your normal iOS concepts that you would usually use in iOS development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- [ReactiveTableViewController](~/api/ReactiveUI.ReactiveTableViewController.yml)
- [ReactiveTableView](~/api/ReactiveUI.ReactiveTableView.yml)
- [ReactiveCollectionView](~/api/ReactiveUI.ReactiveCollectionView-1.yml)
- [ReactiveCollectionViewController](~/api/ReactiveUI.ReactiveCollectionViewController.yml)
- [ReactiveNavigationController](~/api/ReactiveUI.ReactiveNavigationController.yml)
- [ReactiveTabBarController](~/api/ReactiveUI.ReactiveTabBarController.yml)
- [ReactivePageViewController](~/api/ReactiveUI.ReactivePageViewController.yml)
