---
Order: 8
---
# Views

[Run the complete page example](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/views/views.csproj).

An application shows a screen for each view model. A navigation service holds a to-do list view model
and has to ask: which screen shows this? A *view* is the class for that screen. A *view locator* is the object
that answers the question. You give it a view model and it gives you the view, with the view model already set.

ReactiveUI.Binding writes most of that lookup for you. The [source generator](index.md) finds every class that
implements `IViewFor<T>` and writes a lookup for them while your project builds. You can also register views
by hand, and you can choose between several screens for one view model.

This page starts with one view and one lookup. Later sections cover the interfaces a view implements,
the locator and its errors, contracts, the generated lookup, hand-written mappings and the order the locator checks.

## Find the view for a view model

**1. Implement `IViewFor<T>`.** A view declares the type of view model it shows. `IViewFor<T>` has one
member, a typed `ViewModel` property. Its base interface, `IViewFor`, has the same property typed as `object`.
Implement the base one explicitly and forward it to the typed one.

```csharp
public sealed class AccountListView : ObservableObject, IViewFor<IAccountList>
{
    public IAccountList? ViewModel
    {
        get;
        set => SetProperty(ref field, value);
    }

    public Label CountLabel { get; } = new();

    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (IAccountList?)value;
    }
}
```

`ObservableObject` is a helper class from the examples that raises `PropertyChanged`. A view should raise a
change when its `ViewModel` changes, because bindings follow that property.
This view names an interface, `IAccountList`, as its view model type. That choice matters in the next step.

**2. Resolve a view.** Create a `DefaultViewLocator` and pass it a view model. `ResolveView` returns the view with
its `ViewModel` set to the object you passed.

```csharp
AccountListViewModel current = new([CreateAccount(EverydayId, EverydayName)]);
ClosedAccountListViewModel closed = new([]);
DefaultViewLocator locator = new();

IViewFor? currentView = locator.ResolveView(current);
IViewFor? closedView = locator.ResolveView(closed);

Console.WriteLine(currentView?.GetType().Name);
Console.WriteLine(closedView?.GetType().Name);
Console.WriteLine(ReferenceEquals(closedView?.ViewModel, closed));
```

```text
AccountListView
AccountListView
True
```

**3. Read the result.** Both view models implement `IAccountList`, so one view serves both. You wrote no
registration code. The generator saw `AccountListView` and added it to the lookup. `ResolveView` returns
`null` when no view matches, so check for it.

## Give a view its view model

A host, such as a navigation service, hands a view model to a view through the interface. It does not know the
concrete view type. `Present` takes the typed interface and reads the property back.

```csharp
public static TViewModel? Present<TViewModel>(IViewFor<TViewModel> view, TViewModel viewModel)
    where TViewModel : class
{
    view.ViewModel = viewModel;
    return view.ViewModel;
}
```

The next snippet shows a new to-do view before and after the host presents a view model to it. A view starts
empty, so it only shows data once the host assigns one.

```csharp
TodoView view = new();
TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());

Console.WriteLine(view.ViewModel is null);

TodoListViewModel? shown = Present(view, viewModel);

Console.WriteLine(ReferenceEquals(shown, viewModel));
Console.WriteLine(ReferenceEquals(view.ViewModel, viewModel));
```

```text
True
True
True
```

### Assign through the non-generic interface

A navigation stack often holds view models as `object`. `IViewFor` takes an `object`, so the host needs no
generic type argument.

```csharp
IssueBoardView view = new();
IssueBoardViewModel viewModel = new(InMemoryGitHubServer.CreateSeeded());
IViewFor screen = (IViewFor)view;

screen.ViewModel = viewModel;

Console.WriteLine(ReferenceEquals(screen.ViewModel, viewModel));
Console.WriteLine(ReferenceEquals(view.ViewModel, viewModel));
```

```text
True
True
```

The forwarding property casts the object to the view model type. A view model of the wrong type throws
`InvalidCastException`, and the view keeps its old value.

```csharp
IViewFor screen = (IViewFor)new IssueBoardView();
TodoListViewModel wrongViewModel = new(InMemoryTodoStore.CreateSeeded());
bool refused = false;

try
{
    screen.ViewModel = wrongViewModel;
}
catch (InvalidCastException)
{
    refused = true;
}

Console.WriteLine(refused);
Console.WriteLine(screen.ViewModel is null);
```

```text
True
True
```

### Observe the view model of a view

The `ViewModel` property is an ordinary property, so you observe it like any other. `WhenChanged` delivers the
current value on subscribe and then each change. See [observing](observing.md) for how that works.

```csharp
TodoView view = new();
TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());
List<TodoListViewModel?> shown = [];

using (view.WhenChanged(x => x.ViewModel!).Subscribe(shown.Add))
{
    view.ViewModel = viewModel;
}

Console.WriteLine(shown.Count);
Console.WriteLine(shown[0] is null);
Console.WriteLine(ReferenceEquals(shown[1], viewModel));
```

```text
2
True
True
```

The first value is `null` because the view had no view model when the subscription began.

### Recognize activatable views

`IActivatableView` is a marker interface. It has no members, and every `IViewFor` implements it. A host uses it
to tell views from other objects.

```csharp
public static int CountActivatable(IEnumerable<object> candidates) => candidates.OfType<IActivatableView>().Count();
```

The next snippet puts two views and one view model in a list and counts the activatable items. Only the views
count, so the check separates screens from data.

```csharp
List<object> candidates =
[
    new TodoView(),
    new TodoListViewModel(InMemoryTodoStore.CreateSeeded()),
    new AccountsView(),
];

Console.WriteLine(CountActivatable(candidates));
```

```text
2
```

## Bind a view to its view model

The view-first binding methods, `OneWayBind` and `Bind`, take the view model and two property paths. They need a
view that implements `IViewFor`. On a view that implements `IViewFor<T>`, the bindings follow `view.ViewModel`, not the
instance you pass, which only decides the types. A view that implements only the non-generic `IViewFor` binds the view
model you pass. Read [bindings](bindings.md) for the full set of binding methods.

```csharp
TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());
TodoView view = new();
await viewModel.LoadAsync();

// The bindings follow view.ViewModel, so the screen shows the view model they read.
_ = Present(view, viewModel);

using (view.OneWayBind(viewModel, x => x.RemainingCount, v => v.RemainingLabel.Text, static count => count.ToString(CultureInfo.InvariantCulture)))
using (view.Bind(viewModel, x => x.NewTitle, v => v.NewTitleTextBox.Text))
{
    Console.WriteLine(view.RemainingLabel.Text);

    view.NewTitleTextBox.Text = NewItemTitle;

    Console.WriteLine(viewModel.NewTitle);
}
```

```text
3
Book vet appointment
```

`OneWayBind` copies the remaining count to the label. `Bind` carries the text the user types back into the view
model. `BindAccountsView` in the [example project](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/views/ViewForExamples.cs)
does the same for a banking screen. Every binding writes to the view on the thread that owns it. See
[threading](threading.md).

A binding made before the view has a view model waits for one. While `view.ViewModel` is null, the binding writes
nothing, so the label keeps its own text. Assigning a view model starts the binding, and replacing it moves the binding
to the new one.

```csharp
TodoListViewModel household = new(InMemoryTodoStore.CreateSeeded());
TodoListViewModel shared = new(InMemoryTodoStore.CreateSeeded());
await household.LoadAsync();
await shared.LoadAsync();
shared.SelectedItem = shared.Items[0];
await shared.CompleteAsync();
TodoView view = new();

// The binding follows view.ViewModel, which is still empty, so the label is left alone.
using (view.OneWayBind(household, x => x.RemainingCount, v => v.RemainingLabel.Text, static count => count.ToString(CultureInfo.InvariantCulture)))
{
    Console.WriteLine(view.RemainingLabel.Text ?? NoViewModelText);

    view.ViewModel = household;
    Console.WriteLine(view.RemainingLabel.Text);

    // Replacing the view model moves the binding to the new one.
    view.ViewModel = shared;
    Console.WriteLine(view.RemainingLabel.Text);
}
```

```text
no view model yet
3
2
```

## Use the locator your application registers

`new DefaultViewLocator()` is fine in a test. An application registers one locator for everyone to share.
`ViewLocator.GetCurrent()` returns that locator. `WithCoreServices` registers a `DefaultViewLocator` among the
core services. The example calls it and then `BuildApp`. See [setup](setup.md) for the builder.

```csharp
IReactiveUIBindingBuilder builder = (IReactiveUIBindingBuilder)RxBindingBuilder.CreateReactiveUIBindingBuilder();
_ = builder.WithCoreServices().BuildApp();

IViewLocator locator = ViewLocator.GetCurrent();

Console.WriteLine(locator is DefaultViewLocator);
```

```text
True
```

The locator is an `IViewLocator`. Asking for it before any registration throws `ViewLocatorNotFoundException`.
The message names the calls that fix it.

```csharp
ViewLocatorNotFoundException? failure = null;

try
{
    _ = ViewLocator.GetCurrent();
}
catch (ViewLocatorNotFoundException ex)
{
    failure = ex;
}

Console.WriteLine(failure is not null);
Console.WriteLine(failure!.Message.Contains("WithCoreServices", StringComparison.Ordinal));
```

```text
True
True
```

The next snippet asks the registered locator for the screen of a to-do list. It returns `TodoView` with the view
model set, which is what a navigation service needs before it shows the screen.

```csharp
TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());
IViewLocator locator = ViewLocator.GetCurrent();

IViewFor? view = locator.ResolveView(viewModel);

Console.WriteLine(view?.GetType().Name);
Console.WriteLine(ReferenceEquals(view?.ViewModel, viewModel));
```

```text
TodoView
True
```

## Resolve views

`IViewLocator` declares two overloads of `ResolveView`. Each takes a contract, a string that picks between
screens for one view model. Pass `null` for the default screen. Contracts are covered [below](#choose-a-view-by-contract).

| Member | Use it when |
|--------|-------------|
| `ResolveView<TViewModel>(viewModel, contract)` | You know the view model type when you write the call. |
| `ResolveView(object, contract)` | You hold the view model as an `object`. |
| `ResolveView(viewModel)` | You want the default contract. `ViewLocatorMixins` adds this to both forms. |

The generic overload reads the view model type at compile time. When no generated or mapped view answers, it
also asks the service locator, closing `IViewFor<TViewModel>` over that known type, so it is safe for Native AOT.
The `object` overload reads the runtime type instead, and it stops after the generated lookup and the `Map`
registrations: closing `IViewFor<>` over a type read at run time needs code the compiler never saw, so the
`object` overload never asks the service locator. When neither tier answers, it logs a warning and returns
`null`. [Reach a view registered only in the service locator](#reach-a-view-registered-only-in-the-service-locator)
covers the fix. `ResolveViewUnsafe` adds the service locator as a third tier for a view model held as an
`object`; it closes `IViewFor<>` with reflection, so only it carries `[RequiresDynamicCode]`.

A *service locator* is a shared registry where an application registers services by type. This library uses
Splat's `AppLocator` for that.

The next snippet holds an issue board view model in an `object` variable, as a navigation stack does. The
compiler picks the `object` overload, so the call needs no cast and no type argument.

```csharp
object viewModel = new IssueBoardViewModel(InMemoryGitHubServer.CreateSeeded());
IViewLocator locator = ViewLocator.GetCurrent();

IViewFor? view = locator.ResolveView(viewModel);

Console.WriteLine(view?.GetType().Name);
Console.WriteLine(ReferenceEquals(view?.ViewModel, viewModel));
```

```text
IssueBoardView
True
```

A host that passes a contract calls the interface directly. The helper below forwards a contract for an
`object` view model.

```csharp
public static IViewFor? ResolveForContract(IViewLocator locator, object viewModel, string? contract) =>
    locator.ResolveView(viewModel, contract);
```

The next snippet uses that helper to ask for the accounts screen with and without a contract. The default screen
and the compact screen come back, so a host can pick a layout by name.

```csharp
object viewModel = new AccountsViewModel(new InMemoryBankingBackend());
DefaultViewLocator locator = new();

IViewFor? standard = ResolveForContract(locator, viewModel, null);
IViewFor? compact = ResolveForContract(locator, viewModel, AccountViewContracts.Compact);

Console.WriteLine(standard?.GetType().Name);
Console.WriteLine(compact?.GetType().Name);
```

```text
AccountsView
CompactAccountsView
```

The next snippet resolves a view for a `null` view model. It returns `null` and does not throw, so a host can
pass a missing view model safely. Both overloads behave this way.

```csharp
IViewLocator locator = ViewLocator.GetCurrent();

IViewFor? view = locator.ResolveView((object?)null);

Console.WriteLine(view is null);
```

```text
True
```

### Reach a view registered only in the service locator

A screen registered only with Splat's `AppLocator`, and never mapped or generated, is a case `ResolveView` with an
`object` view model cannot reach. The next snippet registers `TodoItemDetailView` that way, then asks for it
through both overloads. `ResolveView` finds nothing and logs a warning that names `ResolveViewUnsafe`.
`ResolveViewUnsafe` asks the service locator too, and finds it.

```csharp
object viewModel = new TodoItem { Title = NoteTitle };
DefaultViewLocator locator = new DefaultViewLocator();
AppLocator.CurrentMutable.Register<IViewFor<TodoItem>>(static () => new TodoItemDetailView());

try
{
    IViewFor? aheadOfTimeSafe = locator.ResolveView(viewModel);
    IViewFor? reflective = locator.ResolveViewUnsafe(viewModel);

    Console.WriteLine(aheadOfTimeSafe is null);
    Console.WriteLine(reflective?.GetType().Name);
}
finally
{
    AppLocator.CurrentMutable.UnregisterAll<IViewFor<TodoItem>>();
}
```

```text
True
TodoItemDetailView
```

Call `MapFromServiceLocator<TViewModel, TView>` on the mapping builder to fix this without reflection. It adds a
`Map` that reads the view from the service locator the first time `ResolveView` asks for it, so the `object`
overload finds it and stays Native AOT safe.

```csharp
object viewModel = new TodoItem { Title = NoteTitle };
DefaultViewLocator locator = new DefaultViewLocator();
AppLocator.CurrentMutable.Register<IViewFor<TodoItem>>(static () => new TodoItemDetailView());

try
{
    locator.CreateMappingBuilder().MapFromServiceLocator<TodoItem, IViewFor<TodoItem>>();

    IViewFor? view = locator.ResolveView(viewModel);

    Console.WriteLine(view?.GetType().Name);
}
finally
{
    AppLocator.CurrentMutable.UnregisterAll<IViewFor<TodoItem>>();
}
```

```text
TodoItemDetailView
```

The analyzer catches this at build time instead of at the warning above. RXUIBIND020 (Info) reports a Splat
`Register`, `RegisterLazySingleton` or `RegisterConstant` of `IViewFor<T>` when the project has no generated view
and no `Map` for `T`, so you can add the mapping before the warning ever logs. Both the warning and RXUIBIND020
arrived in ReactiveUI.Binding 8.1.0.

### Reach two contracted views registered in the service locator

`MapFromServiceLocator<TViewModel, TView>` also takes a contract and a service contract, added in
ReactiveUI.Binding 8.2.0. The `contract` parameter is the contract callers pass to `ResolveView`; the mapping only
answers that contract. The `serviceContract` parameter is the contract the service locator registration used, so
the mapping can tell two registrations of the same `IViewFor<TViewModel>` type apart. The next snippet registers a
detail screen under the default service contract and a preview screen under a `"preview"` service contract, both as
`IViewFor<TodoItem>`, then maps each one to a matching `ResolveView` contract.

```csharp
TodoItem item = CreateItem();
DefaultViewLocator locator = new();
AppLocator.CurrentMutable.Register<IViewFor<TodoItem>>(static () => new TodoItemDetailView());
AppLocator.CurrentMutable.Register<IViewFor<TodoItem>>(static () => new TodoItemPreviewView(), PreviewContract);

try
{
    _ = locator.CreateMappingBuilder()
        .MapFromServiceLocator<TodoItem, IViewFor<TodoItem>>()
        .MapFromServiceLocator<TodoItem, IViewFor<TodoItem>>(PreviewContract, serviceContract: PreviewContract);

    IViewFor? detail = locator.ResolveView(item);
    IViewFor? preview = locator.ResolveView(item, PreviewContract);

    Console.WriteLine(detail?.GetType().Name);
    Console.WriteLine(preview?.GetType().Name);
}
finally
{
    AppLocator.CurrentMutable.UnregisterAll<IViewFor<TodoItem>>();
    AppLocator.CurrentMutable.UnregisterAll<IViewFor<TodoItem>>(PreviewContract);
}
```

```text
TodoItemDetailView
TodoItemPreviewView
```

A mapping made this way still resolves lazily, the first time `ResolveView` asks for its contract. If the service
locator has no registration under `serviceContract` at that point, the mapping throws `InvalidOperationException`
naming the missing contract, instead of returning `null`.

## Handle a missing view

A view model with no view resolves to `null`. The locator does not throw, because a missing view is often
normal. A host that needs a screen decides what to do. This helper turns `null` into a
`ViewLocatorNotFoundException` with a message for the user.

```csharp
public static IViewFor RequireView(IViewLocator locator, object viewModel)
{
    IViewFor? view = locator.ResolveView(viewModel);
    return view ?? throw new ViewLocatorNotFoundException($"No screen is registered for {viewModel.GetType().Name}.");
}
```

The next snippet asks for the screen of a to-do item that has none. The plain call returns `null`, and
`RequireView` throws with its message. That shows both ways a host can react.

```csharp
TodoItem note = new() { Title = NoteTitle };
IViewLocator locator = ViewLocator.GetCurrent();
ViewLocatorNotFoundException? failure = null;

IViewFor? view = locator.ResolveView(note);

try
{
    _ = RequireView(locator, note);
}
catch (ViewLocatorNotFoundException ex)
{
    failure = ex;
}

Console.WriteLine(view is null);
Console.WriteLine(failure?.Message);
```

```text
True
No screen is registered for TodoItem.
```

A view that exists but fails to build is a different case. The locator does not catch the exception from a view
constructor or a factory. It reaches your code. This helper reports the failure as a missing screen and keeps the
cause in `InnerException`.

```csharp
public static IViewFor? ResolveOrExplain(IViewLocator locator, TodoItem viewModel)
{
    try
    {
        return locator.ResolveView(viewModel);
    }
    catch (InvalidOperationException ex)
    {
        throw new ViewLocatorNotFoundException($"The screen for {nameof(TodoItem)} could not be built.", ex);
    }
}
```

The next snippet maps a factory that throws, then resolves through the helper. The caller sees the not-found
exception with the original error inside it, so the cause is not lost.

```csharp
TodoItem viewModel = new() { Title = NoteTitle };
DefaultViewLocator locator = new();
locator.Map<TodoItem>(static () => throw new InvalidOperationException(LayoutFailure));
ViewLocatorNotFoundException? failure = null;

try
{
    _ = ResolveOrExplain(locator, viewModel);
}
catch (ViewLocatorNotFoundException ex)
{
    failure = ex;
}

Console.WriteLine(failure?.Message);
Console.WriteLine(failure?.InnerException?.Message);
```

```text
The screen for TodoItem could not be built.
The layout file is missing.
```

`ViewLocatorNotFoundException` has three constructors: one with no arguments, one with a message and one with a
message and an inner exception. The one with no arguments carries a message that names the builder calls
that register the locator.

```csharp
ViewLocatorNotFoundException failure = new();

Console.WriteLine(failure.Message.Contains("BuildApp", StringComparison.Ordinal));
```

```text
True
```

## Write your own locator

`IViewLocator` is a small interface. An application with one screen can implement it directly. Both overloads
return the view for a to-do list and `null` for anything else.

```csharp
public sealed class TodoViewLocator : IViewLocator
{
    public IViewFor? ResolveView(object? viewModel, string? contract) =>
        viewModel is TodoListViewModel todoList ? new TodoView { ViewModel = todoList } : null;

    public IViewFor? ResolveView<TViewModel>(TViewModel viewModel, string? contract)
        where TViewModel : class =>
        viewModel is TodoListViewModel todoList ? new TodoView { ViewModel = todoList } : null;
}
```

The next snippet resolves the to-do list and an unrelated item through it. The list gets its screen with the view
model set and the other item gets `null`, so a custom locator behaves like the default one.

```csharp
TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());
TodoViewLocator locator = new();

IViewFor? view = locator.ResolveView(viewModel, null);
IViewFor? missing = locator.ResolveView(new TodoItem(), null);

Console.WriteLine(view?.GetType().Name);
Console.WriteLine(ReferenceEquals(view?.ViewModel, viewModel));
Console.WriteLine(missing is null);
```

```text
TodoView
True
True
```

## Choose a view by contract

One view model can have more than one screen. A *contract* is a string that names one of them. Put
`[ViewContract]` on a view to register it under a contract. A view with no attribute is the default screen.
The next snippet reads the contract back from the attribute with reflection. It shows the string the view is
registered under, which is the string a caller passes to `ResolveView`.

```csharp
ViewContractAttribute? attribute = typeof(AccountStatementView).GetCustomAttribute<ViewContractAttribute>();

Console.WriteLine(attribute?.Contract);
```

```text
statement
```

`AccountStatementView` and `AccountSummaryView` both show an `Account`. The statement view carries
`[ViewContract(AccountViewContracts.Statement)]`. The summary view has no contract. Pass the contract to
`ResolveView` to choose.

```csharp
Account account = CreateAccount();
DefaultViewLocator locator = new();

IViewFor? statement = locator.ResolveView(account, AccountViewContracts.Statement);
IViewFor? summary = locator.ResolveView(account, null);

Console.WriteLine(statement?.GetType().Name);
Console.WriteLine(summary?.GetType().Name);
```

```text
AccountStatementView
AccountSummaryView
```

The next snippet asks for a contract that no view claims. `Account` has more than one view, so the locator
cannot pick one for an unknown contract, and it returns `null`. Check for `null` before you show the view.

```csharp
Account account = CreateAccount();
DefaultViewLocator locator = new();

IViewFor? view = locator.ResolveView(account, UnclaimedContract);

Console.WriteLine(view is null);
```

```text
True
```

Two more rules follow from how the generator writes the lookup. A view model that has only one registered view,
with no contract, answers to every contract. A view model whose only view has a contract answers to that
contract and no other. `ResolveBankingViewByContract` in the
[example project](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/views/ViewContractExamples.cs)
picks `AccountsView` or `CompactAccountsView` for `AccountsViewModel` this way.

## The generated view dispatch

The generator writes one lookup for each assembly that contains views. Each entry pairs a view model type with a
view and a contract. The lookup is a type switch, so it uses no reflection and works with trimming and Native AOT.
This lookup is the *view dispatch*: it chooses which view to build for a view model.

A few facts describe how the generated lookup behaves.

- **It prefers the service locator.** The generated resolver for a view asks the service locator first.
  When the service locator has no view, the resolver builds the view with its parameterless constructor.
- **It registers itself.** From C# 9 the registration runs in a module initializer, before any code in the
  assembly. In older projects it runs when a binding first uses the generated class.
- **It keeps one view for each pair of view model and contract.** The first registration in source order wins.
- **It skips open generic views.** That includes a view nested in an open generic type.
- **Assemblies stack.** `DefaultViewLocator` keeps the lookup of each assembly, in registration order, and asks
  the most recent first. When two assemblies both have a view for the same view model, the assembly that
  registered last wins.

### Reuse one view

`[SingleInstanceView]` tells the lookup to build a view once and hand out the same instance every time. Each
resolve sets the latest view model on that instance. Use it only for a view that is not shown in more than one
place at once.

```csharp
Account everyday = CreateAccount(EverydayId, EverydayName);
Account savings = CreateAccount(SavingsId, SavingsName);
DefaultViewLocator locator = new();

IViewFor? first = locator.ResolveView(everyday);
IViewFor? second = locator.ResolveView(savings);

Console.WriteLine(first?.GetType().Name);
Console.WriteLine(ReferenceEquals(first, second));
Console.WriteLine(ReferenceEquals(first?.ViewModel, savings));
```

```text
AccountSummaryView
True
True
```

### Leave a view out

`[ExcludeFromViewRegistration]` keeps a view out of the lookup. Nothing answers until the application maps the
view by hand. The next sections show `Map`.

```csharp
TodoItem item = new TodoItem { Title = "Renew car registration" };
DefaultViewLocator locator = new();

IViewFor? before = locator.ResolveView(item);
locator.Map<TodoItem, TodoItemPreviewView>();
IViewFor? after = locator.ResolveView(item);

Console.WriteLine(before is null);
Console.WriteLine(after?.GetType().Name);
```

```text
True
TodoItemPreviewView
```

### Register a view that needs arguments

`ReceiptView` has no parameterless constructor, so the lookup cannot build it. The lookup finds it only when
the service locator holds it. Register the view with `AppLocator`, and unregister it when you finish.

```csharp
TransferReceipt receipt = new("RCPT-000001", TransferAmount, AccountBalance - TransferAmount, DateTimeOffset.UnixEpoch);
DefaultViewLocator locator = new();

IViewFor? before = locator.ResolveView(receipt);
AppLocator.CurrentMutable.Register<IViewFor<TransferReceipt>>(static () => new ReceiptView(ReceiptHeading));

try
{
    ReceiptView? after = (ReceiptView?)locator.ResolveView(receipt);

    Console.WriteLine(before is null);
    Console.WriteLine(after?.HeadingLabel.Text);
    Console.WriteLine(ReferenceEquals(after?.ViewModel, receipt));
}
finally
{
    AppLocator.CurrentMutable.UnregisterAll<IViewFor<TransferReceipt>>();
}
```

```text
True
Transfer receipt
True
```

### Register a view for an interface

The generated lookup tests the view model with `is`, so a view registered for an interface serves every
class that implements it. You saw this in the first walkthrough. A view model can also have a view for its own
class next to the interface view. The lookup tests registrations in source-file order and returns the first
match. Nothing reports the overlap.

```csharp
AccountListViewModel current = new([CreateAccount(EverydayId, EverydayName)]);
DefaultViewLocator locator = new();

IViewFor? view = locator.ResolveView(current);

Console.WriteLine(view?.GetType().Name);
Console.WriteLine(view is DetailedAccountListView);
```

```text
AccountListView
False
```

Here the interface view comes first, so `DetailedAccountListView` is never returned. Give one of the two a contract,
or map it by hand, when you want both.

### Register a lookup of your own

`DefaultViewLocator.SetGeneratedViewDispatch` is the method the generated code calls. You can call it yourself
with a function that takes a view model and a contract. Return `null` for anything the function does not know.
Editors hide the method from completion lists, because generated code is its intended caller.

```csharp
TodoItem item = new TodoItem { Title = "Renew car registration" };
DefaultViewLocator locator = new();

IViewFor? before = locator.ResolveView(item, PreviewCardContract);

DefaultViewLocator.SetGeneratedViewDispatch(static (viewModel, contract) =>
    viewModel is TodoItem && contract == PreviewCardContract ? new TodoItemPreviewView() : null);

IViewFor? after = locator.ResolveView(item, PreviewCardContract);
IViewFor? otherContract = locator.ResolveView(item, null);

Console.WriteLine(before is null);
Console.WriteLine(after?.GetType().Name);
Console.WriteLine(ReferenceEquals(after?.ViewModel, item));
Console.WriteLine(otherContract is null);
```

```text
True
TodoItemPreviewView
True
True
```

The lookup is shared by every `DefaultViewLocator` in the process. Adding it once is enough, and adding the
same function again has no effect.

## Map views by hand

`Map` registers a view on one locator. Use it for a view the generator skips, or for a view you choose at
run time. A mapping belongs to the locator instance you call it on. Calling `Map` again for the same view model
and contract replaces the old mapping.

| Member | What it registers |
|--------|-------------------|
| `Map<TViewModel, TView>()` | A view built with its parameterless constructor. |
| `Map<TViewModel, TView>(contract)` | The same view under a contract. |
| `Map<TViewModel>(factory)` | A view built by a factory. |
| `Map<TViewModel>(factory, contract)` | A factory under a contract. |
| `Unmap<TViewModel>()` and `Unmap<TViewModel>(contract)` | Removes a mapping. |

The next snippet maps the preview view for a to-do item and resolves it. The locator builds the view with its
parameterless constructor and sets the view model, so a view the generator skips still works.

```csharp
TodoItem item = CreateItem();
DefaultViewLocator locator = new();

locator.Map<TodoItem, TodoItemPreviewView>();

IViewFor? view = locator.ResolveView(item);

Console.WriteLine(view?.GetType().Name);
Console.WriteLine(ReferenceEquals(view?.ViewModel, item));
```

```text
TodoItemPreviewView
True
```

Add a contract, and the mapping answers only to that contract.

```csharp
TodoItem item = CreateItem();
DefaultViewLocator locator = new();

locator.Map<TodoItem, TodoItemPreviewView>(PreviewContract);

IViewFor? preview = locator.ResolveView(item, PreviewContract);
IViewFor? plain = locator.ResolveView(item, null);

Console.WriteLine(preview?.GetType().Name);
Console.WriteLine(plain is null);
```

```text
TodoItemPreviewView
True
```

Use a factory for a screen that needs setup before it is shown. The locator calls the factory on every resolve,
so each call gets a new view.

```csharp
TodoItem item = CreateItem();
DefaultViewLocator locator = new();

locator.Map<TodoItem>(static () => new TodoItemPreviewView { IsCompact = true });

TodoItemPreviewView? view = (TodoItemPreviewView?)locator.ResolveView(item);

Console.WriteLine(view?.IsCompact);
Console.WriteLine(ReferenceEquals(view?.ViewModel, item));
```

```text
True
True
```

`Unmap` removes a mapping and returns `true` when one existed. A `null` contract removes the default mapping.

```csharp
TodoItem item = CreateItem();
DefaultViewLocator locator = new();
locator.Map<TodoItem, TodoItemPreviewView>();
locator.Map<TodoItem, TodoItemPreviewView>(PreviewContract);

bool removedDefault = locator.Unmap<TodoItem>();
bool removedAgain = locator.Unmap<TodoItem>();
bool removedContract = locator.Unmap<TodoItem>(PreviewContract);

Console.WriteLine(removedDefault);
Console.WriteLine(removedAgain);
Console.WriteLine(removedContract);
Console.WriteLine(locator.ResolveView(item) is null);
```

```text
True
False
True
True
```

A mapping matches the exact type you register. The generic `ResolveView` reads that type from the
compile-time type of the variable you pass. The `object` overload reads it from the runtime type. A mapping for
an interface matches only when the call is typed as that interface.

### Chain mappings with the builder

`CreateMappingBuilder` returns a `ViewMappingBuilder`. It has the same four `Map` overloads, and each returns the
builder so that you can chain them.

```csharp
TodoItem item = CreateItem();
DefaultViewLocator locator = new();

_ = locator.CreateMappingBuilder()
    .Map<TodoItem, TodoItemPreviewView>()
    .Map<TodoItem, TodoItemDetailView>(DetailContract)
    .Map<TodoItem>(static () => new TodoItemPreviewView { IsCompact = true }, CompactContract);

IViewFor? standard = locator.ResolveView(item, null);
IViewFor? detail = locator.ResolveView(item, DetailContract);
TodoItemPreviewView? compact = (TodoItemPreviewView?)locator.ResolveView(item, CompactContract);

Console.WriteLine(standard?.GetType().Name);
Console.WriteLine(detail?.GetType().Name);
Console.WriteLine(compact?.IsCompact);
```

```text
TodoItemPreviewView
TodoItemDetailView
True
```

## Register mappings at startup

A shared locator needs its mappings before the first `ResolveView`. Do that in the builder. `ConfigureViewLocator`
creates a `DefaultViewLocator`, hands you its `ViewMappingBuilder` and registers the locator. Call it after
`WithCoreServices`, so the locator you configured is the one registered last. The next snippet maps two views in
the builder and reads them back from the shared locator.

```csharp
TodoItem item = CreateItem();
IReactiveUIBindingBuilder builder = (IReactiveUIBindingBuilder)RxBindingBuilder.CreateReactiveUIBindingBuilder();

_ = builder
    .WithCoreServices()
    .ConfigureViewLocator(static mappings => mappings
        .Map<TodoItem, TodoItemPreviewView>()
        .Map<TodoItem, TodoItemDetailView>(DetailContract))
    .BuildApp();

IViewLocator locator = ViewLocator.GetCurrent();

IViewFor? standard = locator.ResolveView(item);
IViewFor? detail = locator.ResolveView(item, DetailContract);

Console.WriteLine(standard?.GetType().Name);
Console.WriteLine(detail?.GetType().Name);
```

```text
TodoItemPreviewView
TodoItemDetailView
```

The same call exists as an extension on Splat's `IAppBuilder`, so a chain that has passed through the Splat builder
can still reach it. The extension lives in `ReactiveUI.Binding.Mixins`. It throws `InvalidOperationException` when
the builder is not a ReactiveUI.Binding builder.

```csharp
TodoItem item = CreateItem();

_ = RxBindingBuilder.CreateReactiveUIBindingBuilder()
    .WithCoreServices()
    .ConfigureViewLocator(static mappings => mappings.Map<TodoItem, TodoItemPreviewView>())
    .BuildApp();

IViewFor? view = ViewLocator.GetCurrent().ResolveView(item);

Console.WriteLine(view?.GetType().Name);
```

```text
TodoItemPreviewView
```

The generated lookup is shared by every locator, so a locator built by `ConfigureViewLocator` still answers for the
views the generator found.

## The order the locator checks

`DefaultViewLocator.ResolveView` checks three sources and returns the first view it finds.

1. The generated lookup, the most recently registered assembly first.
2. The mappings you added with `Map`.
3. The service locator.

Every source sets the view model on the view before the locator returns it. The example registers one view in
each source and removes the mapping to show the next source answer.

```csharp
TodoItem item = CreateItem();
Account account = CreateAccount();
DefaultViewLocator locator = new();
locator.Map<Account, AccountStatementView>();
locator.Map<TodoItem, TodoItemPreviewView>();
AppLocator.CurrentMutable.Register<IViewFor<TodoItem>>(static () => new TodoItemDetailView());

try
{
    IViewFor? generated = locator.ResolveView(account);
    IViewFor? mapped = locator.ResolveView(item);
    _ = locator.Unmap<TodoItem>();
    IViewFor? registered = locator.ResolveView(item);

    Console.WriteLine(generated?.GetType().Name);
    Console.WriteLine(mapped?.GetType().Name);
    Console.WriteLine(registered?.GetType().Name);
}
finally
{
    AppLocator.CurrentMutable.UnregisterAll<IViewFor<TodoItem>>();
}
```

```text
AccountSummaryView
TodoItemPreviewView
TodoItemDetailView
```

The generated lookup answers for the account, even though the example mapped `AccountStatementView` for
`Account`. The generated source comes first, so a mapping cannot override a view the generator registered.
To replace a generated view, add `[ExcludeFromViewRegistration]` to it.

## Next steps

- [Setup](setup.md) shows the builder that registers the locator.
- [Bindings](bindings.md) covers the view-first binding methods used above.
- [Threading](threading.md) explains where a binding writes to a view.
- [API reference](api.md) lists every member.

## API reference

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`IViewFor`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IViewFor.cs) | Marks a class as a view of one view model, held as an `object`. | Interface. Extends `IActivatableView`. | Use it when the host does not know the view model type. |
| [`IViewFor.ViewModel`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IViewFor.cs) | Gets or sets the view model the view shows. | Read and write [`object?`](https://learn.microsoft.com/dotnet/api/system.object). | A view implements it explicitly and forwards it to the typed property. |
| [`IViewFor<T>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IViewFor%7BT%7D.cs) | Marks a class as a view of one view model type. | Interface. `T : class`. Extends `IViewFor`. | The generator reads each class that implements it to build the view dispatch. |
| [`IViewFor<T>.ViewModel`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IViewFor%7BT%7D.cs) | Gets or sets the view model the view shows, typed. | Read and write `T?`. | Hides the `object` property of `IViewFor`. The value is `null` until a host assigns one. |
| [`IActivatableView`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IActivatableView.cs) | Marks an object as a view a host may activate. | Marker interface with no members. | Every `IViewFor` implements it. |
| [`IViewLocator`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IViewLocator.cs) | Finds the view for a view model. | Interface. | Implement it to write your own locator. |
| [`IViewLocator.ResolveView`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IViewLocator.cs) | Returns the view for a view model under a contract. | Generic form `ResolveView<TViewModel>(TViewModel, string?)` with `TViewModel : class`, and `ResolveView(object?, string?)`. Both return `IViewFor?`. | A `null` contract picks the default screen. The result is `null` when no view matches. The `object` form carries `[RequiresDynamicCode]`. |
| [`ViewLocatorMixins.ResolveView`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/ViewLocatorMixins.cs) | Resolves a view with the default contract. | Extension of `IViewLocator`, in a generic form and an `object?` form. Both return `IViewFor?`. | Throws `ArgumentNullException` when the locator is `null`. The `object?` form carries `[RequiresDynamicCode]`. |
| [`ViewLocator.GetCurrent`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/ViewLocator.cs) | Returns the locator registered with the service locator. | Static method on the static class `ViewLocator`. Returns `IViewLocator`. | Throws `ViewLocatorNotFoundException` when no locator is registered. |
| [`ViewLocatorNotFoundException`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/ViewLocatorNotFoundException.cs) | Reports that no view locator is registered, or that a screen is missing. | Class that extends [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception). | `ViewLocator.GetCurrent` throws it. Your own code can throw it. |
| [`ViewLocatorNotFoundException(...)`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/ViewLocatorNotFoundException.cs) | Creates the exception. | No arguments, `string message`, or `string message` with `Exception innerException`. | The form with no arguments has a message that names `WithCoreServices` and `BuildApp`. |
| [`DefaultViewLocator`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/DefaultViewLocator.cs) | The locator that `WithCoreServices` registers. | Sealed class. Implements `IViewLocator`. Has a public parameterless constructor. | Checks the generated lookup, then the mappings of the instance, then the service locator. |
| [`DefaultViewLocator.ResolveView`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/DefaultViewLocator.cs) | Returns the view for a view model and sets its `ViewModel`. | The two forms of `IViewLocator.ResolveView`. | A `null` view model gives `null`. The generic form reads the compile-time type and the `object` form reads the runtime type. An exception from a view constructor or factory reaches the caller. |
| [`DefaultViewLocator.Map`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/DefaultViewLocator.cs) | Registers a view for a view model type on this locator. | `Map<TViewModel, TView>()`, with `TView : IViewFor, new()`, or `Map<TViewModel>(Func<IViewFor> factory)`. Each takes an optional `string? contract`. `TViewModel : class`. Returns `void`. | Replaces a mapping for the same type and contract. A `null` contract is the default mapping. A `null` factory throws `ArgumentNullException`. |
| [`DefaultViewLocator.Unmap`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/DefaultViewLocator.cs) | Removes a mapping. | `Unmap<TViewModel>()` or `Unmap<TViewModel>(string? contract)`. `TViewModel : class`. Returns [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | `true` when a mapping existed. A `null` contract removes the default mapping. |
| [`DefaultViewLocator.CreateMappingBuilder`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/DefaultViewLocator.cs) | Creates a builder that registers mappings in a chain. | Returns `ViewMappingBuilder`. | The builder writes to this locator. |
| [`DefaultViewLocator.SetGeneratedViewDispatch`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/DefaultViewLocator.cs) | Adds a lookup that the locators check before mappings. | Static. Takes `Func<object, string, IViewFor?>`. | Generated code calls it. Editors hide it. The most recent lookup is asked first. A `null` argument throws `ArgumentNullException`. Adding a function twice has no effect. |
| [`ViewMappingBuilder`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/ViewMappingBuilder.cs) | Registers mappings on a locator in a chain. | Sealed class. | Only `DefaultViewLocator.CreateMappingBuilder` and `ConfigureViewLocator` create one. |
| [`ViewMappingBuilder.Map`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/ViewMappingBuilder.cs) | Registers a view for a view model type. | The same forms as `DefaultViewLocator.Map`. Returns `ViewMappingBuilder`. | Returns the builder so calls chain. A `null` factory throws `ArgumentNullException`. |
| [`ViewContractAttribute`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/ViewContractAttribute.cs) | Registers a view in the generated lookup under a contract. | Sealed attribute for classes. Constructor takes `string contract`. | A view with no attribute is the default screen for its view model. |
| [`ViewContractAttribute.Contract`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/ViewContractAttribute.cs) | Gets the contract string. | Read-only [`string`](https://learn.microsoft.com/dotnet/api/system.string). | The string a caller passes to `ResolveView`. |
| [`SingleInstanceViewAttribute`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/SingleInstanceViewAttribute.cs) | Makes the generated lookup build the view once and reuse it. | Sealed attribute for classes. | Needs a parameterless constructor. Do not use it on a view shown in more than one place at once. |
| [`ExcludeFromViewRegistrationAttribute`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/View/ExcludeFromViewRegistrationAttribute.cs) | Leaves a view out of the generated lookup. | Sealed attribute for classes. Not inherited. One per class. | Register the view with `Map` or the service locator instead. |
| [`ConfigureViewLocator`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingBuilder.cs) | Registers a `DefaultViewLocator` that holds the mappings you add. | Takes `Action<ViewMappingBuilder> configure`. Returns `IReactiveUIBindingBuilder`. | A `null` action throws `ArgumentNullException`. An extension on `IAppBuilder` in [`BuilderMixins`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/BuilderMixins.cs) throws `InvalidOperationException` for another builder type. |
