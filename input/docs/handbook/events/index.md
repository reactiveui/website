Install the `ReactiveMarbles.ObservableEvents.SourceGenerator` package into your application. See <a href="https://reactiveui.net/docs/getting-started/installation/">installation guide</a> for more info. You can use this events package standalone, without any reference to ReactiveUI. `ReactiveMarbles.ObservableEvents.SourceGenerator` will always be a separate package that has no dependancy on the `ReactiveUI` package.

This package uses SourceGenerator to generate the observables for events within the platform.  `ReactiveMarbles.ObservableEvents.SourceGenerator` has now replaced the `ReactiveUI.Events.*` packages. Don't use `EventHandlers` ever, use the generated `Observable.FromEventPattern` versions. Combine multiple `Observable.FromEventPattern`together to get amazing composition. Remember to [dispose of your subscriptions](https://reactiveui.net/docs/reactive-programming/#lifecycle) using the features provided by the Reactive Extensions.

```csharp
var codes = new[]
{
    Key.Up,
    Key.Up,
    Key.Down,
    Key.Down,
    Key.Left,
    Key.Right,
    Key.Left,
    Key.Right,
    Key.A,
    Key.B
};

// convert the array into an sequence
var koanmi = codes.ToObservable();

this.Events().KeyUp

    // we want the keycode
    .Select(x => x.Key)
    .Do(key => Debug.WriteLine($"{key} was pressed."))

    // get the last ten keys
    .Window(10)

    // compare to known konami code sequence
    .SelectMany(x => x.SequenceEqual(koanmi))
    .Do(isMatch => Debug.WriteLine(isMatch))

    // where we match
    .Where(x => x)
    .Do(x => Debug.WriteLine("Konami sequence"))
    .Subscribe(y => { });
```

<iframe width="560" height="315" src="https://www.youtube.com/embed/tNn-7fen3DA" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

## Using events with WhenActivated

If you are reacting to events emitted by the view and referencing the view model in your observable sequence, remember to dispose your subscriptions. If your view model outlives your view or vice versa, there is a potential for a memory leak, and `WhenActivated` helps you to avoid that. See [WhenActivated documentation](/docs/handbook/when-activated) for more info.

```cs
InitializeComponent();
this.WhenActivated(disposables =>
{
    RefreshButton
      // observe button click events
      // namespace: System.Windows.Controls.Primitives
      .Events().Click
      // transform arguments
      .Select(args => Unit.Default)
      // invoke command when button is clicked
      .InvokeCommand(this, x => x.ViewModel.Refresh)
      // dispose subscription when the view
      // gets deactivated.
      .DisposeWith(disposables);
});
```

## Prefer ObservableEvents over XAML behaviors

Although XAML behaviors is a nice technique which allows you to bind to any event exposed by a control, it has several drawbacks. First, its syntax is quite verbose. Second, you lose intellisense when typing the event name. Third, if you'd like to modify the way how your view model reacts to an event, you need to write a new action and/or behavior. Consider the following example which uses UWP XAML behaviors:

```xml
<interactivity:Interaction.Behaviors>
    <core:EventTriggerBehavior EventName="Tapped">
        <core:InvokeCommandAction Command="{x:Bind ViewModel.Refresh}" />
    </core:EventTriggerBehavior>
</interactivity:Interaction.Behaviors>
```

With `ReactiveUI.Events` all events are strongly typed. This means your IDE will help you by suggesting available events.

```cs
this.Events().Tapped
    // Use any of reactive extensions operators here!
    .Select(args => Unit.Default)
    .InvokeCommand(this, x => x.ViewModel.Refresh);
```

## How do I convert my own C# events into Observables?

[Reactive Extensions for .NET](https://github.com/dotnet/reactive) provide three approaches how you can do this. The first one is using `Observable.FromEventPattern`.

```cs
Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
  handler => PasswordBox.PasswordChanged += handler,
  handler => PasswordBox.PasswordChanged -= handler) // Got IObservable here!
```

Another option is to use an overload which accepts a string.

```cs
Observable.FromEventPattern(PasswordBox, nameof(PasswordBox.PasswordChanged))
```

The last option is `Observable.FromEvent` which works with any event delegate type.

```cs
Observable.FromEvent<KeyPressEventHandler, KeyPressEventArgs>(
  handler => {
    KeyPressEventHandler press = (sender, e) => handler(e);
    return press;
  }, 
  handler => KeyPress += handler,
  handler => KeyPress -= handler)
```

See [Reactive Extensions documentation](https://reactivex.io/documentation/operators/from.html) for more info.
