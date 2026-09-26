---
Order: 13
---
# Members ReactiveUI.SourceGenerators writes

[Run the complete page example](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/sourcegenerators/sourcegenerators.csproj).

[ReactiveUI.SourceGenerators](../source-generators/index.md) is another source generator. You mark a field
`[Reactive]`, a method `[ReactiveCommand]`, or a class `[IReactiveObject]`, and it writes the property, the command
or the notification members for you. Since ReactiveUI.Binding 8.3.0, `WhenAnyValue`, `WhenAny`, `Bind`, `OneWayBind`,
`BindCommand` and `ToProperty` all work on a member ReactiveUI.SourceGenerators writes. Earlier versions could not
see such a member, and the call fell back to the runtime stub.

ReactiveUI.Binding cannot read ReactiveUI.SourceGenerators's generated code directly; the two run as separate
generators in the same build. Instead, the binding generator reads the same attributes and follows the same rules
ReactiveUI.SourceGenerators uses to decide what it writes, so it knows the shape of the member without seeing the
code for it.

## Observe a property written from a field

`[Reactive]` on a private field named `_displayName` writes a public property named `DisplayName`. The example view
model below also writes a command, `SaveCommand`, from a method marked `[ReactiveCommand]`.

```csharp
public partial class ProfileViewModel : ReactiveObject
{
    [Reactive]
    private string _displayName = "Ada";

    public int Saves { get; private set; }

    [ReactiveCommand]
    private void Save() => Saves++;
}
```

`WhenAnyValue` reads `DisplayName` like any other property, even though your source never declares it: the property
exists only in the code ReactiveUI.SourceGenerators writes.

```csharp
ProfileViewModel profile = new ProfileViewModel();

using IDisposable subscription = profile.WhenAnyValue(x => x.DisplayName).Subscribe(Console.WriteLine);

profile.DisplayName = "Grace";
```

```text
Ada
Grace
```

## Observe a class marked IReactiveObject

`[IReactiveObject]` goes on a class that cannot derive from `ReactiveObject`, for example one that already derives
from something else. ReactiveUI.SourceGenerators makes it implement `IReactiveObject` and raises its notifications
for it. A `[Reactive]` field on that class still writes a property the same way.

```csharp
[IReactiveObject]
public partial class ProfileCard
{
    [Reactive]
    private string _title = "Engineer";
}
```

```csharp
ProfileCard card = new ProfileCard();

using IDisposable subscription = card.WhenAnyValue(x => x.Title).Subscribe(Console.WriteLine);

card.Title = "Lead engineer";
```

```text
Engineer
Lead engineer
```

## Bind a property written from a field

`Bind` connects `DisplayName` to a view property in both directions, the same way it connects a hand-written
property.

```csharp
ProfileViewModel profile = new ProfileViewModel();
ProfileView view = new ProfileView { ViewModel = profile };

using IDisposable binding = view.Bind(profile, x => x.DisplayName, v => v.NameText);

Console.WriteLine(view.NameText);

view.NameText = "Linus";

Console.WriteLine(profile.DisplayName);
```

```text
Ada
Linus
```

## Bind a button to a generated command

`BindCommand` connects `SaveCommand`, written from the `Save` method, to a button's click event.

```csharp
ProfileViewModel profile = new ProfileViewModel();
ProfileView view = new ProfileView { ViewModel = profile };

using IDisposable binding = view.BindCommand(profile, x => x.SaveCommand, v => v.Save);

view.Save.Press();

Console.WriteLine(profile.Saves);
```

```text
1
```

## When a member is still out of reach

A view member another source generator writes, such as a field a UI framework's XAML compiler adds to a partial
view class, is not one ReactiveUI.SourceGenerators writes, so ReactiveUI.Binding still has no generated binding for
it. A plain call on such a member reports RXUIBIND021 at build time and throws when it runs. Call the `Unsafe` twin
instead: [Unsafe twins and the runtime fallback](unsafe.md) shows `BindUnsafe` and `BindCommandUnsafe` on a view
built this way.

## Run the examples

The [documentation examples](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/tree/main/src/examples/Documentation)
live in folders named for their page. From the repository's `src` folder, run the example for this page:

```bash
dotnet run --project examples/Documentation/Pages/sourcegenerators/sourcegenerators.csproj -c Release
```
