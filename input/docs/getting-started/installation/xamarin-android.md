NoTitle: true
Title: Xamarin Android
Order: 60
---

## Package Installation

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.Services.Droid (platform library)
- MyCoolApp.Droid (application)
- MyCoolApp.UnitTests (tests)
```

* Install [ReactiveUI](https://www.nuget.org/packages/ReactiveUI) into your netstandard libraries and tests.
* Install [ReactiveUI.AndroidSupport](https://www.nuget.org/packages/ReactiveUI.AndroidSupport) into your application.
* Install [ReactiveMarbles.ObservableEvents.SourceGenerator](https://www.nuget.org/packages/ReactiveMarbles.ObservableEvents.SourceGenerator) into your application.
* Install [ReactiveUI.Testing](https://www.nuget.org/packages/ReactiveUI.Testing) into your tests.
