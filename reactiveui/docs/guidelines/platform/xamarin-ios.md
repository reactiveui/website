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

- [ReactiveTableViewController](~/api/reactiveui/reactivetableviewcontroller/)
- [ReactiveTableView](~/api/reactiveui/reactivetableview/)
- [ReactiveCollectionView](~/api/reactiveui/reactivecollectionview/)
- [ReactiveCollectionViewController](~/api/reactiveui/reactivecollectionviewcontroller)
- [ReactiveNavigationController](~/api/reactiveui/reactivenavigationcontroller)
- [ReactiveTabBarController](~/api/reactiveui/reactivetabbarcontroller/)
- [ReactivePageViewController](~/api/reactiveui/reactivepageviewcontroller/)
