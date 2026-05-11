---
NoTitle: true
Title: Caliburn.Micro
Order: 4
---
## Caliburn.Micro vs. ReactiveUI

[Caliburn.Micro](https://github.com/Caliburn-Micro/Caliburn.Micro) is a long-running, convention-driven MVVM framework. It has carved out a strong niche on WPF and is actively maintained.

### Approach

Caliburn.Micro is built around **conventions** and **conductors/screens**. The view-model class name conventionally maps to a view (`ShellViewModel` -> `ShellView`); a method named `Save` on a view-model automatically binds to a `<Button x:Name="Save" />`; an `IScreen` defines an activation/deactivation lifecycle; `IConductor<T>` types compose screens into navigable hierarchies.

It is opinionated about app structure (Bootstrapper, Conductors, Screens) and largely leaves async coordination, derived state, and event composition to plain async/await + `INotifyPropertyChanged`.

### Where ReactiveUI differs

| Aspect | Caliburn.Micro | ReactiveUI |
|--------|---------------|-----------|
| View binding | Convention-based (`x:Name` matches a VM member) | Explicit (`this.Bind`, `this.OneWayBind`, `BindCommand`) with strongly-typed Expressions |
| Navigation | `Conductor<T>` / `Screen` lifecycle | `IScreen` + `RoutingState`, or [Sextant](../documentation/handbook/sextant/index.md) |
| Reactive composition | Not built in | First-class via Rx |
| Commands | `Task` / `void` methods bound by name | `ReactiveCommand<TParam, TResult>` with `IsExecuting` / `ThrownExceptions` |
| Activation | `IActivate` / `IDeactivate` | `IActivatableViewModel` + `WhenActivated` |
| DI | Bootstrapper-driven (you wire whatever container) | Splat by default, adapters for every common container |

### When to pick which

- Pick **Caliburn.Micro** when you want a strongly opinionated, convention-driven structure (especially for WPF), and you don't need reactive composition.
- Pick **ReactiveUI** when you want explicit, refactor-safe bindings (no string conventions), Rx-based composition, and cross-platform views beyond WPF.

### Mixing them

It's uncommon but technically possible: Caliburn.Micro relies on `INotifyPropertyChanged`, which ReactiveUI's `ReactiveObject` already implements. The two frameworks have overlapping ideas of "screen / conductor" vs "routable view-model / screen", so picking one as the navigation owner is recommended.
