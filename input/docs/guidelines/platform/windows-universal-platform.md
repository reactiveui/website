Title: Windows Universal Platform
---

Your viewmodels should inherit from `ReactiveObject`

- [ReactiveObject](https://reactiveui.net/api/reactiveui/reactiveobject/)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](https://reactiveui.net/api/reactiveui/IActivatableViewModel/)
- [When Activated](https://reactiveui.net/docs/handbook/when-activated/)

Keep references to your subscriptions

- [Reactive Programming#lifecycle](https://reactiveui.net/docs/reactive-programming#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive Programming#disposables](https://reactiveui.net/docs/reactive-programming#disposables)

Don't use eventhandlers, use the extension methods shipped in `reactiveui.events` instead

- [Events](https://reactiveui.net/docs/handbook/events/)

Use your normal UWP concepts that you would usually use in UWP development. There's also some extension methods which will make your life easier

- [ReactiveUserControl](https://reactiveui.net/api/reactiveui/reactiveusercontrol_1/)
- [IViewFor](https://reactiveui.net/api/reactiveui/iviewfor_1/)
- [RoutedViewHost](https://reactiveui.net/api/reactiveui/routedviewhost/)
- [AutoDataTemplateBindingHook](https://reactiveui.net/api/reactiveui/autodatatemplatebindinghook/)
