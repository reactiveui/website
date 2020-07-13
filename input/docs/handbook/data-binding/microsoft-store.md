For Universal Windows applications, you need to implement `IViewFor<T>` by hand and ensure that ViewModel is a `DependencyProperty`. Also, always dispose bindings via [WhenActivated](../when-activated), or else the bindings leak memory. You can easily use the new `x:Bind` syntax with ReactiveUI. All you need is doing `{x:Bind ViewModel.TheText, Mode=OneWay}`. Remember, that `x:Bind` bindings are `OneTime` by default, not `OneWay`, so in certain scenarios you need to specify the `OneWay` mode explicitly.

The goal in the example below is to two-way bind `TheText` property of `TheViewModel` to the TextBox and one-way bind `TheText` property to the TextBlock, so the TextBlock updates when the user types text into the TextBox.
  
```csharp
public class TheViewModel : ReactiveObject
{
    private string theText;
    public string TheText
    {
        get => theText;
        set => this.RaiseAndSetIfChanged(ref theText, value);
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
<Page /* snip */>
  <StackPanel>
    <TextBox x:Name="TheTextBox" />
    <TextBlock x:Name="TheTextBlock" />
    <Button x:Name="TheTextButton" />
  </StackPanel>
</Page>
```

```csharp
public partial class ThePage : Page, IViewFor<TheViewModel>
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty
        .Register(nameof(ViewModel), typeof(TheViewModel), typeof(TheView), new PropertyMetadata(null));
        
    public ThePage()
    {
        InitializeComponent(); 
        ViewModel = new TheViewModel();
        
        // Setup the bindings.
        // Note: We have to use WhenActivated here, since we need to dispose the
        // bindings on XAML-based platforms, or else the bindings leak memory.
        this.WhenActivated(disposable =>
        {
            this.Bind(ViewModel, x => x.TheText, x => x.TheTextBox.Text)
                .DisposeWith(disposable);
            this.OneWayBind(ViewModel, x => x.TheText, x => x.TheTextBlock.Text)
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
