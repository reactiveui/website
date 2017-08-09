Title: Xamarin iOS
----

Your viewmodels should inherit from `ReactiveObject`

- https://reactiveui.net/api/reactiveui/reactiveobject/

Use `ISupportsActivation` and `WhenActivated` for lifecycle

- https://reactiveui.net/api/reactiveui/isupportsactivation/
- https://reactiveui.net/docs/handbook/when-activated/

Keep references to your subscriptions

- https://reactiveui.net/docs/concepts/reactive-programming/subscriptions#lifecycle

Use disposables to manage lifetime, scope and resources:

- https://reactiveui.net/docs/concepts/reactive-programming/disposables

Don't use eventhandlers, use the extension methods shipped in `reactiveui-events` instead

- https://reactiveui.net/docs/handbook/events/

Use your normal iOS concepts that you would usually use in iOS development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- https://reactiveui.net/api/reactiveui/reactivetableviewcontroller/
- https://reactiveui.net/api/reactiveui/reactivetableview/
- https://reactiveui.net/api/reactiveui/reactivecollectionview/
- https://reactiveui.net/api/reactiveui/reactivecollectionviewcontroller
- https://reactiveui.net/api/reactiveui/reactivenavigationcontroller
- https://reactiveui.net/api/reactiveui/reactivetabbarcontroller/
- https://reactiveui.net/api/reactiveui/reactivepageviewcontroller/
