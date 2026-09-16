---
Order: 12
---
# UI platforms

A UI framework lets you touch a control only from its **UI thread**: the one thread that draws the screen and
handles input. A stream often sends values from somewhere else, such as a timer or a thread-pool thread. Setting
a label's text from there throws an exception, or silently corrupts the control.

Each UI platform has a **UI sequencer**: a [sequencer](scheduling.md) that runs work on that platform's UI thread.
Put `WitnessOn` in front of `Subscribe`, pass it the UI sequencer, and your callback runs on the UI thread.

| Platform | Package | Sequencer |
|---|---|---|
| WPF | `ReactiveUI.Primitives.Wpf` | `DispatcherSequencer` |
| WinForms | `ReactiveUI.Primitives.WinForms` | `ControlSequencer` |
| WinUI | `ReactiveUI.Primitives.WinUI` | `DispatcherQueueSequencer` |
| Avalonia | `ReactiveUI.Primitives.Avalonia` | `AvaloniaScheduler` |
| MAUI | `ReactiveUI.Primitives.Maui` | `MauiDispatcherSequencer` |
| Blazor | `ReactiveUI.Primitives.Blazor` | `BlazorRendererSequencer`, and the `ReactiveComponentBase` component |
| Android | `ReactiveUI.Primitives`, on an Android target | `HandlerSequencer` |
| iOS, macOS, Mac Catalyst, tvOS | `ReactiveUI.Primitives`, on an Apple target | `NSRunloopSequencer` |

Every sequencer is in the `ReactiveUI.Primitives.Concurrency` namespace. The Blazor types are the exception: they
are in `ReactiveUI.Primitives.Blazor.Concurrency` and `ReactiveUI.Primitives.Blazor.Components`.

In a ReactiveUI app you rarely create one yourself. ReactiveUI sets its main thread scheduler to the right one for
your platform. See [scheduling in the handbook](../handbook/scheduling.md).

## Your first UI update

You want a WPF window whose title counts the seconds since it opened.

**1. Add the package.**

```bash
dotnet add package ReactiveUI.Primitives.Wpf
```

**2. Make the sequencer.** `DispatcherSequencer` takes the window's `Dispatcher`: the WPF object that runs work
on the UI thread.

**3. Move the values to the UI thread.** `Signal.Every` ticks on a thread-pool timer. `WitnessOn(ui)` hands each
tick to the UI thread before your callback runs.

**4. Dispose when the window closes.** Disposing the subscription stops the timer.

```csharp
using System.Windows;
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.Primitives.Signals;

public sealed class ClockWindow : Window
{
    private readonly IDisposable _clock;

    public ClockWindow()
    {
        var ui = new DispatcherSequencer(Dispatcher);

        _clock = Signal.Every(TimeSpan.FromSeconds(1))
                       .WitnessOn(ui)
                       .Subscribe(tick => Title = $"Open for {tick + 1} s");
    }

    protected override void OnClosed(EventArgs e)
    {
        _clock.Dispose();
        base.OnClosed(e);
    }
}
```

After three seconds the title reads:

```text
Open for 3 s
```

Without `WitnessOn`, the first tick throws `InvalidOperationException`, because the timer's thread does not own the
window. The same four steps work on every platform below. Only the sequencer changes.

## How UI sequencers behave

All eight share the same rules.

- **Work runs in batches.** Everything you schedule before the UI thread gets to it runs in one go, in the order
  you scheduled it. The sequencer asks the UI thread for one turn, not one turn per item, so a busy stream does not
  flood the platform's queue.
- **Work never runs inline.** Scheduling from the UI thread itself still waits for the next batch. Your callback
  never runs in the middle of the code that sent the value.
- **Disposing cancels work that has not started.** Work you cancel before its batch runs is skipped.
- **Delays wait off the UI thread.** A delayed item waits on a timer, then joins a batch when it is due. WPF,
  WinUI, Avalonia, MAUI, Android and Apple use the platform's own timer. WinForms and Blazor use a shared
  thread-pool timer.
- **`Now` and `Timestamp`** read the system clock, the same as `Sequencer.Default`. See
  [Now and Timestamp](scheduling.md#now-and-timestamp).

Keep callbacks short. A batch runs on the UI thread, and the screen cannot redraw until it ends. Move slow work
before `WitnessOn` in the chain, so it runs off the UI thread. See [best practices](best-practices.md).

## WPF

`DispatcherSequencer` runs work through a WPF `Dispatcher`. [Your first UI update](#your-first-ui-update) shows it in a
window.

| Member | What it is |
|---|---|
| `new DispatcherSequencer(dispatcher)` | Runs work at `DispatcherPriority.Normal`. |
| `new DispatcherSequencer(dispatcher, priority)` | Runs work at the priority you give. |
| `Dispatcher` | The dispatcher you passed in. |
| `Priority` | The priority you passed in. |

A dispatcher **priority** decides what runs first when the UI thread is busy. Use a lower one, such as
`DispatcherPriority.Background`, for updates that can wait until input and drawing are done:

```csharp
using System.Windows.Threading;

var background = new DispatcherSequencer(Dispatcher, DispatcherPriority.Background);
```

## WinForms

`ControlSequencer` runs work on the thread that owns a control. Pass it the form, or any control on it.

```csharp
using System.Windows.Forms;
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.Primitives.Signals;

public sealed class ClockForm : Form
{
    private readonly Label _label = new() { Dock = DockStyle.Fill };
    private readonly IDisposable _clock;

    public ClockForm()
    {
        Controls.Add(_label);

        _clock = Signal.Every(TimeSpan.FromSeconds(1))
                       .WitnessOn(new ControlSequencer(this))
                       .Subscribe(tick => _label.Text = $"Open for {tick + 1} s");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _clock.Dispose();
        }

        base.Dispose(disposing);
    }
}
```

A WinForms control has a **handle**: the Windows object behind it, created when the control is first shown. Work
you schedule before the handle exists waits, and runs once it is created. So it is safe to subscribe in the
constructor, as above.

Scheduling on a control that has been disposed throws `ObjectDisposedException`. Dispose your subscriptions
first, as `Dispose(bool)` does above.

| Member | What it is |
|---|---|
| `new ControlSequencer(control)` | Runs work on the control's thread. |
| `Control` | The control you passed in. |

## WinUI

`DispatcherQueueSequencer` runs work through a WinUI `DispatcherQueue`. The `ToSequencer` extension makes one from
a queue you already hold, such as a window's `DispatcherQueue` property.

```csharp
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.Primitives.Signals;

public sealed class ClockWindow : Window
{
    private readonly IDisposable _clock;

    public ClockWindow()
    {
        var label = new TextBlock();
        Content = label;

        _clock = Signal.Every(TimeSpan.FromSeconds(1))
                       .WitnessOn(DispatcherQueue.ToSequencer())
                       .Subscribe(tick => label.Text = $"Open for {tick + 1} s");

        Closed += (_, _) => _clock.Dispose();
    }
}
```

When the queue has shut down, as it does after the window's thread ends, scheduling throws
`InvalidOperationException`. Dispose your subscriptions when the window closes, as above.

| Member | What it is |
|---|---|
| `new DispatcherQueueSequencer(queue)` | Runs work at `DispatcherQueuePriority.Normal`. |
| `new DispatcherQueueSequencer(queue, priority)` | Runs work at the priority you give. |
| `queue.ToSequencer()` | The same as `new DispatcherQueueSequencer(queue)`. |
| `DispatcherQueue` | The queue you passed in. |
| `Priority` | The priority you passed in. |

## Avalonia

`AvaloniaScheduler` runs work through an Avalonia `Dispatcher`. `AvaloniaScheduler.Instance` is ready-made for
`Dispatcher.UIThread`, so most apps never create one.

```csharp
using Avalonia.Controls;
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.Primitives.Signals;

public sealed class ClockWindow : Window
{
    private readonly IDisposable _clock;

    public ClockWindow()
    {
        _clock = Signal.Every(TimeSpan.FromSeconds(1))
                       .WitnessOn(AvaloniaScheduler.Instance)
                       .Subscribe(tick => Title = $"Open for {tick + 1} s");
    }

    protected override void OnClosed(EventArgs e)
    {
        _clock.Dispose();
        base.OnClosed(e);
    }
}
```

`Instance` and the one-argument constructor run work at `DispatcherPriority.Background`, after input and layout.
Pass a priority to run sooner:

```csharp
using Avalonia.Threading;

var urgent = new AvaloniaScheduler(Dispatcher.UIThread, DispatcherPriority.Normal);
```

| Member | What it is |
|---|---|
| `AvaloniaScheduler.Instance` | Runs work on `Dispatcher.UIThread` at `Background` priority. |
| `new AvaloniaScheduler(dispatcher)` | Runs work on your dispatcher at `Background` priority. |
| `new AvaloniaScheduler(dispatcher, priority)` | Runs work at the priority you give. |
| `Dispatcher` | The dispatcher you passed in. |
| `Priority` | The priority you passed in. |

## MAUI

`MauiDispatcherSequencer` runs work through a MAUI `IDispatcher`. Every page and view has a `Dispatcher` property,
and `ToSequencer` makes a sequencer from it.

Subscribe when the page appears and dispose when it goes, so a page in the back stack does no work:

```csharp
using Microsoft.Maui.Controls;
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.Primitives.Signals;

public sealed class ClockPage : ContentPage
{
    private readonly Label _label = new();
    private IDisposable? _clock;

    public ClockPage() => Content = _label;

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _clock = Signal.Every(TimeSpan.FromSeconds(1))
                       .WitnessOn(Dispatcher.ToSequencer())
                       .Subscribe(tick => _label.Text = $"Open for {tick + 1} s");
    }

    protected override void OnDisappearing()
    {
        _clock?.Dispose();
        base.OnDisappearing();
    }
}
```

On MAUI, cancelling delayed work skips it, but the platform's timer still runs to its due time.

| Member | What it is |
|---|---|
| `new MauiDispatcherSequencer(dispatcher)` | Runs work through the dispatcher. |
| `dispatcher.ToSequencer()` | The same as `new MauiDispatcherSequencer(dispatcher)`. |
| `Dispatcher` | The dispatcher you passed in. |

## Blazor

A Blazor component renders through the **renderer**: the part of Blazor that turns components into HTML. Code that
changes a component's state must run through the renderer's dispatcher, and must then call `StateHasChanged` so the
component draws again.

`ReactiveUI.Primitives.Blazor` gives you two ways to do that. `ReactiveComponentBase` does it all for you. Use
`BlazorRendererSequencer` when you cannot change a component's base class.

### `ReactiveComponentBase`

Inherit from `ReactiveComponentBase` instead of `ComponentBase`. Call `Observe` with a stream and a callback. The
component then:

- runs your callback through the renderer,
- calls `StateHasChanged` after it, so the page draws the new value,
- disposes the subscription when the component is disposed.

```razor
@using ReactiveUI.Primitives.Blazor.Components
@using ReactiveUI.Primitives.Signals
@inherits ReactiveComponentBase

<p>Open for @_seconds s</p>

@code {
    private long _seconds;

    protected override void OnInitialized() =>
        Observe(Signal.Every(TimeSpan.FromSeconds(1)), tick => _seconds = tick + 1);
}
```

After three seconds the component shows:

```text
<p>Open for 3 s</p>
```

There is no `Dispose` to write. The subscription ends with the component.

This `ReactiveComponentBase` has no view model. ReactiveUI's Blazor package has its own
`ReactiveComponentBase<T>`, which adds one. See [Blazor](../getting-started/installation/blazor.md).

#### When the stream fails or completes

`Observe` also takes an `onError` and an `onCompleted` callback. Each runs through the renderer, followed by
`StateHasChanged`.

```csharp
Observe(prices,
        price => _price = price,
        error => _message = $"Prices are unavailable: {error.Message}",
        () => _message = "The market is closed");
```

If you give no `onError`, the component calls `OnObservedError`. By default it throws an
`InvalidOperationException` that wraps the failure. Blazor passes that to the nearest `ErrorBoundary`: the
component that shows an error in place of its content. Override `OnObservedError` to handle every failure on the
component in one place:

```csharp
protected override void OnObservedError(Exception error) => _message = error.Message;
```

#### Choosing when to draw

Pass `false` as the last argument to skip the automatic `StateHasChanged`. Call `InvalidateAsync` when you do want
to draw. It calls `StateHasChanged` through the renderer, so it is safe from any thread.

```csharp
Observe(readings,
        reading => _buffer.Add(reading),
        onError: null,
        onCompleted: null,
        refreshAfterCallbacks: false);

Track(Signal.Every(TimeSpan.FromMilliseconds(500))
            .Subscribe(_ => InvalidateAsync()));
```

Here the page takes every reading, but draws at most twice a second.

#### Other members

- `Track(subscription)` disposes a subscription you made yourself when the component is disposed, and hands it
  back. Called after the component is disposed, it disposes the subscription straight away. It does not draw the
  component: call `StateHasChanged` yourself.
- `RendererSequencer` is a sequencer for this component's renderer. Pass it to operators such as `WitnessOn`,
  `Calm` or `Shift` when your own chain needs to run there.
- `IsDisposed` is `true` once the component is disposed.
- `Dispose(bool disposing)` is the method to override to release your own resources. Call the base method.

```csharp
protected override void OnInitialized() =>
    Track(searchText.Calm(TimeSpan.FromMilliseconds(300), RendererSequencer)
                    .Subscribe(text =>
                    {
                        _query = text;
                        StateHasChanged();
                    }));
```

`Calm` waits on `RendererSequencer`, so the callback runs through the renderer, where `StateHasChanged` is safe.

### `BlazorRendererSequencer`

`BlazorRendererSequencer` runs work through the renderer. In a component, build it from the component's
`InvokeAsync` method:

```razor
@using ReactiveUI.Primitives
@using ReactiveUI.Primitives.Blazor.Concurrency
@using ReactiveUI.Primitives.Signals
@implements IDisposable

<p>Open for @_seconds s</p>

@code {
    private long _seconds;
    private IDisposable? _clock;

    protected override void OnInitialized()
    {
        var ui = new BlazorRendererSequencer(InvokeAsync);

        _clock = Signal.Every(TimeSpan.FromSeconds(1))
                       .WitnessOn(ui)
                       .Subscribe(tick =>
                       {
                           _seconds = tick + 1;
                           StateHasChanged();
                       });
    }

    public void Dispose() => _clock?.Dispose();
}
```

Code that holds a renderer instead of a component, such as an `HtmlRenderer` that renders components to a string,
uses its `Dispatcher`: `new BlazorRendererSequencer(renderer.Dispatcher)`, or `renderer.Dispatcher.ToSequencer()`.

When scheduled work throws, the sequencer passes the exception to `UnhandledExceptionHandler`. If you set none, it
throws the exception again on a thread-pool thread, which ends the app. Set a handler that logs it:

```csharp
ui.UnhandledExceptionHandler = error => logger.LogError(error, "A stream callback failed");
```

| Member | What it is |
|---|---|
| `new BlazorRendererSequencer(invokeAsync)` | Runs work through a method such as `ComponentBase.InvokeAsync`. |
| `new BlazorRendererSequencer(dispatcher)` | Runs work through a renderer `Dispatcher`. |
| `dispatcher.ToSequencer()` | The same as `new BlazorRendererSequencer(dispatcher)`. |
| `UnhandledExceptionHandler` | Receives exceptions thrown by scheduled work. |

## Android

On Android, the main `ReactiveUI.Primitives` package has `HandlerSequencer`. It runs work through an Android
`Handler`: the object that posts work to a thread's message loop. `HandlerSequencer.Main` is ready-made for the
app's UI thread.

```csharp
using Android.App;
using Android.OS;
using Android.Widget;
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.Primitives.Signals;
using Signal = ReactiveUI.Primitives.Signals.Signal;

[Activity(Label = "Clock", MainLauncher = true)]
public sealed class ClockActivity : Activity
{
    private TextView? _label;
    private IDisposable? _clock;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        _label = new TextView(this);
        SetContentView(_label);
    }

    protected override void OnResume()
    {
        base.OnResume();

        _clock = Signal.Every(TimeSpan.FromSeconds(1))
                       .WitnessOn(HandlerSequencer.Main)
                       .Subscribe(tick => _label!.Text = $"Open for {tick + 1} s");
    }

    protected override void OnPause()
    {
        _clock?.Dispose();
        base.OnPause();
    }
}
```

Android has its own `Android.OS.Signal` class. In a file with `using Android.OS;`, a plain `Signal` does not
compile, because the compiler cannot tell the two apart. The `using Signal = ...` alias above picks this library's
`Signal`.

To run work on a background thread with its own message loop, pass that thread's handler:

```csharp
var worker = new HandlerThread("worker");
worker.Start();

var background = new HandlerSequencer(new Handler(worker.Looper!));
```

| Member | What it is |
|---|---|
| `HandlerSequencer.Main` | Runs work on the app's UI thread. |
| `new HandlerSequencer(handler)` | Runs work on the handler's thread. |
| `Handler` | The handler you passed in. |

## iOS, macOS, Mac Catalyst and tvOS

On Apple platforms, the main `ReactiveUI.Primitives` package has `NSRunloopSequencer`. `NSRunloopSequencer.Main`
runs work on the main queue, which is the UI thread. There is no constructor: `Main` is the only instance.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.Primitives.Signals;
using UIKit;

public sealed class ClockViewController : UIViewController
{
    private readonly UILabel _label = new();
    private IDisposable? _clock;

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();
        View!.AddSubview(_label);
    }

    public override void ViewWillAppear(bool animated)
    {
        base.ViewWillAppear(animated);

        _clock = Signal.Every(TimeSpan.FromSeconds(1))
                       .WitnessOn(NSRunloopSequencer.Main)
                       .Subscribe(tick => _label.Text = $"Open for {tick + 1} s");
    }

    public override void ViewWillDisappear(bool animated)
    {
        _clock?.Dispose();
        base.ViewWillDisappear(animated);
    }
}
```

On macOS with AppKit, use an `NSViewController` and an `NSTextField`. The chain is the same. Only the view code
changes: `ViewWillAppear` and `ViewWillDisappear` take no argument, and you set the label's `StringValue`.

## A UI sequencer of your own

For a UI toolkit with no package, such as a game engine that runs queued work once per frame, build the sequencer
on `DispatchSequencerState`, in `ReactiveUI.Primitives.Advanced`. It is the engine behind all eight sequencers
above, and gives yours the same batching and cancelling.

You give it three things:

- **the sequencer**, so delayed work can come back through it when due;
- **a post method**, which asks the UI thread to call the drain method once. Return `false` if the UI thread
  cannot take work yet. Call `PostDrain` when it can, to try again;
- **a drain method**, which the UI thread calls. It calls `RunDrain`, which runs the batch.

A fourth, optional, method schedules delayed work on the platform's timer. Without it, delays use a shared
thread-pool timer.

`DispatchSequencerState` is a mutable struct. Keep it in a field that is not `readonly`, and call it in place. A copy
is a separate queue.

```csharp
using ReactiveUI.Primitives.Advanced;
using ReactiveUI.Primitives.Concurrency;

var frames = new FrameSequencer();

frames.Schedule(() => Console.WriteLine("move the player"));
frames.Schedule(() => Console.WriteLine("update the score"));
IDisposable cancelled = frames.Schedule(() => Console.WriteLine("never printed"));
cancelled.Dispose();

Console.WriteLine("frame starts");
frames.RunFrame();

public sealed class FrameSequencer : ISequencer
{
    private DispatchSequencerState _state;
    private Action? _drainForNextFrame;

    public FrameSequencer() => _state = new DispatchSequencerState(this, Post, RunDrain);

    public DateTimeOffset Now => DispatchSequencerState.Now;

    public long Timestamp => DispatchSequencerState.Timestamp;

    public void Schedule(IWorkItem item) => _state.Schedule(item);

    public void Schedule(IWorkItem item, long dueTimestamp) => _state.Schedule(item, dueTimestamp);

    // The game loop calls this once per frame, on its own thread.
    public void RunFrame() => Interlocked.Exchange(ref _drainForNextFrame, null)?.Invoke();

    private bool Post(Action drain)
    {
        Volatile.Write(ref _drainForNextFrame, drain);
        return true;
    }

    private void RunDrain() => _state.RunDrain();
}
```

Output:

```text
frame starts
move the player
update the score
```

Both items ran in one batch, and the cancelled one was skipped.

In a platform's own delayed method, call `DispatchSequencerState.DelayUntil(dueTimestamp)` for how long to wait. When
the timer fires, on the UI thread, call `DispatchSequencerState.RunIfActive(item)`. It runs the item unless it was
cancelled.

| Member | What it is |
|---|---|
| `new DispatchSequencerState(sequencer, post, drain)` | An engine whose delays use the shared thread-pool timer. |
| `new DispatchSequencerState(sequencer, post, drain, scheduleDelayed)` | An engine whose delays use your method. |
| `Schedule(item)` | Queues work for the next batch. |
| `Schedule(item, dueTimestamp)` | Queues work for a batch once it is due. |
| `PostDrain()` | Asks for a batch again, if work is waiting. |
| `RunDrain()` | Runs the waiting work. Call it from the drain method. |
| `DelayUntil(dueTimestamp)` | The time left until a due timestamp. |
| `RunIfActive(item)` | Runs an item unless it was cancelled. |
| `Now` and `Timestamp` | The clocks to hand back from your sequencer. |

## The two package flavours

Each UI package also ships as a `.Reactive` package, such as `ReactiveUI.Primitives.Wpf.Reactive`. It is the same
source compiled against System.Reactive. Its sequencers are System.Reactive `IScheduler` types, in
`ReactiveUI.Primitives.Reactive.Concurrency`. The Blazor types are in `ReactiveUI.Primitives.Blazor.Reactive.Concurrency`
and `ReactiveUI.Primitives.Blazor.Reactive.Components`. On Android and Apple they come with
`ReactiveUI.Primitives.Reactive`. The type names and behaviour are the same. See
[using both libraries together](system-reactive.md#using-both-libraries-together).

## Every platform member at a glance

| Member | Platform | What it does |
|---|---|---|
| `DispatcherSequencer` | WPF | Runs work through a `Dispatcher`, at a priority. |
| `ControlSequencer` | WinForms | Runs work on a control's thread, waiting for its handle. |
| `DispatcherQueueSequencer` | WinUI | Runs work through a `DispatcherQueue`, at a priority. |
| `DispatcherQueue.ToSequencer` | WinUI | Makes a `DispatcherQueueSequencer`. |
| `AvaloniaScheduler` | Avalonia | Runs work through a `Dispatcher`, at a priority. |
| `AvaloniaScheduler.Instance` | Avalonia | The sequencer for `Dispatcher.UIThread`. |
| `MauiDispatcherSequencer` | MAUI | Runs work through an `IDispatcher`. |
| `IDispatcher.ToSequencer` | MAUI | Makes a `MauiDispatcherSequencer`. |
| `BlazorRendererSequencer` | Blazor | Runs work through the renderer. |
| `Dispatcher.ToSequencer` | Blazor | Makes a `BlazorRendererSequencer`. |
| `ReactiveComponentBase` | Blazor | A component that runs stream callbacks through the renderer and draws after them. |
| `Observe` | Blazor | Subscribes, draws after each callback, and disposes with the component. |
| `Track` | Blazor | Disposes a subscription with the component. |
| `InvalidateAsync` | Blazor | Draws the component again, from any thread. |
| `OnObservedError` | Blazor | Handles a failure that `Observe` got no `onError` for. |
| `RendererSequencer` | Blazor | The component's renderer, as a sequencer. |
| `HandlerSequencer` | Android | Runs work through a `Handler`. |
| `HandlerSequencer.Main` | Android | The sequencer for the UI thread. |
| `NSRunloopSequencer.Main` | Apple | The sequencer for the main queue. |
| `DispatchSequencerState` | Any | The batching engine for a UI sequencer of your own. |
