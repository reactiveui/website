---
Order: 9
---
# Threading

[Run the complete page example](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/threading/threading.csproj).

A UI framework lets only one thread touch a control. That thread is the control's **owning thread**. A view model often changes on another thread: a background sync delivers a new title, or an upload reports its progress from a pool thread. If a binding wrote that value to the control from the wrong thread, the framework would throw.

Every binding API that writes to a view keeps the write on the owning thread. This page shows how the engine finds that thread, how you choose where waiting writes run, and what each platform module adds. The observing APIs, such as `WhenChanged`, do not move anything. They deliver on the thread that raised the change, and you decide where to observe. See [observing](../observing.md).

The WPF and WinForms examples need Windows and run from the `net10.0-windows10.0.19041.0` build. The Avalonia, MAUI and sequencer examples run on any operating system from the `net10.0` build. [Run the examples](#run-the-examples) shows the commands.

Start with the walkthrough, which uses Avalonia. Later sections cover the invoker contract, the delivery rules, the schedulers you can set, the sequencer overloads and the WPF, WinForms, MAUI, Avalonia and Uno modules.

## Write to a control from any thread

**1. See what the framework refuses.** Avalonia throws `InvalidOperationException` when a thread other than the UI thread sets a control property. The control stays as it was. A binding has to avoid this call. The example below sets `Text` from a worker thread and records whether Avalonia refused. It shows the failure that every later step avoids.

```csharp
TextBlock titleLabel = new();
bool refused = false;

Thread worker = new Thread(() =>
{
    try
    {
        titleLabel.Text = RenamedTitle;
    }
    catch (InvalidOperationException)
    {
        refused = true;
    }
});
worker.Start();
worker.Join();

Console.WriteLine(refused);
Console.WriteLine(titleLabel.Text ?? NoText);
```

```text
True
(no text)
```

**2. Get an invoker.** An **invoker** is a small object that tells the engine which thread owns a control and how to reach it. It implements `IViewThreadInvoker`, which has three members. `Claims` says whether the object belongs to this invoker's platform. `CheckAccess` says whether the calling thread may write to the object directly. `Post` queues a callback on the owning thread. The `ReactiveUI.Binding.Avalonia` package ships the Avalonia invoker, `AvaloniaViewThreadInvoker`. It asks each control for its dispatcher, and uses that dispatcher's `AvaloniaScheduler`: the ReactiveUI.Primitives sequencer that runs work on the dispatcher's thread. The class below is the whole invoker.

```csharp
public sealed class AvaloniaViewThreadInvoker : SequencerViewThreadInvoker<AvaloniaObject, AvaloniaScheduler>
{
    public static AvaloniaViewThreadInvoker Instance { get; } = new();

    protected override AvaloniaScheduler SequencerFor(AvaloniaObject target) => AvaloniaScheduler.For(target.Dispatcher);
}
```

`SequencerViewThreadInvoker` supplies the three members. `Claims` accepts any `AvaloniaObject`. `CheckAccess` asks the sequencer whether the calling thread owns it. `Post` schedules the callback on the sequencer and hands `state` to it unchanged. The engine passes its own object as `state`, so the callback needs no captured variables.

**3. Register the invoker.** Register one instance as an `IViewThreadInvoker` service, then call `ViewThreadInvokers.Refresh`. The engine reads the registered invokers once and keeps them. `Refresh` makes it read them again, so call it when you register an invoker after the first binding exists. The example below registers the Avalonia invoker, then asks `ForTarget` about an Avalonia control, a MAUI label and `null`. The answers show which objects the invoker claims.

```csharp
TextBlock titleLabel = new();
MauiLabel unclaimed = new();
AvaloniaViewThreadInvoker invoker = new();

AppLocator.CurrentMutable.RegisterConstant<IViewThreadInvoker>(invoker);
ViewThreadInvokers.Refresh();

Console.WriteLine(ReferenceEquals(ViewThreadInvokers.ForTarget(titleLabel), invoker));
Console.WriteLine(ViewThreadInvokers.ForTarget(unclaimed) is null);
Console.WriteLine(ViewThreadInvokers.ForTarget(null) is null);
```

```text
True
True
True
```

`ViewThreadInvokers.ForTarget` returns the first registered invoker that claims an object. It returns `null` when no invoker claims the object, and when the object is `null`. The engine calls it for you, so it is hidden from IntelliSense. The examples after this one run with the Avalonia invoker registered.

**4. Bind as usual.** You write nothing extra in the binding. When the view model changes on the owning thread, the write runs at once and nothing waits. The example below binds a to-do title to a label and changes the title on the UI thread. It prints the label after each step, so you can see the write happen at once.

```csharp
TextBlock titleLabel = new();
TodoItem item = new() { Title = OriginalTitle };

using (item.BindOneWay(titleLabel, x => x.Title, x => x.Text))
{
    Console.WriteLine(titleLabel.Text);

    item.Title = RenamedTitle;

    Console.WriteLine(titleLabel.Text);
}
```

```text
Renew car registration
Renew car registration online
```

**5. Change the value from another thread.** The binding does not write on the worker thread. It holds the value and asks the invoker to post a callback to the owning thread. The label keeps its text until the owning thread runs its queue. In Avalonia, `Dispatcher.UIThread.RunJobs()` runs that queue. The example below changes the title from a worker thread and prints the label before and after the queue runs. The two lines show the write waiting for the owning thread.

```csharp
TextBlock titleLabel = new();
TodoItem item = new() { Title = OriginalTitle };

using (item.BindOneWay(titleLabel, x => x.Title, x => x.Text))
{
    Thread worker = new Thread(() => item.Title = FirstSyncedTitle);
    worker.Start();
    worker.Join();

    Console.WriteLine(titleLabel.Text);

    Dispatcher.UIThread.RunJobs();

    Console.WriteLine(titleLabel.Text);
}
```

```text
Renew car registration
Renew car registration (draft)
```

**6. Change it more than once.** Only the newest value waits. When three values arrive before the owning thread runs, the control receives one write with the last value. The example subscribes to the label's own `Text` changes to count the writes. The first entry is the empty text the label held when the observation started.

```csharp
TextBlock titleLabel = new();
TodoItem item = new() { Title = OriginalTitle };
List<string?> writes = [];

using (titleLabel.WhenChanged(x => x.Text).Subscribe(writes.Add))
using (item.BindOneWay(titleLabel, x => x.Title, x => x.Text))
{
    Thread worker = new Thread(() =>
    {
        item.Title = FirstSyncedTitle;
        item.Title = SecondSyncedTitle;
        item.Title = LastSyncedTitle;
    });
    worker.Start();
    worker.Join();

    Dispatcher.UIThread.RunJobs();

    Console.WriteLine(string.Join(" | ", writes.Select(static text => text ?? NoText)));
}
```

```text
(no text) | Renew car registration | Renew car registration (final)
```

The draft and reviewed titles never reach the label. [Only the newest value waits](#only-the-newest-value-waits) explains why.

## The view thread invoker

### The three members

The invoker answers its three questions from the platform's own API. The example calls each member from the test thread and from a worker thread.

```csharp
TextBlock titleLabel = new();
MauiLabel unclaimed = new();
AvaloniaViewThreadInvoker invoker = new();
List<string> log = [];
bool accessFromWorker = true;

Thread worker = new Thread(() => accessFromWorker = invoker.CheckAccess(titleLabel));
worker.Start();
worker.Join();
invoker.Post(titleLabel, static state => ((List<string>)state!).Add(PostedText), log);

Console.WriteLine(invoker.Claims(titleLabel));
Console.WriteLine(invoker.Claims(unclaimed));
Console.WriteLine(invoker.CheckAccess(titleLabel));
Console.WriteLine(accessFromWorker);
Console.WriteLine(log.Count);

Dispatcher.UIThread.RunJobs();

Console.WriteLine(string.Join(", ", log));
```

```text
True
False
True
False
0
posted
```

These rules govern the members:

- `Claims` answers for one platform's types only. It returns `false` for every other object.
- `CheckAccess` returns `true` on the owning thread. It also returns `true` for an object that has no owning thread, so the binding writes at once.
- `Post` queues the callback on the owning thread. It must not run the callback on the calling thread, except for an object that has no owning thread.
- The engine calls `CheckAccess` and `Post` only for an object that `Claims` accepted. The platform invokers throw `InvalidCastException` for any other type.

A platform that has no invoker in the library needs only these three members. When the platform has a ReactiveUI.Primitives sequencer that implements `IThreadAffineSequencer`, derive from `SequencerViewThreadInvoker` instead, as the Avalonia invoker does. Return the sequencer that owns an object from `SequencerFor`, or `null` for an object that has no owning thread.

### Find the invoker for a control

`ViewThreadInvokers` holds the registered invokers. It has two members: `Refresh` and `ForTarget`, both shown in step 3. `ForTarget` asks each registered invoker in registration order and returns the first that claims the object. Register one invoker for each platform and keep `Claims` narrow, so the answer does not depend on registration order.

### When no invoker claims the control

A binding that finds no invoker for its target writes on the calling thread. It hands the value straight to the control and does not wait. The control then decides what happens. An `Unsafe` binding uses only the registered invokers. The example makes one before any invoker is registered, so Avalonia refuses the write from the worker thread. The exception reaches the code that changed the view model property. The binding wraps it in a `TargetInvocationException`, with Avalonia's `InvalidOperationException` as its inner exception.

```csharp
AvaloniaTodoView view = new();
TodoItem item = new() { Title = OriginalTitle };
string? failure = string.Empty;

using (item.BindOneWayUnsafe(view, x => x.Title, v => v.RemainingLabel.Text))
{
    Thread worker = new Thread(() =>
    {
        try
        {
            item.Title = RenamedTitle;
        }
        catch (Exception ex)
        {
            failure = ex.GetType().Name;
        }
    });
    worker.Start();
    worker.Join();
}

Console.WriteLine(failure);
```

```text
TargetInvocationException
```

For WPF, WinForms, MAUI, Avalonia and Uno controls, [the generated binding carries its own invoker](#the-generated-fallback), so this case does not arise for a generated binding.

## How a write is delivered

The engine sends each value a binding writes through one stage. The stage follows these rules in order.

1. **Pick the invoker.** The stage uses the first registered invoker that claims the target. A generated binding also carries a fallback invoker for its platform. The stage uses the fallback when no registered invoker claims the target. With no invoker at all, the source passes through unchanged.
2. **Write at once when it is safe.** A notification runs inline on the calling thread when no value is waiting and `CheckAccess` returns `true`.
3. **Otherwise wait.** The stage holds the notification and schedules one drain. The drain writes what waits. It runs on `BindingSchedulers.MainThread` when that property is set. Otherwise it runs through the invoker's `Post`.

### Only the newest value waits

The stage keeps one slot for a waiting value. A newer value replaces the one in the slot. That holds even for a value raised on the owning thread while a drain runs. The drain loops until the slot is empty. An error or the end of the stream waits beside the value and reaches the observer after it.

The engine keeps only the newest value because of two-way bindings. Writing a view raises the view's own change at once. If an older value were waiting, that echo would write the older value back to the view model. The write would raise another change, and the two sides would bounce forever. Disposing the subscription drops the waiting value.

`WitnessLatestOn` applies the same rule to a stream you own. It comes from ReactiveUI.Primitives and takes a sequencer. The [observing](../observing.md) page uses it on a `WhenChanged` stream. The operator builds a `WitnessLatestOnSignal<T>`. Its subscription keeps one pending value under a lock. Each new value overwrites the pending one. The subscription asks the sequencer for a single drain, and each drain delivers the newest value. An error or completion waits behind the pending value. The plain `WitnessOn` operator delivers every value instead.

The binding engine keeps this rule in its own stage, `ViewThreadObservable`, because the stage also has the inline path from rule 2. The two differ in one way. `WitnessLatestOn` always goes through its sequencer. The engine's stage writes inline on the owning thread when nothing waits.

## Choose where waiting writes run

`BindingSchedulers` sets where a waiting write runs. A **sequencer** is an `ISequencer` from ReactiveUI.Primitives: an object that decides when and on which thread queued work runs. `BindingSchedulers` has one property, three routing methods and one helper.

| Member | What it does |
|--------|--------------|
| `MainThread` | The sequencer that runs a waiting write for an object an invoker claims. `null` uses the invoker's `Post`. |
| `UseSynchronizationContext` | Sets `MainThread` from a `SynchronizationContext`. `null` clears it. |
| `ObserveOnViewThread` | Routes a stream onto the thread that owns a control, with the registered invokers. One overload takes a fallback invoker. |
| `ObserveOnSequencer` | Routes a stream onto a sequencer, so that only the newest waiting value is delivered. |

The three routing methods are for generated code and are hidden from IntelliSense. The sections below show what each one does.

### Run waiting writes on the UI sequencer

By default the invoker's `Post` runs a waiting write. Set `BindingSchedulers.MainThread` when your application has a sequencer for its UI thread. The Avalonia example uses `AvaloniaScheduler.Instance` from the `ReactiveUI.Primitives.Avalonia` package.

`AvaloniaScheduler` implements `ISequencer` on top of Avalonia's dispatcher. `Instance` is the shared scheduler for `Dispatcher.UIThread`. It posts to the dispatcher at `DispatcherPriority.Background`. You can build your own with `new AvaloniaScheduler(dispatcher)` or `new AvaloniaScheduler(dispatcher, priority)`. The scheduler batches queued work into one dispatcher drain and runs it without reentrancy.

The two objects have separate jobs. The invoker decides **who** a write belongs to and whether the calling thread may write. The sequencer decides **where and when** a waiting write runs. The engine reads `MainThread` each time it schedules a drain, so a change applies to existing bindings. Set it once at start-up. If a test sets it, clear it in a `finally` block, as the examples do.

The example below sets `MainThread` to the shared Avalonia scheduler and changes the title from a worker thread. It prints the label before and after the queue runs, so you can see that the sequencer delivers the write.

```csharp
TextBlock titleLabel = new();
TodoItem item = new() { Title = OriginalTitle };
AvaloniaScheduler sequencer = AvaloniaScheduler.Instance;
BindingSchedulers.MainThread = sequencer;

try
{
    using (item.BindOneWay(titleLabel, x => x.Title, x => x.Text))
    {
        Thread worker = new Thread(() => item.Title = SyncedTitle);
        worker.Start();
        worker.Join();

        Console.WriteLine(ReferenceEquals(BindingSchedulers.MainThread, sequencer));
        Console.WriteLine(titleLabel.Text);

        Dispatcher.UIThread.RunJobs();

        Console.WriteLine(titleLabel.Text);
    }
}
finally
{
    BindingSchedulers.MainThread = null;
}
```

```text
True
Renew car registration
Renew car registration (reviewed)
```

`MainThread` only carries writes from another thread to an object an invoker claims. It never sees a write on the owning thread, because there is nothing to carry. The example below sets `MainThread` and then changes the title on the UI thread. The label updates at once, which shows the sequencer is not involved.

```csharp
TextBlock titleLabel = new();
TodoItem item = new() { Title = OriginalTitle };
BindingSchedulers.MainThread = AvaloniaScheduler.Instance;

try
{
    using (item.BindOneWay(titleLabel, x => x.Title, x => x.Text))
    {
        item.Title = RenamedTitle;

        Console.WriteLine(titleLabel.Text);
    }
}
finally
{
    BindingSchedulers.MainThread = null;
}
```

```text
Renew car registration online
```

`MainThread` never sees a write to an object that no invoker claims either. The write runs on the thread that raised it. This example binds a MAUI `Label`, which the Avalonia invoker does not claim. The write from the worker thread reaches the label at once, before any dispatcher runs.

```csharp
MauiLabel titleLabel = new();
TodoItem item = new() { Title = OriginalTitle };
BindingSchedulers.MainThread = AvaloniaScheduler.Instance;

try
{
    using (item.BindOneWay(titleLabel, x => x.Title, x => x.Text))
    {
        Thread worker = new Thread(() => item.Title = SyncedTitle);
        worker.Start();
        worker.Join();

        Console.WriteLine(titleLabel.Text);
    }
}
finally
{
    BindingSchedulers.MainThread = null;
}
```

```text
Renew car registration (reviewed)
```

### Use a synchronization context

A **synchronization context** is a .NET object that queues work back onto a particular thread. `SynchronizationContext.Current` returns the one for the calling thread. `UseSynchronizationContext` wraps a context in a `SynchronizationContextSequencer` and sets it as `MainThread`. Passing `null` sets `MainThread` back to `null`, so writes return to the invoker's `Post`. See [`SynchronizationContext`](https://learn.microsoft.com/dotnet/api/system.threading.synchronizationcontext) for the type itself.

The example below sets the current context as `MainThread`, changes the title from a worker thread and clears the setting at the end. The write waits for the UI queue, as it does with a sequencer.

```csharp
TextBlock titleLabel = new();
TodoItem item = new() { Title = OriginalTitle };
BindingSchedulers.UseSynchronizationContext(SynchronizationContext.Current);

try
{
    Console.WriteLine(BindingSchedulers.MainThread is SynchronizationContextSequencer);

    using (item.BindOneWay(titleLabel, x => x.Title, x => x.Text))
    {
        Thread worker = new Thread(() => item.Title = SyncedTitle);
        worker.Start();
        worker.Join();

        Console.WriteLine(titleLabel.Text);

        Dispatcher.UIThread.RunJobs();

        Console.WriteLine(titleLabel.Text);
    }
}
finally
{
    BindingSchedulers.UseSynchronizationContext(null);
}

Console.WriteLine(BindingSchedulers.MainThread is null);
```

```text
True
Renew car registration
Renew car registration (reviewed)
True
```

### Route your own stream

Generated bindings call the routing methods for you. You can call them on a stream you write to a control yourself. `ObserveOnViewThread` takes the stream and the control. It returns the stream itself when the control is `null` or no registered invoker claims it. Otherwise it returns the stream routed with the rules above. The example below routes a `WhenChanged` stream to a label and raises a change from a worker thread. It prints what the subscriber holds before and after the queue runs, to show that delivery waits for the owning thread.

```csharp
TextBlock titleLabel = new();
TodoItem item = new() { Title = OriginalTitle };
List<string> delivered = [];

IObservable<string> routed = BindingSchedulers.ObserveOnViewThread(item.WhenChanged(x => x.Title), titleLabel);

using (routed.Subscribe(delivered.Add))
{
    Thread worker = new Thread(() => item.Title = SyncedTitle);
    worker.Start();
    worker.Join();

    Console.WriteLine(string.Join(", ", delivered));

    Dispatcher.UIThread.RunJobs();

    Console.WriteLine(string.Join(", ", delivered));
}
```

```text
Renew car registration
Renew car registration, Renew car registration (reviewed)
```

A value that another thread raises reaches the subscriber only after the owning thread runs its queue. The first line shows the values the subscriber had when the worker thread finished. The second line shows them after `Dispatcher.UIThread.RunJobs()` runs the queue.

The overload with a `fallback` parameter uses that invoker when no registered invoker claims the control. It returns the stream itself only when the control is `null`, and it throws `ArgumentNullException` for a `null` stream or fallback. Generated code passes the platform's fallback here. The example below passes `new AvaloniaViewThreadInvoker()` as the fallback. The registered invoker claims the label, so the fallback goes unused and the output matches the previous example.

```csharp
TextBlock titleLabel = new();
TodoItem item = new() { Title = OriginalTitle };
AvaloniaViewThreadInvoker fallback = new();
List<string> delivered = [];

IObservable<string> routed = BindingSchedulers.ObserveOnViewThread(item.WhenChanged(x => x.Title), titleLabel, fallback);

using (routed.Subscribe(delivered.Add))
{
    Thread worker = new Thread(() => item.Title = SyncedTitle);
    worker.Start();
    worker.Join();

    Console.WriteLine(string.Join(", ", delivered));

    Dispatcher.UIThread.RunJobs();

    Console.WriteLine(string.Join(", ", delivered));
}
```

```text
Renew car registration
Renew car registration, Renew car registration (reviewed)
```

`ObserveOnSequencer` routes a stream onto a sequencer. A delivery on a sequencer never runs inline. Every value waits for the sequencer, and a newer value replaces one that has not been delivered. The example below changes the title twice on the UI thread. It prints how many values arrived before the queue ran, then the values after, so you can see that only the newer title is delivered.

```csharp
TodoItem item = new() { Title = OriginalTitle };
AvaloniaScheduler sequencer = AvaloniaScheduler.Instance;
List<string> delivered = [];

IObservable<string> routed = BindingSchedulers.ObserveOnSequencer(item.WhenChanged(x => x.Title), sequencer);

using (routed.Subscribe(delivered.Add))
{
    item.Title = DraftTitle;
    item.Title = SyncedTitle;

    Console.WriteLine(delivered.Count);

    Dispatcher.UIThread.RunJobs();

    Console.WriteLine(string.Join(", ", delivered));
}
```

```text
0
Renew car registration (reviewed)
```

## Deliver a binding on a sequencer

The property-binding APIs `BindOneWay`, `BindTwoWay`, `OneWayBind` and `Bind` have overloads that take an `ISequencer`. The sequencer follows the property lambdas and any conversion arguments. The binding then delivers each write through `ObserveOnSequencer`. The first write waits too. These examples use `VirtualClock` from ReactiveUI.Primitives. It queues work and runs it only when you call `Start()`, so the examples show each step. In an application you pass the sequencer for your UI thread.

The example below binds a title to a MAUI label on a `VirtualClock`. It prints the label before the first `Start()`, after it and after a later edit. Every write, even the first, waits for the sequencer.

```csharp
Label titleLabel = new();
TodoItem item = new() { Title = OriginalTitle };
VirtualClock sequencer = new();

using (item.BindOneWay(titleLabel, x => x.Title, x => x.Text, sequencer))
{
    Console.WriteLine(titleLabel.Text ?? NoText);

    sequencer.Start();

    Console.WriteLine(titleLabel.Text);

    item.Title = RenamedTitle;

    Console.WriteLine(titleLabel.Text);

    sequencer.Start();

    Console.WriteLine(titleLabel.Text);
}
```

```text
(no text)
Renew car registration
Renew car registration
Renew car registration online
```

A two-way binding sends both directions through the sequencer. Two edits before the sequencer runs write once, with the second value. The binding does not replay the first edit. The example below edits the reference twice before the sequencer runs and prints both sides. Both show the second value.

```csharp
Entry referenceBox = new();
TransferDraft draft = new() { Reference = RentReference };
VirtualClock sequencer = new();

using (draft.BindTwoWay(referenceBox, x => x.Reference, x => x.Text, sequencer))
{
    sequencer.Start();

    draft.Reference = RentAprilReference;
    draft.Reference = RentMayReference;
    sequencer.Start();

    Console.WriteLine(referenceBox.Text);
    Console.WriteLine(draft.Reference);
}
```

```text
Rent May
Rent May
```

A conversion function or a converter goes before the sequencer. The example below formats the amount as text for the view and parses the text back for the view model. Each direction runs through the sequencer.

```csharp
Entry amountBox = new();
TransferDraft draft = new() { Amount = RentAmount };
VirtualClock sequencer = new();

using (draft.BindTwoWay(
    amountBox,
    x => x.Amount,
    x => x.Text,
    static amount => amount.ToString(TwoDecimalPlaces, CultureInfo.InvariantCulture),
    ParseAmount,
    sequencer))
{
    sequencer.Start();

    Console.WriteLine(amountBox.Text);

    amountBox.Text = RaisedRentAmountText;
    sequencer.Start();

    Console.WriteLine(draft.Amount);
}
```

```text
1200.00
1250.50
```

A converter also takes an optional hint, such as a format string. The example below passes a converter and the format `F2` as the hint, so the amount shows two decimal places once the sequencer runs.

```csharp
Entry amountBox = new();
TransferDraft draft = new() { Amount = RentAmount };
VirtualClock sequencer = new();

using (draft.BindOneWay(amountBox, x => x.Amount, x => x.Text, new DecimalToStringTypeConverter(), TwoDecimalPlaces, sequencer))
{
    sequencer.Start();

    Console.WriteLine(amountBox.Text);
}
```

```text
1200.00
```

`SequencerExamples.cs` in the example project has a method for every shape below. A scheduler argument of `null` means the binding writes on the thread that owns the target, as if you had named no scheduler.

| API | Sequencer overloads | Example method |
|-----|---------------------|----------------|
| `BindOneWay` | property, conversion function, converter with hint | `BindOneWayOnSequencer`, `BindOneWayWithConversionOnSequencerAsync`, `BindOneWayWithConverterOnSequencer` |
| `BindTwoWay` | property, two conversion functions, two converters with hint | `BindTwoWayOnSequencer`, `BindTwoWayBurstOnSequencer`, `BindTwoWayWithConversionOnSequencer`, `BindTwoWayWithConvertersOnSequencer` |
| `OneWayBind` | selector, converter with hint | `OneWayBindViewOnSequencerAsync`, `OneWayBindViewWithConverterOnSequencer` |
| `Bind` | two conversion functions, two converters with hint | `BindViewOnSequencer`, `BindViewWithConvertersOnSequencer` |
| `BindOneWayUnsafe`, `BindTwoWayUnsafe`, `OneWayBindUnsafe`, `BindUnsafe` | the same shapes, resolved at run time | `BindOneWayUnsafeOnSequencer` and the other `Unsafe` methods in the same file |

The `Unsafe` overloads are covered in [unsafe](../unsafe.md). The [bindings](../bindings.md) page covers the calls themselves and [converters](../converters.md) covers converters and hints.

## The platform modules

The library ships a view thread invoker for WPF, WinForms, MAUI, Avalonia and Uno. Each comes from a platform package and a module that registers it. Each invoker derives from `SequencerViewThreadInvoker` and routes writes through the ReactiveUI.Primitives sequencer that owns the object's thread.

| Platform | Builder call and module | Invoker | `Claims` | Sequencer | Runs inline when |
|----------|-------------------------|---------|----------|-----------|------------------|
| WPF (Windows) | `WithWpf`, `WpfBindingModule` | `Wpf.DispatcherViewThreadInvoker` | `DispatcherObject` | `DispatcherSequencer` for the object's dispatcher, at normal priority | the object has no dispatcher |
| WinForms (Windows) | `WithWinForms`, `WinFormsBindingModule` | `WinForms.ControlViewThreadInvoker` | `Control` | `ControlSequencer` for the control | no invoke is required |
| MAUI (any OS) | `WithMaui`, `MauiBindingModule` | `Maui.DispatcherViewThreadInvoker` | `BindableObject` | `MauiDispatcherSequencer` for the object's dispatcher | the object has no dispatcher |
| Avalonia (any OS) | `WithAvalonia`, `AvaloniaBindingModule` | `Avalonia.AvaloniaViewThreadInvoker` | `AvaloniaObject` | `AvaloniaScheduler` for the object's dispatcher, at background priority | never |
| Uno (any OS) | `WithUno`, `UnoBindingModule` | `Uno.UnoViewThreadInvoker` | `DependencyObject` | `DispatcherQueueSequencer` for the object's dispatcher queue | never |

`CheckAccess` asks the sequencer whether the calling thread owns it, and `Post` schedules the callback on the sequencer. The WPF, WinForms and Avalonia modules also register an observer for their platform's properties. The Avalonia module also registers a command binder. The MAUI module also registers the two Visibility converters. [Setup](../setup.md) covers the builder. Each `With` method returns the builder it was called on, so you can chain it, and each is available on `IAppBuilder` and on `IReactiveUIBindingBuilder`.

The example below builds an app with the MAUI module. It checks that the invoker and both converters are registered, and that the converter service holds the converters only after `ImportFrom`.

```csharp
ReactiveUIBindingBuilder builder = RxBindingBuilder.CreateReactiveUIBindingBuilder();
IReactiveUIBindingInstance app = builder.WithCoreServices().WithMaui().BuildApp();
ViewThreadInvokers.Refresh();
IBindingTypeConverter[] converters = app.Current!.GetServices<IBindingTypeConverter>().ToArray();

Console.WriteLine(app.Current!.GetServices<IViewThreadInvoker>().OfType<DispatcherViewThreadInvoker>().Any());
Console.WriteLine(converters.OfType<BooleanToVisibilityTypeConverter>().Any());
Console.WriteLine(converters.OfType<VisibilityToBooleanTypeConverter>().Any());
Console.WriteLine(builder.ConverterService.TypedConverters.TryGetConverter(typeof(bool), typeof(Visibility)) is null);

builder.ConverterService.ImportFrom(app.Current!);

Console.WriteLine(BindingConverters.Current.TypedConverters.TryGetConverter(typeof(bool), typeof(Visibility)) is BooleanToVisibilityTypeConverter);
Console.WriteLine(BindingConverters.Current.TypedConverters.TryGetConverter(typeof(Visibility), typeof(bool)) is VisibilityToBooleanTypeConverter);
```

```text
True
True
True
True
True
True
```

The MAUI module registers the invoker and both Visibility converters with the dependency resolver. The converter service does not hold the converters until `ImportFrom` copies them in, which the example shows. Call `ViewThreadInvokers.Refresh` after `BuildApp`, as the example does, when a binding has run.

You can apply a module to a resolver you own, without the builder. Create the module and call its `Configure` method with the resolver. The example below applies the module to a new resolver and checks what it registered. The invoker and both converters appear in the resolver.

```csharp
using ModernDependencyResolver resolver = new();
MauiBindingModule module = new();

module.Configure(resolver);

IBindingTypeConverter[] converters = resolver.GetServices<IBindingTypeConverter>().ToArray();

Console.WriteLine(resolver.GetServices<IViewThreadInvoker>().OfType<DispatcherViewThreadInvoker>().Any());
Console.WriteLine(converters.OfType<BooleanToVisibilityTypeConverter>().Any());
Console.WriteLine(converters.OfType<VisibilityToBooleanTypeConverter>().Any());
```

```text
True
True
True
```

The builder calls chain from either builder type. The first example holds the builder as `IAppBuilder`. It checks that `WithMaui` returns the same builder, so calls can keep chaining.

```csharp
using ModernDependencyResolver resolver = new();
IAppBuilder appBuilder = resolver.CreateReactiveUIBindingBuilder();

IReactiveUIBindingBuilder chained = appBuilder.WithMaui();

Console.WriteLine(ReferenceEquals(appBuilder, chained));
```

```text
True
```

The second example holds the builder as `IReactiveUIBindingBuilder` and checks the same for `WithWpf`.

```csharp
using ModernDependencyResolver resolver = new();
IReactiveUIBindingBuilder? builder = (IReactiveUIBindingBuilder)resolver.CreateReactiveUIBindingBuilder();

IReactiveUIBindingBuilder? chained = builder.WithWpf();

Console.WriteLine(ReferenceEquals(builder, chained));
```

```text
True
```

`WinFormsExamples.cs` and `WpfExamples.cs` hold the same checks for `WithWinForms`, `WithWpf`, `WinFormsBindingModule` and `WpfBindingModule`.

### The generated fallback

A binding the generator writes does not need the module to move its writes. The generator checks the type of the target while it reads the binding. It matches `System.Windows.Threading.DispatcherObject`, `System.Windows.Forms.Control`, `Microsoft.Maui.Controls.BindableObject`, `Avalonia.AvaloniaObject` and `Microsoft.UI.Xaml.DependencyObject`. The generated binding then carries the invoker for that platform as its fallback. `BindTo`, `BindOneWay`, `OneWayBind` and `Bind` do this for the target. `BindTwoWay` does it for both sides. `BindCommand` does it for the view, and for the control when the view has no invoker of its own, such as a plain view object that holds WPF buttons.

The fallback is the platform package's own invoker, through its `Instance` property: `DispatcherViewThreadInvoker` from `ReactiveUI.Binding.Wpf`, `ControlViewThreadInvoker` from `ReactiveUI.Binding.WinForms`, `DispatcherViewThreadInvoker` from `ReactiveUI.Binding.Maui`, `AvaloniaViewThreadInvoker` from `ReactiveUI.Binding.Avalonia`, or `UnoViewThreadInvoker` from `ReactiveUI.Binding.Uno`. The generator uses it only when your project references that package. Without the package the binding has no fallback, and RXUIBIND017 reports the call. Uno shares its dependency object type with WinUI, so a binding onto one without `ReactiveUI.Binding.Uno` is not reported; a plain WinUI app needs an invoker you register, as [Uno and WinUI](winui-uno.md) explains.

The first example follows an upload whose progress arrives on a pool thread. It runs without a module, so `ForTarget` finds nothing. The write reaches the progress bar on its owning thread, because the generated binding carries the WPF invoker.

```csharp
using WinFormsUploadForm view = new();

Console.WriteLine(ViewThreadInvokers.ForTarget(view.UploadProgressBar) is null);

await RunUploadFromWorkerThreadAsync(view);
```

```text
True
100
```

The second example runs the same upload after `BuildWpfApplication` registers the WPF module. The first line shows that `ForTarget` returns the registered invoker for the bar, and the progress arrives the same way.

```csharp
WpfUploadWindow view = new();

Console.WriteLine(ViewThreadInvokers.ForTarget(view.UploadProgressBar) is DispatcherViewThreadInvoker);

await RunUploadFromWorkerThreadAsync(view);
```

```text
True
100
```

The registered invoker matters for the `Unsafe` binding APIs. They resolve the target at run time and have no generated fallback, so they move a write only when a platform module or your own registration supplies the invoker.

### Platform pages

Each platform has its own page with its module, its invoker and its examples:

- [Avalonia](avalonia.md)
- [WPF](wpf.md)
- [WinForms](winforms.md)
- [MAUI](maui.md)
- [Uno and WinUI](winui-uno.md)

### Visibility converters

WPF and MAUI both have a `Visibility` type, and the platform packages ship two converters for it. `BooleanToVisibilityTypeConverter` turns a `bool` into a `Visibility`. `VisibilityToBooleanTypeConverter` turns a `Visibility` back into a `bool`. Both use `BooleanToVisibilityHints` as their conversion hint. `Inverse` flips the result. `UseHidden` gives `Hidden` instead of `Collapsed` for a `false` value. The hints combine with `|`. `None` and any other hint value leave the default behavior. Each converter reports an affinity of 2.

The example below converts a `bool` with each hint, including one that is not a hint, and prints the result. The output shows how `Inverse` and `UseHidden` change the answer and how an unknown hint leaves the default.

```csharp
BooleanToVisibilityTypeConverter converter = new();
(bool Value, object? Hint)[] cases =
[
    (true, null),
    (false, null),
    (false, BooleanToVisibilityHints.None),
    (true, BooleanToVisibilityHints.Inverse),
    (false, BooleanToVisibilityHints.Inverse),
    (false, BooleanToVisibilityHints.UseHidden),
    (true, BooleanToVisibilityHints.UseHidden),
    (true, BooleanToVisibilityHints.Inverse | BooleanToVisibilityHints.UseHidden),
    (false, "not a hint"),
];

Console.WriteLine(converter.GetAffinityForObjects());

foreach (var (value, hint) in cases)
{
    _ = converter.TryConvert(value, hint, out var visibility);

    Console.WriteLine(visibility);
}
```

```text
2
Visible
Collapsed
Collapsed
Collapsed
Visible
Hidden
Visible
Hidden
Collapsed
```

The reverse converter returns `true` only for `Visible`. The `Inverse` hint flips that. The example below converts each `Visibility` back to a `bool` with different hints, so you can check which values count as visible.

```csharp
VisibilityToBooleanTypeConverter converter = new();
(Visibility Value, object? Hint)[] cases =
[
    (Visibility.Visible, null),
    (Visibility.Hidden, null),
    (Visibility.Collapsed, BooleanToVisibilityHints.None),
    (Visibility.Visible, BooleanToVisibilityHints.Inverse),
    (Visibility.Collapsed, BooleanToVisibilityHints.Inverse),
    (Visibility.Collapsed, "not a hint"),
];

Console.WriteLine(converter.GetAffinityForObjects());

foreach (var (value, hint) in cases)
{
    _ = converter.TryConvert(value, hint, out var isVisible);

    Console.WriteLine(isVisible);
}
```

```text
2
True
False
False
False
True
False
```

You can bind a tick mark both ways with the two converters. The example below shows the mark when the item is done, and it reopens the item when the mark is hidden. The two converters make that round trip without any code of your own.

```csharp
TodoItem item = new() { Title = CarRegistrationTitle };
MauiTodoPage view = new();

using (item.BindTwoWay(view, x => x.IsDone, v => v.DoneMarkVisibility, new BooleanToVisibilityTypeConverter(), new VisibilityToBooleanTypeConverter()))
{
    Console.WriteLine(view.DoneMarkVisibility);
    Console.WriteLine(view.DoneMark.IsVisible);

    item.IsDone = true;

    Console.WriteLine(view.DoneMarkVisibility);
    Console.WriteLine(view.DoneMark.IsVisible);

    view.DoneMarkVisibility = Visibility.Hidden;

    Console.WriteLine(item.IsDone);
}
```

```text
Collapsed
False
Visible
True
False
```

`WpfExamples.cs` has the same converter methods, and its `BindDoneMarkWithBothConverters` binds `DoneMark.Visibility` instead. See [converters](../converters.md) for how converters and hints work in a binding.

### Whole screens on each platform

Each platform file in the example project also binds a to-do list and a transfer form to real controls, with the calls from the [bindings](../bindings.md) page. Look for `BindTransferFormAsync` and `BindTodoFilterAndCheckBoxAsync` in `AvaloniaExamples.cs`, `WpfExamples.cs`, `WinFormsExamples.cs` and `MauiExamples.cs`. The list and picker screens are `BindItemsListAndSelectionAsync` (Avalonia), `BindItemsListAndPriorityPickerAsync` and `BindPriorityPickerToSelectedIndexAsync` (WinForms) and `BindListViewItemsAndSelectionAsync` (MAUI).

## Run the examples

The example project has two target frameworks. Run it from the folder `src/examples/Documentation/Pages/threading`.

```bash
dotnet run --framework net10.0
```

That command runs the Avalonia, MAUI and sequencer examples on any operating system. On Windows, run the WPF examples and then the WinForms examples with these commands.

```bash
dotnet run --framework net10.0-windows10.0.19041.0
dotnet run --framework net10.0-windows10.0.19041.0 -- winforms
```

Related pages:

- [observing](../observing.md) covers delivery on the thread that raised a change.
- [bindings](../bindings.md) covers the binding calls.
- [setup](../setup.md) covers the builder.
- [unsafe](../unsafe.md) covers the run-time overloads.
- [api](../api.md) is the full reference.

## API reference

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`BindingSchedulers.MainThread`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingSchedulers.cs) | Sets the sequencer that runs a waiting write for an object an invoker claims. | Static property. `ISequencer?`, get and set. | `null` means the invoker's `Post` runs the write. The engine reads it each time it schedules a drain. It never sees a write on the owning thread or a write to an unclaimed object. |
| [`BindingSchedulers.UseSynchronizationContext`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingSchedulers.cs) | Sets `MainThread` from a synchronization context. | Static method. Takes a `SynchronizationContext?` and returns nothing. | A context becomes a `SynchronizationContextSequencer`. `null` sets `MainThread` to `null`. |
| [`BindingSchedulers.ObserveOnViewThread`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingSchedulers.cs) | Routes an observable onto the thread that owns a control. | Static method with two overloads. Takes `IObservable<T> source`, `object? target` and, in the second overload, an `IViewThreadInvoker fallback`. Returns `IObservable<T>`. | Returns `source` itself when `target` is `null`. The first overload also returns `source` when no registered invoker claims the target. The second uses `fallback` in that case. A `null` source throws `ArgumentNullException`, and so does a `null` fallback. Hidden from IntelliSense. |
| [`BindingSchedulers.ObserveOnSequencer`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingSchedulers.cs) | Routes an observable onto a sequencer, so only the newest waiting value is delivered. | Static method. Takes `IObservable<T> source` and `ISequencer scheduler`. Returns `IObservable<T>`. | Every value waits for the sequencer, including the first. Errors and completion follow the waiting value. A `null` source or scheduler throws `ArgumentNullException`. Hidden from IntelliSense. |
| [`IViewThreadInvoker.Claims`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IViewThreadInvoker.cs) | Says whether the invoker handles an object. | `bool Claims(object target)`. | `true` when the object belongs to the invoker's platform. Answer for one platform's types only. |
| [`IViewThreadInvoker.CheckAccess`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IViewThreadInvoker.cs) | Says whether the calling thread may write to an object directly. | `bool CheckAccess(object target)`. | `true` on the owning thread, and for an object that has no owning thread. The engine calls it only for an object that `Claims` accepted. |
| [`IViewThreadInvoker.Post`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Interfaces/IViewThreadInvoker.cs) | Queues a callback on the thread that owns an object. | `void Post(object target, Action<object?> callback, object? state)`. | Hands `state` to `callback` unchanged. Do not run the callback on the calling thread, except for an object that has no owning thread. |
| [`SequencerViewThreadInvoker<TTarget, TSequencer>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/SequencerViewThreadInvoker.cs) | Routes writes to a view object through the sequencer that owns its thread. | Abstract class that implements `IViewThreadInvoker`. `TSequencer` is an `ISequencer` that implements `IThreadAffineSequencer`. Override `protected abstract TSequencer? SequencerFor(TTarget target)`. | `Claims` accepts a `TTarget`. `CheckAccess` and `Post` ask the sequencer `SequencerFor` returns. When it returns `null`, `CheckAccess` is `true` and `Post` runs the callback at once. Any other type throws `InvalidCastException`. A `null` callback throws `ArgumentNullException`. |
| [`ViewThreadInvokers.ForTarget`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/ViewThreadInvokers.cs) | Finds the first registered invoker that claims an object. | Static method. Takes `object? target`. Returns `IViewThreadInvoker?`. | Returns `null` when `target` is `null` or nothing claims it. Asks the invokers in registration order. Hidden from IntelliSense. |
| [`ViewThreadInvokers.Refresh`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/ViewThreadInvokers.cs) | Makes the engine read the registered invokers again. | Static method. Takes nothing and returns nothing. | Call it after you register an invoker when a binding has run. The engine reads the registrations once and keeps them. Hidden from IntelliSense. |
