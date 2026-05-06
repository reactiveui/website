# AndroidX

Ensure that you install either `ReactiveUI.AndroidX` into your applications.

Your viewmodels should inherit from `ReactiveObject`

- `ReactiveObject`

Use `WireUpControls`

- [Wire Up Controls](../../handbook/data-binding/xamarin-android/wire-up-controls.md)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- `IActivatableViewModel`
- [When Activated](../../handbook/when-activated.md)

Keep references to your subscriptions

- [Reactive Programming#lifecycle](../../reactive-programming/index.md#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive Programming#disposables](../../reactive-programming/index.md#disposables)

Don't use eventhandlers, use the extension methods shipped in `ReactiveMarbles.ObservableEvents.SourceGenerator` instead

- [Events](../../handbook/events.md)

Use your normal Android concepts that you would usually use in Android development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- `ReactiveActivity`
- `ReactiveFragment`
- `ReactivePreferenceFragment`
- `ReactivePreferenceActivity`
- `ReactiveUI.AndroidX`

There's also some extension methods which will make your life easier

- `ControlFetcherMixin`
- `SharedPreferencesExtensions`
- `UsbManagerExtensions`
- `AndroidObservableForWidgets`
- `FlexibleCommandBinder`
