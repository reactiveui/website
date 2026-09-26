---
NoTitle: true
Order: 2
---

> **Obsolete:** `ReactiveList` and its `CreateDerivedCollection` method were removed in ReactiveUI 9.

## CreateDerivedCollection

`CreateDerivedCollection` projected a `ReactiveList` into another list that stayed in step with the source: adding,
removing or filtering an item in the source updated the derived list automatically, and the derived list could also
sort and filter on its own terms.

The same projections — mapping, filtering and ordering a collection that updates as its source changes — are
available today through DynamicData's source lists and caches. See [DynamicData operators](../../collections.md).
