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
implementation name. Refit also uses this name for its HTTP client factory registration.
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

Source: [method record](https://github.com/reactiveui/refit/blob/main/src/Refit/RestMethodInfo.cs),
[parameter metadata](https://github.com/reactiveui/refit/blob/main/src/Refit/RestMethodParameterInfo.cs),
[property chains](https://github.com/reactiveui/refit/blob/main/src/Refit/RestMethodParameterProperty.cs),
[parameter kinds](https://github.com/reactiveui/refit/blob/main/src/Refit/ParameterType.cs),
and [generated names](https://github.com/reactiveui/refit/blob/main/src/Shared/UniqueName.cs).
