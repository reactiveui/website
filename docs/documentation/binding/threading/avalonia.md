---
Order: 1
---
# Avalonia threading

The `ReactiveUI.Binding.Avalonia` package adds `WithAvalonia` and `AvaloniaBindingModule`. The module registers the invoker from the walkthrough, an observer for registered Avalonia properties, and a command binder that uses a control's `Command` property or a routed event. None of them reflect over members, so the package is safe to trim and to publish ahead of time. A progress report from a pool thread waits for the UI thread. The example below binds an upload's progress to a progress bar. The upload reports progress from pool threads, and the invoker moves each write to the UI thread.

```csharp
StorageBrowserViewModel viewModel = new(InMemoryObjectStorage.CreateSeeded());
await viewModel.LoadBucketsAsync();
viewModel.SelectedBucket = viewModel.Buckets[0];
AvaloniaUploadView view = new() { ViewModel = viewModel };
UploadRequest photo = new(OffsitePhotoName, OffsitePhotoSize, JpegType);

using (viewModel.BindOneWay(view, x => x.UploadPercent, v => v.UploadProgressBar.Value))
{
    Console.WriteLine(view.UploadProgressBar.Value);

    await viewModel.UploadAsync(photo);

    Console.WriteLine(view.UploadProgressBar.Value);
}
```

```text
0
100
```

An Avalonia control raises `PropertyChanged` with the name of the property. That is the notification the engine observes, so `WhenChanged` works on Avalonia controls without more setup. The example below subscribes to the control's own `PropertyChanged` event and prints the property name it raises. It shows the notification the engine relies on.

```csharp
AvaloniaTodoView view = new();
INotifyPropertyChanged notifying = (INotifyPropertyChanged)view.FilterTextBox;
notifying.PropertyChanged += static (_, e) => Console.WriteLine(e.PropertyName);

view.FilterTextBox.Text = CarFilter;
```

```text
Text
```

`WhenChanged` observes that event. The example below observes `Text` and sets the text once. The first value is the empty text the box held when the observation started, and the second is the new text.

```csharp
AvaloniaTodoView view = new();
List<string?> texts = [];

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
| [`Avalonia.AvaloniaViewThreadInvoker`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Avalonia.Shared/AvaloniaViewThreadInvoker.cs) | Routes writes to an Avalonia object onto the thread its dispatcher owns. | Sealed class that derives from `SequencerViewThreadInvoker<AvaloniaObject, AvaloniaScheduler>`. | Claims `AvaloniaObject`. Routes through `AvaloniaScheduler.For` the object's dispatcher, at background priority. Every Avalonia object has a dispatcher. Any other type throws `InvalidCastException`. A `null` callback throws `ArgumentNullException`. |
| [`Avalonia.AvaloniaBindingModule`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Avalonia.Shared/AvaloniaBindingModule.cs) | Registers the Avalonia observer, command binder and invoker with a dependency resolver. | Sealed class that implements `IModule`. `Configure(IMutableDependencyResolver resolver)`. | Registers `AvaloniaObjectObservableForProperty`, `AvaloniaCreatesCommandBinding` and `AvaloniaViewThreadInvoker.Instance`. A `null` resolver throws `ArgumentNullException`. |
| [`WithAvalonia`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Avalonia.Shared/Builder/AvaloniaBindingBuilderExtensions.cs) | Adds the Avalonia module to the builder. | Extension method on `IReactiveUIBindingBuilder` and on `IAppBuilder`. Returns `IReactiveUIBindingBuilder`. | Returns the builder for chaining. A `null` builder throws `ArgumentNullException`. The `IAppBuilder` overload casts to `IReactiveUIBindingBuilder`. |
| [`AvaloniaObjectObservableForProperty`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Avalonia.Shared/AvaloniaObjectObservableForProperty.cs) | Observes a registered Avalonia property. | Sealed class that implements `ICreatesObservableForProperty`. | `GetAffinityForObject` returns 4 for an `AvaloniaObject` type that registers an Avalonia property by that name, otherwise 0. Each notification carries the new value and arrives after the change. A `null` sender throws `ArgumentNullException`, a sender that is not an `AvaloniaObject` throws `ArgumentException`, and an unregistered property throws `MissingMemberException`. |
| [`AvaloniaCreatesCommandBinding`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Avalonia.Shared/AvaloniaCreatesCommandBinding.cs) | Binds a command to an Avalonia input element. | Sealed class that implements `ICreatesCommandBinding`. | Scores 10 for an `ICommandSource` bound without an event and sets its `Command` and `CommandParameter`. Scores 6 for any input element bound through a named routed event, which executes the command and keeps `IsEnabled` in step with `CanExecute`. Other types score 0. |
