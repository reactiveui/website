# Data Binding in WPF

Implement `IViewFor<T>` by hand and ensure that ViewModel is a `DependencyProperty`. Also, always dispose bindings via [WhenActivated](~/docs/handbook/when-activated.md), or else the bindings leak memory. The XAML `DependencyProperty` system causes memory leaks if you don't use `WhenActivated`. There's a few rules, but the number one rule is: if you do a `WhenAny` on anything other than `this`, then you need to put it inside a `WhenActivated`. See [WhenActivated](~/docs/handbook/when-activated.md) for details.
  
The goal in this example is to two-way bind `TheText` property of the ViewModel to the TextBox and one-way bind `TheText` property to the TextBlock, so the TextBlock updates when the user types text into the TextBox. 
  
```csharp
public class TheViewModel : ReactiveObject
{
    private string theText;
    public string TheText
    {
        get => theText;
        set => this.RaiseAndSetIfChanged(ref this.theText, value);
    }
    
    public ReactiveCommand<Unit,Unit> TheTextCommand { get; }

    public TheViewModel()
    {
        TheTextCommand = ReactiveCommand
            .CreateFromObservable(ExecuteTextCommand);
    }

    private IObservable<Unit> ExecuteTextCommand()
    {
        TheText = "Hello ReactiveUI";
        return Observable.Return(Unit.Default);
    }
}
```

```xml
<Window /* snip */>
  <StackPanel>
    <TextBox x:Name="TheTextBox" />
    <TextBlock x:Name="TheTextBlock" />
    <Button x:Name="TheTextButton" />
  </StackPanel>
</Window>
```

In this example we are implementing the `IViewFor<TViewModel>` interface by hands, but for a `UserControl` we would use a `ReactiveUserControl<TViewModel>` that encapsulates the `IViewFor` implementation.

```csharp
public partial class TheView : Window, IViewFor<TheViewModel>
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty
        .Register(nameof(ViewModel), typeof(TheViewModel), typeof(TheView));
        
    public TheView()
    {
        InitializeComponent();
        ViewModel = new TheViewModel();
        
        // Setup the bindings
        // Note: We have to use WhenActivated here, since we need to dispose the
        // bindings on XAML-based platforms, or else the bindings leak memory.
        this.WhenActivated(disposable =>
        {
            this.Bind(this.ViewModel, x => x.TheText, x => x.TheTextBox.Text)
                .DisposeWith(disposable);
            this.OneWayBind(this.ViewModel, x => x.TheText, x => x.TheTextBlock.Text)
                .DisposeWith(disposable);
            this.BindCommand(ViewModel, x => x.TheTextCommand, x => x.TheTextButton)
                .DisposeWith(disposable);
        });
    }

    public TheViewModel ViewModel
    {
        get => (TheViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (TheViewModel)value;
    }
}
```
