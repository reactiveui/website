Title: Xamarin Forms
---

Ensure that you install `reactiveui.xamforms` into your applications.

Your viewmodels should inherit from `ReactiveObject`

- https://reactiveui.net/api/reactiveui/reactiveobject/

Use wireupcontrols

- https://reactiveui.net/docs/handbook/data-binding/xamarin-android/wire-up-controls

Use `ISupportsActivation` and `WhenActivated` for lifecycle

- https://reactiveui.net/api/reactiveui/isupportsactivation/
- https://reactiveui.net/docs/handbook/when-activated/

Keep references to your subscriptions

- https://reactiveui.net/docs/reactive-programming#lifecycle

Use disposables to manage lifetime, scope and resources:

- https://reactiveui.net/docs/reactive-programming#disposables

Don't use eventhandlers, use the extension methods shipped in `reactiveui.events.xamforms` instead

- https://reactiveui.net/docs/handbook/events/

Use your normal Xamarin Forms concepts that you would usually use in  Xamarin Forms development, we have some base classes which you should use as they expose observables such as `Changed`, `Changing` and `Deactivated` that can be used for composition.

- https://reactiveui.net/api/reactiveui.xamforms/reactivecarouselpage_1/
- https://reactiveui.net/api/reactiveui.xamforms/reactivecontentpage_1/
- https://reactiveui.net/api/reactiveui.xamforms/reactivecontentview_1/
- https://reactiveui.net/api/reactiveui.xamforms/reactiveentrycell_1/
- https://reactiveui.net/api/reactiveui.xamforms/reactiveimagecell_1/
- https://reactiveui.net/api/reactiveui.xamforms/reactivemasterdetailpage_1/
- https://reactiveui.net/api/reactiveui.xamforms/reactivenavigationpage_1/
- https://reactiveui.net/api/reactiveui.xamforms/reactiveswitchcell_1/
- https://reactiveui.net/api/reactiveui.xamforms/reactivetabbedpage_1/
- https://reactiveui.net/api/reactiveui.xamforms/reactivetextcell_1/
- https://reactiveui.net/api/reactiveui.xamforms/reactiveviewcell_1/
- https://reactiveui.net/api/reactiveui.xamforms/routedviewhost/
- https://reactiveui.net/api/reactiveui.xamforms/viewmodelviewhost/


