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

- [ReactiveCarouselPage](~/api/ReactiveUI.XamForms.ReactiveCarouselPage-1.yml)
- [ReactiveContentPage](~/api/ReactiveUI.XamForms.ReactiveContentPage-1.yml)
- [ReactiveContentView](~/api/ReactiveUI.XamForms.ReactiveContentView-1.yml)
- [ReactiveEntryCell](~/api/ReactiveUI.XamForms.ReactiveEntryCell-1.yml)
- [ReactiveImageCell](~/api/ReactiveUI.XamForms.ReactiveImageCell-1.yml)
- [ReactiveMasterDetailPage](~/api/ReactiveUI.XamForms.ReactiveMasterDetailPage-1.yml)
- [ReactiveNavigationPage](~/api/ReactiveUI.XamForms.ReactiveNavigationPage-1.yml)
- [ReactiveSwitchCell](~/api/ReactiveUI.XamForms.ReactiveSwitchCell-1.yml)
- [ReactiveTabbedPage](~/api/ReactiveUI.XamForms.ReactiveTabbedPage-1.yml)
- [ReactiveTextCell](~/api/ReactiveUI.XamForms.ReactiveTextCell-1.yml)
- [ReactiveViewCell](~/api/ReactiveUI.XamForms.ReactiveViewCell-1.yml)
- [RoutedViewHost](~/api/ReactiveUI.XamForms.RoutedViewHost.yml)
- [ViewModelViewHost](~/api/ReactiveUI.XamForms.ViewModelViewHost.yml)


