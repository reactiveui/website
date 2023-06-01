NoTitle: true
Title: Xamarin iOS
----

Your viewmodels should inherit from `ReactiveObject`

- [ReactiveObject](../../../api/reactiveui/reactiveobject/)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](../../../api/reactiveui/IActivatableViewModel/)
- [When Activated](../../../docs/handbook/when-activated/)

Keep references to your subscriptions

- [Reactive Programming#lifecycle](../../../docs/reactive-programming#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive Programming#disposables](../../../docs/reactive-programming#disposables)

Don't use eventhandlers, use the extension methods shipped in `ReactiveMarbles.ObservableEvents.SourceGenerator` instead

- [Events](../../../docs/handbook/events/)

Use your normal iOS concepts that you would usually use in iOS development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- [ReactiveTableViewController](../../../api/reactiveui/reactivetableviewcontroller/)
- [ReactiveTableView](../../../api/reactiveui/reactivetableview/)
- [ReactiveCollectionView](../../../api/reactiveui/reactivecollectionview/)
- [ReactiveCollectionViewController](../../../api/reactiveui/reactivecollectionviewcontroller)
- [ReactiveNavigationController](../../../api/reactiveui/reactivenavigationcontroller)
- [ReactiveTabBarController](../../../api/reactiveui/reactivetabbarcontroller/)
- [ReactivePageViewController](../../../api/reactiveui/reactivepageviewcontroller/)
