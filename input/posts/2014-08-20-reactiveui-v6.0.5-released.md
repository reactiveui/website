NoTitle: true
IsBlog: true
Title: ReactiveUI v6.0.5 released
Tags: Release Notes
Author: Anaïs Betts
Published: 2014-08-20
---

## [What's New](https://github.com/reactiveui/ReactiveUI/compare/6.0.4...6.0.5)

### ToProperty and ReactiveCommand are no longer lazy in a test runner

In certain situations, tests that appear to be reasonable would fail, due to a View not being bound to the ViewModel under test. This is due to an optimization that was added in the RxUI 6.0 era, where subscriptions would not be created until the value was requested for the first time. 

While the behavior is correct, it also resulted in tricky-to-debug test failures. Now, in the unit test runner, we disable this feature and eagerly subscribe to the source. Check out #705 for more information.

### Bug Fixes
- ReactiveUI is now built using the Xamarin Stable branch, which means that RxUI-Events for iOS is now based on iOS 7.x instead of being based on the iOS 8 Beta
- Fixed typo in error message (#704, thanks @mteper)
