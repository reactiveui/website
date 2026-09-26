---
Order: 20
---
# Snippets

Snippets are short code templates that expand as you type, so a common pattern such as a two-way binding or a
backing property needs no retyping. The **snippets** folder in the [ReactiveUI repository](https://github.com/reactiveui/reactiveui/)
holds a set for Visual Studio, JetBrains ReSharper and JetBrains Rider. Every shortcut starts with **rui**, so
autocomplete finds them together.

## Visual Studio

The `snippets/Visual Studio` folder holds one `.snippet` file per shortcut:

| Shortcut | Inserts |
|---|---|
| `ruib` | A `Bind` call |
| `ruibc` | A `BindCommand` call |
| `ruiowb` | A `OneWayBind` call |
| `ruicommand` | A `ReactiveCommand` property |
| `ruiinteraction` | An `Interaction<TInput, TOutput>` property |
| `ruioaph` | An `ObservableAsPropertyHelper<T>` field and its property |
| `ruiprop` | A property that raises a change notification |
| `ruiiv4` | The `DependencyProperty` an `IViewFor<T>` implementation needs |
| `ruiviewreg` | Registers a view with `Locator.CurrentMutable` |

1. Open **Tools** > **Code Snippets Manager**, set the language to **CSharp**, and click **Add**.
1. Point it at the folder that holds the `.snippet` files.

## JetBrains ReSharper

The `snippets/Resharper/RxUI.DotSettings` file holds ReSharper's live templates for the same shortcuts. Import it
through ReSharper's settings: **ReSharper** > **Manage Options**, then **Import and Export** > **Import from File**,
and select `RxUI.DotSettings`.

## JetBrains Rider

The `snippets/Rider/ReactiveUI.xml` file holds Rider's live templates for `ruiprop`, `ruib`, `ruibc` and `ruiowb`.
Import it through **Settings** > **Editor** > **Live Templates**: open the gear menu and choose **Import Settings**,
then select `ReactiveUI.xml`.
