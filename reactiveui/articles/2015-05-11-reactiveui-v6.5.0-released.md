---
NoTitle: true
IsBlog: true
Title: ReactiveUI v6.5.0 released
Tags: Release Notes
Author: Anaïs Betts
Published: 2015-05-11
---

<!--excerpt-->

## [What's New](https://github.com/reactiveui/ReactiveUI/compare/6.4.0.1...6.5.0)

### Collection View improvements (#820)

Thanks to @kentcb, our support of UITableViewController and UICollectionViewController are **much** more reliable, and will correctly animate in items. A huge thanks for an awesome PR!

![](https://cloud.githubusercontent.com/assets/1901832/6593544/761d202e-c829-11e4-8b05-52a5a630a599.gif)

### Dependency version bumps

This version of ReactiveUI requires Xamarin.Forms 1.4.2 and the latest Xamarin.Android AppCompat library. This most likely won't be a problem, but if it is you can downgrade to 6.4.x. 

### Bug fixes
- Fix issue where WhenAnyObservable doesn't protect against null (#831, thanks @kentcb)
- Improve WeakEventHandler when working with Xamarin.Forms, prevent crash (#826, thanks @nsainaney)
- Allow null property name to be used with WhenAny (#811, thanks @asarium)
- Improve binding error logging (#840, thanks @bradtwurst)
- Add constructors to ReactiveActivity and ReactiveFragment to allow them to inherit a Java reference (#841, thanks @jonfuller)
- Fix Xamarin.Forms navigation with ViewModel-first navigation (#819, thanks @bratsche)
- Cleanup to activation on WP (#825, thanks @flagbug)
