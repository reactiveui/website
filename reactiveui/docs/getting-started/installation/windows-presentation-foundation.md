# Windows Presentation Foundation

## Package Installation

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.WPF (application)
- MyCoolApp.UnitTests (tests)
```

* Install [ReactiveUI](https://www.nuget.org/packages/ReactiveUI) into your netstandard libraries and tests.
* Install [ReactiveUI.WPF](https://www.nuget.org/packages/ReactiveUI.WPF) into your application and tests.
* Install [ReactiveMarbles.ObservableEvents.SourceGenerator](https://www.nuget.org/packages/ReactiveMarbles.ObservableEvents.SourceGenerator) into your application.
* Install [ReactiveUI.Testing](https://www.nuget.org/packages/ReactiveUI.Testing) into your tests.

Please ensure that you are targeting at least windows10.0.19041.0

i.e `<TargetFramework>net10.0-windows10.0.19041.0</TargetFramework>` in your csproj file.

In your `App.xaml.cs`'s `OnStartup()`, create a `ReactiveUiBuilder`, configure it for WPF and run it:

```c#
//...
using ReactiveUI.Builder;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        RxAppBuilder.CreateReactiveUIBuilder()
                    .WithWpf()
                    .WithViewsFromAssembly(typeof(App).Assembly)
                    .Build();
        //...
    }

    //...
}
```