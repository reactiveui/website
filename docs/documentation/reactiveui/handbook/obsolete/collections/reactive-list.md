---
NoTitle: true
Title: ReactiveList
Order: 3
---

> **Obsolete:** `ReactiveList` was removed in ReactiveUI 9. Use [DynamicData](../../collections.md) for reactive
> collections.

`ReactiveList` was an `ObservableCollection<T>` with extra Rx-flavored notifications: streams for items added,
removed or moved, a `Changed` stream of `NotifyCollectionChangedEventArgs`, per-item change tracking through
`ChangeTrackingEnabled`, and `SuppressChangeNotifications` to batch several edits into one update.

DynamicData's source lists and caches cover the same ground today: a change-set stream instead of a set of separate
`ItemsAdded` / `ItemsRemoved` streams, and operators that batch, filter and transform without an intermediate
`ReactiveList`. See [Collections](../../collections.md) for the current API, including
[`ActOnEveryObject`](../../collections.md#track-every-add-and-remove) and
[change sets](../../collections.md#observe-changes-as-a-change-set).
