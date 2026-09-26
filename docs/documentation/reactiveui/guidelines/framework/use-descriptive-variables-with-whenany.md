# Use descriptive variables in your WhenAny

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/guidelines/guidelines.csproj).

`WhenAny` watches several properties at once and passes each one's latest value to a `selector` lambda you
write. Name the selector's parameters after what they mean, not after their position, so the expression reads
like the rule it checks.

## Name each parameter

`PreferDescriptiveParameterNames` watches two properties on a nested `Enrollment` object and combines them into
one boolean:

```csharp
using IDisposable subscription = student.WhenAny(
        x => x.Enrollment.IsEnabled,
        x => x.Enrollment.IsLoading,
        static (isEnabled, isLoading) => isEnabled.Value && isLoading.Value)
    .Subscribe(canJoin => log.Add(canJoin ? "Can join now" : "Cannot join yet"));
```

`isEnabled` and `isLoading` say what each value means, so `isEnabled.Value && isLoading.Value` reads as the
rule it is: join only once enrollment is both enabled and finished loading.

`AvoidUnnamedParameters` runs the identical rule, but names the same two parameters `x` and `y`:

```csharp
using IDisposable subscription = student.WhenAny(
        x => x.Enrollment.IsEnabled,
        x => x.Enrollment.IsLoading,
        static (x, y) => x.Value && y.Value)
    .Subscribe(canJoin => log.Add(canJoin ? "Can join now" : "Cannot join yet"));
```

Both compile, and both send the same values here:

```text
Cannot join yet, Can join now
```

The difference is not in what the code does. It is in what a reader has to do to understand it: with `x` and
`y`, you have to look back at the two expressions above the selector to work out which is which. With
`isEnabled` and `isLoading`, the selector already tells you.

## Why this matters

This guideline is about readability, not runtime behavior. It matters most in a boolean expression like the one
above, where `x.Value && y.Value` gives no clue what either side means. It matters more the more properties a
`WhenAny` call watches.

## At a glance

| Do | Instead of | Why |
|---|---|---|
| Name each `WhenAny` selector parameter after what it means | `x`, `y` positional names | The expression reads as the rule it checks. |
