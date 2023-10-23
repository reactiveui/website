
## Windows Forms

Ensure that you install `ReactiveUI.WinForms` into your application.

Your viewmodels should inherit from `ReactiveObject`

- [ReactiveObject](api/reactiveui/reactiveobject/)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](api/reactiveui/IActivatableViewModel/)
- [When Activated](docs/handbook/when-activated/)

Keep references to your subscriptions

- [Reactive Programming#lifecycle](docs/reactive-programming#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive Programming#disposables](docs/reactive-programming#disposables)

Don't use eventhandlers, use the extension methods shipped in `ReactiveMarbles.ObservableEvents.SourceGenerator` instead

- [Events](docs/handbook/events/)

Use your normal WinForms concepts that you would usually use in WinForms development. There's also some extension methods which will make your life easier

- [ReactiveUserControl](api/reactiveui/reactiveusercontrol_1/)
- [IViewFor](api/reactiveui/iviewfor_1/)
- [RoutedViewHost](api/reactiveui/routedviewhost/)
- [AutoDataTemplateBindingHook](api/reactiveui/autodatatemplatebindinghook/)
