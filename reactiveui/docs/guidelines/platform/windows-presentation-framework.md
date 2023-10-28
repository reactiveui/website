---
Title: Windows Presentation Framework
---

Ensure that you install `ReactiveUI.WPF` into your application.

Your viewmodels should inherit from `ReactiveObject`

- [ReactiveObject](~/api/reactiveui/reactiveobject/)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](~/api/reactiveui/IActivatableViewModel/)
- [When Activated](~/docs/handbook/when-activated/index.md)

Keep references to your subscriptions

- [Reactive programming#lifecycle](~/docs/reactive-programming/index.md#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive programming#disposables](~/docs/reactive-programming/index.md#disposables)

Don't use eventhandlers, use the extension methods shipped in `ReactiveMarbles.ObservableEvents.SourceGenerator` instead

- [Events](~/docs/handbook/events/index.md)

When starting a WPF application from another application (e.g. Console) set `RxApp.MainThreadScheduler` to a `DispatcherScheduler` after creating the WPF application on a separate thread, to avoid threading errors.

Use your normal WPF concepts that you would usually use in WPF development. There's also some extension methods which will make your life easier

- [ReactiveUserControl](~/api/reactiveui/reactiveusercontrol_1/)
- [IViewFor](~/api/reactiveui/iviewfor_1/)
- [RoutedViewHost](~/api/reactiveui/routedviewhost/)
- [AutoDataTemplateBindingHook](~/api/reactiveui/autodatatemplatebindinghook/)
