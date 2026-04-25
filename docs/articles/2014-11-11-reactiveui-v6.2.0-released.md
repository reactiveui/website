---
NoTitle: true
IsBlog: true
Title: ReactiveUI v6.2.0 released
Tags: Release Notes
Author: Anaïs Betts
Published: 2014-11-11
---

## [What's New](https://github.com/reactiveui/ReactiveUI/compare/6.1.0...6.2.0)

### Xamarin.iOS 64-bit support (#747)

ReactiveUI now fully supports the new 64-bit Xamarin.iOS API, if you are running Xamarin.iOS 8.4 or higher (currently in the Beta channel).

### Small Fixes
- Derived collections now have an `onRemove` method that can be used to clean up objects that are removed from the collection (#744, thanks @TheGrandUser)
- Remove opportunistic scheduling in Cocoa because of ordering issues (#745, thanks @tberman)
- Fix crashes where section changes could crash the app on iOS (#749, thanks @srimoyee-factory)
- Fix a crash when using `x => x` as your selector (#741, thanks @eggapauli)
- Propagate WhenActivated to subviews on iOS (#735, thanks @justin-factory)
- Fix issue where `INotifyPropertyChanging` was not defined on all platforms
