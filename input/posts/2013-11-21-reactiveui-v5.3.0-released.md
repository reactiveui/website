IsBlog: true
IsPost: true
Title: ReactiveUI v5.3.0 released
Tags: Release Notes
Lead: Anaïs Betts
---

## [What's New](https://github.com/reactiveui/ReactiveUI/compare/5.2.0...5.3.0)

### Improved iOS Table View / Collection View support

Thanks to @meteficha and @alanpog, ReactiveUI's support for UITableView and UICollectionView is now vastly improved. We now support:
- Custom Section Headers and Footers
- Support for UICollectionView via new ReactiveCollectionViewSource class, similar to ReactiveTableViewSource
- Support for adding / removing sections dynamically in a Reactive way, via the `Data` property on ReactiveTableViewSource
- Added several new IViewFor-friendly Cocoa view subclasses, such as ReactiveCollectionViewCell
- You can now easily detect when the table has finished updating to avoid making changes during a table reshuffle, via `DidPerformUpdates`

### ViewModelViewHost and RoutedViewHost for WinForms

Thanks to @rikbosch, ReactiveUI.WinForms now has support for both RoutedViewHost and ViewModelViewHost, so you can create IViewFor-based Controls. (#396) 

### Bug Fixes
- Make FuncDependencyResolver handle `GetService` correctly via returning the last item (#389), thanks @journeyman!
- Fix a race condition in ObservableAsyncMRUCache, thanks @npnelson!
- Create an overload of ObservableForProperty that's a bit more F# friendly, thanks @marklam!
- Code cleanups to our project files (#387), thanks @pH-minamo!
