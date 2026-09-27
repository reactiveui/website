---
NoTitle: true
Title: ReactiveCompositeCollections
Order: 4
---

> **Obsolete / historical:** preserved for context. `ReactiveCompositeCollections` is a third-party project, not
> part of ReactiveUI. Modern ReactiveUI projects should use [DynamicData](../../collections.md) for composing
> observable collections.

`ReactiveCompositeCollections`, by Brad Phelan, added an `ICompositeCollection<T>`. It supported `Select`,
`SelectMany` and `Where`, the way `IEnumerable<T>` and `IObservable<T>` do. That let a screen compose and filter
several source lists into one collection declaratively. The library lives at
[Reactive Composite Collections (GitHub)](https://github.com/Weingartner/ReactiveCompositeCollections). It was
published on [NuGet](https://www.nuget.org/packages/ReactiveCompositeCollections/) too.

DynamicData covers the same ground today. It has its own source lists, caches and operators.
[Collections](../../collections.md) documents DynamicData.
