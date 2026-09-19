---
Order: 6
---
# Local request context

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/requests-request-context/requests-request-context.csproj).

Some information is useful to your HTTP code even though the service should not receive it.
For example, a handler may need a tenant identifier to choose credentials, or a label to add
to a log entry. Refit lets you attach these values to the request as local properties.

Your handlers can read them before sending the request. They stay in your app unless your
own code chooses to put them in a header, URL or body.

## Attach and read context

**1. Mark a method argument.** The `BuildAsync` method on [the header interface](headers.md#build-a-request-with-headers)
uses `Property("tenant")` for an explicit key. `Property` on `page` uses the parameter name as its key.
The request takes both values from the method call.

**2. Build the request.** The complete [request-context example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Headers/RequestContext.cs)
builds a request for `customer-a` with page `1`.

**3. Read its options.** On modern .NET, use `HttpRequestMessage.Options` with a matching key and value type.
The example also reads Refit's method name and route template.

```csharp
_ = request.Options.TryGetValue(new("tenant"), out string? tenant);
_ = request.Options.TryGetValue(new("page"), out int page);
_ = request.Options.TryGetValue(new(HttpRequestMessageOptions.MethodName), out string? method);
_ = request.Options.TryGetValue(new(HttpRequestMessageOptions.RelativePathTemplate), out string? route);
Console.WriteLine(tenant); // customer-a
Console.WriteLine(page); // 1
Console.WriteLine(method); // BuildAsync
Console.WriteLine(route); // /headers
```

Use the same option reads inside your handler's `SendAsync` method.
On older .NET targets, use `HttpRequestMessage.Properties` to read the keys.
Both the parameterless and explicit-key `PropertyAttribute` constructors support this context.
`PropertyAttribute.Key` is null when Refit should use the parameter name.

## Context for all calls

`RefitSettings.HttpRequestMessageOptions` adds a dictionary of local values to each request.
The sample adds `trace-category` with the value `delivery`.
Configure shared settings before you start making calls.

`CaptureMethodArguments` also attaches an `object?[]` under the method-arguments key.
It keeps the arguments in declaration order, including a cancellation token.
It defaults to false. Enable it only when a handler needs those values.
It creates an array for each call and keeps the argument objects alive with the request.
Keep tokens and private user data out of logs.

## Refit's option keys

`HttpRequestMessageOptions` exposes these string keys.

| Key property | Value and use |
| --- | --- |
| `InterfaceType` | The top-level interface type for the call. |
| `MethodName` | The declared method name, such as `BuildAsync`. |
| `RelativePathTemplate` | The unfilled route, such as `/people/{id}`. Use this stable name for request metrics. |
| `RestMethodInfo` | Reflected method details when the request-building path supplies them. Generated requests avoid this reflection. |
| `MethodArguments` | The argument array when `CaptureMethodArguments` is true. |
| `RequestContent` | The captured body text when `CaptureRequestContent` is true. |

A route template groups calls to different IDs under the same metric name.
Use it instead of putting each person's URL into a separate metric group.

## Read send-time metadata

An interface property marked `Property` supplies context for its client instance.
Settings options are applied first, then interface properties, then method arguments.
When they share a key, the later value wins. The `IContextApi` shown on
[the headers page](headers.md#choose-an-authorization-scheme-explicitly) uses the same `tenant` key
for all three sources. Its `SaveAsync` call supplies `call-tenant`.

The complete sample enables `CaptureRequestContent` and `CaptureMethodArguments`, then sends a person
through a local handler. That handler observes the request used below. Its serializer uses the generated
`SampleJsonContext` from [the AOT setup](../aot.md#make-one-call-ready-for-aot), which includes `Person`.
`TenantKey` is the constant `"tenant"`.


```csharp
_ = request.Options.TryGetValue(new(HttpRequestMessageOptions.InterfaceType), out Type? interfaceType);
bool hasReflectedInfo = request.Options.TryGetValue(new(HttpRequestMessageOptions.RestMethodInfo), out object? reflectedInfo);
_ = request.Options.TryGetValue(new(HttpRequestMessageOptions.MethodArguments), out object?[]? arguments);
_ = request.Options.TryGetValue(new(HttpRequestMessageOptions.RequestContent), out string? body);
_ = request.Options.TryGetValue(new(TenantKey), out string? tenant);
Console.WriteLine(interfaceType == typeof(IContextApi)); // True
Console.WriteLine(hasReflectedInfo); // False for this generated method
Console.WriteLine(arguments?.Length); // 3, including CancellationToken
Console.WriteLine(body); // JSON: {"id":1,"name":"Ada"}.
Console.WriteLine(tenant); // call-tenant
```

The generated method supplies `InterfaceType`, method name and route template without reflection.
It does not add `RestMethodInfo`. Body capture reads the serialized content before sending and stores
that text under `RequestContent`. An unsent `Task<HttpRequestMessage>` result has not run this send-time
capture step. Requests with no body do not get that option.
