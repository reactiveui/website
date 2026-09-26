---
Order: 9
---
# View Location

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/view-location/view-location.csproj).

An app built around view models has a router, a navigation stack or a list that holds view models, not views.
Something still has to turn each view model into the screen that shows it, and set that screen's `ViewModel`
property before it appears. Writing that lookup by hand for every screen does not scale as an app grows.

A *view locator* is the object that does this lookup. You give it a view model, and it gives you back the view
for it, with the view model already set. [Extending IViewFor](extending-iviewfor.md) covers implementing
`IViewFor<T>` on a view; this page covers finding that view once it exists. `DefaultViewLocator`,
`ViewLocator`, `ViewMappingBuilder` and `ViewLocatorNotFoundException` come from `ReactiveUI.Binding`, the
package `ReactiveUI` builds on. [Views](../../../binding/views.md) documents them, along with the source generator
that writes most of the lookup for you. This page shows how an app that uses routing and view models puts them
to work. `IViewModule` and `WithViewModule` are part of `ReactiveUI` itself.

```mermaid
%%{init: {"theme": "base", "themeVariables": {"fontFamily": "Roboto, Helvetica, Arial, sans-serif", "fontSize": "15px", "primaryColor": "#DCE9FF", "primaryBorderColor": "#6C8EC4", "primaryTextColor": "#0B2447", "secondaryColor": "#E3F2E8", "secondaryBorderColor": "#7FA88C", "secondaryTextColor": "#12301C", "tertiaryColor": "#F3E5F5", "tertiaryBorderColor": "#A98BB0", "tertiaryTextColor": "#2E1437", "lineColor": "#7B8699", "textColor": "#1B1F27", "noteBkgColor": "#FFF4D6", "noteBorderColor": "#C9A94F", "noteTextColor": "#3A2A00", "actorBkg": "#DCE9FF", "actorBorder": "#6C8EC4", "actorTextColor": "#0B2447", "signalColor": "#7B8699", "signalTextColor": "#1B1F27", "labelBoxBkgColor": "#F1F3F8", "labelBoxBorderColor": "#A7AEBB", "edgeLabelBackground": "#F7F9FC", "clusterBkg": "#F7F9FC", "clusterBorder": "#C9D1DE"}}}%%
flowchart LR
    classDef view fill:#DCE9FF,stroke:#6C8EC4,color:#0B2447
    classDef vm fill:#E3F2E8,stroke:#7FA88C,color:#12301C
    classDef model fill:#F3E5F5,stroke:#A98BB0,color:#2E1437
    classDef warn fill:#FDE7E4,stroke:#C98A82,color:#410E0B
    classDef neutral fill:#F1F3F8,stroke:#A7AEBB,color:#1B1F27
    VM(["View model"]):::vm -- "ResolveView(viewModel, contract)" --> Locator(["View locator"]):::neutral
    Locator -- "looks up by type and contract" --> Map(["Registered mapping"]):::neutral
    Map -- "builds or reuses" --> View(["View, ViewModel set"]):::view
```

The locator matches on the view model's type and, optionally, a contract, and it always sets the view's
`ViewModel` before handing it back.

## Ask the locator for a view

**1. Get the locator the app registered.** `ViewLocator.GetCurrent()` returns the `IViewLocator` an app's
builder set up. Call it once you know the app has finished starting.

**2. Resolve the view.** `ResolveView` takes the view model and returns its view, with `ViewModel` already set
to the object you passed.

```csharp
using TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());

IViewFor? view = ViewLocator.GetCurrent().ResolveView(viewModel);

Console.WriteLine(view?.GetType().Name);
Console.WriteLine(ReferenceEquals(view?.ViewModel, viewModel));
```

```text
TodoListView
True
```

**3. Read the result.** The example registered no mapping for `TodoListViewModel`. `ReactiveUI.Binding`'s
source generator found `TodoListView` while the project built and added it to the lookup, so the call above
needed no setup. `ResolveView` returns `null` when nothing matches; a router checks for that before it shows a
screen.

`ResolveView` has a generic overload, `ResolveView<TViewModel>(viewModel, contract)`, that reads the view
model's type at compile time, and it also has `ResolveView(object?, contract)` for a navigation stack that
holds view models as `object`. The generic overload also asks the service locator when no generated or mapped
view answers, so it is safe in a trimmed or Native AOT app. The `object` overload stops after the generated
lookup and the `Map` registrations: closing `IViewFor<>` over a runtime type needs code the compiler never saw,
so it never asks the service locator, and it logs a warning when neither tier answers. `ResolveViewUnsafe`
adds the service locator as a third tier for a view model held as an `object`, and it carries
`[RequiresDynamicCode]` because it closes `IViewFor<>` with reflection. [Reflection](../reflection.md) covers
what that attribute means for your app. [Reach a view registered only in the service
locator](../../../binding/views.md#reach-a-view-registered-only-in-the-service-locator) covers the warning, the
fix and the RXUIBIND020 analyzer rule that catches it at build time.

## Which lookup the view hosts use

The non-generic view hosts on every platform hold their view model as an `object`, so they call
`ResolveView(object?, contract)`. That covers `ViewModelViewHost` and `RoutedViewHost`, WinForms'
`ViewModelControlHost` and `RoutedControlHost`, and the item template `AutoDataTemplateBindingHook` assigns. They find every view in the generated lookup and every view
you add with `Map`, with no reflection, so they are safe to trim and to publish with Native AOT.

Each of those hosts has an Unsafe twin, such as `ViewModelViewHostUnsafe` or `RoutedControlHostUnsafe`, that calls
`ResolveViewUnsafe` instead. Use a twin only for a view that Splat's service locator alone knows. For that view you
have two choices:

| Choice | Safe for Native AOT | What you change |
| --- | --- | --- |
| Bridge the registration with `MapFromServiceLocator` | Yes | One call on the view locator while the app starts; every default host then finds the view. |
| Use the Unsafe twin of the host | No: marked `[RequiresDynamicCode]` | Swap the host type where that view is shown. |

The bridge is one line on the app's view locator, with the view's service-locator type as `TView`:

```csharp
_ = locator.CreateMappingBuilder().MapFromServiceLocator<RadarImageViewModel, IViewFor<RadarImageViewModel>>();
```

Each platform page shows its twins: [WinUI](../platforms/winui.md#which-views-the-hosts-find),
[WPF](../platforms/wpf.md#show-a-view-only-the-service-locator-knows),
[Windows Forms](../platforms/winforms.md#show-a-view-only-the-service-locator-knows),
[.NET MAUI](../platforms/maui.md#show-a-view-only-the-service-locator-knows) and
[iOS and macOS](../data-binding/ios.md#which-views-the-hosts-find).

## Choose a view by contract

A view model can have more than one screen: a compact list next to a full one, for example. A *contract* is a
string that picks between them. Passing `null` selects the default screen.

```csharp
using TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());
IViewLocator locator = ViewLocator.GetCurrent();

Console.WriteLine(locator.ResolveView(viewModel, ViewContracts.Compact)?.GetType().Name);
Console.WriteLine(locator.ResolveView(viewModel, ViewContracts.Print)?.GetType().Name ?? "(none)");
```

```text
CompactTodoListView
(none)
```

A contract with no matching view resolves to `null`, not to the default screen picked by dropping the
contract. A host decides whether to fall back to `ResolveView(viewModel)` itself. [Choose a view by
contract](../../../binding/views.md#choose-a-view-by-contract) covers `[ViewContract]`. It also covers the rules
for a view model whose only view carries a contract.

## Group a feature's views in a module

An app grows by feature, and a feature's views should register themselves rather than have the app list every
one of them by hand. `IViewModule` is a small interface for that: implement `RegisterViews` and map the
feature's views with a `ViewMappingBuilder`.

```csharp
public sealed class GitHubViewModule : IViewModule
{
    /// <inheritdoc/>
    public void RegisterViews(DefaultViewLocator locator) =>
        locator.CreateMappingBuilder()
            .Map<RepositorySearchViewModel, RepositorySearchView>();
}
```

Add the module once, while the app starts, with `WithViewModule<TModule>()` on the app's builder.
[RxAppBuilder](../rxappbuilder.md) covers the rest of that builder. Call `WithViewModule` after the step that
registers a `DefaultViewLocator`, such as `WithCoreServices` or a platform module. Called earlier, it throws
`InvalidOperationException`, because it has no locator to add the module's views to.

```csharp
ExampleApp.Start(static builder => _ = builder.WithViewModule<GitHubViewModule>());
```

Resolving a view model from that feature works the same as any other.

```csharp
using RepositorySearchViewModel viewModel = new(new InMemoryGitHubApi());

IViewFor? view = ViewLocator.GetCurrent().ResolveView(viewModel);

Console.WriteLine(view?.GetType().Name);
```

```text
RepositorySearchView
```

`WithViewModule<TModule>` requires a parameterless constructor on `TModule`. It creates one instance of the
module and calls `RegisterViews` on it immediately, so the module's mappings are ready before the rest of the
app runs.

## Map a view without building a view model first

`DefaultViewLocator.Map` registers a view for a view model type on one locator instance. `ResolveView<T>()`
with no view model argument resolves it too, which is useful when you only need to know which view a route
maps to, not to show it yet. The view it returns has no view model set.

```csharp
DefaultViewLocator locator = new();
locator.Map<TimetableViewModel, DayTimetableView>();
locator.Map<TimetableViewModel, WeekTimetableView>(ViewContracts.Week);

IViewFor<TimetableViewModel>? day = locator.ResolveView<TimetableViewModel>();
IViewFor<TimetableViewModel>? week = locator.ResolveView<TimetableViewModel>(ViewContracts.Week);

Console.WriteLine(day?.GetType().Name);
Console.WriteLine(week?.GetType().Name);
Console.WriteLine(day?.ViewModel is null);
```

```text
DayTimetableView
WeekTimetableView
True
```

A mapping belongs to the `DefaultViewLocator` instance you call `Map` on. Registering one on a locator built
with `new DefaultViewLocator()` for a test, for example, does not affect the locator the app shares.

## Map and unmap a view at run time

An app can add and remove mappings while it runs, such as showing a staff-only screen only while a teacher is
signed in. `Map<TViewModel>(factory)` builds the view from a factory instead of a parameterless constructor,
and `Unmap` removes a mapping and reports whether one existed.

```csharp
DefaultViewLocator locator = new();
TeacherAdminViewModel admin = new("Ms. Ito");

locator.Map<TeacherAdminViewModel>(static () => new TeacherAdminView());
locator.Map<TeacherAdminViewModel>(static () => new TeacherAdminView(), ViewContracts.Admin);

Console.WriteLine(locator.ResolveView(admin)?.GetType().Name);
Console.WriteLine(locator.ResolveView(admin, ViewContracts.Admin)?.GetType().Name);

bool unmappedDefault = locator.Unmap<TeacherAdminViewModel>();
bool unmappedAdmin = locator.Unmap<TeacherAdminViewModel>(ViewContracts.Admin);

Console.WriteLine(locator.ResolveView(admin)?.GetType().Name ?? "(none)");
Console.WriteLine(unmappedDefault);
Console.WriteLine(unmappedAdmin);
```

```text
TeacherAdminView
TeacherAdminView
(none)
True
True
```

After sign-out, the locator finds nothing for the view model again, the same as before it was ever mapped. The
factory runs on every resolve, so each call to `ResolveView` after signing in again would build a new
`TeacherAdminView`.

## Chain mappings with a builder, including views from the service locator

`CreateMappingBuilder` returns a `ViewMappingBuilder` for the locator. Its `Map` overloads mirror
`DefaultViewLocator.Map` and each returns the builder, so a screen's mappings read as one chain.
`MapFromServiceLocator<TViewModel, TView>` maps a view that another part of the app already registered with
Splat's service locator, rather than building it itself.

```csharp
AppLocator.Register(static () => new SchoolProfileView());
AppLocator.Register(static () => new PrintableProfileView());

DefaultViewLocator locator = new();
locator.CreateMappingBuilder()
    .Map<TimetableViewModel, WeekTimetableView>(ViewContracts.Week)
    .Map<TeacherAdminViewModel>(static () => new TeacherAdminView())
    .Map<TeacherAdminViewModel>(static () => new TeacherAdminView(), ViewContracts.Admin)
    .MapFromServiceLocator<SchoolProfileViewModel, SchoolProfileView>()
    .MapFromServiceLocator<SchoolProfileViewModel, PrintableProfileView>(ViewContracts.Print);

TeacherAdminViewModel admin = new("Ms. Ito");
SchoolProfileViewModel profile = new("Aiko Tanaka");

Console.WriteLine(locator.ResolveView<TimetableViewModel>(ViewContracts.Week)?.GetType().Name);
Console.WriteLine(locator.ResolveView(admin)?.GetType().Name);
Console.WriteLine(locator.ResolveView(admin, ViewContracts.Admin)?.GetType().Name);
Console.WriteLine(locator.ResolveView(profile)?.GetType().Name);
Console.WriteLine(locator.ResolveView(profile, ViewContracts.Print)?.GetType().Name);
Console.WriteLine(ReferenceEquals(locator.ResolveView(profile)?.ViewModel, profile));
```

```text
WeekTimetableView
TeacherAdminView
TeacherAdminView
SchoolProfileView
PrintableProfileView
True
```

`MapFromServiceLocator` asks the service locator each time the mapping resolves; a view that is not registered
there throws `InvalidOperationException` instead of resolving to nothing. Use it for a view a service locator
already owns, such as one another Splat-registered module built.

## Handle a missing locator

An app has to register a locator before anything can call `ViewLocator.GetCurrent()`. Calling it too early, or
in an app that never registered one, throws `ViewLocatorNotFoundException` with a message naming the calls
that fix it.

```csharp
ViewLocatorNotFoundException defaultMessage = new();
ViewLocatorNotFoundException customMessage = new("No view locator was registered for the timetable module.");
InvalidOperationException containerDisposed = new("The module container was disposed.");
ViewLocatorNotFoundException wrapped = new("The timetable module could not resolve its view locator.", containerDisposed);

Console.WriteLine(defaultMessage.Message);
Console.WriteLine(customMessage.Message);
Console.WriteLine(wrapped.Message);
Console.WriteLine(wrapped.InnerException?.Message);
```

```text
No IViewLocator is registered. Call RxBindingBuilder.CreateReactiveUIBindingBuilder().WithCoreServices().BuildApp() to register default services.
No view locator was registered for the timetable module.
The timetable module could not resolve its view locator.
The module container was disposed.
```

`ViewLocatorNotFoundException` has three constructors: one with no arguments and the message above, one that
takes your own message, and one that also takes an inner exception. Your own code can throw it too, for
example when a module fails to reach the container that holds its locator.

## Wrap the locator for a placeholder screen

`IViewLocator` is a small interface, so you can wrap the app's locator in one of your own. This one asks the
inner locator first and shows a placeholder screen for a view model that has no view yet, rather than a blank
area.

```csharp
PlaceholderViewLocator locator = new(ViewLocator.GetCurrent());
using TodoListViewModel todos = new(InMemoryTodoStore.CreateSeeded());

Console.WriteLine(locator.ResolveView(todos)?.GetType().Name);
Console.WriteLine(locator.ResolveView(new SettingsViewModel())?.GetType().Name);
```

```text
TodoListView
PlaceholderView
```

`SettingsViewModel` has no registered view in the example, so the inner locator returns `null` and the
wrapper's placeholder view takes over.

An `IViewLocator` implements three methods: the generic `ResolveView<TViewModel>`, `ResolveView(object?, contract)`
and `ResolveViewUnsafe(object?, contract)`. Keep `ResolveView(object?, contract)` free of any step that closes a
generic type over the view model's run-time type, because the default view hosts call it. Put that step in
`ResolveViewUnsafe`, which the Unsafe hosts call, and mark it `[RequiresDynamicCode]`. The wrapper forwards each
method to the same method on the inner locator:

```csharp
public IViewFor? ResolveView(object? viewModel, string? contract) =>
    inner.ResolveView(viewModel, contract) ?? new PlaceholderView { ViewModel = viewModel };

[RequiresDynamicCode("Asks the inner locator's reflective lookup, which closes IViewFor<> over the view model's runtime type.")]
public IViewFor? ResolveViewUnsafe(object? viewModel, string? contract) =>
    inner.ResolveViewUnsafe(viewModel, contract) ?? new PlaceholderView { ViewModel = viewModel };
``` [Write your own locator](../../../binding/views.md#write-your-own-locator)
covers implementing `IViewLocator` directly, for an app that replaces the default lookup rather than wrapping
it.

## Next steps

- [Views](../../../binding/views.md) covers `IViewFor`, the source generator's lookup, contracts and mappings in
  depth.
- [Extending IViewFor](extending-iviewfor.md) shows implementing `IViewFor<T>` on a view of your own.
- [Routing](../routing.md) uses a view locator to show the view for each view model on the navigation stack.
- [RxAppBuilder](../rxappbuilder.md) covers the builder that registers the app's locator and view modules.
- [Reflection](../reflection.md) covers `[RequiresDynamicCode]` and the object-typed overloads it appears on.

## At a glance

| Member | What it does |
| --- | --- |
| `ViewLocator.GetCurrent()` | Returns the `IViewLocator` the app's builder registered; throws `ViewLocatorNotFoundException` when none is. |
| `IViewLocator.ResolveView<TViewModel>(viewModel, contract)` | Resolves a view for a view model whose type is known at compile time. Safe for Native AOT. |
| `IViewLocator.ResolveView(object?, contract)` | Resolves a view for a view model held as `object`, from the generated lookup and the `Map` entries. Safe for Native AOT; the default view hosts call it. |
| `IViewLocator.ResolveViewUnsafe(object?, contract)` | The same, then the service locator for `IViewFor<T>` of the run-time type; carries `[RequiresDynamicCode]`. The Unsafe view hosts call it. |
| `ResolveView(viewModel)` | Extension that resolves with the default contract, on either overload. |
| `DefaultViewLocator` | The locator an app's builder registers. Checks the generated lookup, then this instance's mappings; the generic overload and `ResolveViewUnsafe` then ask the service locator. |
| `DefaultViewLocator.Map` | Registers a view, by type or by factory, for a view model type and an optional contract on this locator. |
| `DefaultViewLocator.Unmap` | Removes a mapping and reports whether one existed. |
| `DefaultViewLocator.CreateMappingBuilder()` | Returns a `ViewMappingBuilder` for chaining several mappings on this locator. |
| `ViewMappingBuilder.Map` | The same mappings as `DefaultViewLocator.Map`, returning the builder so calls chain. |
| `ViewMappingBuilder.MapFromServiceLocator<TViewModel, TView>` | Maps a view the service locator already builds, instead of constructing one directly. |
| `IViewModule` | Interface with one method, `RegisterViews(DefaultViewLocator)`, for a feature to register its own views. |
| `WithViewModule<TModule>()` | Builder method that constructs a module and calls `RegisterViews` on the app's locator. |
| `ViewLocatorNotFoundException` | Thrown when no locator is registered, or by your own code when a screen cannot be resolved. |
