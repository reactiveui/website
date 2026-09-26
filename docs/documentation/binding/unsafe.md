---
Order: 11
---
# Unsafe twins and the runtime fallback

[Run the complete page example](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/unsafe/unsafe.csproj).

Most calls in this section are read by the source generator while your project builds. The generator needs the lambda written in the call, such as `x => x.Title`. Sometimes you cannot write it there. The path may sit in a variable, or the name of a column may come from a user, a file or a database. Then the generator has nothing to read, and the plain method throws when it runs.

An **Unsafe twin** is the method for that case. `WhenChangedUnsafe` is the twin of `WhenChanged`, and `BindOneWayUnsafe` is the twin of `BindOneWay`. A twin takes the same kind of arguments, but it reads the property path while the app runs, by reflection. It delivers the same values as the plain method.

Use the plain method whenever you can write the lambda in the call. It is faster and safe to trim. Reach for a twin only when the path is not known until run time.

## Call an Unsafe twin

The examples use a small to-do app. `OriginalTitle` holds the text `Renew car registration`, and the other constants hold sample text of the same kind.

**1. See what a plain call does with a stored path.** The lambda lives in the variable `titleColumn`, so the generator cannot read it at the call. The plain `WhenChanged` is a **stub**, a method that only throws. The message names the twin to use.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
Expression<Func<TodoItem, string>> titleColumn = x => x.Title;

try
{
    using IDisposable subscription = item.WhenChanged(titleColumn).Subscribe(Console.WriteLine);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}
```

```text
No generated WhenChanged dispatch matched this call site. Use WhenChangedUnsafe to resolve the expression at run time.
```

**2. Call the twin before the app is built.** `WhenChangedUnsafe` reads the path at run time. It then asks the registered services how to observe `Title`. The app has registered none, so the call fails. The example prints the first sentence of the message.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
Expression<Func<TodoItem, string>> titleColumn = x => x.Title;

try
{
    using IDisposable subscription = item.WhenChangedUnsafe(titleColumn).Subscribe(Console.WriteLine);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message.Split(". ")[0]);
}
```

```text
Could not find a ICreatesObservableForProperty for ReactiveUI.Binding.Documentation.Todo.TodoItem property Title
```

**3. Build the application once, at startup.** `WithCoreServices` registers the default ways to observe a property and to convert a value. `WithConverter` adds a converter, and `WithCommandBinder` adds a binder that connects a command to a control. Later sections use both. [Setup](setup.md) explains the builder, [Custom converters](custom-converters.md) covers converters, and [Mechanisms](mechanisms.md) covers binders and observation providers.

```csharp
IReactiveUIBindingBuilder builder = RxBindingBuilder.CreateReactiveUIBindingBuilder();

_ = builder
    .WithCoreServices()
    .WithConverter(new TodoItemTitleConverter())
    .WithCommandBinder(new ButtonClickCommandBinder())
    .BuildApp();
```

**4. Call the twin again.** The services are registered, so the twin reads the path and observes the property. It delivers the current title when you subscribe.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
Expression<Func<TodoItem, string>> titleColumn = x => x.Title;

using IDisposable subscription = item.WhenChangedUnsafe(titleColumn).Subscribe(Console.WriteLine);
```

```text
Renew car registration
```

## What a twin costs

- **It reads members by reflection.** The twin walks the path at run time, so it is slower than generated code.
- **It needs registered services.** The twin looks up how to observe each property and how to convert each value. Build the application first, as step 3 shows. The registered observation provider with the highest affinity wins. [Mechanisms](mechanisms.md) explains affinity.
- **It warns a trimmed publish.** Each twin carries `[RequiresUnreferencedCode]`. That attribute tells the compiler the method reads members by name, so trimming and Native AOT can remove code it needs. The warning appears at your own call site. [Setup](setup.md) covers trimming.
- **It writes on the owning thread only with a platform module.** A generated binding carries its own fallback for WPF, WinForms and MAUI. A twin uses only the invokers your app registers. [Threading and platforms](threading.md) shows how.

The analyzer marks a call the generator cannot read with the info diagnostic `RXUIBIND001`. [Setup](setup.md) lists the diagnostics.

## Observe properties chosen at run time

Every observation method has a twin. A twin takes an `Expression<Func<TObj, T>>` in place of a lambda the generator reads. Your code can build that expression itself. This example builds `row => row.Title` from a name known only at run time.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
ParameterExpression row = Expression.Parameter(typeof(TodoItem), "row");
Expression<Func<TodoItem, string>> titleColumn = Expression.Lambda<Func<TodoItem, string>>(Expression.Property(row, nameof(TodoItem.Title)), row);
List<string> titles = [];

using (item.WhenChangedUnsafe(titleColumn).Subscribe(titles.Add))
{
    item.Title = RenamedTitle;
}

Console.WriteLine(string.Join(", ", titles));
```

```text
Renew car registration, Renew car registration online
```

The twins repeat the same shape for more properties. Each table row below names the twin and the most paths one call takes.

| Twin | Delivers | Most paths in one call |
| --- | --- | --- |
| `WhenChangedUnsafe` | Each value after it changes | 16 |
| `WhenChangingUnsafe` | Each value before it changes | 16 |
| `WhenAnyValueUnsafe` | Each value after it changes, from any object that notifies | 16 |
| `WhenAnyUnsafe` | The result of a selector that receives each change | 12 |
| `WhenAnyObservableUnsafe` | The values of the streams that properties hold | 12 |

`WhenChangedUnsafe` and `WhenChangingUnsafe` take one to sixteen paths. With one path they deliver the value. With two to sixteen they deliver a `PropertyValues` struct, or the result of a selector you add at the end. `WhenAnyValueUnsafe` takes one to sixteen paths, and a selector is allowed from one path up. `WhenAnyUnsafe` takes one to twelve paths and always takes a selector. `WhenAnyObservableUnsafe` takes one to twelve paths to merge streams, and two to twelve with a selector to combine them. [Observing](observing.md) explains `PropertyValues`, selectors and the plain methods.

The [example project](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/tree/main/src/examples/Documentation/Pages/unsafe) has one class for each wide family, such as `UnsafeWhenChangedWideExamples`, `UnsafeWhenAnyValueWideSelectorExamples` and `UnsafeWhenAnyObservableWideExamples`. Each class has one method for every path count.

**Add a selector.** A selector turns the observed values into one result. It receives the values in the order of the paths. This selector joins a title and a done flag into one line, so a screen can show one value instead of two. The output shows a line for the starting state and a line after the flag changes.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
Expression<Func<TodoItem, string>> titleColumn = x => x.Title;
Expression<Func<TodoItem, bool>> doneColumn = x => x.IsDone;
List<string> lines = [];

using (item.WhenChangedUnsafe(titleColumn, doneColumn, static (title, done) => done ? $"[x] {title}" : $"[ ] {title}").Subscribe(lines.Add))
{
    item.IsDone = true;
}

Console.WriteLine(string.Join(", ", lines));
```

```text
[ ] Renew car registration, [x] Renew car registration
```

**Observe before a change.** `WhenChangingUnsafe` delivers the value a property held before each change, and nothing when you subscribe. The object must raise `INotifyPropertyChanging`. This example changes the title twice and receives the two earlier titles.

```csharp
EditableTodoItem item = new EditableTodoItem { Title = OriginalTitle };
Expression<Func<EditableTodoItem, string>> titleColumn = x => x.Title;
List<string> titles = [];

using (item.WhenChangingUnsafe(titleColumn).Subscribe(titles.Add))
{
    item.Title = RenamedTitle;
    item.Title = FinalTitle;
}

Console.WriteLine(string.Join(", ", titles));
```

```text
Renew car registration, Renew car registration online
```

**Map a value with a selector.** A selector turns the observed values into one result, and it receives the values in the order of the paths. `WhenAnyValueUnsafe` takes a selector even for one path. This selector maps each title to its length.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
Expression<Func<TodoItem, string>> titleColumn = x => x.Title;
List<int> lengths = [];

using (item.WhenAnyValueUnsafe(titleColumn, static title => title.Length).Subscribe(lengths.Add))
{
    item.Title = RenamedTitle;
}

Console.WriteLine(string.Join(", ", lengths));
```

```text
22, 29
```

**Read the change itself.** `WhenAnyUnsafe` passes each change to the selector. A change holds the object that changed, in `Sender`, and the new value, in `Value`.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
Expression<Func<TodoItem, string>> titleColumn = x => x.Title;
List<string> lines = [];

using (item.WhenAnyUnsafe(titleColumn, static change => $"{change.Sender.Id}: {change.Value}").Subscribe(lines.Add))
{
    item.Title = RenamedTitle;
}

Console.WriteLine(string.Join(", ", lines));
```

```text
0: Renew car registration, 0: Renew car registration online
```

**Follow streams that properties hold.** `WhenAnyObservableUnsafe` watches a property that holds an `IObservable<string>` and delivers the values of the stream it holds. When the property gets a new stream, the twin switches to it. With several paths it merges the streams, so each value arrives as its stream delivers it.

```csharp
TodoAnnouncer announcer = new();
Expression<Func<TodoAnnouncer, IObservable<string>?>> latestColumn = x => x.Latest;
Expression<Func<TodoAnnouncer, IObservable<string>?>> urgentColumn = x => x.Urgent;
List<string> messages = [];

using (announcer.WhenAnyObservableUnsafe(latestColumn, urgentColumn).Subscribe(messages.Add))
{
    announcer.Latest = Signal.Return(SyncComplete);
    announcer.Urgent = Signal.Return(DiskFull);
}

Console.WriteLine(string.Join(", ", messages));
```

```text
Sync complete, Disk full
```

Add a selector as the last argument to combine the latest value of each stream. The selector runs when every stream has delivered a value, so this example prints one line.

```csharp
TodoAnnouncer announcer = new();
Expression<Func<TodoAnnouncer, IObservable<string>?>> latestColumn = x => x.Latest;
Expression<Func<TodoAnnouncer, IObservable<string>?>> urgentColumn = x => x.Urgent;
List<string> lines = [];

using (announcer.WhenAnyObservableUnsafe(latestColumn, urgentColumn, static (latest, urgent) => $"{latest} / {urgent}").Subscribe(lines.Add))
{
    announcer.Latest = Signal.Return(SyncComplete);
    announcer.Urgent = Signal.Return(DiskFull);
}

Console.WriteLine(string.Join(", ", lines));
```

```text
Sync complete / Disk full
```

## Bind with the Unsafe twins

A binding twin takes two expressions, one for each side, and an optional conversion. The twins are `BindOneWayUnsafe`, `BindTwoWayUnsafe`, `OneWayBindUnsafe`, `BindUnsafe`, `BindToUnsafe`, `BindCommandUnsafe`, `InvokeCommandUnsafe` and `BindInteractionUnsafe`. The [API reference](#api-reference) lists the extra arguments of each.

"Written view first" means the call starts from the view, as in `view.BindUnsafe(viewModel, ...)`. [Bindings](bindings.md) explains each plain method.

**Copy a property one way.** The `BindOneWayUnsafe` call takes the source, then the target, then the two expressions. Without a conversion the two properties share one type.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new();
Expression<Func<TodoListViewModel, string>> source = x => x.FilterText;
Expression<Func<TodoView, string>> target = v => v.FilterTextBox.Text;

using (viewModel.BindOneWayUnsafe(view, source, target))
{
    viewModel.FilterText = FilterQuery;
}

Console.WriteLine(view.FilterTextBox.Text);
```

```text
car
```

**Convert both ways.** A two-way twin takes one conversion for each direction. A value that a conversion cannot handle is written as the default of the destination type.

```csharp
TransferViewModel viewModel = new(new InMemoryBankingBackend());
TransferView view = new();
Expression<Func<TransferViewModel, decimal>> source = x => x.Draft.Amount;
Expression<Func<TransferView, string>> target = v => v.AmountTextBox.Text;

using (viewModel.BindTwoWayUnsafe(
    view,
    source,
    target,
    static amount => amount.ToString("F2", CultureInfo.InvariantCulture),
    static text => decimal.Parse(text, CultureInfo.InvariantCulture)))
{
    view.AmountTextBox.Text = AmountText;

    Console.WriteLine(viewModel.Draft.Amount);
}
```

```text
125.50
```

**Start from the view.** `OneWayBindUnsafe` and `BindUnsafe` are called on the view and take the view model as the first argument. `OneWayBindUnsafe` accepts a selector.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new() { ViewModel = viewModel };
Expression<Func<TodoListViewModel, int>> source = x => x.RemainingCount;
Expression<Func<TodoView, string>> target = v => v.RemainingLabel.Text;

using (view.OneWayBindUnsafe(viewModel, source, target, static count => $"{count} left"))
{
    Console.WriteLine(view.RemainingLabel.Text);
}
```

```text
3 left
```

A path through an indexer, such as `x => x.Items[0].Title`, is one the generator leaves alone and RXUIBIND006 reports. The `Unsafe` overloads read it at run time. The binding observes the item the indexer returns, so renaming that item updates the box.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new() { ViewModel = viewModel };
Expression<Func<TodoListViewModel, string>> source = x => x.Items[0].Title;
Expression<Func<TodoView, string>> target = v => v.SelectedTitleTextBox.Text;

using (view.OneWayBindUnsafe(viewModel, source, target))
{
    Console.WriteLine(view.SelectedTitleTextBox.Text);

    // The binding observes the item the indexer returns, so renaming it updates the box.
    viewModel.Items[0].Title = RenamedTitle;
    Console.WriteLine(view.SelectedTitleTextBox.Text);
}
```

```text
Renew car registration
Renew registration online
```

`BindUnsafe` carries the property both ways. Without conversion functions it uses the registered converters. With them it takes the same two functions as `BindTwoWayUnsafe`. The [update stream section](#hold-a-write-until-a-signal-fires) shows it in use.

**Write a stream to a property.** `BindToUnsafe` writes each value a stream delivers. It converts the value with the converter registered for the two types. Pass a hint, a converter or both to change that. A null target writes nothing.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new();
Expression<Func<TodoListViewModel, TodoItem?>> source = x => x.SelectedItem;
Expression<Func<TodoView, string?>> target = v => v.SelectedTitleTextBox.Text;

using (viewModel.WhenChangedUnsafe(source).BindToUnsafe(view, target))
{
    viewModel.SelectedItem = viewModel.Items[0];

    Console.WriteLine(view.SelectedTitleTextBox.Text);
}
```

```text
Renew car registration
```

The hint and the converter follow the target expression. The `TodoItemTitleConverter` in the example reads the hint to upper-case the title. [Custom converters](custom-converters.md) shows how to write one. The next call names only the converter. It passes no hint, so the converter leaves the title as it is.

```csharp
using (viewModel.WhenChangedUnsafe(source).BindToUnsafe(view, target, new TodoItemTitleConverter()))
{
    viewModel.SelectedItem = viewModel.Items[0];

    Console.WriteLine(view.SelectedTitleTextBox.Text);
}
```

```text
Renew car registration
```

The next call names only the hint. The library hands the hint to the converter registered for the two types, which reads it and upper-cases the title. You can change how a registered converter works without naming the converter.

```csharp
using (viewModel.WhenChangedUnsafe(source).BindToUnsafe(view, target, TodoItemTitleConverter.UpperCaseHint))
{
    viewModel.SelectedItem = viewModel.Items[0];

    Console.WriteLine(view.SelectedTitleTextBox.Text);
}
```

```text
RENEW CAR REGISTRATION
```

The next call names both a hint and a converter. Use it when you want a specific converter to receive a hint in the same call.

```csharp
using (viewModel.WhenChangedUnsafe(source).BindToUnsafe(view, target, TodoItemTitleConverter.UpperCaseHint, new TodoItemTitleConverter()))
{
    viewModel.SelectedItem = viewModel.Items[0];

    Console.WriteLine(view.SelectedTitleTextBox.Text);
}
```

```text
RENEW CAR REGISTRATION
```

**Run a command for each value.** `InvokeCommandUnsafe` runs the command a property holds for each value of a stream. It skips a value while the property holds no command or the command cannot run. A null target runs nothing. The example waits for the command with a `TaskCompletionSource` that a `PropertyChanged` handler completes. The excerpts leave that wait-handler out.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
Expression<Func<TodoListViewModel, TodoItem?>> source = x => x.SelectedItem;
Expression<Func<TodoListViewModel, ICommand?>> command = x => x.CompleteCommand;

using (viewModel.WhenChangedUnsafe(source).InvokeCommandUnsafe(viewModel, command))
{
    viewModel.SelectedItem = viewModel.Items[0];
    await completed.Task;
}

Console.WriteLine(viewModel.RemainingCount);
```

```text
2
```

**Bind a command to a button.** `BindCommandUnsafe` connects the command to the control. It finds the way to do that in the registered command binders, and step 3 registered one for a MAUI `Button`. Without a binder for the control, the command is not connected. Passing `null` as the last argument picks the control's default event. A null view model binds nothing.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new() { ViewModel = viewModel };
Expression<Func<TodoListViewModel, Command?>> command = x => x.AddCommand;
Expression<Func<TodoView, Button>> button = v => v.AddButton;

using (view.BindCommandUnsafe(viewModel, command, button, null))
{
    viewModel.NewTitle = NewItemTitle;
    ((IButtonController)view.AddButton).SendClicked();
    await itemAdded.Task;
}

Console.WriteLine(viewModel.Items.Count);
```

```text
5
```

Give the command a parameter with an overload that takes a view model property or an `IObservable<T>`. Both go after the control expression and before the event name. The next call passes a stream that delivers one title, and it names the `Clicked` event. The command receives that title as its parameter when the user clicks the button, so the button can send a value the view model did not store.

```csharp
using (view.BindCommandUnsafe(viewModel, command, button, Signal.Return<string?>(NewItemTitle), nameof(Button.Clicked)))
{
    viewModel.NewTitle = NewItemTitle;
    ((IButtonController)view.AddButton).SendClicked();
    await itemAdded.Task;
}

Console.WriteLine(viewModel.Items.Count);
```

```text
5
```

The next call reads the parameter from the view model property `NewTitle`. The command receives whatever the property holds when the button is clicked, so the view model stays in charge of the value.

```csharp
Expression<Func<TodoListViewModel, string?>> parameter = x => x.NewTitle;

using (view.BindCommandUnsafe(viewModel, command, button, parameter, null))
{
    viewModel.NewTitle = NewItemTitle;
    ((IButtonController)view.AddButton).SendClicked();
    await itemAdded.Task;
}

Console.WriteLine(viewModel.Items.Count);
```

```text
5
```

**Register an interaction handler.** `BindInteractionUnsafe` registers a handler on the interaction a property holds. It moves the handler when the property gets another interaction. The handler returns a `Task`, or an `IObservable<T>` in the second overload. A null view model registers nothing.

```csharp
TransferViewModel viewModel = new(new InMemoryBankingBackend());
TransferView view = new() { ViewModel = viewModel };
Expression<Func<TransferViewModel, IInteraction<TransferDraft, bool>>> interaction = x => x.ConfirmTransfer;

using (view.BindInteractionUnsafe(
    viewModel,
    interaction,
    static context =>
    {
        context.SetOutput(true);
        return Task.CompletedTask;
    }))
{
    bool confirmed = await viewModel.ConfirmTransfer.Handle(viewModel.Draft);

    Console.WriteLine(confirmed);
}
```

```text
True
```

The observable form of the handler returns a stream instead of a `Task`. This handler sets the output and returns a stream that delivers one value, which suits code that works with streams. The output shows the same answer as the task form.

```csharp
using (view.BindInteractionUnsafe(
    viewModel,
    interaction,
    static context =>
    {
        context.SetOutput(true);
        return Signal.Return(true);
    }))
{
    bool confirmed = await viewModel.ConfirmTransfer.Handle(viewModel.Draft);

    Console.WriteLine(confirmed);
}
```

```text
True
```

## Back a property with ToPropertyUnsafe

`ToPropertyUnsafe` is the twin of [`ToProperty`](properties.md). It reads the property name when the helper is
created, from an expression such as `x => x.Headline` or from any string. It then finds the member that raises the
type's notification by reflection. It tries ReactiveUI's raise extensions for an `IReactiveObject`, then a
`RaisePropertyChanged`, `OnPropertyChanged`, `NotifyPropertyChanged` or `NotifyOfPropertyChange` method, then the
field behind a field-like `PropertyChanged` event. Reflection reaches a protected or private raise method, so the
type does not have to be `partial`.

The view model below inherits only a protected `RaisePropertyChanged` and is not `partial`. `ToProperty` reports
RXUIBIND012 for it, and `ToPropertyUnsafe` backs the property instead.

```csharp
public sealed class TodoHeadlineViewModel : ObservableObject
{
    private readonly ObservableAsPropertyHelper<string> _headline;

    public TodoHeadlineViewModel(IObservable<string> titles) =>
        _headline = titles.ToPropertyUnsafe(this, x => x.Headline);

    public string Headline => _headline.Value;
}
```

The snippet below pushes a new title and prints the headline and the notifications raised after the helper was
created.

```csharp
BehaviorSignal<string> titles = new(OriginalTitle);
TodoHeadlineViewModel viewModel = new TodoHeadlineViewModel(titles);
List<string> raised = [];
viewModel.PropertyChanged += (_, e) => raised.Add(e.PropertyName ?? string.Empty);

titles.OnNext(RenamedTitle);

Console.WriteLine(viewModel.Headline);
Console.WriteLine(string.Join(", ", raised));
```

```text
Renew car registration online
Headline
```

A type with none of those members throws `InvalidOperationException` when the helper is created. The overloads take
the same initial value, initial-value factory, `deferSubscription`, scheduler and `out` arguments as `ToProperty`.

## Write on a sequencer
A **sequencer** decides when queued work runs. [Threading and platforms](threading.md) explains sequencers and the thread that owns a view. Each property-binding twin has an overload that takes an `ISequencer`. The binding delivers each write to the target on that sequencer, and the first write waits too. A newer value replaces a value that waits.

A null sequencer means the binding writes on the thread that owns the target. An immediate sequencer writes inline. The overloads are members of `ReactiveSchedulerExtensions`.

`BindOneWayUnsafe` and `BindTwoWayUnsafe` take a sequencer with no conversion, with conversion `Func` values, or with converters and a hint. `OneWayBindUnsafe` takes one with a selector, or with a converter and a hint. `BindUnsafe` takes one with two conversion `Func` values, or with two converters and a hint.

The sequencer goes after the expressions and any conversion `Func` values. When you pass converters, the order is the converters, the sequencer, then the hint. The example uses a `VirtualClock` from `ReactiveUI.Primitives`. It holds the work until the example calls `Start()`, as a UI thread does between two turns of its message loop.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new();
VirtualClock sequencer = new();
Expression<Func<TodoListViewModel, string>> source = x => x.FilterText;
Expression<Func<TodoView, string>> target = v => v.FilterTextBox.Text;

using (viewModel.BindOneWayUnsafe(view, source, target, sequencer))
{
    viewModel.FilterText = FilterQuery;

    Console.WriteLine($"'{view.FilterTextBox.Text}'");

    sequencer.Start();

    Console.WriteLine($"'{view.FilterTextBox.Text}'");
}
```

```text
''
'car'
```

A two-way binding waits in only one direction. The write to the target waits on the sequencer, and the write back to the source does not. This means a user's edit reaches the view model at once, while the screen updates when the sequencer runs.

```csharp
using (viewModel.BindTwoWayUnsafe(view, source, target, sequencer))
{
    viewModel.FilterText = FilterQuery;

    Console.WriteLine($"'{view.FilterTextBox.Text}'");

    sequencer.Start();

    Console.WriteLine($"'{view.FilterTextBox.Text}'");

    view.FilterTextBox.Text = SecondFilterQuery;

    Console.WriteLine($"'{viewModel.FilterText}'");
}
```

```text
''
'car'
'tax'
```

A conversion `Func` goes before the sequencer. This call turns an `int` count into text for a label, and the label stays empty until the sequencer starts.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new();
VirtualClock sequencer = new();
Expression<Func<TodoListViewModel, int>> source = x => x.RemainingCount;
Expression<Func<TodoView, string>> target = v => v.RemainingLabel.Text;

using (viewModel.BindOneWayUnsafe(view, source, target, static count => count.ToString(CultureInfo.InvariantCulture), sequencer))
{
    Console.WriteLine($"'{view.RemainingLabel.Text}'");

    sequencer.Start();

    Console.WriteLine($"'{view.RemainingLabel.Text}'");
}
```

```text
''
'3'
```

Converter objects go before the sequencer and the hint goes after it. This call passes a converter object, the sequencer and a hint that asks for upper case. The output shows the converted title once the sequencer starts.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new();
VirtualClock sequencer = new();
Expression<Func<TodoListViewModel, TodoItem?>> source = x => x.SelectedItem;
Expression<Func<TodoView, string>> target = v => v.SelectedTitleTextBox.Text;

using (viewModel.BindOneWayUnsafe(view, source, target, new TodoItemTitleConverter(), sequencer, TodoItemTitleConverter.UpperCaseHint))
{
    viewModel.SelectedItem = viewModel.Items[0];
    sequencer.Start();

    Console.WriteLine(view.SelectedTitleTextBox.Text);
}
```

```text
RENEW CAR REGISTRATION
```

A two-way twin takes two converter objects, the sequencer and the hint. This call carries an amount between a `decimal` and text, and it passes `null` for the hint. The output shows the amount as text after the model changes, and then the amount in the model after the view changes.

```csharp
TransferViewModel viewModel = new(new InMemoryBankingBackend());
TransferView view = new();
VirtualClock sequencer = new();
Expression<Func<TransferViewModel, decimal>> source = x => x.Draft.Amount;
Expression<Func<TransferView, string>> target = v => v.AmountTextBox.Text;

using (viewModel.BindTwoWayUnsafe(view, source, target, new DecimalToStringTypeConverter(), new StringToDecimalTypeConverter(), sequencer, null))
{
    sequencer.Start();
    viewModel.Draft.Amount = AmountValue;
    sequencer.Start();

    Console.WriteLine(view.AmountTextBox.Text);

    view.AmountTextBox.Text = SecondAmountText;

    Console.WriteLine(viewModel.Draft.Amount);
}
```

```text
125.50
10.00
```

`OneWayBindUnsafe` takes a selector before the sequencer. `BindUnsafe` takes two conversion `Func` values, or two converters and a hint. This call starts from the view and turns a count into text with a selector. The label stays empty until the sequencer starts.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new() { ViewModel = viewModel };
VirtualClock sequencer = new();
Expression<Func<TodoListViewModel, int>> source = x => x.RemainingCount;
Expression<Func<TodoView, string>> target = v => v.RemainingLabel.Text;

using (view.OneWayBindUnsafe(viewModel, source, target, static count => $"{count} left", sequencer))
{
    Console.WriteLine($"'{view.RemainingLabel.Text}'");

    sequencer.Start();

    Console.WriteLine($"'{view.RemainingLabel.Text}'");
}
```

```text
''
'3 left'
```

The next `BindUnsafe` call starts from the view and carries an amount both ways through two converter objects. It passes `null` for the hint. The output shows the amount as text once the sequencer has run.

```csharp
TransferViewModel viewModel = new(new InMemoryBankingBackend());
TransferView view = new() { ViewModel = viewModel };
VirtualClock sequencer = new();
Expression<Func<TransferViewModel, decimal>> source = x => x.Draft.Amount;
Expression<Func<TransferView, string>> target = v => v.AmountTextBox.Text;

using (view.BindUnsafe(viewModel, source, target, new DecimalToStringTypeConverter(), new StringToDecimalTypeConverter(), sequencer, null))
{
    sequencer.Start();
    viewModel.Draft.Amount = AmountValue;
    sequencer.Start();

    Console.WriteLine(view.AmountTextBox.Text);
}
```

```text
125.50
```

## Hold a write until a signal fires

A two-way binding copies every edit at once. Sometimes one side should wait. A filter box may search only when the user commits the text. A screen may redraw only when a refresh tick arrives. `BindUnsafe` takes an **update stream** for this. Each value the stream delivers tells the binding to copy one direction. The values themselves are ignored.

`TriggerUpdate` names the direction the stream drives.

| `TriggerUpdate` | What the stream drives | What is not held back |
| --- | --- | --- |
| `ViewToViewModel` | Writes to the view model | View model changes reach the view at once. |
| `ViewModelToView` | Writes to the view | View edits reach the view model at once. |

`ViewToViewModel` is the default, so the first example names no direction. The binding holds the view edit until the `commit` stream fires. It then copies the text to the view model.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new() { ViewModel = viewModel };
Signal<EventArgs> commit = new();

using (view.BindUnsafe(viewModel, x => x.FilterText, v => v.FilterTextBox.Text, commit))
{
    view.FilterTextBox.Text = FilterQuery;

    Console.WriteLine($"'{viewModel.FilterText}'");

    commit.OnNext(EventArgs.Empty);

    Console.WriteLine($"'{viewModel.FilterText}'");

    viewModel.FilterText = SecondFilterQuery;

    Console.WriteLine($"'{view.FilterTextBox.Text}'");
}
```

```text
''
'car'
'tax'
```

Pass `TriggerUpdate.ViewModelToView` after the stream to hold view model changes instead. The view model change then reaches the view when the `refresh` stream fires, and a view edit reaches the view model at once.

The binding reads the property when the stream fires, not when the property changes. Several changes between two signals become one write with the latest value. This example changes the text twice and counts one write.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new() { ViewModel = viewModel };
Signal<EventArgs> refresh = new();
int writes = 0;

using (view.BindUnsafe(viewModel, x => x.FilterText, v => v.FilterTextBox.Text, refresh, TriggerUpdate.ViewModelToView))
{
    view.FilterTextBox.PropertyChanged += (_, e) => writes += e.PropertyName == nameof(view.FilterTextBox.Text) ? 1 : 0;

    viewModel.FilterText = FilterQuery;
    viewModel.FilterText = SecondFilterQuery;
    refresh.OnNext(EventArgs.Empty);

    Console.WriteLine(view.FilterTextBox.Text);
    Console.WriteLine(writes);
}
```

```text
tax
1
```

When the binding starts, it writes the view model value to the view in both directions, so a view never starts empty while it waits for the first signal. A null stream turns the option off. The binding then observes both properties and copies each change at once, as `BindUnsafe` does without a stream.

A converted binding takes the two conversion `Func` values before the stream. This example refreshes an amount that the view shows as text.

```csharp
TransferViewModel viewModel = new(new InMemoryBankingBackend());
TransferView view = new() { ViewModel = viewModel };
Signal<EventArgs> refresh = new();

using (view.BindUnsafe(
    viewModel,
    x => x.Draft.Amount,
    v => v.AmountTextBox.Text,
    static amount => amount.ToString("F2", CultureInfo.InvariantCulture),
    static text => decimal.Parse(text, CultureInfo.InvariantCulture),
    refresh,
    TriggerUpdate.ViewModelToView))
{
    viewModel.Draft.Amount = AmountValue;

    Console.WriteLine(view.AmountTextBox.Text);

    refresh.OnNext(EventArgs.Empty);

    Console.WriteLine(view.AmountTextBox.Text);
}
```

```text
0.00
125.50
```

## Call the runtime fallback types

The twins are thin. Each one passes its expressions to a static class in `ReactiveUI.Binding.Fallback`, and generated code calls the same classes. You can call them yourself. All but `RuntimeBindingConverter` are hidden from IntelliSense, because generated code is their main caller. The observation, binding, command and interaction methods carry `[RequiresUnreferencedCode]`.

Most binding members end with a `bindingExpression` string. It names the bound expression in the log entry that a failed write produces. The examples pass constants such as `FilterExpression`, which holds `x => x.FilterText`.

### Observe with RuntimeObservationFallback

`RuntimeObservationFallback` has `WhenChanged`, `WhenChanging` and `WhenAnyValue`. Each takes one, two or three property expressions. The after-change methods deliver the current value when you subscribe. When a link along the path is null, they deliver nothing until the path resolves. The methods for two or three paths deliver once every path has a value.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
List<string> titles = [];

using (RuntimeObservationFallback.WhenChanged(item, x => x.Title).Subscribe(titles.Add))
{
    item.Title = RenamedTitle;
}

Console.WriteLine(string.Join(", ", titles));
```

```text
Renew car registration, Renew car registration online
```

Two paths deliver a `PropertyValues` struct with both values on every change. This call watches a title and a done flag, and each change prints both. You get a consistent pair without reading the second property yourself.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };

using (RuntimeObservationFallback.WhenChanged(item, x => x.Title, x => x.IsDone).Subscribe(static values => Console.WriteLine($"{values.Property1} {values.Property2}")))
{
    item.IsDone = true;
}
```

```text
Renew car registration False
Renew car registration True
```

`WhenChanged` accepts up to three paths. Three paths follow the same pattern and deliver all three values on each change.

`WhenChanging` delivers the value before each change. The object must raise `INotifyPropertyChanging`, and the call delivers nothing when you subscribe. This call changes the title once and collects the value the title held before that change. Use it when you need the old value, for example to undo an edit.

```csharp
EditableTodoItem item = new EditableTodoItem { Title = OriginalTitle };
List<string> titles = [];

using (RuntimeObservationFallback.WhenChanging(item, x => x.Title).Subscribe(titles.Add))
{
    item.Title = RenamedTitle;
}

Console.WriteLine(string.Join(", ", titles));
```

```text
Renew car registration
```

It accepts up to three paths. With two paths, each change delivers both values as they stood before the change. This call changes the notes, and the output shows the old notes with the title.

```csharp
EditableTodoItem item = new EditableTodoItem { Title = OriginalTitle, Notes = OriginalNotes };

using (RuntimeObservationFallback.WhenChanging(item, x => x.Title, x => x.Notes).Subscribe(static values => Console.WriteLine($"{values.Property1} {values.Property2}")))
{
    item.Notes = ReplacementNotes;
}
```

```text
Renew car registration Bring the insurance certificate.
```

Three paths follow the same pattern.

`WhenAnyValue` delivers the value after each change, with one, two or three paths. This first call watches one property and collects the title when you subscribe and again after the change.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
List<string> titles = [];

using (RuntimeObservationFallback.WhenAnyValue(item, x => x.Title).Subscribe(titles.Add))
{
    item.Title = RenamedTitle;
}

Console.WriteLine(string.Join(", ", titles));
```

```text
Renew car registration, Renew car registration online
```

With two paths, `WhenAnyValue` delivers a pair after each change. This call raises the priority, and the output shows the pair before and after, so a screen can redraw with both values at once.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };

using (RuntimeObservationFallback.WhenAnyValue(item, x => x.Title, x => x.Priority).Subscribe(static values => Console.WriteLine($"{values.Property1} {values.Property2}")))
{
    item.Priority = TodoPriority.High;
}
```

```text
Renew car registration Normal
Renew car registration High
```

Three paths follow the same pattern and deliver a struct of three values.

### Bind with RuntimeBindingFallback

`RuntimeBindingFallback` holds the binding methods. A method ends with a sequencer, which may be `null`, and the `bindingExpression`. A conversion, when the method takes one, comes before the sequencer.

The methods are `BindOneWay`, `BindTwoWay`, `OneWayBind`, `Bind` and `BindTo`. The [API reference](#api-reference) lists the overloads of each.

`BindOneWay` and `BindTwoWay` take the source first and the target second. `OneWayBind` and `Bind` take the view first. The `null` and the string at the end of each call are the sequencer and the expression text. This call carries a filter text from a view model to a text box. It shows the simplest binding call, and the output shows the text box holding the new filter.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new();

using (RuntimeBindingFallback.BindOneWay(viewModel, view, x => x.FilterText, v => v.FilterTextBox.Text, null, FilterExpression))
{
    viewModel.FilterText = FilterQuery;
}

Console.WriteLine(view.FilterTextBox.Text);
```

```text
car
```

`OneWayBind` starts from the view and takes a conversion `Func` when the two properties differ in type. This call turns a count into the text of a label, and the output shows the label holding the text as soon as the binding starts.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new() { ViewModel = viewModel };

using (RuntimeBindingFallback.OneWayBind(
    view,
    viewModel,
    x => x.RemainingCount,
    v => v.RemainingLabel.Text,
    static count => $"{count} left",
    null,
    RemainingCountExpression))
{
    Console.WriteLine(view.RemainingLabel.Text);
}
```

```text
3 left
```

`Bind` carries a property both ways and starts from the view. This call types text into a text box and reads the text back from the view model, so the output shows the edit reaching the view model.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new() { ViewModel = viewModel };

using (RuntimeBindingFallback.Bind(view, viewModel, x => x.FilterText, v => v.FilterTextBox.Text, null, FilterExpression))
{
    view.FilterTextBox.Text = FilterQuery;

    Console.WriteLine(viewModel.FilterText);
}
```

```text
car
```

A two-way method takes a `TwoWayConverterPair` where a one-way method takes a conversion `Func`. The example's `CreateAmountConverters()` builds a pair for an amount and its text. This call types an amount into a text box, and the output shows the view model holding the parsed `decimal`.

```csharp
TransferViewModel viewModel = new(new InMemoryBankingBackend());
TransferView view = new();

using (RuntimeBindingFallback.BindTwoWay(viewModel, view, x => x.Draft.Amount, v => v.AmountTextBox.Text, CreateAmountConverters(), null, AmountExpression))
{
    view.AmountTextBox.Text = AmountText;

    Console.WriteLine(viewModel.Draft.Amount);
}
```

```text
125.50
```

The `Bind` overloads that take an update stream end with the stream and a `TriggerUpdate` value, in place of the sequencer and the expression text. This call holds the view edit until `commit` fires. The output shows the view model empty after the edit and holding the text after the signal.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
TodoView view = new() { ViewModel = viewModel };
Signal<EventArgs> commit = new();

using (RuntimeBindingFallback.Bind(view, viewModel, x => x.FilterText, v => v.FilterTextBox.Text, commit, TriggerUpdate.ViewToViewModel))
{
    view.FilterTextBox.Text = FilterQuery;

    Console.WriteLine($"'{viewModel.FilterText}'");

    commit.OnNext(EventArgs.Empty);

    Console.WriteLine($"'{viewModel.FilterText}'");
}
```

```text
''
'car'
```

`BindTo` takes the stream first. A null target drops the values, because a view has no property to write before it exists. The three `null` arguments are the hint, the converter and the sequencer. This call passes a null target and selects an item. The output shows the selection changing and the call not failing, because a view often does not exist yet when a stream starts.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
IObservable<TodoItem?> selected = RuntimeObservationFallback.WhenChanged(viewModel, x => x.SelectedItem);
TodoView? missing = default;

using (RuntimeBindingFallback.BindTo(selected, missing, v => v.SelectedTitleTextBox.Text, null, null, null, SelectedTitleExpression))
{
    viewModel.SelectedItem = viewModel.Items[0];

    Console.WriteLine(ReferenceEquals(viewModel.Items[0], viewModel.SelectedItem));
}
```

```text
True
```

### Convert with RuntimeBindingConverter and TwoWayConverters

`RuntimeBindingConverter.TryConvert` converts a value from one type to another. It returns `false` and leaves the default in `result` when no converter can do it. When nothing is registered for the two types, a value that already is the target type, or a null the target can hold, passes through unchanged. A converter you pass wins over the registered ones. Otherwise it uses the converter registered for the two types. The hint goes to the converter, which decides what it means. This call converts a to-do item to text with a converter you name and a hint that asks for upper case. Use it to test a converter without a binding.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };

bool converted = RuntimeBindingConverter.TryConvert<TodoItem, string>(item, TodoItemTitleConverter.UpperCaseHint, new TodoItemTitleConverter(), out var text);

Console.WriteLine(converted);
Console.WriteLine(text);
```

```text
True
RENEW CAR REGISTRATION
```

Pass `null` for the hint and the converter to use the registered converter. The methods `ConvertWithRegisteredConverter` and `ConvertWithConversionHint` in the [example project](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/unsafe/FallbackRuntimeExamples.cs) show both. The next call asks for a conversion that no converter provides, from `TodoItem` to `int`. It returns `false` and the default of the result type, so check the result before you use the value.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };

bool converted = RuntimeBindingConverter.TryConvert<TodoItem, int>(item, null, null, out var number);

Console.WriteLine(converted);
Console.WriteLine(number);
```

```text
False
0
```

A value typed as `object`, the way a picker or a list holds its selection, often already is the type you want. Nothing is registered from `object` to `string`, but the value is a string, so it passes through as it is. This is what lets a two-way binding carry a list's `SelectedItem` into a `string` property.

```csharp
object status = DraftStatus;

// Nothing converts object to string, but the value is a string, so it passes through as it is.
bool converted = RuntimeBindingConverter.TryConvert<object, string>(status, null, null, out var text);

Console.WriteLine(converted);
Console.WriteLine(text);
```

```text
True
Draft
```

`TwoWayConverters.Create` pairs a forward and a reverse conversion into a `TwoWayConverterPair` and infers both types. This call builds a pair between a `decimal` and text and runs each direction. The output shows both conversions giving the same text.

```csharp
TwoWayConverterPair<decimal, string> pair = TwoWayConverters.Create<decimal, string>(
    static amount => amount.ToString("F2", CultureInfo.InvariantCulture),
    static text => decimal.Parse(text, CultureInfo.InvariantCulture));

Console.WriteLine(pair.Forward(AmountValue));
Console.WriteLine(pair.Reverse(AmountText));
```

```text
125.50
125.50
```

`TwoWayConverterPair<TSourceProp, TTargetProp>` is a record, so two pairs built from the same two functions are equal. The `with` expression replaces one function. This call compares two equal pairs, and then a pair with a changed forward function. The output shows the first comparison true, the second false and the new function rounding to a whole number.

```csharp
Func<decimal, string> forward = static amount => amount.ToString("F2", CultureInfo.InvariantCulture);
Func<string, decimal> reverse = static text => decimal.Parse(text, CultureInfo.InvariantCulture);
TwoWayConverterPair<decimal, string> pair = new(forward, reverse);
TwoWayConverterPair<decimal, string> same = new(forward, reverse);
TwoWayConverterPair<decimal, string> swapped = pair with { Forward = static amount => amount.ToString("F0", CultureInfo.InvariantCulture) };

Console.WriteLine(pair.Equals(same));
Console.WriteLine(pair.Equals(swapped));
Console.WriteLine(swapped.Forward(AmountValue));
```

```text
True
False
126
```

### Commands and interactions

`RuntimeCommandBindingFallback.BindCommand` connects a command to a control, `RuntimeCommandFallback.InvokeCommand` runs a command for each value of a stream, and `RuntimeInteractionFallback.BindInteraction` registers an interaction handler. `BindCommand` takes a stream of parameters and an optional event name. A null view model is the ordinary state before a view model is assigned, and the binding then does nothing. This call passes a null view model. The output shows the button has no command, which means the call is safe to make early.

```csharp
TodoView view = new();
TodoListViewModel? missing = default;

using (RuntimeCommandBindingFallback.BindCommand(view, missing, x => x.AddCommand, v => v.AddButton, Signal.Never<object?>(), null, AddCommandExpression))
{
    Console.WriteLine(view.AddButton.Command is null);
}
```

```text
True
```

`InvokeCommand` takes the stream, the target and the command property. As before, the excerpt leaves out the `TaskCompletionSource` wait-handler. This call runs the complete command for each item that is selected. The output shows the remaining count falling to two.

```csharp
TodoListViewModel viewModel = await LoadTodoAsync();
IObservable<TodoItem?> selected = RuntimeObservationFallback.WhenAnyValue(viewModel, x => x.SelectedItem);

using (RuntimeCommandFallback.InvokeCommand(selected, viewModel, x => x.CompleteCommand))
{
    viewModel.SelectedItem = viewModel.Items[0];
    await completed.Task;
}

Console.WriteLine(viewModel.RemainingCount);
```

```text
2
```

`BindInteraction` takes the view model, the interaction property, a callback that registers the handler on the interaction, and the expression text. This call registers a handler that confirms the transfer. The output shows the view model receiving the answer `True`, so the view model can ask the user a question without knowing about the screen.

```csharp
TransferViewModel viewModel = new(new InMemoryBankingBackend());

using (RuntimeInteractionFallback.BindInteraction(
    viewModel,
    x => x.ConfirmTransfer,
    static interaction => interaction.RegisterHandler(static context =>
    {
        context.SetOutput(true);
        return Task.CompletedTask;
    }),
    "x => x.ConfirmTransfer"))
{
    bool confirmed = await viewModel.ConfirmTransfer.Handle(viewModel.Draft);

    Console.WriteLine(confirmed);
}
```

```text
True
```

## Read and write a path

The twins walk a property path with two static classes in `ReactiveUI.Binding.Expressions`: `Reflection` and `ExpressionMixins`. You can call them to read a path yourself. A path such as `x.Draft.Amount` has two **links**, `Draft` and `Amount`, and each link is one member access or one indexer.

The extension members `GetExpressionChain`, `GetMemberInfo`, `GetParent` and `GetArgumentsArray` are on `ExpressionMixins`. The rest are on `Reflection`. The [API reference](#api-reference) lists each one. This call names the path of a lambda, and the output shows the property names joined by dots. That text is what the twins use to describe a binding in a log entry.

```csharp
Expression<Func<TransferViewModel, decimal>> path = x => x.Draft.Amount;

Console.WriteLine(Reflection.ExpressionToPropertyNames(path.Body));
```

```text
Draft.Amount
```

`GetExpressionChain` returns one expression for each link, from the root outwards. This call splits `x.Draft.Amount` into its two links and prints their names, so you can work with each link on its own.

```csharp
Expression<Func<TransferViewModel, decimal>> path = x => x.Draft.Amount;

List<Expression> chain = new List<Expression>(path.Body.GetExpressionChain());

Console.WriteLine(chain.Count);
Console.WriteLine(((MemberExpression)chain[0]).Member.Name);
Console.WriteLine(((MemberExpression)chain[1]).Member.Name);
```

```text
2
Draft
Amount
```

The compiler adds a conversion when it boxes a value. `GetMemberInfo` looks through that conversion to the member. This call reads a lambda that returns an `int` as an `object`, and the output shows the conversion and then the property name `Id`.

```csharp
Expression<Func<TodoItem, object>> path = x => x.Id;

Console.WriteLine(path.Body.NodeType);
Console.WriteLine(path.Body.GetMemberInfo()!.Name);
```

```text
Convert
Id
```

`GetParent` returns the expression a link is read from. This call asks for the parent of the last link in `x.Draft.Amount`, and the output shows it is `Draft`. Use it to walk a path backwards.

```csharp
Expression<Func<TransferViewModel, decimal>> path = x => x.Draft.Amount;

MemberExpression parent = (MemberExpression)path.Body.GetParent()!;

Console.WriteLine(parent.Member.Name);
```

```text
Draft
```

`GetArgumentsArray` returns the arguments of an indexer link, and `null` for a plain member. The arguments must be constants. The compiler writes an indexer as a method call, so `Rewrite` turns it into an indexer link first. This call reads the argument of `x.Tags[0]`, and then shows that a plain member has no arguments. The output shows an index link with one argument, `0`.

```csharp
Expression<Func<TodoItem, string>> path = x => x.Tags[0];

Expression indexer = Reflection.Rewrite(path.Body);
object?[]? arguments = indexer.GetArgumentsArray();

Console.WriteLine(indexer.NodeType);
Console.WriteLine(arguments!.Length);
Console.WriteLine((int)arguments[0]!);

Expression<Func<TodoItem, string>> member = x => x.Title;

Console.WriteLine(member.Body.GetArgumentsArray() is null);
```

```text
Index
1
0
True
```

`Rewrite` also removes a conversion, so the boxed `x => x.Id` from above becomes a plain member access.

**Read and write one member.** A fetcher reads a member and a setter writes it. Each has two forms. The `ForProperty` form returns `null` for a member that is not a property or a field. The `OrThrow` form throws `ArgumentException`. The fetcher for a field throws `InvalidOperationException` when the field holds null. The fetcher takes the object and the indexer arguments, and the setter takes the object, the value and the indexer arguments. The arguments are `null` for a property.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
PropertyInfo? title = typeof(TodoItem).GetProperty(TitleName);

Func<object?, object?[]?, object?>? fetcher = Reflection.GetValueFetcherForProperty(title);

Console.WriteLine((string)fetcher!(item, null)!);
Console.WriteLine(Reflection.GetValueFetcherForProperty(typeof(TodoItem).GetMethod(nameof(TodoItem.Clone))) is null);
```

```text
Renew car registration
True
```

The `OrThrow` fetcher fails loudly for a member it cannot read, which is better when a missing property is a mistake. The `Throws` method in the example reports whether an action throws the exception you name. This call reads the title, and then asks for a fetcher for a method, which throws.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
Func<object?, object?[]?, object?> fetcher = Reflection.GetValueFetcherOrThrow(typeof(TodoItem).GetProperty(TitleName));

Console.WriteLine((string)fetcher(item, null)!);
Console.WriteLine(Throws<ArgumentException>(static () => Reflection.GetValueFetcherOrThrow(typeof(TodoItem).GetMethod(nameof(TodoItem.Clone)))));
```

```text
Renew car registration
True
```

A setter writes the value it receives. This call builds a setter for the title and writes a new title with it. The output shows the new title, and then shows that the `ForProperty` setter returns `null` for a method.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
Action<object?, object?, object?[]?>? setter = Reflection.GetValueSetterForProperty(typeof(TodoItem).GetProperty(TitleName));

setter!(item, RenamedTitle, null);

Console.WriteLine(item.Title);
Console.WriteLine(Reflection.GetValueSetterForProperty(typeof(TodoItem).GetMethod(nameof(TodoItem.Clone))) is null);
```

```text
Renew car registration online
True
```

The `OrThrow` setter writes the title in the same way and throws for a method. Choose it when you want a mistake to stop the program instead of doing nothing.

```csharp
TodoItem item = new TodoItem { Title = OriginalTitle };
Action<object?, object?, object?[]?> setter = Reflection.GetValueSetterOrThrow(typeof(TodoItem).GetProperty(TitleName));

setter(item, RenamedTitle, null);

Console.WriteLine(item.Title);
Console.WriteLine(Throws<ArgumentException>(static () => Reflection.GetValueSetterOrThrow(typeof(TodoItem).GetMethod(nameof(TodoItem.Clone)))));
```

```text
Renew car registration online
True
```

**Read and write a whole path.** The chain methods take the links from `GetExpressionChain`. The example builds them with a small helper named `CreateChain`. `TryGetValueForPropertyChain` returns `true` and the value when it reaches the end of the path. This call reads the amount of a draft transfer, and the output shows that it found the amount.

```csharp
TransferViewModel viewModel = new(new InMemoryBankingBackend());
viewModel.Draft.Amount = AmountValue;
List<Expression> chain = CreateChain<TransferViewModel, decimal>(static x => x.Draft.Amount);

bool found = Reflection.TryGetValueForPropertyChain<decimal>(out var amount, viewModel, chain);

Console.WriteLine(found);
Console.WriteLine(amount);
```

```text
True
125.50
```

It returns `false` and a default value when a link along the path is null. This path ends at `SelectedItem!.Title`, and no item is selected. The output shows `False` and a null title, so you can tell a missing value from an empty one.

```csharp
TodoListViewModel viewModel = new TodoListViewModel(InMemoryTodoStore.CreateSeeded());
List<Expression> chain = CreateChain<TodoListViewModel, string>(static x => x.SelectedItem!.Title);

bool found = Reflection.TryGetValueForPropertyChain<string>(out var title, viewModel, chain);

Console.WriteLine(found);
Console.WriteLine(title is null);
```

```text
False
True
```

`TryGetAllValuesForPropertyChain` returns one entry for each link. Each entry holds the object the link was read from, in `Sender`, and the value. The entries from the first null link on are `null`. This call reads both links of the draft amount path, and the output checks each entry against the objects it should hold. It shows the path was read one link at a time.

```csharp
TransferViewModel viewModel = new(new InMemoryBankingBackend());
viewModel.Draft.Amount = AmountValue;
List<Expression> chain = CreateChain<TransferViewModel, decimal>(static x => x.Draft.Amount);

bool found = Reflection.TryGetAllValuesForPropertyChain(out var changes, viewModel, chain);

Console.WriteLine(found);
Console.WriteLine(changes.Length);
Console.WriteLine(ReferenceEquals(viewModel, changes[0].Sender));
Console.WriteLine(ReferenceEquals(viewModel.Draft, changes[0].Value));
Console.WriteLine(ReferenceEquals(viewModel.Draft, changes[1].Sender));
Console.WriteLine((decimal)changes[1].Value!);
```

```text
True
2
True
True
True
125.50
```

`TrySetValueToPropertyChain` writes the value at the end of a path. It returns `false` when the object that owns the last property is null. It throws `ArgumentNullException` when a link earlier in the path is null. This call writes an amount through the draft, and the output shows the write succeeded and the draft holds the amount.

```csharp
TransferViewModel viewModel = new(new InMemoryBankingBackend());
List<Expression> chain = CreateChain<TransferViewModel, decimal>(static x => x.Draft.Amount);

bool written = Reflection.TrySetValueToPropertyChain(viewModel, chain, AmountValue);

Console.WriteLine(written);
Console.WriteLine(viewModel.Draft.Amount);
```

```text
True
125.50
```

By default the method throws when a link is not a property or a field. A fourth argument of `false` skips such a link instead and returns `false` when the last member cannot be written. This call sets the title of an item with the fourth argument `false`, and the output shows the title changed.

```csharp
TodoItem item = new TodoItem { Tags = [FirstTag] };
List<Expression> chain = CreateChain<TodoItem, string>(static x => x.Title);

bool written = Reflection.TrySetValueToPropertyChain(item, chain, RenamedTitle, false);

Console.WriteLine(written);
Console.WriteLine(item.Title);
```

```text
True
Renew car registration online
```

## Where to go next

- [Observing](observing.md) covers the plain observation methods that these twins mirror.
- [Bindings](bindings.md) covers the plain binding methods, commands and interactions.
- [Mechanisms](mechanisms.md) covers the providers and binders that the fallback looks up.
- [Threading and platforms](threading.md) covers the owning thread, sequencers and the platform modules.
- [Setup](setup.md) covers the builder, trimming and the analyzer diagnostics.
- [API reference](api.md) lists every twin and fallback member.

## API reference

The first column links to the source file. A twin that repeats for each number of paths has one row, and its type column gives the most paths one call takes and the range. Most members that read a path by reflection carry `[RequiresUnreferencedCode]`.

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`WhenChangedUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenChanged.WideArity.Unsafe.cs) | Observes properties by reflection and delivers each value after it changes. | Extension on a class in `ReactiveUI.Binding`. Takes one to 16 paths, up to `PropertyValues<T1, ..., T16>`. One path returns `IObservable<T1>`. Two or more return `IObservable<PropertyValues<...>>`, or `IObservable<TReturn>` with a selector (two to 16 paths). | Delivers the current value when you subscribe. A null object or path throws `ArgumentNullException`. The selector forms are in [the selector file](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenChanged.Selectors.WideArity.Unsafe.cs). |
| [`WhenChangingUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenChanging.WideArity.Unsafe.cs) | Observes properties by reflection and delivers each value as it stood before a change. | Extension on a class. Takes one to 16 paths, up to `PropertyValues<T1, ..., T16>`. The return types match `WhenChangedUnsafe`, with a selector from two to 16 paths. | The object must raise `INotifyPropertyChanging`. A null object or path throws `ArgumentNullException`. |
| [`WhenAnyValueUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenAnyValue.WideArity.Unsafe.cs) | Observes properties by reflection and delivers each value after it changes. | Extension on a class. Takes one to 16 paths, up to `PropertyValues<T1, ..., T16>`. With a selector it returns `IObservable<TRet>`, from one to 16 paths. | Delivers the current value when you subscribe. A null sender or path throws `ArgumentNullException`. |
| [`WhenAnyUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenAny.WideArity.Unsafe.cs) | Observes properties by reflection and passes each change to a selector. | Extension on a class. Takes one to 12 paths and always a `Func` selector over `IObservedChange<TSender, T>` values. Returns `IObservable<TRet>`. | Delivers when you subscribe and after any change. A null sender or path throws `ArgumentNullException`. |
| [`WhenAnyObservableUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenAnyObservable.WideArity.Unsafe.cs) | Follows the streams that properties hold and delivers their values. | Extension on a class. Takes one to 12 paths to `IObservable<TRet>?` properties and returns `IObservable<TRet>`. Two to 12 paths and a selector combine the latest value of each stream. | One path switches to the new stream when the property changes. Several paths merge their streams. A null stream delivers nothing. |
| [`BindOneWayUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.Binding.Unsafe.cs) | Copies a source property to a target property. | Returns `IDisposable`. Takes the same type, or a conversion `Func`. | Writes on the target's owning thread when a registered invoker claims it. A null conversion throws `ArgumentNullException`. |
| [`BindTwoWayUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.Binding.Unsafe.cs) | Carries a property both ways between two objects. | Returns `IDisposable`. Takes the same type, or two conversion `Func` values. | Seeds the target from the source. Each side writes on its owning thread when a registered invoker claims it. |
| [`OneWayBindUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.Binding.Unsafe.cs) | Copies a view model property to a view property, called on the view. | Returns `IReactiveBinding<TView, TVProp>`. Takes no conversion, or a selector. | Without a selector it uses the registered converters. The view must implement `IViewFor`. |
| [`BindUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.Binding.Unsafe.cs) | Carries a property both ways, called on the view. | Returns `IReactiveBinding<TView, BindingChange>`. Takes no conversion, two conversion `Func` values, or an update stream `IObservable<TDontCare>?` with a `TriggerUpdate`. | Writes the view model value to the view first. Without conversion functions it uses the registered converters, and a value they cannot convert is written as the default. A null stream observes both properties. |
| [`BindToUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.BindTo.Unsafe.cs) | Writes each value of a stream to a target property. | Extension on `IObservable<TValue>`. Returns `IDisposable`. Takes no extras, a hint `object?`, an `IBindingTypeConverter?`, or both. | A null target writes nothing. A value the converter cannot convert is written as the default. A null source throws `ArgumentNullException`. |
| [`BindCommandUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.BindCommand.Unsafe.cs) | Connects the command a property holds to a control. | Returns `IDisposable`. Takes an event name `string?`, plus an `IObservable<TParam?>` or a parameter expression for the command parameter. | A null event name picks the control's default event. Nothing is bound while the view model, the command or the control is null, or when no registered binder supports the control. |
| [`InvokeCommandUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.InvokeCommand.Unsafe.cs) | Runs the command a property holds for each value of a stream. | Extension on `IObservable<T>`. Returns `IDisposable`. | Skips a value while there is no command or the command cannot run. A null target runs nothing. A null source or command path throws `ArgumentNullException`. |
| [`BindInteractionUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.BindInteraction.Unsafe.cs) | Registers a handler on the interaction a property holds. | Returns `IDisposable`. The handler is a `Func` returning `Task`, or returning `IObservable<TDontCare>`. | Moves the handler when the property holds another interaction. A null view model registers nothing. |
| [`ToPropertyUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.ToProperty.Unsafe.cs) | Backs a read-only property with an observable's latest value, finding the property and its raise member by reflection. | Extension on `IObservable<TRet>`. Takes the declaring object and an `Expression<Func<TObj, TRet>>` of the form `x => x.Property`, or the property's name as any string. Optional: an initial value or a `Func<TRet>` factory, `deferSubscription`, an `ISequencer?` scheduler, and an `out ObservableAsPropertyHelper<TRet>`. Returns `ObservableAsPropertyHelper<TRet>`. | Throws `InvalidOperationException` when the type raises no notification reflection can reach, and `ArgumentException` for a selector that does not read one property off its parameter. |
| [`ReactiveSchedulerExtensions`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveSchedulerExtensions.Unsafe.cs) | Holds the twins of `BindOneWay`, `BindTwoWay`, `OneWayBind` and `Bind` that take a sequencer. | Static class in `ReactiveUI.Binding`. Each twin takes an `ISequencer?` after the expressions and any conversion `Func`. A converter form takes the converters, the sequencer, then an `object?` hint. | A null sequencer writes on the target's owning thread. An immediate sequencer writes inline. A two-way twin does not schedule the write back. |
| [`TriggerUpdate`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/TriggerUpdate.cs) | Selects the direction that an update stream drives. | Enum in `ReactiveUI.Binding`. Values below. | The optional last argument of the `BindUnsafe` forms that take an update stream. |
| [`TriggerUpdate.ViewToViewModel`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/TriggerUpdate.cs) | The stream replaces view notifications and requests writes to the view model. | `0`; the default. | View model changes reach the view at once. |
| [`TriggerUpdate.ViewModelToView`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/TriggerUpdate.cs) | The stream requests writes to the view in place of view model notifications after the first. | `1` | The first view model value is written when the binding starts. View notifications are observed and reach the view model at once. |
| [`RuntimeObservationFallback.WhenChanged`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/RuntimeObservationFallback.cs) | Observes one to three property expressions and delivers each value after it changes. | Static method in `ReactiveUI.Binding.Fallback`. Takes up to three paths, up to `PropertyValues<T1, T2, T3>`. | Delivers the current value first and skips a value equal to the one before. A null link on a path delivers nothing until the path resolves. The type is hidden from IntelliSense. |
| [`RuntimeObservationFallback.WhenChanging`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/RuntimeObservationFallback.cs) | Observes one to three property expressions and delivers each value before it changes. | Takes up to three paths, up to `PropertyValues<T1, T2, T3>`. | The object must raise `INotifyPropertyChanging`. |
| [`RuntimeObservationFallback.WhenAnyValue`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/RuntimeObservationFallback.cs) | Observes one to three property expressions and delivers each value after it changes. | Takes up to three paths, up to `PropertyValues<T1, T2, T3>`. | Delivers the current value first. |
| [`RuntimeBindingFallback.BindOneWay`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/RuntimeBindingFallback.cs) | Binds a source property one way onto a target property. | Returns `IDisposable`. Takes the same type, or a conversion `Func`, then an `ISequencer?` and the expression text. | A null sequencer writes on the thread that owns the target. A null conversion throws `ArgumentNullException`. |
| [`RuntimeBindingFallback.BindTwoWay`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/RuntimeBindingFallback.cs) | Binds two properties to each other. | Returns `IDisposable`. Takes the same type, or a `TwoWayConverterPair`, then an `ISequencer?` and the expression text. | Only the write to the target uses the sequencer. A null pair throws `ArgumentNullException`. |
| [`RuntimeBindingFallback.OneWayBind`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/RuntimeBindingFallback.cs) | Binds a view model property one way onto a view property, starting from the view. | Returns `IReactiveBinding<TView, TProp>`. Takes the same type, or a conversion `Func`, then an `ISequencer?` and the expression text. | A null conversion throws `ArgumentNullException`. |
| [`RuntimeBindingFallback.Bind`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/RuntimeBindingFallback.cs) | Binds a view model property and a view property to each other, starting from the view. | Returns `IReactiveBinding<TView, BindingChange>`. Takes the same type, or a `TwoWayConverterPair`, then an `ISequencer?` and the expression text. Two more forms end with an update stream and a `TriggerUpdate` instead. | A null pair throws `ArgumentNullException`. The forms with an update stream take no sequencer and no expression text. |
| [`RuntimeBindingFallback.BindTo`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/RuntimeBindingFallback.cs) | Writes each value of a stream into a property. | Returns `IDisposable`. Takes a hint `object?`, an `IBindingTypeConverter?`, an `ISequencer?` and the expression text. | A null target drops the values. A value the converter refuses is written as the default of the property type. A null source or property throws `ArgumentNullException`. |
| [`RuntimeBindingConverter.TryConvert`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/RuntimeBindingConverter.cs) | Converts a value from one type to another. | Returns `bool`. Takes the value, a hint `object?`, an `IBindingTypeConverter?` override and an `out` result. | A converter you pass wins over the registered ones. Returns `false` and the default when no converter can convert. The converter is chosen from the declared types, not the runtime type. With nothing registered for the pair, a value that already is the target type, or a null the target can hold, passes through. |
| [`RuntimeCommandBindingFallback.BindCommand`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/RuntimeCommandBindingFallback.cs) | Keeps the command a property holds bound to a control. | Returns `IDisposable`. Takes an `IObservable<object?>` of parameters, an event name `string?` and the expression text. | Rebinds when either property changes. Nothing is bound while the view model, the command or the control is null. A null command or control path throws `ArgumentNullException`. |
| [`RuntimeCommandFallback.InvokeCommand`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/RuntimeCommandFallback.cs) | Runs the command a property holds for each value of a stream. | Returns `IDisposable`. Takes a stream, a target and a command path. | A null target drops the values. A null source or path throws `ArgumentNullException`. |
| [`RuntimeInteractionFallback.BindInteraction`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/RuntimeInteractionFallback.cs) | Keeps a handler registered on the interaction a property holds. | Returns `IDisposable`. Takes a view model, an interaction path, a `Func` that registers the handler and returns `IDisposable`, and the expression text. | Moves the handler when the property holds another interaction. A null view model registers nothing. A null path or callback throws `ArgumentNullException`. |
| [`TwoWayConverters.Create`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/TwoWayConverters.cs) | Pairs a forward and a reverse conversion. | Returns `TwoWayConverterPair<TSourceProp, TTargetProp>`. Takes two `Func` values. | Infers both type arguments. |
| [`TwoWayConverterPair<TSourceProp, TTargetProp>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/TwoWayConverterPair.cs) | Holds the two conversions a two-way binding needs. | Sealed record with the properties `Forward` and `Reverse`. | Two pairs with the same functions are equal. `with` replaces one function. |
| [`TwoWayConverterPair.Forward`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/TwoWayConverterPair.cs) | Converts a source value to the target's type. | `Func<TSourceProp, TTargetProp>` | Set it with `with`. |
| [`TwoWayConverterPair.Reverse`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/TwoWayConverterPair.cs) | Converts a target value back to the source's type. | `Func<TTargetProp, TSourceProp>` | Set it with `with`. |
| [`Reflection.ExpressionToPropertyNames`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/Reflection.cs) | Names the links of a path, joined by dots. | Static method in `ReactiveUI.Binding.Expressions`. Returns `string`. | An indexer link needs constant arguments. A null expression throws `ArgumentNullException`. A node that is not a member access or an index throws `NotSupportedException`. |
| [`Reflection.Rewrite`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/Reflection.cs) | Rewrites a path into the plain shape the other members read. | Takes and returns `Expression`. | Removes conversions and turns an indexer call into an indexer link. Returns null for a null expression. |
| [`Reflection.GetValueFetcherForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/Reflection.cs) | Builds a method that reads a property or a field. | Returns `Func<object?, object?[]?, object?>?`. | Returns `null` for a member that is neither. The fetcher for a field throws `InvalidOperationException` when the field holds null. A null member throws `ArgumentNullException`. |
| [`Reflection.GetValueFetcherOrThrow`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/Reflection.cs) | Builds a method that reads a property or a field. | Returns `Func<object?, object?[]?, object?>`. | Throws `ArgumentException` for a member that is neither. |
| [`Reflection.GetValueSetterForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/Reflection.cs) | Builds a method that writes a property or a field. | Returns `Action<object?, object?, object?[]?>?`. The arguments are the object, the value and the indexer arguments. | Returns `null` for a member that is neither. A null member throws `ArgumentNullException`. |
| [`Reflection.GetValueSetterOrThrow`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/Reflection.cs) | Builds a method that writes a property or a field. | Returns `Action<object?, object?, object?[]?>`. | Throws `ArgumentException` for a member that is neither. |
| [`Reflection.TryGetValueForPropertyChain`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/Reflection.cs) | Reads the value at the end of a path. | Returns `bool`. Takes an `out` value, the start object and the links. | Returns `false` and the default when the start object or a link is null. An empty chain throws `InvalidOperationException`. |
| [`Reflection.TryGetAllValuesForPropertyChain`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/Reflection.cs) | Reads the value of every link. | Returns `bool`. Fills an `out` array of `IObservedChange<object, object?>`, one entry per link. | Entries from the first null link on are `null`, and the method returns `false`. An empty chain throws `InvalidOperationException`. |
| [`Reflection.TrySetValueToPropertyChain`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/Reflection.cs) | Writes the value at the end of a path. | Returns `bool`. Takes the start object, the links, the value, and an optional `shouldThrow` `bool`. | Returns `false` when the object that owns the last property is null. A null link before that throws `ArgumentNullException`. With `shouldThrow` set to `false` it skips a link that is not a property or a field. |
| [`ExpressionMixins.GetExpressionChain`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/ExpressionMixins.cs) | Splits a path into its links, from the root outwards. | Extension on `Expression`. Returns `IEnumerable<Expression>`. | Empty for a parameter or a null expression. A node that is not a member access or an index throws `NotSupportedException`. |
| [`ExpressionMixins.GetMemberInfo`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/ExpressionMixins.cs) | Finds the member a link names. | Returns `MemberInfo?`. | Looks through a conversion. A null expression throws `ArgumentNullException`, and a node that is not an index, a member access or a conversion throws `NotSupportedException`. |
| [`ExpressionMixins.GetParent`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/ExpressionMixins.cs) | Finds the expression a link is read from. | Returns `Expression?`. | Returns `null` for a static member. Anything but an index or a member access throws `NotSupportedException`. |
| [`ExpressionMixins.GetArgumentsArray`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Expression/ExpressionMixins.cs) | Reads the arguments of an indexer link. | Returns `object?[]?`. | Returns `null` for a link that is not an indexer. A non-constant argument throws `InvalidCastException`. |
