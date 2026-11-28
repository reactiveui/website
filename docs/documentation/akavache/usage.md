---
Order: 4
---
# Usage

Common operations:
```csharp
// strings/bytes
await ak.LocalMachine.Insert("msg", Encoding.UTF8.GetBytes("hello"));
var bytes = await ak.LocalMachine.Get("msg");

// POCO
await ak.LocalMachine.InsertObject("user", user);
var user2 = await ak.LocalMachine.GetObject<User>("user");

// GetOrFetch with expiration (throttles dup requests)
var posts = await ak.LocalMachine.GetOrFetchObject("posts",
    () => api.GetPostsAsync(), DateTimeOffset.Now.AddMinutes(5));

// invalidate
await ak.LocalMachine.Invalidate("user");
await ak.LocalMachine.InvalidateAll();
```

## Cache a download

Register an `IHttpService` with `cache.SetHttpService(service)` to choose the transport
for one cache. A key lets the application name the cached data independently of the URL.
The first download below keeps its result for an hour. The second reads the stored bytes.
Pass `fetchAlways: true` when the user asks for fresh instructions.

```csharp
var address = new Uri("https://dispatch.example/orders/42/delivery");
const string key = "orders/42/delivery";
var expires = cache.Scheduler.Now.AddHours(1);
byte[] original = await cache.DownloadUrl(key, address, false, expires).ToTask(token);
byte[] cached = await cache.DownloadUrl(key, address).ToTask(token);
byte[] refreshed = await cache.DownloadUrl(key, address, fetchAlways: true).ToTask(token);
```

The example changes the reply between reads so you can see which call reaches the service. [Delivery instructions example](https://github.com/reactiveui/Akavache/blob/main/src/examples/OrderCache/DeliveryInstructions.cs).
