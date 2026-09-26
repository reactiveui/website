# .NET MAUI

.NET MAUI is Microsoft's cross-platform framework for building native mobile and desktop apps with C# and XAML.
`ReactiveUI.Maui` connects ReactiveUI to it. Add the package by following
[Installation](../../getting-started/installation/maui.md). `ReactiveUI.Maui` also ships as
`ReactiveUI.Maui.Reactive`, built from the same source, for an app that uses System.Reactive. See
[.NET MAUI](../../handbook/platforms/maui.md) for a full walkthrough, built around a recipe book, covering the app
builder, binding a page to a view model, navigation and activation.

## Guidelines

- **View models inherit from `ReactiveObject`.** They stay free of any MAUI type, so you can unit test them
  without a device or an emulator; see [Testing](../../handbook/testing.md).
- **Pages and controls inherit from the reactive base classes.** `ReactiveContentPage<TViewModel>`,
  `ReactiveContentView<TViewModel>`, `ReactiveNavigationPage<TViewModel>`, `ReactiveTabbedPage<TViewModel>`,
  `ReactiveFlyoutPage<TViewModel>`, `ReactiveShell<TViewModel>` and `ReactiveWindow<TViewModel>` each implement
  `IViewFor<TViewModel>`. Each one exposes a `ViewModel` property that MAUI's data binding can use.
- **Use `IActivatableViewModel` and `WhenActivated` for lifecycle.** See [When Activated](../../handbook/when-activated.md).
- **Keep every subscription disposed.** See [Cleaning up subscriptions](../../../reactive-programming/observables.md#cleaning-up)
  and [Disposables](../../../primitives/disposables.md).
- **Navigate with a router, or with MAUI Shell, but be consistent about which one owns navigation.** The handbook
  page shows both: a `Router` navigated through `RoutedViewHost`, and MAUI's own Shell routes side by side.
- **A custom control that needs a view model implements `IViewFor<TViewModel>` itself.** It does this the same way
  the platform's own reactive base classes do. The handbook page shows this for a custom item view.
- **`ReactiveUI.SourceGenerators` removes the boilerplate around reactive properties, computed properties and
  commands** on any platform, including MAUI; see [Source Generators](../../../source-generators/index.md).

## Platform APIs

MAUI's own platform APIs, such as `Geolocation` and `Connectivity`, are not part of ReactiveUI. Wrap a call to one
in `Signal.FromAsync`. Convert an event such as `Connectivity.ConnectivityChanged` to a stream with `ToObservable`,
the same way you would wrap any other event-based API.
