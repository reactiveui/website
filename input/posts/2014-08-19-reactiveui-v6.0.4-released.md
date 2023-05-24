IsBlog: true
Title: ReactiveUI v6.0.4 released
Tags: Release Notes
Lead: Anaïs Betts
---

## [What's New](https://github.com/reactiveui/ReactiveUI/compare/6.0.3...6.0.4)

### Bug Fixes
- Fix SupportLib NuGet package (#692, thanks @shiftkey)
- Fix bug in WhenActivated where deactivation wouldn't run (#690, thanks @jlaanstra)
- Ensure SetItem fires the correct notification (#693, thanks @TheGrandUser)
- Fix activation on NSWindowController (#696, thanks @mteper)
- Use ExecuteAsync in InvokeCommand (#700, #701, thanks @jlaanstra + @timmit93)
- Ensure ToProperty properties fire Changing notification (#703)
- Don't set the Scheduler in Xamarin.Forms in the test runner. 
- Link Xamarin.Mac to NuGet Rx binaries instead of ones shipped in the framework, since Mono no longer ships them.
