Title: Xamarin Android
---

Ensure that you install either `ReactiveUI.AndroidX` or `ReactiveUI.AndroidSupport` into your applications.

Your viewmodels should inherit from `ReactiveObject`

- https://reactiveui.net/api/reactiveui/reactiveobject/

Use `WireUpControls`

- https://reactiveui.net/docs/handbook/data-binding/xamarin-android/wire-up-controls

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- https://reactiveui.net/api/reactiveui/IActivatableViewModel/
- https://reactiveui.net/docs/handbook/when-activated/

Keep references to your subscriptions

- https://reactiveui.net/docs/reactive-programming#lifecycle

Use disposables to manage lifetime, scope and resources:

- https://reactiveui.net/docs/reactive-programming#disposables

Don't use eventhandlers, use the extension methods shipped in `reactiveui.events` instead

- https://reactiveui.net/docs/handbook/events/

Use your normal Android concepts that you would usually use in Android development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- [ReactiveActivity](https://reactiveui.net/api/reactiveui/reactiveactivity_1/)
- [ReactiveFragment](https://reactiveui.net/api/reactiveui/reactivefragment_1/)
- [ReactivePreferenceFragment](https://reactiveui.net/api/reactiveui/reactivepreferencefragment_1/)
- [ReactivePreferenceActivity](https://reactiveui.net/api/reactiveui/reactivepreferenceactivity_1/)
- [ReactiveUI.AndroidSupport](https://www.reactiveui.net/api/reactiveui.androidsupport/)
- [ReactiveUI.AndroidX](https://www.reactiveui.net/api/reactiveui.androidx/)

There's also some extension methods which will make your life easier

- [ControlFetcherMixin](https://reactiveui.net/api/reactiveui.androidsupport/controlfetchermixin/)
- [SharedPreferencesExtensions](https://reactiveui.net/api/reactiveui/sharedpreferencesextensions/)
- [UsbManagerExtensions](https://reactiveui.net/api/reactiveui/usbmanagerextensions/)
- [AndroidObservableForWidgets](https://reactiveui.net/api/reactiveui/androidobservableforwidgets/)
- [FlexibleCommandBinder](https://reactiveui.net/api/reactiveui/flexiblecommandbinder/)
