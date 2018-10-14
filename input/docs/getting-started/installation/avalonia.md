Title: Avalonia
Order: 90
---

# Package Installation

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.Avalonia (application)
- MyCoolApp.UnitTests (tests)
```

* Install `ReactiveUI` into your netstandard libraries, application and tests.
* Install `Avalonia.ReactiveUI` into your application and add `.UseReactiveUI()` to your AppBuilder.
* Install `ReactiveUI.Testing` into your tests.
