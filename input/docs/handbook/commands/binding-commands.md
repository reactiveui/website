# Binding

View model commands that need to be bound to view controls must implement the `ICommand` interface. View model commands are typically bound to view controls using one of the `BindCommand` overloads available in the view. Let's see an example:

```cs
// In a view
this.BindCommand(
    this.ViewModel,
    vm => vm.MyCommand,
    v => v.myControl); 
```

Here we bind `myControl` in the view to `MyCommand` from the view model. What happens next depends on any `ICreatesCommandBinding` instances registered in the [service locator](https://reactiveui.net/docs/handbook/dependency-inversion/). Normally, `myControl` in the view will be disabled whenever the command cannot execute. If the control is enabled and the user interacts with it in the expected way, the command will execute. For example, if `myControl` is a button, the user would have to click or tap on it.

> **Note:** The above example shows a naked call to `BindCommand`; however, `BindCommand` will often be called in a `WhenActivated` block:
> 
> ```cs
> this.WhenActivated(
>     d =>
>     {
>         d(this.BindCommand(
>             this.ViewModel,
>             vm => vm.MyCommand,
>             v => v.myControl));
>     });
> ```
> 
> See the [documentation](https://reactiveui.net/docs/handbook/when-activated/) on `WhenActivated` for more information.

The `BindCommand` call shown above doesn't provide any hint as to which event will trigger command execution. In this case, the default event for the control will be used, such as `Click` or `Tapped`. If the command should execute when a specific event is triggered, use an overload of `BindCommand` that takes an event name:

```cs
this.BindCommand(
    this.ViewModel,
    vm => vm.MyCommand,
    v => v.myControl,
    nameof(myControl.SomeEvent));
```

Here `myControl.SomeEvent` (and not the default event) will cause the command to execute.

> **Note:** When this `BindCommand` overload is used in a `WhenActivated` block, it is important to dispose the binding when the view gets deactivated. `BindCommand` subscribes to the given event each time the view is activated. If the binding is not disposed, the subscription to the event will not be canceled. This will lead to multiple subscriptions to the same event, and the command will execute once for each of the event subscriptions whenever the event is triggered.

Some `BindCommand` overloads also accept a parameter for the command. The parameter can be a function, an observable, or an expression that resolves to a property of the view model:

```cs
// use an observable as the source for command parameters
IObservable<int> param = ...;
this.BindCommand(
    this.ViewModel,
    vm => vm.MyCommand,
    v => v.myControl,
    param);

// use a property on the VM as a command parameter
this.BindCommand(
    this.ViewModel,
    vm => vm.MyCommand,
    v => v.myControl,
    vm => vm.SomeProperty);
```

|Platform Feature Description|WPF|Window Forms|Xamarin Android|Xamarin iOS|UWP|Xamarin Forms|
|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|
|CommandParameter binding|&#x2714;|-|-|-|&#x2714;|&#x2714;|

<details><summary>Command Execution using CommandParameter binding</summary>

CommandParameter binds automatically to `TInput` in `ReactiveCommand<TInput, Unit>`

```xml
//In the view
<Button x:Name="FeedType"
    Content="Live Feed"
    CommandParameter="LiveFeed">
</Button>
```

```cs
 //In the code-behind file
 this.WhenActivated(disposableRegistration =>
 {
    this.BindCommand(ViewModel,
    viewModel => viewModel.ProcessFeed,
    view => view.FeedType)
    .DisposeWith(disposableRegistration);
 });

 //In the ViewModel
 public class MyViewModel
 {
    public ReactiveCommand<string, Unit> ProcessFeed { get; }
   
    public MyViewModel()
    {
        //Create a ReactiveCommand that accepts an input of type string.
        ProcessFeed = ReactiveCommand.Create<string>(x => FeedProcessor(x));
    }

    private void FeedProcessor(string feedType)
    {
        //Here feedType will be "LiveFeed"
    }
 }
 ```

</details>

<details><summary>Command Execution using ViewModel Properties</summary>

```xml
//In the view
<Button x:Name="FeedType"
    Content="Live Feed">
</Button>
```

```cs
 //In the code-behind file
 this.WhenActivated(disposableRegistration =>
 {
    this.BindCommand(ViewModel,
    viewModel => viewModel.ProcessFeed,
    view => view.FeedType)
    .DisposeWith(disposableRegistration);
 });

 //In the ViewModel
 public class MyViewModel
 {
    public ReactiveCommand<Unit, Unit> ProcessFeed { get; }

    private string _feedType;
    public string FeedType
    {
        get => _feedType;
        set => this.RaiseAndSetIfChanged(ref _feedType, value);
    }

    public MyViewModel()
    {
        //Create a ReactiveCommand that accepts an input of type string.
        ProcessFeed = ReactiveCommand.Create(FeedProcessor);
    }

    private void FeedProcessor()
    {
        var feedName = FeedType;
         //Here FeedType will be the value assigned when the ViewModel was created
    }
 }
 ```

</details>


`CommandParameter` can be a good choice when the view contains multiple buttons that cannot be generated using an `ItemTemplate`. 
`CommandParameter` is a way to reuse a command without having to create additional properties on the ViewModel. Be aware that `CommandParameter` cannot be used to determine if a command can execute or not.

However, if the buttons can be generated at runtime through `ItemTemplate`, it is highly recommended to use ViewModel properties for CommandExecution.
