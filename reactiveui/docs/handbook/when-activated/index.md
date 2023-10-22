---
NoTitle: true
---
## ViewModels

WhenActivated is a way to track disposables. Besides that, it can be used to defer the setup of a ViewModel until it's truly required. WhenActivated also gives us an ability to start or stop reacting to hot observables, like a background task that periodically pings a network endpoint or an observable updating users current location. Moreover, one can use WhenActivated to trigger startup logic when the ViewModel comes on stage. See an example:

```cs
public class ActivatableViewModel : IActivatableViewModel 
{
    public ViewModelActivator Activator { get; }

    public ActivatableViewModel()
    {
        Activator = new ViewModelActivator();
        this.WhenActivated(disposables => 
        {        
            // Use WhenActivated to execute logic
            // when the view model gets activated.
            this.HandleActivation();
            
            // Or use WhenActivated to execute logic
            // when the view model gets deactivated.
            Disposable
                .Create(() => this.HandleDeactivation())
                .DisposeWith(disposables);
        
            // Here we create a hot observable and 
            // subscribe to its notifications. The
            // subscription should be disposed when we'd 
            // like to deactivate the ViewModel instance.
            var interval = TimeSpan.FromMinutes(5);
            Observable
                .Timer(interval, interval)
                .Subscribe(x => { /* do smth every 5m */ })
                .DisposeWith(disposables);
                
            // We also can observe changes of a
            // property belonging to another ViewModel,
            // so we need to unsubscribe from that
            // changes to ensure we won't have the
            // potential for a memory leak.
            this.WhenAnyValue(...)
                .InvokeCommand(...)
                .DisposeWith(disposables);
        });
    }
    
    private void HandleActivation() { }
    
    private void HandleDeactivation() { }
}
```

## How to ensure a view model gets activated?

The framework will acknowledge the link from your `ViewModel` to your `View` if the latter implements the [IViewFor interface](../../handbook/view-location/extending-iviewfor). Remember to make your ViewModel a `DependencyProperty` and to add a call to `WhenActivated` in your `IViewFor<TViewModel>` implementation constructor.

## Views

Whenever you subscribe one object to an event exposed by another object, you introduce the potential for a memory leak. This is especially true for XAML based platforms where objects/events referenced by a dependency property may not get garbage collected for you automatically.

In a nutshell, when object A provides a handler for an event exposed by object B, the handler is attached to the event and the lifetime of the handler is tied to the lifetime of object B. When object B is disposed, it's event handlers are cleared, thus under normal circumstances it is not necessary to explicitly clear event subscriptions. In XAML, however, there is an additional wrinkle with dependency properties. If you hook change events on the "Value" property, _even when the object goes way_, you have leaked the event because it's tied to the static property ValueProperty.

ReactiveUI provides a variant of the Dispose pattern to help handle this concern:

```cs
public class ActivatableControl : ReactiveUserControl<ActivatableViewModel>
{
    public ActivatableControl()
    {
        this.InitializeComponent();
        this.WhenActivated(disposables =>
        {   
            // If you put the WhenActivated block into your IViewFor
            // implementation constructor, the view model will also
            // get activated if it implements IActivatableViewModel.
        
            // Dispose bindings to prevent dependency property memory 
            // leaks that may occur on XAML based platforms. 
            this.Bind(ViewModel, vm => vm.UserName, v => v.Username.Text)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel, vm => vm.Login, v => v.Login)
                .DisposeWith(disposables);
                
            // Dispose WhenAny bindings to ensure we won't have memory
            // leaking here if the ViewModel outlives the View and vice 
            // versa.
            this.WhenAnyValue(v => v.ViewModel.IsBusy)
                .BindTo(this, v => v.ProgressRing.IsActive)
                .DisposeWith(disposables);
        });
    }
}
```

As a rule of thumb for all platforms, you should use it for bindings and any time there's something your view sets up that will outlive the view bindings. It is also super useful for setting up things that should get added to the visual tree, even if they are not a disposable.

## When should I bother disposing of IDisposable objects?

If you do a [WhenAny](../when-any) on anything other than `this`, you **need** to put it inside a `WhenActivated` block and add a call to `DisposeWith`. If you just launch a window and then that window goes away when app goes away and you have nothing else to manage, you don't need `WhenActivated`. 

Always use `WhenActivated` and `DisposeWith` with XAML views, if you're writing `this.WhenAnyValue(x => x.ViewModel.Prop).BindTo(...)` or `this.Bind*(...)` (read more on bindings [here](../data-binding)). You should use it any time there's something your view sets up that will outlive the view - on a Xaml platform, you may have a subscription that you don't want to stay active when the view is detached from the visual tree. It's also useful for setting up things when you get added to the visual tree, although usually the correct place for something like that is in the ViewModel's `WhenActivated`. 

### 1. No need

```cs
public MyViewModel()
{
    MyReactiveCommand
        .Execute()
        .Subscribe(...);
}
```

> When the execution of a ReactiveCommand completes, all observers are auto-unsubscribed anyway. Generally, subscriptions to pipelines that have a finite lifetime (eg. via a timeout) need not be disposed manually. Disposing of such a subscription is about as useful as disposing of a MemoryStream. — [Kent Boogaart](https://github.com/kentcb) @ [You, I and ReactiveUI](https://kent-boogaart.com/you-i-and-reactiveui/)

### 2. Do dispose

```cs
public MyView()
{
    this.WhenAnyValue(x => x.ViewModel)
        .Do(PopulateFromViewModel)
        .Subscribe();
}
```

This one is tricky. Disposing of this subscription is a must if developing for a dependency property-based platform such as WPF or UWP. This is because "there's no non-leaky way to observe a dependency property. (quoting Anaïs Betts)," which is exactly what the ViewModel property of a ReactiveUserControl is. However, if you happen to know that your ViewModel won't change for the liftime of the view then you can make ViewModel a normal property, eliminating the need to dispose. For other platforms such as Xamarin.Forms, Xamarin.Android, and Xamarin.iOS there's no need to dispose because you're simply monitoring the property (ViewModel) on the view itself, so the subscription is attaching to PropertyChanged on that view. This means the view has a reference to itself and thus, doesn't prevent the it from being garbage collected.

### 3. Do dispose

```cs
public MyViewModel()
{
    SomeService.SomePipeline
        .Subscribe(...);
}
```

Services commonly have a longer lifetime than view models, especially in the case of singletons and global application variables. Therefore, it's vital that these kinds of subscriptions are disposed of.

### 4. No need

```cs
public MyViewModel()
{
    SomeService.SomePipelineModelingAsynchrony
        .Subscribe(...);
}
```

Pipelines modeling asynchrony can be relied upon to complete, and thus the subscription will be disposed of automatically via OnComplete (or OnError).

### 5. Do dispose

```cs
public MyView()
{
    this.WhenAnyValue(x => x.ViewModel.SomeProperty)
        .Do(AssignValueToViewControl)
        .Subscribe();
}
```

Now you're saying "attach to PropertyChanged on this and tell me when the ViewModel property changes, then attach to PropertyChanged on that (the view model) and tell me when SomeProperty changes." This implies the view model has a reference back to the view, which needs to be cleaned up or else the view model will keep the view alive.

### 6. Performance tip

```cs
public MyView()
{
    // For a dependency property-based platform such as WPF and UWP
    this.WhenActivated(
        disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Do(PopulateFromViewModel)
                .Subscribe()
                .DisposeWith(disposables);
        });
        
        // For other platforms it can be simplified to the following
        this.WhenAnyValue(x => x.ViewModel)
            .Where(x => x != null)
            .Do(PopulateFromViewModel)
            .Subscribe()
}

private void PopulateFromViewModel(MyViewModel vm)
{
    // Assign values from vm to controls
}
```

More efficient than binding to properties. If your ViewModel properties don't change over time, definitely use this pattern. The WhenActivated part is important for dependency property-based platforms (as mentioned in case 2) since it will handle disposing of the subscription every time the view is deactivated.

### 7. No need

```cs
// Should I dispose of the IDisposable that WhenActivated returns?
this.WhenActivated(disposables => { ... });
```

> If you're using WhenActivated in a view, when do you dispose of the disposable that it returns? You'd have to store it in a local field and make the view disposable. But then who disposes of the view? You'd need platform hooks to know when an appropriate time to dispose it is - not a trivial matter if that view is reused in virtualization scenarios. In addition to this, I have found that reactive code in VMs in particular tends to juggle a lot of disposables. Storing all those disposables away and attempting disposal tends to clutter the code and force the VM itself to be disposable, further confusing matters. Perf is another factor to consider, particularly on Android. — [Kent Boogaart](https://github.com/kentcb) @ [You, I and ReactiveUI](https://kent-boogaart.com/you-i-and-reactiveui/)


Special thanks to [@cabauman](https://github.com/cabauman) for creating and sharing the [ReactiveUI activation cheat-sheet](https://github.com/cabauman/Rx.Net-ReactiveUI-CheatSheet) you see above.
