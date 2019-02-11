For [WhenActivated](../when-activated) to work, you need to use custom base classes from `Avalonia.ReactiveUI` package, such as `ReactiveWindow<TViewModel>` or `ReactiveUserControl<TViewModel>`. Of course, you can also implement the `IViewFor<TViewModel>` interface by hand in your class, but ensure to store the `ViewModel` inside an `AvaloniaProperty`. [Activation and deactivation](../when-activated) feature will work for your view model only in case you put an empty `WhenActivated` block right before a call to  `AvaloniaXamlRenderer.Load(this)`. See an example:

```cs
public class ViewModel : ReactiveObject, ISupportsActivation
{
    public ViewModelActivator Activator { get; }

    public ViewModel()
    {
        Activator = new ViewModelActivator();
        this.WhenActivated(disposables =>
        {
            // Handle ViewModel activation and deactivation.
        });
    }
}
```

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        Background="#f0f0f0" FontFamily="Ubuntu"
        MinHeight="590" MinWidth="750">
  <TextBlock Text="Hello, world!" />
</Window>
```

Remember to place a call to WhenActivated to code-behind of the view, otherwise your view model's WhenActivated block won't get called.

```cs
public partial class View : ReactiveWindow<ViewModel>
{
    public View()
    {
        // ViewModel's WhenActivated will also get called.
        this.WhenActivated(disposables => { /* Handle interactions etc. */ });
        AvaloniaXamlLoader.Load(this);
    }
}
```

Unfortunately, Avalonia XAML rendering engine won't generate strongly typed `x:Name` references to controls, so the only way for now is to use the `FindControl` method that will find a control by the name specified in XAML, or to use `{Binding Path}` syntax. 

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
