---
Order: 4
---
# Boilerplate Code

A view model repeats the same few shapes. Each read-write property needs a backing field and a setter that calls
`RaiseAndSetIfChanged`. Each output property needs an `ObservableAsPropertyHelper<T>` field, a getter that returns
its `Value`, and a `ToProperty` call. Each command needs a property and a `ReactiveCommand.Create` call.
[View Models](index.md#write-a-property-the-view-can-bind-to) shows each shape written by hand.

ReactiveUI brings two source generators that write these shapes for you. A **source generator** is a compiler add-on
that writes C# code while your project builds. You install nothing extra: every project that references a ReactiveUI
package gets both.

## Let the generators write a view model

1. **Make the class `partial`.** The generators add their code to the other part of the class.
2. **Add `using ReactiveUI.SourceGenerators;`** to each file that uses `[Reactive]` or `[ReactiveCommand]`.
   ReactiveUI adds no global `using` for that namespace.
3. **Mark each read-write property `[Reactive]`.** Declare it as a `partial` property with an empty `get` and `set`.
4. **Mark each command method `[ReactiveCommand]`.** The generator writes a property named after the method, followed
   by `Command`.
5. **Mark each output property `[ObservableAsProperty]`.** Declare it as a `partial` get-only property, and assign
   the generated `_{name}Helper` field with `ToProperty` in the constructor.
6. **Build.** The generators write the property bodies, the fields and the commands.

[ReactiveUI.SourceGenerators](../../../source-generators/index.md#write-your-first-view-model) walks through a full
view model built this way.

## Which attribute writes which shape

| Shape you would write by hand | Attribute | Comes from | Reference |
|---|---|---|---|
| A read-write property that calls `RaiseAndSetIfChanged` | `[Reactive]` | ReactiveUI.SourceGenerators | [Properties with `[Reactive]`](../../../source-generators/index.md#properties-with-reactive) |
| A `ReactiveCommand` property that wraps a method | `[ReactiveCommand]` | ReactiveUI.SourceGenerators | [Commands with `[ReactiveCommand]`](../../../source-generators/index.md#commands-with-reactivecommand) |
| An output property backed by `ObservableAsPropertyHelper<T>` | `[ObservableAsProperty]` | ReactiveUI.Binding | [Declare the property with an attribute](../../../binding/properties.md#declare-the-property-with-an-attribute) |
| A property that follows an `ObservableCollection<T>` | `[ReactiveCollection]` | ReactiveUI.SourceGenerators | [Collections](../../../source-generators/index.md#collections) |
| The `IReactiveObject` members on a class with another base class | `[IReactiveObject]` | ReactiveUI.SourceGenerators | [Classes that cannot derive from `ReactiveObject`](../../../source-generators/index.md#classes-that-cannot-derive-from-reactiveobject) |

Use a `[Reactive]` partial property, not a `[Reactive]` field, for any property you pass to `WhenAnyValue`.
ReactiveUI.Binding cannot see a property generated from a field, so `WhenAnyValue` on it throws at run time.

## Analyzer messages

The ReactiveUI.SourceGenerators analyzers report an `RXUISG` warning or error when they cannot handle an attribute.
[Analyzer messages](../../../source-generators/index.md#analyzer-messages) lists each one and what it means.

A project that references ReactiveUI.SourceGenerators itself, at a version older than 4.0.0, fails to restore with
error NU1605. Remove that `PackageReference`, or set it to 4.0.0 or later.
