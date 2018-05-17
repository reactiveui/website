Title: Xamarin Mac
----

This platform has two different base class libraries:

* Mobile which is a custom, super lean and restrictive subset of NET45 that excludes many Windowisms. 

> /Library/Frameworks/Xamarin.Mac.framework/Versions/Current/lib/mono/Xamarin.Mac/

* XM 4.5 which looks, acts and barks like a normal .NET program but doesn't include things like System.Drawing and System.Windows.Forms on OSX. This allows you to easily consume standard "desktop" libraries that target NET45 via NuGet as long as the library does not depend on assemblies not available in XM45.

> /Library/Frameworks/Xamarin.Mac.framework/Versions/Current/lib/mono/4.5/

_ReactiveUI-Events_ and _ReactiveUI_ use Xamarin.Mac Mobile but can be consumed into a Xamarin Mac XM 4.5 application. We would love to ship XM45 assemblies with the project but [it is currently impossible to do so](https://github.com/NuGet/Home/issues/2662).

Your viewmodels should inherit from `ReactiveObject`

- https://reactiveui.net/api/reactiveui/reactiveobject/

Use `ISupportsActivation` and `WhenActivated` for lifecycle

- https://reactiveui.net/api/reactiveui/isupportsactivation/
- https://reactiveui.net/docs/handbook/when-activated/

Keep references to your subscriptions

- https://reactiveui.net/docs/concepts/reactive-programming/subscriptions#lifecycle

Use disposables to manage lifetime, scope and resources:

- https://reactiveui.net/docs/concepts/reactive-programming/disposables

Don't use eventhandlers, use the extension methods shipped in `reactiveui.events` instead

- https://reactiveui.net/docs/handbook/events/

Use your normal iOS concepts that you would usually use in OSX development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- https://reactiveui.net/api/reactiveui/reactivetableviewcontroller/
- https://reactiveui.net/api/reactiveui/reactivetableview/
- https://reactiveui.net/api/reactiveui/reactivecollectionview/
- https://reactiveui.net/api/reactiveui/reactivecollectionviewcontroller
- https://reactiveui.net/api/reactiveui/reactivenavigationcontroller
- https://reactiveui.net/api/reactiveui/reactivetabbarcontroller/
- https://reactiveui.net/api/reactiveui/reactivepageviewcontroller/
