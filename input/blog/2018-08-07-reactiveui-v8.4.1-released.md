ShowInSidebar: false
---
title: ReactiveUI v8.4.1 released
category: 
  - Release Notes
author: Rodney Littles, II
---

Release 8.4.1 of ReactiveUI is available!

Thank You to Sebastian Richter for fixing a bug that resolve issues for a number of users.

Thank You to Glenn Watson for helping getting this released pushed.

**Note:** There was a [WPF regression](https://github.com/reactiveui/ReactiveUI/pull/1710) in this release which has been fixed in release v8.4.4 that immediately followed this release. If you use WPF please skip this release.

As part of this release we had [27 commits](https://github.com/reactiveui/reactiveui/compare/8.3.1...8.4.1) which resulted in [8 issues](https://github.com/reactiveui/ReactiveUI/issues?milestone=8&state=closed) being closed.

__ReactiveUI-Core__

- [__#1604__](https://github.com/reactiveui/ReactiveUI/pull/1604) feature: stamp ReactiveUI ($(TargetFramework)) into builds
- [__#1651__](https://github.com/reactiveui/ReactiveUI/pull/1651) Fix InitializeReactiveUI not resolving namespaces correctly.
- [__#1688__](https://github.com/reactiveui/ReactiveUI/pull/1688) Fix ActOnEveryObject missing changes when SuppressChangeNotifications is used
- [__#1690__](https://github.com/reactiveui/ReactiveUI/pull/1690) fix: Update teams to point towards correct URI
- [__#1701__](https://github.com/reactiveui/ReactiveUI/pull/1701) feat: Update ReactiveUI to use Reactive Extensions 4.0.0 as minimum

__Tizen__

- [__#1546__](https://github.com/reactiveui/ReactiveUI/pull/1546) feature: add Tizen platform support

__Universal-Windows-Platform__

- [__#1312__](https://github.com/reactiveui/ReactiveUI/pull/1312) fix: wait for scheduler guards against exception in early initialisation

__Windows Forms__

- [__#1651__](https://github.com/reactiveui/ReactiveUI/pull/1651) Fix InitializeReactiveUI not resolving namespaces correctly.

__Windows Presentation Foundation__

- [__#1366__](https://github.com/reactiveui/ReactiveUI/pull/1366) fix: check for unsupported range actions in ReactiveList on WPF
- [__#1654__](https://github.com/reactiveui/ReactiveUI/pull/1654) Fix double registration of ComponentModelTypeConverter
- [__#1707__](https://github.com/reactiveui/ReactiveUI/pull/1707) bug: Fix the project to use TargetFrameworks

__Xamarin-Android__

- [__#1680__](https://github.com/reactiveui/ReactiveUI/pull/1680) Fix: Android.Support.V4.App.Fragment is missing ResolveStrategy parameter

__Xamarin-Forms__

- [__#1631__](https://github.com/reactiveui/ReactiveUI/pull/1631) feat: Update to Xamarin forms 3.0

__Housekeeping__

- [__#1693__](https://github.com/reactiveui/ReactiveUI/pull/1693) fix: MSBuild.Extras was mistakenly adding incorrect framework references
- [__#1694__](https://github.com/reactiveui/ReactiveUI/pull/1694) housekeeping: header incorrectly said MS-PL instead of MIT
- [__#1695__](https://github.com/reactiveui/ReactiveUI/pull/1695) Fix MSBuild.Sdk.Extras support to use the new syntax
- [__#1703__](https://github.com/reactiveui/ReactiveUI/pull/1703) housekeeping: Splat 4.0.2 is now required to fix MsBuild.Sdk.Extras issues
- [__#1705__](https://github.com/reactiveui/ReactiveUI/pull/1705) housekeeping: Update the testing projects and cake file to have latest nuget packages

__Documentation__

- [__#1708__](https://github.com/reactiveui/ReactiveUI/pull/1708) housekeeping: Fix typos and broken links in readme


### Where to get it
You can download this release from [nuget.org](https://www.nuget.org/packages/reactiveui/8.4.1)
