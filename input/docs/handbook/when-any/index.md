In interactive UI applications, state is continually changing in response to user actions and application events. ReactiveUI enables you to express changes to application state as streams of values and combine and manipulate them using the powerful Reactive Extensions library.

The motivation is intuitive enough when you think about it. It's not hard to imagine that changes to a property can be considered events - that's how `INotifyPropertyChanged` works. From there, the same argument for using Rx over events applies. In the context of MVVM application design specifically, modelling property changes as observables leads to several advantages:

- The logic of an application can be defined in terms of changes to properties
- This logic can be composed and expressed declaratively, using the power of Rx operators
- Concepts like time and asynchronicity become easier to reason about, due to their first-class treatment in an Observable context.

ReactiveUI provides several variants of `WhenAny` to help you work with properties as an observable stream.

# What is WhenAny

`WhenAny` is a set of extension methods starting the prefix `WhenAny` that allow you to get notifications when property on objects change.

You can think of the `WhenAny` set of extension methods notifying you when one or many property values have changed.

`WhenAny` supports multiple property types including `INotifyPropertyChanged`, `DependencyProperty` and `BindableProperty`. 

It will check the property for support for each of those property types, and when you `Subscribe()` it will subscribe to the events offered by the applicable property notification mechanism.

`WhenAny` by default is just a wrapper around these property notification events, and won't store any values before a `Subscribe`. You can use techniques such as `Publish`, `Replay()` to get it to store these values.

You can also wrap the `WhenAny` in a `Observable.Defer` to avoid the value being calculated until a `Subscribe` has happened. This is useful for `ObservableAsPropertyHelper` when you using the defer feature.

# Basic syntax

The following examples demonstrate simple uses of `WhenAnyValue`, the `WhenAny` variant you are likely to use most frequently.

### Watching single property

This returns an observable that yields the current value of Foo each time it changes:

```cs
this.WhenAnyValue(x => x.Foo)
```

### Watching a number of properties

This returns an observable that yields a new `Color` with the latest RGB values each time any of the properties change. The final parameter is a selector describing how to combine the three observed properties:

```cs
this.WhenAnyValue(x => x.Red, x => x.Green, x => x.Blue, 
                  (r, g, b) => new Color(r, g, b));
```

### Watching a nested property

`WhenAny` variants can observe nested properties for changes, too:

```cs
this.WhenAnyValue(x => x.Foo.Bar.Baz);
```

# Usage

Below are some typical usages of the observables returned by the `WhenAny` variants:

### Subscribing

You can `Subscribe` to the observable returned by the `WhenAny` variant and get notifications whenever the value changes.

```cs
this.WhenAnyValue(x => x.SearchText)
    .Subscribe(x => Console.WriteLine(x));
```

This will subscribe to whenever the current object's `SearchText` is changed and print the value to the `Console.WriteLine` method.

### Exposing 'calculated' properties

In general, using `Subscribe` on a `WhenAny` observable (or any observable, for that matter) just to set a property is likely a code smell. Idiomatically, the `ToProperty` operator is used to create a 'read-only' calculated property that can be exposed to the rest of your application, only settable by the `WhenAny` chain that preceded it:

```cs
this.WhenAnyValue(x => x.SearchText, x => x.Length, (text, length) => text + " (" + length + ")")
    .ToProperty(this, x => x.SearchTextLength, out _searchTextLength);
```

This will set a (an [ObservableAsPropertyHelper](../oaph/) property) field called `_searchTextLength` which you then expose via the `SearchTextLength` property. `ObservableAsPropertyHelper` properties cannot be mutated directly, but are instead calculated via the `WhenAnyValue` selector lambda.

See the [ObservableAsPropertyHelper](../oaph/) section for more information on this pattern.

### `ReactiveCommand.CanExecute` observable

`WhenAny` can be used for providing logic if a `ReactiveCommand` can be executed or not.

```cs
var canCreateUser = this.WhenAnyValue(
    x => x.Username, x => x.Password, 
    (user, pass) => 
        !string.IsNullOrWhiteSpace(user) && 
        !string.IsNullOrWhiteSpace(pass) && 
        user.Length >= 3 && 
        pass.Length >= 8)
    .DistinctUntilChanged();

CreateUserCommand = ReactiveCommand.CreateFromTask(CreateUser, canCreateUser); 
```

Here `WhenAnyValue` is used to observe the changing values of the `Username` and `Password` fields, and the selector will determine if the `CreateUserCommand` can be executed, preventing the user from executing the command until the validation conditions are met.

### Invoking commands

[Commands](./commands) are often bound to buttons or controls in the view that can be triggered by the user. You can also trigger commands when a property value has changed.

```cs
// In the ViewModel.
this.WhenAnyValue(x => x.SearchText)
    .Where(x => !String.IsNullOrWhiteSpace(x))
    .Throttle(TimeSpan.FromSeconds(.25))
    .InvokeCommand(SearchCommand);

// In the View.
this.Bind(ViewModel, vm => vm.SearchText, v => v.SearchTextField.Text);
```

Above whenever the `SearchText` property has changed, and a quarter second has passed since the last change, the `InvokeCommand` will cause the `SearchCommand` to be invoked.

The view will Bind to the `SearchText` property which will trigger the command automatically.

### Performing view-specific transforms as an input to `BindTo`

Ideally, controls on your view bind directly to properties on your view model. In cases where you need to convert a viewmodel value to a view-specific value (e.g. `bool` to `Visibility`), you should register a `BindingConverter`. Still, you may come across a situation in which you want to perform a transformation in the view directly. Here, we observe the `ShowToolTip` property of the view model, transform the `true`/`false` values to `1` and `0` respectively, then bind the result to the `ToolTipLabel`'s alpha property. 

```cs
// In the View.
ViewModel.WhenAnyValue(x => x.ShowToolTip)
         .Select(show => show ? 1f : 0f)
         .BindTo(this, x => x.ToolTipLabel.Alpha);
```

The preferable option is to use the `OneWayBind` property to perform the binding.

```cs
this.OneWayBind(this.ViewModel, vm => vm.ShowToolTip, view => view.ToolTipLabel.Alpha, show => show ? 1f : 0f);
```

# Variants of `WhenAny`
 
Several variants of `WhenAny` exist, suited for different scenarios.

### WhenAny

`WhenAny` allows you to get the Sender and the Expression passed into the `WhenAny`. This is useful for scenarios where the `Sender` is important such as View's where you need to know the Control which invoked the Property Change.

`WhenAny` will pass a `ObservedChange` object which exposes the following properties:

* `Value` - the updated value
* `Sender` - the object whose has property changed 
* `Expression` - the expression that changed. Not needed often for external users.


```cs
this.WhenAny(x => x.ComboBox.SelectedItem).Subscribe(x => Console.WriteLine($"The {x.Sender} changed value to {x.Value}"));
```

Above, we write out the Sender and the new property value of the `SelectedItem`.

`WhenAnyValue` should be preferred over `WhenAny` where possible and you don't need to know the `Sender` or the `Expression`.

### WhenAnyObservable

`WhenAnyObservable` observes one or multiple observables and provides the latest observable value, handles automatic subscriptions of the new observable and disposal of previous observables.  `WhenAnyObservable` by default will be a lazy subscription, this means you will not get a value until you subscribe.

```csharp
public class MyViewModel
{
    [Reactive]
    public Document Document { get; set; }

    public MyViewModel()
    {
      this.WhenAnyObservable(x => x.Document.IsSaved).Subscribe(x => Console.WriteLine($"Document Saved: {x}"));
    }
}

public class Document
{
    public IObservable<bool> IsSaved { get; }
}
```

Above whenever the document is saved, it will print the value from the `IsSaved` observable.  It will automatically unsubscribed and re-subscribe when the `Document` property is changed.

# Additional Considerations

### Property Changed Notifications needed
Using `WhenAny` variants is fairly straightforward. However, there are a few aspects of their behaviour that are worth highlighting.

`WhenAny` variants support a wide range of property changed notifications. For example it can support `INotifyPropertyChanged` for your view models, `DependencyProperty` on windows based XAML platforms, `NSObject` property changed notifications on Apple. To get value changed notifications your object must implement one of these known property changed notification mechanisms. 

If one of these isn't supported you will only get the initial value of the property and won't have any notifications of updates. Additionally, a warning will be issued at run time (ensure you have registered a service for `ILogger` to see this).

Commonly on your view model you will use `INotifyPropertyChanged` which the `ReactiveObject` supports, where you'll commonly use ReactiveUI's `RaiseAndSetIfChanged` or raise the standard `INotifyPropertyChanged` events

### `WhenAny` has cold observable and behavioural semantics

`WhenAny` is a purely cold Observable, which eventually directly connects to UI component events. For events such as `DependencyProperties`, this could potentially be a minor place to optimize, via `Publish`. When chaining to `ToProperty` (another cold operator), the target `ObservableAsPropertyHelper` must be read (`.Value`) or observed (e.g. used in a binding or used as part of another `WhenAny` with a subscription), for any part of the chain to execute. 

Additionally, `WhenAny` always provides you with the current value as soon as you subscribe to it - in this sense it is effectively a `BehaviorSubject`.

### `WhenAny` will not propagate `NullReferenceException`s within the watched expression

`WhenAny` will only send notifications if reading the given expression would not throw a `NullReferenceException`. Consider the following code:

```cs
this.WhenAny(x => x.Foo.Bar.Baz, _ => "Hello!")
    .Subscribe(x => Console.WriteLine(x));

// Example 1
this.Foo.Bar.Baz = null;
>>> Hello!

// Example 2: Nothing printed!
this.Foo.Bar = null;

// Example 3
this.Foo.Bar = new Bar() { Baz = "Something" };
>>> Hello!
```

In Example 1, even though `Baz` is null, because the expression could be evaluated, you get a notification.

In Example 2 however, evaluating this.Foo.Bar.Baz wouldn't give you null, it would crash. `WhenAny` therefore suppresses any notifications from being generated. Setting `Bar` to a new value generates a new notification.

### `WhenAny` only notifies on change of the output value

`WhenAny` only tells you when the final value of the input expression has changed. This is true even if the resulting change is because of an intermediate value in the expression chain. Here's an explaining example:

```cs
this.WhenAny(x => x.Foo.Bar.Baz, _ => "Hello!")
    .Subscribe(x => Console.WriteLine(x));

// Example 1
this.Foo.Bar.Baz = "Something";
>>> Hello!

// Example 2: Nothing printed!
this.Foo.Bar.Baz = "Something";

// Example 3: Still nothing
this.Foo.Bar = new Bar() { Baz = "Something" };

// Example 4: The result changes, so we print
this.Foo.Bar = new Bar() { Baz = "Else" };
>>> Hello!
```

Notably, in Example 3, even though the intermediate `Bar` object was replaced with a new instance, no change is fired - as the result of the full `Foo.Bar.Baz` expression has not changed.

### null propogation inside `WhenAnyValue`
`WhenAnyValue` is not directly able to perform null propogation due to the fact Expression's don't support this feature yet.

You can simulate null propogation support by chaining your WhenAnyValue() call to each property along the way. Here's an example:

```cs
this.WhenAnyValue(x => x.Foo, x => x.Foo.Bar, x => x.Foo.Bar.Baz, (foo, bar, baz) => foo?.Bar?.Baz)
    .Subscribe(x => Console.WriteLine(x));
```

# How WhenAny knows about different type of properties

`WhenAny` operators will look for services registered with Splat with the `ICreatesObservableForProperty` interface.

The interface has two methods, `GetAffinityForObject` and `GetNotificationForProperty`.

The first method `GetAffinityForObject` based on the property type, property name, and it should notify before or after the property change, will ask for a "vote" on how confident the property changed observable is at converting the value. A value of 0 from `GetAffinityForObject` means it cannot create a property changed observable at all. The system will then take votes from all registered `ICreatesObservableForProperty` interfaces and the one with the highest numeric vote wins. In the worst case a `POCOObservableForProperty` will always have a value greater than 0. This type of property changed observable will only get the initial value for the property and never update.

After the voting has finished it will call `GetNotificationForProperty` with the property type, name and if it should be before or after the property changed. It will then create the property changed observable.

When you are calling as part of a `WhenAny` chain it will get the property changed observable for each property along the chain.

So for instance `this.WhenAnyValue(x => x.Property1.Property2)` will get the property changed observables for the Property1, then for the Property2. So each can be a different type of observable property, for example a `INotifyPropertyChanged` and a `DependencyObject`.
