NoTitle: true
IsBlog: true
Title: ReactiveUI v6.0.3 released
Tags: Release Notes
Author: Anaïs Betts
Published: 2014-08-03
---

## [What's New](https://github.com/reactiveui/ReactiveUI/compare/6.0.2...6.0.3)

### Bug Fixes
- Fixed an issue where subscribing to `Changed` could cause weird crashes (#665, thanks @sickboy + @mteper)
- Fix a bug where ReactiveCommand wouldn't correctly marshal exceptions (#686, thanks @flagbug)
- Don't leak an event to `IsHitTestVisible` when using `WhenActivated` (#689, thanks @jlaanstra)
