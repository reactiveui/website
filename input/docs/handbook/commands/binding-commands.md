# Binding

Once you have created a command and exposed it from your view model, the next logical step is to consume it from your view. The most common means of achieving this is via the `BindCommand` method. This method - of which there are several overloads - is responsible for tying any source `ICommand` to a target control. Typical usage looks like this:

```cs
// in a view
this.BindCommand(
    this.ViewModel,
    x => x.MyCommand,
    x => x.myControl);
```

Here we bind the `myControl` control to the `MyCommand` command exposed by our view model. What happens next is contingent upon any `ICreatesCommandBinding` instances registered in the [service locator](http://docs.reactiveui.net/en/user-guide/dependency-injection/index.html). However, normally `myControl` will be disabled whenever the command is unavailable. In addition, performing some default action against `myControl` will execute the command. For example, if `myControl` is a button, the required action would be a click (or tap).

> **Note** The above example shows a naked call to `BindCommand`, but it will often be performed inside a `WhenActivated` block:
> 
> ```cs
> this.WhenActivated(
>     d =>
>     {
>         d(this.BindCommand(
>             this.ViewModel,
>             x => x.MyCommand,
>             x => x.myControl));
>     });
> ```
> 
> Please see [the documentation on `WhenActivated`](http://docs.reactiveui.net/en/user-guide/when-activated/index.html) for more information.

The form of `BindCommand` demonstrated above does not provide any hint as to which event instigates command execution. Hence, a default event will be used (such as `Click` or `Tapped`). If, on the other hand, you want to tie your command's execution to some event other than the default, you can use an overload of `BindCommand` that takes an event name:

```cs
this.BindCommand(
    this.ViewModel,
    x => x.MyCommand,
    x => x.myControl,
    nameof(myControl.SomeEvent));
```

Here, the `SomeEvent` on `myControl` will be used to trigger command execution instead of the default event.

> **Note** When using this overload inside `WhenActivated`, it's important to dispose the binding when deactivating the view. `BindCommand` will subscribe to the given event each time the view is activated, if the binding is not disposed it will not unsubscribe from the event. This will lead to multiple subscriptions to the event, which will make the command execute once for each of the event subscriptions.

Finally, `BindCommand` also provides overloads that allow you to specify a parameter with which to execute the command. The parameter can be provided as a function, an observable, or even an expression that resolves a property on the view model:

```cs
// pass through an execution count as the command parameter
var count = 0;
this.BindCommand(
    this.ViewModel,
    x => x.MyCommand,
    x => x.myControl,
    () => count++);

// use an observable as the source for command parameters
IObservable<int> param = ...;
this.BindCommand(
    this.ViewModel,
    x => x.MyCommand,
    x => x.myControl,
    param);

// use a property on the VM as a command parameter
this.BindCommand(
    this.ViewModel,
    x => x.MyCommand,
    x => x.myControl,
    x => x.SomeProperty);
```