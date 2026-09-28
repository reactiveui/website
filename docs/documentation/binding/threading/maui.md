---
Order: 4
---
# MAUI threading

A MAUI `BindableObject` reads its dispatcher from `BindableObject.Dispatcher`. MAUI throws `InvalidOperationException` when it finds no dispatcher, as it does in a view's unit test. The MAUI invoker catches that and treats the object as having no owning thread. `CheckAccess` then returns `true` and `Post` runs the callback at once. The example below posts a callback to a control with no dispatcher, as in a unit test. The callback runs before `Post` returns.

```csharp
DispatcherViewThreadInvoker invoker = new();
StorageBrowserView view = new();
List<string> log = [];

invoker.Post(view.UploadProgressBar, static state => ((List<string>)state!).Add(PostedText), log);

Console.WriteLine(string.Join(", ", log));
```

```text
posted
```

The three members work like the other platforms. With no dispatcher, as in a unit test, the calling thread owns every control, so `CheckAccess` is `true` even on a worker thread. The example below calls all three members and prints the answers. The `true` from the worker thread is the difference from WPF and WinForms.

```csharp
DispatcherViewThreadInvoker invoker = new();
StorageBrowserView view = new();
ProgressBar progressBar = view.UploadProgressBar;
List<string> log = [];
object unclaimed = new();
bool accessFromWorker = false;

Thread worker = new Thread(() => accessFromWorker = invoker.CheckAccess(progressBar));
worker.Start();
worker.Join();
invoker.Post(progressBar, static state => ((List<string>)state!).Add(PostedText), log);

Console.WriteLine(invoker.Claims(progressBar));
Console.WriteLine(invoker.Claims(unclaimed));
Console.WriteLine(invoker.CheckAccess(progressBar));
Console.WriteLine(accessFromWorker);
Console.WriteLine(string.Join(", ", log));
```

```text
True
False
True
True
posted
```

You can also call the members through the `IViewThreadInvoker` type. That is how you call any platform's invoker, or one you write. The method below takes the interface, asks whether the invoker claims a progress bar, whether the thread may write and posts a callback. It works the same for every platform.

```csharp
public static void CallInvokerThroughInterface(IViewThreadInvoker invoker)
{
    StorageBrowserView view = new();
    ProgressBar progressBar = view.UploadProgressBar;
    List<string> log = [];

    bool claimed = invoker.Claims(progressBar);
    bool mayWrite = invoker.CheckAccess(progressBar);
    invoker.Post(progressBar, static state => ((List<string>)state!).Add(PostedText), log);

    Console.WriteLine(claimed);
    Console.WriteLine(mayWrite);
    Console.WriteLine(string.Join(", ", log));
}
```

```text
True
True
posted
```

A MAUI `BindableObject` raises `PropertyChanged`, so `WhenChanged` works on a MAUI entry with no module. The example below observes the filter entry's `Text` and sets it once. The first value is the empty text the entry held when the observation started.

```csharp
MauiTodoPage view = new();
List<string?> texts = [];

using (view.FilterEntry.WhenChanged(x => x.Text).Subscribe(texts.Add))
{
    view.FilterEntry.Text = CarFilter;
}

Console.WriteLine(string.Join(", ", texts));
```

```text
, car
```


## API reference

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`Maui.DispatcherViewThreadInvoker`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Maui.Shared/DispatcherViewThreadInvoker.cs) | Routes writes to a MAUI object through the dispatcher it carries. | Sealed class that derives from `SequencerViewThreadInvoker<BindableObject, MauiDispatcherSequencer>`. | Claims `BindableObject`. Routes through `MauiDispatcherSequencer.For` the object's dispatcher, so `CheckAccess` is `false` only when the dispatcher requires a dispatch. With no dispatcher, `CheckAccess` is `true` and `Post` runs the callback at once. Any other type throws `InvalidCastException`. A `null` callback throws `ArgumentNullException`. |
| [`Maui.MauiBindingModule`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Maui.Shared/MauiBindingModule.cs) | Registers the MAUI invoker and the two Visibility converters with a dependency resolver. | Sealed class that implements `IModule`. `Configure(IMutableDependencyResolver resolver)`. | The converters go to the resolver only. `ImportFrom` copies them into a converter service. The WinUI build also registers a dependency-property observer. A `null` resolver throws `ArgumentNullException`. |
| [`WithMaui`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Maui.Shared/Builder/MauiBindingBuilderExtensions.cs) | Adds the MAUI module to the builder. | Extension method on `IReactiveUIBindingBuilder` and on `IAppBuilder`. Returns `IReactiveUIBindingBuilder`. | Returns the builder for chaining. A `null` builder throws `ArgumentNullException`. The `IAppBuilder` overload casts to `IReactiveUIBindingBuilder`. |
| [`Maui.BooleanToVisibilityHints`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Maui.Shared/BooleanToVisibilityHints.cs) | Changes how a `bool` maps to a `Visibility`. | Flags enum. `None = 0`, `Inverse = 2`, `UseHidden = 4`. | Same values as the WPF enum. `UseHidden` is ignored on WinUI, which has no `Hidden`. |
| [`Maui.BooleanToVisibilityTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Maui.Shared/BooleanToVisibilityTypeConverter.cs) | Converts a `bool` to a MAUI `Visibility`. | Sealed class. `BindingTypeConverter<bool, Visibility>`. | Same rules as the WPF converter. The conversion always succeeds. A hint that is not a `BooleanToVisibilityHints` value counts as `None`. Affinity is 2. |
| [`Maui.VisibilityToBooleanTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Maui.Shared/VisibilityToBooleanTypeConverter.cs) | Converts a MAUI `Visibility` to a `bool`. | Sealed class. `BindingTypeConverter<Visibility, bool>`. | Only `Visible` gives `true`. `Inverse` flips the result. The conversion always succeeds. Any other hint counts as `None`. Affinity is 2. |
