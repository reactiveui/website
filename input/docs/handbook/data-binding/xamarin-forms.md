# Xamarin Forms

For Xamarin.Forms applications, you need to install the `ReactiveUI.XamForms` package and use base classes for your Views from there. Your `ContentPage` should inherit from `ReactiveContentPage<TViewModel>`, your `TextCell` should inherit from `ReactiveTextCell<TViewModel>`, etc. Those classes contain the `IViewFor<TViewModel>` interface implementation. Also, always dispose bindings via [WhenActivated](../when-activated), or else the bindings leak memory.

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
