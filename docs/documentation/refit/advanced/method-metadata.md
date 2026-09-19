---
Order: 4
---
# Method metadata and client names

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/advanced-method-metadata/advanced-method-metadata.csproj).

Code that builds a request sometimes needs to carry around the method's details: its name,
route, return type and the properties used to fill in the path. Refit's metadata objects
keep that information together for request builders and other client infrastructure.

This page shows what those objects store and how generated clients are named. You will
usually only need them when extending or inspecting the client-building process.

## Describe a method

The [complete sample](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/RuntimeHelpers)
uses .NET 10 and C# 14. Its metadata example deliberately calls reflection APIs.
It is separate from the generated descriptors shown in [request helpers](request-helpers.md)
and does not claim Native AOT compatibility.

`RestMethodInfo` is a sealed record. Its constructor takes `Name`, `HostingType`,
`MethodInfo`, `RelativePath`, and `ReturnType`. Those five properties have `init` setters.
The object records the values you supply; it does not validate the path or derive one
from the method. `ReturnType` is the declared return type, such as `Task<int>`.

The compiler supplies record equality: `Equals(RestMethodInfo)`, `Equals(object)`, `==`,
`!=`, and `GetHashCode`. It also supplies `ToString`, positional `Deconstruct`, and copying
with `with`. Equality compares all five stored values. Two paths therefore produce different
records even when they describe the same reflected method.

## Describe route parameters and properties

`RestMethodParameterInfo(string name, ParameterInfo)` assigns the name and reflected
parameter. The overload taking `bool isObjectPropertyParameter` assigns that flag and
parameter, leaving `Name` null. Both start with `Type = ParameterType.Normal` and an
empty `ParameterProperties` list. The list property has an `init` setter; the list itself
can still be changed. `Name`, `ParameterInfo`, `IsObjectPropertyParameter`, and `Type`
have ordinary setters.

`ParameterType.Normal` has value `0`; `RoundTripping` has value `1`. The reflection
builder uses the latter for a catch-all route placeholder, such as `{**path}`, preserving
the `/` separators while escaping each section. Setting this enum on an isolated metadata
object does not escape any text or dispatch a request.

`RestMethodParameterProperty(string name, PropertyInfo)` creates a one-element
`PropertyChain`. The overload taking `IReadOnlyList<PropertyInfo>` retains your chain and
sets `PropertyInfo` to its final element. A dotted placeholder uses the ordered chain to
walk from the parameter to its bound value. Supply a non-empty chain.
`Name`, `PropertyInfo`, and `PropertyChain` can all be changed independently.
Updating either property navigation value does not update the other; keep them consistent.

The sample's `IHelperApi` declares `Task<int> GetAsync(int id)` and its `FormBody` has
a public `Count` property. Add `using System.Reflection;` and `using Refit;`.

```csharp
MethodInfo method = typeof(IHelperApi).GetMethod(nameof(IHelperApi.GetAsync))!;
RestMethodInfo info = new(method.Name, typeof(IHelperApi), method, "/items/{id}", method.ReturnType);
RestMethodInfo alternate = info with { RelativePath = "/other/{id}" };
(string name, Type hostingType, MethodInfo reflected, string relativePath, Type returnType) = info;
ParameterInfo parameter = method.GetParameters()[0];
RestMethodParameterInfo named = new("id", parameter) { Type = ParameterType.Normal };
RestMethodParameterInfo objectParameter = new(true, parameter) { Name = "body", Type = ParameterType.RoundTripping };
PropertyInfo count = typeof(FormBody).GetProperty(nameof(FormBody.Count))!;
RestMethodParameterProperty direct = new(nameof(count), count);
RestMethodParameterProperty chained = new("body.count", new[] { count });
objectParameter.ParameterProperties.Add(chained);
```

This one-element chain keeps the example short. For nested properties, supply each
`PropertyInfo` in navigation order. No reflected properties are required by the direct
getter descriptors that generated form code uses.

## Match a named client

`UniqueName.ForType<T>()` and `UniqueName.ForType(Type)` return the same generated
implementation name. Refit also uses this name for its HTTP client factory registration, as
described in the [Refit README](https://github.com/reactiveui/refit/blob/main/README.md#using-httpclientfactory).
Use the method when configuring the same named client instead of rebuilding the naming
scheme yourself. It includes the sanitized interface assembly name, namespace and nested
type name, generic arguments when present, and assembly identity.

Each overload also has a `serviceKey` variant. Null and an empty string add no suffix.
Other keys append `, ServiceKey=` followed by the key's string representation. Supply
the same key used for registration; this method neither registers nor resolves a service.

```csharp
string generatedName = UniqueName.ForType<IHelperApi>();
string runtimeName = UniqueName.ForType(interfaceType);
string keyedName = UniqueName.ForType<IHelperApi>("primary");
string runtimeKeyedName = UniqueName.ForType(interfaceType, "primary");
```

Here `interfaceType` is `typeof(IHelperApi)`. The sample checks that both pairs match,
that null or empty keys preserve the base name, and that `Type.GetType(generatedName)`
resolves the emitted implementation of `IHelperApi`.

## Method record reference

`RestMethodInfo` stores the five values below. None of its constructor arguments has a default.
The record methods compare or copy those stored values. They do not inspect the service or send requests.

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `RestMethodInfo(string Name, Type HostingType, MethodInfo MethodInfo, string RelativePath, Type ReturnType)` | Packages the reflected details that identify one Refit method. | [string] `Name`: method name; [Type] `HostingType`: declaring interface; [MethodInfo] [`MethodInfo`](https://learn.microsoft.com/dotnet/api/system.reflection.methodinfo): reflected method; [string] `RelativePath`: route template; [Type] `ReturnType`: declared result type. | A [`RestMethodInfo`](method-metadata.md) containing the supplied metadata. |
| `Deconstruct(out string Name, out Type HostingType, out MethodInfo MethodInfo, out string RelativePath, out Type ReturnType)` | Splits the record into its positional values for deconstruction syntax. | The five `out` arguments receive the corresponding properties below, in constructor order. | `void`; copies the stored values to the arguments. |
| `Equals(RestMethodInfo? other)` | Compares this record with another method record. | `other`: another method record, or `null`. | [bool]: `true` when all five properties are equal; `false` for `null`. |
| `Equals(object? obj)` | Compares this record with an arbitrary object of the same record type. | [object] `obj`: any object, or `null`. | [bool]: `true` only for a [`RestMethodInfo`](method-metadata.md) with equal properties. |
| `operator ==(RestMethodInfo? left, RestMethodInfo? right)` | Tests two records for value equality. | `left`, `right`: records to compare. Both may be `null`. | [bool]: `true` for equal records or two nulls. |
| `operator !=(RestMethodInfo? left, RestMethodInfo? right)` | Tests two records for unequal values. | `left`, `right`: records to compare. Both may be `null`. | [bool]: the opposite of `==`. |
| `GetHashCode()` | Produces a hash for use in hash-based collections. | None. | [int]: a hash based on the stored values. Equal records have equal hashes. |
| `ToString()` | Renders the record and its values for diagnostics. | None. | [string]: the record name and its property names and values. |
| `<Clone>$()` (compiler member used by `with`) | Makes the shallow copy used by a C# `with` expression. | None. Use a [with expression](https://learn.microsoft.com/dotnet/csharp/language-reference/operators/with-expression) in C# rather than calling this metadata name. | A shallow [`RestMethodInfo`](method-metadata.md) copy. The reflected objects are shared with the original. |

| Property | Type | Value and access |
| --- | --- | --- |
| `Name` | [string] | Method name supplied to the constructor; `get; init;`. |
| `HostingType` | [Type] | Declaring interface supplied to the constructor; `get; init;`. |
| `MethodInfo` | [MethodInfo] | Reflected method supplied to the constructor; `get; init;`. |
| `RelativePath` | [string] | Route template supplied to the constructor; `get; init;`. |
| `ReturnType` | [Type] | Declared result type supplied to the constructor; `get; init;`. |

## Parameter metadata reference

The constructor names identify the type they create. All arguments are required.

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `RestMethodParameterInfo(string name, ParameterInfo parameterInfo)` | Describes a route parameter by its binding name. | [string] `name`: route parameter name; [ParameterInfo] `parameterInfo`: reflected parameter. | A named parameter description with `IsObjectPropertyParameter = false`. |
| `RestMethodParameterInfo(bool isObjectPropertyParameter, ParameterInfo parameterInfo)` | Describes a parameter whose properties supply route values. | [bool] `isObjectPropertyParameter`: whether the binding reads object properties; [ParameterInfo] `parameterInfo`: reflected parameter. | A parameter description with the supplied flag and `Name = null`. |
| `RestMethodParameterProperty(string name, PropertyInfo propertyInfo)` | Describes one direct property used in route binding. | [string] `name`: route binding name; [PropertyInfo] `propertyInfo`: property to read. | A property description with a one-element navigation chain. |
| `RestMethodParameterProperty(string name, IReadOnlyList<PropertyInfo> propertyChain)` | Describes a nested property walk used in route binding. | [string] `name`: route binding name; [`IReadOnlyList<PropertyInfo>`][property-list] `propertyChain`: non-empty chain of [PropertyInfo] objects in navigation order. | A property description that retains the list and uses its final element as [`PropertyInfo`](https://learn.microsoft.com/dotnet/api/system.reflection.propertyinfo). |

| Property | Type | Value and access |
| --- | --- | --- |
| `RestMethodParameterInfo.Name` | [string], nullable | Name supplied to the named constructor, or `null` for the flag constructor; `get; set;`. |
| `RestMethodParameterInfo.ParameterInfo` | [ParameterInfo] | Reflected parameter supplied to either constructor; `get; set;`. |
| `RestMethodParameterInfo.IsObjectPropertyParameter` | [bool] | Whether the binding reads object properties; defaults to `false` in the named constructor; `get; set;`. |
| `RestMethodParameterInfo.ParameterProperties` | [`List<RestMethodParameterProperty>`][parameter-property-list] | Starts empty. The list can be replaced during initialization and its contents can be changed later; `get; init;`. |
| `RestMethodParameterInfo.Type` | [`ParameterType`](method-metadata.md) | Starts as `Normal`; `get; set;`. See the values below. |
| `RestMethodParameterProperty.Name` | [string] | Binding name supplied to either constructor; `get; set;`. |
| `RestMethodParameterProperty.PropertyInfo` | [PropertyInfo] | Final property to read; `get; set;`. Assigning it does not change `PropertyChain`. |
| `RestMethodParameterProperty.PropertyChain` | [`IReadOnlyList<PropertyInfo>`][property-list] | Ordered navigation chain; `get; set;`. Assigning it does not change [`PropertyInfo`](https://learn.microsoft.com/dotnet/api/system.reflection.propertyinfo). |

| `ParameterType` value | Numeric value | Meaning |
| --- | --- | --- |
| `Normal` | `0` | Ordinary route value escaping. |
| `RoundTripping` | `1` | Catch-all path handling that retains `/` separators. |

## Client name overloads

`UniqueName` is a static helper. `T` or `refitInterfaceType` selects the Refit interface.
The service key arguments are required on their overloads, but accept `null`.

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `UniqueName.ForType<T>()` | Reconstructs the generated implementation name for interface `T`. | None. `T` selects the interface. | [string]: generated implementation name, including assembly identity. |
| `UniqueName.ForType<T>(object? serviceKey)` | Adds a service-key suffix when naming interface `T`. | [object] `serviceKey`: key used for registration, or `null`. | [string]: generated name with a service-key suffix, unless the key is `null` or an empty string. |
| `UniqueName.ForType(Type refitInterfaceType)` | Reconstructs a generated implementation name from a runtime interface type. | [Type] `refitInterfaceType`: interface to name. | [string]: the same name as the generic overload for that interface. |
| `UniqueName.ForType(Type refitInterfaceType, object? serviceKey)` | Reconstructs a runtime interface name with an optional service-key suffix. | [Type] `refitInterfaceType`: interface to name; [object] `serviceKey`: registration key, or `null`. | [string]: name with the same service-key rules as the generic overload. |

[string]: https://learn.microsoft.com/dotnet/api/system.string
[Type]: https://learn.microsoft.com/dotnet/api/system.type
[MethodInfo]: https://learn.microsoft.com/dotnet/api/system.reflection.methodinfo
[ParameterInfo]: https://learn.microsoft.com/dotnet/api/system.reflection.parameterinfo
[PropertyInfo]: https://learn.microsoft.com/dotnet/api/system.reflection.propertyinfo
[object]: https://learn.microsoft.com/dotnet/api/system.object
[bool]: https://learn.microsoft.com/dotnet/api/system.boolean
[int]: https://learn.microsoft.com/dotnet/api/system.int32
[property-list]: https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1
[parameter-property-list]: https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1

Source: [method record](https://github.com/reactiveui/refit/blob/main/src/Refit/RestMethodInfo.cs),
[parameter metadata](https://github.com/reactiveui/refit/blob/main/src/Refit/RestMethodParameterInfo.cs),
[property chains](https://github.com/reactiveui/refit/blob/main/src/Refit/RestMethodParameterProperty.cs),
[parameter kinds](https://github.com/reactiveui/refit/blob/main/src/Refit/ParameterType.cs),
and [generated names](https://github.com/reactiveui/refit/blob/main/src/Shared/UniqueName.cs).
