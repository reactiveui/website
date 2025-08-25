# .NET MAUI

## Package Installation
- Install ReactiveUI.Maui into your MAUI app project(s):
  - MyApp.Maui: ReactiveUI.Maui
  - Shared core library: ReactiveUI
  - Test project: ReactiveUI.Testing

## Getting Started

1. Register platform services in MauiProgram:
```csharp
using ReactiveUI;
using Splat;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        // Optional: Register ILogger implementation
        Locator.CurrentMutable.RegisterConstant(new SplatLogger(), typeof(ILogger));

        return builder.Build();
    }
}
```

2. Create a ViewModel:
```csharp
public class MainViewModel : ReactiveObject
{
    private string _name;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public ReactiveCommand<Unit, Unit> SayHello { get; }

    public MainViewModel()
    {
        SayHello = ReactiveCommand.Create(() => Console.WriteLine($"Hello {Name}"));
    }
}
```

3. Bind in XAML and code-behind:
```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:rxui="clr-namespace:ReactiveUI.Maui;assembly=ReactiveUI.Maui"
             x:Class="MyApp.MainPage">
    <VerticalStackLayout>
        <Entry x:Name="NameEntry" />
        <Button x:Name="HelloButton" Text="Say Hello" />
    </VerticalStackLayout>
</ContentPage>
```
```csharp
public partial class MainPage : ReactiveContentPage<MainViewModel>
{
    public MainPage()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();

        this.WhenActivated(d =>
        {
            d(this.Bind(ViewModel, vm => vm.Name, v => v.NameEntry.Text));
            d(this.BindCommand(ViewModel, vm => vm.SayHello, v => v.HelloButton));
        });
    }
}
```

## Notes
- Use ReactiveContentPage<TViewModel> base class to enable WhenActivated and bindings.
- Prefer ReactiveUI.SourceGenerators for code generation.
