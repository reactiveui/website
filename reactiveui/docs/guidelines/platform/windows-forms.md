
## Windows Forms

Ensure that you install `ReactiveUI.WinForms` into your application.

Your viewmodels should inherit from `ReactiveObject`

- [ReactiveObject](~/api/ReactiveUI.ReactiveObject.yml)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](~/api/ReactiveUI.IActivatableViewModel.yml)
- [When Activated](~/docs/handbook/when-activated.md)

Keep references to your subscriptions

- [Reactive Programming#lifecycle](~/docs/reactive-programming/index.md#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive Programming#disposables](~/docs/reactive-programming/index.md#disposables)

Don't use eventhandlers, use the extension methods shipped in `ReactiveMarbles.ObservableEvents.SourceGenerator` instead

- [Events](~/docs/handbook/events.md)

Use your normal WinForms concepts that you would usually use in WinForms development. There's also some extension methods which will make your life easier

- [ReactiveUserControl](~/api/ReactiveUI.ReactiveUserControl-1.yml)
- [IViewFor](~/api/ReactiveUI.IViewFor-1.yml)
- [RoutedViewHost](~/api/ReactiveUI.RoutedViewHost.yml)
- [AutoDataTemplateBindingHook](~/api/ReactiveUI.AutoDataTemplateBindingHook.yml)
