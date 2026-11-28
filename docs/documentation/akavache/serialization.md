---
Order: 5
---
# Serialization

Akavache stores bytes. To save a .NET object as JSON, add `Akavache.SystemTextJson` and import `Akavache` and `Akavache.SystemTextJson`. Apps using System.Reactive can use `Akavache.SystemTextJson.Reactive` and the corresponding `Akavache.Reactive` namespaces. Both versions run the [order cache example](https://github.com/reactiveui/Akavache/tree/main/src/examples).

## Save an order with generated JSON metadata

Declare the model and register it with a JSON serializer context. The compiler generates the code that reads and writes the order. This example uses camel-case property names on disk.

```csharp
internal sealed record Order(int Number, string Customer);

[JsonSerializable(typeof(Order))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class OrderJsonContext : JsonSerializerContext;
```

Register the serializer before adding the cache. Call `WithSerializerSystemTextJson()` for default settings, pass an existing options object as below, or call `UseSystemTextJsonSerializer(configure)` to configure options with a delegate.

```csharp
var instance = CacheDatabase.CreateBuilder("OrderCache")
    .WithSerializerSystemTextJson(OrderJsonContext.Default.Options)
    .WithInMemory()
    .Build();
using var cache = instance.InMemory!;
```

Pass the generated `JsonTypeInfo<Order>` to both serializer calls. Store the bytes under a key and supply the same type when retrieving them. The cache scheduler supplies the clock for the expiration time. Here `token` is the caller's cancellation token.

```csharp
const string key = "orders/42";
var metadata = OrderJsonContext.Default.Order;
var bytes = SystemJsonSerializer.SerializeAot(new Order(42, "Ada"), metadata);
await cache.Insert(key, bytes, typeof(Order), cache.Scheduler.Now.AddHours(1)).ToTask(token);

var stored = await cache.Get(key, typeof(Order)).ToTask(token);
var order = SystemJsonSerializer.DeserializeAot(stored!, metadata);
// order is Order(42, "Ada").
```

## Read an existing cache

The metadata overloads on `SystemJsonBsonSerializer` read and write plain JSON. Reading historical binary BSON requires the serializer's BSON-capable deserialization path in an untrimmed application. Before switching serializers, back up the cache and test with records written by the old version. See [migration](migration.md).
