NoTitle: true
IsBlog: true
Title: ReactiveUI v5.0.1 released
Tags: Release Notes
Author: Anaïs Betts
Published: 2013-07-03
---

After 3 months, 427 commits by 12 different contributors, and 689 total files changed, the stable release of ReactiveUI 5.0 is now live! A huge thanks to the contributors for this release:
- Markus Olsson
- Johan Laanstra
- Oliver Weichhold
- Phil Haack
- Christopher Atkins
- Brad Phelan
- Georg Rollinger
- Kent Boogaart
- Wenda Zhou

## [Check out the Ship PR](https://github.com/reactiveui/ReactiveUI/pull/219)

Here's the release highlights:

## ReactiveUI is now totally Portable-Friendly

ReactiveUI now is compatible with .NET 4.5 Portable Libraries, you can now write cross-platform ViewModels but still use almost all of the RxUI features. 

Unfortunately, to do this, we had to **drop support** for a number of older platforms. If you are using Silverlight, WP7, or .NET 4.0, you'll have to stick with ReactiveUI 4.x, which will still be maintained in a separate branch. 

ReactiveUI 5.x has full support for the following platforms:
- Xamarin.iOS
- Xamarin.Android
- Xamarin.Mac
- .NET 4.5 (WPF)
- Windows Phone 8
- Windows Store Apps (WinRT)

## ReactiveUI.Events

ReactiveUI now makes it easy to bind to UI events, without having to use `Observable.FromEventPattern`. Many UI controls and other objects now have an `Events()` extension method. For example:

``` cs
theButton.Events().Clicked.Subscribe(x => /* ... */);
```

This makes it far cleaner to compose complex UI interactions at the view level.

## A much improved API surface

This release is the "Clean up" breaking changes release - many deprecated methods have been removed - I'm proud to say that this release removes over 2x the number of lines that it adds, and only leaves what Should Be There. 

Many names have been changed to be more clear, methods have been removed or clarified, and in general, ReactiveUI 5.0 is a more pleasant framework to use. However, this means that moving from RxUI 4.x to 5.x can be a bit of work. Check out [the migration guide](https://github.com/reactiveui/ReactiveUI/blob/main/docs/migrating-from-rxui4.md) for more information. 

## Testable Initialization

ReactiveUI now has much more straightforward initialization - you can initialize ReactiveUI yourself in a test runner, ensure that tests won't register over other tests, and there is a new Service Locator implementation that is much more flexible with regard to object lifetimes than the old built-in service locator. RxApp itself now has much less in it, relying instead on the new `RxApp.DependencyResolver` property. 
