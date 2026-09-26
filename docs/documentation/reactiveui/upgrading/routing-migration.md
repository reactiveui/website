---
Order: 6
---
# Migration Guide: Routing

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/routing/routing.csproj).

`RoutingState` reports navigation through three plain streams of values. A stream is an `IObservable<T>` that calls
your lambda with each new value once you subscribe to it:

- `CurrentViewModel` hands you the page on top of the stack.
- `NavigationStackChanged` hands you the whole stack after each change.
- `CanNavigateBack` hands you whether there is a page to go back to.

Earlier releases handed you `NavigationChanges`, a stream of change sets you had to decode. A change set is a batch
of adds and removes. The DynamicData helpers for routing lived in the separate `ReactiveUI.Routing` and
`ReactiveUI.Routing.Reactive` packages, and those packages pulled System.Reactive into an app that did not otherwise
use it. Views only ever needed the page on top, the stack, and whether they could go back, so each is now a stream of
exactly that value. Routing no longer depends on DynamicData, and the two packages are gone.

[Routing](../handbook/routing.md) covers routing in full. This guide maps the old members to the new ones.

## What moved where

| Old | New |
|---|---|
| `RoutingState.NavigationChanges` (`IObservable<IReactiveChangeSet<IRoutableViewModel>>`) | `RoutingState.NavigationStackChanged` (`IObservable<IReadOnlyList<IRoutableViewModel>>`) for the whole stack, or `NavigationStack.ToReactiveChangeSet()` for the same change sets as before |
| `RoutingState.CurrentViewModel` (`IObservable<IRoutableViewModel>`) | `RoutingState.CurrentViewModel` (`IObservable<IRoutableViewModel?>`). The values are unchanged; the type now says it delivers `null` while the stack is empty |
| Counting stack changes to enable a back button | `RoutingState.CanNavigateBack` (`IObservable<bool>`) |
| A subclass that assigns `NavigationChanges` | Assign `NavigationStackChanged`. `CurrentViewModel` and `CanNavigateBack` read it when you subscribe, so one stream drives all three |
| `ReactiveUI.Routing`, `ReactiveUI.Routing.Reactive` packages | Removed. Drop the package reference; the routing types stay in `ReactiveUI` and `ReactiveUI.Reactive` |

`NavigationStackChanged` delivers after each change only. `NavigationChanges` also delivered a first batch when you
subscribed. Read `NavigationStack` when you subscribe if you need the stack as it is at that moment.

## Replacements for the ReactiveUI.Routing helpers

| Old (`ReactiveUI.Routing`) | New (`ReactiveUI`) |
|---|---|
| `RoutingStateDynamicDataMixins.NavigationChanged()` | `RoutingState.NavigationStackChanged` |
| `DynamicDataChangeSetMixins.CountChanged()` on `IObservable<IChangeSet<T>>` | `WhenCountChanged()` on the `IObservable<IReactiveChangeSet<T>>` that `ToReactiveChangeSet()` returns |
| `DynamicDataChangeSetMixins.HasCountChanged()` on `IChangeSet` | `CountHasChanged()` on `IReactiveChangeSet<T>` |
| `DynamicDataAutoPersistMixins.ActOnEveryObject()` on `IObservable<IChangeSet<T>>` | `ActOnEveryObject()` on the collection itself, or on an `IObservable<IReactiveChangeSet<T>>` |
| `DynamicDataInteropMixins.ToDynamicDataChangeSet()` | No replacement. Call DynamicData's own `ToObservableChangeSet()` on the collection in your own code |

With `ReactiveUI.Routing` gone, a lean app no longer gets System.Reactive through DynamicData. If you called
`SubscribePrimitives` only to avoid the clash between the two `Subscribe` methods, a plain `Subscribe` compiles again.
An app that references DynamicData itself still gets System.Reactive, and the clash stays;
[Importing both in one file](../../primitives/system-reactive.md#importing-both-in-one-file) covers that case.

## Watch the stack instead of decoding change sets

**Before**

```csharp
using IDisposable subscription = shell.Router.NavigationChanges.Subscribe(static changeSet =>
{
    foreach (ReactiveChange<IRoutableViewModel> change in changeSet)
    {
        Console.WriteLine($"{change.Reason}: {change.Current.UrlPathSegment}");
    }
});
```

**After**

Most code wanted the stack itself. `NavigationStackChanged` hands you a read-only copy of it after each change, oldest
page first:

```csharp
AppShell shell = new();
using IDisposable subscription = shell.Router.NavigationStackChanged
    .Subscribe(static stack => Console.WriteLine(string.Join(" > ", stack.Select(static page => page.UrlPathSegment))));

TodoListPage list = await OpenListAsync(shell);
_ = await list.Open.Execute(list.Items[0]);
_ = await shell.Router.NavigateBack.Execute();
```

```text
todos
todos > todos/1
todos
```

Code that really needs each add and remove calls `ToReactiveChangeSet()` on `NavigationStack`. `NavigationStack` is an
`ObservableCollection<IRoutableViewModel>`, and `NavigationChanges` was built the same way:

```csharp
AppShell shell = new();
using IDisposable subscription = shell.Router.NavigationStack.ToReactiveChangeSet().Subscribe(static changeSet =>
{
    foreach (ReactiveChange<IRoutableViewModel> change in changeSet)
    {
        Console.WriteLine($"{change.Reason} at {change.CurrentIndex}: {change.Current.UrlPathSegment}");
    }
});
```

```text
Add at 0: todos
Add at 1: todos/1
Remove at 1: todos/1
```

## Animate a push or a pop

**Before**

```csharp
using IDisposable subscription = shell.Router.NavigationChanges.Subscribe(changeSet =>
{
    foreach (ReactiveChange<IRoutableViewModel> change in changeSet)
    {
        if (change.Reason == ReactiveChangeReason.Add)
        {
            SlideIn(change.Current);
        }
        else if (change.Reason == ReactiveChangeReason.Remove)
        {
            SlideOut(change.Current);
        }
    }
});
```

**After**

Compare the size of each copy of the stack with the one before it. A bigger stack is a push, a smaller one is a pop,
and `NavigateAndReset` shows up as an empty stack followed by a push:

```csharp
AppShell shell = new();
int previousCount = shell.Router.NavigationStack.Count;
using IDisposable subscription = shell.Router.NavigationStackChanged.Subscribe(stack =>
{
    string change = stack.Count switch
    {
        0 => "Cleared",
        int count when count > previousCount => $"Slide in {stack[^1].UrlPathSegment}",
        _ => $"Slide back to {stack[^1].UrlPathSegment}",
    };

    previousCount = stack.Count;
    Console.WriteLine(change);
});
```

```text
Slide in todos
Slide in todos/1
Slide back to todos
Cleared
Slide in todos
```

## Enable a back button

**Before**

```csharp
IObservable<bool> canGoBack = router.NavigationChanges
    .WhenCountChanged()
    .Select(_ => router.NavigationStack.Count > 1);
```

**After**

`CanNavigateBack` delivers the answer when you subscribe, then again only when it changes. It is the same stream
`NavigateBack` uses for its `CanExecute`:

```csharp
AppShell shell = new();
using IDisposable subscription = shell.Router.CanNavigateBack.Subscribe(Console.WriteLine);

TodoListPage list = await OpenListAsync(shell);
_ = await list.Open.Execute(list.Items[0]);
_ = await shell.Router.NavigateBack.Execute();
_ = await list.Open.Execute(list.Items[1]);
```

```text
False
True
False
True
```

## Handle a null current page

`CurrentViewModel` always delivered `null` while the stack was empty. Its type now says so. With nullable reference
types turned on, the compiler warns (CS8604) where your code passes the value on without a check.

**Before**

```csharp
using IDisposable host = shell.Router.CurrentViewModel
    .Select(page => locator.ResolveView<object>(page, null))
    .Subscribe(view => Show(view));
```

**After**

```csharp
AppShell shell = new();
IViewLocator locator = ViewLocator.GetCurrent();
using IDisposable host = shell.Router.CurrentViewModel
    .Select(page => page is null ? null : locator.ResolveView<object>(page, null))
    .Subscribe(static view => Console.WriteLine(view?.GetType().Name ?? "(default content)"));
```

```text
(default content)
TodoListPageView
TodoDetailPageView
TodoListPageView
```

## Filter change sets by count

**Before**

```csharp
using IDisposable subscription = router.NavigationChanged()
    .CountChanged()
    .SubscribePrimitives(batch => Console.WriteLine(batch.HasCountChanged()));
```

**After**

For the navigation stack, `NavigationStackChanged` already delivers once per change, and each copy carries its
`Count`. For any other collection, `WhenCountChanged()` on `ToReactiveChangeSet()` passes on only the batches that add
or remove an item, and `CountHasChanged()` asks one batch the same question:

```csharp
ObservableCollection<Product> inventory = [new Product("Kettle", 4)];
List<IReactiveChangeSet<Product>> countChangingBatches = [];
using IDisposable subscription = inventory.ToReactiveChangeSet().WhenCountChanged().Subscribe(countChangingBatches.Add);
```

```csharp
foreach (IReactiveChangeSet<Product> batch in allBatches)
{
    Console.WriteLine(batch.CountHasChanged());
}
```

[Collections](../handbook/collections.md#watch-for-a-count-change) covers both.

## Act on every page in the stack

**Before**

```csharp
using IDisposable subscription = router.NavigationChanged().ActOnEveryObject(
    page => log.Add($"enter {page.UrlPathSegment}"),
    page => log.Add($"leave {page.UrlPathSegment}"));
```

**After**

Call `ActOnEveryObject` on `NavigationStack` itself:

```csharp
AppShell shell = new();
using IDisposable subscription = shell.Router.NavigationStack.ActOnEveryObject(
    static page => Console.WriteLine($"enter {page.UrlPathSegment}"),
    static page => Console.WriteLine($"leave {page.UrlPathSegment}"));

TodoListPage list = await OpenListAsync(shell);
_ = await list.Open.Execute(list.Items[0]);
_ = await shell.Router.NavigateBack.Execute();
```

```text
enter todos
enter todos/1
leave todos/1
leave todos
```

The collection overload calls the leave method for every page still on the stack when you dispose the subscription.
That is the last line above. The DynamicData overload did not do that. If you want the old behaviour, call
`ActOnEveryObject` on `NavigationStack.ToReactiveChangeSet()` instead: the change-set overload only unsubscribes.

## A page on the stack twice

`WhenNavigatedTo`, `WhenNavigatedToObservable` and `WhenNavigatingFromObservable` keep their rules. They react when the
stack changes size, and the two observable forms complete when their page leaves the stack. One edge case changed.
The same page object can sit on the stack more than once. The old streams completed as soon as any copy was removed.
The new ones complete only when the last copy leaves:

```csharp
RecipeShell shell = new();
RecipeListPage list = new(shell, RecipeBook.Seeded());

using IDisposable arrivals = list.WhenNavigatedToObservable().Subscribe(
    static _ => Console.WriteLine("List arrived"),
    static () => Console.WriteLine("List completed"));

_ = await shell.Router.Navigate.Execute(list);
_ = await list.Open.Execute(list.Recipes[0]);

// An "All recipes" link on the detail page opens the same list page again.
_ = await shell.Router.Navigate.Execute(list);
_ = await shell.Router.NavigateBack.Execute();
Console.WriteLine("Back on the detail page");

_ = await shell.Router.NavigateAndReset.Execute(new RecipeDetailPage(shell, list.Recipes[1]));
```

```text
List arrived
List arrived
Back on the detail page
List completed
```

Going back past the second copy leaves the first one underneath, so the stream keeps running. Only
`NavigateAndReset`, which clears the stack, completes it.

## Where to go next

- [Routing](../handbook/routing.md)
- [Collections](../handbook/collections.md)
- [Dispose your subscriptions](../guidelines/framework/dispose-your-subscriptions.md)
