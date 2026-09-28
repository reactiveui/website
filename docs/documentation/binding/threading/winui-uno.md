---
Order: 5
---
# Uno and WinUI threading

The `ReactiveUI.Binding.Uno` package adds `WithUno` and `UnoBindingModule`. The module registers `UnoViewThreadInvoker`, which moves a write to an Uno control onto the thread that owns it. It targets every Uno Platform head: desktop, WebAssembly, Android, iOS and Windows.

The invoker derives from `SequencerViewThreadInvoker<DependencyObject, DispatcherQueueSequencer>`, as the [Avalonia invoker](avalonia.md) does:

- `Claims` accepts any `Microsoft.UI.Xaml.DependencyObject`, the base class of every Uno control.
- `SequencerFor` returns `DispatcherQueueSequencer.For(target.DispatcherQueue)`, from the `ReactiveUI.Primitives.Uno` package. Every dependency object belongs to the dispatcher queue of the thread that created it.
- `CheckAccess` is `true` on that queue's thread, and `Post` queues the write on it.

A generated binding onto an Uno control carries `UnoViewThreadInvoker.Instance` when your project references the package, so it routes writes even when the module is not registered. The [generated fallback](index.md#the-generated-fallback) explains how. An `Unsafe` binding uses the registered invokers only, so register the module when you use `Unsafe` bindings.

## WinUI

Uno and WinUI share the `Microsoft.UI.Xaml.DependencyObject` type. A plain WinUI app has no invoker package of its own. The generator does not report RXUIBIND017 for a WinUI binding, and a binding onto a WinUI control writes on the thread that raised the change. To move those writes, register an invoker that derives from `SequencerViewThreadInvoker<DependencyObject, DispatcherQueueSequencer>`, with `DispatcherQueueSequencer` from `ReactiveUI.Primitives.WinUI`, and call `ViewThreadInvokers.Refresh`.

WinUI and Uno dependency properties raise no `PropertyChanged`. The generator observes them through their dependency property. [Mechanisms](../mechanisms.md) covers how.
