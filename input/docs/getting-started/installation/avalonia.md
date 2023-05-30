NoTitle: true
Title: Avalonia
Order: 100
---

<div class="youtube-video-container"><iframe src="https://www.youtube.com/embed/q6uWPtKw3UQ" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>

# Package Installation

Please note that the ReactiveUI support for [Avalonia](https://avaloniaui.net/) is provided directly by that team. See them for any support questions.

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.Avalonia (application)
- MyCoolApp.UnitTests (tests)
```

* Install [ReactiveUI](https://www.nuget.org/packages/ReactiveUI) into your netstandard libraries and tests.
* Install [Avalonia.ReactiveUI](https://www.nuget.org/packages/Avalonia.ReactiveUI) into your application and add `.UseReactiveUI()` to your AppBuilder.
* Install [ReactiveUI.Testing](https://www.nuget.org/packages/ReactiveUI.Testing) into your tests.
