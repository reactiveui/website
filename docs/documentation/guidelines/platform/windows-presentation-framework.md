# Windows Presentation Framework

Ensure that you install `ReactiveUI.WPF` into your application.

Please ensure that you are targeting at least windows10.0.19041.0

i.e `<TargetFramework>net10.0-windows10.0.19041.0</TargetFramework>` in your csproj file.

Your viewmodels should inherit from `ReactiveObject`

- `ReactiveObject`

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- `IActivatableViewModel`
- [When Activated](../../handbook/when-activated.md)

Keep references to your subscriptions

- [Reactive programming#lifecycle](../../reactive-programming/index.md#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive programming#disposables](../../reactive-programming/index.md#disposables)

Don't use eventhandlers, use the extension methods shipped in `ReactiveMarbles.ObservableEvents.SourceGenerator` instead

- [Events](../../handbook/events.md)

When starting a WPF application from another application (e.g. Console) set `RxSchedulers.MainThreadScheduler` to a `DispatcherScheduler` after creating the WPF application on a separate thread, to avoid threading errors.

Use your normal WPF concepts that you would usually use in WPF development. There's also some extension methods which will make your life easier

- `ReactiveUserControl`
- `IViewFor`
- `RoutedViewHost`
- `AutoDataTemplateBindingHook`
