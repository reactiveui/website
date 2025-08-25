# Xamarin Android

Ensure that you install either `ReactiveUI.AndroidX` or `ReactiveUI.AndroidSupport` into your applications.

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

Use your normal Android concepts that you would usually use in Android development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- [ReactiveActivity](~/api/ReactiveUI.ReactiveActivity-1.yml)
- [ReactiveFragment](~/api/ReactiveUI.ReactiveFragment-1.yml)
- [ReactivePreferenceFragment](~/api/ReactiveUI.ReactivePreferenceFragment-1.yml)
- [ReactivePreferenceActivity](~/api/ReactiveUI.ReactivePreferenceActivity-1.yml)
- [ReactiveUI.AndroidSupport](~/api/reactiveui.androidsupport.yml)
- [ReactiveUI.AndroidX](~/api/ReactiveUI.AndroidX.yml)

There's also some extension methods which will make your life easier

- [ControlFetcherMixin](~/api/reactiveui.androidsupport.controlfetchermixin.yml)
- [SharedPreferencesExtensions](~/api/ReactiveUI.SharedPreferencesExtensions.yml)
- [UsbManagerExtensions](~/api/ReactiveUI.UsbManagerExtensions.yml)
- [AndroidObservableForWidgets](~/api/ReactiveUI.AndroidObservableForWidgets.yml)
- [FlexibleCommandBinder](~/api/ReactiveUI.FlexibleCommandBinder.yml)
