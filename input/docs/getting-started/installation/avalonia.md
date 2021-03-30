Title: Avalonia
Order: 90
---

<div class="video-iframe-wrapper">
    <div class="video-iframe-item">
        <?# YouTube q6uWPtKw3UQ /?>
    </div>
</div>

# Package Installation

Please note that the ReactiveUI support for [Avalonia](https://avaloniaui.net/) is provided directly by that team. See them for any support questions.

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.Avalonia (application)
- MyCoolApp.UnitTests (tests)
```

* Install `ReactiveUI` into your netstandard libraries, application and tests.
* Install `Avalonia.ReactiveUI` into your application and add `.UseReactiveUI()` to your AppBuilder.
* Install `ReactiveUI.Testing` into your tests.
