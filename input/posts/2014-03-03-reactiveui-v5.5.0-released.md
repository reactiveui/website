IsBlog: true
IsPost: true
Title: ReactiveUI v5.5.0 released
Tags: Release Notes
Lead: Anaïs Betts
---

## [What's New](https://github.com/reactiveui/ReactiveUI/compare/5.4.0...5.5.0)

## Bug Fixes
- Command bindings in WinForms now affect Enabled (#443, thanks @rzhw)
- Ensure that common WinForms controls don't get trumped by WPF (#447, thanks @rzhw)
- Enable BindCommand to work with nested ViewModels (#450, thanks @onovotny!)
- Attempt to prevent the Mono linker from stripping things we need (#455, thanks @onovotny!)
- Improvements to iOS binding (#473, #496, thanks @tberman)

## Activation

Thanks to the great work by @jen20, the View / ViewModel activation from ReactiveUI 6.0 has been backported to 5.x. Normally large features aren't backported, but due to discovering that DependencyProperties leak memory in bindings without this feature, we decided to backport it. 

### What do you mean, leaks?

The following code, in a sane world, wouldn't leak:

``` cs
public MyCoolUserControl()
{
    this.OneWayBind(ViewModel, x => x.FirstName, x => x.FirstName.Text);
}
```

Normally, when both the View and the ViewModel go out of scope, the GC would clean them both up and everything would be great. However, because of the Dependency Property system, this isn't true. **Every time you WhenAny or Bind through a DependencyProperty, you must explicitly clean it up by Disposing**. To help out with this, a new method has been created on Views and ViewModels.

Consider the following ViewModel constructor:

``` cs
public MyBrokenViewModel()
{
    UserError.RegisterHandler(x => {
        // NB: Stuff
    });
}
```

This is broken because every time we create MyBrokenViewModel, we create another error handler. What Do? What we really want for certain global things like UserError, is for UserError to be subscribed only when the View associated with the ViewModel is _visible_. However, that information isn't available to ViewModels, and even if it was, it's not super obvious. Let's fix it

### How does this work:

Activation allows you, for both Views and ViewModels, to set up _the things that should be_ **active** _when the View is visible_. Here's how you do it for ViewModels:

``` cs
public class MyWorkingViewModel : ReactiveObject, ISupportsActivation
{
    public ViewModelActivator Activator { get; protected set; }

    public ActivatingViewModel()
    {
        Activator = this.WhenActivated(d => {
            // d() registers a Disposable to be cleaned up when
            // the View is deactivated / removed
            d(UserError.RegisterHandler(x => {
                // NB: Stuff
            }));
        });
    }
}
```

Here's how it works for Views:

``` cs
public class MyWorkingView : UserControl, IViewFor<MyWorkingViewModel>
{
    public ActivatingView()
    {
        this.WhenActivated(d => {
            Console.WriteLine("Helloooooo Nurse!")
            d(Disposable.Create(() => Console.WriteLine("Goodbye, Cruel World")));
        });
    }
}
```

Note that calling `WhenActivated` in a View automatically means that the associated ViewModel gets notified for activated / deactivated changes (and in fact, you _must_ call `WhenActivated` in the View to get the ViewModel to be notified).
