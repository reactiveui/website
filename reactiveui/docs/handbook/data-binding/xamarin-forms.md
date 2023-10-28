---
NoTitle: true
---
For Xamarin.Forms applications you need to install the ReactiveUI.XamForms [Nuget package](https://www.nuget.org/packages/ReactiveUI.XamForms/).

## ViewModels

Your Viewmodels should inherit from `ReactiveObject`. This brings all the power of ReactiveUI, such as `WhenAnyValue`, that are powerful building blocks for any viewmodel.

```csharp
public class TheViewModel : ReactiveObject
{
    private string theText;
    public string TheText
    {
        get => theText;
        set => this.RaiseAndSetIfChanged(ref theText, value);
    }

    public ReactiveCommand<Unit, Unit> TheTextCommand { get; }

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

## Views

Xamarin.Forms has the option to create views with either C# or XAML. ReactiveUI supports both. As a general rule you should use the _Reactive_ versions of the Xamarin.Forms controls (e.g. use `ReactiveContentPage<TViewModel>` instead of `ContentPage`).

**XAML**

Your XAML views should inherit from `ReactiveContentPage`, as shown here:

```xml
<rxui:ReactiveContentPage
  x:Class="App.Views.TheContentPage"
  x:TypeArguments="vm:TheViewModel"          
  xmlns:vm="clr-namespace:App.ViewModels;assembly=App"
  xmlns:rxui="clr-namespace:ReactiveUI.XamForms;assembly=ReactiveUI.XamForms"
  xmlns:x="https://schemas.microsoft.com/winfx/2009/xaml"
  xmlns="https://xamarin.com/schemas/2014/forms">
  <StackLayout>
    <Entry x:Name="TheTextBox" />
    <Label x:Name="TheTextBlock" />
    <Button x:Name="TheTextButton" />
  </StackLayout>
</rxui:ReactiveContentPage>
```

## Binding

The example below demonstrates how to use the ReactiveUI binding, but the Xamarin.Forms binding engine and XAML can also be used. Using one doesn't limit the use of the others, so all of them can be used in the same application.

```csharp
public partial class TheContentPage : ReactiveContentPage<TheViewModel>
{
    public ThePage()
    {
        InitializeComponent();

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
}
```

The ReactiveUI binding engine is very powerful, but there may be cases where you want to bind to C# Events coming from your view (such as when a buttons `Tap` event fires). For this scenario use [Pharmacist](https://github.com/reactiveui/Pharmacist), which automatically creates Observables for all the C# Events in your project.

```csharp
public partial class TheContentPage : ReactiveContentPage<TheViewModel>
{
    public ThePage()
    {
        InitializeComponent();

        // Setup the bindings.
        // Note: We have to use WhenActivated here, since we need to dispose the
        // bindings on XAML-based platforms, or else the bindings leak memory.
        this.WhenActivated(disposable =>
        {
            this.Bind(ViewModel, x => x.TheText, x => x.TheTextBox.Text)
                .DisposeWith(disposable);
            this.OneWayBind(ViewModel, x => x.TheText, x => x.TheTextBlock.Text)
                .DisposeWith(disposable);
                
            this.TheTextButton
                //Provided by Pharmacist
                .Events()
                .Released
                //Don't pass the EventArgs to the command
                .Select(_ => Unit.Default)
                .InvokeCommand(ViewModel, x => x.TheTextCommand)
                .DisposeWith(disposable);
        });
    }
}
```

## Routing

Want to know how this affects ViewModel based routing?

See [Routing documentation](~/docs/handbook/routing/index.md)!
