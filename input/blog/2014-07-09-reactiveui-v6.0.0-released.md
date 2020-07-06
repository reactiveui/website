---
title: ReactiveUI v6.0.0 released
category: Release Notes
author: Anaïs Betts
---

# Welcome to ReactiveUI 6.0

After 8 months of work, 878 commits, and 1032 files changed, ReactiveUI 6.0 is finally released, the biggest RxUI release ever! A huge thanks to our contributers for this release:
- Johan Laanstra
- Oren Novotny
- Todd Berman
- Michael Teper
- Felipe Lessa
- Amy Palamountain
- Dennis Daume
- Rik Bosch
- James Nugent
- Phil Haack
- Maratoss
- David Lechner
- Justin Manus
- Keith Dahlby
- Markus Olsson

In particular, a huge thanks goes to Johan, who has done an enormous amount of great work on this release. Thank you so much!

## Migrating from existing code

We've written [a migration guide](https://github.com/reactiveui/ReactiveUI/blob/main/docs/migrating-from-rxui5.md) to help existing application authors migrate their 5.x applications to ReactiveUI 6.0. Check out this document before updating your dependencies to get a heads-up as to what you're in for.

## What's New in ReactiveUI 6.0

Over 120 new features were merged into ReactiveUI 6.0, trying to sum them all up is a huge undertaking! Here are some of the highlights:

### Universal Windows Phone app and Xamarin Forms Support

ReactiveUI 6.0 has great support for all of the latest developer platforms, including WinRT Universal Apps as well as support for the new Xamarin Forms UI toolkit via the new `ReactiveUI-XamForms` NuGet package. Use either the updated Portable Library support, or use the new Shared Projects tooling in Visual Studio.

Existing support for Android and iOS has also been greatly improved, including support for unit test runners on those platforms, as well as creating Observable abstractions for all events via the `ReactiveUI-Events` package. Helpers for the Android Support Library are now also provided, via the `ReactiveUI-AndroidSupport` package.

ReactiveUI 6.0 supports the following platforms (In order of personal developer joy):
- Xamarin.Android
- Xamarin.iOS
- Xamarin.Mac
- Xamarin Forms (iOS + Android + WP8)
- .NET 4.5 (WPF and Windows Forms, via `ReactiveUI-WinForms`)
- Universal Windows Apps (WPA81)
- Windows Phone 8.0 Apps (Silverlight-based)
- Windows Store Apps (WinRT)

### ReactiveUI makes creating list-based views a snap

We've added great support for recycling list-based views on iOS and Android (`UICollectionView` and `UITableView` on iOS, `ListAdapter` on Android). These new adapter classes allow you to map a ReactiveList of ViewModel objects and automatically create and recycle the associated views, for high-performance lists without writing a ton of boilerplate code.

On iOS, added and removed items will even be automatically animated in and out. On Android, we help you easily implement the ViewHolder pattern to limit the amount of work done while scrolling.

### Large Application Performance

One of the focuses of this release has been performance and memory usage in large applications. ReactiveUI 6.0 is much less prone to creating memory leaks in application code via WeakEventManager, as well as more performant by eliminating scheduling latency as much as possible. Other features, such as View and ViewModel Activation, allow you to create and clean-up objects only when the View is actually visible on-screen, saving a lot of unnecessary work.

While some of these changes will require you to update your application and unit tests, the end result is an application that uses less memory and feels more responsive.

### The same Rx, Everywhere

ReactiveUI 5.x used a separate installation of the Reactive Extensions for .NET for Xamarin projects, which made creating proper Portable Libraries more difficult. RxUI 6.0 now resolves this completely, and you can now build ViewModels that work on every supported platform.

## Questions, Comments, Concerns?

There are three great venues for problems / questions related to this release:
- [The ReactiveUI mailing list](http://groups.google.com/group/reactivexaml)
- [Issues on GitHub](https://github.com/reactiveui/ReactiveUI/issues)
- The ReactiveUI Slack chat room - if you're interested in joining this chat room, please Email anais@anaisbetts.org from the Email you want to use and I can add you.
