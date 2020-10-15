Title: Windows Presentation Framework
---

Ensure that you install `ReactiveUI.WPF` into your application.

Your viewmodels should inherit from `ReactiveObject`

- https://reactiveui.net/api/reactiveui/reactiveobject/

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- https://reactiveui.net/api/reactiveui/IActivatableViewModel/
- https://reactiveui.net/docs/handbook/when-activated/

Keep references to your subscriptions

- https://reactiveui.net/docs/reactive-programming/#lifecycle

Use disposables to manage lifetime, scope and resources:

- https://reactiveui.net/docs/reactive-programming/#disposables

Don't use eventhandlers, use the extension methods shipped in `reactiveui.events.wpf` instead

- https://reactiveui.net/docs/handbook/events/

When starting a WPF application from another application (e.g. Console) set `RxApp.MainThreadScheduler` to a `DispatcherScheduler` after creating the WPF application on a separate thread, to avoid threading errors.

Use your normal WPF concepts that you would usually use in WPF development. There's also some extension methods which will make your life easier

- https://reactiveui.net/api/reactiveui/reactiveusercontrol_1/
- https://reactiveui.net/api/reactiveui/iviewfor_1/
- https://reactiveui.net/api/reactiveui/routedviewhost/
- https://reactiveui.net/api/reactiveui/autodatatemplatebindinghook/
