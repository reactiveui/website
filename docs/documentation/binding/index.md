---
Order: 1
---
# ReactiveUI.Binding

[Run the complete page example](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/index/index.csproj).

A screen shows data that changes. When the title of a to-do item changes, the label that shows it must change too.
When the user types in a box, the view model must learn the new text.
Writing that code by hand means an event handler for every pair of properties, and a handler you forget to remove.

ReactiveUI.Binding keeps a view and a view model in step. You write one line that names the two properties,
and the library does the listening, the copying and the cleanup.

A **source generator** is a compiler add-on that writes code while your project builds. ReactiveUI.Binding reads
each place where you observe or bind a property and writes the code for that place. Nothing looks up
a property by name at run time. The result is safe to trim and to publish with Native AOT,
and a binding costs little to create and to run.

The generated code takes part in your build like any file you wrote. You can read it,
step through it in a debugger, and the compiler checks it against your types.

## Install

Install the `ReactiveUI.Binding` NuGet package in your view model project and in your view project.
The package carries the runtime library, the source generator and an analyzer. The analyzer reports
binding calls that the generator cannot handle, as you type.

```bash
dotnet add package ReactiveUI.Binding
```

The example on this page shows a label from .NET MAUI, so its project also references the MAUI package,
`ReactiveUI.Binding.Maui`. [Threading and platforms](threading.md) explains what the platform packages add.
[Setup](setup.md) covers the builder, the analyzer and Native AOT.

These examples use .NET 10 and C# 14. They run against a store that keeps its data in memory,
so you can run them as they are.

## Your first binding

The walkthrough below uses a to-do screen. `TodoListViewModel` loads items from a store and counts how many
are left to do. `TodoView` holds the controls, among them a `RemainingLabel` that should show that count.
A real UI framework builds the controls from markup. The example view creates them in code.

**1. Load a view model.** The code below creates the view model with an in-memory store and loads its items.
A binding needs data to show, so this gives the later steps four to-do items to work with.

```csharp
var viewModel = new TodoListViewModel(InMemoryTodoStore.CreateSeeded());

await viewModel.LoadAsync();

Console.WriteLine(viewModel.Items.Count);
```

```text
4
```

**2. Observe a property.** `WhenChanged` takes a lambda that names a property and returns a stream of that property's values.
The stream is an `IObservable<T>`. Nothing happens until you **subscribe**. A subscription starts the listening and
tells the stream what to do with each value.

The code below watches the title of the first item and prints every title the stream delivers.
It then renames the item, so you can see the stream report the change without any polling.

```csharp
var viewModel = new TodoListViewModel(InMemoryTodoStore.CreateSeeded());
await viewModel.LoadAsync();
var registration = viewModel.Items[0];

using (registration.WhenChanged(static x => x.Title).Subscribe(Console.WriteLine))
{
    registration.Title = RenamedTitle;
}
```

The first value is the current title. Later values arrive each time the title changes.
The example prints the current title, then the new one.

```text
Renew car registration
Renew car registration online
```

The lambda is `static x => x.Title`. Mark a lambda `static` when it uses nothing from the method around it.
The generator reads the lambda's text at build time, so the lambda has to be written inline at the call.
The `Subscribe` overload that takes a delegate comes from `ReactiveUI.Primitives`, which the example imports with a `using`.

`Subscribe` returns an `IDisposable`. Disposing it ends the subscription. The `using` statement disposes it at the closing brace,
so the subscription is gone when the block ends. Dispose every subscription you create.
A subscription you keep alive holds the object it listens to in memory.

The [Observing](observing.md) page covers watching one to sixteen properties at once, paths that cross several objects,
and the before-change form, `WhenChanging`.

**3. Bind a property to a control.** `BindOneWay` copies a view model property to a view property and keeps the copy up to date.
The code below shows the number of unfinished items in a label, then finishes an item.
You never write the update yourself: the label follows the view model.

```csharp
var viewModel = new TodoListViewModel(InMemoryTodoStore.CreateSeeded());
var view = new TodoView();
await viewModel.LoadAsync();

using (viewModel.BindOneWay(view, static x => x.RemainingCount, static v => v.RemainingLabel.Text, static count => count.ToString(CultureInfo.InvariantCulture)))
{
    Console.WriteLine(view.RemainingLabel.Text);

    viewModel.SelectedItem = viewModel.Items[0];
    await viewModel.CompleteAsync();

    Console.WriteLine(view.RemainingLabel.Text);
}
```

The label shows the count the moment the binding is created. It shows a new count each time an item is finished.
The example selects the first item and finishes it, so the count falls from three to two.

```text
3
2
```

The call takes the view model, which is the source, and the view, which is the target.
Two lambdas name the properties. `RemainingCount` is an `int` and the label's `Text` is a `string`, so
the last argument converts one to the other. The example adds `using System.Globalization;` for `CultureInfo`.

The target lambda, `static v => v.RemainingLabel.Text`, is a path. A **property path** is a chain of properties,
here `RemainingLabel` and then `Text`. A binding follows each link of the chain. If a link on the way changes,
the binding moves to the new object.

The binding also picks the thread it writes on. A UI framework lets only one thread change a control.
`BindOneWay` writes to the target on the thread that owns it. [Threading and platforms](threading.md) shows how.

**4. Stop the binding.** `BindOneWay` returns an `IDisposable`, the same as `Subscribe`.
Dispose it to stop the binding.
The code below creates a binding, disposes it at once, and then finishes an item.
It shows that a disposed binding stops writing to the view, so a screen that closes leaves nothing behind.

```csharp
var binding = viewModel.BindOneWay(view, static x => x.RemainingCount, static v => v.RemainingLabel.Text, static count => count.ToString(CultureInfo.InvariantCulture));
binding.Dispose();

viewModel.SelectedItem = viewModel.Items[0];
await viewModel.CompleteAsync();

Console.WriteLine(view.RemainingLabel.Text);
```

The label keeps the text it had when the binding was disposed. Finishing an item no longer changes it.

```text
3
```

## What happens at build time

For each call to `WhenChanged`, `BindOneWay` and the other binding methods, the generator writes a method
that does the work for that call. It reads the property names from your lambdas and picks the way to
listen for changes from the types of the objects. A type that implements `INotifyPropertyChanged`
announces a change by raising its `PropertyChanged` event. Other types announce changes in other ways.
The [Mechanisms](mechanisms.md) page explains how the generator picks one.

The generated method reaches your call in one of two ways. On a compiler that supports interceptors,
the compiler replaces your call with the generated method. Otherwise the generator adds an overload
that the compiler picks in place of the library's own method. Both ways run the generated code.
Your source stays as you wrote it.

The library's own methods, such as the `WhenChanged` you call, are stubs. A stub throws an
`InvalidOperationException` when it runs, and the message names the `Unsafe` overload to use instead.
You meet the stub when a call site is one the generator cannot read, for example when you pass a lambda that
you built earlier and stored in a variable. The analyzer reports such a call while you edit,
with a diagnostic whose id starts with `RXUIBIND`. [Setup](setup.md) lists the diagnostics.

The `Unsafe` overloads find properties by reflection while the program runs. They accept expressions
the generator cannot read, and they cost more and are not safe for trimming.
[Unsafe twins and the runtime fallback](unsafe.md) covers them.

## The five sample apps

Every page in this section uses the same sample apps, so a type you meet on one page is the same type on the next.

| App | What it models |
| --- | --- |
| To-do list | A list with a filter box, a count of unfinished items and commands to add and finish an item. |
| GitHub issues | An issue board with repositories, issues, comments and users. |
| Cloud storage | A storage browser with buckets, objects, uploads and a connection state. |
| School | A gradebook with students, courses, enrolments, assignments and grades. |
| Bank | Accounts, payees and transfers with validation. |

The apps live in the [Common folder](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/tree/main/src/examples/Documentation/Common)
of the examples. They talk to in-memory services, so no example needs a network or a database.

## Find a topic

| Page | What you can do |
| --- | --- |
| [Observing](observing.md) | Watch one to sixteen properties with `WhenChanged`, `WhenChanging`, `WhenAnyValue`, `WhenAny` and `WhenAnyObservable`. |
| [Bindings](bindings.md) | Connect properties one way and two ways, bind to a command or an interaction, and handle errors. |
| [Converters](converters.md) | Use the built-in converters for numbers, dates, booleans, strings and nullable values. |
| [Custom converters](custom-converters.md) | Write and register your own converter and choose a fallback. |
| [Mechanisms](mechanisms.md) | See how a type announces a change and how the generator picks a way to listen. |
| [Views](views.md) | Use `IViewFor<T>`, the view locator and view mappings. |
| [Threading and platforms](threading.md) | Write on the owning thread, choose a scheduler, and use the WPF, WinForms, MAUI and Avalonia packages. |
| [Setup](setup.md) | Start the builder, choose the System.Reactive package, publish with Native AOT and read the analyzer's diagnostics. |
| [Unsafe twins and the runtime fallback](unsafe.md) | Bind a call site that the generator cannot read. |
| [API reference](api.md) | Look up every public type and member with its parameters and return value. |

## Run the examples

The [documentation examples](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/tree/main/src/examples/Documentation)
live in folders named for their page. Each page links to its folder. Each folder is a small program that
prints what the page shows. The page examples reference the library source projects, including the generator that writes the binding code.

The code on a page is an excerpt of the real example. Setup and helper code stay in the project when a page does not need them.
An example prints its results with `Console.WriteLine`, and the page shows those lines in a block under the code.

From the repository's `src` folder, run the example for this page:

```bash
dotnet run --project examples/Documentation/Pages/index/index.csproj -c Release
```

## API reference

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`WhenChanged`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenChanged.WideArity.cs) | Watches a property. It delivers the current value when you subscribe, then the new value after each change. | Extension method on `TObj : class`. Takes `Expression<Func<TObj, T1>> property1`. Returns [`IObservable<T1>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1). Other forms take up to 16 properties and return `PropertyValues<T1..T16>`. | The lambda has to be written inline at the call. A call the generator cannot read throws `InvalidOperationException` when it runs. The [Observing](observing.md) page covers the other forms. |
| [`WhenChangedUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.WhenChanged.WideArity.Unsafe.cs) | Watches a property by reflection, for a lambda the generator cannot read. | Extension method on `TObj : class`. Takes `Expression<Func<TObj, T1>> property1`. Returns `IObservable<T1>`. Other forms take up to 16 properties. | Carries `RequiresUnreferencedCode`. Throws `ArgumentNullException` for a null object or lambda. |
| [`BindOneWay`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.Binding.cs) | Copies a source property to a target property, then again after each change. It writes on the thread that owns the target. | Extension method on `TSource : class`. Takes `TTarget : class target`, a source lambda and a target lambda. The target lambda may be a property path. Add a `Func<TSourceProp, TTargetProp> conversionFunc` when the two types differ. Returns [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable). | Dispose the result to stop the binding. A call the generator cannot read throws `InvalidOperationException` when it runs. Forms that take an `ISequencer` scheduler or an `IBindingTypeConverter` are declared in [`ReactiveSchedulerExtensions`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveSchedulerExtensions.cs). The [Bindings](bindings.md) page covers them. |
| [`BindOneWayUnsafe`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/ReactiveUIBindingExtensions.Binding.Unsafe.cs) | Binds one way by reflection, for a lambda the generator cannot read. | Extension method on `TSource : class`. Takes the same arguments as `BindOneWay`, with or without a `conversionFunc`. Returns `IDisposable`. | Carries `RequiresUnreferencedCode`. [Unsafe twins and the runtime fallback](unsafe.md) covers it. |
