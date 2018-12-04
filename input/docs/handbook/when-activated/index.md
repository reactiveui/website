# ViewModel

WhenActivated is a way to track disposables. Besides that, it can be used to defer the setup of a ViewModel until it's truly required. WhenActivated also gives us an ability to start or stop reacting to hot observables, like a background task that periodically pings a network endpoint or an observable updating users current location. Moreover, one can use WhenActivated to trigger startup logic when the ViewModel comes on stage. See an example:

```cs
public class ActivatableViewModel : ISupportsActivation 
{
    public ViewModelActivator Activator { get; }

    public ActivatableViewModel()
    {
        Activator = new ViewModelActivator();
        this.WhenActivated(disposables => 
        {        
            // Here we use WhenActivated to execute 
            // startup logic for our ViewModel.
            this.ExecuteStartupLogic();
        
            // Here we create a hot observable and 
            // subscribe to its notifications. The
            // subscription should be disposed when we'd 
            // like to deactivate the ViewModel instance.
            var interval = TimeSpan.FromMinutes(5);
            Observable
                .Timer(interval, interval)
                .Subscribe(...)
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
}
```

# View

## How to ensure `ActivatableViewModel` gets activated?

The framework will acknowledge the link from your `ViewModel` to your `View` if the latter implements the pattern on [Getting Started -- 4. Create Views](../../getting-started/#create-views). Watch out for `public static readonly DependencyProperty ViewModelProperty` and and the normal and explicit implementations of `IViewFor<AppViewModel>`.

## Caveat

Whenever you subscribe one object to an event exposed by another object, you introduce the potential for a memory leak. This is especially true for XAML based platforms where [objects/events referenced by a dependency property may not get garbage collected for you automatically](http://sharpfellows.com/post/Memory-Leaks-and-Dependency-Properties).

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

# Do I need WhenActivated and DisposeWith?

If you do a [WhenAny](../when-any) on anything other than `this`, you **need** to put it inside a `WhenActivated` block and add a call to `DisposeWith`. If you just launch a window and then that window goes away when app goes away and you have nothing else to manage, you don't need `WhenActivated`. 

Always use `WhenActivated` and `DisposeWith` with XAML views, if you're writing `this.WhenAnyValue(x => x.ViewModel.Prop).BindTo(...)` or `this.Bind*(...)` (read more on bindings [here](../data-binding)). You should use it any time there's something your view sets up that will outlive the view - on a Xaml platform, you may have a subscription that you don't want to stay active when the view is detached from the visual tree. It's also useful for setting up things when you get added to the visual tree, although usually the correct place for something like that is in the ViewModel's `WhenActivated`. 
