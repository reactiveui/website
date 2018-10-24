# Xamarin Forms

For Xamarin.Forms applications, you need to implement `IViewFor<T>` by hands and ensure that ViewModel is a `BindableProperty`. You can use base classes containing default `IViewFor` implementation from `ReactiveUI.XamForms` package, such as `ReactiveContentPage<TViewModel>`, `ReactiveTextCell<TViewModel>`, etc. Also, always dispose bindings via [WhenActivated](../when-activated), or else the bindings leak memory.

The goal in the example below is to two-way bind `TheText` property of `TheViewModel` to the Entry and one-way bind `TheText` property to the Label, so the Label updates when the user types text into the Entry.
 
```csharp
public class TheViewModel : ReactiveObject
{
    private string theText;
    public string TheText
    {
        get => theText;
        set => RaiseAndSetIfChanged(ref theText, value);
    }
}
```

```xml
<rxui:ReactiveContentPage 
  x:Class="local:TheContentPage"
  x:TypeArguments="vm:TheViewModel">
  <StackLayout>
    <Entry x:Name="TheTextBox" />
    <Label x:Name="TheTextBlock" />
  </StackLayout>
</rxui:ReactiveContentPage>
```

```csharp
public partial class TheContentPage : ReactiveContentPage<TViewModel>
{
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
        });
    }
}
```
