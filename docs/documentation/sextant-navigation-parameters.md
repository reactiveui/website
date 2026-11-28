---
Order: 17
---
# Pass data between Sextant pages

An order list needs to tell its editor which order to open. When the editor closes,
the list needs to know whether the order was saved. Use the same
`IParameterViewStackService` for both calls so the editor and the list share a page stack.

## Open the editor

Register the view model under a contract when a type has several registrations.
A contract is the string the factory uses to choose between them. Here the contract is
`"editor"`, and the selected order travels separately in a `NavigationParameter`.

```csharp
var selected = new NavigationParameter { [OrderPage.Order] = 42 };
await navigation.PushPage<OrderPage>(selected, "editor").ToTask(token);
```

The editor implements `INavigable`. Its `WhenNavigatingTo` callback reads the number with
`parameter.GetValue(OrderPage.Order, 0)`. Sextant calls `WhenNavigatedTo` after the host
has pushed the page. For a parameter that may be absent, use
`TryGetValue<T>(key, out var value)`. A present value must have the type requested.

## Return the result

```csharp
var result = new NavigationParameter { [OrderPage.Saved] = true };
await navigation.PopPage(result, animate: false).ToTask(token);
```

When the host reports the pop, Sextant calls `WhenNavigatedFrom(result)` on the editor
and `Destroy()` if it implements `IDestructible`. The exposed page receives
`WhenNavigatedTo(result)`. It can update the list as soon as it sees the saved flag.
The parameter service dispatches these callbacks through the host's `MainThreadScheduler`.

## Read the stacks

Subscribe to `PageStack` or `ModalStack` to follow changes. Each sequence supplies the
current stack on subscription. On a nonempty stack, `TopPage()` or `TopModal()` gives
the current view model. Opening an editor with `PushModal` adds it to the modal stack,
and `PopModal()` closes it.

The example runs the page and modal flows on .NET 10 and .NET 11. Its local navigation
host shows how a view reports a back action through `IView.PagePopped`.
[Run the navigation example](https://github.com/reactiveui/Sextant/tree/main/src/examples/NavigationLifecycle).
