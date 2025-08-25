# Windows Forms

## Package Installation

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.WinForms (application)
- MyCoolApp.UnitTests (tests)
```

* Install [ReactiveUI](https://www.nuget.org/packages/ReactiveUI) into your netstandard libraries and tests.
* Install [ReactiveUI.WinForms](https://www.nuget.org/packages/ReactiveUI.WinForms) into your application and tests.
* Install [ReactiveMarbles.ObservableEvents.SourceGenerator](https://www.nuget.org/packages/ReactiveMarbles.ObservableEvents.SourceGenerator) into your application.
* Install [ReactiveUI.Testing](https://www.nuget.org/packages/ReactiveUI.Testing) into your tests.

Please ensure that you are targeting at least windows10.0.17763.0

i.e `<TargetFramework>net8.0-windows10.0.17763.0</TargetFramework>` in your csproj file.
