---
Order: 2
---
# WPF threading

WPF controls are `DispatcherObject` instances, and each belongs to one dispatcher. `CheckAccess` asks that dispatcher. `Post` queues the callback on it, and the callback runs when the dispatcher drains. A frozen `Freezable`, such as a frozen brush, belongs to no thread and has no dispatcher. `Post` runs the callback at once for it. This example calls the three members from the UI thread and a worker thread. The worker thread may not write, and its posted callback runs when the dispatcher drains.

```csharp
DispatcherViewThreadInvoker invoker = new();
WpfUploadWindow view = new();
System.Windows.Controls.ProgressBar? progressBar = view.UploadProgressBar;
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

progressBar.Dispatcher.Invoke(static () => { }, DispatcherPriority.Background);

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

The example below posts a callback to a frozen brush, which has no dispatcher. The callback runs before `Post` returns, so a frozen object never waits for a thread that does not exist.

```csharp
DispatcherViewThreadInvoker invoker = new();
SolidColorBrush brush = new(Colors.Green);
brush.Freeze();
List<string> log = [];

invoker.Post(brush, static state => ((List<string>)state!).Add(PostedText), log);

Console.WriteLine(string.Join(", ", log));
```

```text
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

WPF also refuses a write from a thread that does not own the control. The example below sets a progress bar value from a worker thread. The exception and the unchanged value show what a binding has to avoid.

```csharp
WpfUploadWindow view = new();
bool refused = false;

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
Console.WriteLine(view.UploadProgressBar.Value);
```

```text
True
0
```

The WPF module registers the dependency-property observer and the invoker. It adds no Visibility converter. Register both converters yourself with `WithConverter`. The example below shows that the converter service has no Visibility converters after `WithWpf`. It then registers both converters and checks that the observer, the invoker and the converters are all in place.

```csharp
ReactiveUIBindingBuilder? builder = RxBindingBuilder.CreateReactiveUIBindingBuilder();
_ = builder.WithCoreServices().WithWpf();

Console.WriteLine(builder.ConverterService.TypedConverters.TryGetConverter(typeof(bool), typeof(Visibility)) is null);
Console.WriteLine(builder.ConverterService.TypedConverters.TryGetConverter(typeof(Visibility), typeof(bool)) is null);

IReactiveUIBindingInstance? app = builder
    .WithConverter(new BooleanToVisibilityTypeConverter())
    .WithConverter(new VisibilityToBooleanTypeConverter())
    .BuildApp();
ViewThreadInvokers.Refresh();

Console.WriteLine(app.Current!.GetServices<ICreatesObservableForProperty>().OfType<DependencyObjectObservableForProperty>().Any());
Console.WriteLine(app.Current!.GetServices<IViewThreadInvoker>().OfType<DispatcherViewThreadInvoker>().Any());
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

`DependencyObjectObservableForProperty` observes a property when the control declares it as a dependency property. It reports an affinity of 4 for those properties and 0 for anything else. See [mechanisms](../mechanisms.md) for what an affinity is. The example below asks the provider about `Text` on a `TextBox`, once for each value of the last argument. It also asks about a property that does not exist and about a type that is not a dependency object.

```csharp
DependencyObjectObservableForProperty provider = new();

Console.WriteLine(provider.GetAffinityForObject(typeof(TextBox), TextPropertyName, false));
Console.WriteLine(provider.GetAffinityForObject(typeof(TextBox), TextPropertyName, true));
Console.WriteLine(provider.GetAffinityForObject(typeof(TextBox), "Missing", false));
Console.WriteLine(provider.GetAffinityForObject(typeof(TodoItem), "Title", false));
```

```text
4
4
0
0
```

The provider raises after each change and carries no value, so the subscriber reads the property. The example below subscribes, sets the text twice and reads the text in each notification. It sets the text a third time after the subscription ends, and that change is not recorded.

```csharp
DependencyObjectObservableForProperty provider = new();
WpfTodoWindow view = new();
ConstantExpression? expression = Expression.Constant(view.FilterTextBox);
List<string> texts = [];

using (provider.GetNotificationForProperty(view.FilterTextBox, expression, TextPropertyName, false, false).Subscribe(change => texts.Add(((TextBox)change.Sender).Text)))
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

The provider throws `ArgumentException` for a property the control has no dependency property for. The example below asks for a property called `Missing` and records the exception, so a wrong property name fails when you ask for the notification.

```csharp
DependencyObjectObservableForProperty provider = new();
WpfTodoWindow view = new();
ConstantExpression? expression = Expression.Constant(view.FilterTextBox);
bool rejected = false;

try
{
    _ = provider.GetNotificationForProperty(view.FilterTextBox, expression, "Missing", false, false);
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

`WhenChanged` reads the dependency property at build time. The example below observes `Text` on a WPF text box and sets it once. The result matches the Avalonia example, so the same code works on both platforms.

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

`ViewModel` on a WPF view is a dependency property too, so `WhenChanged` can keep `DataContext` in step with it. The example below pipes the view's `ViewModel` into its `DataContext`. The first line shows `DataContext` empty, and the second shows it holds the view model after you set it.

```csharp
WpfTodoWindow view = new();
TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());

using (view.WhenChanged(x => x.ViewModel).BindTo(view, x => x.DataContext))
{
    Console.WriteLine(view.DataContext is null);

    view.ViewModel = viewModel;

    Console.WriteLine(ReferenceEquals(view.DataContext, viewModel));
}
```

```text
True
True
```


## API reference

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`Wpf.DispatcherViewThreadInvoker`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Wpf.Shared/DispatcherViewThreadInvoker.cs) | Routes writes to a WPF object onto the thread its dispatcher owns. | Sealed class that derives from `SequencerViewThreadInvoker<DispatcherObject, DispatcherSequencer>`. | Claims `DispatcherObject`. Routes through `DispatcherSequencer.For` the object's dispatcher, at normal priority. `CheckAccess` is `true` and `Post` runs the callback at once for an object with no dispatcher, such as a frozen `Freezable`. Any other type throws `InvalidCastException`. A `null` callback throws `ArgumentNullException`. |
| [`Wpf.WpfBindingModule`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Wpf.Shared/WpfBindingModule.cs) | Registers the WPF observer and the WPF invoker with a dependency resolver. | Sealed class that implements `IModule`. `Configure(IMutableDependencyResolver resolver)`. | Registers `DependencyObjectObservableForProperty` and `DispatcherViewThreadInvoker`. It registers no Visibility converter. A `null` resolver throws `ArgumentNullException`. |
| [`WithWpf`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Wpf.Shared/Builder/WpfBindingBuilderExtensions.cs) | Adds the WPF module to the builder. | Extension method on `IReactiveUIBindingBuilder` and on `IAppBuilder`. Returns `IReactiveUIBindingBuilder`. | Returns the builder for chaining. A `null` builder throws `ArgumentNullException`. The `IAppBuilder` overload casts to `IReactiveUIBindingBuilder`. |
| [`DependencyObjectObservableForProperty.GetAffinityForObject`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Wpf.Shared/DependencyObjectObservableForProperty.cs) | Says how well the WPF observer handles a property. | `int GetAffinityForObject(Type type, string propertyName, bool beforeChanged)`. | Returns 4 for a `DependencyObject` type with a public static `{propertyName}Property` field. Otherwise 0. `beforeChanged` is ignored. |
| [`DependencyObjectObservableForProperty.GetNotificationForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Wpf.Shared/DependencyObjectObservableForProperty.cs) | Returns an observable that raises after the dependency property changes. | `IObservable<IObservedChange<object, object?>> GetNotificationForProperty(object sender, Expression expression, string propertyName, bool beforeChanged, bool suppressWarnings)`. | Each notification carries no value. Notifications are always after the change. A `null` sender throws `ArgumentNullException`. A property with no dependency property throws `ArgumentException`. A missing descriptor throws `InvalidOperationException`. |
| [`Wpf.BooleanToVisibilityHints`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Wpf.Shared/BooleanToVisibilityHints.cs) | Changes how a `bool` maps to a `Visibility`. | Flags enum. `None = 0`, `Inverse = 2`, `UseHidden = 4`. | Pass a value as the conversion hint. Combine values with bitwise or. `Inverse` swaps the mapping. `UseHidden` gives `Hidden` instead of `Collapsed`. |
| [`Wpf.BooleanToVisibilityTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Wpf.Shared/BooleanToVisibilityTypeConverter.cs) | Converts a `bool` to a `Visibility`. | Sealed class. `BindingTypeConverter<bool, Visibility>`. | `true` gives `Visible`. `false` gives `Collapsed`, or `Hidden` with `UseHidden`. The conversion always succeeds. A hint that is not a `BooleanToVisibilityHints` value counts as `None`. Affinity is 2. |
| [`Wpf.VisibilityToBooleanTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Wpf.Shared/VisibilityToBooleanTypeConverter.cs) | Converts a `Visibility` to a `bool`. | Sealed class. `BindingTypeConverter<Visibility, bool>`. | `Visible` gives `true`. `Hidden` and `Collapsed` give `false`. `Inverse` flips the result. The conversion always succeeds. Any other hint counts as `None`. Affinity is 2. |
