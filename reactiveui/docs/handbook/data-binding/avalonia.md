---
NoTitle: true
---
> **Note** First of all, ensure you install the `Avalonia.ReactiveUI` package and *add a call* to `UseReactiveUI()` to your `AppBuilder` — that's super important. Note, that `Avalonia.ReactiveUI` package is supported by AvaloniaUI team, so if anything goes wrong, head over to [AvaloniaUI Gitter](https://gitter.im/AvaloniaUI/Avalonia). Also see [Avalonia.ReactiveUI Docs](https://docs.avaloniaui.net/guides/deep-dives/reactiveui).

For [WhenActivated](~/docs/handbook/when-activated.md) to work, you need to use custom base classes from `Avalonia.ReactiveUI` package, such as `ReactiveWindow<TViewModel>` or `ReactiveUserControl<TViewModel>`. Of course, you can also implement the `IViewFor<TViewModel>` interface by hand in your class, but ensure to store the `ViewModel` inside an `AvaloniaProperty`. If you wish to add activation support to your view models, then implement the `IActivatableViewModel` interface. A view model implementing `IActivatableViewModel` might look like the one below:

```cs
public class ViewModel : ReactiveObject, IActivatableViewModel
{
    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    public ViewModel()
    {
        this.WhenActivated(disposables =>
        {
            /* Handle activation */
            Disposable
                .Create(() => { /* Handle deactivation */ })
                .DisposeWith(disposables);
        });
    }
}
```

Remember to place a call to `WhenActivated` to the code-behind of your view, otherwise your view model's `WhenActivated` block won't get called.

```cs
// This represents the View.xaml.cs code-behind file.
public partial class View : ReactiveWindow<ViewModel>
{
    public View()
    {
        // If you put a WhenActivated block here, your activatable view model 
        // will also support activation, otherwise it won't.
        this.WhenActivated(disposables => { /* Handle interactions etc. */ });
        AvaloniaXamlLoader.Load(this);
    }
}
```

```xml
<!-- This represents the View.xaml markup file. -->
<Window xmlns="https://github.com/avaloniaui">
  <TextBlock Text="Hello, world!" />
</Window>
```

The `WhenActivated` block is called when the `AttachedToVisualTree` event is published, and the disposable is disposed of when the `DetachedFromVisualTree` event is published. The behavior is the same for both the views and the view models. Unfortunately, Avalonia won't generate strongly typed `x:Name` references to controls, so the only way for now is to use the `FindControl` method that will find a control by the name specified in XAML, or to use `{Binding Path}` syntax. 

> **Note** The `FindControl` method shouldn't be used inside an expression. Instead, create a custom property which calls the `FindControl` method, or store the control in a variable. See an example of how to use ReactiveUI type-safe bindings with AvaloniaUI.

```cs
public partial class View : ReactiveWindow<ViewModel>
{
    // Assume the Button control has the Name="ExampleButton" attribute defined in XAML.
    public Button ExampleButton => this.FindControl<Button>("ExampleButton");

    public View()
    {
        this.WhenActivated(disposables => 
        {
            // Bind the 'ExampleCommand' to 'ExampleButton' defined above.
            this.BindCommand(ViewModel, x => x.ExampleCommand, x => x.ExampleButton)
                .DisposeWith(disposables);
        });
        AvaloniaXamlLoader.Load(this);
    }
}
```
