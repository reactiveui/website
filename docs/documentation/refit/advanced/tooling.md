---
Order: 1
---
# Compiler tooling

When you build a project, Refit turns your interface into client code and checks for mistakes
in its declarations. That is why a bad route can produce a diagnostic before you ever run
the app, and why your editor can offer a fix for some problems.

The `Refit` package includes the tools that do this work. This page explains their public APIs
for anyone building an editor integration, a compiler host or tests for generated code.
It starts by running the generator through Roslyn, the .NET library for working with C# code.

## Run a generator yourself

**1. Use a compiler host.** The [complete sample project](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Tooling/Tooling.csproj)
targets .NET 10 and C# 14. It references local Refit source and Roslyn 5.0.0.
The shipping Refit compiler assemblies target .NET Standard 2.0 and compile against Roslyn 4.8.
The sample host's Roslyn version lets it read C# 14.

**2. Create a compilation.** A `Compilation` holds C# source files and references to the libraries
they use. The sample's `ToolingCompilation` helper supplies runtime references and `Refit.dll`.
Its input is an interface with `[Get("/people/{id}")]` and a `Task<string>` method.

**3. Give the generator to a driver.** A generator driver runs source generators for a compilation.
`AsSourceGenerator()` adapts Refit's incremental generator to the driver's interface.
An incremental generator remembers parts of its work and repeats them when their inputs change.
The driver calls `Initialize` with the compiler's context. You do not create that context yourself.

Add `using Microsoft.CodeAnalysis;` and `using Microsoft.CodeAnalysis.CSharp;`.
The full sample uses a project-reference alias to import `Refit.Generator.InterfaceStubGeneratorV2`.

```csharp
InterfaceStubGeneratorV2 generator = new();
GeneratorDriver driver = CSharpGeneratorDriver.Create([generator.AsSourceGenerator()], parseOptions: new(LanguageVersion.CSharp14));
driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out Compilation generated, out _);
GeneratorDriverRunResult result = driver.GetRunResult();
Console.WriteLine(!result.GeneratedTrees.IsEmpty); // True
```

The sample checks that Refit emits a client and that the resulting compilation has no errors.
This does not send an HTTP request. Use [generated clients](../index.md) in application code.

| Public API on `Refit.Generator.InterfaceStubGeneratorV2` | Contract |
| --- | --- |
| `InterfaceStubGeneratorV2()` | Creates a generator for a Roslyn host. |
| `Initialize(IncrementalGeneratorInitializationContext context)` | Registers the pipeline that reads Refit interfaces, checks supported request shapes, and emits implementations. The host supplies the context and runs the pipeline. |

## Reserve names for generated code

`Refit.Generator.UniqueNameBuilder` prevents identifiers from colliding within one helper instance.
It compares names exactly, including their case. Keep one instance for the scope you are generating.
It does not track a parent scope or change a name into a valid C# identifier.

The complete sample's static `ReservedNames` array contains `"client0"` and `"response"`.

Both `Reserve` overloads affect the same set. `New` reserves the name that it returns too.

```csharp
const string identifier = "client";
UniqueNameBuilder names = new();
names.Reserve(identifier);
names.Reserve(ReservedNames);
string next = names.New(identifier);
Console.WriteLine(next); // client1
```

| Public API | Contract |
| --- | --- |
| `UniqueNameBuilder()` | Creates an empty set of reserved names. |
| `Reserve(string name)` | Reserves one name. Reserving it twice has no extra effect. |
| `Reserve(IEnumerable<string> names)` | Reserves each name. The implementation ignores a null sequence. |
| `string New(string name)` | Returns the requested name if it is free. Otherwise it tries suffixes `0`, `1`, and so on. It reserves the result. |

Use a separate instance for concurrent work. The helper's mutable set has no lock.

## Find types in a compilation

`Refit.Generator.WellKnownTypes` finds types in one `Compilation`. It caches the result for each
metadata name. A metadata name identifies a compiled type. For example, `System.String` names
`string`, and ``System.Collections.Generic.List`1`` names `List<T>`.
An `INamedTypeSymbol` describes a C# type for Roslyn. It does not create an instance of that type.

```csharp
WellKnownTypes types = new(compilation);
INamedTypeSymbol stringType = types.Get(typeof(string));
INamedTypeSymbol? missing = types.TryGet("Demo.Missing");
Console.WriteLine(stringType.Name); // String
Console.WriteLine(missing is null); // True
```

| Public API | Contract |
| --- | --- |
| `WellKnownTypes(Compilation compilation)` | Stores the compilation used for every lookup. Create a new helper for a different compilation. |
| `INamedTypeSymbol Get(Type type)` | Uses `type.FullName` to find the compiler symbol. Throws `ArgumentNullException` for null. Throws `InvalidOperationException` when the type has no full name or the compilation cannot resolve it. |
| `INamedTypeSymbol? TryGet(string typeFullName)` | Returns a symbol, or null if Roslyn cannot resolve one type with that metadata name. Caches successful and unsuccessful lookups. |

The helper does not add an assembly reference. The compilation must contain the reference needed
to resolve the type. Its mutable cache has no lock.

## Run interface checks

`Refit.Analyzers.RefitInterfaceAnalyzer` reports diagnostic messages. A diagnostic has an ID,
a severity, and a source location. A descriptor explains a diagnostic before it occurs.

The sample supplies an interface whose `[Get]` route contains a backslash. The analyzer driver
calls `Initialize`. The sample's `Contains` helper looks for an ID in the returned diagnostics.

```csharp
RefitInterfaceAnalyzer analyzer = new();
const string routeDiagnostic = "RF003";
ImmutableArray<DiagnosticDescriptor> supported = analyzer.SupportedDiagnostics;
ImmutableArray<Diagnostic> diagnostics = await compilation.WithAnalyzers([analyzer]).GetAnalyzerDiagnosticsAsync();
Console.WriteLine(Contains(diagnostics, routeDiagnostic)); // True
```

| Public API | Contract |
| --- | --- |
| `RefitInterfaceAnalyzer()` | Creates an analyzer for a Roslyn host. |
| `ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics` | Returns the nine supported descriptors below. They are warnings enabled by default. |
| `Initialize(AnalysisContext context)` | Registers interface checks. Enables concurrent analysis and skips generated code. The host supplies the context. |

| ID | What it checks |
| --- | --- |
| `RF001` | A recognized Refit interface member needs a valid HTTP method attribute with a literal path. |
| `RF003` | A route contains a backslash instead of `/`. |
| `RF004` | A method has more than one `CancellationToken` parameter. |
| `RF005` | A `[HeaderCollection]` parameter needs `IDictionary<string, string>`. |
| `RF006` | The method needs the reflection request builder when generated request building is enabled. |
| `RF008` | A method has more than one `[HeaderCollection]` parameter. |
| `RF009` | A method has more than one `[Authorize]` parameter. |
| `RF011` | A method has more than one `[Body]` parameter. |
| `RF012` | A multipart method also has a `[Body]` parameter. |

For AOT apps, check `RF006` before using generated-only registration. A compiler-generated class
does not make an unsupported request shape safe for AOT. Follow the [AOT guide](../aot.md).

## Apply a code fix

`Refit.CodeFixes.RefitInterfaceCodeFixProvider` offers corrections for `RF003` and `RF005`.
For `RF003`, it changes backslashes in the route literal to `/`.
For `RF005`, it changes the parameter type to `IDictionary<string, string>`.
Check your calling code after changing a parameter type.

The complete sample creates a Roslyn workspace, source document, and diagnostic.
It runs this block for each supported diagnostic and checks the corrected compilation.
`FindChange` selects the document edit from the returned operations.

```csharp
RefitInterfaceCodeFixProvider provider = new();
ImmutableArray<string> fixableIds = provider.FixableDiagnosticIds;
FixAllProvider fixAll = provider.GetFixAllProvider();
List<CodeAction> actions = [];
CodeFixContext context = new(document, diagnostic, (action, _) => actions.Add(action), CancellationToken.None);
await provider.RegisterCodeFixesAsync(context);
ImmutableArray<CodeActionOperation> operations = await actions[0].GetOperationsAsync(CancellationToken.None);
ApplyChangesOperation change = FindChange(operations);
Document fixedDocument = change.ChangedSolution.GetDocument(document.Id) ?? throw new InvalidOperationException("Missing corrected document.");
Console.WriteLine(await fixedDocument.GetTextAsync());
```

| Public API | Contract |
| --- | --- |
| `RefitInterfaceCodeFixProvider()` | Creates a provider for a Roslyn host. |
| `ImmutableArray<string> FixableDiagnosticIds` | Returns `RF003` and `RF005`. |
| `FixAllProvider GetFixAllProvider()` | Returns Roslyn's batch fixer for applying offered corrections across a selected scope. |
| `Task RegisterCodeFixesAsync(CodeFixContext context)` | Registers actions for supported diagnostics in the supplied document. The task completes after registration. The host must request and apply the action's operations to change the document. |

## Public index and range polyfills

Both `InterfaceStubGeneratorV2.dll` and `Refit.Analyzers.dll` contain public `System.Index` and
`System.Range` record structs. A polyfill supplies a type that an older target framework lacks.
These copies let the tools compile for .NET Standard 2.0. Their public declarations are part of
the shipped tooling surface, even though their source comments describe project-only use.

The two assembly copies are separate types. They are also separate from the runtime's
`System.Index` and `System.Range`. Normal .NET 10 app code uses the runtime types.
The sample uses `extern alias GeneratorTooling;` and `extern alias AnalyzerTooling;` with aliased
project references to select each tooling copy. `GeneratorIndex` and `GeneratorRange` below
alias the generator's copies. The executable repeats the checks for the analyzer's copies.

### Index

An index stores a value and whether to count it from the end of a sequence.
The end is the position just after the final item. `GetOffset(length)` calculates a position;
it does not read a collection or check its bounds.

```csharp
const int sequenceLength = 4;
GeneratorIndex first = new(1);
GeneratorIndex last = new(1, fromEnd: true);
GeneratorIndex converted = 1;
Console.WriteLine(first.Value); // 1
Console.WriteLine(last.IsFromEnd); // True
Console.WriteLine(last.GetOffset(sequenceLength)); // 3
Console.WriteLine(GeneratorIndex.Start.GetOffset(sequenceLength)); // 0
Console.WriteLine(GeneratorIndex.End.GetOffset(sequenceLength)); // 4
Console.WriteLine(first == converted); // True
Console.WriteLine(first != last); // True
Console.WriteLine(first.Equals(converted)); // True
Console.WriteLine(first.Equals((object)converted)); // True
Console.WriteLine(first.GetHashCode() == converted.GetHashCode()); // True
Console.WriteLine(first.ToString());
```

| Public API on each `System.Index` copy | Contract |
| --- | --- |
| `Index(int value)` | Stores a position counted from the start. |
| `Index(int value, bool fromEnd)` | Stores the value and direction. Unlike the runtime type, these constructors do not reject negative values. |
| `Index Start` | Returns value `0` counted from the start. |
| `Index End` | Returns value `0` counted from the end. |
| `int Value` | Gets the stored value. |
| `bool IsFromEnd` | Gets the stored direction. |
| `implicit operator Index(int value)` | Creates an index counted from the start. |
| `int GetOffset(int length)` | Returns `Value` for a start index, or `length - Value` for an end index. Does not validate length or bounds. |
| `bool Equals(Index other)`; `bool Equals(object? obj)` | Compare the two stored fields. The object overload returns false for another type, including a different assembly's copy. |
| `operator ==(Index, Index)`; `operator !=(Index, Index)` | Compare field values for equality or inequality. |
| `int GetHashCode()` | Gives equal indices equal hashes. Do not store the hash as an ID. |
| `string ToString()` | Formats the record's property names and values. It does not use the runtime index's `^1` format. |

### Range

A range holds its inclusive start and exclusive end. These polyfills store endpoints without
checking their order or calculating a collection length.

```csharp
GeneratorRange middle = new(first, last);
GeneratorRange same = new(first, last);
Console.WriteLine(middle.Start.Value); // 1
Console.WriteLine(middle.End.IsFromEnd); // True
Console.WriteLine(GeneratorRange.All.Start == GeneratorIndex.Start); // True
Console.WriteLine(GeneratorRange.All.End == GeneratorIndex.End); // True
Console.WriteLine(GeneratorRange.StartAt(first).End == GeneratorIndex.End); // True
Console.WriteLine(GeneratorRange.EndAt(last).Start == GeneratorIndex.Start); // True
Console.WriteLine(middle == same); // True
Console.WriteLine(middle != GeneratorRange.All); // True
Console.WriteLine(middle.Equals(same)); // True
Console.WriteLine(middle.Equals((object)same)); // True
Console.WriteLine(middle.GetHashCode() == same.GetHashCode()); // True
Console.WriteLine(middle.ToString());
```

| Public API on each `System.Range` copy | Contract |
| --- | --- |
| `Range(Index start, Index end)` | Stores both endpoints. |
| `Range All` | Returns `Index.Start` through `Index.End`. |
| `Index Start`; `Index End` | Get the stored endpoints. |
| `Range StartAt(Index start)` | Uses the supplied start and `Index.End`. |
| `Range EndAt(Index end)` | Uses `Index.Start` and the supplied end. |
| `bool Equals(Range other)`; `bool Equals(object? obj)` | Compare both endpoints. The object overload requires the same assembly's range type. |
| `operator ==(Range, Range)`; `operator !=(Range, Range)` | Compare endpoint values for equality or inequality. |
| `int GetHashCode()` | Gives equal ranges equal hashes. Do not persist the hash. |
| `string ToString()` | Formats the range record and its endpoint properties. |

Use the runtime types with .NET 10 collection indexers and slicing APIs.

## Build tools and AOT apps

The compiler tools run during development and builds. Their Roslyn objects and metadata files
belong in the compiler host, rather than your app's request code.
Source generation can prepare request code before a Native AOT publish.
You must also supply [generated JSON metadata](../serialization/json.md) for request and reply models.

The tooling example requires the ordinary .NET runtime and compiler metadata files.
It does not demonstrate Native AOT execution. The [AOT examples](../aot.md) cover application publishing.

Source: [generator](https://github.com/reactiveui/refit/tree/main/src/InterfaceStubGenerator.Shared),
[analyzer](https://github.com/reactiveui/refit/tree/main/src/Refit.Analyzers.Shared), and
[code fix](https://github.com/reactiveui/refit/tree/main/src/Refit.CodeFixes.Shared).
