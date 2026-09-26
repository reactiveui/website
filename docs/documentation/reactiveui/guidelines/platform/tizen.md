# Tizen

ReactiveUI ships no Tizen-specific package. `ReactiveUI.Maui`'s target framework list, and every other platform
package's, does not include a Tizen target. There is no `ReactiveUI.Tizen`, and no Tizen head of `ReactiveUI.Maui`,
to add to a project.

`ReactiveUI` (the core package) and `ReactiveUI.Binding` have no UI framework dependency, so a Tizen app can still
use `ReactiveObject`, `ReactiveCommand`, [routing](../../handbook/routing.md) and
[data binding](../../../binding/index.md). What a platform package would otherwise supply, an app has to set up
by hand:

- **A main-thread sequencer for Tizen's UI thread.** [Scheduling](../../handbook/scheduling.md) describes how to
  build one for a platform with no package of its own. Set `RxSchedulers.MainThreadScheduler` to it directly, or
  pass it to `WithMainThreadScheduler` on the [app builder](../../handbook/rxappbuilder.md).
- **An activation fetcher**, so [`WhenActivated`](../../handbook/when-activated.md) knows when a Tizen page
  appears and disappears. Without one, run activation-scoped setup and teardown by hand instead.
- **View base classes that implement `IViewFor<TViewModel>`**, so a view exposes a `ViewModel` property the way
  every other platform's views do.
