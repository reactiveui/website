---
Order: 10
---
# Setup

[Run the complete page example](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/setup/setup.csproj).
A second project covers the [Native AOT program](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/setup/aot/aot.csproj).

A binding call such as `item.WhenChanged(x => x.Title)` has to do real work when the program runs. Something must
watch the property, convert values, write to controls and find views. This page shows how to install the package
and how to start the library when your program starts.
It also shows how to read the build messages that say a call cannot get generated code.

One package brings the generator and the calls. A program that has its own converters, observation providers,
command binders, platform modules or view mappings also builds a *builder* at startup and registers them.
Later sections cover the System.Reactive package's names, Native AOT, two build properties and every analyzer message.

## Get started

**1. Add the package.** One reference brings the runtime library, the source generator and the analyzer.
A *source generator* is a compiler add-on that writes C# code while your project builds. The generator writes
one method for every binding call in your code, and the analyzer reports the calls it cannot handle.

```bash
dotnet add package ReactiveUI.Binding
```

**2. Import the namespace.** A global using in the project file makes the library available in every file.
You can write `using ReactiveUI.Binding;` at the top of each file instead. Import `ReactiveUI.Binding`, not
`ReactiveUI`; the [analyzer section](#keep-dispatch-in-reach) shows why.

```xml
<ItemGroup>
  <Using Include="ReactiveUI.Binding" />
</ItemGroup>
```

**3. Write a call.** The snippet below watches the title of a to-do item and prints it each time it changes. The lambda
`x => x.Title` is the *property path*: the properties the call follows, from the object to the value. `WhenChanged`
delivers the current value first and then every new one, and the `using` block disposes the subscription when you are done.

```csharp
var item = new TodoItem { Title = "Renew car registration" };

using (item.WhenChanged(x => x.Title).Subscribe(Console.WriteLine))
{
    item.Title = RenamedTitle;
}
```

```text
Renew car registration
Renew car registration online
```

[Observing properties](observing.md) covers `WhenChanged` and its relatives. [Binding](bindings.md) covers the calls that
write to a control.

**4. Build the builder at startup.** The snippet below registers every kind of service this page covers in one chain.
Do this once, when the program starts, because a converting binding reads the converters that `BuildApp()` publishes.
The chain starts with `RxBindingBuilder.CreateReactiveUIBindingBuilder()` and ends with `BuildApp()`. Each `With` method adds one
piece of configuration and returns the builder, so the calls chain.

```csharp
IReactiveUIBindingBuilder builder = RxBindingBuilder.CreateReactiveUIBindingBuilder();

return builder
    .WithCoreServices()
    .WithMaui()
    .WithPlatformModule(new TodoModule())
    .WithConverter(new TagListToStringConverter())
    .WithFallbackConverter(new EnumNameFallbackConverter())
    .WithSetMethodConverter(new TagListSetMethodConverter())
    .WithCommandBinder(binder)
    .WithRegistration(resolver => resolver.RegisterLazySingleton<ICreatesObservableForProperty>(() => provider))
    .ConfigureViewLocator(static mappings => mappings.Map<TodoListViewModel, TodoView>())
    .BuildApp();
```

The next sections take these calls one at a time. `BuildApp()` finishes the work. It does three things:

- It builds the configured services.
- It publishes the converters to `BindingConverters.Current`, the store that every binding reads. That store is empty until `BuildApp` runs.
- It marks the library as started, which is what `EnsureInitialized` checks.

`BuildApp()` returns an `IReactiveUIBindingInstance`. Its `Current` property is the resolver that reads the configured services.
Keep the instance if later code reads services from it, as the sections below do.

**5. Check that the library has started.** `RxBindingBuilder.EnsureInitialized()` throws an `InvalidOperationException` when
`BuildApp` has not run. The snippet below calls it too early and catches the exception, so you can see the guard work.
No binding call runs this check for you, so use it at the top of startup code that must not run early.

```csharp
try
{
    RxBindingBuilder.EnsureInitialized();
}
catch (InvalidOperationException)
{
    Console.WriteLine("Call BuildApp first");
}
```

```text
Call BuildApp first
```

After `BuildApp`, the same call returns and does nothing. The snippet below makes the call after the build and checks
that the built application holds a resolver, which shows startup finished.

```csharp
RxBindingBuilder.EnsureInitialized();

Console.WriteLine(app.Current is not null);
```

```text
True
```

## Create the builder

The builder is a `ReactiveUIBindingBuilder`. It configures a Splat dependency resolver, which is the container that
holds the library's services. `RxBindingBuilder.CreateReactiveUIBindingBuilder()` uses the application-wide resolver.
The builder owns a `ConverterService` that holds the three converter registries. The snippet below creates a builder and asks
for an `int` to `string` converter. The lookup finds nothing, which shows that a new builder registers no converters until
`WithCoreServices` runs.

```csharp
var builder = RxBindingBuilder.CreateReactiveUIBindingBuilder();

Console.WriteLine(builder.ConverterService.TypedConverters.TryGetConverter(typeof(int), typeof(string)) is null);
```

```text
True
```

To configure a resolver you own, call the `CreateReactiveUIBindingBuilder` extension on it, or pass the resolver to the
`ReactiveUIBindingBuilder` constructor. The builder's `CurrentMutable` is that resolver. The snippet below tries both routes
and checks that the builder configures the resolver you passed. Use this in a test or in a program that keeps its own container,
so the configuration never touches the shared resolver.

```csharp
using ModernDependencyResolver resolver = new();
var fromResolver = resolver.CreateReactiveUIBindingBuilder();

Console.WriteLine(ReferenceEquals(resolver, fromResolver.CurrentMutable));

using ModernDependencyResolver other = new();
ReactiveUIBindingBuilder direct = new(other, null);

Console.WriteLine(ReferenceEquals(other, direct.CurrentMutable));
```

```text
True
True
```

The second constructor argument is the resolver that reads the configured services. It may be `null`.

### Register the core services

`WithCoreServices()` registers the default converters, two observation providers and a default view locator.
The providers watch types that raise `PropertyChanged` and plain types with a `{PropertyName}Changed` event.
Calling `WithCoreServices` a second time registers nothing more. Call it first in the chain.

`ReactiveUIBindingModule` registers only the two providers, `INPCObservableForProperty` and `POCOObservableForProperty`.
Use it on its own when you configure a resolver without a builder. The snippet below runs the module against an empty
resolver and lists the providers that appear. It shows that these two are the whole of the core observation support.

```csharp
using ModernDependencyResolver resolver = new();
ReactiveUIBindingModule module = new();

module.Configure(resolver);

var providers = resolver.GetServices<ICreatesObservableForProperty>().Select(static service => service.GetType()).ToList();

Console.WriteLine(providers.Count);
Console.WriteLine(providers.Contains(typeof(INPCObservableForProperty)));
Console.WriteLine(providers.Contains(typeof(POCOObservableForProperty)));
```

```text
2
True
True
```

### Chain from the app builder interface

A builder that you hold as Splat's `IAppBuilder` has none of the binding methods. The
`BuilderMixins` extension methods on `IAppBuilder` add them. `BuildApp`, `WithPlatformModule`, `WithRegistration`,
`WithConverter`, `WithFallbackConverter`, `WithSetMethodConverter` and `ConfigureViewLocator` all exist on `IAppBuilder`. Each one
throws an `InvalidOperationException` when the builder behind the interface is not a ReactiveUI.Binding builder.
The snippet below holds the builder as an `IAppBuilder` and builds through the extension. It shows that `BuildApp` finds the
binding builder behind the interface and still returns a working application.

```csharp
using ModernDependencyResolver resolver = new();
IAppBuilder appBuilder = resolver.CreateReactiveUIBindingBuilder();

var app = appBuilder.BuildApp();

Console.WriteLine(app.Current is not null);
```

```text
True
```

A builder that you hold as `ReactiveUIBindingBuilder` has every method as a member. Each call returns the builder itself, and
`WithRegistration` runs its action against the resolver at once. The snippet below configures the builder one member at a time,
builds the application, and then checks each result. The class member `WithCoreServices` returns `IAppBuilder`, so the
snippet casts to the interface to get the binding builder back. The nine `True` lines show that every registration reached the
resolver and that the builder returned itself each time.

```csharp
using ModernDependencyResolver resolver = new();
var builder = resolver.CreateReactiveUIBindingBuilder();
TodoItemObservableForProperty provider = new();
ButtonCommandBinder binder = new();

var core = ((IReactiveUIBindingBuilder)builder).WithCoreServices();
var module = builder.WithPlatformModule(new TodoModule());
_ = builder.WithFallbackConverter(new EnumNameFallbackConverter());
_ = builder.WithSetMethodConverter(new TagListSetMethodConverter());
_ = builder.WithCommandBinder(binder);
_ = builder.WithRegistration(registry => registry.RegisterLazySingleton<ICreatesObservableForProperty>(() => provider));
_ = builder.ConfigureViewLocator(static mappings => mappings.Map<TodoListViewModel, TodoView>());

var app = builder.BuildApp();

TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());

Console.WriteLine(ReferenceEquals(core, builder));
Console.WriteLine(ReferenceEquals(module, builder));
Console.WriteLine(builder.ConverterService.TypedConverters.TryGetConverter(typeof(int), typeof(string)) is not null);
Console.WriteLine(builder.ConverterService.FallbackConverters.TryGetConverter(typeof(TodoPriority), typeof(string)) is EnumNameFallbackConverter);
Console.WriteLine(builder.ConverterService.SetMethodConverters.TryGetConverter(typeof(List<string>), typeof(string)) is TagListSetMethodConverter);
Console.WriteLine(ReferenceEquals(resolver.GetService<ICreatesCommandBinding>(), binder));
Console.WriteLine(resolver.GetServices<ICreatesObservableForProperty>().Any(service => ReferenceEquals(service, provider)));
Console.WriteLine(resolver.GetService<IViewLocator>()!.ResolveView(viewModel) is TodoView);
Console.WriteLine(app.Current is not null);
```

```text
True
True
True
True
True
True
True
True
True
```

## Register converters

A *converter* turns a value of one type into a value of another. A label shows text, so a binding from a list of
tags to a label needs a converter from the list to `string`. The builder holds three kinds. The
[converters](converters.md) page covers the built-in ones. [Custom converters](custom-converters.md) shows how to write your own.

| Method | Registers | Use it for |
|--------|-----------|------------|
| `WithConverter` | An `IBindingTypeConverter` | One pair of types, such as a list of tags to `string` |
| `WithFallbackConverter` | An `IBindingFallbackConverter` | A rule checked at run time that covers many types, such as any enumeration to its name |
| `WithSetMethodConverter` | An `ISetMethodBindingConverter` | Writing a value into an indexed position, such as one item of a list |

The snippet below registers one converter of each kind through the `IAppBuilder` extensions. It then asks each registry
for a converter by its types and checks that the answer is the one you registered. This shows the three registries are
separate, and that a lookup finds your converter by the pair of types.

```csharp
using ModernDependencyResolver resolver = new();
var builder = resolver.CreateReactiveUIBindingBuilder();
IAppBuilder appBuilder = builder;

_ = appBuilder
    .WithConverter(new TagListToStringConverter())
    .WithFallbackConverter(new EnumNameFallbackConverter())
    .WithSetMethodConverter(new TagListSetMethodConverter());

Console.WriteLine(builder.ConverterService.TypedConverters.TryGetConverter(typeof(IReadOnlyList<string>), typeof(string)) is TagListToStringConverter);
Console.WriteLine(builder.ConverterService.FallbackConverters.TryGetConverter(typeof(TodoPriority), typeof(string)) is EnumNameFallbackConverter);
Console.WriteLine(builder.ConverterService.SetMethodConverters.TryGetConverter(typeof(List<string>), typeof(string)) is TagListSetMethodConverter);
```

```text
True
True
True
```

The fallback and set-method methods return the builder itself, so you can chain from them too. The snippet below registers
the two converters one call at a time and checks that each call returned the builder, and that each registry holds its
converter afterwards.

```csharp
using ModernDependencyResolver resolver = new();
var builder = resolver.CreateReactiveUIBindingBuilder();
IAppBuilder appBuilder = builder;

var fallback = appBuilder.WithFallbackConverter(new EnumNameFallbackConverter());
var setMethod = appBuilder.WithSetMethodConverter(new TagListSetMethodConverter());

Console.WriteLine(ReferenceEquals(builder, fallback));
Console.WriteLine(ReferenceEquals(builder, setMethod));
Console.WriteLine(builder.ConverterService.FallbackConverters.TryGetConverter(typeof(TodoPriority), typeof(string)) is EnumNameFallbackConverter);
Console.WriteLine(builder.ConverterService.SetMethodConverters.TryGetConverter(typeof(List<string>), typeof(string)) is TagListSetMethodConverter);
```

```text
True
True
True
True
```

After `BuildApp`, a binding uses the registered converter. The snippet below binds the tags of the selected item to a label
with no converter argument. The registered typed converter turns the list into text, so the label reads `car, admin`. This is
why you register a converter once at startup instead of at every call.

```csharp
viewModel.SelectedItem = viewModel.Items[0];

using var binding = view.OneWayBind(viewModel, x => x.SelectedItem!.Tags, v => v.TagsLabel.Text);

Console.WriteLine(view.TagsLabel.Text);
```

```text
car, admin
```

You can also read a converter back from the published store and use it yourself. The snippet below finds the fallback converter
for an enumeration and converts `TodoPriority.High` to its name. It shows that the converter `BuildApp` published is the one you
registered.

```csharp
var converter = BindingConverters.Current.FallbackConverters.TryGetConverter(typeof(TodoPriority), typeof(string));

Console.WriteLine(converter is EnumNameFallbackConverter);

_ = converter!.TryConvert(typeof(TodoPriority), TodoPriority.High, typeof(string), null, out var name);

Console.WriteLine(name);
```

```text
True
High
```

A set-method converter writes one item of a collection by index. `ResolveSetMethodConverter` finds the one for a collection type
and an item type. The snippet below resolves the converter for `List<string>` and uses it to replace the item at index 1.
The list changes in place, and no new list is built.

```csharp
var converter = BindingConverters.Current.ResolveSetMethodConverter(typeof(List<string>), typeof(string));

Console.WriteLine(converter is TagListSetMethodConverter);

List<string> tags = ["car", "admin"];
_ = converter!.PerformSet(tags, "money", [1]);

Console.WriteLine(string.Join(", ", tags));
```

```text
True
car, money
```

## Register observation providers

An *observation provider* tells the library how to watch a property. It implements `ICreatesObservableForProperty`.
Each provider answers with an *affinity*, a score for a type and property. A higher score means a better fit.
The [mechanisms](mechanisms.md) page explains the scores and how to write a provider.

The generator picks a provider at build time from the types it can see. A provider registered at run time can outrank that choice.
When a registered provider scores above the generated mechanism, it watches the property. The generated mechanism wins ties.

The snippet below is the one method that gives this provider its weight. It returns 100 for a `TodoItem` and 0 for every other
type, which is above the score of an ordinary `PropertyChanged` type, so this provider wins for a `TodoItem`.

```csharp
public int GetAffinityForObject(Type type, string propertyName, bool beforeChanged) =>
    type == typeof(TodoItem) && !beforeChanged ? TodoItemAffinity : 0;
```

Register it with `WithRegistration`. The action receives the resolver, so this is also how you register any other service.
The snippet below counts the providers in the built application. The three are the two core ones and this one, which shows
the registration reached the resolver.

```csharp
Console.WriteLine(app.Current!.GetServices<ICreatesObservableForProperty>().Count());
```

```text
3
```

The snippet below observes the title of an item, changes it, and then prints how many observations the provider served. The
`1` shows the `WhenChanged` call went through your registered provider and not through the generated observation, so a provider
you register changes how existing calls watch a type.

```csharp
using (item.WhenChanged(x => x.Title).Subscribe(Console.WriteLine))
{
    item.Title = RenamedTitle;
}

Console.WriteLine(provider.ObservationCount);
```

```text
Renew car registration
Renew car registration online
1
```

## Register platform modules

A *platform module* is an `IModule` that registers the services of one UI framework. The builder's `WithPlatformModule`
runs a module against the resolver. Each platform package also has a shortcut.

| Method | Package namespace | What the module adds |
|--------|-------------------|----------------------|
| `WithWpf` | `ReactiveUI.Binding.Wpf.Builder` | Dependency-property observation and the dispatcher view thread invoker |
| `WithWinForms` | `ReactiveUI.Binding.WinForms.Builder` | Event-based property observation and the control view thread invoker |
| `WithMaui` | `ReactiveUI.Binding.Maui.Builder` | The MAUI view thread invoker and the Visibility converters |

A *view thread invoker* moves a write onto the thread that owns a control. [Threading](threading.md) explains how the library
uses it. `WithMaui` registers the invoker into the built application. The snippet below asks the built application for the
invoker and checks its type, which shows the MAUI invoker is in place for writes to reach a control's owning thread.

```csharp
Console.WriteLine(app.Current!.GetService<IViewThreadInvoker>() is DispatcherViewThreadInvoker);
```

```text
True
```

Write a module of your own by implementing `IModule.Configure`. It receives the resolver and registers what your screen needs.
The snippet below registers the to-do database as a lazy singleton, which the resolver creates the first time something asks
for it. A module keeps the registrations of one screen in one place instead of spread through startup code.

```csharp
public void Configure(IMutableDependencyResolver resolver) =>
    resolver.RegisterLazySingleton<ITodoStore>(static () => InMemoryTodoStore.CreateSeeded());
```

After `BuildApp`, ask the built application for the service. The snippet below does that. It fails with a clear message if
the module did not run, so a missing registration shows up at startup and not deep inside a binding.

```csharp
if (app.Current?.GetService<ITodoStore>() is not InMemoryTodoStore store)
{
    throw new InvalidOperationException("The module did not register the to-do database.");
}

return store;
```

Registering a module and a plain service through the `IAppBuilder` extensions works the same way. The snippet below registers
the module and checks that `WithPlatformModule` returned the builder itself. It registers a ready-made store as a
constant, which is a service that already exists and is handed out as is. It then maps a view model to a view and asks the
locator for the view, which shows that the whole chain works through the interface.

```csharp
using ModernDependencyResolver resolver = new();
var builder = resolver.CreateReactiveUIBindingBuilder();
IAppBuilder appBuilder = builder;

Console.WriteLine(ReferenceEquals(builder, appBuilder.WithPlatformModule(new TodoModule())));

_ = appBuilder
    .WithRegistration(static registry => registry.RegisterConstant(InMemoryTodoStore.CreateSeeded()))
    .ConfigureViewLocator(static mappings => mappings.Map<TodoListViewModel, TodoView>());

var locator = resolver.GetService<IViewLocator>();
TodoListViewModel viewModel = new(resolver.GetService<InMemoryTodoStore>()!);

Console.WriteLine(locator!.ResolveView(viewModel) is TodoView);
```

```text
True
True
```

## Register command binders

A *command binder* attaches a command to a control. The generator writes the common patterns itself: a `Command`
property and a button event. Register a binder for a control that has neither. A binder implements `ICreatesCommandBinding`.
Its `GetAffinityForObject` method scores the control type, and the highest score attaches the command.

The snippet below is the binder's scoring method. It returns 100 for a MAUI `Button` and 0 for any other control, so this
binder wins whenever a button is bound.

```csharp
public int GetAffinityForObject<T>(bool hasEventTarget) => typeof(T) == typeof(Button) ? ButtonAffinity : 0;
```

Register it with `WithCommandBinder`. The snippet below binds the add command to the add button. The count shows the binder
attached the command once, and the last line shows the button now holds the view model's own command.

```csharp
using var binding = view.BindCommand(viewModel, x => x.AddCommand, v => v.AddButton);

Console.WriteLine(binder.BindCount);
Console.WriteLine(ReferenceEquals(viewModel.AddCommand, view.AddButton.Command));
```

```text
1
True
```

See [mechanisms](mechanisms.md) for how to write a binder.

## Map view models to views

The *view locator* finds the view for a view model. `ConfigureViewLocator` creates a locator that holds the mappings
you list and registers it as the application's `IViewLocator`. Call it after `WithCoreServices`, which registers a default locator.
`Map<TViewModel, TView>` adds one pair. The snippet below asks the locator for the view of a view model and checks the type
of the answer. It shows that the mapping you registered reaches the locator the rest of the library uses.

```csharp
var view = ViewLocator.GetCurrent().ResolveView(viewModel);

Console.WriteLine(view is TodoView);
```

```text
True
```

[Views](views.md) covers the locator, view contracts and generated view dispatch.

## Use the System.Reactive package

The examples on these pages use the lean `ReactiveUI.Binding` package. It schedules work with a *sequencer*, the
object from `ReactiveUI.Primitives` that decides which thread runs a piece of work. A second package,
`ReactiveUI.Binding.Reactive`, is the same library compiled against System.Reactive. Reference it only if your program
already schedules with System.Reactive's `IScheduler`.

```bash
dotnet add package ReactiveUI.Binding.Reactive
```

The calls read the same in both packages. Only the names you import change:

| Lean package | System.Reactive package |
|--------------|-------------------------|
| `ReactiveUI.Binding` | `ReactiveUI.Binding.Reactive` |
| `ReactiveUI.Binding.Builder` | `ReactiveUI.Binding.Reactive.Builder` |
| `ReactiveUI.Binding.Maui` (package `ReactiveUI.Binding.Maui`) | `ReactiveUI.Binding.Reactive.Maui` (package `ReactiveUI.Binding.Maui.Reactive`) |
| a parameter of type `ISequencer` | a parameter of type `IScheduler` |

The WPF and WinForms packages follow the same pattern as MAUI. Import the shifted namespace once for the whole project:

```xml
<ItemGroup>
  <Using Include="ReactiveUI.Binding.Reactive" />
</ItemGroup>
```

Reference one runtime package, not both. Each package carries the generator, and the generator writes code for the
package it finds. Two copies would write the same files twice.

## Publish with Native AOT and trimming

*Native AOT* compiles your program to machine code when you publish it. *Trimming* removes code the publisher cannot see a use for.
Both work poorly with reflection, because reflection finds types and members by name at run time, and the publisher cannot see those uses.

A generated binding uses no reflection. The generator reads your property paths at build time and writes the exact calls that watch,
convert and write each property. The published program has nothing to look up by name, so a Native AOT program
needs no extra configuration for it. The sample project turns on the switches a Native AOT app uses.

```xml
<PublishAot>true</PublishAot>
<IsAotCompatible>true</IsAotCompatible>
<InvariantGlobalization>true</InvariantGlobalization>
<IlcDisableReflection>true</IlcDisableReflection>
```

`IlcDisableReflection` removes reflection metadata from the published program, so a binding that needed reflection would fail.
The program runs three calls. The first observes a title with `WhenChanged`, as in the first walkthrough. It shows that the
simplest call works in a published program.

```csharp
var item = new TodoItem { Title = "Renew car registration" };

using (item.WhenChanged(x => x.Title).Subscribe(Console.WriteLine))
{
    item.Title = RenamedTitle;
}
```

```text
Renew car registration
Renew car registration online
```

The second call turns a flag into words. The snippet below binds the finished flag of an item to the status text of a row, and
the `static` lambda is the conversion. The row shows `Open` at first and `Done` after the flag changes, so a converting binding
also works without reflection.

```csharp
var item = new TodoItem { Title = "File quarterly tax return" };
var row = new TodoRowView();

using (item.BindOneWay(row, x => x.IsDone, v => v.StatusText, static done => done ? DoneText : OpenText))
{
    Console.WriteLine(row.StatusText);

    item.IsDone = true;

    Console.WriteLine(row.StatusText);
}
```

```text
Open
Done
```

The third call is a two-way binding, `BindTwoWay`, which carries a value in both directions. The snippet below binds the
title of an item to the title text of a row. Writing to the row then changes the item, so edits made on the screen reach the
data without reflection too.

```csharp
var item = new TodoItem { Title = "Book dentist appointment" };
var row = new TodoRowView();

using (item.BindTwoWay(row, x => x.Title, v => v.TitleText))
{
    Console.WriteLine(row.TitleText);

    row.TitleText = "Book dentist appointment for Friday";

    Console.WriteLine(item.Title);
}
```

```text
Book dentist appointment
Book dentist appointment for Friday
```

Publish the program and run the executable to see that no code is generated at run time. The snippet below prints whether the
runtime can generate code. In a published Native AOT program `RuntimeFeature.IsDynamicCodeSupported` is `false`, so every binding
above ran from code written at build time. A normal `dotnet run` prints `True`.

```csharp
Console.WriteLine(RuntimeFeature.IsDynamicCodeSupported);
```

```text
False
```

Every generated binding is safe under trimming and Native AOT. The exception is a call you name with the `Unsafe`
suffix. Those overloads read the path at run time and carry `RequiresUnreferencedCode`, so the trimmer warns at the call.
Keep them out of a program that you publish this way. [Unsafe bindings](unsafe.md) lists them.

## Set build properties

Two MSBuild properties change what the generator writes. Set them in the project file. The defaults suit most projects.

### Choose how calls reach generated code

A *dispatch* sends a call to the code the generator wrote for it. There are two ways to do it.

- **An interceptor.** The generator writes a method that the compiler puts in place of your call. This needs Roslyn 4.13 or newer and C# 11 or later. The call site does not change and the file's namespace does not matter.
- **A concrete overload.** The generator writes an overload of the binding method that wins method lookup over the runtime stub.
  Roslyn 4.8 and newer can use it. From C# 10 the generator matches your call by the text of its lambdas. Before C# 10 it matches by file and line.

The package picks the generator for the compiler that builds your project. Set `ReactiveUIBindingUseInterceptors`
to `false` to keep concrete overloads on a compiler that could intercept.

```xml
<PropertyGroup>
  <ReactiveUIBindingUseInterceptors>false</ReactiveUIBindingUseInterceptors>
</PropertyGroup>
```

Each project's interceptors live in a namespace of their own under `ReactiveUI.Binding.Generated.Interceptors`, named after the
assembly, so a project and the test project it exposes its internals to never see each other's generated types. The compiler
accepts an interceptor only from a namespace the project lists in `InterceptorsNamespaces`, or one inside it. The package adds
`ReactiveUI.Binding.Generated.Interceptors` to that list, after any namespaces you listed, so you do nothing.

### Mark generated files

The generator starts each file it writes with an `<auto-generated/>` comment and a `#pragma warning disable` line. Coverage tools,
formatters and other analyzers skip files that carry them. Set `ReactiveUIBindingEmitGeneratedCodeMarkers` to `false` to drop
both lines, for example to read diagnostics that point into the generated code.

```xml
<PropertyGroup>
  <ReactiveUIBindingEmitGeneratedCodeMarkers>false</ReactiveUIBindingEmitGeneratedCodeMarkers>
</PropertyGroup>
```

### Root namespace and language version

Concrete overloads have to be in a namespace that the call site can see. From C# 10 the generator writes them into the
project's `RootNamespace` and adds a `global using` of it, so files outside the root namespace reach them. Before C# 10 there are no
global usings, so the overloads sit in `ReactiveUI.Binding`. A project below C# 10 that grants `InternalsVisibleTo` gets them in its root
namespace instead, and a file outside it cannot reach them. The analyzer reports that case as RXUIBIND009.

## Read the analyzer messages

The analyzer checks every binding call while you type and while you build. A *diagnostic* is one message with an id, a
severity, some text and a location. Every id starts with `RXUIBIND`. A diagnostic never stops the build. It tells you that
a call gets no generated code. That call runs the runtime path, or throws when the runtime stub finds nothing. The stub's message names
the `Unsafe` overload that resolves the path with reflection.

| Id | Severity | Meaning | Fix |
|----|----------|---------|-----|
| RXUIBIND001 | Info | The expression is not an inline lambda | [Write the path in the call](#write-the-path-in-the-call) |
| RXUIBIND002 | Warning | The observed type raises no notification | [Observe types that raise notifications](#observe-types-that-raise-notifications) |
| RXUIBIND003 | Warning | The path names a private or protected member | [Write the path in the call](#write-the-path-in-the-call) |
| RXUIBIND004 | Warning | `WhenChanging` targets a type with no before-change notification | [Observe types that raise notifications](#observe-types-that-raise-notifications) |
| RXUIBIND005 | Info | The source type implements `INotifyDataErrorInfo` | [Bind the shapes the generator reads](#bind-the-shapes-the-generator-reads) |
| RXUIBIND006 | Warning | The path has an indexer, a method call, a static field, or a read-only field at its end | [Write the path in the call](#write-the-path-in-the-call) |
| RXUIBIND007 | Warning | `BindCommand` finds no event on the control | [Bind the shapes the generator reads](#bind-the-shapes-the-generator-reads) |
| RXUIBIND008 | Warning | The property is not an interaction | [Bind the shapes the generator reads](#bind-the-shapes-the-generator-reads) |
| RXUIBIND009 | Warning | The call site cannot reach the generated dispatch | [Keep dispatch in reach](#keep-dispatch-in-reach) |
| RXUIBIND010 | Warning | The path passes through a type that raises no notification | [Observe types that raise notifications](#observe-types-that-raise-notifications) |
| RXUIBIND011 | Warning | The call resolved to ReactiveUI's own mixin | [Keep dispatch in reach](#keep-dispatch-in-reach) |
| RXUIBIND012 | Warning | `ToProperty` targets a type whose notifications generated code cannot raise | [Properties backed by observables](properties.md#how-the-generator-raises-your-notification) |
| RXUIBIND013 | Warning | `ToProperty` names its property in a form the generator cannot read | [Name the property directly](properties.md#name-the-property-directly) |
| RXUIBIND014 | Error | Below C# 13, a `string` initial value passed by position makes a `ToProperty` call ambiguous | [Name the property directly](properties.md#name-the-property-directly) |
| RXUIBIND015 | Warning | The call names a private or protected nested type | [Name types generated code can reach](#name-types-generated-code-can-reach) |
| RXUIBIND016 | Warning | The call's types are built from a type parameter | [Name types generated code can reach](#name-types-generated-code-can-reach) |
| RXUIBIND017 | Warning | A binding writes to a WPF, WinForms or MAUI object without that platform's Binding package | [The generated fallback](threading.md#the-generated-fallback) |

`RXUIBIND100` is an MSBuild error, not an analyzer message. It appears when the compiler is older than Roslyn 4.8 and reads:
"ReactiveUI.Binding's source generator requires Roslyn 4.8 or later (Visual Studio 2022 17.8+, or .NET SDK 8.0.100+)". Upgrade the build tools.

RXUIBIND002 and RXUIBIND004 are reported only when the generator uses interceptors. On a compiler older than Roslyn 4.13, or with
`ReactiveUIBindingUseInterceptors` set to `false`, those two do not appear.

### Write the path in the call

The generator reads a lambda written in the call: `x => x.Title`. RXUIBIND001 reports a path held in a variable, because the
generator cannot see into a variable.

```text
Expression<Func<TodoItem, string>> titlePath = x => x.Title;
item.WhenChanged(titlePath);

info RXUIBIND001: Expression must be inline lambda
```

Write the lambda in the call. The snippet below does, so the generator can read `x.Title` at build time. It prints the current
title and then the new one, exactly as in the first walkthrough.

```csharp
var item = new TodoItem { Title = "Renew car registration" };

using (item.WhenChanged(x => x.Title).Subscribe(Console.WriteLine))
{
    item.Title = RenamedTitle;
}
```

```text
Renew car registration
Renew car registration online
```

The generated method is a separate method in another class, so it cannot read a private or protected member.
RXUIBIND003 reports a path that names one.

```text
this.WhenChanged(x => x.Draft)

warning RXUIBIND003: Expression contains private or protected member
```

Make the property public and observe it. The snippet below observes `NewTitle`, a public property of the view model. The
title is set before the subscription, so the first line is the value that was already there and the second is the change.

```csharp
var viewModel = new TodoListViewModel(InMemoryTodoStore.CreateSeeded());

viewModel.NewTitle = DentistTitle;

using (viewModel.WhenChanged(x => x.NewTitle).Subscribe(Console.WriteLine))
{
    viewModel.NewTitle = "Buy birthday present for Sam";
}
```

```text
Book dentist appointment
Buy birthday present for Sam
```

A path follows properties and instance fields. A field raises no notification of its own, so the generator reads it once
and observes the rest of the path as usual; that is what lets a binding run through a named control such as `x:Name="TitleLabel"`.
An indexer, a method call, a static field, or a read-only field at the end of a path has nothing the generator can follow or
write, so RXUIBIND006 reports it.

```text
viewModel.WhenChanged(x => x.Items[0].Title)
viewModel.WhenChanged(x => x.SelectedItem!.Title.ToUpperInvariant())
viewModel.WhenChanged(x => SearchSettings.DefaultQuery)

warning RXUIBIND006: Expression contains 'x.Items[0]' which is not a property or instance field access.
```

The last line reads `DefaultQuery`, a static field, which belongs to no instance on the path. Replace each with a property. The call keeps
working through the `Unsafe` overload, which resolves an indexer at run time. The snippet below follows two properties,
`SelectedItem` and then its `Title`. When `SelectedItem` changes, the observation moves to the new item's `Title`, so the
output shows both titles.

```csharp
var viewModel = new TodoListViewModel(InMemoryTodoStore.CreateSeeded());
await viewModel.LoadAsync();
viewModel.SelectedItem = viewModel.Items[0];

using (viewModel.WhenChanged(x => x.SelectedItem!.Title).Subscribe(Console.WriteLine))
{
    viewModel.SelectedItem = viewModel.Items[1];
}
```

```text
Renew car registration
Book dentist appointment
```

### Name types generated code can reach

The generated method lives in a class of its own, so every type the call names has to be one that class can name. Two
shapes cannot be named, and the call is left to the runtime stub, which throws when it runs.

RXUIBIND015 reports a call that names a private or protected nested type, such as a view model declared `private` inside a
test class. Make the type `internal` or `public`, or call the `Unsafe` overload.

```text
item.WhenChanged(x => x.Title)   // item is a private nested class

warning RXUIBIND015: 'Tests.PrivateItem' is private or protected, so generated code cannot name it; this call generates nothing and throws when it runs
```

RXUIBIND016 reports a call whose types are built from a type parameter, such as `Source<TItem>` inside a generic method.
The generated method has no `TItem` to name. Call the `Unsafe` overload, which resolves the types when it runs.

```text
items.BindTo(source, x => x.Data)   // source is Source<TItem> inside a generic method

warning RXUIBIND016: 'Source<TItem>' is built from a type parameter, so generated code cannot name it; this call generates nothing and throws when it runs
```

### Observe types that raise notifications

A type raises a *notification* when a property changes, for example a `PropertyChanged` event. A type that raises
nothing never reports a change. RXUIBIND002 reports a call on such a type. Here `stored` is a `StorageObject`, a plain class.

```text
stored.WhenChanged(x => x.Size)

warning RXUIBIND002: Type has no observable properties
```

Observe a type that raises `PropertyChanged`. The snippet below observes `IsDone` on a `TodoItem`, which does. The output shows
the value at subscription and then the change, so you see every value the flag takes.

```csharp
var item = new TodoItem { Title = "File quarterly tax return" };

using (item.WhenChanged(x => x.IsDone).Subscribe(Console.WriteLine))
{
    item.IsDone = true;
}
```

```text
False
True
```

A silent type in the middle of a path is a different problem. The observation reads the silent object once and never follows the path
past it. RXUIBIND010 reports it. Here `Connection` is a `StorageConnection` that raises nothing.

```text
viewModel.WhenChanged(x => x.Connection.State)

warning RXUIBIND010: Type 'StorageConnection' raises no notification, so 'x.Connection' is read once and the observation stops following the path there
```

Observe a property of the view model that mirrors the state instead. The snippet below observes `ConnectionStatus` on a browser
view model. The view model raises the notification, so the output follows the link going down and coming back up.

```csharp
var storage = InMemoryObjectStorage.CreateSeeded();
var browser = new StorageBrowserViewModel(storage);

using (browser.WhenChanged(x => x.ConnectionStatus).Subscribe(static state => Console.WriteLine(state)))
{
    storage.Disconnect();
    await browser.ConnectAsync();
}
```

```text
Connected
Disconnected
Connecting
Connected
```

`WhenChanging` needs a type that raises `PropertyChanging`. On a type that raises only `PropertyChanged`, RXUIBIND004
reports the call, and the observation reads the value once and stays silent.

```text
item.WhenChanging(x => x.Title)

warning RXUIBIND004: Type does not support before-change notifications
```

Observe a type that raises `PropertyChanging`. The snippet below uses `EditableTodo`, which raises it before each change.
`WhenChanging` prints the current title when you subscribe and again just before the change replaces it, so the same text
appears twice.

```csharp
var todo = new EditableTodo { Title = DentistTitle };

using (todo.WhenChanging(x => x.Title).Subscribe(Console.WriteLine))
{
    todo.Title = "Book dentist appointment for Friday";
}
```

```text
Book dentist appointment
Book dentist appointment
```

### Bind the shapes the generator reads

The generator writes a `BindCommand` binding for a control that has an event to listen to. RXUIBIND007 reports a control without one.
Here the control is an `Entry`.

```text
view.BindCommand(viewModel, x => x.AddCommand, v => v.NewTitleTextBox)

warning RXUIBIND007: Control type 'Entry' has no default bindable event (Click, TouchUpInside, Pressed) and no 'toEvent' was specified
```

Bind to a button, or name the event in the `toEvent` argument. The snippet below binds the add command to the add button and
prints whether the command can run, before and after the view model gets a title. The button follows the view model's
`CanExecute`, so the answer changes from `False` to `True`.

```csharp
var viewModel = new TodoListViewModel(InMemoryTodoStore.CreateSeeded());
var view = new TodoView { ViewModel = viewModel };

using (view.BindCommand(viewModel, x => x.AddCommand, v => v.AddButton))
{
    Console.WriteLine(view.AddButton.Command!.CanExecute(null));

    viewModel.NewTitle = DentistTitle;

    Console.WriteLine(view.AddButton.Command.CanExecute(null));
}
```

```text
False
True
```

A view model that implements `INotifyDataErrorInfo` raises RXUIBIND005 when you bind a value from it. The generator writes the value
binding. It does not carry the validation state.

```text
viewModel.BindTwoWay(view, x => x.AmountText, v => v.NewTitleTextBox.Text)

info RXUIBIND005: Validation binding not generated
```

Expose the errors as a property of the view model and bind that property. The snippet below binds `ValidationSummary` to a
label, then sets an amount and prints the label. The label shows the summary text, so validation messages reach the screen
without the generator reading `INotifyDataErrorInfo`.

```csharp
var viewModel = new TransferViewModel(new InMemoryBankingBackend());
var view = new TransferView { ViewModel = viewModel };

using (viewModel.BindOneWay(view, x => x.ValidationSummary, v => v.ValidationLabel.Text))
{
    viewModel.Draft.Amount = SmallAmount;

    Console.WriteLine(view.ValidationLabel.Text);
}
```

```text
Choose the account to pay from. Choose who to pay.
```

`BindInteraction` needs a property that is an interaction. RXUIBIND008 reports a property of another type. The compiler
rejects most such calls first, so this message is rare.

```text
view.BindInteraction(viewModel, x => x.ErrorMessage, context => Task.CompletedTask)

warning RXUIBIND008: Property is not an IInteraction
```

Bind a handler to a property that is an interaction. The snippet below answers the interaction with `SetOutput(true)`, then
asks the question through `Handle` and prints the answer. The `True` shows a property of the right type gets a generated
binding that works.

```csharp
var viewModel = new IssueBoardViewModel(InMemoryGitHubServer.CreateSeeded());
var view = new IssueBoardView { ViewModel = viewModel };

using (view.BindInteraction(viewModel, x => x.ConfirmClose, static context =>
{
    context.SetOutput(true);
    return Task.CompletedTask;
}))
{
    var confirmed = await viewModel.ConfirmClose.Handle(new Issue { Title = "Crash on startup" });

    Console.WriteLine(confirmed);
}
```

```text
True
```

### Keep dispatch in reach

A call has to reach the code the generator wrote. Two mistakes send it somewhere else.

**Import the right namespace.** A file that imports `ReactiveUI` but not `ReactiveUI.Binding` binds the call to ReactiveUI's
own `WhenAnyValue`. That method knows nothing of the generator. The call runs the runtime expression engine, and the generator writes no code for it.
RXUIBIND011 reports this.

```text
viewModel.WhenAnyValue(x => x.RemainingCount)

warning RXUIBIND011: Binding call resolved to ReactiveUI's own mixin
```

Only ReactiveUI methods that take a property selector are reported. A ReactiveUI method that only shares a binding name, such
as the UIKit `BindTo` that binds a list of sections to a table view, is not a binding mixin and is not reported.

Import `ReactiveUI.Binding`, as in step 2 of [Get started](#get-started). The snippet below is a file that does, so
`WhenAnyValue` reaches this package. It prints the count of unfinished items, first `3` and then `2` after one item is completed.

```csharp
var viewModel = new TodoListViewModel(InMemoryTodoStore.CreateSeeded());
await viewModel.LoadAsync();

using (viewModel.WhenAnyValue(x => x.RemainingCount).Subscribe(Console.WriteLine))
{
    viewModel.SelectedItem = viewModel.Items[0];
    await viewModel.CompleteAsync();
}
```

```text
3
2
```

**Put the file under the root namespace.** RXUIBIND009 needs three things at once. The build uses concrete overloads.
The project sets `LangVersion` below 10, such as 7.3. The assembly grants `InternalsVisibleTo` to another assembly, such as its test project.
A file outside the project's root namespace then cannot see the overloads. The call binds to the stub and throws when it runs.

```text
[assembly: InternalsVisibleTo("TodoApp.Tests")]
viewModel.WhenChanged(x => x.RemainingCount)

warning RXUIBIND009: Generated binding dispatch is out of reach here
```

There are three fixes: move the file under the root namespace, raise the language version to 10 or later, or use a compiler
that intercepts. The snippet below sits under the root namespace of the sample project, `ReactiveUI.Binding.Documentation`,
so it reaches the dispatch. It prints the count of unfinished items before and after one item is completed.

```csharp
var viewModel = new TodoListViewModel(InMemoryTodoStore.CreateSeeded());
await viewModel.LoadAsync();

using (viewModel.WhenChanged(x => x.RemainingCount).Subscribe(Console.WriteLine))
{
    viewModel.SelectedItem = viewModel.Items[0];
    await viewModel.CompleteAsync();
}
```

```text
3
2
```

## Where to go next

- [Observing properties](observing.md) covers `WhenChanged`, `WhenChanging`, `WhenAnyValue` and the other observation calls.
- [Bindings](bindings.md) covers one-way, two-way, command and interaction bindings.
- [Converters](converters.md) and [custom converters](custom-converters.md) cover what the `With...Converter` methods register.
- [Mechanisms](mechanisms.md) covers providers, affinity and command binders.
- [Views](views.md) covers the view locator, and [threading](threading.md) covers invokers and schedulers.
- [Unsafe bindings](unsafe.md) covers the reflection overloads. The [API reference](api.md) lists every type on this page.

## API reference

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`IReactiveUIBindingBuilder`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/IReactiveUIBindingBuilder.cs) | Describes a builder that configures the library's services, converters and platform modules. | Interface that extends Splat's `IAppBuilder`. | Its methods return `IReactiveUIBindingBuilder`, so calls chain. |
| [`IReactiveUIBindingInstance`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/IReactiveUIBindingInstance.cs) | Represents a configured application. | Interface that extends Splat's `IAppInstance`. | The inherited `Current` property is the resolver that reads the configured services. |
| [`ReactiveUIBindingBuilder`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingBuilder.cs) | Configures the library on a Splat resolver. The constructor prepares Splat on that resolver and registers the `ConverterService` with it. | Sealed class. Constructor takes `IMutableDependencyResolver resolver` and `IReadonlyDependencyResolver? current`. | `current` may be `null`. `CurrentMutable` is the resolver you passed in. |
| [`ReactiveUIBindingBuilder.ConverterService`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingBuilder.cs) | Holds the typed, fallback and set-method converter registries that this builder fills. | Read-only `ConverterService`. | Empty until `WithCoreServices` or a `With...Converter` call adds converters. `BuildApp` publishes it to `BindingConverters.Current`. |
| [`WithCoreServices`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingBuilder.cs) | Registers the default converters, the `PropertyChanged` and plain-object observation providers, and a default view locator. | No parameters. Returns `IAppBuilder` on the class and `IReactiveUIBindingBuilder` on the interface. | A second call registers nothing more. |
| [`WithPlatformModule`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingBuilder.cs) | Runs a platform module against the resolver. | `T module`, where `T : IModule`. Returns the builder. | Throws `ArgumentNullException` when `module` is `null`. |
| [`WithRegistration`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingBuilder.cs) | Runs your action against the mutable resolver at the moment of the call. | `Action<IMutableDependencyResolver> configureAction`. Returns the builder. | Throws `ArgumentNullException` when the action is `null`. |
| [`WithConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingBuilder.cs) | Adds a typed converter for one pair of types. | `IBindingTypeConverter converter`. Returns the builder. | Throws `ArgumentNullException` when `converter` is `null`. |
| [`WithFallbackConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingBuilder.cs) | Adds a converter that checks types at run time. | `IBindingFallbackConverter converter`. Returns the builder. | Throws `ArgumentNullException` when `converter` is `null`. |
| [`WithSetMethodConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingBuilder.cs) | Adds a converter that writes a value into an indexed position. | `ISetMethodBindingConverter converter`. Returns the builder. | Throws `ArgumentNullException` when `converter` is `null`. |
| [`WithCommandBinder`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingBuilder.cs) | Registers a binder that attaches a command to a control. | `ICreatesCommandBinding binder`. Returns the builder. | Registered as a lazy singleton. Throws `ArgumentNullException` when `binder` is `null`. |
| [`ConfigureViewLocator`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingBuilder.cs) | Creates a `DefaultViewLocator` that holds the mappings you list and registers it as the `IViewLocator`. | `Action<ViewMappingBuilder> configure`. Returns the builder. | Call it after `WithCoreServices`. Throws `ArgumentNullException` when `configure` is `null`. |
| [`BuildApp`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingBuilder.cs) | Builds the application, publishes the converter service to `BindingConverters.Current` and marks the library as started. | No parameters. Returns `IReactiveUIBindingInstance`. | `RxBindingBuilder.EnsureInitialized` passes after this call. Throws `InvalidOperationException` when the build produces no usable instance. |
| [`RxBindingBuilder.CreateReactiveUIBindingBuilder`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/RxBindingBuilder.cs) | Creates a builder over the application-wide Splat resolver. | No parameters. Returns `ReactiveUIBindingBuilder`. | Reads services from `AppLocator.Current`. |
| [`RxBindingBuilder.EnsureInitialized`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/RxBindingBuilder.cs) | Checks that a builder's `BuildApp` has completed. | No parameters. Returns nothing. | Throws `InvalidOperationException` when `BuildApp` has not run. No binding call runs this check. |
| [`RxBindingBuilderMixins.CreateReactiveUIBindingBuilder`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/RxBindingBuilderMixins.cs) | Creates a builder over a resolver you own. Called on the resolver. | Receiver `IMutableDependencyResolver`. Returns `ReactiveUIBindingBuilder`. | Reads services from the resolver when it is also an `IReadonlyDependencyResolver`, and from `AppLocator.Current` otherwise. Throws `ArgumentNullException` when the resolver is `null`. |
| [`ReactiveUIBindingModule`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/ReactiveUIBindingModule.cs) | Registers the two core observation providers, `INPCObservableForProperty` and `POCOObservableForProperty`. | Sealed class that implements `IModule`. `Configure(IMutableDependencyResolver resolver)` does the work. | Registers both as lazy singletons. Throws `ArgumentNullException` when `resolver` is `null`. |
| [`BuilderMixins.BuildApp`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/BuilderMixins.cs) | Builds the application from an `IAppBuilder`. | Receiver `IAppBuilder`. Returns `IReactiveUIBindingInstance`. | Throws `ArgumentNullException` for a `null` receiver and `InvalidOperationException` when the receiver is not a binding builder. |
| [`BuilderMixins.WithPlatformModule`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/BuilderMixins.cs) | Registers a platform module from an `IAppBuilder`. | Receiver `IAppBuilder`; `T module`, where `T : IModule`. Returns `IReactiveUIBindingBuilder`. | Same exceptions as `BuilderMixins.BuildApp`. |
| [`BuilderMixins.WithRegistration`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/BuilderMixins.cs) | Runs a registration action from an `IAppBuilder`. | Receiver `IAppBuilder`; `Action<IMutableDependencyResolver> configureAction`. Returns `IReactiveUIBindingBuilder`. | Same exceptions as `BuilderMixins.BuildApp`. |
| [`BuilderMixins.WithConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/BuilderMixins.cs) | Registers a typed converter from an `IAppBuilder`. | Receiver `IAppBuilder`; `IBindingTypeConverter converter`. Returns `IReactiveUIBindingBuilder`. | Same exceptions as `BuilderMixins.BuildApp`. |
| [`BuilderMixins.WithFallbackConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/BuilderMixins.cs) | Registers a fallback converter from an `IAppBuilder`. | Receiver `IAppBuilder`; `IBindingFallbackConverter converter`. Returns `IReactiveUIBindingBuilder`. | Same exceptions as `BuilderMixins.BuildApp`. |
| [`BuilderMixins.WithSetMethodConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/BuilderMixins.cs) | Registers a set-method converter from an `IAppBuilder`. | Receiver `IAppBuilder`; `ISetMethodBindingConverter converter`. Returns `IReactiveUIBindingBuilder`. | Same exceptions as `BuilderMixins.BuildApp`. |
| [`BuilderMixins.ConfigureViewLocator`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/BuilderMixins.cs) | Configures the view locator from an `IAppBuilder`. | Receiver `IAppBuilder`; `Action<ViewMappingBuilder> configure`. Returns `IReactiveUIBindingBuilder`. | Same exceptions as `BuilderMixins.BuildApp`. |
| [`WithMaui`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Maui.Shared/Builder/MauiBindingBuilderExtensions.cs) | Registers the MAUI module. | Receiver `IAppBuilder` or `IReactiveUIBindingBuilder`. Returns `IReactiveUIBindingBuilder`. | Lives in `ReactiveUI.Binding.Maui.Builder`. The `IAppBuilder` form casts the receiver to `IReactiveUIBindingBuilder`. The interface form throws `ArgumentNullException` for a `null` receiver. |
| [`WithWpf`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Wpf.Shared/Builder/WpfBindingBuilderExtensions.cs) | Registers the WPF module. | Receiver `IAppBuilder` or `IReactiveUIBindingBuilder`. Returns `IReactiveUIBindingBuilder`. | Lives in `ReactiveUI.Binding.Wpf.Builder`. Same receiver rules as `WithMaui`. |
| [`WithWinForms`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.WinForms.Shared/Builder/WinFormsBindingBuilderExtensions.cs) | Registers the WinForms module. | Receiver `IAppBuilder` or `IReactiveUIBindingBuilder`. Returns `IReactiveUIBindingBuilder`. | Lives in `ReactiveUI.Binding.WinForms.Builder`. Same receiver rules as `WithMaui`. |
| [`MauiBindingModule`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Maui.Shared/MauiBindingModule.cs) | Registers the MAUI view thread invoker and the Visibility converters. | Sealed class that implements `IModule`. | The Visibility converters are registered with the resolver only. Throws `ArgumentNullException` when the resolver is `null`. |
| [`WpfBindingModule`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Wpf.Shared/WpfBindingModule.cs) | Registers dependency-property observation and the dispatcher view thread invoker. | Sealed class that implements `IModule`. | Throws `ArgumentNullException` when the resolver is `null`. |
| [`WinFormsBindingModule`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.WinForms.Shared/WinFormsBindingModule.cs) | Registers event-based property observation and the control view thread invoker. | Sealed class that implements `IModule`. | Throws `ArgumentNullException` when the resolver is `null`. |
| [`ReactiveUIBindingUseInterceptors`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/build/ReactiveUI.Binding.SourceGenerators.targets) | Chooses whether the generator replaces each call with an interceptor. | MSBuild property. `true` when unset; set `false` to use concrete overloads. | Interceptors need Roslyn 4.13 or newer. |
| [`ReactiveUIBindingEmitGeneratedCodeMarkers`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/build/ReactiveUI.Binding.SourceGenerators.props) | Chooses whether generated files start with the `<auto-generated/>` comment and a `#pragma warning disable` line. | MSBuild property. `true` when unset; set `false` to drop both lines. | Visible to the generator through the compiler's property list. |
| [`InterceptorsNamespaces`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/build/ReactiveUI.Binding.SourceGenerators.targets) | Lists the namespaces the compiler accepts interceptors from. | MSBuild property with `;`-separated names. | The package adds `ReactiveUI.Binding.Generated.Interceptors` after your entries when interceptors are in use. |
| [`RXUIBIND001`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a path that is not an inline lambda. | Info. | The call gets no generated code. The `Unsafe` overload reads the path at run time. |
| [`RXUIBIND002`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports an observed type that raises no notification. | Warning. | Does not appear when concrete overloads dispatch the call. |
| [`RXUIBIND003`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a path that names a private or protected member. | Warning. | The call gets no generated code. Make the member public. |
| [`RXUIBIND004`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports `WhenChanging` on a type with no before-change notification. | Warning. | The observation reads the value once and then stays silent. Does not appear when concrete overloads dispatch the call. |
| [`RXUIBIND005`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a source type that implements `INotifyDataErrorInfo`. | Info. | The generated binding carries the value and not the validation state. |
| [`RXUIBIND006`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a path with an indexer, a method call, a static field, or a read-only field at its end. | Warning. | The call gets no generated code. Use properties or instance fields in the path. |
| [`RXUIBIND007`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a `BindCommand` control with no default bindable event. | Warning. | Default events are `Click`, `TouchUpInside` and `Pressed`. Name another event in `toEvent`. |
| [`RXUIBIND008`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a `BindInteraction` property that is not an `IInteraction<TInput, TOutput>`. | Warning. | Bind a property of the interaction type. |
| [`RXUIBIND009`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a call site the generated dispatch cannot reach. | Warning. | Needs concrete overloads, C# 9 or lower, and `InternalsVisibleTo`. The file must sit under the root namespace. |
| [`RXUIBIND010`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a path that passes through a type that raises no notification. | Warning. | The observation reads that link once and stops following the path there. |
| [`RXUIBIND011`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a binding call that resolved to ReactiveUI's own mixin. | Warning. | Only ReactiveUI methods that take a property selector are reported. The call generates nothing and uses the runtime expression engine. Import `ReactiveUI.Binding`. |
| [`RXUIBIND012`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a `ToProperty` source whose notifications generated code cannot raise. | Warning. | The call generates nothing and throws when it runs. [Properties backed by observables](properties.md) lists the ways a type can offer generated code a way in. |
| [`RXUIBIND013`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a `ToProperty` property named in a form the generator cannot read. | Warning. | Name the property as `x => x.Property` or as a constant such as `nameof(Property)`. |
| [`RXUIBIND014`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a `ToProperty` call below C# 13 that a positional `string` initial value makes ambiguous. | Error. | Write the argument as `initialValue: ...`, or move to C# 13. |
| [`RXUIBIND015`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a call that names a private or protected nested type. | Warning. | The call generates nothing and throws when it runs. Make the type `internal` or `public`, or call the `Unsafe` overload. |
| [`RXUIBIND016`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a call whose types are built from a type parameter. | Warning. | The call generates nothing and throws when it runs. Call the `Unsafe` overload. |
| [`RXUIBIND017`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/DiagnosticWarnings.cs) | Reports a binding that writes to a WPF, WinForms or MAUI object when the matching `ReactiveUI.Binding.Wpf`, `.WinForms` or `.Maui` package is not referenced. | Warning. | The binding still generates, but its writes are not moved to the object's thread. Reference the package the message names. |
| [`RXUIBIND100`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.SourceGenerators/build/ReactiveUI.Binding.SourceGenerators.targets) | Stops the build when the compiler is older than Roslyn 4.8. | Error from MSBuild, not from the analyzer. | The message asks for Visual Studio 2022 17.8 or .NET SDK 8.0.100 or later. |
