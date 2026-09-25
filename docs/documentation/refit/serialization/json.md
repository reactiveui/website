---
Order: 1
---
# JSON and generated metadata

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/serialization-json/serialization-json.csproj).

Refit writes your C# models into request bodies and reads reply bodies back into models.
It uses System.Text.Json for the JSON. By default, System.Text.Json inspects each model while the app runs
to find its properties. That inspection is called *reflection-based JSON*.
Trimming removes code that an app seems not to use, and Native AOT compiles the app ahead of time.
Both can remove code that reflection needs, so a reflection-based app can fail after you publish it.

A *JSON context* replaces that inspection. Refit takes advantage of this System.Text.Json
[source generation](https://learn.microsoft.com/dotnet/standard/serialization/system-text-json/source-generation) feature.
You declare a `partial` class that derives from `JsonSerializerContext` and list your model types on it.
The System.Text.Json source generator fills the class in during the build with *metadata*: the property names and the
reading and writing code for each listed type. Give the context to Refit, and Refit reads and writes
those models without reflection.

This page starts with one model and one call. It then covers the naming trap, your own serializer settings,
methods that take metadata, and the serializer APIs. The context APIs are available on .NET 8 and later.

Refit's generator does not write `[JsonSerializable]` entries for you. You list your types on the context yourself.
The compiler runs Refit's generator and the JSON generator separately, and neither can read what the other writes.

## Register a context

**1. Declare the models.** This example uses the same `Person` as the first-request walkthrough.

```csharp
internal sealed record Person(int Id, string Name);
```

**2. Declare the context.** Derive a partial class from `JsonSerializerContext`.
The class name is your choice. This page calls it `SampleJsonContext`.
Add `JsonSerializable` for each root type you send and receive.
Register a collection or closed generic shape when that is the request or reply type.
Add `JsonSourceGenerationOptions(JsonSerializerDefaults.Web)` too. [The next section](#add-the-web-defaults) explains why.

```csharp
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(Person[]))]
[JsonSerializable(typeof(List<Person>))]
internal sealed partial class SampleJsonContext : JsonSerializerContext;
```

Here `Person`, `Person[]` and `List<Person>` are separate JSON roots.
Model properties also contribute their declared types. An `object` property needs each possible runtime type
registered too. The System.Text.Json source generator fills in the context when you build.
[Microsoft's source-generation guide](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation)
describes these registrations.

**3. Give the context to Refit.** `RestService.ForGenerated<T>` takes the context.
Refit reads and writes JSON with the context's own options. Those options hold whatever
`JsonSourceGenerationOptions` sets: the naming, the converters and the number handling.
Refit adds no converters of its own.

**4. Call the API.** `client` is your `HttpClient` with its `BaseAddress` set.

```csharp
IPeopleApi api = RestService.ForGenerated<IPeopleApi>(client, SampleJsonContext.Default);
Person person = await api.GetPersonAsync(1, CancellationToken.None);
Console.WriteLine(person.Name); // Ada
```

Every place that creates or registers a generated client has an overload that takes a context.

| To | Call |
| --- | --- |
| Create a client | `RestService.ForGenerated<T>(client, context)` or `RestService.ForGenerated<T>(hostUrl, context)`. See [create a client](../clients/creation.md). |
| Register a client with dependency injection | `services.AddRefitGeneratedClient<T>(context)` or `services.AddKeyedRefitGeneratedClient<T>(key, context)`. See [dependency injection](../clients/dependency-injection.md). |
| Build settings | `RefitSettings.ForJsonContext(context)`, or `settings.UseJsonContext(context)` on settings you have. See [settings](../clients/settings.md). |
| Build a serializer | `SystemTextJsonContentSerializer.ForContext(context)`, or `serializer.WithContext(context)` on a serializer you have. |

Each overload also takes `allowReflectionFallback`. See [reflection-based JSON is off](#reflection-based-json-is-off).

## Add the web defaults

Put `JsonSerializerDefaults.Web` on every context. A context with no `JsonSourceGenerationOptions` uses the
System.Text.Json defaults. Those defaults expect PascalCase property names and match names by exact case.
Most services send camelCase names, such as `{"id":5,"customer":"Ada"}`.
A plain context reads such a reply into an object whose properties hold default values.
Refit reports no error.

The rest of this page uses a shop's orders API. `OrdersServer.OrderId` in the code is the number 5.
`OrderStatus` is an enum that the JSON writes as text, such as `"Shipped"`.

```csharp
internal sealed record OrderLine(string Sku, int Quantity, decimal UnitPrice);

internal sealed record Order(int Id, string Customer, OrderStatus Status, List<OrderLine> Lines);

internal sealed record NewOrder(string Customer, List<OrderLine> Lines);
```

```csharp
internal interface IOrdersApi
{
    [Get("/orders/{id}")]
    Task<Order> GetOrderAsync(int id, CancellationToken cancellationToken);

    [Get("/orders")]
    Task<List<Order>> ListOrdersAsync(CancellationToken cancellationToken);

    [Post("/orders")]
    Task<Order> PlaceOrderAsync([Body] NewOrder order, CancellationToken cancellationToken);

    [Get("/orders/{id}/shipment")]
    Task<Shipment> GetShipmentAsync(int id, CancellationToken cancellationToken);
}
```

This context has no options. It reads the shop's camelCase reply as an empty order:

```csharp
[JsonSerializable(typeof(Order))]
[JsonSerializable(typeof(List<Order>))]
[JsonSerializable(typeof(NewOrder))]
[JsonSerializable(typeof(Shipment))]
internal sealed partial class UnconfiguredOrdersJsonContext : JsonSerializerContext;
```

```csharp
IOrdersApi unconfigured = RestService.ForGenerated<IOrdersApi>(client, UnconfiguredOrdersJsonContext.Default);
Order empty = await unconfigured.GetOrderAsync(OrdersServer.OrderId, CancellationToken.None);
Console.WriteLine($"{empty.Id} {empty.Customer is null}"); // 0 True
```

The web defaults fix it. They select camelCase names and case-insensitive matching.
They also let a number arrive as a string.

```csharp
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(Order))]
[JsonSerializable(typeof(List<Order>))]
[JsonSerializable(typeof(NewOrder))]
[JsonSerializable(typeof(Shipment))]
[JsonSerializable(typeof(PickupShipment))]
internal sealed partial class OrdersJsonContext : JsonSerializerContext;
```

```csharp
IOrdersApi web = RestService.ForGenerated<IOrdersApi>(client, OrdersJsonContext.Default);
Order order = await web.GetOrderAsync(OrdersServer.OrderId, CancellationToken.None);
Console.WriteLine($"{order.Id} {order.Customer}"); // 5 Ada
```

## Choose whose settings apply

Your own serializer settings win. With none, the context is the settings.

| You give Refit | Refit uses |
| --- | --- |
| A context | The context's own options. `JsonSourceGenerationOptions` sets the naming, converters and number handling. |
| A context and settings | Your serializer options. Your naming policy, converters and number handling apply. The context supplies the metadata for the types it lists. |
| Settings whose options you built | Your options exactly as you built them. You choose the `TypeInfoResolver`. |

In the second row the naming in the context's own `JsonSourceGenerationOptions` does not apply.
A context that says kebab-case, under settings that say camelCase, writes camelCase.

### Build the options yourself

Build your own `JsonSerializerOptions`, wrap them in a `SystemTextJsonContentSerializer`,
and pass `new RefitSettings(serializer)` to `ForGenerated`. Pick this form when you have
options to share, need full control, or want one options object reused elsewhere in your app.
The short path needs no options object. This form makes you assign the `TypeInfoResolver` yourself
and keep the options unchanged after the serializer first uses them.
A `TypeInfoResolver` tells the serializer where to find the metadata for each type. Give it the context.

This app writes snake_case names and prices as text. `PriceJsonConverter` reads and writes a `decimal`
as text with two decimal places, such as `"49.50"`.

```csharp
internal sealed class PriceJsonConverter : JsonConverter<decimal>
{
    private const string PriceFormat = "F2";

    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        decimal.Parse(reader.GetString()!, CultureInfo.InvariantCulture);

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString(PriceFormat, CultureInfo.InvariantCulture));
}
```

```csharp
private static readonly JsonSerializerOptions AppOptions = new(JsonSerializerDefaults.Web)
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Converters = { new PriceJsonConverter() },
    TypeInfoResolver = OrdersJsonContext.Default,
};
```

```csharp
SystemTextJsonContentSerializer serializer = new(AppOptions);
RefitSettings settings = new(serializer);

IOrdersApi api = RestService.ForGenerated<IOrdersApi>(client, settings);

Order order = await api.GetOrderAsync(OrdersServer.OrderId, CancellationToken.None);
Console.WriteLine(order.Lines[0].UnitPrice); // 49.50
```

### Add a context to settings you have

If you have settings and want a context too, skip the hand-assigned resolver.
`ForGenerated(client, context, settings)` and `settings.UseJsonContext(context)` are the bridge:
they keep your settings and add the context. Refit copies your options and adds the context after the
resolvers they hold. It then sets the result as `settings.ContentSerializer`.
These options hold a naming policy and a converter, and no resolver.

```csharp
private static readonly JsonSerializerOptions AppOptions = new(JsonSerializerDefaults.Web) { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower, Converters = { new PriceJsonConverter() } };
```

```csharp
RefitSettings settings = new(new SystemTextJsonContentSerializer(AppOptions));

IOrdersApi api = RestService.ForGenerated<IOrdersApi>(client, OrdersJsonContext.Default, settings);

Order order = await api.GetOrderAsync(OrdersServer.OrderId, CancellationToken.None);
Console.WriteLine(order.Lines[0].UnitPrice); // 49.50
```

The context's own naming is camelCase. The reply and the request body use snake_case names and text prices anyway,
because your settings win.

`UseJsonContext` returns the same settings, so the settings work with every creation method,
including the reflection methods such as `RestService.For<T>(hostUrl, settings)`.
It throws `InvalidOperationException` when the serializer is not a `SystemTextJsonContentSerializer`.
Adding a context that the serializer holds changes nothing.

```csharp
RefitSettings registered = settings.UseJsonContext(OrdersJsonContext.Default);
IOrdersApi api = RestService.ForGenerated<IOrdersApi>(client, settings);
```

### Keep polymorphism you register in code

A *polymorphic* type is a base type whose JSON can hold several derived types. A property in the JSON, the
*discriminator*, names the derived type. You can declare this with attributes on the type.
The context reads those attributes for you. `Shipment` maps `"courier"` to `CourierShipment` this way.

```csharp
[JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
[JsonDerivedType(typeof(CourierShipment), "courier")]
internal abstract record Shipment(string Reference);

internal sealed record CourierShipment(string Reference, string TrackingNumber) : Shipment(Reference);

internal sealed record PickupShipment(string Reference, string Store) : Shipment(Reference);
```

You can also register a derived type in code, with a *contract modifier*. That is a method that changes a
type's metadata before the serializer uses it. Refit moves the contract modifiers of a `DefaultJsonTypeInfoResolver`
in your options onto the context, so the registration keeps working. This modifier adds a `pickup` kind to `Shipment`.

```csharp
private static readonly JsonSerializerOptions ModifiedOptions = new(JsonSerializerDefaults.Web) { TypeInfoResolver = new DefaultJsonTypeInfoResolver { Modifiers = { RegisterPickupShipments } } };
```

```csharp
private static void RegisterPickupShipments(JsonTypeInfo typeInfo)
{
    if (typeInfo.Type == typeof(Shipment) && typeInfo.PolymorphismOptions is { } polymorphism)
    {
        polymorphism.DerivedTypes.Add(new(typeof(PickupShipment), PickupKind));
    }
}
```

```csharp
RefitSettings settings = new(new SystemTextJsonContentSerializer(ModifiedOptions));

IOrdersApi api = RestService.ForGenerated<IOrdersApi>(client, OrdersJsonContext.Default, settings);

Shipment shipment = await api.GetShipmentAsync(OrdersServer.OrderId, CancellationToken.None);
Console.WriteLine(shipment.GetType().Name); // PickupShipment
```

A derived type that you register in code needs its own `[JsonSerializable]` entry on the context.
The example's context class, `OrdersJsonContext`, lists `PickupShipment` for that reason. `PickupKind` is the text `"pickup"`.
`DefaultJsonTypeInfoResolver` uses reflection, so the Native AOT example leaves this scenario out.
Declare polymorphism with attributes when you publish with Native AOT.

## Reflection-based JSON is off

When you give Refit a context, reflection-based JSON is off. A type that the context does not list throws
`NotSupportedException`. The failure is on purpose. A missing entry then shows up in your tests,
not in production after a trimmed publish.

This context lists `Order` and nothing else, so `NewOrder` has no metadata:

```csharp
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(Order))]
internal sealed partial class OrderReadsJsonContext : JsonSerializerContext;
```

```csharp
IOrdersApi api = RestService.ForGenerated<IOrdersApi>(client, OrderReadsJsonContext.Default);
try
{
    _ = await api.PlaceOrderAsync(newOrder, CancellationToken.None);
    throw new InvalidOperationException("NewOrder has no metadata, so the call should throw.");
}
catch (NotSupportedException error)
{
    Console.WriteLine(error.Message); // JsonTypeInfo metadata for type '...NewOrder' was not provided by TypeInfoResolver of type '...OrderReadsJsonContext'. ...
}
```

The message names the missing type and the context. Add `[JsonSerializable(typeof(NewOrder))]` to fix the call.

To let a type that the context does not list use reflection, pass `allowReflectionFallback: true`.
Refit then adds System.Text.Json's reflection resolver after your context.
That option is not trim or Native AOT safe. Use it in an app that is not trimmed or compiled ahead of time,
for example while you move a large app to a context one type at a time.

```csharp
// Reflection-based JSON is not trim or Native AOT safe.
IOrdersApi fallback = RestService.ForGenerated<IOrdersApi>(client, OrderReadsJsonContext.Default, allowReflectionFallback: true);
Order placed = await fallback.PlaceOrderAsync(newOrder, CancellationToken.None);
Console.WriteLine(placed.Id); // 5
```

## Pass metadata to a method

`JsonTypeInfo<T>` holds the metadata for one type. A context has one property for each type you list.
In this page's context, `SampleJsonContext.Default.Person` is a `JsonTypeInfo<Person>`. The `JsonSerializer` methods in .NET accept it:

```csharp
JsonTypeInfo<Person> personInfo = SampleJsonContext.Default.Person;
string json = JsonSerializer.Serialize(new(1, "Ada"), personInfo);
Person? restored = JsonSerializer.Deserialize(json, personInfo);
Console.WriteLine(restored?.Name); // Ada
```

A Refit interface method can take a `JsonTypeInfo<T>` parameter too. Refit uses the metadata for that call.
Refit does not send the parameter. It is not a route, query, header or body value.
This API reads one order, reads its status and headers, reads a list, streams the orders and places an order:

```csharp
internal interface IOrdersTypeInfoApi
{
    [Get("/orders/{id}")]
    Task<Order> GetOrderAsync(int id, JsonTypeInfo<Order> orderInfo, CancellationToken cancellationToken);

    [Get("/orders/{id}")]
    Task<ApiResponse<Order>> GetOrderResponseAsync(int id, JsonTypeInfo<Order> orderInfo, CancellationToken cancellationToken);

    [Get("/orders/{id}")]
    Task<Order> FindOrderAsync(int id, JsonTypeInfo<Order>? orderInfo = null);

    [Get("/orders")]
    Task<List<Order>> ListOrdersAsync(JsonTypeInfo<List<Order>> ordersInfo, CancellationToken cancellationToken);

    [Get("/orders")]
    IAsyncEnumerable<Order> StreamOrdersAsync(JsonTypeInfo<Order> orderInfo, CancellationToken cancellationToken);

    [Post("/orders")]
    Task<Order> PlaceOrderAsync([Body] NewOrder order, JsonTypeInfo<NewOrder> newOrderInfo, JsonTypeInfo<Order> orderInfo, CancellationToken cancellationToken);
}
```

The client in this example has no context. Each call brings the metadata it needs.

```csharp
IOrdersTypeInfoApi api = RestService.ForGenerated<IOrdersTypeInfoApi>(client);

Order order = await api.GetOrderAsync(OrdersServer.OrderId, OrdersJsonContext.Default.Order, CancellationToken.None);
Console.WriteLine($"{order.Customer} {order.Lines[0].UnitPrice}"); // Ada 49.5

List<Order> orders = await api.ListOrdersAsync(OrdersJsonContext.Default.ListOrder, CancellationToken.None);

await foreach (Order streamed in api.StreamOrdersAsync(OrdersJsonContext.Default.Order, CancellationToken.None))
{
    Console.WriteLine(streamed.Customer); // Ada
}

NewOrder newOrder = new("Ada", [new("KB-1", 1, KeyboardPrice)]);
Order placed = await api.PlaceOrderAsync(newOrder, OrdersJsonContext.Default.NewOrder, OrdersJsonContext.Default.Order, CancellationToken.None);
```

Refit matches a parameter by its `T`:

- **Body.** If `T` is the type of a JSON `[Body]`, Refit writes the body with the metadata.
- **Reply.** If `T` is the type Refit reads the reply as, Refit reads the reply with the metadata.
  That type is the `T` of `Task<T>`, `ApiResponse<T>`, `IAsyncEnumerable<T>` or `IObservable<T>`, or the page type of a
  `PagedEnumerable`. It is `Order`, not `ApiResponse<Order>`.
- **Both.** One method can take two parameters, one for the body and one for the reply, as `PlaceOrderAsync` does.
  When the body and the reply have the same type, one parameter covers both.
- **Lists and streams.** A list reply takes the metadata for the list: `JsonTypeInfo<List<Order>>`, which the
  context exposes as `ListOrder`. A streamed reply takes the metadata for one element.
- **Optional.** A parameter can be nullable with a default, as in `JsonTypeInfo<Order>? orderInfo = null`.
  A `null` value means the serializer looks the metadata up.

```csharp
IOrdersTypeInfoApi api = RestService.ForGenerated<IOrdersTypeInfoApi>(client, OrdersJsonContext.Default);

Order looked = await api.FindOrderAsync(OrdersServer.OrderId);
Order passed = await api.FindOrderAsync(OrdersServer.OrderId, OrdersJsonContext.Default.Order);
```

The metadata's own options apply to that call. They win over the serializer options in your settings.
These settings say snake_case, and the call reads the shop's camelCase reply.

```csharp
IOrdersTypeInfoApi api = RestService.ForGenerated<IOrdersTypeInfoApi>(client, RefitSettings.SnakeCase());

Order order = await api.GetOrderAsync(OrdersServer.OrderId, OrdersJsonContext.Default.Order, CancellationToken.None);
Console.WriteLine(order.Lines[0].UnitPrice); // 49.5
```

The parameter works with buffered and streamed request bodies. It works with streamed replies in every
[format](../results/streaming.md#reply-formats), with `IObservable<T>` replies and with [paged methods](../results/pagination.md).

The content serializer must implement `IJsonTypeInfoContentSerializer`. `SystemTextJsonContentSerializer` does.
With a serializer that does not, the call throws `InvalidOperationException` and the message names the interface.

### Build-time checks

The generator checks each `JsonTypeInfo<T>` parameter when your project builds.
A parameter that Refit cannot use fails the build with `RF014`:
`Method 'X' cannot use its JsonTypeInfo parameter: <reason>`. These are the reasons:

| Reason | What to change |
| --- | --- |
| `JsonTypeInfo<T>` matches neither the JSON body type nor the type the reply is read as | Change `T` to the body type or the reply type, or remove the parameter. A form, JSON Lines or multipart body is not a JSON body, so its type never matches. |
| The method declares more than one `JsonTypeInfo<T>` parameter for the same `T` | Keep one. |
| The request cannot be generated inline | Change the method so that Refit can generate its request. |
| Generated request building is switched off | Switch it on. Only generated requests pass the metadata on. |

## Use the serializer directly

`SystemTextJsonContentSerializer` is the object that writes and reads JSON for Refit. You can call it yourself.
Build one straight from a context. It uses the context's own options and never falls back to reflection:

```csharp
private static readonly SystemTextJsonContentSerializer ContextSerializer = SystemTextJsonContentSerializer.ForContext(SampleJsonContext.Default);
```

Or build one from options you own. Copy the context's options so its naming rules stay aligned,
and set `TypeInfoResolver` to that same context.
An `IJsonTypeInfoResolver` supplies JSON metadata for a requested type.

```csharp
private static readonly JsonSerializerOptions Options = new(SampleJsonContext.Default.Options) { TypeInfoResolver = SampleJsonContext.Default, };

private static readonly SystemTextJsonContentSerializer Serializer = new(Options);
```

Give this serializer to `new RefitSettings(Serializer)` and use those settings with your generated client.
Keep the options unchanged after the serializer starts using them.
`serializer.WithContext(context)` is the other bridge. It keeps a serializer you have and adds a context.

```csharp
RefitSettings settings = new(Serializer);
IPeopleApi withSettings = RestService.ForGenerated<IPeopleApi>(client, settings);
Person fromSettings = await withSettings.GetPersonAsync(1, CancellationToken.None);
Console.WriteLine(fromSettings.Name); // Ada
```

These methods form the `IHttpContentSerializer` contract.
`FromHttpContentAsync<T>` also accepts a cancellation token and can return null for JSON null.

```csharp
SystemTextJsonContentSerializer serializer = Serializer;
using HttpContent content = serializer.ToHttpContent(new Person(1, "Ada"));
Person? person = await serializer.FromHttpContentAsync<Person>(content);
Console.WriteLine(person?.Name); // Ada
```

The example owns the content it creates directly and disposes it.
When Refit creates content for an API call, the request owns it.
The serializer can return null for JSON null. Malformed JSON raises a `JsonException`.
If you declare a body as an interface or abstract type, the serializer normally uses its runtime type.
Register that concrete model too. JSON polymorphism settings can keep the declared type's contract.
See: [the runnable serializer examples](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Serialization).

## Serializer capabilities

The built-in serializer implements `IHttpContentSerializer` and four optional capabilities.
Other serializers can implement the capabilities that they support.

| Interface | Description |
| --- | --- |
| [`IHttpContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/IHttpContentSerializer.cs) | Defines the required request-body writer, response-body reader and reflected property-name hook. |
| [`ISynchronousContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/ISynchronousContentSerializer.cs) | Adds synchronous buffered and streamed request-body writers. Refit uses them for `Buffered` and `Streamed` request-body modes. |
| [`ISynchronousContentDeserializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/ISynchronousContentDeserializer.cs) | Adds a reader for an error body that Refit has already buffered as a [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string). |
| [`IStreamingContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/IStreamingContentSerializer.cs) | Adds an incremental response reader for Refit interface methods that return [`IAsyncEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1). |
| [`IJsonTypeInfoContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/IJsonTypeInfoContentSerializer.cs) | Adds writers and readers that take a [`JsonTypeInfo<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1) for one call. Refit uses them for a method that has a `JsonTypeInfo<T>` parameter. Only `SystemTextJsonContentSerializer` implements it. Available on .NET 8 and later. |

The [complete serialization example](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Serialization)
exercises each capability with generated metadata and local content. Use the interface that matches
the work your serializer needs to perform; `SystemTextJsonContentSerializer` implements all five.

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`SystemTextJsonContentSerializer()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates a serializer with Refit's default JSON configuration. | None | Creates and retains a new [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) from `GetDefaultJsonSerializerOptions()`. |
| [`SystemTextJsonContentSerializer(JsonSerializerOptions jsonSerializerOptions)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates a serializer with the supplied JSON configuration. | `jsonSerializerOptions`: [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) that controls JSON conversion and metadata lookup. | Retains and uses the supplied [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) instance. |
| [`ForContext(JsonSerializerContext context)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.JsonContext.cs) | Creates a serializer that runs on a context's own options, with reflection-based JSON off. | `context`: [`JsonSerializerContext`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonserializercontext), the generated context. | Returns a `SystemTextJsonContentSerializer` whose options are the context's options. A type the context does not list throws [`NotSupportedException`](https://learn.microsoft.com/en-us/dotnet/api/system.notsupportedexception). Available on .NET 8 and later. |
| [`ForContext(JsonSerializerContext context, bool allowReflectionFallback)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.JsonContext.cs) | Creates a serializer that runs on a context's own options, optionally with a reflection fallback. | `context`: the generated context; `allowReflectionFallback`: `true` lets a type the context does not list use reflection-based JSON. Not trim or Native AOT safe. | Returns a serializer. With `false` it uses the context's options unchanged. With `true` it adds a reflection resolver after the context. |
| [`WithContext(JsonSerializerContext context)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.JsonContext.cs) | Creates a serializer that keeps this serializer's options and adds a context, with reflection-based JSON off. | `context`: the generated context that supplies metadata for the types it lists. | Returns a serializer built on a copy of this serializer's options, or this serializer when the context is registered. The naming policy, converters and other options come from this serializer. Resolvers it holds stay first and in order. Contract modifiers of a reflection resolver move onto the context. |
| [`WithContext(JsonSerializerContext context, bool allowReflectionFallback)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.JsonContext.cs) | Creates a serializer that keeps this serializer's options and adds a context, optionally with a reflection fallback. | `context`; `allowReflectionFallback`: `true` lets a type no resolver lists use reflection-based JSON. Not trim or Native AOT safe. | Returns a serializer built like `WithContext(context)`. With `true` it keeps or adds a reflection resolver after the context. |
| [`SerializerOptions`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Gets the configuration used by this serializer. | None | Returns the same [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) instance passed to the constructor or created by the default constructor. |
| [`GetDefaultJsonSerializerOptions()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates Refit's general-purpose JSON configuration. | None | Returns a fresh mutable [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) with camel-case names, case-insensitive matching, string-number reading, and Refit's object and enum converters. |
| [`GetFastPathJsonSerializerOptions()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates options that can use System.Text.Json's source-generated serialization fast path after you assign generated metadata. | None | Returns a fresh mutable [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) with camel-case names and case-insensitive matching, without Refit converters or custom number handling. |
| [`ToHttpContent<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Serializes a request value through Refit's normal asynchronous JSON-content path. | `item`: `T`, the request value to serialize. | Returns JSON [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent). It uses configured generated metadata when available; an interface or abstract `T` without polymorphism configuration uses the non-null value's runtime type. |
| [`ToHttpContentSynchronous<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Serializes a request value immediately into a buffered JSON body. | `item`: `T`, the request value to serialize. | Returns UTF-8 JSON [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) with a `ByteArrayContent` body and `application/json; charset=utf-8` content type. |
| [`ToStreamingHttpContent<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates a request body that serializes a value when the HTTP request sends it. | `item`: `T`, the request value to serialize. | Returns [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) that writes UTF-8 JSON to the request stream with `application/json; charset=utf-8` content type. |
| [`FromHttpContentAsync<T>(HttpContent content, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Reads a JSON HTTP body as a value. | `content`: [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent), the response body; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken) that cancels the read. Default: `default`. | Returns [`Task<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1) for the deserialized value. |
| [`DeserializeFromString<T>(string content)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Reads an already buffered JSON string. | `content`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), the JSON text. | Returns `T?`, the deserialized value. Invalid JSON throws [`JsonException`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonexception). |
| [`DeserializeStreamAsync<T>(Stream stream, StreamingContentFormat format, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Reads one JSON value at a time from a framed response stream. | `stream`: [`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream), the response body; `format`: [`StreamingContentFormat`](content.md), its JSON array, JSON Lines, or SSE framing; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken) that cancels enumeration. Default: `default`. | Returns [`IAsyncEnumerable<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1) that yields values as they arrive. See [streaming replies](../results/streaming.md). |
| [`GetFieldNameForProperty(PropertyInfo propertyInfo)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Finds a property's explicit JSON field name for reflected integrations. | `propertyInfo`: [`PropertyInfo`](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo), the property to inspect. | Returns the [`JsonPropertyNameAttribute`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute) name, or `null` when the property has no such attribute. |
| [`ToHttpContent<T>(T item, JsonTypeInfo<T> typeInfo)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IJsonTypeInfoContentSerializer.cs) | Serializes a request value with the metadata you pass. | `item`: `T`, the request value; `typeInfo`: [`JsonTypeInfo<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1), the metadata for `T`, such as a property of a context. | Returns JSON [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent). |
| [`ToHttpContentSynchronous<T>(T item, JsonTypeInfo<T> typeInfo)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IJsonTypeInfoContentSerializer.cs) | Serializes a request value immediately into a buffered body with the metadata you pass. | `item`: `T`; `typeInfo`: the metadata for `T`. | Returns UTF-8 JSON [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) with a `ByteArrayContent` body. |
| [`ToStreamingHttpContent<T>(T item, JsonTypeInfo<T> typeInfo)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IJsonTypeInfoContentSerializer.cs) | Creates a body that serializes a value with the metadata you pass when the request sends it. | `item`: `T`; `typeInfo`: the metadata for `T`. | Returns [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) that writes UTF-8 JSON to the request stream. |
| [`FromHttpContentAsync<T>(HttpContent content, JsonTypeInfo<T> typeInfo, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IJsonTypeInfoContentSerializer.cs) | Reads a JSON HTTP body with the metadata you pass. | `content`: the response body; `typeInfo`: the metadata for `T`; `cancellationToken`: cancels the read. Default: `default`. | Returns [`Task<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1) for the value, or `null` for JSON null. |
| [`DeserializeFromString<T>(string content, JsonTypeInfo<T> typeInfo)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IJsonTypeInfoContentSerializer.cs) | Reads buffered JSON text with the metadata you pass. | `content`: the JSON text; `typeInfo`: the metadata for `T`. | Returns `T?`, the deserialized value. |
| [`DeserializeStreamAsync<T>(Stream stream, StreamingContentFormat format, JsonTypeInfo<T> typeInfo, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IJsonTypeInfoContentSerializer.cs) | Reads one value at a time from a framed response stream with the metadata you pass. | `stream`: the response body; `format`: the framing; `typeInfo`: the metadata for one element, whose options drive the read; `cancellationToken`: cancels enumeration. Default: `default`. | Returns [`IAsyncEnumerable<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1) that yields values as they arrive. |

```csharp
SystemTextJsonContentSerializer synchronous = Serializer;
using HttpContent buffered = synchronous.ToHttpContentSynchronous(new Person(1, "Ada"));
using HttpContent streamed = synchronous.ToStreamingHttpContent(new Person(1, "Ada"));
Person? fromText = Serializer.DeserializeFromString<Person>(await buffered.ReadAsStringAsync());
Person? fromStream = await Serializer.FromHttpContentAsync<Person>(streamed);
Console.WriteLine(fromText?.Name); // Ada
Console.WriteLine(fromStream?.Name); // Ada
```

`GetFieldNameForProperty` accepts `PropertyInfo`. It supports reflected integrations and honors
`JsonPropertyName`. It returns null when the property has no such attribute.
It does not apply the options' naming policy here. Generated request maps avoid that reflection step.
The example below supplies a known property carrying an explicit name and a property without that attribute.
The same hook is available through `IHttpContentSerializer`; a custom implementation can return a name or null.

```csharp
internal sealed class JsonNamedValue
{
    [JsonPropertyName("wire-name")]
    public int Value { get; init; }
}
```

```csharp
JsonNamedValue model = new();
System.Reflection.PropertyInfo explicitName = model.GetType().GetProperty(nameof(JsonNamedValue.Value))!;
System.Reflection.PropertyInfo policyOnly = typeof(Person).GetProperty(nameof(Person.Name))!;
string? name = Serializer.GetFieldNameForProperty(explicitName);
string? absent = Serializer.GetFieldNameForProperty(policyOnly);
Console.WriteLine(name); // wire-name
Console.WriteLine(absent is null); // True
```

`DeserializeStreamAsync<T>(Stream, StreamingContentFormat, CancellationToken = default)` accepts
array, JSON Lines or SSE framing. Here `streaming` is the serializer's `IStreamingContentSerializer`
capability, and `body` and `format` select one of those inputs. The caller owns this direct input stream.

```csharp
await using MemoryStream stream = new(Encoding.UTF8.GetBytes(body));
await foreach (Person? item in streaming.DeserializeStreamAsync<Person>(stream, format, CancellationToken.None))
{
    Console.WriteLine(item!.Name);
}
```

For framework-specific newline and BOM behavior, see [content readers](content.md).
For stream framing and cancellation, see [streaming replies](../results/streaming.md).

## Call the serializer with metadata

`IJsonTypeInfoContentSerializer` is for code that calls the serializer directly and has the metadata in hand.
Each method takes a `JsonTypeInfo<T>` in place of a lookup. The context's `Order` property holds the metadata for `Order`.

```csharp
RefitSettings settings = RefitSettings.ForJsonContext(OrdersJsonContext.Default);
if (settings.ContentSerializer is not IJsonTypeInfoContentSerializer serializer)
{
    throw new InvalidOperationException("The System.Text.Json serializer offers the metadata capability.");
}

Order order = new(OrderId, "Ada", OrderStatus.Shipped, [new("KB-1", 1, KeyboardPrice)]);

using HttpContent content = serializer.ToHttpContent(order, OrdersJsonContext.Default.Order);
string json = await content.ReadAsStringAsync();
Console.WriteLine(json);
```

The output is `{"id":5,"customer":"Ada","status":"Shipped","lines":[{"sku":"KB-1","quantity":1,"unitPrice":49.5}]}`.
The other overloads read a body, read a string and write a buffered or streamed body:

```csharp
Order? read = await serializer.FromHttpContentAsync(content, OrdersJsonContext.Default.Order);
Order? fromText = serializer.DeserializeFromString(json, OrdersJsonContext.Default.Order);
Console.WriteLine(read?.Customer); // Ada

using HttpContent buffered = serializer.ToHttpContentSynchronous(order, OrdersJsonContext.Default.Order);
using HttpContent streamed = serializer.ToStreamingHttpContent(order, OrdersJsonContext.Default.Order);
```

`DeserializeStreamAsync` reads a framed body one element at a time. The format here is JSON Lines.
The metadata's options drive the read.

```csharp
using StringContent content = new(body);
await using Stream stream = await content.ReadAsStreamAsync();

int count = 0;
await foreach (Order? item in serializer.DeserializeStreamAsync(stream, StreamingContentFormat.JsonLines, OrdersJsonContext.Default.Order))
{
    count++;
    Console.WriteLine(item?.Customer); // Ada
}
```

## Defaults and fast-path writers

`SystemTextJsonContentSerializer()` creates a set of default options.
The options overload uses your instance. `SerializerOptions` exposes that instance.
`GetDefaultJsonSerializerOptions()` returns fresh mutable options with camel-case names,
case-insensitive property matching, reading numbers from strings and Refit's default converters.
On .NET 10 it also rejects duplicate JSON properties.
The default options have no generated resolver. Configure it before the first JSON operation if this
serializer will run in a native app, or add a context with `WithContext`.
The sample exercises the default constructor and both options APIs,
and checks that each call to `GetDefaultJsonSerializerOptions` returns an independent instance.

```csharp
SystemTextJsonContentSerializer defaults = new();
JsonSerializerOptions defaultOptions = defaults.SerializerOptions;
defaultOptions.TypeInfoResolver = SampleJsonContext.Default;
JsonSerializerOptions separateDefaults = SystemTextJsonContentSerializer.GetDefaultJsonSerializerOptions();
using HttpContent content = defaults.ToHttpContent(new Person(1, "Ada"));
Person? person = await defaults.FromHttpContentAsync<Person>(content, CancellationToken.None);
Console.WriteLine(person!.Name); // Ada
```


`GetFastPathJsonSerializerOptions()` returns fresh options without Refit's default converters.
Set its resolver before use.
A fast-path writer is generated code that writes a registered model directly.
It helps serialization; reading needs metadata.

```csharp
JsonSerializerOptions fastOptions = SystemTextJsonContentSerializer.GetFastPathJsonSerializerOptions();
fastOptions.TypeInfoResolver = SampleJsonContext.Default;
SystemTextJsonContentSerializer fastSerializer = new(fastOptions);
using HttpContent fastContent = fastSerializer.ToHttpContentSynchronous(new Person(1, "Ada"));
Person? fromFastContent = await fastSerializer.FromHttpContentAsync<Person>(fastContent);
Console.WriteLine(fromFastContent?.Name); // Ada
```

Use the context's default generation mode when you both write requests and read replies.
That mode provides metadata and writers. A serialization-only context cannot supply all reply-reading metadata.
Refit's `Buffered` and `Streamed` modes use synchronous JSON writing and can use these generated writers.
Its `Default` mode uses async JSON content. .NET can use the fast path for repeated small async payloads,
but that depends on runtime size checks. Do not assume every async request takes that path.

Check these conditions before choosing the fast path:

| Condition | What to do |
| --- | --- |
| Generated writer exists | Use `Default` or `Serialization` generation mode. Keep metadata too when you read replies. |
| No custom converters | Avoid entries in `JsonSerializerOptions.Converters` and [`JsonConverter`](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter) attributes on the model or its members. |
| Compatible options | Keep naming, ignored-member and null-handling options aligned with the generated context. |
| Supported features | Avoid custom encoders, dictionary key policies and reference handling for this path. |
| Supported number writing | Avoid number handling that changes JSON output, such as `WriteAsString`. `AllowReadingFromString` alone does not block writing. |

Unsupported settings can make System.Text.Json use generated metadata instead of its fast writer.
That fallback can work with AOT. It needs the metadata to be present.
Read [Microsoft Learn's supported options and attributes](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation-modes#serialization-optimization-fast-path-mode)
for the full conditions, fallback behavior and performance guidance.

## Missing metadata and trimming

Trimming removes code that appears unused. Reflection can need a member the trimmer cannot see.
Generated metadata makes the required model contract visible to the build.
Register all request and reply roots, collection shapes, closed generic wrappers and possible runtime types.
In a trimmed or Native AOT app, do not depend on System.Text.Json's reflection fallback. Missing generated
metadata causes JSON operations to fail (the unregistered `Page<Person>` example throws `NotSupportedException`);
register the type in a context instead.

## Combine contexts from separate modules

Keep each module's registrations in its own context. This second context owns a closed generic reply type.

```csharp
internal sealed record Page<T>(T[] Items);
```

```csharp
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(Page<Person>))]
internal sealed partial class PageJsonContext : JsonSerializerContext;
```

Register each extra context on the settings. Refit checks the contexts in the order you add them and uses the
first that supplies the requested type. The options of the first context apply to every context.

```csharp
RefitSettings settings = RefitSettings.ForJsonContext(SampleJsonContext.Default).UseJsonContext(PageJsonContext.Default);
SystemTextJsonContentSerializer registeredSerializer = (SystemTextJsonContentSerializer)settings.ContentSerializer;
using HttpContent registeredContent = registeredSerializer.ToHttpContent(new Page<Person>([new(1, "Ada")]));
Page<Person>? registeredPage = await registeredSerializer.FromHttpContentAsync<Page<Person>>(registeredContent);
Console.WriteLine(registeredPage?.Items[0].Name); // Ada
```

If you build the options yourself, combine the contexts into one resolver.
Both contexts use the web defaults. The resolver checks them in order and uses the first that supplies the requested type.

```csharp
IJsonTypeInfoResolver resolver = JsonTypeInfoResolver.Combine(SampleJsonContext.Default, PageJsonContext.Default);
JsonSerializerOptions combinedOptions = new(SampleJsonContext.Default.Options) { TypeInfoResolver = resolver };
SystemTextJsonContentSerializer combinedSerializer = new(combinedOptions);
using HttpContent pageContent = combinedSerializer.ToHttpContent(new Page<Person>([new(1, "Ada")]));
Page<Person>? page = await combinedSerializer.FromHttpContentAsync<Page<Person>>(pageContent);
Console.WriteLine(page?.Items[0].Name); // Ada
```

See: [Microsoft Learn on combining source generators](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation#combine-source-generators).

## Check missing registrations

An unregistered type fails instead of being discovered through your generated context.
This check uses the first context alone. It has no registration for `Page<Person>`.

```csharp
try
{
    // Options resolves only SampleJsonContext's roots. Page<Person> belongs to PageJsonContext.
    using HttpContent missingContent = Serializer.ToHttpContent(new Page<Person>([]));
    throw new InvalidOperationException("Missing JSON metadata should fail.");
}
catch (NotSupportedException)
{
    Console.WriteLine("Register Page<Person> or combine the generated contexts.");
}
```

Do not add a reflection resolver to hide missing registrations in an AOT app.
Fix the context and check your [native publish](../aot.md).
An ordinary successful build does not prove that the native app has the required metadata.
