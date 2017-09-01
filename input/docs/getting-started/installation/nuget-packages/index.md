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
MyCoolApp (application)
MyCoolApps.UnitTests (tests)
```

* Install `reactiveui-winforms` into your application and tests.
* Install `reactiveui-events-winforms` into your application.
* Install `reactiveui-testing` into your tests.

# Windows Presentation Framework

_IMPORTANT_: The information below is forward looking that is relevant once ReactiveUI v8 ships. Until it does just use the drop `-wpf` off from all packages.

Assuming the following project structure:

```
- MyCoolApp (application)
- MyCoolApps.UnitTests (tests)
```

* Install `reactiveui-wpf` into your application and tests.
* Install `reactiveui-events-wpf` into your portable class library and application.
* Install `reactiveui-testing` into your tests.

# Universal Windows Platform

Assuming the following project structure:

```
- MyCoolApp (portable class library)
- MyCoolApp.Services.UWP (platform library)
- MyCoolApp.UWP (application)
- MyCoolApps.UnitTests (tests)
```

* Install `reactiveui` into your portable class library, platform library, application and tests.
* Install `reactiveui-events` into your portable class library and application.
* Install `reactiveui-testing` into your tests.

# Xamarin Android

Assuming the following project structure:

```
- MyCoolApp (portable class library)
- MyCoolApp.Services.Droid (platform library)
- MyCoolApp.Droid (application)
- MyCoolApps.UnitTests (tests)
```

* Install `reactiveui` into your portable class library, platform library, application and tests.
* Install `reactiveui-androidsupport` into your application.
* Install `reactiveui-events` into your portable class library and application.
* Install `reactiveui-testing` into your tests.

# Xamarin iOS

Assuming the following project structure:

```
- MyCoolApp (portable class library)
- MyCoolApp.Services.iOS (platform library)
- MyCoolApp.iOS (application)
- MyCoolApps.UnitTests (tests)
```

* Install `reactiveui` into your portable class library, platform library, application and tests.
* Install `reactiveui-events` into your portable class library and application.
* Install `reactiveui-testing` into your tests.

# Xamarin Forms

Assuming the following project structure:

```
- MyCoolApp (portable class library)
- MyCoolApp.Services.Droid (platform library)
- MyCoolApp.Services.iOS (platform library)
- MyCoolApp.Services.UWP (platform library)
- MyCoolApp.Droid (application)
- MyCoolApp.iOS (application)
- MyCoolApp.UWP (application)
- MyCoolApps.UnitTests (tests)
```

* Install `reactiveui-xamforms` into your portable class library, platform library, applications and tests.
* Install `reactiveui-events-xamforms` into your portable class library and applications.
* Install `reactiveui-testing` into your tests.

# Xamarin Mac

Assuming the following project structure:

```
- MyCoolApp (portable class library)
- MyCoolApp.Services.Mac (platform library)
- MyCoolApp.Mac (application)
- MyCoolApps.UnitTests (tests)
```

* Install `reactiveui` into your portable class library, platform library, application and tests.
* Install `reactiveui-events` into your portable class library and application.
* Install `reactiveui-testing` into your tests.
