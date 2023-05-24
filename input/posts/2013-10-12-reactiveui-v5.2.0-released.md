IsBlog: true
Title: ReactiveUI v5.2.0 released
Tags: Release Notes
Lead: Anaïs Betts
---

## [What's New](https://github.com/reactiveui/ReactiveUI/compare/5.1.2...5.2.0)

### iOS Improvements

This release adds Reactive versions of a number of common UIKit classes, such as UIImageView. RxUI also now comes with a new class for UITableViewSource, `ReactiveTableViewSource`. This class will wrap a `ReactiveList<ViewModel>` class and automatically animate in and out cells from the table view as they change (PR #377)

### Android improvements

This release adds basic binding support for common Android controls, thanks to #371. Thanks @oliverw!

### Combined Commands

`ReactiveCommand` now supports combining commands, so it's easy to create a command that invokes one or more "child" commands, check out #382 for more information.

### Modern Xamarin support

ReactiveUI is now built against the official Xamarin Rx binaries. This means that on MonoMac, you need to be up-to-date on the latest Mono install. 

### Bug Fixes
- Fixes to ViewLocator to be more helpful if registration isn't set up (#359, thanks @terenced!)
- Move RxUI.Mobile interfaces to the portable library so you can use them from PLibs (#364)
- Perf improvement when looking up interfaces (#366, thanks @2asoft!)
- Fixes to Auto Data Template (#367, thanks @chrisway!)
- Fix to AutoPersist (#383, thanks @vevix!)
- Updates to handle the latest Rx Microsoft release 
- Make sure ReactiveCommand's `IsExecuting` always comes back on the UI thread (#373)
