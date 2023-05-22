NoTitle: true
Title: Xamarin iOS
----

Your viewmodels should inherit from `ReactiveObject`

- [ReactiveObject](https://reactiveui.net/api/reactiveui/reactiveobject/)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](https://reactiveui.net/api/reactiveui/IActivatableViewModel/)
- [When Activated](https://reactiveui.net/docs/handbook/when-activated/)

Keep references to your subscriptions

- [Reactive Programming#lifecycle](https://reactiveui.net/docs/reactive-programming#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive Programming#disposables](https://reactiveui.net/docs/reactive-programming#disposables)

Don't use eventhandlers, use the extension methods shipped in `reactiveui.events` instead

- [Events](https://reactiveui.net/docs/handbook/events/)

Use your normal iOS concepts that you would usually use in iOS development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- [ReactiveTableViewController](https://reactiveui.net/api/reactiveui/reactivetableviewcontroller/)
- [ReactiveTableView](https://reactiveui.net/api/reactiveui/reactivetableview/)
- [ReactiveCollectionView](https://reactiveui.net/api/reactiveui/reactivecollectionview/)
- [ReactiveCollectionViewController](https://reactiveui.net/api/reactiveui/reactivecollectionviewcontroller)
- [ReactiveNavigationController](https://reactiveui.net/api/reactiveui/reactivenavigationcontroller)
- [ReactiveTabBarController](https://reactiveui.net/api/reactiveui/reactivetabbarcontroller/)
- [ReactivePageViewController](https://reactiveui.net/api/reactiveui/reactivepageviewcontroller/)
