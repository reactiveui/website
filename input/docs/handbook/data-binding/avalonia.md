For [WhenActivated](../when-activated) to work, you need to use custom base classes from `Avalonia.ReactiveUI` package, such as `ReactiveWindow<TViewModel>` or `ReactiveUserControl<TViewModel>`. Of course, you can also implement the `IViewFor<TViewModel>` interface by hand in your class, but ensure to store the `ViewModel` inside an `AvaloniaProperty`. [Activation and deactivation](../when-activated) feature will work for your view model only in case you put an empty `WhenActivated` block right before a call to  `AvaloniaXamlRenderer.Load(this)` (otherwise activation can skip the Avalonia `Loaded` event alternative). See an example:

```cs
// Activatable ViewModel.
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
<Window>
  <TextBlock Text="Hello, world!" />
</Window>
```

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

Unfortunately, Avalonia XAML rendering engine won't generate strongly type `x:Name` references to controls for you, so the only way for now is to use the `FindControl` method that will find a control with specified name, or to use `{Binding Path}` syntax. But handling interactions, events and activation in the `WhenActivated` block is still a nice reason to use `IViewFor`.
