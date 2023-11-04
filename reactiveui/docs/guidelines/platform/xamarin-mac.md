---
Title: Xamarin Mac
---

This platform has two different base class libraries:

* Mobile which is a custom, super lean and restrictive subset of NET45 that excludes many Windowisms. 

> /Library/Frameworks/Xamarin.Mac.framework/Versions/Current/lib/mono/Xamarin.Mac/

* XM 4.5 which looks, acts and barks like a normal .NET program but doesn't include things like System.Drawing and System.Windows.Forms on OSX. This allows you to easily consume standard "desktop" libraries that target NET45 via NuGet as long as the library does not depend on assemblies not available in XM45.

> /Library/Frameworks/Xamarin.Mac.framework/Versions/Current/lib/mono/4.5/

_ReactiveUI-Events_ and _ReactiveUI_ use Xamarin.Mac Mobile but can be consumed into a Xamarin Mac XM 4.5 application. We would love to ship XM45 assemblies with the project but [it is currently impossible to do so](https://github.com/NuGet/Home/issues/2662).

Your viewmodels should inherit from `ReactiveObject`

- [ReactiveObject](~/api/ReactiveUI.ReactiveObject.yml)

Use `IActivatableViewModel` and `WhenActivated` for lifecycle

- [IActivatableViewModel](~/api/ReactiveUI.IActivatableViewModel.yml)
- [When Activated](~/docs/handbook/when-activated.md)

Keep references to your subscriptions

- [Reactive Programming#lifecycle](~/docs/reactive-programming/index.md#lifecycle)

Use disposables to manage lifetime, scope and resources:

- [Reactive Programming#disposables](~/docs/reactive-programming/index.md#disposables)

Don't use eventhandlers, use the extension methods shipped in `ReactiveMarbles.ObservableEvents.SourceGenerator` instead

- [Events](~/docs/handbook/events.md)

Use your normal iOS concepts that you would usually use in OSX development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- [ReactiveTableViewController](~/api/ReactiveUI.ReactiveTableViewController.yml)
- [ReactiveTableView](~/api/ReactiveUI.ReactiveTableView.yml)
- [ReactiveCollectionView](~/api/ReactiveUI.ReactiveCollectionView-1.yml)
- [ReactiveCollectionViewController](~/api/ReactiveUI.ReactiveCollectionViewController.yml)
- [ReactiveNavigationController](~/api/ReactiveUI.ReactiveNavigationController.yml)
- [ReactiveTabBarController](~/api/ReactiveUI.ReactiveTabBarController.yml)
- [ReactivePageViewController](~/api/ReactiveUI.ReactivePageViewController.yml)
