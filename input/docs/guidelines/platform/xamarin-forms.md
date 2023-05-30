NoTitle: true
Title: Xamarin Forms
---

Ensure that you install `ReactiveUI.XamForms` into your applications.

Your viewmodels should inherit from `ReactiveObject`

- [ReactiveObject](https://reactiveui.net/api/reactiveui/reactiveobject/)

Use `WireUpControls`

- [Wire Up Controls](https://reactiveui.net/docs/handbook/data-binding/xamarin-android/wire-up-controls)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](https://reactiveui.net/api/reactiveui/IActivatableViewModel/)
- [When Activated](https://reactiveui.net/docs/handbook/when-activated/)

Keep references to your subscriptions

- [Reactive Programming#lifecycle](https://reactiveui.net/docs/reactive-programming#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive Programming#disposables](https://reactiveui.net/docs/reactive-programming#disposables)

Don't use eventhandlers, use the extension methods shipped in `ReactiveMarbles.ObservableEvents.SourceGenerator` instead

- [Events](https://reactiveui.net/docs/handbook/events/)

Use your normal Xamarin Forms concepts that you would usually use in  Xamarin Forms development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- [ReactiveCarouselPage](https://reactiveui.net/api/reactiveui.xamforms/reactivecarouselpage_1/)
- [ReactiveContentPage](https://reactiveui.net/api/reactiveui.xamforms/reactivecontentpage_1/)
- [ReactiveContentView](https://reactiveui.net/api/reactiveui.xamforms/reactivecontentview_1/)
- [ReactiveEntryCell](https://reactiveui.net/api/reactiveui.xamforms/reactiveentrycell_1/)
- [ReactiveImageCell](https://reactiveui.net/api/reactiveui.xamforms/reactiveimagecell_1/)
- [ReactiveMasterDetailPage](https://reactiveui.net/api/reactiveui.xamforms/reactivemasterdetailpage_1/)
- [ReactiveNavigationPage](https://reactiveui.net/api/reactiveui.xamforms/reactivenavigationpage_1/)
- [ReactiveSwitchCell](https://reactiveui.net/api/reactiveui.xamforms/reactiveswitchcell_1/)
- [ReactiveTabbedPage](https://reactiveui.net/api/reactiveui.xamforms/reactivetabbedpage_1/)
- [ReactiveTextCell](https://reactiveui.net/api/reactiveui.xamforms/reactivetextcell_1/)
- [ReactiveViewCell](https://reactiveui.net/api/reactiveui.xamforms/reactiveviewcell_1/)
- [RoutedViewHost](https://reactiveui.net/api/reactiveui.xamforms/routedviewhost/)
- [ViewModelViewHost](https://reactiveui.net/api/reactiveui.xamforms/viewmodelviewhost/)


