In interactive UI applications, state is continually changing in response to user actions and application events. ReactiveUI enables you to express changes to application state as streams of values and combine and manipulate them using the powerful Reactive Extensions library.

The motivation is intuitive enough when you think about it. It's not hard to imagine that changes to a property can be considered events - that's how `INotifyPropertyChanged` works. From there, the same argument for using Rx over events applies. In the context of MVVM application design specifically, modelling property changes as observables leads to several advantages:

- The logic of an application can be defined in terms of changes to properties
- This logic can be composed and expressed declaratively, using the power of Rx operators
- Concepts like time and asynchronicity become easier to reason about, due to their first-class treatment in an Observable context.

ReactiveUI provides several variants of `WhenAny` to help you work with properties as an observable stream. 

## Basic syntax

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
                 (r,g,b) => new Color(r, g, b));
```

### Watching a nested property

`WhenAny` variants can observe nested properties for changes, too:

```cs
this.WhenAnyValue(x => x.Foo.Bar.Baz);
```

## Idiomatic usage

Naturally, once you have an observable of property changes you can `Subscribe` to it in order to perform actions in response to the changed values. However, in many cases there may be a Better Way to achieve what you want. Below are some typical usages of the observables returned by the `WhenAny` variants:

### Exposing 'calculated' properties

In general, using `Subscribe` on a `WhenAny` observable (or any observable, for that matter) just to set a property is likely a code smell. Idiomatically, the `ToProperty` operator is used to create a 'read-only' calculated property that can be exposed to the rest of your application, only settable by the `WhenAny` chain that preceded it:

```cs
this.WhenAnyValue(x => x.SearchText, x => x.Length)
    .ToProperty(this, x => x.SearchTextLength, out _searchTextLength);
```

This initialises the `SearchTextLength` property (an [ObservableAsPropertyHelper](../oaph/) property) as a property that will be updated with the current search text length every time it changes. The property cannot be set in any other manner and raises change notifications, so can itself be used in a `WhenAny` expression or a binding. 

See the [ObservableAsPropertyHelper](../oaph/) section for more information on this pattern.

### Supporting validation as a `CanExecute` criteria

`WhenAny` can make specifying and adhering to validation logic clean and simple. Here, `WhenAnyValue` is used to observe the changing values of the `Username` and `Password` fields, and project whether the current pair of values is valid. This becomes the `canExecute` parameter for `CreateUserCommand`, preventing the user from proceeding until the validation conditions are met.

```cs
var canCreateUser =
    this.WhenAnyValue(x => x.Username, x => x.Password, 
        (user, pass) => 
            !String.IsNullOrWhiteSpace(user) && !String.IsNullOrWhiteSpace(pass) 
            && user.Length >= 3 && pass.Length >= 8)
        .DistinctUntilChanged();

CreateUserCommand = ReactiveCommand.CreateAsyncTask(canCreateUser, CreateUser); 
```

### Invoking commands
Commands are often bound to buttons or controls in the view that can be triggered by the user. However, it often makes sense to perform work in response to changes in property values. For example, a 'live search' feature may be designed to perform searches as the user types into a textbox, after a small delay is detected. `WhenAny` in conjunction with the `InvokeCommand` operator can be used to achieve this.

```cs
// in the viewmodel
this.WhenAnyValue(x => x.SearchText)
    .Where(x => !String.IsNullOrWhiteSpace(x))
    .Throttle(TimeSpan.FromSeconds(.25))
    .InvokeCommand(SearchCommand)

// in the view
this.Bind(ViewModel, x => x.SearchText, x => x.SearchTextField.Text);
```

In addition to being able to simply and declaratively handle search throttling, building the search execution logic on top of the property change has made it easy to keep all the logic in the viewmodel - all the view needs to do is bind a control to the property.

### Performing view-specific transforms as an input to `BindTo`
Ideally, controls on your view bind directly to properties on your viewmodel. In cases where you need to convert a viewmodel value to a view-specific value (e.g. `bool` to `Visibility`), you should register a `BindingConverter`. Still, you may come across a situation in which you want to perform a transformation in the view directly. Here, we observe the `ShowToolTip` property of the viewmodel, transform the `true`/`false` values to `1` and `0` respectively, then bind the result to the `ToolTipLabel`'s alpha property. 

```cs
// In the view
ViewModel.WhenAny(x => x.ShowToolTip)
         .Select(t => t ? 1f : 0f)
         .BindTo(this, x => x.ToolTipLabel.Alpha);
```

## Variants of `WhenAny`
 
Several variants of `WhenAny` exist, suited for different scenarios.

### WhenAny vs WhenAnyValue

`WhenAnyValue` covers the most common usage of `WhenAny`, and is a useful shortcut in many cases. The following two statements are equivalent and return an observable that yields the updated value of `SearchText` on every change:

- `this.WhenAny(x => x.SearchText, x => x.Value)`
- `this.WhenAnyValue(x => x.SearchText)`

When needing to observe one or many properties for changes, `WhenAnyValue` is quick to type and results in simpler looking code. Working with `WhenAny` directly gives you access to the `ObservedChange<,>` object that ReactiveUI produces on each property change. This is typically useful for framework code or extension methods. `ObservedChange` exposes the following properties:
* `Value` - the updated value
* `Sender` - the object whose has property changed 
* `Expression` - the expression that changed.

At the risk of extreme repetition - use `WhenAnyValue` unless you know you need `WhenAny`. 

### WhenAnyObservable

`WhenAnyObservable` acts a lot like the Rx operator `CombineLatest`, in that it watches one or multiple observables and allows you to define a projection based on the latest value from each. `WhenAnyObservable` differs from `CombineLatest` in that its parameters are expressions, rather than direct references to the target observables. The impact of this difference is that the watch set up by `WhenAnyObservable` is not tied to the specific observable instances present at the time of subscription. That is, the observable pointed to by the expression can be replaced later, and the results of the new observable will still be captured. 

An example of where this can come in handy is when a view wants to observe an observable on a viewmodel, but the viewmodel can be replaced during the view's lifetime. Rather than needing to resubscribe to the target observable after every change of viewmodel, you can use `WhenAnyObservable` to specify the 'path' to watch. This allows you to use a single subscription in the view, regardless of the life of the target viewmodel. 

## Additional Considerations

Using `WhenAny` variants is fairly straightforward. However, there are a few aspects of their behaviour that are worth highlighting.

### `INotifyPropertyChanged` is required

Watched properties must implement ReactiveUI's `RaiseAndSetIfChanged` or raise the standard `INotifyPropertyChanged` events. If you attempt to use `WhenAny` on a property without either of these in place, `WhenAny` will produce the current value of the property upon subscription, and nothing thereafter. Additionally, a warning will be issued at run time (ensure you have registered a service for `ILogger` to see this).

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

* In Example 1, even though `Baz` is null, because the expression could be evaluated, you get a notification.

* In Example 2 however, evaluating this.Foo.Bar.Baz wouldn't give you null, it would crash. `WhenAny` therefore suppresses any notifications from being generated. Setting `Bar` to a new value generates a new notification.

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

# Relevant Samples

Samples demonstrating `WhenAny` use will be listed below.
