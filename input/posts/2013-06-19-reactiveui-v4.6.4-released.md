NoTitle: true
IsBlog: true
Title: ReactiveUI v4.6.4 released
Tags: Release Notes
Author: Anaïs Betts
Published: 2013-06-19
---

## [What's New](https://github.com/reactiveui/reactiveui/compare/4.5.0...4.6.4)

## Notable Changes Since 4.5.0
- Improved support for ReactiveUI on iOS and Android, including Suspension Manager support
- A UINavigationController for iOS that participates in ViewModel-based view location
- `OrderedComparer`, a new way to write IComparable implementations using a LINQ'ish syntax
- Make `ReactiveDerivedCollection` read-only and fix notification bugs
- We now generate Symbol packages for ReactiveUI
- Fixes for BindCommand on WinRT, where EventArgs don't actually have to be derived from System.EventArgs
- ReactiveAsyncAction now signals completion
- Fixes bug where WP8 projects would incorrectly be flagged as running in the test runner
