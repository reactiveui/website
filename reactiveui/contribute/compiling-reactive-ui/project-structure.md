# Project Structure

## ReactiveUI
[ReactiveUI](~/api/reactiveui.yml) is an MVVM framework that integrates with the Reactive Extensions for .NET to create elegant, testable user interfaces that run on any mobile or desktop platform. This is the base package with the core platform implementations.

## ReactiveUI.SourceGenerators (recommended)
The [ReactiveUI.SourceGenerators](https://www.nuget.org/packages/ReactiveUI.SourceGenerators/) package provides compile-time code generation for ReactiveUI, including reactive properties (`[Reactive]`), observable-as-property helper properties (`[ObservableAsProperty]`), ReactiveCommands (`[ReactiveCommand]`), and view wiring (`[IViewFor]`). Prefer this over Fody-based solutions for new projects.

## ReactiveUI.Maui
The [ReactiveUI.Maui](~/api/ReactiveUI.Maui.yml) package contains the ReactiveUI platform-specific extensions for [Microsoft .NET MAUI](https://dotnet.microsoft.com/en-us/apps/maui).

## ReactiveUI.Testing
The [ReactiveUI.Testing](~/api/ReactiveUI.Testing.yml) package provides extensions for testing ReactiveUI-based applications.

## ReactiveUI.WinForms
The [ReactiveUI.WinForms](~/api/reactiveui.winforms.yml) package contains the ReactiveUI platform-specific extensions for [Windows Forms](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/).

## ReactiveUI.WinUI
The [ReactiveUI.WinUI](~/api/reactiveui.winui.yml) package contains the ReactiveUI platform-specific extensions for [WinUI](https://learn.microsoft.com/en-us/windows/apps/winui/).

## ReactiveUI.Wpf
The [ReactiveUI.Wpf](~/api/reactiveui.wpf.yml) package contains the ReactiveUI platform-specific extensions for [Windows Presentation Foundation (WPF)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/overview/).

## ReactiveUI.Fody (legacy)
The [ReactiveUI.Fody](~/api/ReactiveUI.Fody.yml) package is a [Fody](https://github.com/Fody/Fody) extension that generates property change notifications and ObservableAsPropertyHelpers. It remains available but is no longer the recommended approach for new projects.

## ReactiveUI.Fody.Analyzer (legacy)
The [ReactiveUI.Fody.Analyzer](~/api/ReactiveUI.Fody.Analyzer.yml) package is a [Roslyn](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/) analyzer that checks usage of the Fody extension.

## ReactiveUI.Fody.Helpers (legacy)
The [ReactiveUI.Fody.Helpers](~/api/ReactiveUI.Fody.Helpers.yml) package extends Fody helpers. Prefer SourceGenerators going forward.

## ReactiveUI.XamForms (legacy)
The [ReactiveUI.XamForms](~/api/reactiveui.xamforms.yml) package contains the ReactiveUI platform-specific extensions for Xamarin.Forms. Xamarin platforms are legacy and supported for maintenance only.
