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
