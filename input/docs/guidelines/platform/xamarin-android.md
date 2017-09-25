Title: Xamarin Android
---

Your viewmodels should inherit from `ReactiveObject`

- https://reactiveui.net/api/reactiveui/reactiveobject/

Use wireupcontrols

- https://reactiveui.net/docs/handbook/data-binding/xamarin-android/wire-up-controls

Use `ISupportsActivation` and `WhenActivated` for lifecycle

- https://reactiveui.net/api/reactiveui/isupportsactivation/
- https://reactiveui.net/docs/handbook/when-activated/

Keep references to your subscriptions

- https://reactiveui.net/docs/concepts/reactive-programming/subscriptions#lifecycle

Use disposables to manage lifetime, scope and resources:

- https://reactiveui.net/docs/concepts/reactive-programming/disposables

Don't use eventhandlers, use the extension methods shipped in `reactiveui-events` instead

- https://reactiveui.net/docs/handbook/events/

Use your normal Android concepts that you would usually use in Android development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- https://reactiveui.net/api/reactiveui.androidsupport/reactiveappcompatactivity_1/
- https://reactiveui.net/api/reactiveui/reactiveactivity/
- https://reactiveui.net/api/reactiveui/reactivefragment/
- https://reactiveui.net/api/reactiveui.android.support/reactiverecyclerviewadapter_1/
- https://reactiveui.net/api/reactiveui.androidsupport/reactiveactionbaractivity
- https://reactiveui.net/api/reactiveui.androidsupport/reactivefragment/
- https://reactiveui.net/api/reactiveui.androidsupport/reactivedialogfragment/
- https://reactiveui.net/api/reactiveui/reactivefragmentactivity/
- https://reactiveui.net/api/reactiveui/reactivelistadapter_1/
- https://reactiveui.net/api/reactiveui/reactivepageradapter_1/
- https://reactiveui.net/api/reactiveui/reactivepreferencefragment/
- https://reactiveui.net/api/reactiveui/reactivepreferenceactivity/

There's also some extension methods which will make your life easier

- https://reactiveui.net/api/reactiveui.androidsupport/controlfetchermixin/
- https://reactiveui.net/api/reactiveui/sharedpreferencesextensions/
- https://reactiveui.net/api/reactiveui/usbmanagerextensions/
- https://reactiveui.net/api/reactiveui/androidobservableforwidgets/
- https://reactiveui.net/api/reactiveui/flexiblecommandbinder/
