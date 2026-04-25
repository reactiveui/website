---
NoTitle: true
IsBlog: true
Title: ReactiveUI v8.0.1 released
Tags: Release Notes
Author: Geoffrey Huntley
Published: 2018-05-09
---

Elijah Reva noticed a `net461` regression that affected Windows Presentation Foundation and Windows Forms. During the netstandard refactor we missed renaming a compilation symbol which resulted in `PropertyChangedEventManager.DeliverEvent` being used when setting reactive properties. WeakEventManager [should not be used](https://github.com/reactiveui/ReactiveUI/issues/661) on these platforms and this hotfix disables it.

Thank you Elijah for sending in this the hotfix - check your email for an invitation to the GitHub organization!

As part of this release we had [8 commits](https://github.com/reactiveui/reactiveui/compare/8.0.0...8.0.1) which resulted in [3 issues](https://github.com/reactiveui/ReactiveUI/issues?milestone=10&state=closed) being closed.



__Windows Forms__

- [__#1620__](https://github.com/reactiveui/ReactiveUI/pull/1620) fix: disable WeakEventManager for net461 build (#1617)

__Windows Presentation Foundation__

- [__#1620__](https://github.com/reactiveui/ReactiveUI/pull/1620) fix: disable WeakEventManager for net461 build (#1617)

__Housekeeping__

- [__#1626__](https://github.com/reactiveui/ReactiveUI/pull/1626) housekeeping: resolve broken build due to NET45 compilation symbol
- [__#1625__](https://github.com/reactiveui/ReactiveUI/pull/1625) housekeeping: pin xamarin.forms version used by eventbuilder

### Where to get it
You can download this release from [nuget.org](https://www.nuget.org/packages/reactiveui/8.0.1)
