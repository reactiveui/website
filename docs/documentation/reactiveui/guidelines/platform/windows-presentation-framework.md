# Windows Presentation Framework guidelines

See [WPF](wpf-overview.md) for an introduction to the package. See
[WPF](../../handbook/platforms/wpf.md) for a full walkthrough, built around a university grade book. This page
lists the WPF-specific practices worth calling out on their own.

- **View models inherit from `ReactiveObject`.**
- **Use `IActivatableViewModel` and `WhenActivated` for lifecycle.** See [When Activated](../../handbook/when-activated.md).
- **Keep every subscription disposed.** See [Cleaning up subscriptions](../../../reactive-programming/observables.md#cleaning-up)
  and [Disposables](../../../primitives/disposables.md).
- **Convert WPF events to streams instead of attaching event handlers.** The extension methods in
  `ReactiveUI.Primitives.ObservableEvents` turn an event into an `IObservable<T>`. See
  [Events](../../handbook/events.md) and [ObservableEvents](../../../primitives/observable-events/index.md).
- **A window, page or control that needs a view model derives from `ReactiveWindow<TViewModel>`,
  `ReactivePage<TViewModel>` or `ReactiveUserControl<TViewModel>`.** Each of these implements
  `IViewFor<TViewModel>` with a `ViewModel` dependency property.
- **Host a routed page stack with `RoutedViewHost`.** Host a single view model with `ViewModelViewHost`.
- **Starting WPF from something other than its own `Application` entry point needs a scheduler set by hand.** A
  console host that creates the WPF application on its own thread has no dispatcher yet when the process starts.
  Once that thread's dispatcher exists, set `RxSchedulers.MainThreadScheduler` to a dispatcher-backed sequencer.
  Do this before anything schedules work onto it. `WithWpfScheduler` (part of `WithWpf`) needs that same
  dispatcher to already exist.
- **A control bound without a source-generated `Bind` uses `BindWithValidation` instead.** This applies to a
  control whose target is resolved by name at run time.
