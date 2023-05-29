NoTitle: true
Title: Xamarin Mac
Order: 70
---

## Package Installation

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.Services.Mac (platform library)
- MyCoolApp.Mac (application)
- MyCoolApp.UnitTests (tests)
```

* Install [ReactiveUI](https://www.nuget.org/packages/ReactiveUI) into your netstandard libraries and tests.
* Install [ReactiveUI.XamForms](https://www.nuget.org/packages/ReactiveUI.XamForms) into your netstandard UI library, platform library, applications and tests.
* Install [ReactiveMarbles.ObservableEvents.SourceGenerator](https://www.nuget.org/packages/ReactiveMarbles.ObservableEvents.SourceGenerator) into your application.
* Install [ReactiveUI.Testing](https://www.nuget.org/packages/ReactiveUI.Testing) into your tests.
