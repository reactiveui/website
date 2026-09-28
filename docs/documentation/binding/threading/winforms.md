---
Order: 3
---
# WinForms threading

A WinForms `Control` has an owning thread only after it has a window handle. Before that, `InvokeRequired` is `false`, so the invoker lets any thread write and runs a posted callback at once. The first example below uses a control that has no handle. A worker thread may write, and the posted callback runs before `Post` returns. After the handle exists, `CheckAccess` is `false` on a worker thread and `Post` waits for the message loop.

```csharp
ControlViewThreadInvoker invoker = new();
using WinFormsUploadForm view = new();
List<string> log = [];
bool accessFromWorker = false;

Thread? worker = new Thread(() => accessFromWorker = invoker.CheckAccess(view.UploadProgressBar));
worker.Start();
worker.Join();
invoker.Post(view.UploadProgressBar, static state => ((List<string>)state!).Add(PostedText), log);

Console.WriteLine(accessFromWorker);
Console.WriteLine(string.Join(", ", log));
```

```text
True
posted
```

This example creates the handle first. The worker thread may not write, and the posted callback waits until the message loop runs.

```csharp
ControlViewThreadInvoker invoker = new();
using WinFormsUploadForm view = new();
System.Windows.Forms.ProgressBar? progressBar = view.UploadProgressBar;
_ = progressBar.Handle;
List<string> log = [];
object unclaimed = new();
bool accessFromWorker = true;

Thread? worker = new Thread(() =>
{
    accessFromWorker = invoker.CheckAccess(progressBar);
    invoker.Post(progressBar, static state => ((List<string>)state!).Add(PostedText), log);
});
worker.Start();
worker.Join();

Console.WriteLine(invoker.Claims(progressBar));
Console.WriteLine(invoker.Claims(unclaimed));
Console.WriteLine(invoker.CheckAccess(progressBar));
Console.WriteLine(accessFromWorker);
Console.WriteLine(log.Count);

Application.DoEvents();

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

A `null` callback throws `ArgumentNullException`, as it does in every platform invoker. The example below passes `null` to `Post` and records the exception. The invoker rejects the missing callback at once instead of failing later on the UI thread.

```csharp
ControlViewThreadInvoker invoker = new();
using WinFormsUploadForm view = new();
bool rejected = false;

try
{
    invoker.Post(view.UploadProgressBar, null!, null);
}
catch (ArgumentNullException)
{
    rejected = true;
}

Console.WriteLine(rejected);
```

```text
True
```

When `Control.CheckForIllegalCrossThreadCalls` is `true`, WinForms refuses a write from a thread that does not own the control. The example below turns the check on and sets a progress bar value from a worker thread. The exception shows the call that the invoker keeps a binding from making.

```csharp
using WinFormsUploadForm view = new();
_ = view.UploadProgressBar.Handle;
bool refused = false;
Control.CheckForIllegalCrossThreadCalls = true;

Thread? worker = new Thread(() =>
{
    try
    {
        view.UploadProgressBar.Value = FullPercent;
    }
    catch (InvalidOperationException)
    {
        refused = true;
    }
});
worker.Start();
worker.Join();

Console.WriteLine(refused);
```

```text
True
```

The WinForms module registers the event-based observer and the control invoker. `WinFormsCreatesObservableForProperty` follows a public `{Name}Changed` event on a component, such as `TextChanged` for `Text`. It reports an affinity of 8 for a property that has one, and 0 otherwise. Read the [mechanisms](../mechanisms.md) page for how the affinity picks between observers. The example below builds an app with the WinForms module and checks that the observer and the invoker are registered.

```csharp
IReactiveUIBindingInstance? app = RxBindingBuilder.CreateReactiveUIBindingBuilder()
    .WithCoreServices()
    .WithWinForms()
    .BuildApp();
ViewThreadInvokers.Refresh();

Console.WriteLine(app.Current!.GetServices<ICreatesObservableForProperty>().OfType<WinFormsCreatesObservableForProperty>().Any());
Console.WriteLine(app.Current!.GetServices<IViewThreadInvoker>().OfType<ControlViewThreadInvoker>().Any());
```

```text
True
True
```

The affinity is 8 for `Text` on a `TextBox`, `Checked` on a `CheckBox`, and `DataSource`, `SelectedValue` and `SelectedIndex` on a `ListBox`. It is 0 for `SelectedItem`, which has no `SelectedItemChanged` event, so bind `SelectedValue` or `SelectedIndex` instead. The observer raises only after a change, so it returns 0 when you ask for a before-change notification. The example below asks the observer about several properties, one before-change request, a missing property and a view-model type. The numbers show which ones it accepts.

```csharp
WinFormsCreatesObservableForProperty observer = new();

Console.WriteLine(observer.GetAffinityForObject(typeof(TextBox), TextPropertyName, false));
Console.WriteLine(observer.GetAffinityForObject(typeof(CheckBox), "Checked", false));
Console.WriteLine(observer.GetAffinityForObject(typeof(ListBox), "DataSource", false));
Console.WriteLine(observer.GetAffinityForObject(typeof(ListBox), "SelectedValue", false));
Console.WriteLine(observer.GetAffinityForObject(typeof(ListBox), "SelectedIndex", false));
Console.WriteLine(observer.GetAffinityForObject(typeof(ListBox), "SelectedItem", false));
Console.WriteLine(observer.GetAffinityForObject(typeof(TextBox), TextPropertyName, true));
Console.WriteLine(observer.GetAffinityForObject(typeof(TextBox), "Missing", false));
Console.WriteLine(observer.GetAffinityForObject(typeof(TodoItem), "Title", false));
```

```text
8
8
8
8
8
0
0
0
0
```

The observer raises after each `TextChanged` and carries no value, so the subscriber reads the property. This example subscribes through the observer and reads the text after each change. The change after the subscription ends is not recorded.

```csharp
WinFormsCreatesObservableForProperty observer = new();
using WinFormsTodoForm view = new();
ConstantExpression? expression = Expression.Constant(view.FilterTextBox);
List<string> texts = [];

using (observer.GetNotificationForProperty(view.FilterTextBox, expression, TextPropertyName, false, false).Subscribe(change => texts.Add(((TextBox)change.Sender).Text)))
{
    view.FilterTextBox.Text = CarFilter;
    view.FilterTextBox.Text = CarsFilter;
}

view.FilterTextBox.Text = CarFilter;

Console.WriteLine(string.Join(", ", texts));
```

```text
car, cars
```

The observer throws `ArgumentException` for a property that has no changed event. The example below asks for a property called `Missing` and records the exception, so a wrong property name fails when you ask for the notification.

```csharp
WinFormsCreatesObservableForProperty observer = new();
using WinFormsTodoForm view = new();
ConstantExpression? expression = Expression.Constant(view.FilterTextBox);
bool rejected = false;

try
{
    _ = observer.GetNotificationForProperty(view.FilterTextBox, expression, "Missing", false, false);
}
catch (ArgumentException)
{
    rejected = true;
}

Console.WriteLine(rejected);
```

```text
True
```

`WhenChanged` on a WinForms control reads the `TextChanged` event at build time, so it needs no registered observer. The example below observes `Text` on a WinForms text box and sets it once. The first value is the empty text the box held when the observation started.

```csharp
using WinFormsTodoForm view = new();
List<string> texts = [];

using (view.FilterTextBox.WhenChanged(x => x.Text).Subscribe(texts.Add))
{
    view.FilterTextBox.Text = CarFilter;
}

Console.WriteLine(string.Join(", ", texts));
```

```text
, car
```


## API reference

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`WinForms.ControlViewThreadInvoker`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.WinForms.Shared/ControlViewThreadInvoker.cs) | Routes writes to a WinForms control onto the thread that created its handle. | Sealed class that derives from `SequencerViewThreadInvoker<Control, ControlSequencer>`. | Claims `Control`. Routes through `ControlSequencer.For` the control while `InvokeRequired` is `true`. Otherwise, including while the control has no handle, `CheckAccess` is `true` and `Post` runs the callback at once. Any other type throws `InvalidCastException`. A `null` callback throws `ArgumentNullException`. |
| [`WinForms.WinFormsBindingModule`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.WinForms.Shared/WinFormsBindingModule.cs) | Registers the WinForms observer and the control invoker with a dependency resolver. | Sealed class that implements `IModule`. `Configure(IMutableDependencyResolver resolver)`. | Registers `WinFormsCreatesObservableForProperty` and `ControlViewThreadInvoker`. A `null` resolver throws `ArgumentNullException`. |
| [`WithWinForms`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.WinForms.Shared/Builder/WinFormsBindingBuilderExtensions.cs) | Adds the WinForms module to the builder. | Extension method on `IReactiveUIBindingBuilder` and on `IAppBuilder`. Returns `IReactiveUIBindingBuilder`. | Returns the builder for chaining. A `null` builder throws `ArgumentNullException`. The `IAppBuilder` overload casts to `IReactiveUIBindingBuilder`. |
| [`WinFormsCreatesObservableForProperty.GetAffinityForObject`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.WinForms.Shared/WinFormsCreatesObservableForProperty.cs) | Says how well the WinForms observer handles a property. | `int GetAffinityForObject(Type type, string propertyName, bool beforeChanged)`. | Returns 8 for a `Component` type with a public instance `{propertyName}Changed` event. Otherwise 0. Returns 0 when `beforeChanged` is `true`. |
| [`WinFormsCreatesObservableForProperty.GetNotificationForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.WinForms.Shared/WinFormsCreatesObservableForProperty.cs) | Returns an observable that raises when the control raises its `{propertyName}Changed` event. | `IObservable<IObservedChange<object, object?>> GetNotificationForProperty(object sender, Expression expression, string propertyName, bool beforeChanged, bool suppressWarnings)`. | Each notification carries no value. The handler is added on subscription and removed on disposal. A `null` sender throws `ArgumentNullException`. A type with no such event throws `ArgumentException`. The observer uses reflection, so it carries a trimming warning. |
