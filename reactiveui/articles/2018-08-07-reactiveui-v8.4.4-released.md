---
NoTitle: true
IsBlog: true
Title: ReactiveUI v8.4.4 released
Tags: Release Notes
Author: Geoffrey Huntley
Published: 2018-08-07
---

Release 8.4.4 of ReactiveUI is available!

Unfortunately there was a [WPF regression](https://github.com/reactiveui/ReactiveUI/pull/1710) in the previous release which was promptly resolved by Glenn Watson within hours. Thank-you Michael Stonis for sending a pull-request that adds support for generating the events in Xamarin Essentials.

As part of this release we had [3 commits](https://github.com/reactiveui/reactiveui/compare/8.4.1...8.4.4).

__ReactiveUI Core__
- [__#1709__](https://github.com/reactiveui/ReactiveUI/pull/1709) fix: sourcelink wasn't working, now is

__ReactiveUI Events__
- [__#1682__](https://github.com/reactiveui/ReactiveUI/pull/1682) feature: generate events for xamarin essentials 

__Windows Presentation Foundation__
- [__#1710__](https://github.com/reactiveui/ReactiveUI/pull/1710) fix: viewhost templates/themes weren't included
