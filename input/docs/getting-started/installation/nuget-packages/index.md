Title: NuGet Packages
Order: 10
---

<blockquote class="twitter-tweet" data-lang="en"><p lang="en" dir="ltr">Packages for <a href="https://twitter.com/ReactiveXUI">@ReactiveXUI</a> are now signed by the <a href="https://twitter.com/dotnetfdn">@dotnetfdn</a>. Only builds from their infrastructure carry this seal &lt;3 <a href="https://twitter.com/onovotny">@onovotny</a> for your help <a href="https://t.co/3PZt8GNtWl">pic.twitter.com/3PZt8GNtWl</a></p>&mdash; Geoffrey Huntley (@GeoffreyHuntley) <a href="https://twitter.com/GeoffreyHuntley/status/903619522883497984">September 1, 2017</a></blockquote>
<script async src="//platform.twitter.com/widgets.js" charset="utf-8"></script>

# Tizen

* https://github.com/reactiveui/ReactiveUI/pull/1387
* https://reactiveui.net/docs/guidelines/platform/tizen

# Windows Forms

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.WinForms (application)
- MyCoolApp.UnitTests (tests)
```

* Install `ReactiveUI` into your netstandard libraries, application and tests.
* Install `ReactiveUI.WinForms` into your application and tests.
* Install `ReactiveUI.Events.WinForms` into your application.
* Install `ReactiveUI.Testing` into your tests.

# Windows Presentation Foundation

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.WPF (application)
- MyCoolApp.UnitTests (tests)
```

* Install `ReactiveUI` into your netstandard libraries, application and tests.
* Install `ReactiveUI.WPF` into your application and tests.
* Install `ReactiveUI.Events.WPF` into your application.
* Install `ReactiveUI.Testing` into your tests.

# Universal Windows Platform

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.Services.UWP (platform library)
- MyCoolApp.UWP (application)
- MyCoolApp.UnitTests (tests)
```

* Install `ReactiveUI` into your netstandard library, platform library, application and tests.
* Install `ReactiveUI.Events` into your netstandard library and application.
* Install `ReactiveUI.Testing` into your tests.

# Xamarin Android

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.Services.Droid (platform library)
- MyCoolApp.Droid (application)
- MyCoolApp.UnitTests (tests)
```

* Install `ReactiveUI` into your netstandard library, platform library, application and tests.
* Install `ReactiveUI.AndroidSupport` into your application.
* Install `ReactiveUI.Events` into your application.
* Install `ReactiveUI.Testing` into your tests.

# Xamarin iOS

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.Services.iOS (platform library)
- MyCoolApp.iOS (application)
- MyCoolApp.UnitTests (tests)
```

* Install `ReactiveUI` into your netstandard library, platform library, application and tests.
* Install `ReactiveUI.Events` into your application.
* Install `ReactiveUI.Testing` into your tests.

# Xamarin Forms

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.UI (netstandard library)
- MyCoolApp.Services.Droid (platform library)
- MyCoolApp.Services.iOS (platform library)
- MyCoolApp.Services.UWP (platform library)
- MyCoolApp.Droid (application)
- MyCoolApp.iOS (application)
- MyCoolApp.UWP (application)
- MyCoolApp.UnitTests (tests)
```

* Install `ReactiveUI` into your netstandard libraries, platform library, applications and tests.
* Install `ReactiveUI.XamForms` into your netstandard UI library, platform library, applications and tests.
* Install `ReactiveUI.Event.XamForms` into your netstandard library and applications.
* Install `ReactiveUI.Testing` into your tests.

# Xamarin Mac

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.Services.Mac (platform library)
- MyCoolApp.Mac (application)
- MyCoolApp.UnitTests (tests)
```

* Install `ReactiveUI` into your netstandard library, platform library, application and tests.
* Install `ReactiveUI.Events` into your application.
* Install `ReactiveUI.Testing` into your tests.

# Avalonia

Assuming the following project structure:

```
- MyCoolApp (netstandard library)
- MyCoolApp.Avalonia (application)
- MyCoolApp.UnitTests (tests)
```

* Install `ReactiveUI` into your netstandard library, application and tests.
* Install `Avalonia.ReactiveUI` into your application.
* Install `ReactiveUI.Testing` into your tests.
