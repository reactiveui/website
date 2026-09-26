# Data Binding on iOS and macOS

iOS, Mac Catalyst, tvOS and macOS views come from UIKit or AppKit, neither of which gives a view a built-in way
to raise a change notification. To bind a view to a view model, implement `IViewFor<TViewModel>` on the view.
The core `ReactiveUI` package ships base classes that already do this. It builds them for the `ios`,
`maccatalyst`, `macos` and `tvos` target frameworks, so there is no separate iOS package to install beyond
`ReactiveUI` itself.

Across all four of those platforms, `ReactiveViewController<TViewModel>`, `ReactiveView<TViewModel>`,
`ReactiveControl<TViewModel>`, `ReactiveImageView<TViewModel>` and `ReactiveSplitViewController<TViewModel>`
wrap `UIViewController`, `UIView`, `UIControl`, `UIImageView` and `UISplitViewController` on Apple's platforms
with a `ViewModel` property and its change notifications. `ViewModelViewHost` shows whatever view model you
assign it, resolving the matching view through the view locator, the same way `ViewModelControlHost` does on
other platforms.

iOS, Mac Catalyst and tvOS add a UIKit-specific layer on top. `ReactiveTableViewController<TViewModel>` and
`ReactiveCollectionViewController<TViewModel>` pair with `ReactiveTableViewSource` and
`ReactiveCollectionViewSource` to bind a collection of view models into a `UITableView` or a `UICollectionView`.
`ReactiveNavigationController<TViewModel>`, `ReactiveTabBarController<TViewModel>` and
`ReactivePageViewController<TViewModel>` wrap the corresponding UIKit containers. `RoutedViewHost` follows a
router's navigation stack, the way `RoutedControlHost` does elsewhere. macOS instead adds
`ReactiveWindowController`, an `NSWindowController` with a `ViewModel` property, since AppKit has no direct
equivalent of a `UITableViewController` in this surface.

Derive your view controller, view or window controller from the matching base class and give it a `ViewModel`
property. The base class raises `PropertyChanged` for you, the same way `ReactiveObject` does on a view model.
From there, binding a control to a property works exactly as it does on any other platform.

## Which views the hosts find

`ViewModelViewHost` and `RoutedViewHost` ask the [view locator](../view-location/index.md) for a view in two steps,
and neither step uses reflection. The first step is the view lookup the ReactiveUI.Binding source generator writes
for every view class in your project that implements `IViewFor<T>`. The second is the views you add to the view
locator with `Map`. So both hosts are safe to trim and to compile ahead of time, which matters on iOS, where apps
are compiled ahead of time.

A view registered only in Splat's service locator, whose class the generator never sees, sits outside both steps.
For that view, each host has an Unsafe twin. A twin asks both steps first, then the service locator for `IViewFor<T>`
closed over the view model's run-time type. Building that type while the app runs needs code the compiler never
generated, so each twin is marked `[RequiresDynamicCode]`.

| AOT-safe type | Unsafe twin | Platforms | When the view is only in the service locator |
| --- | --- | --- | --- |
| `ViewModelViewHost` | `ViewModelViewHostUnsafe` | iOS, Mac Catalyst, tvOS and macOS | The safe host throws an `InvalidOperationException` that names the twin. |
| `RoutedViewHost` | `RoutedViewHostUnsafe` | iOS, Mac Catalyst and tvOS | The safe host throws an `InvalidOperationException` that names the twin. |

Prefer to keep the default hosts and bridge the registration with
`locator.CreateMappingBuilder().MapFromServiceLocator<TViewModel, IViewFor<TViewModel>>()`. It adds a `Map` entry
whose view comes from the service locator, so the default hosts find it in their second step.
[View location](../view-location/index.md#which-lookup-the-view-hosts-use) covers both lookups.
