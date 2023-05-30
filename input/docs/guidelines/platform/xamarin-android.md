NoTitle: true
Title: Xamarin Android
---

Ensure that you install either `ReactiveUI.AndroidX` or `ReactiveUI.AndroidSupport` into your applications.

Your viewmodels should inherit from `ReactiveObject`

- [ReactiveObject](../../../api/reactiveui/reactiveobject/)

Use `WireUpControls`

- [Wire Up Controls](../../../docs/handbook/data-binding/xamarin-android/wire-up-controls)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](../../../api/reactiveui/IActivatableViewModel/)
- [When Activated](../../../docs/handbook/when-activated/)

Keep references to your subscriptions

- [Reactive Programming#lifecycle](../../../docs/reactive-programming#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive Programming#disposables](../../../docs/reactive-programming#disposables)

Don't use eventhandlers, use the extension methods shipped in `ReactiveMarbles.ObservableEvents.SourceGenerator` instead

- [Events](../../../docs/handbook/events/)

Use your normal Android concepts that you would usually use in Android development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- [ReactiveActivity](../../../api/reactiveui/reactiveactivity_1/)
- [ReactiveFragment](../../../api/reactiveui/reactivefragment_1/)
- [ReactivePreferenceFragment](../../../api/reactiveui/reactivepreferencefragment_1/)
- [ReactivePreferenceActivity](../../../api/reactiveui/reactivepreferenceactivity_1/)
- [ReactiveUI.AndroidSupport](../../../api/reactiveui.androidsupport/)
- [ReactiveUI.AndroidX](../../../api/reactiveui.androidx/)

There's also some extension methods which will make your life easier

- [ControlFetcherMixin](../../../api/reactiveui.androidsupport/controlfetchermixin/)
- [SharedPreferencesExtensions](../../../api/reactiveui/sharedpreferencesextensions/)
- [UsbManagerExtensions](../../../api/reactiveui/usbmanagerextensions/)
- [AndroidObservableForWidgets](../../../api/reactiveui/androidobservableforwidgets/)
- [FlexibleCommandBinder](../../../api/reactiveui/flexiblecommandbinder/)
