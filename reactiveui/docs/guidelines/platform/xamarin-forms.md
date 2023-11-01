---
Title: Xamarin Forms
---

Ensure that you install `ReactiveUI.XamForms` into your applications.

Your viewmodels should inherit from `ReactiveObject`

- [ReactiveObject](~/api/ReactiveUI.ReactiveObject.yml)

Use `WireUpControls`

- [Wire Up Controls](~/docs/handbook/data-binding/xamarin-android/wire-up-controls.md)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](~/api/ReactiveUI.IActivatableViewModel.yml)
- [When Activated](~/docs/handbook/when-activated.md)

Keep references to your subscriptions

- [Reactive Programming#lifecycle](~/docs/reactive-programming/index.md#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive Programming#disposables](~/docs/reactive-programming/index.md#disposables)

Don't use eventhandlers, use the extension methods shipped in `ReactiveMarbles.ObservableEvents.SourceGenerator` instead

- [Events](~/docs/handbook/events.md)

Use your normal Xamarin Forms concepts that you would usually use in  Xamarin Forms development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- [ReactiveCarouselPage](~/api/reactiveui.xamforms/reactivecarouselpage_1/)
- [ReactiveContentPage](~/api/reactiveui.xamforms/reactivecontentpage_1/)
- [ReactiveContentView](~/api/reactiveui.xamforms/reactivecontentview_1/)
- [ReactiveEntryCell](~/api/reactiveui.xamforms/reactiveentrycell_1/)
- [ReactiveImageCell](~/api/reactiveui.xamforms/reactiveimagecell_1/)
- [ReactiveMasterDetailPage](~/api/reactiveui.xamforms/reactivemasterdetailpage_1/)
- [ReactiveNavigationPage](~/api/reactiveui.xamforms/reactivenavigationpage_1/)
- [ReactiveSwitchCell](~/api/reactiveui.xamforms/reactiveswitchcell_1/)
- [ReactiveTabbedPage](~/api/reactiveui.xamforms/reactivetabbedpage_1/)
- [ReactiveTextCell](~/api/reactiveui.xamforms/reactivetextcell_1/)
- [ReactiveViewCell](~/api/reactiveui.xamforms/reactiveviewcell_1/)
- [RoutedViewHost](~/api/reactiveui.xamforms/routedviewhost/)
- [ViewModelViewHost](~/api/reactiveui.xamforms/viewmodelviewhost/)


