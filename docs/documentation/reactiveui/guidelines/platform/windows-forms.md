# Windows Forms

Ensure that you install `ReactiveUI.WinForms` into your application.


Please ensure that you are targeting at least windows10.0.19041.0

i.e `<TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>` in your csproj file.

Your ViewModels should inherit from `ReactiveObject`

- `ReactiveObject`

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- `IActivatableViewModel`
- [When Activated](../../handbook/when-activated.md)

Keep references to your subscriptions

- [Cleaning up subscriptions](../../../reactive-programming/observables.md#cleaning-up)

Use disposables to manage lifetime, scope and resources:

- [Disposables](../../../primitives/disposables.md)

Don't use eventhandlers, use the extension methods shipped in `ReactiveUI.Primitives.ObservableEvents` instead

- [Events](../../handbook/events.md)

Use your normal WinForms concepts that you would usually use in WinForms development. There's also some extension methods which will make your life easier

- `ReactiveUserControl`
- `IViewFor`
- `RoutedViewHost`
- `AutoDataTemplateBindingHook`
