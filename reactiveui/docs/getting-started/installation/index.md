# Installation

ReactiveUI packages are signed by the .NET Foundation. Install platform-specific packages alongside the core ReactiveUI package as needed.

| Platform          | ReactiveUI Package                  | NuGet                  |
| ----------------- | ----------------------------------- | ---------------------- |
| .NET (Core libs)  | [ReactiveUI][CoreDoc]               | [![CoreBadge]][Core]   |
| Unit Testing      | [ReactiveUI.Testing][TestDoc]       | [![TestBadge]][Test]   |
| WPF               | [ReactiveUI.WPF][WpfDoc]            | [![WpfBadge]][Wpf]     |
| WinUI             | [ReactiveUI.WinUI][WinUiDoc]        | [![WinUiBadge]][WinUi] |
| MAUI              | [ReactiveUI.Maui][MauiDoc]          | [![MauiBadge]][Maui]   |
| Windows Forms     | [ReactiveUI.WinForms][WinDoc]       | [![WinBadge]][Win]     |
| AndroidX (Xamarin) | [ReactiveUI.AndroidX][DroDoc]      | [![DroXBadge]][DroX]   |
| Blazor            | [ReactiveUI.Blazor][BlazDoc]        | [![BlazBadge]][Blaz]   |
| Uno               | [ReactiveUI.Uno][UnoDoc]            | [![UnoBadge]][Uno]     |
| Uno WinUI         | [ReactiveUI.Uno.WinUI][UnoWinUiDoc] | [![UnoWinUiBadge]][UnoWinUi] |
| Avalonia          | [ReactiveUI.Avalonia][AvaDoc]       | [![AvaBadge]][Ava]     |
| Validation        | [ReactiveUI.Validation][ValDocs]    | [![ValBadge]][ValCore] |

[Core]: https://www.nuget.org/packages/ReactiveUI/
[CoreBadge]: https://img.shields.io/nuget/v/ReactiveUI.svg
[CoreDoc]: ~/docs/getting-started/installation/index.md

[Test]: https://www.nuget.org/packages/ReactiveUI.Testing/
[TestBadge]: https://img.shields.io/nuget/v/ReactiveUI.Testing.svg
[TestDoc]: ~/docs/handbook/testing.md

[Wpf]: https://www.nuget.org/packages/ReactiveUI.WPF/
[WpfBadge]: https://img.shields.io/nuget/v/ReactiveUI.WPF.svg
[WpfDoc]: ~/docs/getting-started/installation/windows-presentation-foundation.md

[WinUi]: https://www.nuget.org/packages/ReactiveUI.WinUI/
[WinUiBadge]: https://img.shields.io/nuget/v/ReactiveUI.WinUI.svg
[WinUiDoc]: ~/docs/getting-started/installation/winui.md

[Maui]: https://www.nuget.org/packages/ReactiveUI.Maui/
[MauiBadge]: https://img.shields.io/nuget/v/ReactiveUI.Maui.svg
[MauiDoc]: ~/docs/getting-started/installation/maui.md

[Win]: https://www.nuget.org/packages/ReactiveUI.WinForms/
[WinBadge]: https://img.shields.io/nuget/v/ReactiveUI.WinForms.svg
[WinDoc]: ~/docs/getting-started/installation/windows-forms.md

[DroX]: https://www.nuget.org/packages/ReactiveUI.AndroidX/
[DroXBadge]: https://img.shields.io/nuget/v/ReactiveUI.AndroidX.svg
[DroDoc]: ~/docs/getting-started/installation/androidx.md

[Uno]: https://www.nuget.org/packages/ReactiveUI.Uno/
[UnoBadge]: https://img.shields.io/nuget/v/ReactiveUI.Uno.svg
[UnoDoc]: https://platform.uno/blog/getting-started-with-uno-platform-and-reactiveui/
[UnoWinUi]: https://www.nuget.org/packages/ReactiveUI.Uno.WinUI/
[UnoWinUiBadge]: https://img.shields.io/nuget/v/ReactiveUI.Uno.WinUI.svg
[UnoWinUiDoc]: https://platform.uno/docs/articles/uwp-vs-winui3.html

[Blaz]: https://www.nuget.org/packages/ReactiveUI.Blazor/
[BlazBadge]: https://img.shields.io/nuget/v/ReactiveUI.Blazor.svg
[BlazDoc]: ~/docs/getting-started/installation/blazor.md

[Ava]: https://www.nuget.org/packages/ReactiveUI.Avalonia/
[AvaBadge]: https://img.shields.io/nuget/v/ReactiveUI.Avalonia.svg
[AvaDoc]: ~/docs/getting-started/installation/avalonia.md

[ValCore]: https://www.nuget.org/packages/ReactiveUI.Validation/
[ValBadge]: https://img.shields.io/nuget/v/ReactiveUI.Validation.svg
[ValDocs]: ~/docs/handbook/user-input-validation.md

> Note: Xamarin platforms are considered legacy. For new applications, prefer .NET (WPF/WinForms/WinUI), MAUI, Avalonia, or Uno. Fody-based packages are legacy; prefer ReactiveUI.SourceGenerators for code generation.

## Example

A typical solution might include the following packages:

<pre>
.
├── MyApp.Core (.NET class library)
│   ├── ReactiveUI
│   └── ReactiveUI.SourceGenerators
├── MyApp.Wpf (WPF)
│   └── ReactiveUI.WPF
├── MyApp.WinUI (WinUI)
│   └── ReactiveUI.WinUI
├── MyApp.Maui (MAUI)
│   └── ReactiveUI.Maui
├── MyApp.Tests
│   └── ReactiveUI.Testing
└── MyApp.Avalonia
    └── Avalonia.ReactiveUI
</pre>

## Release Packages
ReactiveUI is published to NuGet.org when releases are cut. Subscribe for notifications on libraries.io.

