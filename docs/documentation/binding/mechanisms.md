---
Order: 7
---
# Mechanisms

[Run the complete page example](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/mechanisms/mechanisms.csproj).

A property cannot tell you when it changes. The class that owns it has to announce the change, and classes announce in different ways. One class raises `PropertyChanged`. Another raises an event named after the property. A third raises nothing at all. When you observe or bind a property, the library must choose a way to listen for each of them.

A **mechanism** is one way of receiving those announcements. A **provider** is a class that implements `ICreatesObservableForProperty`, and each provider supplies one mechanism. A provider reports an **affinity**, which is a score for a type and a property. Zero means the provider cannot observe that property, and the highest score wins. This page shows how the winner is chosen, how to add a provider for your own type, how command binders follow the same rules, and what the observable types deliver. [Observing](observing.md) explains observables and subscriptions, and the [index](index.md) explains the source generator.

Start with the common case. When a class raises `PropertyChanged`, the generated code listens to that event, so you receive the current value and then each change. This excerpt observes the done flag of a to-do item and then sets it.

```csharp
using (item.WhenChanged(x => x.IsDone).Subscribe(Console.WriteLine))
{
    item.IsDone = true;
}
```

```text
False
True
```

A plain class raises nothing, so no mechanism can tell you about a later edit. This excerpt sets the size of a stored object after subscribing. The edit never reaches the subscriber, which is why a class you observe should announce its changes.

```csharp
using (stored.WhenChanged(x => x.Size).Subscribe(Console.WriteLine))
{
    stored.Size = ResizedBytes;
}
```

```text
2411724
```

## Observe a type that announces changes its own way

A `StorageConnection` raises a plain `StateChanged` event and implements no notification interface. No built-in provider can observe it, so you write one.

**1. Write the provider.** `GetAffinityForObject` bids `BindingAffinity.WinFormsEvent` (8) for the `State` property of a `StorageConnection` and 0 for everything else. `GetNotificationForProperty` returns the stream of changes. `Signal.FromEventPattern` turns the event into a stream, and `Select` turns each event into an `ObservedChange` that names the sender, the expression and the current value. A before-change request or a different sender gets `ImmutableNeverSignal`, a stream that never delivers. See [creation factories](../primitives/creation-factories.md) and the [Primitives types](../primitives/types.md).

```csharp
public sealed class StorageConnectionObservableForProperty : ICreatesObservableForProperty
{
    public int GetAffinityForObject(Type type, string propertyName, bool beforeChanged) =>
        !beforeChanged && type == typeof(StorageConnection) && propertyName == nameof(StorageConnection.State)
            ? BindingAffinity.WinFormsEvent
            : 0;

    public IObservable<IObservedChange<object, object?>> GetNotificationForProperty(
        object sender,
        Expression expression,
        string propertyName,
        bool beforeChanged,
        bool suppressWarnings)
    {
        if (beforeChanged || sender is not StorageConnection connection)
        {
            return ImmutableNeverSignal<IObservedChange<object, object?>>.Instance;
        }

        Console.WriteLine("The connection state is observed by the StateChanged provider");

        return Signal.FromEventPattern(handler => connection.StateChanged += handler, handler => connection.StateChanged -= handler)
            .Select(_ => new ObservedChange<object, object?>(connection, expression, connection.State));
    }
}
```

**2. Register it.** `WithRegistration` adds the provider to the service locator. `ObservationAffinityChecker.Refresh()` clears the cached scores so the next observation reads the new registration. `HasHigherAffinityPlugin` asks whether a registered provider outranks a given score, here the fallback score.

```csharp
bool outranksBefore = ObservationAffinityChecker.HasHigherAffinityPlugin(typeof(StorageConnection), StatePropertyName, BindingAffinity.Fallback, false);

Console.WriteLine($"Provider outranks the fallback before registering: {outranksBefore}");

IReactiveUIBindingBuilder builder = RxBindingBuilder.CreateReactiveUIBindingBuilder();

_ = builder
    .WithCoreServices()
    .WithRegistration(resolver => resolver.RegisterConstant<ICreatesObservableForProperty>(provider))
    .BuildApp();

ObservationAffinityChecker.Refresh();

Console.WriteLine($"Provider is registered: {AppLocator.Current.GetServices<ICreatesObservableForProperty>().Contains(provider)}");
bool outranksAfter = ObservationAffinityChecker.HasHigherAffinityPlugin(typeof(StorageConnection), StatePropertyName, BindingAffinity.Fallback, false);

Console.WriteLine($"Provider outranks the fallback after registering: {outranksAfter}");
```

```text
Provider outranks the fallback before registering: False
Provider is registered: True
Provider outranks the fallback after registering: True
```

**3. Observe the property.** `WhenChanged` works as it does for any other type. The provider serves the call. The first value is the current state, and each later value comes from the event.

```csharp
using (browser.Connection.WhenChanged(x => x.State).Subscribe(static state => Console.WriteLine(state)))
{
    storage.Disconnect();
    storage.Connection.State = ConnectionState.Connected;
}
```

```text
The connection state is observed by the StateChanged provider
Connected
Disconnected
Connected
```

## How affinity picks the winner

Every mechanism uses the same scale. `BindingAffinity` holds the scores.

| Score | Value | Meaning |
| --- | --- | --- |
| `Fallback` | 1 | The POCO provider, for a class that raises nothing. |
| `DefaultInternalTypeConverter` | 2 | The built-in value and string converters. |
| `DefaultEvent` | 3 | A conventional default event of a type. |
| `WpfDependencyObject` | 4 | A WPF dependency property. |
| `EventEnabledControl` | 4 | A command binding through an event on a control. |
| `Explicit` | 5 | An interface such as `INotifyPropertyChanged`, or a named event. |
| `WinUiDependencyObject` | 6 | A WinUI dependency property. |
| `WinFormsEvent` | 8 | A WinForms event-based property observation. |
| `ExactType` | 10 | A strong match, such as an `IReactiveObject`. |
| `Kvo` | 15 | Apple key-value observing on an `NSObject`. |

The source generator picks the best mechanism it can see when your project builds. A provider you register at run time replaces that choice only when its score is strictly higher. A tie keeps the generated mechanism. Among registered providers, the highest score wins and the first registered wins a tie. `ObservationAffinityChecker` reads the registrations on first use and caches the best score for each type, property and timing. Call `Refresh()` after you register a provider. The [threading](threading.md) page covers the platform mechanisms.

This excerpt scores three properties and names the provider that wins each. The `PropertyChanged` provider bids 5 for a `TodoItem` and 0 for a plain `StorageObject`. The fallback provider bids 1 for both. `TodoPropertyObservableForProperty` is a provider in the example project that bids a chosen score for one `TodoItem` property.

```csharp
Console.WriteLine($"PropertyChanged provider, to-do item: {new INPCObservableForProperty().GetAffinityForObject(typeof(TodoItem), nameof(TodoItem.IsDone))}");
Console.WriteLine($"PropertyChanged provider, stored object: {new INPCObservableForProperty().GetAffinityForObject(typeof(StorageObject), nameof(StorageObject.Size))}");
Console.WriteLine($"Fallback provider, stored object: {new POCOObservableForProperty().GetAffinityForObject(typeof(StorageObject), nameof(StorageObject.Size))}");

Console.WriteLine($"IsDone is observed by {SelectHighestAffinity(typeof(TodoItem), nameof(TodoItem.IsDone))?.GetType().Name}");
Console.WriteLine($"Size is observed by {SelectHighestAffinity(typeof(StorageObject), nameof(StorageObject.Size))?.GetType().Name}");
Console.WriteLine($"Title is observed by {SelectHighestAffinity(typeof(TodoItem), TitlePropertyName)?.GetType().Name}");
```

```text
PropertyChanged provider, to-do item: 5
PropertyChanged provider, stored object: 0
Fallback provider, stored object: 1
IsDone is observed by INPCObservableForProperty
Size is observed by POCOObservableForProperty
Title is observed by TodoPropertyObservableForProperty
```

The next excerpt shows a tie and an outranking bid. One provider bids `Explicit` for `Notes`, the same score as the `PropertyChanged` mechanism. Another bids one more for `Title`. The tied provider is never used, and the outranking one is.

```csharp
using (item.WhenChanged(x => x.Notes).Subscribe(Console.WriteLine))
using (item.WhenChanged(x => x.Title).Subscribe(Console.WriteLine))
{
    item.Notes = EditedNotes;
    item.Title = EditedTitle;
}
```

```text
Bring the old plate as well.
Title is observed by the provider that bids 6
Renew car registration by post
Bring the insurance certificate and the old plate.
Renew car registration online
```

This excerpt compares every score with a provider that bids `Explicit` for `Notes`. The provider outranks each score below its bid. It loses to its own score and to every score above it.

```csharp
(string Name, int Score)[] scores =
[
    (nameof(BindingAffinity.Fallback), BindingAffinity.Fallback),
    (nameof(BindingAffinity.DefaultInternalTypeConverter), BindingAffinity.DefaultInternalTypeConverter),
    (nameof(BindingAffinity.DefaultEvent), BindingAffinity.DefaultEvent),
    (nameof(BindingAffinity.WpfDependencyObject), BindingAffinity.WpfDependencyObject),
    (nameof(BindingAffinity.EventEnabledControl), BindingAffinity.EventEnabledControl),
    (nameof(BindingAffinity.Explicit), BindingAffinity.Explicit),
    (nameof(BindingAffinity.WinUiDependencyObject), BindingAffinity.WinUiDependencyObject),
    (nameof(BindingAffinity.WinFormsEvent), BindingAffinity.WinFormsEvent),
    (nameof(BindingAffinity.ExactType), BindingAffinity.ExactType),
    (nameof(BindingAffinity.Kvo), BindingAffinity.Kvo),
];

foreach (var (name, score) in scores)
{
    bool outranked = ObservationAffinityChecker.HasHigherAffinityPlugin(typeof(TodoItem), NotesPropertyName, score, false);

    Console.WriteLine($"{name} ({score}): provider outranks it = {outranked}");
}
```

```text
Fallback (1): provider outranks it = True
DefaultInternalTypeConverter (2): provider outranks it = True
DefaultEvent (3): provider outranks it = True
WpfDependencyObject (4): provider outranks it = True
EventEnabledControl (4): provider outranks it = True
Explicit (5): provider outranks it = False
WinUiDependencyObject (6): provider outranks it = False
WinFormsEvent (8): provider outranks it = False
ExactType (10): provider outranks it = False
Kvo (15): provider outranks it = False
```

## The built-in providers

`INPCObservableForProperty` bids `Explicit` (5) for any type that implements `INotifyPropertyChanged`. It does not look at the property name. For before-change observation it needs `INotifyPropertyChanging` instead. A type without that interface, such as `TodoItem`, gets 0 for before-change requests.

The excerpt asks the provider about a `TodoItem` and listens to it directly. Editing `Notes` raises `PropertyChanged` for another property, so the stream ignores it. Only the `Title` edit arrives, and the change names the item as its sender.

```csharp
INPCObservableForProperty provider = new();
Expression<Func<TodoItem, string>> property = x => x.Title;

Console.WriteLine($"Affinity: {provider.GetAffinityForObject(typeof(TodoItem), TitlePropertyName)}");
Console.WriteLine($"Affinity after a change: {provider.GetAffinityForObject(typeof(TodoItem), TitlePropertyName, false)}");

// TodoItem does not implement INotifyPropertyChanging, so the provider bids nothing for before-change observation.
Console.WriteLine($"Affinity before a change: {provider.GetAffinityForObject(typeof(TodoItem), TitlePropertyName, true)}");

List<IObservedChange<object, object?>> changes = [];
using (provider.GetNotificationForProperty(item, property.Body, TitlePropertyName).Subscribe(changes.Add))
{
    item.Notes = EditedNotes;
    item.Title = RetitledByPost;
}

Console.WriteLine($"Notifications: {changes.Count}");
Console.WriteLine($"Sender is the item: {ReferenceEquals(item, changes[0].Sender)}");
Console.WriteLine($"Property: {changes[0].GetPropertyName()}");
```

```text
Affinity: 5
Affinity after a change: 5
Affinity before a change: 0
Notifications: 1
Sender is the item: True
Property: Title
```

A plain class does not implement the interface, so this provider bids 0 and reports nothing. The excerpt shows why the fallback provider, not this one, serves such a class.

```csharp
INPCObservableForProperty provider = new();
Expression<Func<StorageObject, long>> property = x => x.Size;

Console.WriteLine($"Affinity: {provider.GetAffinityForObject(typeof(StorageObject), SizePropertyName)}");

List<IObservedChange<object, object?>> changes = [];
using (provider.GetNotificationForProperty(stored, property.Body, SizePropertyName).Subscribe(changes.Add))
{
    stored.Size = ResizedBytes;
}

Console.WriteLine($"Notifications: {changes.Count}");
```

```text
Affinity: 0
Notifications: 0
```

A MAUI control is a `BindableObject`, which implements `INotifyPropertyChanged`. `WhenChanged` delivers the text of an `Entry` at subscription and after each edit.

```csharp
TodoView view = new();

using (view.NewTitleTextBox.WhenChanged(x => x.Text).Subscribe(static text => Console.WriteLine(text ?? "(none)")))
{
    view.NewTitleTextBox.Text = TypedTitle;
}
```

```text
(none)
Call the plumber
```

The next excerpt asks both built-in providers about an `Entry`. The `PropertyChanged` provider bids 5 and the fallback provider bids 1, so the `PropertyChanged` mechanism wins for a MAUI control.

```csharp
INPCObservableForProperty propertyChanged = new();
POCOObservableForProperty fallback = new();

Console.WriteLine($"PropertyChanged provider: {propertyChanged.GetAffinityForObject(typeof(Entry), TextPropertyName)}");
Console.WriteLine($"Fallback provider: {fallback.GetAffinityForObject(typeof(Entry), TextPropertyName)}");
```

```text
PropertyChanged provider: 5
Fallback provider: 1
```

`POCOObservableForProperty` bids `Fallback` (1) for every type and property. Its stream delivers one notification when observation starts and then stays silent. It writes a debug message the first time it observes a type and property, unless the caller suppresses warnings. The last argument of `GetNotificationForProperty` is `suppressWarnings`.

```csharp
POCOObservableForProperty provider = new();
Expression<Func<StorageObject, long>> property = x => x.Size;

Console.WriteLine($"Affinity for a stored object: {provider.GetAffinityForObject(typeof(StorageObject), SizePropertyName)}");
Console.WriteLine($"Affinity for a to-do item: {provider.GetAffinityForObject(typeof(TodoItem), TitlePropertyName)}");

List<IObservedChange<object, object?>> changes = [];
using (provider.GetNotificationForProperty(stored, property.Body, SizePropertyName, false, true).Subscribe(changes.Add))
{
    Console.WriteLine($"Notifications at the start: {changes.Count}");

    stored.Size = ResizedBytes;
}

Console.WriteLine($"Notifications after the change: {changes.Count}");
Console.WriteLine($"Sender is the stored object: {ReferenceEquals(stored, changes[0].Sender)}");
Console.WriteLine($"Property: {changes[0].GetPropertyName()}");
```

```text
Affinity for a stored object: 1
Affinity for a to-do item: 1
Notifications at the start: 1
Notifications after the change: 1
Sender is the stored object: True
Property: Size
```

The fallback does not look at timing, so it answers a before-change request as well as an after-change one. Its score is the lowest positive one, so any other provider outranks it. The excerpt asks for both timings.

```csharp
POCOObservableForProperty provider = new();

Console.WriteLine($"Affinity after a change: {provider.GetAffinityForObject(typeof(StorageObject), SizePropertyName, false)}");
Console.WriteLine($"Affinity before a change: {provider.GetAffinityForObject(typeof(StorageObject), SizePropertyName, true)}");
```

```text
Affinity after a change: 1
Affinity before a change: 1
```

You get both built-in providers when you call `WithCoreServices` on the builder, so an application that registers no provider of its own can observe any type. The loop lists the registered providers, `INPCObservableForProperty` first.

```csharp
IReactiveUIBindingBuilder builder = RxBindingBuilder.CreateReactiveUIBindingBuilder();

_ = builder.WithCoreServices().BuildApp();

foreach (ICreatesObservableForProperty provider in AppLocator.Current.GetServices<ICreatesObservableForProperty>())
{
    Console.WriteLine(provider.GetType().Name);
}
```

```text
INPCObservableForProperty
POCOObservableForProperty
```

A path can pass through a notifying object to a plain one. This excerpt observes the selected object of a view model. The view model raises `PropertyChanged` for the selection, and the plain `Size` is read on each delivery. The first `Size` edit raises nothing, so it prints nothing.

```csharp
browser.SelectedObject = first;

using (browser.WhenChanged(x => x.SelectedObject!).Subscribe(static selected => Console.WriteLine(selected.Size)))
{
    first.Size = ResizedBytes;
    browser.SelectedObject = second;
}
```

```text
2411724
5562368
```

A class that implements `INotifyPropertyChanging` lets you read a property before an edit replaces it. `DraftTodo` implements it, so `WhenChanging` delivers the title on subscription and before the edit. Editing `Notes` delivers nothing.

```csharp
DraftTodo draft = new() { Title = RegistrationTitle };

using (draft.WhenChanging(x => x.Title).Subscribe(Console.WriteLine))
{
    draft.Title = RenamedTitle;
    draft.Notes = RegistrationNotes;
}
```

```text
Renew car registration
Renew car registration
```

The provider itself bids 5 for a before-change request on a `DraftTodo`. Its stream delivers the notification while the sender holds the old title.

```csharp
DraftTodo draft = new() { Title = RegistrationTitle };
INPCObservableForProperty provider = new();
Expression<Func<DraftTodo, string>> property = x => x.Title;

Console.WriteLine($"Affinity before a change: {provider.GetAffinityForObject(typeof(DraftTodo), TitlePropertyName, true)}");

List<string> titlesBefore = [];
using (provider.GetNotificationForProperty(draft, property.Body, TitlePropertyName, true).Subscribe(change => titlesBefore.Add(((DraftTodo)change.Sender).Title)))
{
    draft.Title = RenamedTitle;
}

Console.WriteLine($"Titles read before the change: {string.Join(", ", titlesBefore)}");
```

```text
Affinity before a change: 5
Titles read before the change: Renew car registration
```

## Call a provider directly

You can call a provider yourself. `CreatesObservableForPropertyMixins` adds overloads that default `beforeChanged` and `suppressWarnings` to `false`, so the short calls below ask about after-change observation. The last argument of the full `GetNotificationForProperty` call, `suppressWarnings`, silences the message the fallback provider writes. The provider reports its bid for the `State` property (8), for `Endpoint` (0) and for a before-change request (0). Its stream delivers one notification for each event and nothing on subscription.

```csharp
Expression<Func<StorageConnection, ConnectionState>> property = x => x.State;

Console.WriteLine($"Affinity for State: {provider.GetAffinityForObject(typeof(StorageConnection), StatePropertyName)}");
Console.WriteLine($"Affinity for Endpoint: {provider.GetAffinityForObject(typeof(StorageConnection), nameof(StorageConnection.Endpoint))}");
Console.WriteLine($"Affinity for State before a change: {provider.GetAffinityForObject(typeof(StorageConnection), StatePropertyName, true)}");

List<IObservedChange<object, object?>> after = [];
using (provider.GetNotificationForProperty(connection, property.Body, StatePropertyName).Subscribe(after.Add))
{
    connection.State = ConnectionState.Connecting;
    connection.State = ConnectionState.Connected;
}

Console.WriteLine($"Notifications: {after.Count}");
Console.WriteLine($"Sender is the connection: {ReferenceEquals(connection, after[0].Sender)}");
Console.WriteLine($"First value: {after[0].Value}");
Console.WriteLine($"Second value: {after[1].Value}");

List<IObservedChange<object, object?>> before = [];
using (provider.GetNotificationForProperty(connection, property.Body, StatePropertyName, true).Subscribe(before.Add))
{
    connection.State = ConnectionState.Connecting;
    connection.State = ConnectionState.Connected;
}

Console.WriteLine($"Notifications before a change: {before.Count}");
```

```text
Affinity for State: 8
Affinity for Endpoint: 0
Affinity for State before a change: 0
The connection state is observed by the StateChanged provider
Notifications: 2
Sender is the connection: True
First value: Connecting
Second value: Connected
Notifications before a change: 0
```

The full interface call takes all five arguments. This excerpt picks the registered provider with the highest bid, as the run-time engine does, and calls it through `ICreatesObservableForProperty`. The last two arguments are `beforeChanged` and `suppressWarnings`. This provider delivers the two changes and nothing on subscription.

```csharp
ICreatesObservableForProperty registered = AppLocator.Current
    .GetServices<ICreatesObservableForProperty>()
    .MaxBy(static candidate => candidate.GetAffinityForObject(typeof(StorageConnection), StatePropertyName, false))!;
Expression<Func<StorageConnection, ConnectionState>> property = x => x.State;

Console.WriteLine($"Affinity for State: {registered.GetAffinityForObject(typeof(StorageConnection), StatePropertyName, false)}");

using (registered.GetNotificationForProperty(connection, property.Body, StatePropertyName, false, true).Subscribe(static change => Console.WriteLine(change.Value)))
{
    connection.State = ConnectionState.Connecting;
    connection.State = ConnectionState.Connected;
}
```

```text
Affinity for State: 8
The connection state is observed by the StateChanged provider
Connecting
Connected
```

## How generated code consults providers

The `ObservableForProperty` extension method observes a property by asking the registered providers directly. It picks the provider with the highest affinity for the runtime type, and it throws `InvalidOperationException` when no provider bids above zero. It has overloads that take a property name or a lambda, and optional `beforeChange`, `skipInitial` and `isDistinct` arguments. It reads members by reflection, so it is not safe for trimming. The excerpt observes the connection state through the registered provider and prints every state the provider delivers.

```csharp
using (browser.Connection.ObservableForProperty(x => x.State, false).Subscribe(static change => Console.WriteLine(change.Value)))
{
    storage.Disconnect();
    await browser.ConnectAsync().ConfigureAwait(false);
}
```

```text
The connection state is observed by the StateChanged provider
Connected
Disconnected
Connecting
Connected
```

Generated code makes a choice for each link of a property path. It asks `FindHigherAffinityPlugin` for a registered provider that outranks the score of the mechanism it was generated from. `HasHigherAffinityPlugin` gives the same answer as a `bool`. The excerpt asks at four generated scores. The provider outranks the fallback, and it is the provider that outranks the `PropertyChanged` score. An equal score and a higher score both keep the generated mechanism.

```csharp
Type connectionType = typeof(StorageConnection);

bool outranksFallback = ObservationAffinityChecker.HasHigherAffinityPlugin(connectionType, StatePropertyName, BindingAffinity.Fallback, false);
ICreatesObservableForProperty? winnerOverPropertyChanged = ObservationAffinityChecker.FindHigherAffinityPlugin(connectionType, StatePropertyName, BindingAffinity.Explicit, false);

Console.WriteLine($"Outranks the fallback: {outranksFallback}");
Console.WriteLine($"Outranks the PropertyChanged provider: {winnerOverPropertyChanged?.GetType().Name}");

// A tie goes to the generated mechanism, and so does a higher generated score.
bool outranksEqual = ObservationAffinityChecker.HasHigherAffinityPlugin(connectionType, StatePropertyName, BindingAffinity.WinFormsEvent, false);
ICreatesObservableForProperty? winnerOverHigher = ObservationAffinityChecker.FindHigherAffinityPlugin(connectionType, StatePropertyName, BindingAffinity.Kvo, false);

Console.WriteLine($"Outranks an equal score: {outranksEqual}");
Console.WriteLine($"Nothing outranks a higher score: {winnerOverHigher is null}");

// The provider answers for one property and only after a change.
ICreatesObservableForProperty? forEndpoint = ObservationAffinityChecker.FindHigherAffinityPlugin(connectionType, nameof(StorageConnection.Endpoint), BindingAffinity.Fallback, false);
ICreatesObservableForProperty? beforeChange = ObservationAffinityChecker.FindHigherAffinityPlugin(connectionType, StatePropertyName, BindingAffinity.Fallback, true);

Console.WriteLine($"Nothing answers for Endpoint: {forEndpoint is null}");
Console.WriteLine($"Nothing answers before a change: {beforeChange is null}");
```

```text
Outranks the fallback: True
Outranks the PropertyChanged provider: StorageConnectionObservableForProperty
Outranks an equal score: False
Nothing outranks a higher score: True
Nothing answers for Endpoint: True
Nothing answers before a change: True
```

`PluginObservationSource.Choose` applies that rule to one link. You give it the source object, the expression, the property name, the before-change flag, the score of the generated mechanism, a getter and the generated observation. It returns the generated observation when no provider outranks the score.

```csharp
Expression<Func<TodoItem, bool>> isDone = x => x.IsDone;
PropertyObservable<bool> generated = new(item, nameof(TodoItem.IsDone), static source => ((TodoItem)source).IsDone, true);

IObservable<bool> chosen = PluginObservationSource.Choose(
    item,
    isDone.Body,
    nameof(TodoItem.IsDone),
    false,
    BindingAffinity.Explicit,
    static source => ((TodoItem)source).IsDone,
    generated);

Console.WriteLine($"The generated observation is kept: {ReferenceEquals(generated, chosen)}");
```

```text
The generated observation is kept: True
```

A provider that only ties the generated score does not take over either. Here the generated observation is an `EventObservable<ConnectionState>`, and the score comes from the provider's own bid.

```csharp
StorageConnection connection = new() { Endpoint = StorageEndpoint };
Expression<Func<StorageConnection, ConnectionState>> state = x => x.State;
EventObservable<ConnectionState> generated = new(
    handler => connection.StateChanged += handler,
    handler => connection.StateChanged -= handler,
    () => connection.State,
    true);

IObservable<ConnectionState> chosen = PluginObservationSource.Choose(
    connection,
    state.Body,
    StateName,
    false,
    provider.GetAffinityForObject(typeof(StorageConnection), StateName, false),
    static source => ((StorageConnection)source).State,
    generated);

Console.WriteLine($"The generated observation is kept: {ReferenceEquals(generated, chosen)}");
```

```text
The generated observation is kept: True
```

A low generated score hands the observation to the provider, and a high one keeps the generated observation. `UnchangingPropertyObservable<T>` stands in for the generated observation here. Subscribing to the provider's observable prints the provider's message and then the states.

```csharp
Expression<Func<StorageConnection, ConnectionState>> property = x => x.State;
UnchangingPropertyObservable<ConnectionState> generated = new(connection.State);

IObservable<ConnectionState> lowGenerated = PluginObservationSource.Choose(
    connection,
    property.Body,
    StatePropertyName,
    false,
    BindingAffinity.Fallback,
    static source => ((StorageConnection)source).State,
    generated);
IObservable<ConnectionState> highGenerated = PluginObservationSource.Choose(
    connection,
    property.Body,
    StatePropertyName,
    false,
    BindingAffinity.Kvo,
    static source => ((StorageConnection)source).State,
    generated);

Console.WriteLine($"A low score hands over to the provider: {lowGenerated is PluginPropertyObservable<ConnectionState>}");
Console.WriteLine($"A high score keeps the generated observation: {ReferenceEquals(generated, highGenerated)}");

using (lowGenerated.Subscribe(static state => Console.WriteLine(state)))
{
    connection.State = ConnectionState.Disconnected;
    connection.State = ConnectionState.Connected;
}
```

```text
A low score hands over to the provider: True
A high score keeps the generated observation: True
The connection state is observed by the StateChanged provider
Connected
Disconnected
Connected
```

When a provider does win, `Choose` returns a `PluginPropertyObservable<T>`. The provider says when the property changed, and the getter reads the value, so the value never comes from the notification. The observable delivers the current value on subscription and again after each notification. The last two constructor arguments are the before-change flag and the flag that drops repeated values.

```csharp
Expression<Func<StorageConnection, ConnectionState>> property = x => x.State;
PluginPropertyObservable<ConnectionState> observable = new(
    provider,
    connection,
    property.Body,
    StatePropertyName,
    static source => ((StorageConnection)source).State,
    false,
    true);

using (observable.Subscribe(static state => Console.WriteLine(state)))
{
    connection.State = ConnectionState.Disconnected;
    connection.State = ConnectionState.Connected;
}
```

```text
The connection state is observed by the StateChanged provider
Connected
Disconnected
Connected
```

`Subscribe` also takes an observer object. `Witness.Create<T>` builds one from a lambda. Every observable on this page takes one the same way.

```csharp
Expression<Func<StorageConnection, ConnectionState>> property = x => x.State;
PluginPropertyObservable<ConnectionState> observable = new(
    provider,
    connection,
    property.Body,
    StatePropertyName,
    static source => ((StorageConnection)source).State,
    false,
    true);
IObserver<ConnectionState> observer = Witness.Create<ConnectionState>(static state => Console.WriteLine(state));

using (observable.Subscribe(observer))
{
    connection.State = ConnectionState.Disconnected;
    connection.State = ConnectionState.Connected;
}
```

```text
The connection state is observed by the StateChanged provider
Connected
Disconnected
Connected
```

`ObservableForPropertySink<TSender, TValue>` wraps a provider's notifications and reads the value on each one. Its last two arguments skip the initial value and drop repeated values.

```csharp
Expression<Func<StorageConnection, ConnectionState>> property = x => x.State;
IObservable<IObservedChange<object, object?>> notifications = provider.GetNotificationForProperty(connection, property.Body, StatePropertyName);
ObservableForPropertySink<StorageConnection, ConnectionState> sink = new(
    connection,
    property.Body,
    notifications,
    () => connection.State,
    false,
    true);

using (sink.Subscribe(static change => Console.WriteLine(change.Value)))
{
    connection.State = ConnectionState.Disconnected;
    connection.State = ConnectionState.Connected;
}
```

```text
The connection state is observed by the StateChanged provider
Connected
Disconnected
Connected
```

The sink also accepts an observer object. Use this form when you have an `IObserver<T>` in hand, as the excerpt does. The values it receives are the same.

```csharp
Expression<Func<StorageConnection, ConnectionState>> property = x => x.State;
IObservable<IObservedChange<object, object?>> notifications = provider.GetNotificationForProperty(connection, property.Body, StatePropertyName);
ObservableForPropertySink<StorageConnection, ConnectionState> sink = new(
    connection,
    property.Body,
    notifications,
    () => connection.State,
    false,
    true);
IObserver<IObservedChange<StorageConnection, ConnectionState>> observer = Witness.Create<IObservedChange<StorageConnection, ConnectionState>>(static change => Console.WriteLine(change.Value));

using (sink.Subscribe(observer))
{
    connection.State = ConnectionState.Disconnected;
    connection.State = ConnectionState.Connected;
}
```

```text
The connection state is observed by the StateChanged provider
Connected
Disconnected
Connected
```

`ExpressionChainSink<TSender, TValue>` observes a whole path such as `x.Connection.State`. It observes each link on the value the previous link produced, and it attaches the deeper links again when a value in the middle changes. It delivers nothing while a value in the middle is null, and every change it delivers names the root object as its sender. `ExpressionChainParameters<TSender>` bundles the arguments. The chain engine reads members by reflection, so it is not safe for trimming.

```csharp
Expression<Func<StorageBrowserViewModel, ConnectionState>> property = x => x.Connection.State;
Expression[] links = [.. Reflection.Rewrite(property.Body).GetExpressionChain()];
ExpressionChainParameters<StorageBrowserViewModel> parameters = new(browser, property.Body, links, false, false, true, true);
ExpressionChainSink<StorageBrowserViewModel, ConnectionState> sink = new(
    parameters.Source,
    parameters.Expression,
    parameters.Links,
    parameters.BeforeChange,
    parameters.SkipInitial,
    parameters.IsDistinct,
    parameters.SuppressWarnings);

using (sink.Subscribe(change => Console.WriteLine($"{change.Value}, sent by the browser: {ReferenceEquals(browser, change.Sender)}")))
{
    browser.Connection.State = ConnectionState.Disconnected;
    browser.Connection.State = ConnectionState.Connected;
}
```

```text
The connection state is observed by the StateChanged provider
Connected, sent by the browser: True
Disconnected, sent by the browser: True
Connected, sent by the browser: True
```

The sink constructor also takes the values directly, so you do not need the parameters record. This excerpt passes them in one call and reads the changes through an observer object. It matters when you write your own wiring, because the sink needs only the source, the expression and the links.

```csharp
Expression<Func<StorageBrowserViewModel, ConnectionState>> property = x => x.Connection.State;
Expression[] links = [.. Reflection.Rewrite(property.Body).GetExpressionChain()];
ExpressionChainSink<StorageBrowserViewModel, ConnectionState> sink = new(browser, property.Body, links, false, false, true, true);
IObserver<IObservedChange<StorageBrowserViewModel, ConnectionState>> observer = Witness.Create<IObservedChange<StorageBrowserViewModel, ConnectionState>>(static change => Console.WriteLine(change.Value));

using (sink.Subscribe(observer))
{
    browser.Connection.State = ConnectionState.Disconnected;
    browser.Connection.State = ConnectionState.Connected;
}
```

```text
The connection state is observed by the StateChanged provider
Connected
Disconnected
Connected
```

## Command binders

A command binder does for `BindCommand` what a provider does for observation. It implements `ICreatesCommandBinding`, and the binder with the highest affinity for a control type serves the call. Without a binder, the generated binding hands the view model's command to the button. This excerpt binds the add button before any binder is registered and checks who holds the command.

```csharp
using (view.BindCommand(viewModel, x => x.AddCommand, v => v.AddButton))
{
    Console.WriteLine($"The button has no command: {view.AddButton.Command is null}");
    Console.WriteLine($"The button holds the view model's command: {ReferenceEquals(viewModel.AddCommand, view.AddButton.Command)}");
}
```

```text
The button has no command: False
The button holds the view model's command: True
```

The interface has three `BindCommandToObject` overloads. One binds to the control's default event. One binds to an event you name. The third takes `addHandler` and `removeHandler` delegates and needs no event lookup. `GetAffinityForObject<T>` receives `hasEventTarget`, which is true when the caller names an event.

### Write a command binder

`ClickCommandBinder` serves a MAUI `Button`. It bids `ExactType` (10) for a `Button` and 0 for other controls. It returns null when there is no command or the target is not a button. Returning null means no binding was created. These two members are the first part of the binder, and they show how a binder claims a control type.

```csharp
public int GetAffinityForObject<T>(bool hasEventTarget) =>
    typeof(Button).IsAssignableFrom(typeof(T)) ? BindingAffinity.ExactType : 0;

public IDisposable? BindCommandToObject<T>(ICommand? command, T? target, IObservable<object?> commandParameter)
    where T : class =>
    command is null || target is not Button button
        ? null
        : AttachToClicks(command, button, commandParameter, ObserveClicks(button));
```

`AttachToClicks` builds the binding from Primitives parts. A `BehaviorSignal` keeps the latest command parameter, and it starts with null. The binder feeds it from the `commandParameter` stream. A click runs the command only when `CanExecute` accepts the parameter. `Merge` joins the parameter stream with each `CanExecuteChanged` event, which re-reads the parameter, so `button.IsEnabled` follows `CanExecute`. An `ActionDisposable` puts the original `IsEnabled` back. A `MultipleDisposable` bundles every part, so disposing the binding detaches all of them. See [signals](../primitives/signals.md) and [disposables](../primitives/disposables.md).

```csharp
bool wasEnabled = button.IsEnabled;
BehaviorSignal<object?> parameter = new(null);
IObservable<EventPattern<EventArgs>> canExecuteChanged = Signal.FromEventPattern(handler => command.CanExecuteChanged += handler, handler => command.CanExecuteChanged -= handler);

return new(
    commandParameter.Subscribe(parameter.OnNext),
    clicks.Where(_ => command.CanExecute(parameter.Value)).Subscribe(_ => command.Execute(parameter.Value)),
    parameter.Merge(canExecuteChanged.Select(_ => parameter.Value)).Select(command.CanExecute).Subscribe(canRun => button.IsEnabled = canRun),
    new ActionDisposable(() => button.IsEnabled = wasEnabled),
    parameter);
```

Register a binder with `WithCommandBinder`. `CommandBinderService.GetBinder<T>` returns the binder with the highest affinity for a control type. The first registered binder wins a tie, and the method returns null when every binder bids 0 or less.

```csharp
IReactiveUIBindingBuilder builder = RxBindingBuilder.CreateReactiveUIBindingBuilder();

_ = builder.WithCoreServices().WithCommandBinder(binder).BuildApp();

Console.WriteLine($"Binder for a button: {ReferenceEquals(binder, CommandBinderService.GetBinder<Button>(false))}");
Console.WriteLine($"Binder for a button with an event target: {ReferenceEquals(binder, CommandBinderService.GetBinder<Button>(true))}");
Console.WriteLine($"No binder for an entry: {CommandBinderService.GetBinder<Entry>(false) is null}");
```

```text
Binder for a button: True
Binder for a button with an event target: True
No binder for an entry: True
```

`CommandBindingAffinityChecker.HasHigherAffinityPlugin<T>` tells generated code whether a registered binder outranks the generated mechanism for a control type. It reads the registrations on every call, so it needs no `Refresh`. The loop below runs over every `BindingAffinity` score. The binder bids 10, so it outranks the scores from `Fallback` up to `WinFormsEvent`, and it loses at `ExactType` and `Kvo`. An `Entry` has no binder, so nothing outranks the fallback.

```csharp
foreach (var (name, score) in scores)
{
    Console.WriteLine($"{name} ({score}): binder outranks it = {CommandBindingAffinityChecker.HasHigherAffinityPlugin<Button>(score, false)}");
}

Console.WriteLine($"Binder outranks the fallback for an entry: {CommandBindingAffinityChecker.HasHigherAffinityPlugin<Entry>(BindingAffinity.Fallback, false)}");
```

```text
Fallback (1): binder outranks it = True
DefaultInternalTypeConverter (2): binder outranks it = True
DefaultEvent (3): binder outranks it = True
WpfDependencyObject (4): binder outranks it = True
EventEnabledControl (4): binder outranks it = True
Explicit (5): binder outranks it = True
WinUiDependencyObject (6): binder outranks it = True
WinFormsEvent (8): binder outranks it = True
ExactType (10): binder outranks it = False
Kvo (15): binder outranks it = False
Binder outranks the fallback for an entry: False
```

With the binder registered, `BindCommand` never touches the button's own `Command` property. The binder enables the button while the command can run and runs the command on each click. A click after you dispose the binding does nothing. The binder prints a line each time it attaches.

```csharp
IAwaitSignal<TodoItem> added = viewModel.WhenChanged(x => x.SelectedItem).Where(static item => item is not null).Take(1).GetAwaiter();

using (view.BindCommand(viewModel, x => x.AddCommand, v => v.AddButton))
{
    Console.WriteLine($"The button has no command: {view.AddButton.Command is null}");
    Console.WriteLine($"Enabled with no title: {view.AddButton.IsEnabled}");

    viewModel.NewTitle = PlumberTitle;

    Console.WriteLine($"Enabled with a title: {view.AddButton.IsEnabled}");

    ((IButtonController)view.AddButton).SendClicked();
    await added;

    Console.WriteLine(viewModel.Items[^1].Title);
}

int itemCount = viewModel.Items.Count;

viewModel.NewTitle = PlumberTitle;
((IButtonController)view.AddButton).SendClicked();

Console.WriteLine($"A click after disposing adds nothing: {viewModel.Items.Count == itemCount}");
```

```text
The click binder is attached to the Add button
The button has no command: True
Enabled with no title: False
Enabled with a title: True
Call the plumber
A click after disposing adds nothing: True
```

A stream of parameters is another `BindCommand` argument. The binder passes the latest value to the command, and it enables the button only while the command accepts that value. Here the upload button stays disabled until the first file arrives on the stream. See [Bindings](bindings.md).

```csharp
IAwaitSignal<IReadOnlyList<StorageObject>> stored = browser.WhenChanged(x => x.Objects).Where(static objects => objects.Any(static item => item.Key == ScreenshotName)).Take(1).GetAwaiter();

using (view.BindCommand(browser, x => x.UploadCommand, v => v.UploadButton, uploads))
{
    Console.WriteLine($"Enabled with no file: {view.UploadButton.IsEnabled}");

    uploads.OnNext(new(ScreenshotName, ScreenshotBytes, PngType));

    Console.WriteLine($"Enabled with a file: {view.UploadButton.IsEnabled}");

    ((IButtonController)view.UploadButton).SendClicked();
    await stored;
}

Console.WriteLine($"Objects: {browser.Objects.Count}");
Console.WriteLine($"No error: {browser.ErrorMessage.Length == 0}");
```

```text
The click binder is attached to the Upload button
Enabled with no file: False
Enabled with a file: True
Objects: 6
No error: True
```

Name an event and the binder receives the event name and the type of its event data. `ClickCommandBinder` serves only `Clicked` and returns null for any other name. This excerpt names the `Clicked` event, which lets a control offer several events and the view choose one.

```csharp
browser.CurrentPrefix = PhotoFolder;

IAwaitSignal<IReadOnlyList<StorageObject>> listed = browser.WhenChanged(x => x.Objects).Skip(1).Take(1).GetAwaiter();

using (view.BindCommand(browser, x => x.RefreshCommand, v => v.RefreshButton, nameof(Button.Clicked)))
{
    ((IButtonController)view.RefreshButton).SendClicked();
    await listed;
}

Console.WriteLine(browser.Objects.Count);
```

```text
The click binder is asked for the Clicked event, which carries EventArgs
The click binder is attached to the Refresh button
2
```

You can also call a binder yourself. The default-event overload takes the command, the target and a parameter stream. `ImmutableEmptySignal<object>` produces no values, so the parameter stays null.

```csharp
browser.CurrentPrefix = string.Empty;

IAwaitSignal<IReadOnlyList<StorageObject>> listed = browser.WhenChanged(x => x.Objects).Skip(1).Take(1).GetAwaiter();

using (binder.BindCommandToObject(browser.RefreshCommand, view.RefreshButton, ImmutableEmptySignal<object>.Instance))
{
    ((IButtonController)view.RefreshButton).SendClicked();
    await listed;
}

Console.WriteLine(browser.Objects.Count);
```

```text
The click binder is attached to the Refresh button
6
```

The overload with `addHandler` and `removeHandler` names the event through two delegates. The binder subscribes and unsubscribes through them, so nothing is looked up by name and the call is compatible with Native AOT. The excerpt binds the reconnect button, which starts disabled-looking work only when the command can run, and prints the link state after the click.

```csharp
Button button = view.ConnectButton;
storage.Disconnect();

IAwaitSignal<ConnectionState> connected = browser.WhenChanged(x => x.ConnectionStatus).Where(static state => state == ConnectionState.Connected).Take(1).GetAwaiter();

using (binder.BindCommandToObject<Button, EventArgs>(
    browser.ConnectCommand,
    button,
    ImmutableEmptySignal<object>.Instance,
    handler => button.Clicked += handler.Invoke,
    handler => button.Clicked -= handler.Invoke))
{
    Console.WriteLine($"Enabled while disconnected: {button.IsEnabled}");

    ((IButtonController)button).SendClicked();
    await connected;
}

Console.WriteLine($"Connection: {storage.Connection.State}");
```

```text
The click binder is attached to the Reconnect button
Enabled while disconnected: True
Connection: Connected
```

## Run a command for each value

`CommandInvoker.Invoke` executes a command with each value a stream produces. It asks `CanExecute` for each value and drops a value the command refuses. An error from the stream is rethrown on the thread that raised it, and completion is ignored. The first overload takes one fixed command. Disposing the result stops the executions. This excerpt lists the objects again each time the prefix property changes, and it shows that a change after disposal lists nothing.

```csharp
browser.CurrentPrefix = string.Empty;

IAwaitSignal<IReadOnlyList<StorageObject>>? listed = browser.WhenChanged(x => x.Objects).Skip(1).Take(1).GetAwaiter();

using (CommandInvoker.Invoke(browser.WhenChanged(x => x.CurrentPrefix), browser.RefreshCommand))
{
    await listed;

    Console.WriteLine($"Objects in the whole bucket: {browser.Objects.Count}");

    listed = browser.WhenChanged(x => x.Objects).Skip(1).Take(1).GetAwaiter();
    browser.CurrentPrefix = PhotoFolder;
    await listed;

    Console.WriteLine($"Objects in the photo folder: {browser.Objects.Count}");
}

browser.CurrentPrefix = string.Empty;

Console.WriteLine($"Objects after disposing: {browser.Objects.Count}");
```

```text
Objects in the whole bucket: 6
Objects in the photo folder: 2
Objects after disposing: 2
```

The second overload takes a stream of commands and executes whichever command arrived last. It subscribes to the commands first, so a command the stream produces on subscription is held before any value can arrive. A null command drops the values that arrive while it is current, and a later command takes over from the next value. Replacing the command runs nothing by itself. The excerpt sends a file while the command is null, which drops it, and then sends a second file after a command arrives.

```csharp
browser.CurrentPrefix = string.Empty;
await browser.LoadObjectsAsync().ConfigureAwait(false);

int objectCount = browser.Objects.Count;
IAwaitSignal<IReadOnlyList<StorageObject>> stored = browser.WhenChanged(x => x.Objects).Where(static objects => objects.Any(static item => item.Key == ThirdScreenshotName)).Take(1).GetAwaiter();

using (CommandInvoker.Invoke(uploads, commands))
{
    commands.OnNext(null);
    uploads.OnNext(new(SecondScreenshotName, ScreenshotBytes, PngType));

    Console.WriteLine($"No command yet, nothing uploaded: {browser.Objects.Count == objectCount}");

    commands.OnNext(browser.UploadCommand);
    uploads.OnNext(new(ThirdScreenshotName, ScreenshotBytes, PngType));
    await stored;
}

Console.WriteLine($"One file uploaded: {browser.Objects.Count == objectCount + 1}");
Console.WriteLine($"The first file was dropped: {!browser.Objects.Any(static item => item.Key == SecondScreenshotName)}");
```

```text
No command yet, nothing uploaded: True
One file uploaded: True
The first file was dropped: True
```

## The observable types

Generated code and the run-time engine build the types below. You rarely build one yourself. The examples build each by hand to show what it delivers. Each type also accepts an observer object in `Subscribe`. `Witness.Create<T>` builds one from a lambda, as [Writing your own operator](../primitives/advanced.md) shows.

| Type | What it delivers |
| --- | --- |
| `PropertyObservable<T>` | The value at subscription and after each `PropertyChanged` for one property. |
| `PropertyChangingObservable<T>` | The value at subscription and before each `PropertyChanging` for one property. |
| `NotifyPropertyChangedObservable` | An `IObservedChange` for each notification, with no value. |
| `EventObservable<T>` | The value at subscription and after each raise of a plain event. |
| `UnchangingPropertyObservable<T>` | One value, and then nothing. |
| `AppliedChangeObservable` | Each `BindingChange` a binding writes. |
| `CombineLatestObservable` | A result from the latest value of two to sixteen sources. |
| `PluginPropertyObservable<T>` | The value read after each provider notification. |
| `PluginObservationSource` | The choice between a generated observation and a provider. |

`PropertyObservable<T>` takes the source, the property name, a getter and a flag. With the flag true it drops a value equal to the last one. A change notification with a null or empty property name applies to every property. This excerpt observes whether an item is urgent. Setting the priority to `Low` repeats `False`, so the observable drops it, and a subscriber never sees a change that is not one.

```csharp
TodoItem item = new() { Title = RegistrationTitle };
PropertyObservable<bool> isUrgent = new(item, nameof(TodoItem.Priority), static source => ((TodoItem)source).Priority == TodoPriority.High, true);

using (isUrgent.Subscribe(Console.WriteLine))
{
    item.Priority = TodoPriority.Low;
    item.Priority = TodoPriority.High;
}
```

```text
False
True
```

The next excerpt builds the same observable with the flag false. Use this form when every change matters, even one that repeats the last value. Each priority change delivers a value.

```csharp
TodoItem item = new() { Title = RegistrationTitle };
PropertyObservable<bool> isUrgent = new(item, nameof(TodoItem.Priority), static source => ((TodoItem)source).Priority == TodoPriority.High, false);

using (isUrgent.Subscribe(Console.WriteLine))
{
    item.Priority = TodoPriority.Low;
    item.Priority = TodoPriority.High;
}
```

```text
False
False
True
```

Any of these observables also accepts an observer object in place of a lambda. An observer is an `IObserver<T>`, and `Witness.Create<T>` builds one from a lambda. The excerpt subscribes the first observable that way and prints the same values.

```csharp
TodoItem item = new() { Title = RegistrationTitle };
PropertyObservable<bool> isUrgent = new(item, nameof(TodoItem.Priority), static source => ((TodoItem)source).Priority == TodoPriority.High, true);
IObserver<bool> observer = Witness.Create<bool>(Console.WriteLine);

using (isUrgent.Subscribe(observer))
{
    item.Priority = TodoPriority.Low;
    item.Priority = TodoPriority.High;
}
```

```text
False
True
```

`PropertyChangingObservable<T>` reads the value before each edit. It does not drop repeats, because the value has not changed when the event fires. The first line is the value at subscription. The next two are the values before each edit.

```csharp
DraftTodo draft = new() { Title = RegistrationTitle };
PropertyChangingObservable<string> beforeChange = new(draft, nameof(DraftTodo.Title), static source => ((DraftTodo)source).Title);

using (beforeChange.Subscribe(Console.WriteLine))
{
    draft.Title = RenamedTitle;
    draft.Title = FinalTitle;
    draft.Notes = RegistrationNotes;
}
```

```text
Renew car registration
Renew car registration
Renew car registration online
```

The same observable works with an observer object. This excerpt reads the title before one edit and prints two lines, the value at subscription and the value before the edit.

```csharp
DraftTodo draft = new() { Title = RegistrationTitle };
PropertyChangingObservable<string> beforeChange = new(draft, nameof(DraftTodo.Title), static source => ((DraftTodo)source).Title);
IObserver<string> observer = Witness.Create<string>(Console.WriteLine);

using (beforeChange.Subscribe(observer))
{
    draft.Title = RenamedTitle;
}
```

```text
Renew car registration
Renew car registration
```

`NotifyPropertyChangedObservable` delivers no value on subscription. Each change names the sender and the expression, and you read the property from the sender. Pass `true` as the last argument to observe before the change. This excerpt observes after the change and prints the property name with the new title. The `Notes` edit is ignored, so the observable reports only the property you named.

```csharp
DraftTodo draft = new() { Title = RegistrationTitle };
Expression<Func<DraftTodo, string>> title = x => x.Title;
NotifyPropertyChangedObservable afterChange = new(draft, title.Body, nameof(DraftTodo.Title), false);

using (afterChange.Subscribe(static change => Console.WriteLine($"{change.GetPropertyName()}: {((DraftTodo)change.Sender).Title}")))
{
    draft.Title = RenamedTitle;
    draft.Notes = RegistrationNotes;
}
```

```text
Title: Renew car registration online
```

With `true` as the last argument, the observable reports before the change. Reading the property from the sender then gives the old title, which is useful when you want to keep the value that is about to be replaced. The `Notes` edit is ignored.

```csharp
DraftTodo draft = new() { Title = RegistrationTitle };
Expression<Func<DraftTodo, string>> title = x => x.Title;
NotifyPropertyChangedObservable beforeChange = new(draft, title.Body, nameof(DraftTodo.Title), true);

using (beforeChange.Subscribe(static change => Console.WriteLine($"{change.GetPropertyName()}: {((DraftTodo)change.Sender).Title}")))
{
    draft.Title = RenamedTitle;
    draft.Notes = RegistrationNotes;
}
```

```text
Title: Renew car registration
```

The next excerpt passes an observer object and prints only the property name of each change.

```csharp
DraftTodo draft = new() { Title = RegistrationTitle };
Expression<Func<DraftTodo, string>> title = x => x.Title;
NotifyPropertyChangedObservable afterChange = new(draft, title.Body, nameof(DraftTodo.Title), false);
IObserver<IObservedChange<object, object?>> observer = Witness.Create<IObservedChange<object, object?>>(static change => Console.WriteLine(change.GetPropertyName()));

using (afterChange.Subscribe(observer))
{
    draft.Title = RenamedTitle;
}
```

```text
Title
```

`EventObservable<T>` observes a class that raises a plain event. It takes delegates that add and remove the handler, a getter and the flag that drops repeats. It ignores the event arguments and reads the getter on subscription and on each raise. It never completes. Use it for a class that offers only a plain event. This excerpt observes whether a connection is open. A repeated `False` is dropped, and the event after disposal delivers nothing.

```csharp
StorageConnection connection = new() { Endpoint = StorageEndpoint };
EventObservable<bool> isOpen = new(
    handler => connection.StateChanged += handler,
    handler => connection.StateChanged -= handler,
    () => connection.State == ConnectionState.Connected,
    true);

using (isOpen.Subscribe(Console.WriteLine))
{
    connection.State = ConnectionState.Connecting;
    connection.State = ConnectionState.Connected;
    connection.State = ConnectionState.Disconnected;
}

connection.State = ConnectionState.Connected;
```

```text
False
True
False
```

With the flag false, every event delivers a value, including a repeat. This excerpt raises three events and receives the value at subscription plus one value for each event.

```csharp
StorageConnection connection = new() { Endpoint = StorageEndpoint };
EventObservable<bool> isOpen = new(
    handler => connection.StateChanged += handler,
    handler => connection.StateChanged -= handler,
    () => connection.State == ConnectionState.Connected,
    false);

using (isOpen.Subscribe(Console.WriteLine))
{
    connection.State = ConnectionState.Connecting;
    connection.State = ConnectionState.Connected;
    connection.State = ConnectionState.Disconnected;
}
```

```text
False
False
True
False
```

The event observable also takes an observer object. This excerpt raises two events and prints two values, because the repeated `False` is dropped.

```csharp
StorageConnection connection = new() { Endpoint = StorageEndpoint };
EventObservable<bool> isOpen = new(
    handler => connection.StateChanged += handler,
    handler => connection.StateChanged -= handler,
    () => connection.State == ConnectionState.Connected,
    true);
IObserver<bool> observer = Witness.Create<bool>(Console.WriteLine);

using (isOpen.Subscribe(observer))
{
    connection.State = ConnectionState.Connecting;
    connection.State = ConnectionState.Connected;
}
```

```text
False
True
```

`UnchangingPropertyObservable<T>` observes a property whose owner raises no notification. It delivers the value once and stays silent. It never completes, so a binding that observes it stays subscribed. The error and completion handlers below never run.

```csharp
StorageObject file = new() { Key = "photos/2026/launch.png" };
UnchangingPropertyObservable<string> key = new(file.Key);

using (key.Subscribe(
    Console.WriteLine,
    static error => Console.WriteLine($"Failed: {error.Message}"),
    static () => Console.WriteLine("Completed")))
{
    file.Key = "photos/2026/renamed.png";
}
```

```text
photos/2026/launch.png
```

With an observer object, the subscription delivers the same single value. Keep the returned subscription in a `using` variable so it is disposed at the end of the method.

```csharp
StorageObject file = new() { Key = "photos/2026/launch.png" };
UnchangingPropertyObservable<string> key = new(file.Key);
IObserver<string> observer = Witness.Create<string>(Console.WriteLine);

using IDisposable subscription = key.Subscribe(observer);
```

```text
photos/2026/launch.png
```

`AppliedChangeObservable` reports the changes a binding wrote. `OnNext` publishes a `BindingChange`, which carries the `Value` and a `FromViewModel` flag. The flag separates an edit from the echo of one. `HasObservers` tells you whether anyone is subscribed, and it turns false again when a subscriber disposes its subscription. The excerpt shows a subscriber that is counted while it listens and forgotten after it disposes its subscription. See [Bindings](bindings.md).

```csharp
AppliedChangeObservable applied = new();

Console.WriteLine($"Observers at the start: {applied.HasObservers}");

using (applied.Subscribe(static change => Console.WriteLine($"{change.Value}, from the view model: {change.FromViewModel}")))
{
    Console.WriteLine($"Observers while subscribed: {applied.HasObservers}");

    applied.OnNext(new(RenamedTitle, true));
    applied.OnNext(new(FinalTitle, false));
}

applied.OnNext(new(RegistrationTitle, true));

Console.WriteLine($"Observers after disposing: {applied.HasObservers}");
```

```text
Observers at the start: False
Observers while subscribed: True
Renew car registration online, from the view model: True
Renew car registration by post, from the view model: False
Observers after disposing: False
```

An observer object receives the same `BindingChange` values. This excerpt publishes one change and prints it.

```csharp
AppliedChangeObservable applied = new();
IObserver<BindingChange> observer = Witness.Create<BindingChange>(static change => Console.WriteLine($"{change.Value}, from the view model: {change.FromViewModel}"));

using (applied.Subscribe(observer))
{
    applied.OnNext(new(RenamedTitle, true));
}
```

```text
Renew car registration online, from the view model: True
```

A two-way `Bind` returns a binding whose `Changed` property is an `AppliedChangeObservable`. The next excerpt edits the entry, then the view model, and reads the side that produced each change. The `FromViewModel` flag lets a subscriber tell an edit from the echo of one, and the entry ends up showing the view model's value.

```csharp
TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());
TodoView view = new() { ViewModel = viewModel };

using IReactiveBinding<TodoView, BindingChange> binding = view.Bind(viewModel, x => x.NewTitle, v => v.NewTitleTextBox.Text);
using IDisposable subscription = binding.Changed.Subscribe(static change => Console.WriteLine($"{change.Value}, from the view model: {change.FromViewModel}"));

view.NewTitleTextBox.Text = RenamedTitle;
viewModel.NewTitle = FinalTitle;

Console.WriteLine($"The entry shows: {view.NewTitleTextBox.Text}");
```

```text
Renew car registration online, from the view model: False
Renew car registration by post, from the view model: True
The entry shows: Renew car registration by post
```

## Combine several sources

`CombineLatestObservable.Create` combines two to sixteen sources. It delivers a result once every source has produced a value, and again each time any source produces a new one. This excerpt joins the title and the done flag of an item into a checklist line. `Title(item)` and `IsDone(item)` are helpers in the example that return a `PropertyObservable<T>`.

```csharp
TodoItem item = new() { Title = RegistrationTitle };

using (CombineLatestObservable
    .Create(Title(item), IsDone(item), static (title, isDone) => (isDone ? DoneMark : OpenMark) + title)
    .Subscribe(Console.WriteLine))
{
    item.IsDone = true;
    item.Title = RenamedTitle;
}
```

```text
[ ] Renew car registration
[x] Renew car registration
[x] Renew car registration online
```

Every larger count follows the same pattern: pass the sources and then a selector that takes one value for each source. `CombineLatestObservable.Create` overloads take from 2 to 16 sources. The [example project](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/mechanisms/CombineLatestExamples.cs) has one method for each count from two to sixteen.

## Where to go next

- [Observing](observing.md) covers `WhenChanged` and the other observation methods.
- [Bindings](bindings.md) covers `BindCommand` and the other binding methods.
- [Threading and platforms](threading.md) covers the platform mechanisms.
- [Setup](setup.md) covers the builder and `WithCoreServices`.
- [Unsafe twins and the runtime fallback](unsafe.md) covers call sites the generator cannot read.
- [API reference](api.md) lists every public type and member.

## API reference

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`BindingAffinity`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingAffinity.cs) | Holds the scores that providers, command binders and converters share. | Static class of read-only `int` fields: `Fallback` 1, `DefaultInternalTypeConverter` 2, `DefaultEvent` 3, `WpfDependencyObject` 4, `EventEnabledControl` 4, `Explicit` 5, `WinUiDependencyObject` 6, `WinFormsEvent` 8, `ExactType` 10, `Kvo` 15. | A higher score wins. Zero means the candidate does not apply. |
| [`ICreatesObservableForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/ICreatesObservableForProperty.cs) | Supplies one mechanism for observing properties, called a provider. | Interface with `GetAffinityForObject` and `GetNotificationForProperty`. | The registration with the highest affinity for a type and property serves the observation. |
| [`ICreatesObservableForProperty.GetAffinityForObject`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/ICreatesObservableForProperty.cs) | Scores how well the provider can observe a property of a type. | `Type type`, `string propertyName`, `bool beforeChanged`. Returns `int`. | A positive score means the provider can observe the property. Zero or a negative score means it cannot. |
| [`ICreatesObservableForProperty.GetNotificationForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/ICreatesObservableForProperty.cs) | Creates a stream that delivers each time the property changes. | `object sender`, `Expression expression`, `string propertyName`, `bool beforeChanged`, `bool suppressWarnings`. Returns `IObservable<IObservedChange<object, object?>>`. | Callers read the current value from `sender`, not from the change. `suppressWarnings` silences the warning for a property that cannot notify. |
| [`CreatesObservableForPropertyMixins.GetAffinityForObject`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/CreatesObservableForPropertyMixins.cs) | Asks a provider for its after-change score. | Extension on `ICreatesObservableForProperty` with `Type type` and `string propertyName`. Returns `int`. | Passes `false` for `beforeChanged`. Throws `ArgumentNullException` when the provider is null. |
| [`CreatesObservableForPropertyMixins.GetNotificationForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/CreatesObservableForPropertyMixins.cs) | Creates a change stream without naming the two flags. | Extension on `ICreatesObservableForProperty` with `object sender`, `Expression expression`, `string propertyName` and an optional `bool beforeChanged`. Returns `IObservable<IObservedChange<object, object?>>`. | Passes `false` for `suppressWarnings`, and for `beforeChanged` when you omit it. Throws `ArgumentNullException` when the provider is null. |
| [`INPCObservableForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/INPCObservableForProperty.cs) | Provides the `PropertyChanged` mechanism. | Public class with a parameterless constructor. | `WithCoreServices` registers it. |
| [`INPCObservableForProperty.GetAffinityForObject`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/INPCObservableForProperty.cs) | Scores a type by the notification interface it implements. | Returns `BindingAffinity.Explicit` (5) or 0. | Needs `INotifyPropertyChanged` for after-change requests and `INotifyPropertyChanging` for before-change requests. Ignores the property name. |
| [`INPCObservableForProperty.GetNotificationForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/INPCObservableForProperty.cs) | Delivers a change each time the sender raises a notification for the property. | Returns `IObservable<IObservedChange<object, object?>>`. | Returns a stream that never delivers when the sender implements neither interface. A before-change request on a sender without `INotifyPropertyChanging` uses `PropertyChanged`. An indexer matches the name followed by `[]`. Ignores `suppressWarnings`. Throws `ArgumentNullException` when `expression` is null. |
| [`POCOObservableForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/POCOObservableForProperty.cs) | Provides the fallback mechanism for a class that raises nothing. | Sealed public class with a parameterless constructor. | `WithCoreServices` registers it after `INPCObservableForProperty`. |
| [`POCOObservableForProperty.GetAffinityForObject`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/POCOObservableForProperty.cs) | Scores every type and property at the lowest positive score. | Returns `BindingAffinity.Fallback` (1). | Ignores `beforeChanged`. Throws `ArgumentNullException` when `type` or `propertyName` is null. |
| [`POCOObservableForProperty.GetNotificationForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/ObservableForProperty/POCOObservableForProperty.cs) | Delivers one change on subscription and then nothing. | Returns `IObservable<IObservedChange<object, object?>>`. | Never completes, so a binding stays subscribed. Writes a debug message once for each type and property unless `suppressWarnings` is true. Ignores `beforeChanged`. Throws `ArgumentNullException` when `sender`, `expression` or `propertyName` is null. |
| [`ObservationAffinityChecker`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/ObservationAffinityChecker.cs) | Finds a registered provider that outranks the mechanism the generator picked. | Static class. | Hidden from IntelliSense. Reads registrations on first use and caches the best score for each type, property and timing. |
| [`ObservationAffinityChecker.FindHigherAffinityPlugin`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/ObservationAffinityChecker.cs) | Returns the provider that beats a given score. | `Type type`, `string propertyName`, `int generatedAffinity`, `bool beforeChanged`. Returns `ICreatesObservableForProperty?`. | Returns null when no provider scores strictly higher, so a tie keeps the generated mechanism. Throws `ArgumentNullException` when `type` or `propertyName` is null. |
| [`ObservationAffinityChecker.HasHigherAffinityPlugin`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/ObservationAffinityChecker.cs) | Tells you whether a provider beats a given score. | Same four arguments as `FindHigherAffinityPlugin`. Returns `bool`. | True exactly when `FindHigherAffinityPlugin` returns a provider. |
| [`ObservationAffinityChecker.Refresh`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/ObservationAffinityChecker.cs) | Clears the cached registrations and scores. | No arguments. | Call it after you register a provider. A lookup that runs at the same time can finish with the registrations it read. |
| [`PluginObservationSource.Choose`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Observables/PluginObservationSource.cs) | Picks between a generated observation and a provider that outranks it. | `object source`, `Expression expression`, `string propertyName`, `bool beforeChange`, `int generatedAffinity`, `Func<object, T?> getter`, `IObservable<T> generated`. Returns `IObservable<T>`. | Returns `generated` when no provider outranks `generatedAffinity`. Otherwise it returns a `PluginPropertyObservable<T>` that keeps repeated values. Static class, hidden from IntelliSense. Throws `ArgumentNullException` when `source` or `generated` is null. |
| [`PluginPropertyObservable<T>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Observables/PluginPropertyObservable.cs) | Observes one property through a provider. The provider says when the property changed and the getter reads the value. | Sealed class. Constructor: `ICreatesObservableForProperty plugin`, `object source`, `Expression expression`, `string propertyName`, `Func<object, T?> getter`, `bool beforeChange`, `bool distinctUntilChanged`. `Subscribe(IObserver<T>)` returns `IDisposable`. | Delivers the current value on subscription and after each notification. Passes on a provider error or completion after any value that waits to be read. Hidden from IntelliSense. Throws `ArgumentNullException` for a null reference argument. |
| [`CommandBindingAffinityChecker.HasHigherAffinityPlugin<T>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/CommandBindingAffinityChecker.cs) | Tells you whether a registered binder outranks the generated command binding for a control type. | `T` is the control type. `int generatedAffinity`, `bool hasEventTarget`. Returns `bool`. | Reads the registrations on every call, so it needs no refresh. Static class, hidden from IntelliSense. |
| [`CommandBinderService.GetBinder<T>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/CommandBinding/CommandBinderService.cs) | Returns the binder with the highest affinity for a control type. | `T` is the control type. `bool hasEventTarget`. Returns `ICreatesCommandBinding?`. | The first registered binder wins a tie. Returns null when every binder scores 0 or less. Static class, hidden from IntelliSense. |
| [`CommandInvoker.Invoke`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/CommandBinding/CommandInvoker.cs) | Runs a command with each value a stream produces. | `IObservable<T> source` plus either an `ICommand` or an `IObservable<ICommand?>`. Returns `IDisposable`. | Skips a value that `CanExecute` refuses. With a stream of commands, a null command drops values and the latest command runs. A source error is rethrown on the thread that raised it, and completion is ignored. Throws `ArgumentNullException` for a null argument. Static class, hidden from IntelliSense. |
| [`ICreatesCommandBinding`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/ICreatesCommandBinding.cs) | Binds an `ICommand` to a control. | Interface with `GetAffinityForObject<T>` and three `BindCommandToObject` overloads. | The binder with the highest affinity for a control type serves `BindCommand`. Register one with `WithCommandBinder`. |
| [`ICreatesCommandBinding.GetAffinityForObject`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/ICreatesCommandBinding.cs) | Scores how well the binder fits a control type. | `T` is the control type. `bool hasEventTarget`. Returns `int`. | A positive score means the binder can bind the control. Zero or a negative score means it cannot. `hasEventTarget` is true when the caller names an event. |
| [`ICreatesCommandBinding.BindCommandToObject`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/ICreatesCommandBinding.cs) | Binds a command to a control. | `ICommand? command`, `T? target`, `IObservable<object?> commandParameter`, and one of three forms: nothing (default event), `string eventName`, or `addHandler` and `removeHandler` delegates. Returns `IDisposable?`. | Returns null when no binding is created, and a null command creates none. The default-event and named-event forms carry `RequiresUnreferencedCode`. The delegate form needs no event lookup. |
| [`AppliedChangeObservable`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Observables/AppliedChangeObservable.cs) | Carries the changes a two-way binding wrote. | Sealed class with a parameterless constructor. `Subscribe(IObserver<BindingChange>)` returns `IDisposable`. | Disposing the subscription removes the observer. Throws `ArgumentNullException` for a null observer. |
| [`AppliedChangeObservable.OnNext`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Observables/AppliedChangeObservable.cs) | Reports a change the binding wrote. | `BindingChange value`, which holds `object? Value` and `bool FromViewModel`. | Reaches every observer subscribed at that moment. `FromViewModel` tells an edit from the echo of one. |
| [`AppliedChangeObservable.HasObservers`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Observables/AppliedChangeObservable.cs) | Tells you whether anyone is subscribed. | Read-only `bool` property. | False again after the last subscriber disposes its subscription. |
| [`CombineLatestObservable.Create`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Observables/CombineLatestObservable.cs) | Combines the latest value of several sources. | Two to sixteen sources, `IObservable<T1>` up to `IObservable<T16>`, and a `Func<T1, ..., T16, TResult>` selector. Returns `IObservable<TResult>`. | Delivers once every source has produced a value, then again on each new value from any source. Static class, hidden from IntelliSense. |
| [`EventObservable<T>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Observables/EventObservable.cs) | Observes a value that changes when a plain event is raised. | Sealed class. Constructor: `Action<EventHandler> addHandler`, `Action<EventHandler> removeHandler`, `Func<T> getter`, `bool distinctUntilChanged`. `Subscribe(IObserver<T>)` returns `IDisposable`. | Reads the getter on subscription and on each raise, and ignores the event arguments. With the flag true it drops a value equal to the last one. Never completes. A getter that throws on subscription makes `Subscribe` throw after the handler is removed. Hidden from IntelliSense. Throws `ArgumentNullException` for a null argument. |
| [`NotifyPropertyChangedObservable`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Observables/NotifyPropertyChangedObservable.cs) | Delivers an observed change for each notification about one property. | Sealed class. Constructor: `object sender`, `Expression expression`, `string expectedName`, `bool beforeChanged`. `Subscribe(IObserver<IObservedChange<object, object?>>)` returns `IDisposable`. | Delivers nothing on subscription, and a change carries no value. A null or empty notification name matches every property. Observes `PropertyChanging` when `beforeChanged` is true and the sender implements it, and `PropertyChanged` otherwise. Never completes. Hidden from IntelliSense. Throws `ArgumentNullException` for a null argument. |
| [`PropertyChangingObservable<T>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Observables/PropertyChangingObservable.cs) | Reads a property before each change. | Sealed class. Constructor: `INotifyPropertyChanging source`, `string propertyName`, `Func<INotifyPropertyChanging, T?> getter`. `Subscribe(IObserver<T>)` returns `IDisposable`. | Delivers the current value on subscription. Keeps repeated values, because the value has not changed when the event fires. A null or empty notification name matches every property. Hidden from IntelliSense. Throws `ArgumentNullException` for a null argument. |
| [`PropertyObservable<T>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Observables/PropertyObservable.cs) | Reads a property on subscription and after each change. | Sealed class. Constructor: `INotifyPropertyChanged source`, `string propertyName`, `Func<INotifyPropertyChanged, T?> getter`, `bool distinctUntilChanged`. `Subscribe(IObserver<T>)` returns `IDisposable`. | With the flag true it drops a value equal to the last one. A null or empty notification name matches every property. Hidden from IntelliSense. Throws `ArgumentNullException` for a null argument. |
| [`UnchangingPropertyObservable<T>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Observables/UnchangingPropertyObservable.cs) | Observes a property whose owner raises no notification. | Sealed class. Constructor: `T value`. `Subscribe(IObserver<T>)` returns `IDisposable`. | Delivers the value once and then nothing. Never completes, so a binding that observes it stays subscribed. Hidden from IntelliSense. Throws `ArgumentNullException` for a null observer. |
