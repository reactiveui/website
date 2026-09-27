---
NoTitle: true
Order: 2
---

> **Obsolete:** `ReactiveList` and its `CreateDerivedCollection` method were removed in ReactiveUI 9.

## CreateDerivedCollection

`CreateDerivedCollection` projected a `ReactiveList` into another list that stayed in step with the source.
Adding, removing or filtering an item in the source updated the derived list automatically. The derived list
could also sort and filter on its own terms.

DynamicData's source lists and caches offer the same projections today. That means mapping, filtering and
ordering a collection so it updates as its source changes. See [DynamicData operators](../../collections.md).
