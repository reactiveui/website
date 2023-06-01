---
title: ReactiveUI v6.1.0 released
category: Release Notes
author: Anaïs Betts
---

## [What's New](https://github.com/reactiveui/ReactiveUI/compare/6.0.7...6.1.0)

### ViewModel-based Routing for Xamarin.Forms Apps (#724)

Several small fixes have come together to enable ReactiveUI ViewModel-based Routing and auto-serialization for Xamarin.Forms-based apps:

<img src="https://camo.githubusercontent.com/d158f56ab9c22c212ff3758d1d156be12a87c2ad/687474703a2f2f636c2e6c792f696d6167652f323631333173326e336b33482f636f6e74656e7423706e67" width=50% alt=""/>

See [the PR for a sample app](https://github.com/reactiveui/ReactiveUI/pull/724), and check out the [documentation on Routing](https://github.com/reactiveui/ReactiveUI/blob/main/docs/basics/routing.md) for more information on how it works.

### Other Fixes
- Added a version of `TestScheduler.With` that is async/await aware
- Added a SupportLib version of ReactiveFragment
