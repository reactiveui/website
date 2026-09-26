# Windows Forms

`ReactiveUI.WinForms` connects ReactiveUI to Windows Forms. Add it by following
[Installation](../../getting-started/installation/windows-forms.md). `ReactiveUI.WinForms` also ships as
`ReactiveUI.WinForms.Reactive`, built from the same source, for an app that uses System.Reactive. See
[Windows Forms](../../handbook/platforms/winforms.md) for a full walkthrough, built around a library loans desk.

## Guidelines

- **View models inherit from `ReactiveObject`.**
- **Use `IActivatableViewModel` and `WhenActivated` for lifecycle.** See [When Activated](../../handbook/when-activated.md).
- **Keep every subscription disposed.** See [Cleaning up subscriptions](../../../reactive-programming/observables.md#cleaning-up)
  and [Disposables](../../../primitives/disposables.md).
- **Convert WinForms events to streams instead of attaching event handlers.** The extension methods in
  `ReactiveUI.Primitives.ObservableEvents` turn an event into an `IObservable<T>`; see
  [Events](../../handbook/events.md) and [ObservableEvents](../../../primitives/observable-events/index.md).
- **A control that needs a view model derives from `ReactiveUserControl<TViewModel>`,** a `UserControl` that
  implements `IViewFor<TViewModel>`.
- **Host a routed page stack with `RoutedControlHost`, and a single view model with `ViewModelControlHost`.**
  Neither needs a hand-written `switch` over the current page or view model.
