---
NoTitle: true
Title: ReactiveCompositeCollections
Order: 4
---

> **Obsolete / historical:** preserved for context. `ReactiveCompositeCollections` is a third-party project, not
> part of ReactiveUI. Modern ReactiveUI projects should use [DynamicData](../../collections.md) for composing
> observable collections.

`ReactiveCompositeCollections`, by Brad Phelan, added an `ICompositeCollection<T>`. It supported `Select`,
`SelectMany` and `Where` the way `IEnumerable<T>` and `IObservable<T>` do, so a screen could compose and filter
several source lists into one collection declaratively. The library lives at
[Reactive Composite Collections (GitHub)](https://github.com/Weingartner/ReactiveCompositeCollections) and was
published on [NuGet](https://www.nuget.org/packages/ReactiveCompositeCollections/).

DynamicData covers the same ground today, with its own source lists, caches and operators, and is what
[Collections](../../collections.md) documents.
