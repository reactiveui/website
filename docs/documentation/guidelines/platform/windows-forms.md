# Windows Forms

Ensure that you install `ReactiveUI.WinForms` into your application.


Please ensure that you are targeting at least windows10.0.19041.0

i.e `<TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>` in your csproj file.

Your viewmodels should inherit from `ReactiveObject`

- `ReactiveObject`

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- `IActivatableViewModel`
- [When Activated](../../handbook/when-activated.md)

Keep references to your subscriptions

- [Reactive Programming#lifecycle](../../reactive-programming/index.md#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive Programming#disposables](../../reactive-programming/index.md#disposables)

Don't use eventhandlers, use the extension methods shipped in `ReactiveMarbles.ObservableEvents.SourceGenerator` instead

- [Events](../../handbook/events.md)

Use your normal WinForms concepts that you would usually use in WinForms development. There's also some extension methods which will make your life easier

- `ReactiveUserControl`
- `IViewFor`
- `RoutedViewHost`
- `AutoDataTemplateBindingHook`
