# Android

`ReactiveUI.AndroidX` connects ReactiveUI to Android through the AndroidX support libraries. Add it by following
[Installation](../../getting-started/installation/androidx.md). `ReactiveUI.AndroidX` also ships as
`ReactiveUI.AndroidX.Reactive`, built from the same source, for an app that uses System.Reactive.
[Android](../../handbook/platforms/android.md) walks through every pattern below with a school timetable app. The core
`ReactiveUI` package's Android build adds `ReactiveActivity`, `SharedPreferencesExtensions` and
`UsbManagerExtensions` for apps that do not use AndroidX.

## Guidelines

- **View models inherit from `ReactiveObject`.** They stay free of any Android type, so you can unit test them
  without a device or an emulator.
- **Activities, fragments and dialogs inherit from the reactive base classes.** `ReactiveAppCompatActivity<TViewModel>`,
  `ReactiveFragmentActivity<TViewModel>`, `ReactiveFragment<TViewModel>`, `ReactiveDialogFragment<TViewModel>` and
  `ReactivePreferenceFragment<TViewModel>` each implement `IViewFor<TViewModel>`. Each one exposes a `ViewModel`
  property, so you never wire one up by hand.
- **Wire up controls with `WireUpControls()`, not `FindViewById`.** `ControlFetcherMixins.WireUpControls` reads a
  view's properties by reflection and finds each one's matching resource by name, once, instead of a
  `FindViewById` call per control. Its `ResolveStrategy` decides which properties it wires: `Implicit` wires every
  writable `View`-typed property, `ExplicitOptIn` wires only properties marked to opt in, and `ExplicitOptOut`
  wires every property except those marked to opt out.
- **Use `IActivatableViewModel` and `WhenActivated` for lifecycle.** See [When Activated](../../handbook/when-activated.md)
  for how a subscription's lifetime follows the view's.
- **Keep every subscription disposed.** See [Cleaning up subscriptions](../../../reactive-programming/observables.md#cleaning-up)
  and [Disposables](../../../primitives/disposables.md).
- **Convert Android events to streams instead of attaching event handlers.** The extension methods in
  `ReactiveUI.Primitives.ObservableEvents` turn an event into an `IObservable<T>`. Compose it with the rest of a
  view model's streams; see [Events](../../handbook/events.md) and
  [ObservableEvents](../../../primitives/observable-events/index.md).
- **Bind a list without a hand-written adapter.** `ReactiveRecyclerViewAdapter<TViewModel, TCollection>` and
  `ReactiveRecyclerViewViewHolder` bind a `RecyclerView` to a collection of view models. `ReactivePagerAdapter<TViewModel, TCollection>`
  does the same for a `ViewPager`.
- **Await an activity result instead of overriding `OnActivityResult`.** `StartActivityForResultAsync` on the reactive
  activity base classes turns a launched activity's result into a `Task`, so the calling code reads like ordinary
  asynchronous code.
