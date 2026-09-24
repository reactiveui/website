---
Order: 4
---
# Bindings

[Run the complete page example](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/bindings/bindings.csproj).

A screen shows data that lives in a view model. A label shows a count, a text box shows a filter and a button starts an action. Keeping each control in step with its property by hand takes an event handler for every pair, and you must remove each handler again.

A **binding** is one call that connects two things and keeps them connected. The **view model** holds the data and the actions, and the **view** shows them. The **source** of a binding is the object it reads, and the **target** is the object it writes. A **one-way** binding writes from source to target. A **two-way** binding also writes an edit in the target back to the source. The page ends with how a binding fails and how to refuse one.

| You want to | Call | It returns |
| --- | --- | --- |
| Copy a property one way | `BindOneWay` (source first), `OneWayBind` (view first) | `IDisposable`, or an `IReactiveBinding` |
| Copy a property both ways | `BindTwoWay` (source first), `Bind` (view first) | `IDisposable`, or an `IReactiveBinding` |
| Write a stream into a property | `BindTo` | `IDisposable` |
| Attach a command to a control | `BindCommand` | `IDisposable` |
| Run a command for each value of a stream | `InvokeCommand` | `IDisposable` |
| Answer a question from the view model | `BindInteraction` | `IDisposable` |

Every call takes its property paths as lambdas written inline, so the generator can read them at build time ([Your first binding](index.md#your-first-binding) shows the idea). Each call has an `Unsafe` twin for a lambda the generator cannot read, such as `BindOneWayUnsafe` and `BindCommandUnsafe`. [Unsafe twins and the runtime fallback](unsafe.md) lists them all. The examples use .NET MAUI controls built in code, and in-memory data.

## Bind one property to another

`BindOneWay` copies a property of a source object to a property of a target object. The value arrives when you create the binding and again after each change. The examples use an issue board: `IssueBoardViewModel` lists issues and holds the selected one, and `IssueBoardView` holds the controls.

**1. Name the two properties.** The first lambda names the source property and the second names the target property. A lambda can be a **property path**, a chain of properties ([Observing](observing.md#follow-a-path-of-properties) covers paths). This path passes through `SelectedIssue`, which is `null` until the user picks an issue. The binding follows each link. While a link is `null` the label's `Text` stays `null`, and when `SelectedIssue` changes the binding moves to the new issue. `OpenIssueBoardAsync` loads the view model from the in-memory GitHub server.

```csharp
var board = await OpenIssueBoardAsync();
IssueBoardView view = new();

using (board.BindOneWay(view, x => x.SelectedIssue!.Title, v => v.IssueTitleLabel.Text))
{
    // The path passes through SelectedIssue, which is null until the user picks an issue.
    Console.WriteLine(view.IssueTitleLabel.Text ?? NothingSelectedText);

    board.SelectedIssue = board.Issues[CheckoutIssueIndex];
    Console.WriteLine(view.IssueTitleLabel.Text);

    board.SelectedIssue = board.Issues[0];
    Console.WriteLine(view.IssueTitleLabel.Text);
}
```

```text
nothing selected
Checkout button unresponsive on Safari
Add gift-card support
```

**2. Convert when the types differ.** The plain overload needs the same type on both sides. When the types differ, pass a conversion function after the two lambdas. To show an enum in a `string` label, the function turns each state into text. Mark a function `static` when it uses nothing from the method around it.

```csharp
var board = await OpenIssueBoardAsync();
IssueBoardView view = new();

using (board.BindOneWay(view, x => x.SelectedIssue!.State, v => v.StateLabel.Text, static state => state == IssueState.Open ? OpenText : ClosedText))
{
    board.SelectedIssue = board.Issues[CheckoutIssueIndex];
    Console.WriteLine(view.StateLabel.Text);

    board.SelectedIssue.State = IssueState.Closed;
    Console.WriteLine(view.StateLabel.Text);
}
```

```text
Open
Closed
```

**3. Stop the binding.** `BindOneWay` returns an `IDisposable`. Dispose it when the view goes away. After that the label keeps the last text it was given. Dispose every binding you create: a live binding holds both objects in memory.

```csharp
var list = await OpenTodoListAsync();
TodoView view = new();

using (list.BindOneWay(view, x => x.RemainingCount, v => v.RemainingLabel.Text, static count => count.ToString(CultureInfo.InvariantCulture)))
{
    Console.WriteLine(view.RemainingLabel.Text);

    list.SelectedItem = list.Items[0];
    await list.CompleteAsync();
    Console.WriteLine(view.RemainingLabel.Text);
}

// Disposing the binding disconnects it: the label keeps the last text it was given.
list.SelectedItem = list.Items[CheckoutIssueIndex];
await list.CompleteAsync();
Console.WriteLine(view.RemainingLabel.Text);
```

```text
3
2
2
```

## Bind from the view

`OneWayBind` does the same work from the view's side. Call it on a view that implements `IViewFor` ([Views](views.md)) and pass the view model. It reads the view model and writes the view, and it returns an `IReactiveBinding` instead of an `IDisposable`. That object is a disposable that also reports what the binding writes, as [Read what a binding reports](#read-what-a-binding-reports) shows. A view-first call reads its view model from the view's `ViewModel` property, so set that property first.

```csharp
var list = await OpenTodoListAsync();
TodoView view = new() { ViewModel = list };

using (view.OneWayBind(list, x => x.FilterText, v => v.FilterTextBox.Text))
{
    list.FilterText = CarFilter;
    Console.WriteLine(view.FilterTextBox.Text);
}
```

```text
car
```

Each call takes the same kinds of extra arguments, and each kind adds an overload.

| Call | Reads | Writes | Extra arguments its overloads accept |
| --- | --- | --- | --- |
| `BindOneWay` | source | target | a conversion function, or a converter object with a hint; a sequencer |
| `OneWayBind` | view model | view | a selector function, or a converter object with a hint; a sequencer |
| `BindTwoWay` | source | target | one conversion function per direction, or two converter objects with a hint; a sequencer |
| `Bind` | view model | view | the same as `BindTwoWay` |

## Convert with a converter object

A **converter object** implements `IBindingTypeConverter`. It converts one pair of types and can act on a **hint**, an extra value whose meaning the converter defines. Pass the object after the two lambdas, then the hint. A converter you name outranks every converter the library registers ([Converters](converters.md) lists them and [Custom converters](custom-converters.md) shows how to write one). `CurrencyTextConverter` in the example turns a `decimal` into text with a dollar sign. Its hint is the number format, and it shows two decimal places when you pass none.

```csharp
var accounts = await OpenAccountsAsync();
AccountsView view = new();

using (accounts.BindOneWay(view, x => x.TotalBalance, v => v.TotalBalanceLabel.Text, new CurrencyTextConverter(), GroupedFormat))
{
    Console.WriteLine(view.TotalBalanceLabel.Text);
}
```

```text
$17,681
```

`OneWayBind`, `BindTwoWay` and `Bind` take a converter object in the same way. A two-way call takes two, one for each direction, and the hint reaches both. The view-first `OneWayBind` below colours a badge from a priority. The hint names the dark theme.

```csharp
TodoItem item = new() { Title = ItemTitle, Priority = TodoPriority.High };
TodoItemBadgeView view = new() { ViewModel = item };

using (view.OneWayBind(item, x => x.Priority, v => v.PriorityBadge.Color, new PriorityColourConverter(), DarkThemeHint))
{
    Console.WriteLine(view.PriorityBadge.Color.ToArgbHex());

    item.Priority = TodoPriority.Normal;
    Console.WriteLine(view.PriorityBadge.Color.ToArgbHex());
}
```

```text
#EF5350
#BDBDBD
```

A call that names a converter object is matched to its generated code by the file path and line number of the call. The generated match compares the path with forward slashes, and a Windows compiler writes backslashes. On Windows such a call reaches the library's own method and throws "No generated binding found", unless the build maps the source root with `PathMap`, for example `-p:PathMap=C:\src\=/_/`.

## Choose when writes run

A binding writes to its target on the thread that owns the target ([Threading and platforms](threading.md) explains how it finds that thread). To pick another place, pass a **sequencer** as the last argument. A sequencer decides when and on which thread queued work runs. `VirtualClock` in the example is a sequencer that runs its work only when the example calls `AdvanceBy`, so the example can show the write waiting. [Choose a sequencer for one binding](threading.md#deliver-a-binding-on-a-sequencer) shows how to choose one for your app.

```csharp
var board = await OpenIssueBoardAsync();
IssueBoardView view = new();
VirtualClock uiThread = new();

using (board.BindOneWay(view, x => x.SelectedIssue!.Title, v => v.IssueTitleLabel.Text, uiThread))
{
    board.SelectedIssue = board.Issues[CheckoutIssueIndex];

    Console.WriteLine($"Waiting: '{view.IssueTitleLabel.Text}'");

    uiThread.AdvanceBy(NextTurn);

    Console.WriteLine($"Written: '{view.IssueTitleLabel.Text}'");
}
```

```text
Waiting: ''
Written: 'Checkout button unresponsive on Safari'
```

`BindOneWay`, `OneWayBind`, `BindTwoWay` and `Bind` all accept a sequencer, and it comes last. A `null` sequencer means the same as naming none: the write happens on the thread that owns the target, and a target with no owning thread is written at once. Name the argument, as in `scheduler: null` below, because a bare `null` also fits the string parameters of the overload that takes no sequencer.

```csharp
var board = await OpenIssueBoardAsync();
IssueBoardView view = new();

using (board.BindOneWay(view, x => x.SelectedIssue!.Title, v => v.IssueTitleLabel.Text, scheduler: null))
{
    board.SelectedIssue = board.Issues[CheckoutIssueIndex];
    Console.WriteLine(view.IssueTitleLabel.Text);

    board.SelectedIssue = board.Issues[0];
    Console.WriteLine(view.IssueTitleLabel.Text);
}
```

```text
Checkout button unresponsive on Safari
Add gift-card support
```

## Bind both ways

`BindTwoWay` watches both properties and copies whichever one changes to the other. It also writes the source value to the target when you create the binding. Disposing the binding disconnects both directions.

```csharp
var list = await OpenTodoListAsync();
TodoView view = new();

using (list.BindTwoWay(view, x => x.FilterText, v => v.FilterTextBox.Text))
{
    list.FilterText = CarFilter;
    Console.WriteLine(view.FilterTextBox.Text);

    view.FilterTextBox.Text = DentistFilter;
    Console.WriteLine(list.FilterText);
    Console.WriteLine(list.Items.Count);
}

// Disposing the binding disconnects both directions.
view.FilterTextBox.Text = string.Empty;
Console.WriteLine(list.FilterText);

list.FilterText = CarFilter;
Console.WriteLine($"'{view.FilterTextBox.Text}'");
```

```text
car
dentist
1
dentist
''
```

When the two types differ, pass one conversion function for each direction. `FormatAmount` writes a `decimal` as text and `ParseAmount` reads text back. The function for the return trip decides what an unusable value means: in the example, text that is not a number becomes `0`.

```csharp
TransferDraft draft = new() { Amount = RentAmount };
TransferView view = new();

using (draft.BindTwoWay(view, x => x.Amount, v => v.AmountTextBox.Text, FormatAmount, ParseAmount))
{
    Console.WriteLine(view.AmountTextBox.Text);

    view.AmountTextBox.Text = TypedAmountText;
    Console.WriteLine(draft.Amount);

    // The reverse converter decides what text that is not a number means.
    view.AmountTextBox.Text = NotAnAmountText;
    Console.WriteLine(draft.Amount);
}
```

```text
1200.00
85.50
0
```

Converter objects come in pairs, one for each direction. The example uses the library's decimal converters and passes the number format as the hint.

```csharp
TransferDraft draft = new() { Amount = RentAmount };
TransferView view = new();
DecimalToStringTypeConverter toText = new();
StringToDecimalTypeConverter toAmount = new();

using (draft.BindTwoWay(view, x => x.Amount, v => v.AmountTextBox.Text, toText, toAmount, TwoDecimalPlaces))
{
    Console.WriteLine(view.AmountTextBox.Text);

    view.AmountTextBox.Text = TypedAmountText;
    Console.WriteLine(draft.Amount);
}
```

```text
1200.00
85.50
```

A list holds its selection as an `object`, so a two-way binding to a selection converts in each direction. A one-way binding shows the selected title in a detail box.

```csharp
var viewModel = await OpenTodoListAsync();
TodoView view = new() { ViewModel = viewModel };
view.ItemsList.ItemsSource = viewModel.Items;

// The list holds its selection as an object, so each direction converts.
using (viewModel.BindTwoWay(view, x => x.SelectedItem, v => v.ItemsList.SelectedItem, static item => item, static selected => (selected as TodoItem)!))
using (viewModel.BindOneWay(view, x => x.SelectedItem!.Title, v => v.SelectedTitleTextBox.Text))
{
    Console.WriteLine(view.SelectedTitleTextBox.Text ?? NothingSelectedText);

    // The user clicks the second row.
    var dentist = viewModel.Items[DentistIndex];
    view.ItemsList.SelectedItem = dentist;

    Console.WriteLine(ReferenceEquals(viewModel.SelectedItem, dentist));
    Console.WriteLine(view.SelectedTitleTextBox.Text);

    // Code selects the first item, and the list follows.
    var renew = viewModel.Items[0];
    viewModel.SelectedItem = renew;

    Console.WriteLine(ReferenceEquals(view.ItemsList.SelectedItem, renew));
    Console.WriteLine(view.SelectedTitleTextBox.Text);

    // The box follows a change to the selected item itself.
    renew.Title = RenamedTitle;

    Console.WriteLine(view.SelectedTitleTextBox.Text);

    // Clearing the selection in the list clears the detail.
    view.ItemsList.SelectedItem = null;

    Console.WriteLine(view.SelectedTitleTextBox.Text ?? NothingSelectedText);
}
```

```text
nothing selected
True
Book dentist appointment
True
Renew car registration
Book dentist for a check-up
nothing selected
```

[`ListSelectionBindingExamples`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/bindings/ListSelectionBindingExamples.cs) also binds a list of items, a busy indicator and a command that follows the selection.

With a sequencer, both directions go through it. Only the newest value waits: an edit that a newer edit replaces before the sequencer runs is never written. Two edits made before the sequencer runs write once, with the second ([Only the latest value waits](threading.md#only-the-newest-value-waits) gives the reason).

```csharp
TransferDraft draft = new() { Reference = RentMarchReference };
TransferView view = new();
VirtualClock uiThread = new();

using (draft.BindTwoWay(view, x => x.Reference, v => v.ReferenceTextBox.Text, uiThread))
{
    // The view is empty and the view model holds "Rent March": the view shows the view model once the sequencer runs.
    Console.WriteLine($"Waiting: '{view.ReferenceTextBox.Text}'");

    uiThread.AdvanceBy(NextTurn);
    Console.WriteLine($"Written: '{view.ReferenceTextBox.Text}'");

    draft.Reference = RentAprilReference;
    draft.Reference = RentMayReference;
    uiThread.AdvanceBy(NextTurn);

    Console.WriteLine(view.ReferenceTextBox.Text);
    Console.WriteLine(draft.Reference);
}
```

```text
Waiting: ''
Written: 'Rent March'
Rent May
Rent May
```

`Bind` is the view-first form of `BindTwoWay`. It takes the same arguments in the same order and returns an `IReactiveBinding`. It writes back to the view model only while the view's `ViewModel` property is set. The example keeps the amount of a transfer draft equal to the amount box.

```csharp
TransferViewModel viewModel = new(new InMemoryBankingBackend());
TransferView view = new() { ViewModel = viewModel };
viewModel.Draft.Amount = RentAmount;

using (view.Bind(viewModel, x => x.Draft.Amount, v => v.AmountTextBox.Text, FormatAmount, ParseAmount))
{
    Console.WriteLine(view.AmountTextBox.Text);

    view.AmountTextBox.Text = TypedAmountText;
    Console.WriteLine(viewModel.Draft.Amount);
}
```

```text
1200.00
85.50
```

## Read what a binding reports

A view-first call returns an `IReactiveBinding<TView, TValue>`. It disposes like any binding and it also carries these members.

| Member | What it gives you |
| --- | --- |
| `View` | The view the binding writes. |
| `Direction` | A `BindingDirection`: `OneWay`, `TwoWay` or `AsyncOneWay`. |
| `Changed` | A stream of every value the binding writes. It starts with the current value. |
| `ViewModelExpression`, `ViewExpression` | The two property expressions, or `null`. A generated binding reads its paths at build time, so it carries none. |

`OneWayBind` reports each value it writes, such as a `double` for a progress bar, and its `Direction` is `OneWay`. `Changed` replays the bar's starting value, so the empty bar is the first value a subscriber sees.

```csharp
StorageBrowserViewModel browser = new(InMemoryObjectStorage.CreateSeeded());
await browser.LoadBucketsAsync();
browser.SelectedBucket = browser.Buckets[0];
StorageBrowserView view = new() { ViewModel = browser };
List<double> written = [];

using (var binding = view.OneWayBind(browser, x => x.UploadPercent, v => v.UploadProgressBar.Progress, static percent => percent / PercentPerBar))
{
    Console.WriteLine(binding.Direction);
    Console.WriteLine(ReferenceEquals(view, binding.View));

    using (binding.Changed.Subscribe(written.Add))
    {
        await browser.UploadAsync(new("launch-video.mp4", UploadSizeBytes, "video/mp4"));
    }
}

// Changed replays the bar's starting value, so the empty bar is the first value a subscriber sees.
Console.WriteLine(string.Join(", ", written));
```

```text
OneWay
True
0, 0.25, 0.5, 0.75, 1
```

`Bind` reports a `BindingChange` for each value. `BindingChange` is a record struct with two parts: the `Value` and `FromViewModel`, which tells an edit in the view apart from the echo of the binding's own write. The first change below came from the view model, and the second came from the box.

```csharp
var list = await OpenTodoListAsync();
TodoView view = new() { ViewModel = list };
List<BindingChange> changes = [];

using (var binding = view.Bind(list, x => x.FilterText, v => v.FilterTextBox.Text))
using (binding.Changed.Subscribe(changes.Add))
{
    Console.WriteLine(binding.Direction);
    Console.WriteLine(ReferenceEquals(view, binding.View));

    list.FilterText = CarFilter;
    view.FilterTextBox.Text = DentistFilter;
}

foreach (var change in changes)
{
    Console.WriteLine($"{change.Value}, from the view model: {change.FromViewModel}");
}
```

```text
TwoWay
True
car, from the view model: True
dentist, from the view model: False
```

You can build a `ReactiveBinding<TView, TValue>` yourself from a view, a stream of changes, a direction and the subscription that does the work. Disposing it disposes the subscription once, however often you call `Dispose`. The example builds one with `BindingDirection.AsyncOneWay`.

```csharp
var list = await OpenTodoListAsync();
TodoView view = new() { ViewModel = list };
Signal<int> remainingChanges = new();
var subscription = Scope.Create(static () => Console.WriteLine("subscription disposed"));

ReactiveBinding<TodoView, int> binding = new(view, remainingChanges, BindingDirection.AsyncOneWay, subscription);
List<int> seen = [];
using var reader = binding.Changed.Subscribe(seen.Add);

remainingChanges.OnNext(list.RemainingCount);

Console.WriteLine(binding.Direction);
Console.WriteLine(ReferenceEquals(view, binding.View));
Console.WriteLine(binding.ViewExpression is null);
Console.WriteLine(binding.ViewModelExpression is null);
Console.WriteLine(string.Join(", ", seen));

binding.Dispose();
binding.Dispose();
```

```text
AsyncOneWay
True
True
True
3
subscription disposed
```

## Choose when the view model updates

A two-way binding writes each edit back at once. Sometimes the edit should wait, for example until the user leaves the box. `TriggerUpdate` names the direction that waits for a stream you supply. `ViewToViewModel` is the default: the typed value reaches the view model when the stream signals, and view model changes still reach the view at once. With `ViewModelToView` the first value reaches the view at once, and later view model changes wait for the stream.

These triggers exist on the `BindUnsafe` overloads, which read their paths while the program runs. They are not safe for trimming, and they look up services, so run the builder once when the app starts. [Setup](setup.md) covers the builder, and [Unsafe twins and the runtime fallback](unsafe.md#hold-a-write-until-a-signal-fires) covers the triggers.

```csharp
IReactiveUIBindingBuilder builder = RxBindingBuilder.CreateReactiveUIBindingBuilder();

_ = builder.WithCoreServices().BuildApp();
```

The example commits the reference when the box loses focus. `boxLeft` is a `Signal<RxVoid>`, a stream that you feed with `OnNext`. `RxVoid` carries no data.

```csharp
TransferViewModel viewModel = new(new InMemoryBankingBackend());
TransferView view = new() { ViewModel = viewModel };
Signal<RxVoid> boxLeft = new();
viewModel.Draft.Reference = MarchReference;

using (view.BindUnsafe(viewModel, x => x.Draft.Reference, v => v.ReferenceTextBox.Text, boxLeft))
{
    Console.WriteLine(view.ReferenceTextBox.Text);

    view.ReferenceTextBox.Text = AprilReference;
    Console.WriteLine(viewModel.Draft.Reference);

    boxLeft.OnNext(RxVoid.Default);
    Console.WriteLine(viewModel.Draft.Reference);

    viewModel.Draft.Reference = MayReference;
    Console.WriteLine(view.ReferenceTextBox.Text);
}
```

```text
Rent March
Rent March
Rent April
Rent May
```

Pass `TriggerUpdate.ViewModelToView` after the stream to hold back view model changes instead. Both triggers also come with a conversion function in each direction. Here the score reaches the box when the stream signals, and the teacher's typing reaches the view model at once.

```csharp
GradebookViewModel viewModel = new(InMemoryStudentRecords.CreateSeeded()) { ScoreToRecord = FirstScore };
GradebookView view = new() { ViewModel = viewModel };
Signal<RxVoid> refresh = new();

using (view.BindUnsafe(viewModel, x => x.ScoreToRecord, v => v.ScoreEntry.Text, FormatScore, ParseScore, refresh, TriggerUpdate.ViewModelToView))
{
    Console.WriteLine(view.ScoreEntry.Text);

    viewModel.ScoreToRecord = RemarkedScore;
    Console.WriteLine(view.ScoreEntry.Text);

    refresh.OnNext(RxVoid.Default);
    Console.WriteLine(view.ScoreEntry.Text);

    view.ScoreEntry.Text = TypedScoreText;
    Console.WriteLine(viewModel.ScoreToRecord);
}
```

```text
64.0
64.0
91.0
82.5
```

You can leave the converters out. Then the registered converter for the pair of types runs, and a pair of `string` properties passes the text through. The box stays empty until the stream signals, then shows the view model's value, and typing in the box still reaches the view model at once.

```csharp
var list = await OpenTodoListAsync();
TodoView view = new() { ViewModel = list };
Signal<RxVoid> refresh = new();

using (view.BindUnsafe(list, x => x.FilterText, v => v.FilterTextBox.Text, refresh, TriggerUpdate.ViewModelToView))
{
    list.FilterText = CarFilter;
    Console.WriteLine($"'{view.FilterTextBox.Text}'");

    refresh.OnNext(RxVoid.Default);
    Console.WriteLine(view.FilterTextBox.Text);

    view.FilterTextBox.Text = DentistFilter;
    Console.WriteLine(list.FilterText);
}
```

```text
''
car
dentist
```

Pass a `null` stream, cast to `IObservable<RxVoid>?`, and the binding observes both properties and writes in both directions as they change. The cast picks the overload.

```csharp
var list = await OpenTodoListAsync();
TodoView view = new() { ViewModel = list };

using (view.BindUnsafe(list, x => x.FilterText, v => v.FilterTextBox.Text, (IObservable<RxVoid>?)null))
{
    list.FilterText = CarFilter;
    Console.WriteLine(view.FilterTextBox.Text);

    view.FilterTextBox.Text = DentistFilter;
    Console.WriteLine(list.FilterText);
}
```

```text
car
dentist
```

The same works with converters, and with a stream that commits an amount when the box is left. [`ReactiveBindingExamples`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/bindings/ReactiveBindingExamples.cs) shows both.

## Write a stream into a property

`BindTo` is an extension of `IObservable<T>`. It writes each value of a stream to a property. Use it when the value comes from a stream that you built with an operator. The example turns the upload percentage into the fraction that a progress bar shows.

`BindTo` hands each write to the thread that owns the target, so the stream needs no `ObserveOn`. When the value type differs from the property type, the converter registered for that pair runs ([Converters](converters.md)). When no converter is registered for the pair, such as an enum stream and a `string` property, `BindTo` writes nothing and reports nothing. Convert the value in the stream first, as `Select(state => state.ToString())` does.

```csharp
var browser = await OpenBucketAsync();
StorageBrowserView view = new();

using (browser.WhenChanged(x => x.UploadPercent).Select(static percent => percent / PercentPerBar).BindTo(view, v => v.UploadProgressBar.Progress))
{
    Console.WriteLine(view.UploadProgressBar.Progress);

    await browser.UploadAsync(new("launch-video.mp4", UploadSizeBytes, "video/mp4"));
    Console.WriteLine(view.UploadProgressBar.Progress);
}
```

```text
0
1
```

`BindTo` has four overloads: the plain call, one with a converter to use instead of the registered ones, one with a conversion hint, and one with both. A hint that is a `string` would bind to the caller's expression parameter, so name the argument. The hint here is a number format.

```csharp
var accounts = await OpenAccountsAsync();
AccountsView view = new();

using (accounts.WhenChanged(x => x.TotalBalance).BindTo(view, v => v.TotalBalanceLabel.Text, conversionHint: GroupedFormat))
{
    Console.WriteLine(view.TotalBalanceLabel.Text);
}
```

```text
17,681
```

Sometimes the registered converters do not give the text you want. Name a converter to use it instead of every converter the library registers. Here `CurrencyTextConverter` adds a dollar sign to the balance.

```csharp
var accounts = await OpenAccountsAsync();
AccountsView view = new();

using (accounts.WhenChanged(x => x.TotalBalance).BindTo(view, v => v.TotalBalanceLabel.Text, new CurrencyTextConverter()))
{
    Console.WriteLine(view.TotalBalanceLabel.Text);
}
```

```text
$17,680.75
```

Pass the hint and the converter together when the converter needs a hint. The hint comes first. The converter in the example reads the hint as a number format, so the total loses its decimal places.

```csharp
var accounts = await OpenAccountsAsync();
AccountsView view = new();

using (accounts.WhenChanged(x => x.TotalBalance).BindTo(view, v => v.TotalBalanceLabel.Text, GroupedFormat, new CurrencyTextConverter()))
{
    Console.WriteLine(view.TotalBalanceLabel.Text);
}
```

```text
$17,681
```

A stream can come from an event. The example turns each `TextChanged` event of the filter box into the new text and writes it to the view model, so each text the user types narrows the list.

```csharp
var list = await OpenTodoListAsync();
TodoView view = new();
var typedTexts = Signal.FromEventPattern<TextChangedEventArgs>(handler => view.FilterTextBox.TextChanged += handler, handler => view.FilterTextBox.TextChanged -= handler)
    .Select(static pattern => pattern.EventArgs.NewTextValue);

using (typedTexts.BindTo(list, x => x.FilterText))
{
    Console.WriteLine(list.Items.Count);

    view.FilterTextBox.Text = DentistFilter;

    Console.WriteLine(list.FilterText);
    Console.WriteLine(list.Items.Count);
}
```

```text
4
dentist
1
```

A value can arrive on a background thread, such as a socket callback. `BindTo` hands the write to the label's owning thread, so the stream needs no `ObserveOn`. A console process has no UI thread, so here the write runs where the value arrived.

```csharp
var storage = InMemoryObjectStorage.CreateSeeded();
StorageBrowserViewModel browser = new(storage);
StorageBrowserView view = new();
var callerThread = Environment.CurrentManagedThreadId;

using (browser.WhenChanged(x => x.ConnectionStatus)
    .Do(state => Console.WriteLine($"{state} arrived on another thread: {Environment.CurrentManagedThreadId != callerThread}"))
    .Select(static state => state.ToString())
    .BindTo(view, v => v.ConnectionLabel.Text))
{
    // The connection drops on a thread of its own, as a socket callback does.
    Thread socketThread = new(storage.Disconnect);
    socketThread.Start();
    socketThread.Join();

    Console.WriteLine(view.ConnectionLabel.Text);
}
```

```text
Connected arrived on another thread: False
Disconnected arrived on another thread: True
Disconnected
```

## Bind a command to a control

A **command** is an `ICommand`: an action with a `CanExecute` check. A view model exposes commands, and `BindCommand` attaches one to a control such as a button. It takes the view, the view model, a lambda for the command property and a lambda for the control. It rebinds when either the command or the control changes, and it binds nothing when the view model is `null`.

For a MAUI `Button` the plain call attaches the command to the button, so the button follows `CanExecute`. The example add button is disabled until the new item has a title.

```csharp
TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());
await viewModel.LoadAsync();
TodoView view = new() { ViewModel = viewModel };

using var binding = view.BindCommand(viewModel, x => x.AddCommand, v => v.AddButton);

Console.WriteLine(view.AddButton.IsEnabled);

viewModel.NewTitle = NewTodoTitle;

Console.WriteLine(view.AddButton.IsEnabled);
```

```text
False
True
```

Pass `toEvent` to bind through an event you name. The binding runs the command on each raise of the event and leaves the button's enabled state alone. Use it when the click, and not the enabled state, should decide when the command runs. The complete button below is bound through `Clicked`. Nothing is selected at first, so the command refuses the first click. The button stays enabled after the command runs.

```csharp
TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());
await viewModel.LoadAsync();
TodoView view = new() { ViewModel = viewModel };

using var binding = view.BindCommand(viewModel, x => x.CompleteCommand, v => v.CompleteButton, toEvent: ClickedEventName);

// No item is selected, so the command refuses the click.
((IButtonController)view.CompleteButton).SendClicked();

Console.WriteLine(viewModel.RemainingCount);

var item = viewModel.Items[0];
viewModel.SelectedItem = item;
var completed = item.WhenChanged(x => x.IsDone).Where(static done => done).FirstAsync();
((IButtonController)view.CompleteButton).SendClicked();
await completed;

Console.WriteLine(viewModel.RemainingCount);
Console.WriteLine(view.CompleteButton.IsEnabled);
```

```text
3
2
True
```

A command can take a **parameter**, the value that `Execute` and `CanExecute` receive. `BindCommand` never picks a parameter for you. A plain call passes none, so an event-driven control runs the command with `null`. A control that has its own `CommandParameter` property keeps the value it already holds, because the plain call sets only its `Command`. To supply a parameter, use one of two more overloads. Each also accepts `toEvent`.

- **A stream.** The parameter is the latest value the stream produced. It is `null` until the first value arrives.
- **A lambda on the view model.** The parameter is that property, and the binding follows every value the property takes.

A control with settable `Command` and `CommandParameter` properties, such as a MAUI `Button`, receives the parameter in its `CommandParameter` property. The binding writes the parameter before the command. The other kinds of control keep the latest parameter and pass it to `CanExecute` and `Execute` when the event fires. The enrol button below passes the student selected in a candidate list.

```csharp
GradebookViewModel viewModel = new(InMemoryStudentRecords.CreateSeeded());
await viewModel.LoadCoursesAsync();
viewModel.SelectedCourse = viewModel.Courses[MathCourseIndex];
await viewModel.OpenCourseAsync();
GradebookView view = new() { ViewModel = viewModel };
view.CandidateList.ItemsSource = viewModel.Candidates.ToList();

using var binding = view.BindCommand(viewModel, x => x.EnrolCommand, v => v.EnrolButton, view.CandidateList.WhenChanged(x => x.SelectedItem));

Console.WriteLine(view.EnrolButton.IsEnabled);

view.CandidateList.SelectedItem = viewModel.Candidates[DiegoCandidateIndex];

Console.WriteLine(view.EnrolButton.IsEnabled);

var rosterLoaded = viewModel.WhenChanged(x => x.Roster).Skip(1).FirstAsync();
((IButtonController)view.EnrolButton).SendClicked();
await rosterLoaded;

Console.WriteLine(viewModel.Roster.Count);
```

```text
False
True
3
```

The stream form takes `toEvent` too. Here each file the user picks is a value of a `Signal`, and the button is bound through `Clicked`.

```csharp
var browser = await OpenPhotosFolderAsync();
StorageBrowserView view = new() { ViewModel = browser };
Signal<UploadRequest> pickedFiles = new();

using var binding = view.BindCommand(browser, x => x.UploadCommand, v => v.UploadButton, pickedFiles, toEvent: ClickedEventName);

pickedFiles.OnNext(SpringCampaign());
var uploaded = browser.WhenChanged(x => x.IsUploading).Skip(1).Where(static uploading => !uploading).FirstAsync();
((IButtonController)view.UploadButton).SendClicked();
await uploaded;

Console.WriteLine(browser.Objects.Count);
```

```text
3
```

The other overload takes a lambda on the view model, and the parameter is that property. The upload button stays disabled until a file is picked, and the pick reaches the command as its parameter.

```csharp
var browser = await OpenPhotosFolderAsync();
UploadPanelViewModel viewModel = new(browser);
UploadPanelView view = new() { ViewModel = viewModel };

using var binding = view.BindCommand(viewModel, x => x.UploadCommand, v => v.UploadButton, x => x.PendingUpload);

Console.WriteLine(view.UploadButton.IsEnabled);

viewModel.PendingUpload = SpringCampaign();

Console.WriteLine(view.UploadButton.IsEnabled);

var uploaded = browser.WhenChanged(x => x.IsUploading).Skip(1).Where(static uploading => !uploading).FirstAsync();
((IButtonController)view.UploadButton).SendClicked();
await uploaded;

Console.WriteLine(browser.Objects.Count);
Console.WriteLine(browser.Objects[SpringCampaignPosition].Key);
```

```text
False
True
3
photos/2026/spring-campaign.png
```

The lambda form takes `toEvent` in the same way. Use it when the click, and not the button's enabled state, should decide when the command runs. The view model's `PendingUpload` property supplies the parameter.

```csharp
var browser = await OpenPhotosFolderAsync();
UploadPanelViewModel viewModel = new(browser);
UploadPanelView view = new() { ViewModel = viewModel };

using var binding = view.BindCommand(viewModel, panel => panel.UploadCommand, v => v.UploadButton, panel => panel.PendingUpload, toEvent: ClickedEventName);

viewModel.PendingUpload = SpringCampaign();
var uploaded = browser.WhenChanged(x => x.IsUploading).Skip(1).Where(static uploading => !uploading).FirstAsync();
((IButtonController)view.UploadButton).SendClicked();
await uploaded;

Console.WriteLine(browser.Objects.Count);
```

```text
3
```

Which overload to use depends on how the controls come about. When a few fixed controls share one command, such as a row of buttons that each add a different amount, bind each control with its own parameter stream. When the controls are generated from a collection, give each item a view model with the value as a property, and bind that property with the lambda form. Each row then reads its own value.

Every kind of control the generator knows about supports a bound parameter. The generator picks a mechanism by the shape of the control, so a control on another platform works when it has one of these shapes.

| Shape of the control | How the command runs | How the parameter arrives |
| --- | --- | --- |
| A settable `Command` and `CommandParameter` (MAUI, WPF and WinUI buttons) | The control runs its own command | Written to `CommandParameter` before `Command` |
| A `Click`, `TouchUpInside` or `Pressed` event and an `Enabled` property (WinForms controls) | The binding handles the event | Passed to `CanExecute` and `Execute` |
| A `Click`, `TouchUpInside` or `Pressed` event and no `Enabled` property | The binding handles the event | Passed to `CanExecute` and `Execute` |
| An Android `View` | The binding handles `Click` | Passed to `CanExecute` and `Execute` |
| A UIKit `UIControl`, `UIRefreshControl` or `UIBarButtonItem` | The binding handles a touch, `ValueChanged` or `Clicked` | Passed to `CanExecute` and `Execute` |
| A Cocoa control with a target and action | The binding sets the target and action | Read when the control fires |

Only the MAUI row runs in the examples. The other rows follow from the generator's source.

Each `BindCommand` call is its own binding, and it returns an `IDisposable`. Disposing it removes the handler from the control's event and stops the binding from watching the command. Suppose you make a binding with `toEvent` and never dispose it. Its handler stays subscribed for as long as the control lives. A long-lived view should dispose it when the view goes away. Bind one button twice, with and without `toEvent`, and both bindings reach the command. The plain binding attaches the command to the button and follows `CanExecute`. The event binding runs the command on each click. After `clicked.Dispose()` a press still reaches the command through the plain binding, and the count rises by one instead of two.

```csharp
StatementExportViewModel viewModel = new() { HasStatement = true };
StatementExportView view = new() { ViewModel = viewModel };

var attached = view.BindCommand(viewModel, x => x.ExportCommand, v => v.ExportButton);
var clicked = view.BindCommand(viewModel, x => x.ExportCommand, v => v.ExportButton, toEvent: ClickedEventName);

Console.WriteLine(ReferenceEquals(viewModel.ExportCommand, view.ExportButton.Command));

// One press reaches the command through both bindings.
((IButtonController)view.ExportButton).SendClicked();

Console.WriteLine(viewModel.ExportCount);

clicked.Dispose();
((IButtonController)view.ExportButton).SendClicked();

Console.WriteLine(viewModel.ExportCount);

// Only the plain binding follows CanExecute, so the button goes dark while no statement is chosen.
viewModel.HasStatement = false;

Console.WriteLine(view.ExportButton.IsEnabled);

attached.Dispose();
```

```text
True
2
3
False
``` The analyzer reports RXUIBIND007 when the control has no event to bind ([Setup](setup.md#understand-the-analyzers)). On WPF and WinForms, raise `CanExecuteChanged` on the thread that owns the button. A command that changes its state on a pool thread throws on WPF ([Threading and platforms](threading.md)).

### Choose how a control gets its command

An `ICreatesCommandBinding` decides how a command reaches a control. The runtime asks every registered binder how well it fits the control and uses the one with the highest score, its **affinity**. A score of `0` means the binder cannot bind the control ([Mechanisms](mechanisms.md#write-a-command-binder) shows how to write and register one). The example binder, `ClickedCommandBinder`, scores a MAUI `Button` at `10` and any other control at `0`. Ask a binder for its score with `GetAffinityForObject`.

```csharp
var binder = ChooseBinder();

Console.WriteLine(binder.GetAffinityForObject<Button>(hasEventTarget: false));
Console.WriteLine(binder.GetAffinityForObject<Entry>(hasEventTarget: false));
```

```text
10
0
```

A binder has three `BindCommandToObject` methods. Each returns an `IDisposable` that detaches the command. The [example file](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/bindings/CreatesCommandBindingExamples.cs) calls all three. The first binds the control's default event. Disposing the result detaches the command.

```csharp
StatementExportViewModel viewModel = new() { HasStatement = true };
StatementExportView view = new();
var binder = ChooseBinder();

using (binder.BindCommandToObject(viewModel.ExportCommand, view.ExportButton, Signal.Never<object?>()))
{
    ((IButtonController)view.ExportButton).SendClicked();

    Console.WriteLine(viewModel.ExportCount);
}

((IButtonController)view.ExportButton).SendClicked();

Console.WriteLine(viewModel.ExportCount);
```

```text
1
1
```

The second binds an event by name, which is what `toEvent` reaches. It returns `null` for an event the binder does not know, so check the result. The example asks for `Clicked`, which the binder knows, and for `Pressed`, which it does not.

```csharp
StatementExportViewModel viewModel = new() { HasStatement = true };
StatementExportView view = new();
var binder = ChooseBinder();

using var clicked = binder.BindCommandToObject<Button, EventArgs>(viewModel.ExportCommand, view.ExportButton, Signal.Never<object?>(), ClickedEventName);
var pressed = binder.BindCommandToObject<Button, EventArgs>(viewModel.ExportCommand, view.ExportButton, Signal.Never<object?>(), PressedEventName);

((IButtonController)view.ExportButton).SendClicked();

Console.WriteLine(viewModel.ExportCount);
Console.WriteLine(pressed is null);
```

```text
The binder is asked for the Clicked event, which carries EventArgs
The binder is asked for the Pressed event, which carries EventArgs
1
True
```

The third takes the add and remove handlers for an event. Use it when you know the event at build time, because no reflection then finds the event. The example runs a search command each time the filter box raises `TextChanged`, and stops once the binding is disposed.

```csharp
TodoView view = new();
var binder = ChooseBinder();
Command search = new(static () => Console.WriteLine("search"));
var entry = view.FilterTextBox;

using (binder.BindCommandToObject<Entry, TextChangedEventArgs>(
    search,
    entry,
    Signal.Never<object?>(),
    handler => entry.TextChanged += handler,
    handler => entry.TextChanged -= handler))
{
    entry.Text = CarFilter;
}

entry.Text = string.Empty;
```

```text
search
```

`BindCommandUnsafe` asks the registered binders for the control's event. When none fits the control, nothing is attached. The MAUI package registers no binder, so register your own for the controls you use.

## Run a command for each value

`InvokeCommand` runs a command with each value of a stream, and the value is the command's parameter. It asks the command's `CanExecute` first and drops a value the command refuses. It does not queue the value for later. An error in the stream is rethrown on the thread that raised it, and completion is ignored. Disposing the returned `IDisposable` stops the runs.

The example runs the sign-in command each time the access token changes. A blank token is refused by the command and dropped.

```csharp
IssueBoardViewModel viewModel = new(InMemoryGitHubServer.CreateSeeded());

using var subscription = viewModel.WhenChanged(x => x.Token).InvokeCommand(viewModel.SignInCommand);

viewModel.Token = "   ";

Console.WriteLine(viewModel.IsSignedIn);
Console.WriteLine(viewModel.RateLimitRemaining);

var loaded = viewModel.WhenChanged(x => x.Repositories).Where(static repositories => repositories.Count > 0).FirstAsync();
viewModel.Token = InMemoryGitHubServer.PriyaToken;
await loaded;

Console.WriteLine(viewModel.IsSignedIn);
```

```text
False
60
True
```

A second overload takes an object and a lambda for a command property, and it reads the command from the property. A `null` command drops the values offered while it stands. A later command takes over from the next value, and replacing the command runs nothing by itself. Below, choosing a repository loads its issues.

```csharp
var viewModel = await SignInAsync();

using var subscription = viewModel.WhenChanged(x => x.SelectedRepository!).InvokeCommand(viewModel, x => x.LoadIssuesCommand);

Console.WriteLine(viewModel.Issues.Count);

var loaded = viewModel.WhenChanged(x => x.RateLimitRemaining).Skip(1).FirstAsync();
viewModel.SelectedRepository = viewModel.Repositories[0];
await loaded;

Console.WriteLine(viewModel.Issues.Count);
```

```text
0
2
```

The [example file](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/bindings/InvokeCommandExamples.cs) also drives a command from a timer and skips the ticks that arrive while a refresh runs. It loads the next page when the last row appears, and it runs a command once at start-up.

## Answer a question from the view model

A view model sometimes needs an answer from the user, such as "Close this issue?". It has no business showing a dialog. An **interaction** lets it ask without knowing the UI. The view model owns an `Interaction<TInput, TOutput>` and calls `Handle` with the input. The view registers a **handler**, a method that shows the dialog and calls `SetOutput` with the answer. `Handle` returns that answer.

`BindInteraction` registers a handler on the interaction that a view model property holds. The handler receives an `IInteractionContext<TInput, TOutput>` with the `Input` and the `SetOutput` method. Register a handler that returns a `Task`, and the question stays open until the task ends. In the example the close waits for the dialog.

```csharp
var viewModel = await OpenWebshopIssuesAsync();
IssueBoardView view = new() { ViewModel = viewModel };
TaskCompletionSource<bool> dialog = new(TaskCreationOptions.RunContinuationsAsynchronously);

using var binding = view.BindInteraction(viewModel, x => x.ConfirmClose, async context => context.SetOutput(await dialog.Task));

var issue = viewModel.Issues[0];
viewModel.SelectedIssue = issue;
var closing = viewModel.CloseIssueAsync();

Console.WriteLine(closing.IsCompleted);
Console.WriteLine(issue.State);

dialog.SetResult(true);
await closing;

Console.WriteLine(issue.State);
```

```text
False
Open
Closed
```

A handler can also return an `IObservable<T>`. The interaction moves on when the observable completes, ignores its values and treats a fault in it as a fault of `Handle`. The teacher below confirms a drop only on the second try.

```csharp
GradebookViewModel viewModel = new(InMemoryStudentRecords.CreateSeeded());
await viewModel.LoadCoursesAsync();
viewModel.SelectedCourse = viewModel.Courses[0];
await viewModel.OpenCourseAsync();
viewModel.SelectedStudent = viewModel.Roster[BenRosterIndex];
GradebookView view = new() { ViewModel = viewModel };
var teacherConfirms = false;

using var binding = view.BindInteraction(viewModel, x => x.ConfirmDrop, context =>
{
    context.SetOutput(teacherConfirms);
    return Signal.Return(context.Input.Student.Id);
});

await viewModel.DropAsync();

Console.WriteLine(viewModel.Roster.Count);

teacherConfirms = true;
await viewModel.DropAsync();

Console.WriteLine(viewModel.Roster.Count);
```

```text
3
2
```

The property can have the type `IInteraction<TInput, TOutput>`, the interface that `Interaction<TInput, TOutput>` implements. `BindInteraction` accepts either.

```csharp
var board = await OpenWebshopIssuesAsync();
IssueTriageViewModel viewModel = new(board);
IssueTriageView view = new() { ViewModel = viewModel };

using var binding = view.BindInteraction(viewModel, x => x.ConfirmClose, static context =>
{
    context.SetOutput(context.Input.State == IssueState.Open);
    return Task.CompletedTask;
});

var issue = board.Issues[0];
board.SelectedIssue = issue;
await board.CloseIssueAsync();

Console.WriteLine(issue.State);
```

```text
Closed
```

Disposing the binding removes its handler, so the next question goes unanswered. The first question below is answered. The second throws `UnhandledInteractionException`.

```csharp
var viewModel = await OpenWebshopIssuesAsync();
IssueBoardView view = new() { ViewModel = viewModel };
var issue = viewModel.Issues[0];
var binding = view.BindInteraction(viewModel, x => x.ConfirmClose, static context =>
{
    context.SetOutput(false);
    return Task.CompletedTask;
});

Console.WriteLine(await viewModel.ConfirmClose.Handle(issue));

binding.Dispose();

var unanswered = false;
try
{
    _ = await viewModel.ConfirmClose.Handle(issue);
}
catch (UnhandledInteractionException<Issue, bool>)
{
    unanswered = true;
}

Console.WriteLine(unanswered);
```

```text
False
True
```

A view often exists before its view model does. In that case `BindInteraction` registers nothing and does not throw, and disposing the result is safe. The handler below never runs.

```csharp
IssueBoardView view = new();
IssueBoardViewModel? viewModel = null;
var handlerRuns = 0;

var binding = view.BindInteraction(viewModel, x => x.ConfirmClose, context =>
{
    handlerRuns++;
    context.SetOutput(true);
    return Task.CompletedTask;
});

binding.Dispose();

Console.WriteLine(handlerRuns);
```

```text
0
```

A screen can bind two interactions in turn. The bank confirms a large transfer, and then asks the view for a one-time code.

```csharp
var (viewModel, view) = await OpenTransferScreenAsync();

using var confirmation = view.BindInteraction(viewModel, x => x.ConfirmTransfer, static context =>
{
    Console.WriteLine("confirm");
    context.SetOutput(true);
    return Task.CompletedTask;
});

using var approval = view.BindInteraction(viewModel, x => x.ApproveTransfer, static context =>
{
    Console.WriteLine("approve");
    context.SetOutput(InMemoryBankingBackend.ApprovalCode);
    return Task.CompletedTask;
});

FillLargeTransfer(viewModel);
await viewModel.TransferAsync();

Console.WriteLine(viewModel.LastReceipt!.Amount);
```

```text
confirm
approve
1200
```

### Register handlers on the interaction

`Interaction<TInput, TOutput>` has three `RegisterHandler` overloads. One takes an `Action`, one takes a method that returns a `Task`, and one takes a method that returns an `IObservable<T>`. Each returns an `IDisposable` that removes the handler. `IInteraction<TInput, TOutput>` declares the same three methods and `Handle`. The example below uses only the interface. It registers each kind of handler, asks a question and removes a handler.

```csharp
IssueTriageViewModel triage = new(new(InMemoryGitHubServer.CreateSeeded()));
var confirmClose = triage.ConfirmClose;
Issue issue = new() { Number = CheckoutIssueNumber };
using var declines = confirmClose.RegisterHandler(static context => context.SetOutput(false));
using var asksAgain = confirmClose.RegisterHandler(static context => Signal.Return(context.IsHandled));
var confirms = confirmClose.RegisterHandler(static context =>
{
    context.SetOutput(true);
    return Task.CompletedTask;
});

Console.WriteLine(await confirmClose.Handle(issue));

confirms.Dispose();

Console.WriteLine(await confirmClose.Handle(issue));
```

```text
True
False
```

Handlers run in reverse order of registration: the handler registered last answers first. A handler need not answer. When it does not call `SetOutput`, the interaction offers the question to the next handler. Below, the handler in the middle ignores the question and the latest one answers. Disposing the latest registration hands the question to the first.

```csharp
Interaction<TransferDraft, string> approval = new();
TransferDraft draft = new();
using var first = approval.RegisterHandler(static context => context.SetOutput(FirstAnswer));
using var ignoring = approval.RegisterHandler(static context => Signal.Return(context.IsHandled));
var latest = approval.RegisterHandler(static context =>
{
    context.SetOutput(LatestAnswer);
    return Task.CompletedTask;
});

Console.WriteLine(await approval.Handle(draft));

latest.Dispose();

Console.WriteLine(await approval.Handle(draft));
```

```text
latest
first
```

### Read the context

Every handler receives the same context object for one question. `IInteractionContext<TInput, TOutput>` gives the `Input`, `IsHandled` and `SetOutput`. The class behind it, `InteractionContext<TInput, TOutput>`, also implements `IOutputContext<TInput, TOutput>`, which adds `GetOutput`. `IsHandled` is `false` until a handler sets the output. Reading the output early, or setting it a second time, throws `InvalidOperationException`. The handler below casts the context to `InteractionContext` and reads the members in order.

```csharp
Interaction<Issue, bool> confirmClose = new();
Issue issue = new() { Number = CheckoutIssueNumber };

using var registration = confirmClose.RegisterHandler(static context =>
{
    var answer = (InteractionContext<Issue, bool>)context;

    Console.WriteLine(answer.Input.Number);
    Console.WriteLine(answer.IsHandled);

    answer.SetOutput(true);

    Console.WriteLine(answer.IsHandled);
    Console.WriteLine(answer.GetOutput());
});

Console.WriteLine(await confirmClose.Handle(issue));
```

```text
101
False
True
True
True
```

The next example checks the rules. It casts the context to `IOutputContext` and tries to read the output before it is set, and to set it twice. Each attempt is refused.

```csharp
Interaction<Issue, bool> confirmClose = new();
Issue issue = new() { Number = CheckoutIssueNumber };
Issue? receivedInput = null;
var handledBefore = true;
var handledAfter = false;
var isInteractionContext = false;
var earlyReadRefused = false;
var readBack = false;
var secondAnswerRefused = false;

using var registration = confirmClose.RegisterHandler(context =>
{
    receivedInput = context.Input;
    handledBefore = context.IsHandled;
    isInteractionContext = context is InteractionContext<Issue, bool>;

    var output = (IOutputContext<Issue, bool>)context;
    earlyReadRefused = RefusesWithInvalidOperation(() => output.GetOutput());

    context.SetOutput(true);
    handledAfter = context.IsHandled;
    readBack = output.GetOutput();
    secondAnswerRefused = RefusesWithInvalidOperation(() => context.SetOutput(false));
});

var answer = await confirmClose.Handle(issue);

Console.WriteLine(ReferenceEquals(issue, receivedInput));
Console.WriteLine(handledBefore);
Console.WriteLine(handledAfter);
Console.WriteLine(isInteractionContext);
Console.WriteLine(earlyReadRefused);
Console.WriteLine(readBack);
Console.WriteLine(secondAnswerRefused);
Console.WriteLine(answer);
```

```text
True
False
True
True
True
True
True
True
```

### Handle a question that nobody answers

When no handler answers, `Handle` throws `UnhandledInteractionException<TInput, TOutput>`. It carries the `Input` and the `Interaction` that asked.

```csharp
var viewModel = await OpenWebshopIssuesAsync();
var issue = viewModel.Issues[0];
viewModel.SelectedIssue = issue;
UnhandledInteractionException<Issue, bool>? failure = null;

try
{
    await viewModel.CloseIssueAsync();
}
catch (UnhandledInteractionException<Issue, bool> ex)
{
    failure = ex;
}

Console.WriteLine(failure is not null);
Console.WriteLine(failure!.Message);
Console.WriteLine(ReferenceEquals(issue, failure.Input));
Console.WriteLine(ReferenceEquals(viewModel.ConfirmClose, failure.Interaction));
Console.WriteLine(issue.State);
```

```text
True
Failed to find a registration for an Interaction.
True
True
Open
```

The exception has four constructors. They take nothing, a message, a message and an inner exception, or the interaction and its input. The last builds the standard message above. The example calls each one and reads back what it holds.

```csharp
Interaction<Issue, bool> confirmClose = new();
Issue issue = new() { Number = CheckoutIssueNumber };
InvalidOperationException cause = new("The dialog host has shut down.");

UnhandledInteractionException<Issue, bool> empty = new();
UnhandledInteractionException<Issue, bool> withMessage = new(NobodyAnswersMessage);
UnhandledInteractionException<Issue, bool> withCause = new(NobodyAnswersMessage, cause);
UnhandledInteractionException<Issue, bool> forQuestion = new(confirmClose, issue);

Console.WriteLine(empty.Interaction is null);
Console.WriteLine(withMessage.Message);
Console.WriteLine(ReferenceEquals(cause, withCause.InnerException));
Console.WriteLine(forQuestion.Message);
Console.WriteLine(ReferenceEquals(confirmClose, forQuestion.Interaction));
Console.WriteLine(ReferenceEquals(issue, forQuestion.Input));
```

```text
True
Nobody answers close confirmations.
True
Failed to find a registration for an Interaction.
True
True
```

### Derive your own interaction

`Interaction<TInput, TOutput>` is not sealed. Override `Handle` to wrap a question, call `GetHandlers` to read the registered handlers in order of registration, and override `GenerateContext` to wrap the context each handler receives. `AuditedInteraction` writes each question and answer to the console, as a bank keeps an audit trail of an approval.

```csharp
public override async Task<TOutput> Handle(TInput input)
{
    Console.WriteLine($"ask {input} of {GetHandlers().Length} handlers");
    return await base.Handle(input).ConfigureAwait(false);
}

protected override IOutputContext<TInput, TOutput> GenerateContext(TInput input) => new AuditingOutputContext<TInput, TOutput>(base.GenerateContext(input));
```

## When a binding fails

A write can fail: a converter throws, a setter refuses a value, or the stream that feeds the write ends in an error. A failed write has no caller to report to, because it arrives on whichever thread raised the change. `BindingErrors` sets the rules that every binding follows.

- Every fault is logged against the bound expression.
- A fault that carries an inner exception, such as a setter that threw, is rethrown as a `TargetInvocationException`. The exception names the expression and holds the cause.
- A fault with no inner exception is only logged, because the stream itself ended in error. A stream that has ended writes nothing more.
- Completion is ignored.

The log entries go to a logger registered with the service locator. Without one you see nothing. The example registers a logger that writes each entry to the console, so the later examples can print them.

```csharp
AppLocator.CurrentMutable.RegisterConstant<ILogger>(new ConsoleLogger { ExceptionMessageFormat = "{0}" });
```

Generated code calls `BindingErrors.Subscribe`, and you can call it yourself. It takes a stream, the write and the bound expression. The example feeds the three cases through it: values, a fault without a cause and a fault with one.

```csharp
List<string> written = [];
Signal<string> values = new();
Signal<string> withoutCause = new();
Signal<string> withCause = new();
InvalidOperationException cause = new(CauseMessage);

using (BindingErrors.Subscribe(values, written.Add, BoundExpression))
using (BindingErrors.Subscribe(withoutCause, written.Add, BoundExpression))
using (BindingErrors.Subscribe(withCause, written.Add, BoundExpression))
{
    values.OnNext(ShortReference);
    values.OnCompleted();

    Console.WriteLine(string.Join(", ", written));

    // A fault with no cause is logged.
    withoutCause.OnError(new InvalidOperationException(ConversionMessage));

    // A fault with a cause is logged and rethrown, wrapped with the bound expression.
    try
    {
        withCause.OnError(new InvalidOperationException(ConversionMessage, cause));
    }
    catch (TargetInvocationException ex)
    {
        Console.WriteLine(ReferenceEquals(cause, ex.InnerException));
        Console.WriteLine(ex.Message.Contains(BoundExpression, StringComparison.Ordinal));
    }
}
```

```text
Rent March
LogHost: x => x.Reference Binding received an Exception! (OnError)
LogHost: x => x.Reference Binding received an Exception! (OnError)
True
True
```

A setter that throws does so on the thread that raised the change, so the exception reaches whoever changed the view model property. A binding created over a bad value throws while it is created. The example target refuses a reference longer than 18 characters, and both cases throw an `ArgumentException`.

```csharp
TransferDraft draft = new() { Reference = ShortReference };
StatementReferenceView field = new();

using (draft.BindOneWay(field, x => x.Reference, v => v.Reference))
{
    Console.WriteLine(field.Reference);

    try
    {
        draft.Reference = LongReference;
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

Console.WriteLine(field.Reference);

try
{
    using var second = draft.BindOneWay(field, x => x.Reference, v => v.Reference);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
```

```text
Rent March
A statement reference has at most 18 characters. (Parameter 'value')
Rent March
A statement reference has at most 18 characters. (Parameter 'value')
```

A real binding follows the same rules. When the converter throws with no inner exception, the binding logs the fault and rethrows nothing. The faulted stream has ended, so the label writes nothing more.

```csharp
TransferDraft draft = new() { Reference = ShortReference };
Label label = new();

using (draft.BindOneWay(label, x => x.Reference, v => v.Text, static reference => reference == ForbiddenReference ? throw new InvalidOperationException(ConversionMessage) : reference))
{
    Console.WriteLine(label.Text);

    draft.Reference = ForbiddenReference;

    // The faulted sequence has ended, so the binding writes nothing more.
    draft.Reference = ShortReference;
    Console.WriteLine(label.Text);
    draft.Reference = AprilReference;
    Console.WriteLine(label.Text);
}
```

```text
Rent March
LogHost: v => v.Text Binding received an Exception! (OnError)
Rent March
Rent March
```

When the converter throws with an inner exception, the binding logs the fault and rethrows it as a `TargetInvocationException` that names the bound expression and carries the cause.

```csharp
TransferDraft draft = new() { Reference = ShortReference };
Label label = new();
InvalidOperationException cause = new(CauseMessage);

using (draft.BindOneWay(label, x => x.Reference, v => v.Text, reference => reference == ForbiddenReference ? throw new InvalidOperationException(ConversionMessage, cause) : reference))
{
    try
    {
        draft.Reference = ForbiddenReference;
    }
    catch (TargetInvocationException ex)
    {
        Console.WriteLine(ReferenceEquals(cause, ex.InnerException));
        Console.WriteLine(ex.Message);
    }
}
```

```text
LogHost: v => v.Text Binding received an Exception! (OnError)
True
v => v.Text Binding received an Exception!
```

## Refuse a binding with a hook

A **hook** looks at a binding as it is created and can refuse it. Write a class that implements `IPropertyBindingHook` and register it with the service locator. Its `ExecuteHook` method receives the source, the target, two methods that read the current values of each side, and the `BindingDirection`. Return `false` to cancel the binding. The hook below refuses every two-way binding whose source is the accounts view model.

```csharp
public bool ExecuteHook(
    object? source,
    object target,
    Func<IObservedChange<object, object>[]> getCurrentViewModelProperties,
    Func<IObservedChange<object, object>[]> getCurrentViewProperties,
    BindingDirection direction)
{
    if (source is not AccountsViewModel || direction != BindingDirection.TwoWay)
    {
        return true;
    }

    Console.WriteLine("read-only: refused a two-way binding");
    return false;
}
```

`BindOneWay`, `BindTwoWay`, `OneWayBind` and `Bind` each ask the hooks once. A refused binding that returns an `IDisposable` returns an empty one, and nothing is written to the view. A refused view-first binding returns `null`, so check for `null` before you use the result. One-way bindings pass this hook, as the second call below shows.

```csharp
var accounts = await OpenAccountsAsync();
AccountNameView view = new() { ViewModel = accounts };

AppLocator.CurrentMutable.RegisterConstant<IPropertyBindingHook>(new ReadOnlyAccountHook());
BindingHooks.Refresh();

var refused = view.Bind(accounts, x => x.SelectedAccount!.Name, v => v.NameTextBox.Text);
using var allowed = view.OneWayBind(accounts, x => x.SelectedAccount!.Name, v => v.NameTextBox.Text);

Console.WriteLine(refused is null);
Console.WriteLine(allowed is null);
Console.WriteLine(view.NameTextBox.Text);
```

```text
read-only: refused a two-way binding
True
False
Everyday Account
```

`BindingHooks` holds the registered hooks. `BindingHooks.Any` says whether any hook exists. A binding tests it first, so an application with no hook pays nothing. `BindingHooks.ShouldBind` asks the hooks in turn and stops at the first refusal, so a hook after a refusing hook is never asked. Bindings call it for you, and you call it yourself only to test a hook.

Most applications register no hook. `BindingHooks.Any` is then `false` and a binding asks nobody, so the check costs almost nothing. The example prints `Any` and binds one property.

```csharp
var accounts = await OpenAccountsAsync();
AccountNameView view = new();

Console.WriteLine(BindingHooks.Any);

using (accounts.BindOneWay(view, x => x.SelectedAccount!.Name, v => v.NameTextBox.Text))
{
    Console.WriteLine(view.NameTextBox.Text);
}
```

```text
False
Everyday Account
```

The next example registers a hook that writes down every binding. Each of the four binding methods asks the hook once, with its direction.

```csharp
var accounts = await OpenAccountsAsync();
AccountNameView view = new() { ViewModel = accounts };

AppLocator.CurrentMutable.RegisterConstant<IPropertyBindingHook>(new LoggingBindingHook("log"));
BindingHooks.Refresh();

Console.WriteLine(BindingHooks.Any);

using (accounts.BindOneWay(view, x => x.SelectedAccount!.Name, v => v.NameTextBox.Text))
using (accounts.BindTwoWay(view, x => x.SelectedAccount!.Name, v => v.NameTextBox.Text))
using (view.OneWayBind(accounts, x => x.SelectedAccount!.Name, v => v.NameTextBox.Text))
using (view.Bind(accounts, x => x.SelectedAccount!.Name, v => v.NameTextBox.Text))
{
    Console.WriteLine("all four bound");
}

RemoveHooks();
```

```text
True
log: OneWay: AccountsViewModel -> AccountNameView
log: TwoWay: AccountsViewModel -> AccountNameView
log: OneWay: AccountsViewModel -> AccountNameView
log: TwoWay: AccountsViewModel -> AccountNameView
all four bound
```

A refused source-first binding returns an empty disposable. Here the hook refuses `BindTwoWay`, so the box shows nothing and an edit never reaches the account. Once the hook is gone the same binding writes the edit.

```csharp
var accounts = await OpenAccountsAsync();
AccountNameView view = new();
var everydayName = accounts.SelectedAccount!.Name;

AppLocator.CurrentMutable.RegisterConstant<IPropertyBindingHook>(new ReadOnlyAccountHook());
BindingHooks.Refresh();

using (accounts.BindTwoWay(view, x => x.SelectedAccount!.Name, v => v.NameTextBox.Text))
{
    // The refused binding is an empty disposable: nothing was written to the view either.
    Console.WriteLine($"'{view.NameTextBox.Text}'");

    view.NameTextBox.Text = RenamedEverydayName;
    Console.WriteLine(accounts.SelectedAccount.Name == everydayName);
}

// One-way bindings pass the hook.
using (accounts.BindOneWay(view, x => x.SelectedAccount!.Name, v => v.NameTextBox.Text))
{
    Console.WriteLine(view.NameTextBox.Text);
}

RemoveHooks();

// With the hook gone the same two-way binding writes the edit to the account.
using (accounts.BindTwoWay(view, x => x.SelectedAccount!.Name, v => v.NameTextBox.Text))
{
    view.NameTextBox.Text = RenamedEverydayName;
    Console.WriteLine(accounts.SelectedAccount.Name);
}
```

```text
read-only: refused a two-way binding
''
True
Everyday Account
Household Account
```

You can ask the hooks yourself with `BindingHooks.ShouldBind`. The example registers three hooks. The first and third only log, and the second refuses a two-way binding. The one-way question reaches all three. The two-way question stops at the second, so the third is never asked.

```csharp
var accounts = await OpenAccountsAsync();
AccountNameView view = new();

AppLocator.CurrentMutable.RegisterConstant<IPropertyBindingHook>(new LoggingBindingHook("first"));
AppLocator.CurrentMutable.RegisterConstant<IPropertyBindingHook>(new ReadOnlyAccountHook());
AppLocator.CurrentMutable.RegisterConstant<IPropertyBindingHook>(new LoggingBindingHook("last"));
BindingHooks.Refresh();

var oneWay = BindingHooks.ShouldBind(accounts, view, Observed(accounts), Observed(view), BindingDirection.OneWay);
var twoWay = BindingHooks.ShouldBind(accounts, view, Observed(accounts), Observed(view), BindingDirection.TwoWay);

Console.WriteLine(oneWay);
Console.WriteLine(twoWay);

RemoveHooks();
```

```text
first: OneWay: AccountsViewModel -> AccountNameView
last: OneWay: AccountsViewModel -> AccountNameView
first: TwoWay: AccountsViewModel -> AccountNameView
read-only: refused a two-way binding
True
False
```

`BindingHooks` reads the registrations once and keeps them. Register every hook before the first binding, or call `BindingHooks.Refresh` after you register one. The example below registers a hook late. The first binding does not ask it, and the binding after `Refresh` does.

```csharp
var accounts = await OpenAccountsAsync();
AccountNameView view = new();

Console.WriteLine(BindingHooks.Any);

AppLocator.CurrentMutable.RegisterConstant<IPropertyBindingHook>(new LoggingBindingHook("late"));

// The hook is registered, but the registrations were read before it arrived, so this binding does not ask it.
using (accounts.BindOneWay(view, x => x.SelectedAccount!.Name, v => v.NameTextBox.Text))
{
    Console.WriteLine("bound without the hook");
}

BindingHooks.Refresh();

Console.WriteLine(BindingHooks.Any);

using (accounts.BindOneWay(view, x => x.SelectedAccount!.Name, v => v.NameTextBox.Text))
{
    Console.WriteLine("bound with the hook");
}

RemoveHooks();

Console.WriteLine(BindingHooks.Any);
```

```text
False
bound without the hook
True
late: OneWay: AccountsViewModel -> AccountNameView
bound with the hook
False
```

You can also call a hook's `ExecuteHook` yourself with values you build. This is how you test a hook without creating a binding. The logging hook always agrees, and the read-only hook refuses a two-way binding.

```csharp
AccountsViewModel accounts = new(new InMemoryBankingBackend());
AccountNameView view = new();

Console.WriteLine(AskHook(new LoggingBindingHook("audit"), accounts, view, BindingDirection.OneWay));
Console.WriteLine(AskHook(new ReadOnlyAccountHook(), accounts, view, BindingDirection.OneWay));
Console.WriteLine(AskHook(new ReadOnlyAccountHook(), accounts, view, BindingDirection.TwoWay));
```

```text
audit: OneWay: AccountsViewModel -> AccountNameView
True
True
read-only: refused a two-way binding
False
```

## Next steps

- [Observing](observing.md) watches properties without writing them anywhere.
- [Converters](converters.md) lists the converters a binding picks by itself, and [Custom converters](custom-converters.md) writes your own.
- [Threading and platforms](threading.md) explains the owning thread and the sequencers.
- [Unsafe twins and the runtime fallback](unsafe.md) covers a call the generator cannot read.
- [API reference](api.md) lists every public member with its parameters.

## API reference

Each binding call has several overloads, and one row covers all of them. The generic-parameter lists and the compiler-filled `callerFilePath` and `callerLineNumber` parameters are in the [API reference](api.md).

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`BindOneWay`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.Binding.cs) | Copies a source property to a target property one way. It writes the current value and each later change. | Extension method on a source object of a class type. Returns `IDisposable`. Overloads add a conversion function, or a converter object with an optional hint. | The overloads that take a sequencer are in [ReactiveSchedulerExtensions.cs](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveSchedulerExtensions.cs). The plain overload needs the same type on both sides. A hook that refuses the binding leaves nothing bound. |
| [`OneWayBind`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.Binding.cs) | Copies a view model property to a view property one way. | Extension method on a view that implements `IViewFor`. Returns `IReactiveBinding<TView, TVProp>`. Overloads add a selector function, or a converter object with an optional hint, and a sequencer. | Returns `null` when a hook refuses the binding. Reads the view model you pass, so set the view's `ViewModel` first. |
| [`BindTwoWay`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.Binding.cs) | Keeps a source property and a target property equal. It seeds the target from the source, then copies each change to the other side. | Extension method on a source object of a class type. Returns `IDisposable`. Overloads add one conversion function per direction, or two converter objects with an optional hint, and a sequencer. | Disposing disconnects both directions. A hook that refuses the binding leaves nothing bound. A sequencer holds only the newest value. |
| [`Bind`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.Binding.cs) | The view-first form of `BindTwoWay`. It keeps a view model property and a view property equal. | Extension method on a view that implements `IViewFor`. Returns `IReactiveBinding<TView, BindingChange>`. Takes the same extra arguments as `BindTwoWay`. | Returns `null` when a hook refuses the binding. Writes back to the view model only while the view's `ViewModel` is set. |
| [`BindTo`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.BindTo.cs) | Writes each value of a stream to a target property. | Extension method on `IObservable<TValue>`. Returns `IDisposable`. Four overloads: plain, converter override, conversion hint, and hint with converter override. | Writes on the target's owning thread when its platform has one. When no converter matches the two types, it writes nothing and reports nothing. Name a `string` hint as `conversionHint:`. |
| [`BindCommand`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.BindCommand.cs) | Attaches the command a view model property holds to a control on the view. | Extension method on a view that implements `IViewFor`. Returns `IDisposable`. Three overloads: no parameter, a stream parameter, or a view model property parameter. Each takes an optional `toEvent`. | Binds nothing when the view model is `null`. Rebinds when the command or the control changes. A `null` `toEvent` selects the control's default event. Never picks a parameter for you. |
| [`InvokeCommand`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.InvokeCommand.cs) | Runs a command with each value of a stream as its parameter. | Extension method on `IObservable<T>`. Returns `IDisposable`. Two overloads: a fixed `ICommand`, or an object and a lambda for a command property. | Drops a value the command refuses, and does not queue it. The property form drops values while the command is `null`, and executes nothing when the object is `null`. |
| [`BindInteraction`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.BindInteraction.cs) | Registers a handler on the interaction a view model property holds. | Extension method on a view that implements `IViewFor`. Returns `IDisposable`. Two overloads: a handler that returns `Task`, or one that returns `IObservable<T>`. | Moves the handler to the new interaction when the property changes. Registers nothing when the view model is `null`. Disposing removes the handler. |
| [`IInteraction<TInput, TOutput>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/IInteraction.cs) | A question the view model asks and a view answers. | Interface. Members: `Handle` and `RegisterHandler`. | `Interaction<TInput, TOutput>` implements it. |
| [`IInteraction.Handle`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/IInteraction.cs) | Asks the question and returns the answer. | Takes `TInput`. Returns `Task<TOutput>`. | Completes with the output that a handler sets. |
| [`IInteraction.RegisterHandler`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/IInteraction.cs) | Adds a handler. | Three overloads: `Action`, a method that returns `Task`, and a method that returns `IObservable<TDontCare>`. Returns `IDisposable`. | Disposing the result removes the handler. |
| [`IInteractionContext<TInput, TOutput>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/IInteractionContext.cs) | The object a handler receives for one question. | Interface. Members: `Input`, `IsHandled` and `SetOutput`. | Every handler of one question receives the same object. |
| [`IInteractionContext.Input`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/IInteractionContext.cs) | The value the view model asked about. | Read-only `TInput`. | |
| [`IInteractionContext.IsHandled`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/IInteractionContext.cs) | Says whether a handler has set the output. | Read-only `bool`. | `false` until `SetOutput` runs. |
| [`IInteractionContext.SetOutput`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/IInteractionContext.cs) | Answers the question. | Takes `TOutput`. Returns nothing. | A second call throws `InvalidOperationException`. |
| [`IOutputContext<TInput, TOutput>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/IOutputContext.cs) | Adds the ability to read the answer. | Interface that extends `IInteractionContext<TInput, TOutput>`. Member: `GetOutput`. | `Interaction<TInput, TOutput>.GenerateContext` returns this type. |
| [`IOutputContext.GetOutput`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/IOutputContext.cs) | Reads the answer. | Returns `TOutput`. | Throws `InvalidOperationException` before a handler sets the output. |
| [`Interaction<TInput, TOutput>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/Interaction.cs) | The class a view model owns to ask questions. | Non-sealed class that implements `IInteraction<TInput, TOutput>`. | Handlers run in reverse order of registration. Derive from it to wrap `Handle` or the context. |
| [`Interaction()`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/Interaction.cs) | Creates an interaction with no handlers. | Public constructor with no parameters. | |
| [`Interaction.Handle`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/Interaction.cs) | Runs the handlers, latest registered first, until one sets an output. | Virtual. Takes `TInput`. Returns `Task<TOutput>`. | Throws `UnhandledInteractionException<TInput, TOutput>` when no handler sets an output. |
| [`Interaction.RegisterHandler`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/Interaction.cs) | Adds a handler. | Three overloads, as on `IInteraction`. Returns `IDisposable`. | A handler that returns an observable finishes when the observable completes. A fault in the observable faults `Handle`. A `null` handler throws `ArgumentNullException`. |
| [`Interaction.GetHandlers`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/Interaction.cs) | Reads the registered handlers. | Protected. Returns an array of `Func<IInteractionContext<TInput, TOutput>, Task>`. | A copy, earliest registered first. |
| [`Interaction.GenerateContext`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/Interaction.cs) | Creates the context every handler receives for one `Handle` call. | Protected virtual. Takes `TInput`. Returns `IOutputContext<TInput, TOutput>`. | Override it to wrap the context. |
| [`InteractionContext<TInput, TOutput>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/InteractionContext.cs) | The default context. It holds the input and the answer. | Sealed class that implements `IOutputContext<TInput, TOutput>`. Has `Input`, `IsHandled`, `SetOutput` and `GetOutput`. | The constructor is internal, so you receive it from `Handle`. Setting the output twice throws `InvalidOperationException`. |
| [`UnhandledInteractionException<TInput, TOutput>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/UnhandledInteractionException.cs) | Reports a question that no handler answered. | Class that derives from `Exception`. Four constructors: none, a message, a message and an inner exception, or an interaction and its input. | The last constructor sets the message to "Failed to find a registration for an Interaction." |
| [`UnhandledInteractionException.Input`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/UnhandledInteractionException.cs) | The input of the question that went unanswered. | Read-only `TInput`. | The default value when the exception was built without an input. |
| [`UnhandledInteractionException.Interaction`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interactions/UnhandledInteractionException.cs) | The interaction that went unanswered. | Read-only `Interaction<TInput, TOutput>?`. | `null` when the exception was built without an interaction. |
| [`BindingChange`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingChange.cs) | A value a two-way binding moved, and which side produced it. | Readonly record struct with a constructor `(object? Value, bool FromViewModel)`. | Compares, deconstructs and copies like any record struct. |
| [`BindingChange.Value`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingChange.cs) | The value that was written. | `object?`, settable in an object initializer. | Can be `null`. |
| [`BindingChange.FromViewModel`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingChange.cs) | Says whether the view model produced the value. | `bool`, settable in an object initializer. | `false` means the view produced it. Tells an edit apart from the echo of the binding's own write. |
| [`BindingDirection`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/BindingDirection.cs) | Names the direction of a binding. | Enum: `OneWay`, `TwoWay`, `AsyncOneWay`. | Read from `IReactiveBinding.Direction` and passed to hooks. |
| [`BindingDirection.OneWay = 0`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/BindingDirection.cs) | One way, from source to target. | `0` | |
| [`BindingDirection.TwoWay = 1`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/BindingDirection.cs) | Both ways, between source and target. | `1` | |
| [`BindingDirection.AsyncOneWay = 2`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/BindingDirection.cs) | One way, from source to target, delivered asynchronously. | `2` | |
| [`IReactiveBinding<TView, TValue>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IReactiveBinding.cs) | A binding between a view and a view model property. | Interface that extends `IDisposable`. `TView` must implement `IViewFor`. | Returned by `OneWayBind` and `Bind`. |
| [`IReactiveBinding.Changed`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IReactiveBinding.cs) | A stream of the values the binding writes. | Read-only `IObservable<TValue>`. | Starts with the current value. |
| [`IReactiveBinding.Direction`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IReactiveBinding.cs) | The direction of the binding. | Read-only `BindingDirection`. | |
| [`IReactiveBinding.View`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IReactiveBinding.cs) | The view the binding writes. | Read-only `TView`. | |
| [`IReactiveBinding.ViewExpression`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IReactiveBinding.cs) | The expression of the bound view property. | Read-only `Expression?`. | `null` when the binding carries none. A generated binding carries none. |
| [`IReactiveBinding.ViewModelExpression`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IReactiveBinding.cs) | The expression of the bound view model property. | Read-only `Expression?`. | `null` when the binding carries none. A generated binding carries none. |
| [`ReactiveBinding<TView, TValue>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/ReactiveBinding.cs) | The standard `IReactiveBinding` that generated view-first bindings return. | Sealed class. Constructor `(TView view, IObservable<TValue> changed, BindingDirection direction, IDisposable subscription)`. | `ViewExpression` and `ViewModelExpression` are always `null`. |
| [`ReactiveBinding.Dispose`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/ReactiveBinding.cs) | Disposes the subscription. | Returns nothing. | Acts on the first call only. Later calls do nothing. |
| [`BindingHooks`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingHooks.cs) | Lets registered hooks inspect, or refuse, a binding as it is created. | Static class. Members: `Any`, `Refresh` and `ShouldBind`. | The service locator is read on first use, and the hooks are cached until `Refresh`. |
| [`BindingHooks.Any`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingHooks.cs) | Says whether any hook was registered when the hooks were last read. | Read-only `bool`. | A binding tests it first, so an application with no hook pays nothing. |
| [`BindingHooks.Refresh`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingHooks.cs) | Discards the cached hooks so the next use reads them again. | Returns nothing. | Call it after you register a hook late. |
| [`BindingHooks.ShouldBind`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingHooks.cs) | Asks the hooks in turn whether a binding may be created. | Takes the source (`object?`), the target, two functions that read the current values of each side, and a `BindingDirection`. Returns `bool`. Hidden from IntelliSense. | Stops at the first refusal. Returns `true` when no hook is registered. Throws `ArgumentNullException` for a `null` target or `null` functions. |
| [`IPropertyBindingHook`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IPropertyBindingHook.cs) | A class you write to look at, or cancel, a binding as it is created. | Interface with one method, `ExecuteHook`. | Register it with the service locator. |
| [`IPropertyBindingHook.ExecuteHook`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IPropertyBindingHook.cs) | Called as a binding is set up, before anything is wired. | Takes the source (`object?`), the target, two functions that read the current values of each side, and a `BindingDirection`. Returns `bool`. | Return `false` to cancel the binding. |
| [`BindingErrors`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingErrors.cs) | Sets how a binding reacts when the stream that feeds its write faults. | Static class. Member: `Subscribe`. | A fault is logged. A fault with an inner exception is rethrown as `TargetInvocationException`. Completion is ignored. |
| [`BindingErrors.Subscribe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingErrors.cs) | Subscribes a write to its source and applies those rules. | Takes an `IObservable<T>`, an `Action<T>` and the bound expression as a `string`. Returns `IDisposable`. Hidden from IntelliSense. | Throws `ArgumentNullException` for a `null` source or `null` write. Disposing disconnects the write. |
